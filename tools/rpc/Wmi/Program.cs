using System.ComponentModel;
using Wmi.Registry;

namespace Titanis.Cli.WmiTool;

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
[Subcommand("delete", typeof(DeleteCommand))]
[Subcommand("mountfs", typeof(FuseCommand))]
[Subcommand("reg", typeof(RegistryCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
