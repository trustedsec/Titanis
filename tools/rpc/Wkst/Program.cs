using System.ComponentModel;

namespace Titanis.Cli.Wkst;

[Description("Interacts with the Workstation service")]
[Subcommand("getinfo", typeof(GetInfoCommand))]
[Subcommand("enumusers", typeof(EnumUsersCommand))]
[Subcommand("enumnames", typeof(EnumNamesCommand))]
[Subcommand("addaltname", typeof(AddAltNameCommand))]
[Subcommand("joininfo", typeof(JoinInfoCommand))]
[Subcommand("unjoin", typeof(UnjoinCommand))]
[Subcommand("join", typeof(JoinCommand))]
public class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
