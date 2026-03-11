using Titanis.Cli;

namespace Dsrep;

[Subcommand("dcinfo", typeof(DcinfoCommand))]
[Subcommand("rep", typeof(ReplicateCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
