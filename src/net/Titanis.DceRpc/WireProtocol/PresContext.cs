using Titanis.IO;

namespace Titanis.DceRpc.WireProtocol
{
	// [C706] § 12.6.3.1 - Declarations > p_cont_elem_t
	[PduStruct]
	public partial struct PresContext
	{
		public ushort /* p_context_id_t */ p_cont_id;
		private byte n_transfer_syn; /* number of items */
		public byte reserved; /* alignment pad, m.b.z. */
		public SyntaxId abstract_syntax; /* transfer syntax list */

		[PduArraySize(nameof(n_transfer_syn))]
		public SyntaxId[] transferSyntaxes;

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			this.n_transfer_syn = (byte)this.transferSyntaxes.Length;
		}
	}
}
