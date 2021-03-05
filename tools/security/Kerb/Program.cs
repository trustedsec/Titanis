using System.ComponentModel;
using Titanis.Cli;

namespace Kerb
{
	[Description("Commands for working with Kerberos authentication")]
	[Subcommand("getasinfo", typeof(GetASInfoCommand))]
	[Subcommand("asreq", typeof(AsreqCommand))]
	[Subcommand("tgsreq", typeof(RequestTicketCommand))]
	[Subcommand("select", typeof(SelectCommand))]
	internal class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}
}
