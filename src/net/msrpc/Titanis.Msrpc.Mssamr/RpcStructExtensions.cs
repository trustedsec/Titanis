using ms_dtyp;
using ms_samr;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;
using Titanis.Crypto;
using Titanis.Security.Ntlm;

namespace Titanis.Msrpc.Mssamr
{
	static class RpcStructExtensions
	{
		public static unsafe long AsInt64(this OLD_LARGE_INTEGER n)
		{
			OLD_LARGE_INTEGER* pLargeInt = &n;
			var l64 = *(long*)pLargeInt;
			return l64;
		}

		public static DateTime? AsNullableDateTime(this OLD_LARGE_INTEGER n)
		{
			long n64 = n.AsInt64();
			if (n64 == long.MaxValue || n64 == 0)
				return null;
			else
				return DateTime.FromFileTime(n64);
		}

		public static string AsString(this in RPC_UNICODE_STRING rpcString)
		{
			var arr = rpcString.Buffer?.value.Array;
			if (arr == null)
				return null;
			else
			{
				var arrseg = rpcString.Buffer.value;
				return new string(arr, arrseg.Offset, arrseg.Count);
			}
		}

		public static RPC_UNICODE_STRING AsRpcString(this string str)
		{
			if (str == null)
			{
				return new RPC_UNICODE_STRING();
			}
			else
			{
				int cb = Encoding.Unicode.GetByteCount(str);
				return new RPC_UNICODE_STRING
				{
					Length = (ushort)cb,
					MaximumLength = (ushort)(cb + 2),
					Buffer = new DceRpc.RpcPointer<System.ArraySegment<char>>(new System.ArraySegment<char>(
						(str + '\0').ToCharArray(), 0, str.Length
						))
				};
			}
		}

		public static Span<byte> AsNtlm(this string key)
		{
			Buffer128 digestBuffer = new Buffer128();

			SlimHashAlgorithm.ComputeHash<Md4Context>(Encoding.Unicode.GetBytes(key),
				digestBuffer.AsSpan());
			return digestBuffer.AsSpan().ToArray();
		}

		// [MS-SAMR] § 2.2.11.1.2 #4 Encrypting a 64-Bit Block with a 7-Byte Key
		static bool CalculateParity(uint input)
		{
			// If preceding seven bits is odd, parity is 0, else 1
			uint val = 1;
			while (input != 0)
			{
				val ^= (input & 1);
				input >>= 1;
			}

			return val != 0;
		}

		// [MS-SAMR] § 2.2.11.1.2 Encrypting a 64-Bit Block with a 7-Byte Key
		static void EncryptBlock(
			ReadOnlySpan<byte> block,
			ReadOnlySpan<byte> inputKey,
			Span<byte> encrypted)
		{
			Debug.Assert(block.Length == 8);
			Debug.Assert(encrypted.Length == 8);
			Debug.Assert(inputKey.Length >= 7);

			ulong outputKeyVal = 0;
			unsafe
			{
				Span<byte> outputKey = new Span<byte>(&outputKeyVal, sizeof(ulong));

				outputKey[0] = (byte)(inputKey[0] >> 1);
				outputKey[1] = (byte)(((inputKey[0] & 1) << 6) | (inputKey[1] >> 2));
				outputKey[2] = (byte)(((inputKey[1] & 0x03) << 5) | (inputKey[2] >> 3));
				outputKey[3] = (byte)(((inputKey[2] & 0x07) << 4) | (inputKey[3] >> 4));
				outputKey[4] = (byte)(((inputKey[3] & 0x0F) << 3) | (inputKey[4] >> 5));
				outputKey[5] = (byte)(((inputKey[4] & 0x1F) << 2) | (inputKey[5] >> 6));
				outputKey[6] = (byte)(((inputKey[5] & 0x3F) << 1) | (inputKey[6] >> 7));
				outputKey[7] = (byte)(inputKey[6] & 0x7F);

				for (int i = 0; i < 8; i++)
				{
					outputKey[i] = (byte)((outputKey[i] << 1) & 0xfe);
					if (CalculateParity(outputKey[i]))
					{
						outputKey[i] |= 1;
					}
				}

				fixed (byte* pBlock = block)
				{
					fixed (byte* pCipher = encrypted)
					{
						ref ulong cipherVal = ref *(ulong*)pCipher;
						cipherVal = (Des.Encrypt(
							outputKeyVal,
							*(ulong*)pBlock
							));
					}
				}
			}
		}

		// [MS-SAMR] § 2.2.11.1.1 Encrypting an NT or LM Hash Value with a Specified Key
		public static byte[] EncryptHashWithHash(this byte[] data, byte[] key)
			=> EncryptHashWithHash(data.AsSpan(), key.AsSpan());

		public static byte[] EncryptHashWithHash(ReadOnlySpan<byte> data, ReadOnlySpan<byte> key)
		{
			byte[] result = new byte[16];

			EncryptBlock(data.Slice(0, 8), key.Slice(0, 7), result.AsSpan().Slice(0, 8));
			EncryptBlock(data.Slice(8, 8), key.Slice(7, 7), result.AsSpan().Slice(8, 8));

			return result;
		}

	}
}