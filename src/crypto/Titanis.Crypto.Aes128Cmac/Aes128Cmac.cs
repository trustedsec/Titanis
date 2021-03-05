using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto
{
	public static class Aes128Cmac
	{
		const int BlockSize = 16;
		const int KeySize = 16;
		static readonly byte[] EmptyIV = new byte[128 / 8];

		private static void Shl(
			byte[] arr
			)
		{
			byte carry = 0;
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				ushort n = (arr[i]);
				n <<= 1;
				arr[i] = (byte)(n | carry);
				carry = (byte)(n >> 8);
			}
		}

		public static void Sign(
			ReadOnlySpan<byte> key,
			ReadOnlySpan<byte> data,
			Span<byte> signature
			)
		{
			// TODO: Use slim implementation

			byte[] block = new byte[Aes128Cmac.BlockSize];
			Aes aes128ecb = Aes.Create();
			aes128ecb.Mode = CipherMode.ECB;
			var encryptor = aes128ecb.CreateEncryptor(key.ToArray(), block);

			byte[] L = new byte[BlockSize];
			encryptor.TransformBlock(EmptyIV, 0, BlockSize, L, 0);
			bool msb = (L[0] >= 0x80);
			Shl(L);
			if (msb)
				L[15] ^= 0x87;
			var K1 = (byte[])L.Clone();

			msb = (L[0] >= 0x80);
			Shl(L);
			if (msb)
				L[15] ^= 0x87;
			var K2 = L;

			byte[] output = new byte[BlockSize];
			int blockCount = ((data.Length - 1) / BlockSize);
			for (int i = 0; i < blockCount; i++)
			{
				data.Slice(i * BlockSize, BlockSize).CopyTo(block);
				for (int j = 0; j < BlockSize; j++)
				{
					block[j] ^= output[j];
				}
				encryptor.TransformBlock(block, 0, BlockSize, output, 0);
			}

			var lastBlock = data.Slice(blockCount * BlockSize);
			lastBlock.CopyTo(block);
			if (lastBlock.Length < BlockSize)
			{
				block[lastBlock.Length] = 0x80;
				int i;
				for (i = 0; i <= lastBlock.Length; i++)
				{
					block[i] ^= K2[i];
				}
				for (; i < BlockSize; i++)
				{
					block[i] = K2[i];
				}
			}
			else
			{
				for (int i = 0; i < BlockSize; i++)
				{
					block[i] ^= K1[i];
				}
			}
			for (int j = 0; j < BlockSize; j++)
			{
				block[j] ^= output[j];
			}

			encryptor.TransformBlock(block, 0, BlockSize, output, 0);
			output.CopyTo(signature);
		}

		public static void Aes128Encrypt(
			ReadOnlySpan<byte> key,
			ReadOnlySpan<byte> iv,
			ReadOnlySpan<byte> data,
			Span<byte> cipher
			)
		{
			Aes aes = Aes.Create();
			aes.Key = key.ToArray();
			aes.IV = iv.ToArray();

			// TODO: Use slim implementation

			var encryptor = aes.CreateEncryptor();
			var cipherData = encryptor.TransformFinalBlock(data.ToArray(), 0, data.Length);
			new Span<byte>(cipherData).CopyTo(cipher);
		}
	}
}
