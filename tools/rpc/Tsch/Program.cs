using System.ComponentModel;
using Titanis.Cli;
using Tsch;

namespace Titanis.Cli.Tsch;

[Description("Interacts with the Task Scheduler service")]
[Subcommand("enumfolders", typeof(EnumFoldersCommand))]
[Subcommand("enumtasks", typeof(EnumTasksCommand))]
[Subcommand("get", typeof(GetTaskCommand))]
[Subcommand("add", typeof(AddTaskCommand))]
[Subcommand("start", typeof(StartTaskCommand))]
[Subcommand("stop", typeof(StopTaskCommand))]
[Subcommand("stopinstance", typeof(StopInstanceTaskCommand))]
public class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
