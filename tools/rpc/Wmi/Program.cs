using System.ComponentModel;
using Titanis.Cli;

namespace Wmi;

[Description("Commands for interacting with the Windows Management Instrumentation service")]
[Subcommand("query", typeof(QueryCommand))]
[Subcommand("backup", typeof(BackupCommand))]
[Subcommand("restore", typeof(RestoreCommand))]
[Subcommand("lsns", typeof(LsnsCommand))]
[Subcommand("lsclass", typeof(LsclassCommand))]
[Subcommand("lsprop", typeof(LspropCommand))]
[Subcommand("lsmethod", typeof(LsmethodCommand))]
[Subcommand("get", typeof(GetObjectCommand))]
[Subcommand("exec", typeof(ExecCommand))]
[Subcommand("invoke", typeof(InvokeCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
