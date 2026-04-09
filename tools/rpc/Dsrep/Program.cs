using System.ComponentModel;
using Titanis.Cli;

namespace Dsrep;

[Subcommand("dcinfo", typeof(DcinfoCommand))]
[Subcommand("rep", typeof(ReplicateCommand))]
[Description("Interacts with Directory Replication Service")]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
