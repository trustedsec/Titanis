using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <task category="WMI;Enumeration">List the classes within a WMI namespace</task>
[Command]
[Description("Lists the classes within a namespace.")]
[OutputRecordType(typeof(WmiClassObject))]
internal class LsclassCommand : QueryCommandBase
{
	protected sealed override string GetQueryText() => "SELECT * FROM meta_class";
}
