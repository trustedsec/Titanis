using System.ComponentModel;
using Titanis.Cli;

namespace Fasp;

[Command]
[Subcommand("enumrules", typeof(EnumRulesCommand))]
[Subcommand("query", typeof(QueryRulesCommand))]
[Subcommand("addrule", typeof(AddRuleCommand))]
//[Subcommand("setrule", typeof(SetRuleCommand))]
[Description("Interacts with the Windows Firewall service")]
public class Program : MultiCommand
{
	static void Main(string[] args) => RunProgramAsync<Program>(args);
}
