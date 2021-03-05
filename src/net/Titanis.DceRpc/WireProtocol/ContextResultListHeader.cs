using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct ContextResultListHeader
	{
		public unsafe static int StructSize => sizeof(ContextResultListHeader);

		internal byte n_results; /* count */
		internal byte reserved; /* alignment pad, m.b.z. */
		internal ushort reserved2; /* alignment pad, m.b.z. */
		//p_result_t[size_is(n_results)] p_results[];
	}
}
