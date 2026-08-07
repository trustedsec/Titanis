using System.ComponentModel;
using Titanis.Cli;

namespace Infobase;

[Description("Peform tasks with infobase files")]
[Subcommand("history", typeof(HistoryCommand))]
[Subcommand("analyze", typeof(AnalyzeCommand))]
[Subcommand("query", typeof(QueryCommand))]
public class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
