using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Titanis.Crypto
{
	public class AesCcm
	{
		enum B0Flags : byte
		{
			Adata = 64,
		}

		const int BlockSize = 16;

		private static readonly byte[] IV = new byte[BlockSize];

		public static void Encrypt(
			byte[] key,
			int authenticatorSize,
			int lengthFieldLength,
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> input,
			ReadOnlySpan<byte> header,
			Span<byte> output
			)
			=> Transform(
				key,
				authenticatorSize,
				lengthFieldLength,
				nonce,
				input,
				header,
				output,
				false);

		public static void Decrypt(
			byte[] key,
			int authenticatorSize,
			int lengthFieldLength,
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> input,
			ReadOnlySpan<byte> header,
			Span<byte> output
			)
			=> Transform(
				key,
				authenticatorSize,
				lengthFieldLength,
				nonce,
				input,
				header,
				output,
				true);

		public static void Transform(
			byte[] key,
			int authenticatorSize,
			int lengthFieldLength,
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> input,
			ReadOnlySpan<byte> header,
			Span<byte> output,
			bool fDecrypt
			)
		{
			if (
				authenticatorSize < 4
				|| authenticatorSize > 16
				|| (authenticatorSize % 2) != 0
				)
				throw new ArgumentOutOfRangeException(nameof(authenticatorSize));
			if (lengthFieldLength < 2 || lengthFieldLength > 8)
				throw new ArgumentOutOfRangeException(nameof(lengthFieldLength));
			if (nonce.Length != (15 - lengthFieldLength))
				throw new ArgumentOutOfRangeException(nameof(nonce));
			if (fDecrypt)
			{
				if (output.Length < (input.Length - authenticatorSize))
					throw new ArgumentOutOfRangeException(nameof(output));
			}
			else
			{
				if (output.Length < (input.Length + authenticatorSize))
					throw new ArgumentOutOfRangeException(nameof(output));
			}

			int messageLength = input.Length;
			if (fDecrypt)
				messageLength -= authenticatorSize;

			Aes aes = Aes.Create();
			aes.Mode = CipherMode.ECB;
			aes.Padding = PaddingMode.None;
			var enc = aes.CreateEncryptor(key, IV);

			// M'
			int m_ = (authenticatorSize - 2) / 2;
			// L'
			byte l_ = (byte)(lengthFieldLength - 1);

			int cBlocks = (messageLength + BlockSize - 1) / BlockSize;
			byte[] a = new byte[BlockSize];
			a[0] = l_;
			for (int i = 0; i < nonce.Length; i++)
			{
				a[1 + i] = nonce[i];
			}

			byte[] s = new byte[BlockSize];

			for (int iBlock = 0; iBlock < cBlocks; iBlock++)
			{
				{
					int ctr = iBlock + 1;
					for (int i = 0; i < lengthFieldLength; i++)
					{
						a[BlockSize - i - 1] = (byte)ctr;
						ctr >>= 8;
					}
				}

				int readIndex = (iBlock * BlockSize);
				int cbBlock = Math.Min(BlockSize, messageLength - readIndex);
				enc.TransformBlock(a, 0, BlockSize, s, 0);
				for (int i = 0; i < cbBlock; i++)
				{
					output[readIndex + i] = (byte)(s[i] ^ input[readIndex + i]);
				}
			}



			// Compute CMAC
			byte b0_0 = 0;
			if (header.Length > 0)
			{
				b0_0 |= (byte)B0Flags.Adata;
			}

			b0_0 |= (byte)(m_ << 3);

			b0_0 |= l_;

			byte[] b0 = new byte[BlockSize];
			b0[0] = b0_0;
			for (int i = 0; i < nonce.Length; i++)
			{
				b0[1 + i] = nonce[i];
			}

			int l_m = messageLength;
			for (int i = 0; i < lengthFieldLength; i++)
			{
				b0[BlockSize - i - 1] = (byte)l_m;
				l_m >>= 8;
			}

			enc.TransformBlock(b0, 0, BlockSize, b0, 0);

			int cbLengthStr;
			if (header.Length == 0)
			{
				cbLengthStr = 0;
			}
			else if (header.Length < ((1 << 16) - (1 << 8)))
			{
				cbLengthStr = 2;
				b0[0] ^= (byte)(header.Length >> 8);
				b0[1] ^= (byte)(header.Length);
			}
			else // if (additionalData.Length < (1L << 32))
			{
				cbLengthStr = 6;
				b0[0] ^= 0xFF;
				b0[1] ^= 0xFE;
				b0[2] ^= (byte)(header.Length >> 24);
				b0[3] ^= (byte)(header.Length >> 16);
				b0[4] ^= (byte)(header.Length >> 8);
				b0[5] ^= (byte)(header.Length);
			}
			// TODO: Accomodate values > int32


			void UpdateCmac(ReadOnlySpan<byte> span)
			{
				int readIndex = 0;
				while (readIndex < span.Length)
				{
					int cbBlock = Math.Min(BlockSize, span.Length - readIndex);
					for (int i = 0; i < cbBlock; i++)
					{
						b0[i] ^= span[i + readIndex];
					}

					enc.TransformBlock(b0, 0, BlockSize, b0, 0);

					readIndex += cbBlock;
				}
			}

			// First block of authenticator
			{
				int cbBlock = Math.Min(BlockSize - cbLengthStr, header.Length);
				for (int i = 0; i < cbBlock; i++)
				{
					b0[i + cbLengthStr] ^= header[i];
				}
				enc.TransformBlock(b0, 0, BlockSize, b0, 0);

				UpdateCmac(header.Slice(cbBlock));
				UpdateCmac(fDecrypt ? output : input);
			}


			// b0 == the the MAC

			// S_0
			for (int i = 0; i < lengthFieldLength; i++)
			{
				a[BlockSize - i - 1] = 0;
			}
			enc.TransformBlock(a, 0, BlockSize, s, 0);

			if (fDecrypt)
			{
				int readIndex = messageLength;
				for (int i = 0; i < authenticatorSize; i++)
				{
					byte b = (byte)(b0[i] ^ s[i]);
					if (input[readIndex + i] != b)
						throw new InvalidDataException();
				}
			}
			else
			{
				int readIndex = input.Length;
				for (int i = 0; i < authenticatorSize; i++)
				{
					output[readIndex + i] = (byte)(b0[i] ^ s[i]);
				}
			}
		}
	}
}
