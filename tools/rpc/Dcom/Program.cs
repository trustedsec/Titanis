using System.ComponentModel;

namespace Titanis.Cli.DcomTool;

[Description("Utility for working with DCOM")]
[Subcommand("invoke", typeof(InvokeCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
