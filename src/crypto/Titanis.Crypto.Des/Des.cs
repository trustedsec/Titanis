// Inspired and adapted from: https://github.com/dhuertas/DES/blob/master/des.c
// His citation is below
/*
 * Data Encryption Standard
 * An approach to DES algorithm
 * 
 * By: Daniel Huertas Gonzalez
 * Email: huertas.dani@gmail.com
 * Version: 0.1
 * 
 * Based on the document FIPS PUB 46-3
 */


using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Crypto
{
	public class Des
	{
		const uint LsbMask32 = 0x00000001;
		const ulong LsbMask64 = 0x0000000000000001;
		const ulong Low32Mask = 0x00000000ffffffff;
		const ulong High32Mask = 0xffffffff00000000;
		const uint Low28Mask = 0x000000000fffffff;

		// Initial Permutation Table
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

		// Inverse Initial Permutation Table
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

		// Expansion table
		private static readonly byte[] E = new byte[]{
			32,  1,  2,  3,  4,  5,
			 4,  5,  6,  7,  8,  9,
			 8,  9, 10, 11, 12, 13,
			12, 13, 14, 15, 16, 17,
			16, 17, 18, 19, 20, 21,
			20, 21, 22, 23, 24, 25,
			24, 25, 26, 27, 28, 29,
			28, 29, 30, 31, 32,  1
		};

		// Post S-Box permutation
		private static readonly byte[] P = new byte[]{
			16,  7, 20, 21,
			29, 12, 28, 17,
			 1, 15, 23, 26,
			 5, 18, 31, 10,
			 2,  8, 24, 14,
			32, 27,  3,  9,
			19, 13, 30,  6,
			22, 11,  4, 25
		};

		// The S-Box tables
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

		// Permuted Choice 1 Table
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

		// Permuted Choice 2 Table
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

		// Iteration Shift Array
		private static readonly byte[] iteration_shift = new byte[]{
			/* 1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16 */
			   1,  1,  2,  2,  2,  2,  2,  2,  1,  2,  2,  2,  2,  2,  2,  1
		};

		public static ulong Encrypt(ulong key, ulong input) => Process(key, input, false);
		public static ulong Decrypt(ulong key, ulong input) => Process(key, input, true);

		public static ulong Process(ulong key64, ulong input, bool decrypt)
		{
			if (BitConverter.IsLittleEndian)
			{
				key64 = BinaryPrimitives.ReverseEndianness(key64);
				input = BinaryPrimitives.ReverseEndianness(input);
			}

			ulong ip = DoInitialPermutation(input);
			uint L = (uint)(ip >> 32);
			uint R = (uint)ip;

			ulong pc1 = DoPC1(key64);
			Span<ulong> subkeys = stackalloc ulong[16];
			CalcSubkeys(pc1, subkeys);

			for (int i = 0; i < 16; i++)
			{
				/* f(R,k) function */
				ulong s_input = 0;
				for (int j = 0; j < 48; j++)
				{
					s_input <<= 1;
					s_input |= (ulong)((R >> (32 - E[j])) & LsbMask32);
				}

				/*
                 * Encryption/Decryption
                 * XORing expanded Ri with Ki
                 */
				s_input ^= subkeys[decrypt ? (15 - i) : i];

				/* S-Box Tables */
				uint s = 0;
				for (int j = 0; j < 8; j++)
				{
					// 00 00 RCCC CR00 00 00 00 00 00 s_input
					// 00 00 1000 0100 00 00 00 00 00 row mask
					// 00 00 0111 1000 00 00 00 00 00 column mask

					byte row = (byte)((s_input & (0x0000840000000000U >> 6 * j)) >> 42 - 6 * j);
					row = (byte)((row >> 4) | row & 0x01);

					byte column = (byte)((s_input & (0x0000780000000000U >> 6 * j)) >> 43 - 6 * j);

					s <<= 4;
					s |= (uint)(S[j * 64 + (16 * row + column)] & 0x0f);

				}

				uint f = 0;
				for (int j = 0; j < 32; j++)
				{

					f <<= 1;
					f |= (s >> (32 - P[j])) & LsbMask32;

				}

				uint temp = R;
				R = L ^ f;
				L = temp;
			}

			ulong preout = (((ulong)R) << 32) | (ulong)L;

			/* inverse initial permutation */
			ulong invIP = 0;
			for (int i = 0; i < 64; i++)
			{

				invIP <<= 1;
				invIP |= (preout >> (64 - FP[i])) & LsbMask64;

			}

			if (BitConverter.IsLittleEndian)
			{
				invIP = BinaryPrimitives.ReverseEndianness(invIP);
			}

			return invIP;
		}

		public static ulong ExpandKey(ulong key64)
		{
			key64 = BinaryPrimitives.ReverseEndianness(key64);
			key64 >>= 8;

			ulong expanded = 0;
			for (int i = 0; i < 8; i++)
			{
				byte b = (byte)(key64 & 0x7F);
				key64 >>= 7;

				expanded <<= 8;
				expanded |= b;
			}

			expanded <<= 1;
			return expanded;
		}

		private static void CalcSubkeys(ulong pc1, Span<ulong> subkeys)
		{
			uint C = (uint)((pc1 >> 28) & Low28Mask);
			uint D = (uint)(pc1 & Low28Mask);

			for (int i = 0; i < 16; i++)
			{
				/* key schedule */
				// shifting Ci and Di
				for (int j = 0; j < iteration_shift[i]; j++)
				{
					C = Low28Mask & (C << 1) | LsbMask32 & (C >> 27);
					D = Low28Mask & (D << 1) | LsbMask32 & (D >> 27);
				}

				ulong pc2 = (((ulong)C) << 28) | D;
				subkeys[i] = CalcSubkey(pc2);
			}
		}

		private static ulong CalcSubkey(ulong pc2)
		{
			ulong subkey = 0;
			for (int i = 0; i < 48; i++)
			{
				subkey <<= 1;
				subkey |= (pc2 >> (56 - PC2[i])) & LsbMask64;
			}

			return subkey;
		}

		private static ulong DoPC1(ulong key)
		{
			ulong pc1 = 0;
			for (int i = 0; i < 56; i++)
			{
				pc1 <<= 1;
				pc1 |= (key >> (64 - PC1[i])) & LsbMask64;
			}

			return pc1;
		}

		private static ulong DoInitialPermutation(ulong input)
		{
			ulong res = 0;
			for (int i = 0; i < 64; i++)
			{
				res <<= 1;
				res |= (input >> (64 - IP[i])) & LsbMask64;
			}

			return res;
		}
	}
}
