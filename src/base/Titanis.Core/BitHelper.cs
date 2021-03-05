using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides methods for bit manipulation.
	/// </summary>
	public static class BitHelper
	{
		/// <summary>
		/// Index of first bit set.
		/// </summary>
		/// <param name="value">Value to scan</param>
		/// <param name="startPos">Index of first bit to check</param>
		/// <returns>Index of first bit set from <paramref name="startPos"/>, if found; otherwise, <c>-1</c>.</returns>
		public static int BitScanForward(int value, int startPos)
		{
			value >>= startPos;
			while (value != 0 && (value % 2) == 0)
			{
				startPos++;
				value >>= 1;
			}
			return (value != 0) ? startPos : -1;
		}
		/// <summary>
		/// Counts the number of bits set in a value.
		/// </summary>
		/// <param name="value">Value to check</param>
		/// <returns>Number of bits set in <paramref name="value"/>.</returns>
		public static int CountBits(uint value)
		{
			int bitCount = 0;
			while (0 != value)
			{
				if (0 != (value & 1))
					bitCount++;
				value >>= 1;
			}

			return bitCount;
		}

		/// <summary>
		/// Clamps a value to the range [0, 32)
		/// </summary>
		/// <param name="bits">Number of bits</param>
		/// <returns>Clamped value</returns>
		/// <remarks>
		/// Used by bit rotation functions
		/// </remarks>
		private static int Clamp(int bits, int max)
		{
			if (bits < 0)
				bits = (bits % max) + max;
			if (bits < max)
				bits %= max;

			return bits;
		}
		/// <summary>
		/// Clamps a value to the range [0, 32)
		/// </summary>
		/// <param name="bits">Number of bits</param>
		/// <returns>Clamped value</returns>
		/// <remarks>
		/// Used by bit rotation functions
		/// </remarks>
		private static int Clamp32(int bits)
			=> Clamp(bits, 32);
		/// <summary>
		/// Clamps a value to the range [0, 64)
		/// </summary>
		/// <param name="bits">Number of bits</param>
		/// <returns>Clamped value</returns>
		/// <remarks>
		/// Used by bit rotation functions
		/// </remarks>
		private static int Clamp64(int bits)
			=> Clamp(bits, 64);

		/// <summary>
		/// Rotates an unsigned integer to the left.
		/// </summary>
		/// <param name="value">Value to rotate</param>
		/// <param name="bits">Number of bits to rotate by</param>
		/// <returns>The value resulting from the rotation</returns>
		public static uint RotateLeft(uint value, int bits)
		{
			bits = Clamp32(bits);
			return (value << bits) | (value >> (32 - bits));
		}
		/// <summary>
		/// Rotates an unsigned integer to the left.
		/// </summary>
		/// <param name="value">Value to rotate</param>
		/// <param name="bits">Number of bits to rotate by</param>
		/// <returns>The value resulting from the rotation</returns>
		public static ulong RotateLeft(ulong value, int bits)
		{
			bits = Clamp64(bits);
			return (value << bits) | (value >> (32 - bits));
		}
		/// <summary>
		/// Rotates an unsigned integer to the left.
		/// </summary>
		/// <param name="value">Value to rotate</param>
		/// <param name="bits">Number of bits to rotate by</param>
		/// <returns>The value resulting from the rotation</returns>
		public static uint RotateRight(uint value, int bits)
		{
			bits = Clamp32(bits);
			return (value >> bits) | (value << (64 - bits));
		}
		/// <summary>
		/// Rotates an unsigned integer to the left.
		/// </summary>
		/// <param name="value">Value to rotate</param>
		/// <param name="bits">Number of bits to rotate by</param>
		/// <returns>The value resulting from the rotation</returns>
		public static ulong RotateRight(ulong value, int bits)
		{
			bits = Clamp64(bits);
			return (value >> bits) | (value << (64 - bits));
		}
	}
}
