using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;

namespace Kerb;

[Command]
[Description("Display and edit keytab files")]
[Subcommand("list", typeof(ListKeytabCommand))]
internal class KeytabCommand : MultiCommand
{
}
