using System;
using System.Collections.Generic;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Kerberos
{
	static class KerberosReader
	{
		internal static AuthChecksumToken ReadAuthChecksum(Span<byte> buffer)
		{
			ByteMemoryReader reader = new ByteMemoryReader(buffer.ToArray());
			return reader.ReadPduStruct<AuthChecksumToken>();
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
