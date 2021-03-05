using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	// [C706] § 12.6.4.9
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct RequestPduHeader
	{
		public static unsafe int StructSize => sizeof(RequestPduHeader);
		public static unsafe int StructSizeWithObjectId => sizeof(RequestPduHeader) + 16;

		internal uint alloc_hint; /* 16:04 allocation hint */
		internal ushort p_cont_id; /* 20:02 pres context, i.e. data rep */
		internal ushort opnum;
	}
}
