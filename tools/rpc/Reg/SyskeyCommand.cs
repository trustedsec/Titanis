using System.ComponentModel;
using Titanis.Cli;
using Titanis.Winterop.Lsa;
using Titanis.Winterop.Registry;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public class Syskey : IWantServerName
	{
		public string? ServerName { get; set; }
		public string? KeyText { get; set; }

		public override string ToString() => this.KeyText;
	}

	/// <task category="Registry;Enumeration">Get the system key</task>
	[Command]
	[Description("Prints the system key of a remote system")]
	[OutputRecordType(typeof(Syskey))]
	[Example("Prints the syskey using a backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics")]
	internal class SyskeyCommand : RegistryCommand
	{
		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.KeyOptions;

			byte[] syskey = await ExtractSyskey(client, options, this.Log, cancellationToken);

			this.WriteRecord(new Syskey
			{
				KeyText = syskey.ToHexString()
			});

			return 0;
		}

		internal static async Task<byte[]> ExtractSyskey(IRegistryStore registry, RegistryKeyOptions options, ILog log, CancellationToken cancellationToken)
		{
			return await LsaStore.ExtractSyskey(registry, options, log, cancellationToken);
		}
	}
}
