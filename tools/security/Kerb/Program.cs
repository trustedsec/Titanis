using System.ComponentModel;
using Titanis.Cli;

namespace Kerb
{
	[Description("Commands for working with Kerberos authentication")]
	[Subcommand("getasinfo", typeof(GetASInfoCommand))]
	[Subcommand("asreq", typeof(AsreqCommand))]
	[Subcommand("tgsreq", typeof(RequestTicketCommand))]
	[Subcommand("renew", typeof(RenewTicketCommand))]
	[Subcommand("select", typeof(SelectCommand))]
	[Subcommand("changepw", typeof(ChangePasswordCommand))]
	[Subcommand("setpw", typeof(SetPasswordCommand))]
	[Subcommand("s2k", typeof(S2kCommand))]
	internal class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}
}
