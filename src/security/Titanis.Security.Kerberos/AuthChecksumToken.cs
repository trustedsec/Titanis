using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Security.Kerberos
{
	enum GssapiTokenId : ushort
	{
		APReq = 0x0100,
		APRep = 0x0200,
		Error = 0x0300,
	}

	// [RFC 4121] § 4.1.1 - Authenticator Checksum
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AuthChecksumToken
	{
		internal static unsafe int StructSize => sizeof(AuthChecksumToken);

		// [RFC 4121] § 4.1.1 - Authenticator Checksum
		public const int ChecksumType = 0x8003;

		internal uint bindLength;
		internal Guid channelBind;
		internal SecurityCapabilities capabilities;

		internal unsafe Span<byte> AsSpan()
		{
			fixed (AuthChecksumToken* pThis = &this)
			{
				return new Span<byte>((byte*)pThis, sizeof(AuthChecksumToken));
			}
		}
	}
}
