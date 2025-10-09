using ms_wmi;
using System;
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
internal class DeleteCommand : WmiNamespaceCommandBase
{
	[Parameter(10)]
	[Mandatory]
	[Description("Path to object or WQL query of objects to delete")]
	public string ObjectPathOrWqlQuery { get; set; }

	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		int count = 0;

		if (
			this.ObjectPathOrWqlQuery.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
			|| this.ObjectPathOrWqlQuery.StartsWith("ASSOCIATORS OF", StringComparison.OrdinalIgnoreCase)
			)
		{
			var wql = this.ObjectPathOrWqlQuery;
			var query = await ns.ExecuteWqlQueryAsync(wql, 1, cancellationToken);
			bool hasObject = false;
			while (await query.ReadAsync(cancellationToken))
			{
				hasObject = true;
				try
				{
					this.WriteDiagnostic($"Deleting object {query.Current.RelativePath}");
					await ns.DeleteInstance(query.Current.RelativePath, cancellationToken);
					count++;
				}
				catch (Exception ex)
				{
					this.WriteError($"Method invocation failed: {ex.Message}");
				}
			}

			if (!hasObject)
				this.WriteWarning("No objects deleted because the query did not yield any instances");
		}
		else
		{
			string objPath = this.ObjectPathOrWqlQuery;
			var obj = await ns.GetObjectAsync(objPath, cancellationToken);
			if (obj != null)
			{
					this.WriteDiagnostic($"Deleting object {obj.RelativePath}");
				await ns.DeleteInstance(obj.RelativePath, cancellationToken);
			}
			else
			{
				this.WriteError($"Object path `{objPath}' did not return an object.");
			}
		}

		this.WriteVerbose($"Deleted {count} instance(s)");
		return 0;
	}
}
