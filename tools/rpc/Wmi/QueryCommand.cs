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

/// <task category="WMI;Enumeration">Executes a WMI query</task>
[Command]
[Description("Executes a WMI query")]
[Example("Query running processes with all fields", "{0} LUMON-FS1 -UserName milchick -Password \"Br3@kr00m!\" \"SELECT * FROM Win32_Process\"")]
[Example("Query running processes with select fields", "{0} LUMON-FS1 -UserName milchick -Password \"Br3@kr00m!\" -OutputFields Caption, ProcessID, ParentProcessID  \"SELECT * FROM Win32_Process\"")]
internal sealed class QueryCommand : QueryCommandBase
{
	[Parameter(10)]
	[Mandatory]
	[Description("WQL query to execute")]
	public string Query { get; set; }

	/// <inheritdoc/>
	protected sealed override string GetQueryText() => this.Query;
}
