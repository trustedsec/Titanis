using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

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
