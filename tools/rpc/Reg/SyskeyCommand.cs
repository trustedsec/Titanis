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

namespace Titanis.Msrpc.Msrrp.Cli
{
	[Command]
	[Description("Prints the system key of a remote system")]
	[OutputRecordType(typeof(string), DefaultOutputStyle = OutputStyle.Freeform)]
	[Example("Prints the syskey using a backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics")]
	internal class SyskeyCommand : RegistryCommand
	{
		private const string LsaKeyPath = @"SYSTEM\CurrentControlSet\Control\Lsa";

		[Parameter]
		[Description("Open with backup semantics")]
		public SwitchParam BackupSemantics { get; set; }

		private const ulong SyskeyByteSwap = 0xEC6B4D50F91273A8;

		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.BackupSemantics.IsSet ? RegistryKeyOptions.BackupRestore : RegistryKeyOptions.None;

			byte[] syskey = await ExtractSyskey(client, options, this.Log, cancellationToken);

			this.WriteRecord(syskey.ToHexString());

			return 0;
		}

		internal static async Task<byte[]> ExtractSyskey(RemoteRegistryClient client, RegistryKeyOptions options, ILog log, CancellationToken cancellationToken)
		{
			log.WriteDiagnostic($"Opening HKLM");
			await using (var hklm = await client.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken))
			{
				log.WriteDiagnostic($"Opening HKLM\\{LsaKeyPath}");
				await using (var lsaKey = await hklm.OpenSubkey(LsaKeyPath, RegistryAccessRights.QueryValue, options, cancellationToken))
				{
					string[] names = ["JD", "Skew1", "GBG", "Data"];

					byte[] syskey = new byte[16];
					int writeIndex = 0;
					ulong swapKey = SyskeyByteSwap;
					foreach (string? name in names)
					{
						log.WriteDiagnostic($"Opening HKLM\\{LsaKeyPath}\\{name}");
						await using (var subkey = await lsaKey.OpenSubkey(name, RegistryAccessRights.QueryValue, options, cancellationToken))
						{
							var info = await subkey.QueryInfo(cancellationToken);

							log.WriteDiagnostic($"  className={info.ClassName}");
							var bytes = BinaryHelper.ParseHexString(info.ClassName.TrimEnd('\0'));

							syskey[(swapKey & 0x0F)] = bytes[0];
							swapKey >>= 4;
							syskey[(swapKey & 0x0F)] = bytes[1];
							swapKey >>= 4;
							syskey[(swapKey & 0x0F)] = bytes[2];
							swapKey >>= 4;
							syskey[(swapKey & 0x0F)] = bytes[3];
							swapKey >>= 4;
						}
					}

					return syskey;
				}
			}
		}
	}
}
