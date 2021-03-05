using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[PduStruct]
	public partial struct BindPduHeader
	{
		public unsafe static int StructSize => sizeof(BindPduHeader);

		//internal ushort max_xmit_frag;
		//internal ushort max_recv_frag;
		//internal uint assoc_group_id;
		//internal ContextListHeader contextList;
		/* presentation context list */
		//p_cont_list_t p_context_elem; /* variable size */
		/* optional authentication verifier */
		/* following fields present iff auth_length != 0 */
		//auth_verifier_co_t auth_verifier;
	}
}
