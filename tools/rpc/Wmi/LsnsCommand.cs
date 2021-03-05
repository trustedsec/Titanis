using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI;Enumeration">List the namespaces within a WMI namespace</task>
[Command]
[Description("Lists the available namespaces within a namespace.")]
[OutputRecordType(typeof(WmiInstanceObject))]
internal class LsnsCommand : QueryCommandBase
{
	protected sealed override string GetQueryText() => "SELECT * FROM __NAMESPACE";
}
