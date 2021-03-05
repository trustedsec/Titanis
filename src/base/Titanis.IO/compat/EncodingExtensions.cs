using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public static class EncodingExtensions
	{
		public static unsafe string GetString(
			this Encoding encoding,
			ReadOnlySpan<byte> span)
		{
			fixed (byte* pBuf = span)
			{
				return encoding.GetString(pBuf, span.Length);
			}
		}

		public unsafe static int GetChars(
			this Encoding encoding,
			ReadOnlySpan<byte> bytes,
			Span<char> chars)
		{
			fixed (byte* pBuf = bytes)
			{
				fixed (char* pChars = chars)
				{
					return encoding.GetChars(pBuf, bytes.Length, pChars, chars.Length);
				}
			}
		}

		public unsafe static int GetByteCount(
			this Encoding encoding,
			ReadOnlySpan<char> chars
			)
		{
			fixed (char* pChars = chars)
			{
				return encoding.GetByteCount(pChars, chars.Length);
			}
		}

		public unsafe static int GetBytes(
			this Encoding encoding,
			ReadOnlySpan<char> chars,
			Span<byte> bytes
			)
		{
			fixed (char* pChars = chars)
			{
				fixed (byte* pBuf = bytes)
				{
					return encoding.GetBytes(pChars, chars.Length, pBuf, bytes.Length);
				}
			}
		}

		public unsafe static int GetBytes(this Encoding encoding, string str, Span<byte> bytes)
		{
			fixed (char* pChars = str)
			{
				fixed (byte* pBuf = bytes)
				{
					return encoding.GetBytes(pChars, str.Length, pBuf, bytes.Length);
				}
			}
		}

		public unsafe static string CreateString(this ReadOnlySpan<char> chars)
		{
			fixed (char* pChars = chars)
			{
				return new string(pChars, 0, chars.Length);
			}
		}
	}
}
