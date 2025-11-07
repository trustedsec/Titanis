using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	public abstract class SamAccount : SamObject
	{
		internal SamAccount(SamClient samClient, RpcContextHandle handle, SecurityIdentifier sid)
			: base(samClient, handle)
		{
			Sid = sid;
		}

		public SecurityIdentifier Sid { get; }
	}
}