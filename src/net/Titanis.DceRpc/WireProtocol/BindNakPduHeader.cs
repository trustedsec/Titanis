using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct BindNakPduHeader
	{
		public static unsafe int StructSize => sizeof(BindNakPduHeader);

		internal BindRejectReason provider_reject_reason;

		public BindNakPduHeader(BindRejectReason reason)
		{
			this.provider_reject_reason = reason;
		}
	}
}
