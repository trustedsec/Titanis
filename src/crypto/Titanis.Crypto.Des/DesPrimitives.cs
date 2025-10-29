using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Titanis.Crypto
{
	public static class DesPrimitives
	{
		internal static ulong Transform(ref readonly DesSubkeys subkeys, ulong input, bool decrypt)
		{
			if (BitConverter.IsLittleEndian)
				input = BinaryPrimitives.ReverseEndianness(input);

			// [p.9]
			ulong ip = Permute64(input, IP);
			uint L = (uint)(ip >> 32);
			uint R = (uint)ip;

			for (int i = 0; i < DesSubkeys.SubkeyCount; i++)
			{
				// f(R,k) function
				ulong s_input = 0;
				for (int j = 0; j < 48; j++)
				{
					s_input <<= 1;
					s_input |= ((R >> E(j)) & 1);
				}

				/*
                 * Encryption/Decryption
                 * XORing expanded Ri with Ki
                 */
				s_input ^= subkeys[decrypt ? (15 - i) : i];

				// S-Box Tables [p.14]
				uint s = 0;
				for (int j = 0; j < 8; j++)
				{
					// 00 00 RCCC CR00 00 00 00 00 00 s_input
					// 00 00 1000 0100 00 00 00 00 00 row mask
					// 00 00 0111 1000 00 00 00 00 00 column mask

					byte row = (byte)((s_input & (0x0000_8400_0000_0000UL >> 6 * j)) >> 42 - 6 * j);
					row = (byte)((row >> 4) | row & 0x01);

					byte column = (byte)((s_input & (0x0000_7800_0000_0000UL >> 6 * j)) >> 43 - 6 * j);

					s <<= 4;
					s |= S[j * 64 + (16 * row + column)];
				}

				uint f = 0;
				for (int j = 0; j < 32; j++)
				{
					f <<= 1;
					f |= (s >> P[j]) & 1;
				}

				// L' = R, R' = L * f(R,K)
				(L, R) = (R, L ^ f);
			}

			ulong preout = (((ulong)R) << 32) | L;

			/* inverse initial permutation */
			ulong invIP = Permute64(preout, FP);

			if (BitConverter.IsLittleEndian)
				invIP = BinaryPrimitives.ReverseEndianness(invIP);

			return invIP;
		}

		// Inverse Initial Permutation Table [p.10]
		private static readonly byte[] FP = new byte[]{
			40, 8, 48, 16, 56, 24, 64, 32,
			39, 7, 47, 15, 55, 23, 63, 31,
			38, 6, 46, 14, 54, 22, 62, 30,
			37, 5, 45, 13, 53, 21, 61, 29,
			36, 4, 44, 12, 52, 20, 60, 28,
			35, 3, 43, 11, 51, 19, 59, 27,
			34, 2, 42, 10, 50, 18, 58, 26,
			33, 1, 41,  9, 49, 17, 57, 25
		};

		// Post S-Box permutation [p.15]
		private static readonly byte[] P = new byte[]{
			16, 25, 12, 11,
			 3, 20,  4, 15,
			31, 17,  9, 6,
			27, 14,  1, 22,
			30, 24,  8, 18,
			 0,  5, 29, 23,
			13, 19,  2, 26,
			10, 21, 28,  7
		};

		// Expansion table [p.13]
		private static byte E(int j) => (byte)((32 - j + (j / 6 * 2)) & 0x1F);

		// The S-Box tables [p.18]
		private static readonly byte[] S = new byte[] {
			/* S1 */
			14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7,
			 0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8,
			 4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0,
			15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13,

			/* S2 */
			15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10,
			 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5,
			 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15,
			13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9,

			/* S3 */
			10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8,
			13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1,
			13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7,
			 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12,

			/* S4 */
			 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15,
			13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9,
			10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4,
			 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14,

			/* S5 */
			 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9,
			14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6,
			 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14,
			11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3,

			/* S6 */
			12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11,
			10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8,
			 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6,
			 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13,

			/* S7 */
			 4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1,
			13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6,
			 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2,
			 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12,

			/* S8 */
			13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7,
			 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2,
			 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8,
			 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11
		};

		// Initial Permutation Table [p.10]
		private static readonly byte[] IP = new byte[]{
			58, 50, 42, 34, 26, 18, 10, 2,
			60, 52, 44, 36, 28, 20, 12, 4,
			62, 54, 46, 38, 30, 22, 14, 6,
			64, 56, 48, 40, 32, 24, 16, 8,
			57, 49, 41, 33, 25, 17,  9, 1,
			59, 51, 43, 35, 27, 19, 11, 3,
			61, 53, 45, 37, 29, 21, 13, 5,
			63, 55, 47, 39, 31, 23, 15, 7
		};

		private static ulong Permute64(ulong input, byte[] p)
		{
			ulong res = 0;
			for (int i = 0; i < p.Length; i++)
			{
				res <<= 1;
				res |= (input >> (64 - p[i])) & 1;
			}

			return res;
		}

		#region Key schedule
		// Permuted Choice 1 Table [p.19]
		private static readonly byte[] PC1 = {
			57, 49, 41, 33, 25, 17,  9,
			 1, 58, 50, 42, 34, 26, 18,
			10,  2, 59, 51, 43, 35, 27,
			19, 11,  3, 60, 52, 44, 36,

			63, 55, 47, 39, 31, 23, 15,
			 7, 62, 54, 46, 38, 30, 22,
			14,  6, 61, 53, 45, 37, 29,
			21, 13,  5, 28, 20, 12,  4
		};

		internal static ulong DoPC1(ulong key)
		{
			ulong pc1 = 0;
			for (int i = 0; i < PC1.Length; i++)
			{
				pc1 <<= 1;
				pc1 |= (key >> (64 - PC1[i])) & 1;
			}

			return pc1;
		}

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

		private static ulong CalcSubkey(ulong pc2)
		{
			ulong subkey = 0;
			for (int i = 0; i < PC2.Length; i++)
			{
				subkey <<= 1;
				subkey |= (pc2 >> (56 - PC2[i])) & 1;
			}

			return subkey;
		}
		#endregion

		// Permuted Choice 2 Table [p.21]
		private static readonly byte[] PC2 = new byte[]{
			14, 17, 11, 24,  1,  5,
			 3, 28, 15,  6, 21, 10,
			23, 19, 12,  4, 26,  8,
			16,  7, 27, 20, 13,  2,
			41, 52, 31, 37, 47, 55,
			30, 40, 51, 45, 33, 48,
			44, 49, 39, 56, 34, 53,
			46, 42, 50, 36, 29, 32
		};
	}
}
