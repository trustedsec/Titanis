using System.ComponentModel;
using Titanis.Cli;

namespace Titanis.DceRpc.Cli;

[Description("Commands for interacting with the RPC endpoint mapper")]
[Subcommand("lsep", typeof(LsepCommand))]
internal class Program : MultiCommand
{
	static int Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
