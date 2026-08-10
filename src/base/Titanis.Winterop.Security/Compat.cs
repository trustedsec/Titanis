using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop.Security
{
	/// <summary>
	/// Implements shims for methods implemented in netstandard2.1 but unavailable in netstandard2.0.
	/// </summary>
	internal static class Compat
	{
		internal static bool TryParseGuid(ReadOnlySpan<char> chars, out Guid guid)
		{
#if NETSTANDARD2_1_OR_GREATER
			return Guid.TryParse(chars, out guid);
#else
			return Guid.TryParse(chars.ToString(), out guid);
#endif
		}

#if !NETSTANDARD2_1_OR_GREATER
		[StructLayout(LayoutKind.Sequential)]
		private struct GuidHeader
		{
			internal int a;
			internal short b;
			internal short c;
		}

		internal static bool TryWriteBytes(this Guid guid, Span<byte> buffer)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryWrite(buffer, ref guid);
			}
			else
			{
				if (MemoryMarshal.TryWrite(buffer, ref guid))
				{
					ref GuidHeader h = ref MemoryMarshal.Cast<byte, GuidHeader>(buffer)[0];
					h.a = BinaryPrimitives.ReverseEndianness(h.a);
					h.b = BinaryPrimitives.ReverseEndianness(h.b);
					h.c = BinaryPrimitives.ReverseEndianness(h.c);
					return true;
				}
				return false;
			}
		}
#endif

#if !NETSTANDARD2_1_OR_GREATER
		internal static int GetBytes(this Encoding encoding, string str, Span<byte> buffer)
		{
			unsafe
			{
				fixed (char* pChars = str)
				{
					fixed (byte* pBuf = buffer)
					{
						return encoding.GetBytes(pChars, str.Length, pBuf, buffer.Length);
					}
				}
			}
		}
#endif

#if !NETSTANDARD2_1_OR_GREATER
		internal static bool StartsWith(this ReadOnlySpan<char> str, string text)
		{
			if (text != null && str.Length >= text.Length)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] != text[i])
						return false;
				}
				return true;
			}
			return false;
		}
#endif

#if !NETSTANDARD2_1_OR_GREATER
		public delegate void SpanAction<T, TState>(Span<T> span, TState state);
#endif
		internal static string CreateString<TState>(int length, TState state, SpanAction<char, TState> action)
		{
#if NETSTANDARD2_1_OR_GREATER
			return string.Create(length, state, action);
#else
			char[] chars = new char[length];
			action(chars, state);
			return new string(chars);
#endif
		}
	}
}

#if NETSTANDARD2_1_OR_GREATER
#else

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
	struct HashCode
	{
		public static int Combine<T>(T obj)
		{
			return (obj?.GetHashCode() ?? 0);
		}
	}
}
#endif
