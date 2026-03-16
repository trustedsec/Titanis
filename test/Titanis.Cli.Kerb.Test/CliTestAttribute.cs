using System.Reflection;

namespace Titanis.Cli.Kerb.Test;

public sealed class CliTestAttribute : Attribute, ITestDataSource
{
	public CliTestAttribute(string tag)
	{
		this.Tag = tag;
	}

	public string Tag { get; }

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
		var example = FindExample(commandType, this.Tag);
		if (example is null)
			throw new ArgumentException($"No example found on command '{commandType.Name}' matching tag '{this.Tag}' (referenced by test {TestNameFrom(methodInfo)})");
		var args = example.CommandLine;
		if (args.StartsWith("{0}"))
			args = args.Substring(3).TrimStart();

		var argv = CommandLineParser.Tokenize(args);

		return [[argv]];
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
		Type? commandType = FindCommandType(methodInfo);
		var example = FindExample(commandType, this.Tag);
		return (example?.Caption ?? this.Tag);
	}
}
