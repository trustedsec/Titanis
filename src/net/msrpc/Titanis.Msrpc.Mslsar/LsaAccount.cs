using ms_lsar;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaAccount : LsaObject
	{
		internal LsaAccount(LsaClient lsaClient, RpcContextHandle handle, SecurityIdentifier sid)
			: base(lsaClient, handle)
		{
			this.Sid = sid;
		}

		public SecurityIdentifier Sid { get; }

		public async Task<PrivilegeInfo[]> GetPrivileges(CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<LSAPR_PRIVILEGE_SET>> privileges = new();
			var res = (Ntstatus)await this._lsaClient.ClientProxy.LsarEnumeratePrivilegesAccount(this._handle, privileges, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return Array.ConvertAll(privileges.value.value.Privilege, r => new PrivilegeInfo(this.Sid, r.Luid.AsPrivilege(), (PrivilegeAttributes)r.Attributes));
		}

		public Task AddPrivileges(IList<PrivilegeInfo> privs, CancellationToken cancellationToken)
			=> this._lsaClient.AddPrivileges(this._handle, privs, cancellationToken);
		public Task RemoveAllPrivileges(CancellationToken cancellationToken)
			=> this._lsaClient.RemoveAllPrivileges(this._handle, cancellationToken);
		public Task RemovePrivileges(IList<PrivilegeInfo> privs, CancellationToken cancellationToken)
			=> this._lsaClient.RemovePrivileges(this._handle, privs, cancellationToken);

		public Task<SystemAccessRights> GetSystemAccess(CancellationToken cancellationToken)
			=> this._lsaClient.GetSystemAccess(this._handle, cancellationToken);

		public Task SetSystemAccess(SystemAccessRights rights, CancellationToken cancellationToken)
			=> this._lsaClient.SetSystemAccess(this._handle, rights, cancellationToken);
	}
}