using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	// [C706] § 12.6.3.1
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[PduStruct]
	public partial struct PduHeader
	{
		public static unsafe int StructSize = sizeof(PduHeader);

		public byte rpcVersMajor;
		public byte rpcVersMinor;
		public PduType ptype;
		public PfcFlags pfc_flags;
		public uint drep;
		public ushort fragLength;
		public ushort authLength;
		public int callId;

		internal unsafe Span<byte> AsSpan()
		{
			fixed (byte* pStruc = &this.rpcVersMajor)
			{
				return new Span<byte>(pStruc, PduStructSize);
			}
		}
	}
}
