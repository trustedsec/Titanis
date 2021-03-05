using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaAccount : LsaObject
	{
		public LsaAccount(LsaClient lsaClient, RpcContextHandle handle)
			: base(lsaClient, handle)
		{
		}

		public Task<PrivilegeInfo[]> GetPrivileges(CancellationToken cancellationToken)
			=> this._lsaClient.GetAccountPrivileges(this._handle, cancellationToken);
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