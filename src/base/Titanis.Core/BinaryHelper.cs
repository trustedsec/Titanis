using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Specifies options for a string of hexadecimal digits.
	/// </summary>
	/// <seealso cref="BinaryHelper.ToHexString(byte[])"/>
	public enum HexStringOptions
	{
		Default = 0,
		Lowercase = 0,

		/// <summary>
		/// Use uppercase letters for digits
		/// </summary>
		Uppercase = 1,
	}

	/// <summary>
	/// Provides methods for manipulating binary data.
	/// </summary>
	public static class BinaryHelper
	{

		private const string UppercaseHex = "0123456789ABCDEF";
		private const string LowercaseHex = "0123456789abcdef";

		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Array of bytes</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this byte[] bytes) => ToHexString(bytes.AsSpan(), HexStringOptions.Default);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Array of bytes</param>
		/// <param name="options">Options affecting how to create the string</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this byte[] bytes, HexStringOptions options) => ToHexString(bytes.AsSpan(), options);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Array of bytes</param>
		/// <param name="startIndex">Index of first byte to convert</param>
		/// <param name="count">Number of bytes to convert</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this byte[] bytes, int startIndex, int count) => ToHexString(bytes.AsSpan(startIndex, count), HexStringOptions.Default);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Array of bytes</param>
		/// <param name="startIndex">Index of first byte to convert</param>
		/// <param name="count">Number of bytes to convert</param>
		/// <param name="options">Options affecting how to create the string</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this byte[] bytes, int startIndex, int count, HexStringOptions options) => ToHexString(bytes.AsSpan(startIndex, count), options);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Span of bytes</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this Span<byte> bytes) => ToHexString(bytes, HexStringOptions.Default);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Span of bytes</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this ReadOnlySpan<byte> bytes) => ToHexString(bytes, HexStringOptions.Default);
		/// <summary>
		/// Converts a span of bytes to a string of hexadecimal digits.
		/// </summary>
		/// <param name="bytes">Span of bytes</param>
		/// <param name="options">Options affecting how to create the string</param>
		/// <returns>A string of hexadecimal digits</returns>
		public static string ToHexString(this ReadOnlySpan<byte> bytes, HexStringOptions options)
		{
			bool isUpper = (0 != (options & HexStringOptions.Uppercase));
			string charset = isUpper ? UppercaseHex : LowercaseHex;
			StringBuilder sb = new StringBuilder(bytes.Length * 2);
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				sb.Append(charset[(b >> 4)]);
				sb.Append(charset[(b % 16)]);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Parses a hexadecimal digit character.
		/// </summary>
		/// <param name="digit">Digit to parse</param>
		/// <returns>Numeric value of <paramref name="digit"/></returns>
		/// <exception cref="ArgumentException"><paramref name="digit"/> is not a valid hexadecimal digit</exception>
		public static int ParseHexChar(char digit)
		{
			uint n = (uint)(digit - '0');
			if (n < 10)
			{
				return unchecked((int)n);
			}
			else
			{
				n = (uint)(digit - 'A');
				if (n < 6)
					return unchecked((int)(n + 10));
				else
				{
					n = (uint)(digit - 'a');
					if (n < 6)
						return unchecked((int)(n + 10));
					else
						throw new ArgumentException(Messages.HexParse_InvalidHexDigit);
				}
			}
		}
		/// <summary>
		/// Parses a 2-digit hexadecimal number.
		/// </summary>
		/// <param name="high">High digit</param>
		/// <param name="low">Low digit</param>
		/// <returns>Numeric value of the pair of digits</returns>
		/// <exception cref="ArgumentException"><paramref name="high"/> or <paramref name="low"/> is not a valid hexadecimal digit</exception>
		public static int ParseHexByte(char high, char low)
			=> (ParseHexChar(high) << 4) | ParseHexChar(low);

		/// <summary>
		/// Parses a string of hexadecimal digits into bytes.
		/// </summary>
		/// <param name="chars">Hexadecimal characters</param>
		/// <returns>A byte array containing the numeric representation of <paramref name="chars"/>.</returns>
		/// <exception cref="ArgumentException">One of characters in <paramref name="chars"/> is not a valid hexadecimal digit.</exception>
		/// <exception cref="ArgumentException"><paramref name="chars"/> contains an odd number of digits</exception>
		/// <remarks>
		/// <paramref name="chars"/> must contain an even number of digits.
		/// </remarks>
		public static byte[] ParseHexString(ReadOnlySpan<char> chars)
		{
			if (chars.Length % 2 != 0)
				throw new ArgumentException(Messages.HexParse_InvalidSize, nameof(chars));

			int byteCount = chars.Length / 2;
			byte[] bytes = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				char c = chars[i * 2];
				byte b = (byte)(ParseHexChar(c) << 4);
				c = chars[i * 2 + 1];
				b |= (byte)ParseHexChar(c);

				bytes[i] = b;
			}

			return bytes;
		}

		public static byte[] ParseBase64(ReadOnlySpan<char> chars, out int validCharCount)
		{
			int charCount = 0;
			int i;
			for (i = 0; i < chars.Length; i++)
			{
				var c = chars[i];
				if (char.IsWhiteSpace(c))
					continue;
				else if (
					((uint)(c - '0') <= (uint)('9' - '0'))
					|| ((uint)(c - 'a') <= (uint)('z' - 'a'))
					|| ((uint)(c - 'A') <= (uint)('Z' - 'A'))
					|| (c is '/' or '+' or '-' or '_')
					)
				{
					charCount++;
				}
				else
					break;
			}

			int j;
			for (j = i; j < chars.Length; j++)
			{
				var c = chars[j];
				if (c != '=')
				{
					validCharCount = j;
					throw new FormatException($"The string contained an invalid Base64 character at position {j}.");
				}
			}

			validCharCount = j;

			int m = i % 4;
			int cb = i / 4 * 3 + (m switch
			{
				2 => 1,
				3 => 2,
				// 0, 1
				_ => throw new FormatException($"The Base64 string has an invalid length.")
			});
			byte[] bytes = new byte[cb];
			j = 0;
			uint n = 0;
			for (i = 0; i < chars.Length; i++)
			{
				var c = chars[i];
				if (char.IsWhiteSpace(c))
					continue;

				byte b;
				if ((uint)(c - 'A') <= (uint)('Z' - 'A'))
					b = (byte)(c - 'A');
				else if ((uint)(c - 'a') <= (uint)('z' - 'a'))
					b = (byte)(c - 'a' + 26);
				else if ((uint)(c - '0') <= (uint)('9' - '0'))
					b = (byte)(c - '0' + 52);
				else if (c is '+' or '-')
					b = 62;
				else if (c is '/' or '_' or ',')
					b = 63;
				else
					break;

				m = i % 4;
				if (m > 0)
				{
					n |= ((uint)b << ((4 - m) * 2));
					bytes[j] = (byte)n;
					n >>= 8;

					j++;
				}
				else
				{
					n = b;
				}
			}

			Debug.Assert(j == cb);
			return bytes;
		}

		/// <summary>
		/// Aligns a value to an alignment.
		/// </summary>
		/// <param name="value">Value to alignment</param>
		/// <param name="alignment">Desired alignment</param>
		/// <returns><paramref name="value"/> aligned to <paramref name="alignment"/>.</returns>
		public static int Align(int value, int alignment)
		{
			Debug.Assert(alignment > 0);

			int mod = value % alignment;
			if (mod != 0)
			{
				int n = alignment - mod;
				value += (alignment - mod);
			}
			return value;
		}
		/// <summary>
		/// Aligns a value to an alignment and bias.
		/// </summary>
		/// <param name="value">Value to alignment</param>
		/// <param name="alignment">Desired alignment</param>
		/// <param name="bias">Bias to add ot alignment</param>
		/// <returns><paramref name="value"/> aligned to <paramref name="alignment"/> with a bias of <paramref name="bias"/>.</returns>
		public static int Align(int value, int alignment, int bias)
		{
			Debug.Assert(alignment > 0);

			int mod = (value - bias) % alignment;
			if (mod != 0)
			{
				int n = alignment - mod;
				value += n;
			}
			return value;
		}

		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static short ToInt16BE(short n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;
		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static ushort ToUInt16BE(ushort n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;
		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static int ToInt32BE(int n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;
		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static uint ToUInt32BE(uint n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;
		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static long ToInt64BE(long n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;
		/// <summary>
		/// Converts a value to big-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The big-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in big-endian mode.
		/// </remarks>
		public static ulong ToUInt64BE(ulong n) => BitConverter.IsLittleEndian ? BinaryPrimitives.ReverseEndianness(n) : n;

		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static short ToInt16LE(short n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static ushort ToUInt16LE(ushort n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static int ToInt32LE(int n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static uint ToUInt32LE(uint n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static long ToInt64LE(long n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
		/// <summary>
		/// Converts a value to little-endian form.
		/// </summary>
		/// <param name="n">The value to convert, ordered according to the processor architecture.</param>
		/// <returns>The little-endian representation of <paramref name="n"/></returns>
		/// <remarks>
		/// This method only reorders <paramref name="n"/> if the CPU is not operating in little-endian mode.
		/// </remarks>
		public static ulong ToUInt64LE(ulong n) => BitConverter.IsLittleEndian ? n : BinaryPrimitives.ReverseEndianness(n);
	}
}
