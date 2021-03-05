using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	static class KerberosReader
	{
		internal static unsafe ref AuthChecksumToken ReadAuthChecksum(Span<byte> buffer)
		{
			if (buffer.Length < AuthChecksumToken.StructSize)
				throw new ArgumentOutOfRangeException(nameof(buffer));

			fixed (byte* pBuf = buffer)
			{
				return ref *(AuthChecksumToken*)pBuf;
			}
		}

		internal static unsafe ref readonly WrapToken ReadWrapToken(ReadOnlySpan<byte> buffer)
		{
			if (buffer.Length < WrapToken.StructSize)
				throw new ArgumentOutOfRangeException(nameof(buffer));

			fixed (byte* pBuf = buffer)
			{
				return ref *(WrapToken*)pBuf;
			}
		}

		internal static unsafe ref WrapToken ReadWrapToken(Span<byte> buffer)
		{
			if (buffer.Length < WrapToken.StructSize)
				throw new ArgumentOutOfRangeException(nameof(buffer));

			fixed (byte* pBuf = buffer)
			{
				return ref *(WrapToken*)pBuf;
			}
		}
	}
}
