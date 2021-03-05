using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI">Back up the WMI MOF repository</task>
[Command]
[Description("Backs up the WMI repository")]
[Example("Back up to C:\\wmibackup.bak", "{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\\wmibackup.bak")]
internal class BackupCommand : WmiCommand
{
	[Parameter(10)]
	[Mandatory]
	[Description("Name of the file to write the backup to")]
	public string FileName { get; set; }

	protected override async Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken)
	{
		await wmi.Backup(this.FileName, cancellationToken);
		return 0;
	}
}
