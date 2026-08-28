using System.ComponentModel;

namespace Titanis.Cli.Raza;

[Subcommand("check", typeof(CheckCommand))]
[Subcommand("getinfo", typeof(GetInfoCommand))]
[Description("Interacts with the Remote Authorization service")]
internal class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
