using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <task category="WMI;Enumeration">Get a WMI object</task>
[Command]
[Description("Gets an object with a WMI path")]
[OutputRecordType(typeof(WmiObject), DefaultOutputStyle = OutputStyle.List)]
[DetailedHelpText(@"The object path is specified relative to the namespace.

Since the command line parser strips double quotes, use single quotes to delimit strings.  Single quotes are converted to double quotes before sending the request to WMI.")]
[Example("Gets the Win32_Process class", "{0} -namespace root\\cimv2 -UserName milchick -Password \"Br3@kr00m!\" LUMON-FS1 Win32_Process")]
[Example("Gets the Win32_LogicalDisk for C:", "{0} -namespace root\\cimv2 -UserName milchick -Password \"Br3@kr00m!\" LUMON-FS1 Win32_LogicalDisk.DeviceID='C:")]
internal class GetObjectCommand : WmiNamespaceCommandBase
{
	[Parameter(10)]
	[Mandatory]
	[Description("Path of object to get")]
	public string[] ObjectPath { get; set; }

	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		foreach (var objPath in this.ObjectPath)
		{
			var path = objPath.Replace('\'', '"');
			try
			{
				var obj = await ns.GetObjectAsync(path, cancellationToken);
				this.WriteRecord(obj);
			}
			catch (Exception ex)
			{
				this.WriteMessage(LogMessage.Error(null, ex));
			}
		}

		return 0;
	}
}
