using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.NFold.Test")]

namespace Titanis
{
	public static class NFold
	{
		internal static byte GetInputWord(ReadOnlySpan<byte> key, int index)
		{
			if (index < key.Length)
				return (key[index]);

			int round = index / key.Length;
			int offset = index % key.Length;
			offset *= 8;

			int shift = (13 * round);
			shift %= (key.Length * 8);
			offset += ((key.Length * 8) - shift);
			offset %= (key.Length * 8);
			int offset_m = (offset % 8);

			uint b = key[offset / 8];
			if (offset_m != 0)
			{
				b <<= offset_m;
				b |= (uint)(key[((offset / 8) + 1) % key.Length] >> (8 - offset_m));
			}

			return (byte)b;
		}

		public static void DeriveKey(ReadOnlySpan<byte> key, Span<byte> outputBuffer)
		{
			if (key.Length == 0)
				throw new ArgumentNullException(nameof(key));
			if (outputBuffer.Length == 0)
				throw new ArgumentNullException(nameof(outputBuffer));

			int lcm = Lcm(outputBuffer.Length, key.Length);

			uint n = 0;
			for (int i = lcm - 1; i >= 0; i--)
			{
				n += GetInputWord(key, i);

				int j = i % outputBuffer.Length;
				n += outputBuffer[j];
				outputBuffer[j] = (byte)n;

				n >>= 8;
			}

			while (n > 0)
			{
				for (int i = outputBuffer.Length - 1; (n > 0) && i >= 0; i--)
				{
					n += outputBuffer[i];
					outputBuffer[i] = (byte)n;
					n >>= 8;
				}
			}
		}

		private static int Lcm(int x, int y)
			=> (x * y) / Gcd(x, y);

		private static int Gcd(int x, int y)
		{
			Debug.Assert(x > 0);
			Debug.Assert(y > 0);

			if (y > x)
			{
				return Gcd(y, x);
			}
			else
			{
				while (y > 0)
				{
					int n = x % y;
					x = y;
					y = n;
				}
				return x;
			}
		}
	}
}
