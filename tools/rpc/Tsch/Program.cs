using System.ComponentModel;
using Titanis.Cli;

namespace Titanis.Cli.TschTool;

[Command]
[Description("Provides functionality for interacting with the task scheduler on a remote Windows system")]
[Subcommand("query", typeof(QueryCommand))]
[Subcommand("get", typeof(GetCommand))]
[Subcommand("create", typeof(CreateCommand))]
[Subcommand("delete", typeof(DeleteCommand))]
[Subcommand("run", typeof(RunCommand))]
[Subcommand("stop", typeof(StopCommand))]
[Subcommand("enable", typeof(EnableCommand))]
[Subcommand("folders", typeof(FoldersCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
