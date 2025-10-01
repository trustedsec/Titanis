using System;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{

	public class LsaPolicy : LsaObject
	{
		public LsaPolicy(LsaClient lsaClient, RpcContextHandle handle)
			: base(lsaClient, handle)
		{
		}

		public Task<LsaAccountMapping> ResolveAccountName(string name, CancellationToken cancellationToken)
			=> this._lsaClient.LookupAccountName(this._handle, name, cancellationToken);
		/// <summary>
		/// Resolves the domain and SID for one or more account names.
		/// </summary>
		/// <param name="names">Account names to resolve</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An array of <see cref="LsaAccountMapping"/> for each entry in <paramref name="names"/></returns>
		/// <exception cref="LsaAccountMappingException">Some but not all of <paramref name="names"/> could be resolved.</exception>
		public Task<LsaAccountMapping[]> ResolveAccountNames(string[] names, CancellationToken cancellationToken)
			=> this._lsaClient.LookupAccountNames(this._handle, names, cancellationToken);
		public Task<LsaAccountMapping> ResolveSidAsync(SecurityIdentifier sid, CancellationToken cancellationToken)
			=> this._lsaClient.LookupSid(this._handle, sid, cancellationToken);
		/// <summary>
		/// Resolves the domain and account name for one or more SIDs.
		/// </summary>
		/// <param name="sids">SIDs to resolve</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An array of <see cref="LsaAccountMapping"/> for each entry in <paramref name="sids"/></returns>
		/// <exception cref="LsaAccountMappingException">Some but not all of <paramref name="sids"/> could be resolved.</exception>
		public Task<LsaAccountMapping[]> ResolveSidsAsync(SecurityIdentifier[] sids, CancellationToken cancellationToken)
			=> this._lsaClient.LookupSids(this._handle, sids, cancellationToken);

		public Task<LsaSecret> OpenSecrets(string name, LsaSecretAccess access, CancellationToken cancellationToken)
			=> this._lsaClient.OpenSecret(this._handle, name, access, cancellationToken);

		public Task<SecurityIdentifier[]> GetAccounts(CancellationToken cancellationToken)
			=> this._lsaClient.EnumAccounts(this._handle, cancellationToken);

		public Task<LsaAccount> CreateAccount(SecurityIdentifier sid, CancellationToken cancellationToken)
			=> this._lsaClient.CreateAccount(this._handle, sid, cancellationToken);
		public Task<LsaAccount> OpenAccount(SecurityIdentifier sid, LsaAccountAccess access, CancellationToken cancellationToken)
			=> this._lsaClient.OpenAccount(this._handle, sid, access, cancellationToken);
		public Task<string> LookupPrivilege(long luid, CancellationToken cancellationToken)
			=> this._lsaClient.LookupPrivilege(this._handle, luid, cancellationToken);
		public Task<Privilege> LookupPrivilege(string name, CancellationToken cancellationToken)
			=> this._lsaClient.LookupPrivilege(this._handle, name, cancellationToken);

		public Task<UserRightInfo[]> GetUserRights(SecurityIdentifier sid, CancellationToken cancellationToken)
			=> this._lsaClient.GetAccountRights(this._handle, sid, cancellationToken);

		public Task<SecurityIdentifier[]> GetAccountsWithPrivilege(string privilege, CancellationToken cancellationToken)
			=> this._lsaClient.GetAccountsWithPrivilege(this._handle, privilege, cancellationToken);
	}
}