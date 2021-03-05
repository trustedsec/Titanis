using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;

namespace Titanis.Smb2.Cli
{
	/// <task category="Enumeration">Enumerate the network interfaces and network addresses of an SMB server</task>
	[Description("Queries the server for a list of network interfaces.")]
	[OutputRecordType(typeof(Smb2NicInfo))]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_enumnics_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	internal class Smb2EnumNicsCommand : Smb2TreeCommand
	{
		protected override string? DefaultShareName => "IPC$";

		protected override async Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
		{
			var share = await client.GetShare(this.UncPathInfo, cancellationToken);

			// Query network interfaces
			var nics = await share.QueryNetworkInterfacesAsync(cancellationToken);

			this.WriteRecords(nics);

			return 0;
		}
	}
}
