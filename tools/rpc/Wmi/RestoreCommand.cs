using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI">Restore the WMI MOF repository</task>
[Command]
[Description("Restores the WMI repository")]
[Example("Restore from C:\\wmibackup.bak", "{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\\wmibackup.bak")]
[Example("Restore from C:\\wmibackup.bak, shutting down clients", "{0} -ForceShutdown -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\\wmibackup.bak")]
internal class RestoreCommand : WmiCommand
{
	[Parameter(10)]
	[Mandatory]
	[Description("Name of the file to read the backup from")]
	public string FileName { get; set; }

	[Parameter]
	[Description("Forces any active clients to shut down")]
	public SwitchParam ForceShutdown { get; set; }

	protected override async Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken)
	{
		await wmi.Restore(this.FileName, this.ForceShutdown.IsSet ? WmiRestoreOptions.ForceShutdown : WmiRestoreOptions.None, cancellationToken);
		return 0;
	}
}
