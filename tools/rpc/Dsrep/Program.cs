using System.ComponentModel;
using Titanis.Cli;

namespace Titanis.Cli.Dsrep;

[Subcommand("dcinfo", typeof(DcinfoCommand))]
[Subcommand("rep", typeof(ReplicateObjectsCommand))]
[Subcommand("repnc", typeof(ReplicateNcCommand))]
[Description("Interacts with Directory Replication Service")]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
