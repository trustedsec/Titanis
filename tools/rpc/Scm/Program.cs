using System.ComponentModel;
using Titanis.Cli;

namespace Titanis.Cli.ScmTool;

[Command]
[Description("Provides functionality for interacting with the service control manager on a remote Windows system")]
[Subcommand("query", typeof(QueryCommand))]
[Subcommand("qtriggers", typeof(QueryTriggersCommand))]
[Subcommand("create", typeof(CreateServiceCommand))]
[Subcommand("delete", typeof(DeleteCommand))]
[Subcommand("start", typeof(StartCommand))]
[Subcommand("stop", typeof(StopCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
