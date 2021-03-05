using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto
{
	public static class Sp800_108
	{
		private static unsafe void SetInt32(byte[] buf, int offset, int n)
		{
			Debug.Assert((offset + 4) <= buf.Length);

			if (BitConverter.IsLittleEndian)
				n = BinaryPrimitives.ReverseEndianness(n);

			fixed (byte* pBuf = buf)
			{
				*(int*)(pBuf + offset) = n;
			}
		}

		public static byte[] KdfCtr(
			byte[] label,
			byte[] context,
			int outputBitLength,
			HashAlgorithm hashalg
			)
		{
			if (label is null)
				throw new ArgumentNullException(nameof(label));
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			if (outputBitLength < 1)
				throw new ArgumentOutOfRangeException(nameof(outputBitLength));
			if (hashalg is null)
				throw new ArgumentNullException(nameof(hashalg));
			if (0 != (outputBitLength % 8))
				throw new ArgumentOutOfRangeException(nameof(outputBitLength), Messages.Kdf_InvalidBitLength);

			int cbiHash = hashalg.HashSize;
			int cbHash = cbiHash / 8;
			int n = ((outputBitLength + cbiHash - 1) / cbiHash);

			byte[] output = new byte[cbHash * n];

			int cbInput = 4 + label.Length + 1 + context.Length + 4;
			byte[] input = new byte[cbInput];
			int offInput = 4;
			Buffer.BlockCopy(label, 0, input, offInput, label.Length);
			offInput += label.Length + 1;
			Buffer.BlockCopy(context, 0, input, offInput, context.Length);
			offInput += context.Length;
			SetInt32(input, offInput, outputBitLength);

			for (int i = 0; i < n; i++)
			{
				SetInt32(input, 0, i + 1);

				byte[] outChunk = hashalg.ComputeHash(input);
				Buffer.BlockCopy(outChunk, 0, output, i * cbHash, outChunk.Length);
			}

			if (0 != (outputBitLength % cbiHash))
			{
				byte[] outkey = new byte[outputBitLength / 8];
				Buffer.BlockCopy(output, 0, outkey, 0, outputBitLength / 8);
				output = outkey;
			}

			return output;
		}
	}
}
