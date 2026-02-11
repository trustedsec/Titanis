using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <task category="WMI;Enumeration">List the namespaces within a WMI namespace</task>
[Command]
[Description("Lists the available namespaces within a namespace.")]
[OutputRecordType(typeof(WmiInstanceObject))]
internal class LsnsCommand : QueryCommandBase
{
	protected sealed override string GetQueryText() => "SELECT * FROM __NAMESPACE";
}
