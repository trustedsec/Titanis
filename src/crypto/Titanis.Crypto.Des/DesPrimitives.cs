using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Crypto
{
	public static class DesPrimitives
	{
		public static ulong EncryptBlock(ulong key, ulong input)
		{
			DesContext ctx = new DesContext(key);
			return ctx.Transform(input, false);
		}
		public static ulong DecryptBlock(ulong key, ulong input)
		{
			DesContext ctx = new DesContext(key);
			return ctx.Transform(input, true);
		}

		internal static ulong AddParityAndReverse(ulong bits56)
		{
			ulong withParity = 0;
			bits56 <<= 1;
			for (int i = 0; i < 8; i++)
			{
				byte b = (byte)(bits56 & 0xFF);
				if ((BitOperations.PopCount(b) & 1) == 0)
					b ^= 1;

				withParity <<= 8;
				withParity |= b;

				bits56 >>= 7;
			}

			Debug.Assert((BitOperations.PopCount(withParity) & 1) == 0);
			return withParity;
		}

		public static ulong ExpandKey(ulong key56)
		{
			// UNDONE: This was meant as a sanity check
			// However, NTLM passes 64 bits with the expectation that this implementation will ignore them
			//if (key56 >= (1UL << 56))
			//	throw new ArgumentNullException("Key must only be 56 bits.", nameof(key56));

			var key64 = AddParityAndReverse(BinaryPrimitives.ReverseEndianness(key56) >> 8);
			return key64;
		}

		internal static ulong Transform(ref readonly DesSubkeys subkeys, ulong input, bool decrypt)
		{
			if (BitConverter.IsLittleEndian)
				input = BinaryPrimitives.ReverseEndianness(input);

			// [p.9]
			ulong ip = Permute(input, IP_BE);
			uint L = (uint)(ip >> 32);
			uint R = (uint)ip;

			for (int i = 0; i < DesSubkeys.SubkeyCount; i++)
			{
				// [p.13]
				ulong P = Expand(R);
				P ^= subkeys[decrypt ? (15 - i) : i];

				// P has 48 bits

				// S-Box Tables [p.14]
				uint s = 0;
				for (int j = 0; j < 8; j++)
				{
					// Take the high 6 bits (from a 48-bit number) to get the row/column
					var sIndex = (uint)(P >> 42) & 0b11_1111;

					// It would be nice to take the low bits but that's a whole thing.  Another day.
					//var sIndex = (uint)(P & 0b11_1111);

					P <<= 6;
					//P >>= 6;

					s <<= 4;
					var s_i = S[j * 64 + sIndex];
					s |= s_i;
				}

				uint f = (uint)Permute(s, DesPrimitives.P);

				// L' = R, R' = L * f(R,K)
				(L, R) = (R, L ^ f);
			}

			// Reunite the 2 halves
			ulong preout = (((ulong)R) << 32) | L;
			ulong invIP = Permute(preout, FP_BE);

			if (BitConverter.IsLittleEndian)
				invIP = BinaryPrimitives.ReverseEndianness(invIP);

			return invIP;
		}

		// Inverse Initial Permutation Table [p.10] (64-r)
		private static readonly byte[] FP_BE = [
			24, 56, 16, 48,  8, 40,  0, 32,
			25, 57, 17, 49,  9, 41,  1, 33,
			26, 58, 18, 50, 10, 42,  2, 34,
			27, 59, 19, 51, 11, 43,  3, 35,
			28, 60, 20, 52, 12, 44,  4, 36,
			29, 61, 21, 53, 13, 45,  5, 37,
			30, 62, 22, 54, 14, 46,  6, 38,
			31, 63, 23, 55, 15, 47,  7, 39
		];

		// Post S-Box permutation [p.15]
		private static readonly byte[] P = [
			16, 25, 12, 11,
			 3, 20,  4, 15,
			31, 17,  9, 6,
			27, 14,  1, 22,
			30, 24,  8, 18,
			 0,  5, 29, 23,
			13, 19,  2, 26,
			10, 21, 28,  7
		];

		// The S-Box tables [p.18] (permuted from CCCCRR => RCCCCR)
		public static readonly byte[] S = [
			// S1
			14,  0,  4, 15, 13,  7,  1,  4,  2, 14, 15,  2, 11, 13,  8,  1,
			 3, 10, 10,  6,  6, 12, 12, 11,  5,  9,  9,  5,  0,  3,  7,  8,
			 4, 15,  1, 12, 14,  8,  8,  2, 13,  4,  6,  9,  2,  1, 11,  7,
			15,  5, 12, 11,  9,  3,  7, 14,  3, 10, 10,  0,  5,  6,  0, 13,
			// S2
			15,  3,  1, 13,  8,  4, 14,  7,  6, 15, 11,  2,  3,  8,  4, 14,
			 9, 12,  7,  0,  2,  1, 13, 10, 12,  6,  0,  9,  5, 11, 10,  5,
			 0, 13, 14,  8,  7, 10, 11,  1, 10,  3,  4, 15, 13,  4,  1,  2,
			 5, 11,  8,  6, 12,  7,  6, 12,  9,  0,  3,  5,  2, 14, 15,  9,
			// S3
			10, 13,  0,  7,  9,  0, 14,  9,  6,  3,  3,  4, 15,  6,  5, 10,
			 1,  2, 13,  8, 12,  5,  7, 14, 11, 12,  4, 11,  2, 15,  8,  1,
			13,  1,  6, 10,  4, 13,  9,  0,  8,  6, 15,  9,  3,  8,  0,  7,
			11,  4,  1, 15,  2, 14, 12,  3,  5, 11, 10,  5, 14,  2,  7, 12,
			// S4
			 7, 13, 13,  8, 14, 11,  3,  5,  0,  6,  6, 15,  9,  0, 10,  3,
			 1,  4,  2,  7,  8,  2,  5, 12, 11,  1, 12, 10,  4, 14, 15,  9,
			10,  3,  6, 15,  9,  0,  0,  6, 12, 10, 11,  1,  7, 13, 13,  8,
			15,  9,  1,  4,  3,  5, 14, 11,  5, 12,  2,  7,  8,  2,  4, 14,
			// S5
			 2, 14, 12, 11,  4,  2,  1, 12,  7,  4, 10,  7, 11, 13,  6,  1,
			 8,  5,  5,  0,  3, 15, 15, 10, 13,  3,  0,  9, 14,  8,  9,  6,
			 4, 11,  2,  8,  1, 12, 11,  7, 10,  1, 13, 14,  7,  2,  8, 13,
			15,  6,  9, 15, 12,  0,  5,  9,  6, 10,  3,  4,  0,  5, 14,  3,
			// S6
			12, 10,  1, 15, 10,  4, 15,  2,  9,  7,  2, 12,  6,  9,  8,  5,
			 0,  6, 13,  1,  3, 13,  4, 14, 14,  0,  7, 11,  5,  3, 11,  8,
			 9,  4, 14,  3, 15,  2,  5, 12,  2,  9,  8,  5, 12, 15,  3, 10,
			 7, 11,  0, 14,  4,  1, 10,  7,  1,  6, 13,  0, 11,  8,  6, 13,
			// S7
			 4, 13, 11,  0,  2, 11, 14,  7, 15,  4,  0,  9,  8,  1, 13, 10,
			 3, 14, 12,  3,  9,  5,  7, 12,  5,  2, 10, 15,  6,  8,  1,  6,
			 1,  6,  4, 11, 11, 13, 13,  8, 12,  1,  3,  4,  7, 10, 14,  7,
			10,  9, 15,  5,  6,  0,  8, 15,  0, 14,  5,  2,  9,  3,  2, 12,
			// S8
			13,  1,  2, 15,  8, 13,  4,  8,  6, 10, 15,  3, 11,  7,  1,  4,
			10, 12,  9,  5,  3,  6, 14, 11,  5,  0,  0, 14, 12,  9,  7,  2,
			 7,  2, 11,  1,  4, 14,  1,  7,  9,  4, 12, 10, 14,  8,  2, 13,
			 0, 15,  6, 12, 10,  9, 13,  0, 15,  3,  3,  5,  5,  6,  8, 11,
		];

		// Expansion table [p.13]
		private static byte E(int j) => (byte)((32 - j + (j / 6 * 2)) & 0x1F);
		//private static byte E(int j) => (byte)((j / 6 * 10 + 4 - j) & 0x1F);
		/*
32 1 2 3 4 5
4 5 6 7 8 9
8 9 10 11 12 13
12 13 14 15 16 17
16 17 18 19 20 21
20 21 22 23 24 25
24 25 26 27 28 29
28 29 30 31 32 1

Flipped:

32 - ((j+28) - (j/6*10))

28 29 30 31 32 1
24 25 26 27 28 29
20 21 22 23 24 25
16 17 18 19 20 21
12 13 14 15 16 17
8 9 10 11 12 13
4 5 6 7 8 9
32 1 2 3 4 5


(j/6 * 10) + 4 - j
4 3 2 1 0 31
8 7 6 5 4 3
12 11 10 9 8 7
16 15 14 13 12 11
...
0 31 30 29 28 27
		*/

		// [p.13]
		private static ulong Expand(ulong R)
		{
			ulong n = 0;
			for (int j = 0; j < 48; j++)
			{
				n <<= 1;
				n |= ((R >> E(j)) & 1);
			}
			return n;
		}

		// Initial Permutation Table [p.10] (64-r)
		private static readonly byte[] IP_BE = [
			 6, 14, 22, 30, 38, 46, 54, 62,
			 4, 12, 20, 28, 36, 44, 52, 60,
			 2, 10, 18, 26, 34, 42, 50, 58,
			 0,  8, 16, 24, 32, 40, 48, 56,
			 7, 15, 23, 31, 39, 47, 55, 63,
			 5, 13, 21, 29, 37, 45, 53, 61,
			 3, 11, 19, 27, 35, 43, 51, 59,
			 1,  9, 17, 25, 33, 41, 49, 57
		];

		private static ulong Permute(ulong input, byte[] p)
		{
			ulong res = 0;
			for (int i = 0; i < p.Length; i++)
			{
				res <<= 1;
				res |= (input >> p[i]) & 1;
			}

			return res;
		}

		internal static ulong DoPC1(ulong key) => Permute(key, PC1);
		private static ulong CalcSubkey(ulong pc2) => Permute(pc2, PC2);

		#region Key schedule
		// Permuted Choice 1 Table [p.19] (64-r)
		private static readonly byte[] PC1 = [
			 7, 15, 23, 31, 39, 47, 55, 63,
			 6, 14, 22, 30, 38, 46, 54, 62,
			 5, 13, 21, 29, 37, 45, 53, 61,
			 4, 12, 20, 28,  1,  9, 17, 25,
			33, 41, 49, 57,  2, 10, 18, 26,
			34, 42, 50, 58,  3, 11, 19, 27,
			35, 43, 51, 59, 36, 44, 52, 60
		];

		// Permuted Choice 2 Table [p.21] (56-r)
		private static readonly byte[] PC2 = [
			42, 39, 45, 32, 55, 51, 53, 28,
			41, 50, 35, 46, 33, 37, 44, 52,
			30, 48, 40, 49, 29, 36, 43, 54,
			15,  4, 25, 19,  9,  1, 26, 16,
			 5, 11, 23,  8, 12,  7, 17,  0,
			22,  3, 10, 14,  6, 20, 27, 24
		];

		// Iteration Shift Array [p.21]
		private static readonly ushort IS = 0b0111111011111100;
		const ulong Mask1_28 = 1UL | (1UL << 28);
		private static ulong RotateLeft28(ulong value) => ((value >> 27) & Mask1_28) | ((value << 1) & ~Mask1_28);
		const ulong Mask1_28_2 = 1UL | 2UL | (1UL << 28) | (1UL << 29);
		private static ulong RotateLeft28_2(ulong value) => ((value >> 26) & Mask1_28_2) | ((value << 2) & ~Mask1_28_2);
		internal static void CalcSubkeys(ulong pc1, ref DesSubkeys subkeys)
		{
			var shifts = IS;
			for (int i = 0; i < 16; i++)
			{
				pc1 = ((shifts & 1) == 1) ? RotateLeft28_2(pc1) : RotateLeft28(pc1);
				shifts >>= 1;
				subkeys[i] = CalcSubkey(pc1);
			}
		}
		#endregion
	}
}
