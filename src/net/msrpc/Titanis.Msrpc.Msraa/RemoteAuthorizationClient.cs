using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msraa
{
	// [MS-RAA] § 3.1.4.2 AuthzrInitializeContextFromSid (Opnum 1)
	[Flags]
	public enum RemoteAuthorizationOptions
	{
		None = 0,
		ComputePrivileges = 8
	}

	public class RemoteAuthorizationClient : RpcServiceClient<ms_raa.authzrClientProxy>
	{
		// [MS-RAA] § 2.1 Transport
		public override bool SupportsDynamicTcp => true;

		internal ms_raa.authzrClientProxy ClientProxy => this._proxy;

		// [MS-RAA] § 2.1 Transport
		public override Guid? ObjectId => new Guid("9a81c2bd-a525-471d-a4ed-49907c0b23da");
		// [MS-RAA] § 2.1 Transport
		public Guid? ObjectId2 => new Guid("5fc860e0-6f6e-4fc2-83cd-46324f25e90b");

		public override bool RequiresEncryptionOverTcp => true;

		public async Task<RemoteAuthorizationContext> Initialize(SecurityIdentifier sid, RemoteAuthorizationOptions options, CancellationToken cancellationToken, uint luidLow = 0, int luidHigh = 0)
		{
			ArgumentNullException.ThrowIfNull(sid);

			DceRpc.RpcPointer<DceRpc.RpcContextHandle> contextHandle = new();
			var res = (Win32ErrorCode)await _proxy.AuthzrInitializeContextFromSid(
				(uint)options,
				sid.ToRpcSid(),
				null,
				new ms_dtyp.LUID() { LowPart = luidLow, HighPart = luidHigh },
				contextHandle,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new RemoteAuthorizationContext(this, contextHandle);
		}

	}
}
