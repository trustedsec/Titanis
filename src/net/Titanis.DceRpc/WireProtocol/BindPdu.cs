namespace Titanis.DceRpc.WireProtocol
{
	// [C706] Š 12.6.4.3 - The bind PDU
	[PduStruct]
	public partial class BindPdu
	{
		internal ushort max_xmit_frag;
		internal ushort max_recv_frag;
		internal uint assoc_group_id;
		internal PresContextList contextList;

		public BindPdu()
		{

		}
		public BindPdu(params PresContext[] contexts)
		{
			this.contextList.contexts = contexts;
		}
	}
}