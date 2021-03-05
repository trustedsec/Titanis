using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct VerifyTrailerSignature
	{
		internal static unsafe int StructSize => sizeof(VerifyTrailerSignature);
		internal const int Alignment = 4;

		// [MS-RPCE] § 2.2.2.13.1 - rpc_sec_verification_trailer
		internal const ulong ValidSignature = 0x7136f4027113e38a;

		internal ulong sig;
	}

	[Flags]
	enum VerifyTrailerCommand : ushort
	{
		Bitmask = 1,
		PContext = 2,
		Header2 = 3,

		End = 0x4000,
		MustProcess = 0x8000,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct VerifyTrailerPContext
	{
		const int HeaderSize = 4;
		internal static unsafe int StructSize => sizeof(VerifyTrailerPContext);
		internal static unsafe ushort DataSize => (ushort)(StructSize - HeaderSize);

		internal VerifyTrailerCommand command;
		internal ushort length;
		internal SyntaxId interfaceId;
		internal SyntaxId transferSyntaxId;
	}
}
