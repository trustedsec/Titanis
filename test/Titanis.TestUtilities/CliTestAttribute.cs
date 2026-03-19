using System.Collections.Immutable;
using System.Reflection;

namespace Titanis.Cli.Kerb.Test;

public sealed class CliTestAttribute : Attribute, ITestDataSource
{
	public CliTestAttribute(params string[] tags)
	{
		this.Tags = tags;
	}

	public string[] Tags { get; }

	record class TestCaseInfo(ExampleAttribute Example, object[] Args)
	{

	}

	private Dictionary<object[], TestCaseInfo> _testCases;

	private static ExampleAttribute? FindExample(Type commandType, string tag)
	{
		var examples = commandType.GetCustomAttributes<ExampleAttribute>();
		foreach (var example in examples)
		{
			if (example.Tag == tag)
				return example;
		}

		return null;
	}

	private static string TestNameFrom(MethodInfo methodInfo) => $"{methodInfo.DeclaringType.Name}.{methodInfo.Name}";
	public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
	{
		Type? commandType = FindCommandType(methodInfo);
		List<object[]> argsList = new List<object[]>(this.Tags.Length);
		Dictionary<object[], TestCaseInfo> testCases = new Dictionary<object[], TestCaseInfo>();

		foreach (var tag in this.Tags)
		{
			var example = FindExample(commandType, tag);
			if (example is null)
				throw new ArgumentException($"No example found on command '{commandType.Name}' matching tag '{tag}' (referenced by test {TestNameFrom(methodInfo)})");
			var args = example.CommandLine;
			if (args.StartsWith("{0}"))
				args = args.Substring(3).TrimStart();

			var tokens = CommandLineParser.Tokenize(args);
            object[] argv = [tokens];
            argsList.Add(argv);

			testCases.Add(argv, new TestCaseInfo(example,argv));
		}

		this._testCases = testCases;
		return argsList.ToArray();
	}

	private static Type FindCommandType(MethodInfo methodInfo)
	{
		var testType = methodInfo.DeclaringType;
		Type? commandType = null;
		while (commandType is null && testType is not null)
		{
			if (testType.IsGenericType && testType.Name == typeof(CliCommandTest<>).Name)
				commandType = testType.GetGenericArguments()[0];
			else
				testType = testType.BaseType;
		}

		if (commandType is null)
			throw new Exception($"Command type could not be determined for test {TestNameFrom(methodInfo)}.");
		return commandType;
	}

	public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
	{
		if (this._testCases != null && this._testCases.TryGetValue(data, out var test))
			return test.Example.Caption;

		return methodInfo.Name;
		//return $"{methodInfo.Name}({string.Join(",",data)})";
	}
}
