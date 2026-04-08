using System.ComponentModel;

namespace Titanis.Cli.SamTool;

[Command]
[Description("Commands for interacting with a remote Security Accounts Manager")]
[Subcommand("enumusers", typeof(EnumUsersCommand))]
[Subcommand("enumgroups", typeof(EnumGroupsCommand))]
[Subcommand("enumaliases", typeof(EnumAliasesCommand))]
[Subcommand("aliasmembers", typeof(AliasMembersCommand))]
internal partial class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
