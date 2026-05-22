using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <task category="Registry;Expanding Access">Set the security descriptor on a registry key</task>
	[Command]
	[Description("Sets the security descriptor of a registry key")]
	[Example("Set DACL", "LUMON-FS1 -username marks@LUMON -password She's@live!!  -Kdc  lumon-dc1 -BackupSemantics HKCU\\Software\\Microsoft D:AI(A;CIID;0x20019;;;BU)(A;CIID;0xF003F;;;BA)(A;CIID;0xF003F;;;SY)(A;CIIOID;0xF003F;;;CO)")]
	internal class SetsdCommand : RegistryKeyCommand
	{
		[Parameter(After =nameof(KeyPath))]
		[Mandatory]
		[Description("SDDL of the security descriptor to set")]
		public SecurityDescriptor SecurityDescriptor { get; set; }

		protected override RegistryAccessRights RequiredKeyAccess => (RegistryAccessRights)(this.SecurityDescriptor?.RightsToSet ?? StandardAccessRights.None);

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var sd = await key.GetSecurityDescriptor(SecurityInfo.Dacl, cancellationToken);
			this.WriteRecord(sd.ToSddlString(SecurityDescriptorSections.All));

			return 0;
		}
	}
}
