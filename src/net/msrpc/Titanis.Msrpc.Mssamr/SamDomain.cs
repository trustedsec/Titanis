using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	/// <summary>
	/// Represents a domain.
	/// </summary>
	/// <seealso cref="Sam.OpenDomainAsync(string, SamDomainAccessRights, CancellationToken)"/>
	public sealed class SamDomain : SamObject
	{
		internal SamDomain(SamClient samClient, RpcContextHandle handle, SecurityIdentifier sid, string? name)
			: base(samClient, handle)
		{
			Sid = sid;
			this.DomainName = name;
		}

		public SecurityIdentifier Sid { get; }
		public string? DomainName { get; private set; }

		/// <summary>
		/// Looks up an entry in the domain by name.
		/// </summary>
		/// <param name="name">Name of entry</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The <see cref="SamEntry"/> named by <paramref name="name"/></returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public async Task<SamEntry> LookupNameAsync(string name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(name))
				throw new System.ArgumentNullException(nameof(name));

			return (await this.LookupNamesAsync(new string[] { name }, cancellationToken).ConfigureAwait(false))[0];
		}

		public Task<SamEntry[]> LookupNamesAsync(string[] names, CancellationToken cancellationToken)
			=> this._samClient.LookupNames(this._handle, names, cancellationToken);

		public async Task<SamEntry> LookupIdAsync(uint id, CancellationToken cancellationToken)
			=> (await this.LookupIDsAsync(new uint[] { id }, cancellationToken).ConfigureAwait(false))[0];
		public Task<SamEntry[]> LookupIDsAsync(uint[] ids, CancellationToken cancellationToken)
			=> this._samClient.LookupIDs(this._handle, ids, cancellationToken);

		public Task<List<SamEntry>> EnumGroups(CancellationToken cancellationToken)
			=> this._samClient.EnumGroupsInDomains(this._handle, cancellationToken);
		public Task<List<SamEntry>> EnumAliases(CancellationToken cancellationToken)
			=> this._samClient.EnumAliasesInDomains(this._handle, cancellationToken);
		public Task<List<SamEntry>> EnumUsers(CancellationToken cancellationToken)
			=> this._samClient.EnumUsersInDomains(this._handle, cancellationToken);

		public Task<SamGroup> OpenGroupAsync(uint groupId, SamGroupAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.OpenGroup(this._handle, groupId, access, this.Sid, cancellationToken);
		public Task<SamAlias> OpenAliasAsync(uint aliasId, SamAliasAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.OpenAlias(this._handle, aliasId, access, this.Sid, cancellationToken);
		public Task<SamUser> OpenUserAsync(uint userId, SamUserAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.OpenUser(this._handle, userId, access, this.Sid, cancellationToken);

		public Task<SamGroup> CreateGroup(string name, SamGroupAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.CreateGroup(this._handle, name, access, this.Sid, cancellationToken);
		public Task<SamAlias> CreateAlias(string name, SamAliasAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.CreateAlias(this._handle, name, access, this.Sid, cancellationToken);
		public Task<SamUser> CreateUser(string name, SamUserAccountFlags accountType, SamUserAccessRights access, CancellationToken cancellationToken)
			=> this._samClient.CreateUser(this._handle, name, accountType, access, this.Sid, cancellationToken);

		public Task<SamDomainGeneralInfo> QueryGeneralInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainGeneralInfo(this._handle, cancellationToken);
		public Task<SamDomainGeneralInfo2> QueryGeneralInfo2(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainGeneralInfo2(this._handle, cancellationToken);
		public Task<SamDomainPasswordInfo> QueryPasswordInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainPasswordInfo(this._handle, cancellationToken);
		public Task<SamDomainLogoffInfo> QueryLogoffInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainLogoffInfo(this._handle, cancellationToken);
		public async Task<string> QueryDomainName(CancellationToken cancellationToken)
		{
			var name = await _samClient.QueryDomainNameInfo(_handle, cancellationToken).ConfigureAwait(false);
			this.DomainName = name;
			return name;
		}

		public Task<string> QueryReplicaName(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainReplicaInfo(this._handle, cancellationToken);
		public Task<DomainServerRole> QueryServerRole(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainServerRole(this._handle, cancellationToken);
		public Task<DomainServerEnableState> QueryServerState(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainServerEnabledState(this._handle, cancellationToken);
		public Task<string> QueryOemInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainOemInfo(this._handle, cancellationToken);
		public Task<SamDomainModifiedInfo> QueryModifiedInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainModifiedInfo(this._handle, cancellationToken);
		public Task<SamDomainModifiedInfo2> QueryModifiedInfo2(CancellationToken cancellationToken)
			=> this._samClient.QueryDomainModifiedInfo2(this._handle, cancellationToken);
	}
}