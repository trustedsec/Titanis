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
	/// <task category="Registry">Get information on a registry key</task>
	[Command]
	[Description("Gets key info")]
	[OutputRecordType(typeof(RegistryKeyInfo), DefaultOutputStyle = OutputStyle.List)]
	internal class KeyInfoCommand : RegistryKeyCommand
	{
		protected override RegistryAccessRights RequiredKeyAccess => RegistryAccessRights.QueryValue;

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var keyInfo = await key.QueryInfo(cancellationToken);
			this.WriteRecord(keyInfo);

			return 0;
		}
	}
}
