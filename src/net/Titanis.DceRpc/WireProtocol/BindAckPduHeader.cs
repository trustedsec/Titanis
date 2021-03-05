using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct BindAckPduHeader
	{
		public unsafe static int StructSize => sizeof(BindAckPduHeader);

		internal ushort max_xmit_frag;
		internal ushort max_recv_frag;
		internal uint assoc_group_id;
	}
}
