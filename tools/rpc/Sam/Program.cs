using System.ComponentModel;

namespace Titanis.Cli.SamTool;

[Command]
[Description("Commands for interacting with a remote Security Accounts Manager")]
[Subcommand("enumusers", typeof(EnumUsersCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
