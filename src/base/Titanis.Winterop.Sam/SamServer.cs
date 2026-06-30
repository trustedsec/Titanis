using System.Buffers.Binary;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Titanis.IO;
using Titanis.Msrpc.Msrrp.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Sam;
using Titanis.Winterop.Security;

namespace Titanis.Winterop.SamServer
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
		public byte[]? NtlmHash => this._userObj.GetDecryptedNtHash();
		public string? NtlmHashText => this.NtlmHash?.ToHexString();
	}

	public abstract class SamServer
	{
		// [MS-SAMR] § 2.2.10.1 USER_PROPERTIES
		public static SupplementalCredentials DecodeSupplementalCredential(int? kvno, ReadOnlySpan<byte> bytes)
		{
			// This allows keys to be sorted in the keytab
			kvno ??= 3;

			SupplementalCredentials creds = new SupplementalCredentials();

			var reader = new ByteMemoryReader(bytes.ToArray());
			var userProps = reader.ReadPduStruct<USER_PROPERTIES>();
			List<KerberosKeyInfo> newKeys = new List<KerberosKeyInfo>();
			HashSet<uint> newKeyTypes = new HashSet<uint>();
			List<KerberosKeyInfo> oldKeys = new List<KerberosKeyInfo>();
			bool hasKerbNew = false;
			foreach (var prop in userProps.properties)
			{
				var propBytes = BinaryHelper.ParseHexString(prop.valueBytes);
				reader = new ByteMemoryReader(propBytes);
				switch (prop.name)
				{
					case "Primary:WDigest":
						{
							var wdigest = reader.ReadPduStruct<WDIGEST_CREDENTIALS>();
							creds.WDigestHashes = Array.ConvertAll(wdigest.hashes, r => r.bytes);
						}
						break;
					case "Primary:Kerberos":
						if (!hasKerbNew)
						{
							var kerb = reader.ReadPduStruct<KERB_STORED_CREDENTIAL>();
							ExtractKerberosKeysInto(kvno, propBytes, kerb.credentials, newKeys, newKeyTypes);
							ExtractKerberosKeysInto(kvno - 1, propBytes, kerb.oldCredentials, oldKeys, null);
							creds.KerberosSalt = propBytes.Slice(kerb.defaultSaltOffset, kerb.defaultSaltLength).ToArray();
						}
						break;
					case "Primary:Kerberos-Newer-Keys":
						{
							// Clear old-style Primary:Kerberos
							oldKeys.Clear();

							var kerb = reader.ReadPduStruct<KERB_STORED_CREDENTIAL_NEW>();
							ExtractKerberosKeysInto(kvno, propBytes, kerb.credentials, newKeys, newKeyTypes);
							ExtractKerberosKeysInto(kvno, propBytes, kerb.serviceCredentials, newKeys, newKeyTypes);
							ExtractKerberosKeysInto(kvno - 1, propBytes, kerb.oldCredentials, oldKeys, null);
							ExtractKerberosKeysInto(kvno - 2, propBytes, kerb.olderCredentials, oldKeys, null);
							creds.KerberosSalt = propBytes.Slice(kerb.defaultSaltOffset, kerb.defaultSaltLength).ToArray();

							hasKerbNew = true;
						}
						break;
					case "Primary:CLEARTEXT":
						{
							creds.CleartextPassword = Encoding.Unicode.GetString(propBytes);
						}
						break;
					case "Primary:NTLM-Strong-NTOWF":
						{
							creds.NtlmStrongNtowf = propBytes;
						}
						break;
					case "Packages":
						break;
					default:
						break;
				}
			}

			creds.KerberosKeys = newKeys.ToArray();
			creds.KerberosOldKeys = oldKeys.ToArray();

			return creds;
		}

		private static void ExtractKerberosKeysInto(int? kvno, byte[] propBytes, KERB_KEY_DATA[]? keyData, List<KerberosKeyInfo> keys, HashSet<uint>? keyTypes)
		{
			if (keyData != null)
			{
				foreach (var key in keyData)
				{
					var keyInfo = new KerberosKeyInfo(kvno, key.keyType, propBytes.Slice(key.keyOffset, key.keyLength).ToArray());
					keys.Add(keyInfo);
				}
			}
		}

		private static void ExtractKerberosKeysInto(int? kvno, byte[] propBytes, KERB_KEY_DATA_NEW[]? keyData, List<KerberosKeyInfo> keys, HashSet<uint>? keyTypes)
		{
			if (keyData != null)
			{
				foreach (var key in keyData)
				{
					var keyInfo = new KerberosKeyInfo(kvno, key.keyType, propBytes.Slice(key.keyOffset, key.keyLength).ToArray(), key.iterationCount);
					if (keyTypes is null || keyTypes.Add(keyInfo.KeyType))
						keys.Add(keyInfo);
				}
			}
		}
	}

	public partial class SamRegistryServer : SamServer
	{
		private readonly byte[] _syskey;
		public byte[] SystemKey => this._syskey;
		private readonly IRegistryStore _registry;
		private readonly RegistryKeyOptions _regOptions;
		private readonly ILog? _log;

		private SamRegistryServer(
			byte[] syskey,
			IRegistryStore registry,
			RegistryKeyOptions options,
			ILog? log
			)
		{
			this._syskey = syskey;
			this._registry = registry;
			this._regOptions = options;
			this._log = log;
		}

		public static Task<SamRegistryServer> Open(byte[] systemKey, IRegistryStore registry, RegistryKeyOptions options, ILog? log, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(systemKey);
			ArgumentNullException.ThrowIfNull(registry);

			// TODO: Make this async

			return Task.FromResult(new SamRegistryServer(systemKey, registry, options, log));
		}

		public async Task<SamUserHash[]> DumpUserHashes(CancellationToken cancellationToken)
		{
			var log = this._log;

			var syskey = this._syskey;

			Dictionary<uint, string> userNames = new Dictionary<uint, string>();
			var hklm = await _registry.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken).ConfigureAwait(false);
			await using (hklm)
			{
				log?.WriteDiagnostic(@"Opening SAM\SAM\Domains\Account key");

				var accountKey = await hklm.OpenSubkey(@"SAM\SAM\Domains\Account", RegistryAccessRights.EnumerateSubkeys, this._regOptions, cancellationToken).ConfigureAwait(false);
				await using (accountKey)
				{

					var usersF = await accountKey.GetValue("F", cancellationToken).ConfigureAwait(false);
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




					log?.WriteDiagnostic(@"Opening Users key");

					var usersKey = await accountKey.OpenSubkey(@"Users", RegistryAccessRights.EnumerateSubkeys, this._regOptions, cancellationToken).ConfigureAwait(false);
					await using (usersKey)
					{
						List<SamUserHash> hashes = new List<SamUserHash>();

						await foreach (var keyInfo in usersKey.GetSubkeyNames(cancellationToken).ConfigureAwait(false))
						{
							if (keyInfo.KeyName == "Names")
							{
								// Skip
							}
							else if (uint.TryParse(keyInfo.KeyName, System.Globalization.NumberStyles.HexNumber, null, out var rid))
							{
								try
								{
									log?.WriteDiagnostic($"Getting info for {keyInfo.KeyName}");

									var userKey = await usersKey.OpenSubkey(keyInfo.KeyName, RegistryAccessRights.QueryValue, _regOptions, cancellationToken).ConfigureAwait(false);
									await using (userKey)
									{
										var v = (await userKey.GetValue("V", cancellationToken).ConfigureAwait(false)).Bytes;
										var user = new SamUserRegistryObject(store, rid, default, ImmutableArray.Create(v));

										hashes.Add(new SamUserHash(user));
									}
								}
								catch (Exception ex)
								{
									log?.WriteError($"Error getting info for {keyInfo.KeyName}: {ex.Message}");
								}
							}
							else
							{
								log?.WriteDiagnostic($"Found weird user key {keyInfo.KeyName}");
							}
						}

						return hashes.ToArray();
					}
				}
			}
		}
	}
}

