using System.ComponentModel;

namespace Titanis.Cli.Kerb;

[Command]
[Description("Display and edit keytab files")]
[Subcommand("list", typeof(ListKeytabCommand))]
internal class KeytabCommand : MultiCommand
{
}
