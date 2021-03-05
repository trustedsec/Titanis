using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct ResponsePduHeader
	{
		internal unsafe static int StructSize => sizeof(ResponsePduHeader);

		/* needed for request, response, fault */
		internal uint alloc_hint; /* 16:04 allocation hint */
		internal ushort p_cont_id; /* 20:02 pres context, i.e. data rep */
		/* needed for response or fault */
		internal byte cancel_count; /* 22:01 cancel count*/
		internal byte reserved; /* 23:01 reserved, m.b.z. */
	}
}
