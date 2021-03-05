using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mssamr
{
	public sealed class SamGroup : SamObject
	{
		internal SamGroup(SamClient samClient, RpcContextHandle handle) : base(samClient, handle)
		{
		}

		public Task<List<SamMemberInfo>> EnumMembersAsync(CancellationToken cancellationToken)
			=> this._samClient.EnumGroupMembers(this._handle, cancellationToken);

		public Task<SamGroupGeneralInfo> QueryGeneralInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryGroupGeneralInfo(this._handle, cancellationToken);
		public Task<SamGroupGeneralInfo> QueryReplicaInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryGroupReplicaInfo(this._handle, cancellationToken);
		public Task<string> QueryName(CancellationToken cancellationToken)
			=> this._samClient.QueryGroupNameInfo(this._handle, cancellationToken);
		public Task<string> QueryAdminComment(CancellationToken cancellationToken)
			=> this._samClient.QueryGroupAdminComment(this._handle, cancellationToken);
		public Task<SamGroupAttributes> QueryAttributes(CancellationToken cancellationToken)
			=> this._samClient.QueryGroupAttrInfo(this._handle, cancellationToken);

		public Task SetName(string name, CancellationToken cancellationToken)
			=> this._samClient.SetGroupName(this._handle, name, cancellationToken);
		public Task SetComment(string name, CancellationToken cancellationToken)
			=> this._samClient.SetGroupComment(this._handle, name, cancellationToken);

		public Task Delete(CancellationToken cancellationToken)
			=> this._samClient.DeleteGroup(this._handle, cancellationToken);
	}
}