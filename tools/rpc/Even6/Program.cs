using System.ComponentModel;
using Titanis;
using Titanis.Cli;

namespace Even6;

[Subcommand("query", typeof(QueryCommand))]
[Description("Interacts with thte Event Log service")]
public class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
