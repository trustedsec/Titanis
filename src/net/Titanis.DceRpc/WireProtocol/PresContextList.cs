using System.Runtime.InteropServices;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.DceRpc.WireProtocol
{
	// [C706] § 12.6.3.1 - Declarations > p_cont_list_t
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[PduStruct]
	public partial struct PresContextList
	{
		private byte n_context_elem; /* number of items */
		private byte reserved; /* alignment pad, m.b.z. */
		private ushort reserved2; /* alignment pad, m.b.z. */

		[PduArraySize(nameof(n_context_elem))]
		public PresContext[] contexts;

		private int mystrLen => 5;
		[PduString(CharSet.Ansi, nameof(mystrLen))]
		public string mystr;

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			this.n_context_elem = (byte)this.contexts.Length;
		}
	}
}
