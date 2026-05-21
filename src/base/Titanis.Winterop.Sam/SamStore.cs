using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Crypto;

namespace Titanis.Winterop.Sam
{
	public class SamStore
	{
		public SamStore(byte[] masterKey)
		{
			if (masterKey != null)
			{
				if (masterKey.Length != 16)
					throw new ArgumentException("The master key is not the correct size.", nameof(masterKey));

				this._masterKey = masterKey;
				this._aes = Aes.Create();
				this._aes.Key = masterKey;
			}
		}

		private readonly byte[] _masterKey;
		private readonly Aes? _aes;

		public bool HasMasterKey => this._aes != null;

		private Aes VerifyAesKey() => this._aes ?? throw new InvalidOperationException("This SAM instance doesn't have the key and cannot perform decryption.");

		// [MS-SAMR] § 2.2.11.1.3 Deriving Key1 and Key2 from a Little-Endian, Unsigned Integer Key
		private static void DeriveUserKey(uint rid, out ulong key1_56, out ulong key2_56)
		{
			byte
				b0 = (byte)(rid & 0xFF),
				b1 = (byte)((rid >>= 8) & 0xFF),
				b2 = (byte)((rid >>= 8) & 0xFF),
				b3 = (byte)(rid >>= 8);

			key1_56 = ((((((((((((((ulong)b2) << 8) | b1) << 8) | b0) << 8) | b3) << 8) | b2) << 8) | b1) << 8) | b0);
			key2_56 = ((((((((((((((ulong)b1) << 8) | b0) << 8) | b3) << 8) | b2) << 8) | b1) << 8) | b0) << 8) | b3);
		}

		public static void DecryptUserData(uint userRid, Span<byte> bytes)
		{
			DeriveUserKey(userRid, out var k1, out var k2);
			k1 = DesPrimitives.ExpandKey(k1);
			k2 = DesPrimitives.ExpandKey(k2);
			ref ulong b1 = ref MemoryMarshal.AsRef<ulong>(bytes.Slice(0, 8));
			ref ulong b2 = ref MemoryMarshal.AsRef<ulong>(bytes.Slice(8, 8));
			b1 = DesPrimitives.DecryptBlock(k1, b1);
			b2 = DesPrimitives.DecryptBlock(k2, b2);
		}

		public byte[] Decrypt(uint rid, in SamEncryptedBlob blob)
		{
			if (blob.IsEmpty)
				throw new ArgumentException($"The encrypted blob is empty.");

			var aes = this.VerifyAesKey();

			if (blob.Revision == 2)
			{
				var encData = blob.EncryptedData;
				if (encData.Length == 0)
					return Array.Empty<byte>();

				var decrypted = aes.DecryptCbc(encData, blob.Salt);

				DecryptUserData(rid, decrypted.AsSpan(0, 16));

				return decrypted;
			}
			else
			{
				// TODO: Support rev 1 if it comes up
				throw new NotSupportedException($"The encrypted blob uses revision {blob.Revision} which is not supported by this implementation.");
			}
		}
	}
}
