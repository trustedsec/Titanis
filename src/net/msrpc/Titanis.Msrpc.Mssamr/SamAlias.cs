using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mssamr
{
	public sealed class SamAlias : SamObject
	{
		public SamAlias(SamClient samClient, RpcContextHandle handle)
			: base(samClient, handle)
		{
		}

		public Task<List<SamSid>> EnumMembersAsync(CancellationToken cancellationToken)
			=> this._samClient.EnumAliasMembers(this._handle, cancellationToken);

		public Task<SamAliasGeneralInfo> QueryGeneralInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryAliasGeneralInfo(this._handle, cancellationToken);
		public Task<string> QueryName(CancellationToken cancellationToken)
			=> this._samClient.QueryAliasNameInfo(this._handle, cancellationToken);
		public Task<string> QueryAdminComment(CancellationToken cancellationToken)
			=> this._samClient.QueryAliasAdminComment(this._handle, cancellationToken);

		public Task SetName(string name, CancellationToken cancellationToken)
			=> this._samClient.SetAliasName(this._handle, name, cancellationToken);
		public Task SetComment(string name, CancellationToken cancellationToken)
			=> this._samClient.SetAliasComment(this._handle, name, cancellationToken);

		public Task Delete(CancellationToken cancellationToken)
			=> this._samClient.DeleteAlias(this._handle, cancellationToken);
	}
}