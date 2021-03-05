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

/// <task category="WMI;Enumeration">List the classes within a WMI namespace</task>
[Command]
[Description("Lists the classes within a namespace.")]
[OutputRecordType(typeof(WmiClassObject))]
internal class LsclassCommand : QueryCommandBase
{
	protected sealed override string GetQueryText() => "SELECT * FROM meta_class";
}
