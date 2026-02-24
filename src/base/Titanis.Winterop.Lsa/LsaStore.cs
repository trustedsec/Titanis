using System.Buffers.Binary;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Titanis.Crypto;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Winterop.Lsa
{
	public class LsaStore
	{
		private LsaStore(
			byte[] systemKey,
			byte[] encryptionKey,
			IRegistryStore registry,
			RegistryKeyOptions options,
			ILog? log)
		{
			this._systemKey = systemKey;
			this._encryptionKey = encryptionKey;
			this._registry = registry;
			this._regOptions = options;
			this._log = log;
		}

		private const string LsaKeyPath = @"SYSTEM\CurrentControlSet\Control\Lsa";
		private const ulong SyskeyByteSwap = 0xEC6B4D50F91273A8;

		private readonly IRegistryStore _registry;
		private readonly RegistryKeyOptions _regOptions;
		private readonly byte[] _systemKey;
		private readonly byte[] _encryptionKey;
		private readonly ILog? _log;

		public static async Task<LsaStore> Open(IRegistryStore registry, RegistryKeyOptions regOptions, ILog? log, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(registry);

			log?.WriteDiagnostic("Extracting system key to open LSA");
			var systemKey = await ExtractSyskey(registry, regOptions, log, cancellationToken).ConfigureAwait(false);
			log?.WriteDiagnostic("Extracted system key: " + systemKey.ToHexString());

			var ek = await ExtractPolicyEncryptionKey(systemKey, registry, regOptions, log, cancellationToken).ConfigureAwait(false);

			return new LsaStore(
				systemKey,
				ek,
				registry,
				regOptions,
				log);
		}

		public static async Task<byte[]> ExtractSyskey(IRegistryStore registry, RegistryKeyOptions regOptions, ILog? log, CancellationToken cancellationToken)
		{
			log.WriteDiagnostic($"Opening HKLM");
			var hklm = await registry.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken).ConfigureAwait(false);
			await using (hklm)
			{
				log.WriteDiagnostic($"Opening HKLM\\{LsaKeyPath}");
				var lsaKey = await hklm.OpenSubkey(LsaKeyPath, RegistryAccessRights.QueryValue, regOptions, cancellationToken).ConfigureAwait(false);

				await using (lsaKey)
				{
					string[] names = ["JD", "Skew1", "GBG", "Data"];

					byte[] syskey = new byte[16];
					int writeIndex = 0;
					ulong swapKey = SyskeyByteSwap;
					foreach (string? name in names)
					{
						log.WriteDiagnostic($"Opening HKLM\\{LsaKeyPath}\\{name}");
						var subkey = await lsaKey.OpenSubkey(name, RegistryAccessRights.QueryValue, regOptions, cancellationToken).ConfigureAwait(false);
						await using (subkey)
						{
							var info = await subkey.QueryInfo(cancellationToken).ConfigureAwait(false);

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

		public static async Task<byte[]> ExtractPolicyEncryptionKey(
			byte[] systemKey,
			IRegistryStore registry,
			RegistryKeyOptions regOptions,
			ILog? log,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(systemKey);
			ArgumentNullException.ThrowIfNull(registry);

			log?.WriteDiagnostic("Opening HKLM for LSA policy encryption key");

			var hklm = await registry.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken).ConfigureAwait(false);
			await using (hklm)
			{
				log?.WriteDiagnostic("Opening SECURITY\\Policy");
				var hkPolicy = await hklm.OpenSubkey(@"SECURITY\Policy", RegistryAccessRights.EnumerateSubkeys, regOptions, cancellationToken).ConfigureAwait(false);

				await using (hkPolicy)
				{
					byte[]? ek;
					try
					{
						log?.WriteDiagnostic("Opening PolEKList");
						var hkEklist = await hkPolicy.OpenSubkey(@"PolEKList", RegistryAccessRights.QueryValue, regOptions, cancellationToken).ConfigureAwait(false);
						await using (hkEklist)
						{
							var ekValue = await hkEklist.GetValue(null, cancellationToken).ConfigureAwait(false);
							ek = ekValue.Bytes;
						}
					}
					catch (Exception ex)
					{
						log?.WriteError($"Opening PolEKList failed: {ex.Message}");
						ek = null;
					}

					if (ek == null)
						throw new Exception("Unable to retrieve EK");

					ek = DecryptBlob(systemKey, ek)[^32..].ToArray();
					return ek;
				}
			}
		}

		public async Task<LsaSecret[]> GetSecrets(CancellationToken cancellationToken)
		{
			var regOptions = this._regOptions;

			var log = this._log;
			log?.WriteDiagnostic("Opening HKLM for LSA secrets");

			var hklm = await this._registry.OpenLocalMachine(RegistryAccessRights.QueryValue, cancellationToken).ConfigureAwait(false);
			await using (hklm)
			{
				log?.WriteDiagnostic(@"Opening SECURITY\Policy\Secrets");

				// Now for the secrets
				var hkSecrets = await hklm.OpenSubkey(@"SECURITY\Policy\Secrets", RegistryAccessRights.EnumerateSubkeys, regOptions, cancellationToken).ConfigureAwait(false);
				await using (hkSecrets)
				{
					List<LsaSecret> secrets = new List<LsaSecret>();
					await foreach (var subkeyInfo in hkSecrets.GetSubkeyNames(cancellationToken).ConfigureAwait(false))
					{
						var name = subkeyInfo.KeyName;

						try
						{
							log?.WriteDiagnostic($"Getting secret {name}");

							var hkSecret = await hkSecrets.OpenSubkey($@"{subkeyInfo.KeyName}", RegistryAccessRights.QueryValue, regOptions, cancellationToken).ConfigureAwait(false);
							await using (hkSecret)
							{
								var secret = await this.ExtractSecret(name, hkSecret, cancellationToken).ConfigureAwait(false);
								secrets.Add(secret);
							}
						}
						catch (Exception ex)
						{
							log?.WriteError($"Failed to open key for {subkeyInfo.KeyName}");
						}
					}

					return secrets.ToArray();
				}
			}
		}

		private async Task<byte[]?> GetDefaultValueFromKey(IRegistryKey key, string subkeyName, CancellationToken cancellationToken)
		{
			try
			{
				var subkey = await key.OpenSubkey(subkeyName, RegistryAccessRights.QueryValue, this._regOptions, cancellationToken).ConfigureAwait(false);
				await using (subkey)
				{
					var value = await subkey.GetValue(null, cancellationToken).ConfigureAwait(false);
					return value.Bytes;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private DateTime? BytesToTime(byte[] bytes)
		{
			if (bytes != null && bytes.Length == 8)
			{
				long value = BinaryPrimitives.ReadInt64LittleEndian(bytes);
				return DateTime.FromFileTimeUtc(value);
			}
			else
				return null;
		}
		internal async Task<LsaSecret> ExtractSecret(string name, IRegistryKey key, CancellationToken cancellation)
		{
			// CurrVal
			var currVal = await GetDefaultValueFromKey(key, "CurrVal", cancellation).ConfigureAwait(false);
			currVal = DecryptBlob(this._encryptionKey, currVal).ToArray();
			// OldVal
			var oldVal = await GetDefaultValueFromKey(key, "OldVal", cancellation).ConfigureAwait(false);
			if (oldVal != null && oldVal.Length > 0)
				oldVal = DecryptBlob(this._encryptionKey, oldVal).ToArray();
			else
				oldVal = null;
			// OupdTime
			var timeBytes = await GetDefaultValueFromKey(key, "OupdTime", cancellation).ConfigureAwait(false);
			DateTime? oldTime = BytesToTime(timeBytes);
			// CupdTime
			timeBytes = await GetDefaultValueFromKey(key, "CupdTime", cancellation).ConfigureAwait(false);
			DateTime currTime = BytesToTime(timeBytes) ?? new DateTime();
			// SecDesc
			var sdBytes = await GetDefaultValueFromKey(key, "SecDesc", cancellation).ConfigureAwait(false);
			SecurityDescriptor? sd;
			if (sdBytes != null && sdBytes.Length > 0)
				sd = new SecurityDescriptor(sdBytes);
			else
				sd = null;

			return new LsaSecret(name, currVal, oldVal, currTime, oldTime, sd);
		}

		public static ReadOnlySpan<byte> DecryptBlob(ReadOnlySpan<byte> key, ReadOnlySpan<byte> blob)
		{
			int length = BinaryPrimitives.ReadInt32LittleEndian(blob);
			var id = new Guid(blob.Slice(4, 16));
			int alg = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(20, 4));
			int flag = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(24, 4));

			var k0 = blob.Slice(28, 32);

			Sha256Context sha256 = new Sha256Context();
			sha256.Initialize();
			sha256.HashData(key);
			for (int i = 0; i < 1000; i++)
			{
				sha256.HashData(k0);
			}

			byte[] k1 = new byte[256 / 8];
			sha256.HashFinal(k1);

			Aes aes = Aes.Create();
			aes.Key = k1;
			var plain = aes.DecryptEcb(blob.Slice(28 + 32), PaddingMode.None);
			plain = ExtractLsaBlob(plain).ToArray();

			return plain;
		}

		public static ReadOnlySpan<byte> ExtractLsaBlob(ReadOnlySpan<byte> blob)
		{
			int length = BinaryPrimitives.ReadInt32LittleEndian(blob);
			var encrypted = blob.Slice(16, length);
			return encrypted;
		}
	}

	/// <summary>
	/// Describes an LSA secret
	/// </summary>
	public class LsaSecret
	{
		internal LsaSecret(
			string name,
			byte[]? currentValue,
			byte[]? oldValue,
			DateTime? currentUpdateTime,
			DateTime? oldUpdateTime,
			SecurityDescriptor? sd)
		{
			Name = name;
			CurrentValue = currentValue;
			OldValue = oldValue;
			CurrentUpdateTime = currentUpdateTime;
			OldUpdateTime = oldUpdateTime;
			SecurityDescriptor = sd;
		}

		public string Name { get; }
		[Browsable(false)]
		public byte[]? CurrentValue { get; }
		public string? CurrentValueHex => this.CurrentValue?.ToHexString();
		[Browsable(false)]
		public byte[]? OldValue { get; }
		public string? OldValueHex => this.OldValue?.ToHexString();
		public DateTime? CurrentUpdateTime { get; }
		public DateTime? OldUpdateTime { get; }
		[Browsable(false)]
		public SecurityDescriptor? SecurityDescriptor { get; }
		public string? SecurityDescriptorSddl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.All);
	}
}
