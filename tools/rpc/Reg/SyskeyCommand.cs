using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Lsa;
using Titanis.Winterop.Registry;
using Titanis.Winterop.SamServer;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	[Command]
	[Description("Prints the system key of a remote system")]
	[OutputRecordType(typeof(string), DefaultOutputStyle = OutputStyle.Freeform)]
	[Example("Prints the syskey using a backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics")]
	internal class SyskeyCommand : RegistryCommand
	{
		[Parameter]
		[Description("Open with backup semantics")]
		public SwitchParam BackupSemantics { get; set; }

		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.BackupSemantics.IsSet ? RegistryKeyOptions.BackupRestore : RegistryKeyOptions.None;

			byte[] syskey = await ExtractSyskey(client, options, this.Log, cancellationToken);

			this.WriteRecord(syskey.ToHexString());

			return 0;
		}

		internal static async Task<byte[]> ExtractSyskey(IRegistryStore registry, RegistryKeyOptions options, ILog log, CancellationToken cancellationToken)
		{
			return await LsaStore.ExtractSyskey(registry, options, log, cancellationToken);
		}
	}
}
