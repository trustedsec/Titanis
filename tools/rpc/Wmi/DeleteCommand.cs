using ms_wmi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI">Delete a WMI object</task>
[Command]
[Description("Deletes a WMI object")]
[Example("Terminate a process by PID", "{0} -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process.Handle=8008")]
[Example("Terminate a process by name", "{0} -UserName milchick -Password Br3@kr00m! LUMON-DC1 \"SELECT * FROM Win32_Process WHERE Caption='REGEDIT.EXE'\"")]
internal class DeleteCommand : WmiObjectCommandBase
{
	protected sealed override async Task ProcessObject(WmiObject obj, WmiScope scope, CancellationToken cancellationToken)
	{
		await scope.DeleteInstance(obj.RelativePath, cancellationToken);
	}
}
