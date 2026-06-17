using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

// @freefirex Authored original registry MultiCommand code with add, delete, export, and query subcommands.
namespace Wmi.Registry
{
	[Description("Interact with the Windows registry via WMI.")]
	[Subcommand("set", typeof(RegistrySetCommand))]
	[Subcommand("delete", typeof(RegistryDeleteCommand))]
	[Subcommand("export", typeof(RegistryExportCommand))]
	[Subcommand("query", typeof(RegistryQueryCommand))]
	internal class RegistryCommand : MultiCommand
	{
	}

}
