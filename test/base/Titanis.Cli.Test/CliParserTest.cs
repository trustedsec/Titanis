using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;
using System.Threading.Tasks;
using System.Threading;

[assembly: TestDataSourceOptions(TestDataSourceUnfoldingStrategy.Unfold)]

namespace Titanis.Cli.Test
{
	[TestClass]
	public class CliParserTest
	{
		class TestCommandBase : Command
		{
			protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
			{
				return Task.FromResult(0);
			}

		}
		[Command]
		[Description("Text Example of a string command")]
		class StringCommand : TestCommandBase
		{
			[Parameter(0)]
			[Description("This is a string isn't that fun")]
			[Alias("strparm")]
			public string StringParam { get; set; }

		}

		private static TCommand TestCommand<TCommand>(string cmdline)
			where TCommand : Command, new()
		{
			TCommand cmd = new TCommand();
			var tokens = CommandLineParser.Tokenize(cmdline);
			var result = cmd.InvokeAsync("cmd", tokens, 0, CancellationToken.None).Result;
			return cmd;
		}

		[TestMethod]
		[DataRow("str", new string[] { "str" }, DisplayName = "One token")]
		[DataRow("str \"", new string[] { "str", "" }, DisplayName = "Token unclosed quote")]
		[DataRow("\"\"", new string[] { "" }, DisplayName = "Empty quotes")]
		[DataRow("str \"\"\"\"", new string[] { "str", "\"" }, DisplayName = "Token then empty quotes")]
		[DataRow("\"\"\"\" str", new string[] { "\"", "str" }, DisplayName = "Empty quotes then token")]
		[DataRow("str1 \"\"\"\" str2", new string[] { "str1", "\"", "str2" }, DisplayName = "Token empty quotes token")]
		[DataRow("str1 str2", new string[] { "str1", "str2" }, DisplayName = "Two tokens")]
		[DataRow("\"quoted\"", new string[] { "quoted" }, DisplayName = "Quoted token")]
		[DataRow("\"quoted space\"", new string[] { "quoted space" }, DisplayName = "Quoted space")]
		[DataRow("\"quoted unclosed", new string[] { "quoted unclosed" }, DisplayName = "Quoted unclosed")]
		public void TestTokenize(string cmdline, string[] expected)
		{
			var tokens = CommandLineParser.Tokenize(cmdline);
			var actual = Array.ConvertAll(tokens, r => r.Text);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		[DataRow("strvalue", "strvalue")]
		[DataRow("\"strvalue\"", "strvalue")]
		[DataRow("-StringParam strvalue", "strvalue")]
		[DataRow("-StringParam:strvalue", "strvalue")]
		[DataRow("-StringParam \"quoted\"", "quoted")]
		[DataRow("-StringParam:\"quoted\"", "quoted")]
		[DataRow("-StringParam:-dash", "-dash")]
		[DataRow("-StringParam \"-dash\"", "-dash")]
		[DataRow("-StringParam \"quoted space\"", "quoted space")]
		[DataRow("-StringParam quoted\" partial\"", "quoted partial")]
		[DataRow("-StringParam \"\"\"\"", "\"")]
		[DataRow("-StringParam \"unclosed", "unclosed")]
		[DataRow("-stringparam strvalue", "strvalue")]
		[DataRow("-stringp strvalue", "strvalue")]
		[DataRow("-strparm strvalue", "strvalue")]
		public void TestStringParam(string cmdline, string expected)
		{
			StringCommand cmd = TestCommand<StringCommand>(cmdline);
			Assert.AreEqual(expected, cmd.StringParam);
		}

		class Int32Command : TestCommandBase
		{
			[Parameter]
			public int IntParam { get; set; }
		}

		[TestMethod]
		[DataRow("-IntParam 0", 0)]
		[DataRow("-IntParam 3", 3)]
		[DataRow("-IntParam 10", 10)]
		[DataRow("-IntParam:10", 10)]
		[DataRow("-IntParam \"10\"", 10)]
		[DataRow("-IntParam 0x10", 0x10)]
		//[DataRow("-IntParam 0b10", 0b10)]
		public void TestInt32Param(string cmdline, int expected)
		{
			var cmd = TestCommand<Int32Command>(cmdline);
			Assert.AreEqual(expected, cmd.IntParam);
		}

		class Int32ListCommand : TestCommandBase
		{
			[Parameter(0)]
			public int[] IntParam { get; set; }
		}

		[TestMethod]
		[DataRow("1", new int[] { 1 })]
		[DataRow("1, 2", new int[] { 1, 2 })]
		[DataRow("-IntParam 1", new int[] { 1 })]
		[DataRow("-IntParam 1, 2", new int[] { 1, 2 })]
		[DataRow("-IntParam 1, 2, 3", new int[] { 1, 2, 3 })]
		[DataRow("-IntParam 1, 2, 3,", new int[] { 1, 2, 3 })]
		public void TestInt32ListParam(string cmdline, int[] expected)
		{
			var cmd = TestCommand<Int32ListCommand>(cmdline);
			CollectionAssert.AreEqual(expected, cmd.IntParam);
		}

		class SwitchParamCommand : TestCommandBase
		{
			[Parameter]
			public SwitchParam SwitchParam { get; set; }
		}

		[TestMethod]
		[DataRow("-SwitchParam", SwitchParamFlags.Specified | SwitchParamFlags.Set)]
		[DataRow("-SwitchParam:true", SwitchParamFlags.Specified | SwitchParamFlags.Set)]
		[DataRow("-SwitchParam:false", SwitchParamFlags.Specified)]
		[DataRow("", SwitchParamFlags.None)]
		public void TestSwitchParam(string cmdline, SwitchParamFlags expected)
		{
			var cmd = TestCommand<SwitchParamCommand>(cmdline);
			Assert.AreEqual(expected, cmd.SwitchParam.Flags);
		}


		//		[Command("This is a command to test help generation", HelpText =
		//			@"This command is an example of how the help generation looks with all available options
		//If you would like to see variants you can change this call and review the results

		//Examples:
		//	Here could be some fun examples

		//	Maybe another example

		//use cases:
		//	Trying to be helpful about how your command works")]
		//		class HelpGenTestCommand
		//		{
		//			[Parameter(0)]
		//			public int SomeInt { get; set; }

		//			[Parameter(1, Mandatory = true, Description = "Some sort of description about a string argument")]
		//			public string SomeString { get; set; }

		//			[Parameter(Mandatory = true, Description = "This takes multiple strings")]
		//			public List<string> MultiString { get; set; }

		//			[Parameter(Mandatory = true)]
		//			[Alias("theint", "chosenInt")]
		//			public int OtherInt { get; set; }

		//			[Parameter]
		//			public SwitchParam SomeSwitch { get; set; }
		//		}

		//		[TestMethod]
		//		public void TestHelpGenerator()
		//		{
		//			var cmd = new HelpGenTestCommand();
		//			var help = CommandLineParser.GetHelpString(cmd, false, "someCommandname");
		//			Console.Write(help);
		//		}
	}
}
