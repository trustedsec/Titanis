using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	public sealed class SamUser : SamObject
	{
		public SamUser(SamClient samClient, RpcContextHandle handle)
			: base(samClient, handle)
		{
		}

		public Task<SamUserGeneralInfo> QueryGeneralInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserGeneralInfo(this._handle, cancellationToken);
		public Task<SamUserPreferencesInfo> QueryPreferencesInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserPreferencesInfo(this._handle, cancellationToken);
		public Task<SamUserLogonInfo> QueryLogonInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserLogonInfo(this._handle, cancellationToken);
		public Task<SamUserAccountInfo> QueryAccountInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserAccountInfo(this._handle, cancellationToken);
		public Task<string> QueryAccountName(CancellationToken cancellationToken)
			=> this._samClient.QueryUserAccountNameInfo(this._handle, cancellationToken);
		public Task<string> QueryFullName(CancellationToken cancellationToken)
			=> this._samClient.QueryUserFullNameInfo(this._handle, cancellationToken);
		public Task<uint> QueryPrimaryGroup(CancellationToken cancellationToken)
			=> this._samClient.QueryUserPrimaryGroup(this._handle, cancellationToken);
		public Task<string> QueryScript(CancellationToken cancellationToken)
			=> this._samClient.QueryUserScriptInfo(this._handle, cancellationToken);
		public Task<string> QueryUserProfile(CancellationToken cancellationToken)
			=> this._samClient.QueryUserProfileInfo(this._handle, cancellationToken);
		public Task<string> QueryAdminComment(CancellationToken cancellationToken)
			=> this._samClient.QueryUserAdminComment(this._handle, cancellationToken);
		public Task<string> QueryWorkstations(CancellationToken cancellationToken)
			=> this._samClient.QueryUserWorkstations(this._handle, cancellationToken);
		public Task<SamUserAccountFlags> QueryUserControlInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserControlInfo(this._handle, cancellationToken);
		public Task<SamUserAllInfo> QueryAllInfo(CancellationToken cancellationToken)
			=> this._samClient.QueryUserAllInfo(this._handle, cancellationToken);

		public Task SetPassword(string password, CancellationToken cancellationToken)
			=> this._samClient.SetUserPassword(this._handle, this._samClient.EncryptPassword(password), cancellationToken);
		public Task SetPassword(byte[] encrypted, CancellationToken cancellationToken)
			=> this._samClient.SetUserPassword(this._handle, encrypted, cancellationToken);
		public Task SetControlFlags(SamUserAccountFlags flags, CancellationToken cancellationToken)
			=> this._samClient.SetUserControlFlags(this._handle, flags, cancellationToken);


		public async Task<SamUserAccountFlags> EnableAccountFlags(SamUserAccountFlags flagsToEnable, CancellationToken cancellationToken)
		{
			var accountFlags = await this.QueryUserControlInfo(cancellationToken).ConfigureAwait(false);
			accountFlags |= flagsToEnable;
			await this.SetControlFlags(accountFlags, cancellationToken).ConfigureAwait(false);

			return accountFlags;
		}

		public async Task<SamUserAccountFlags> DisableAccountFlags(SamUserAccountFlags flagsToDisable, CancellationToken cancellationToken)
		{
			var accountFlags = await this.QueryUserControlInfo(cancellationToken).ConfigureAwait(false);
			accountFlags &= ~flagsToDisable;
			await this.SetControlFlags(accountFlags, cancellationToken).ConfigureAwait(false);

			return accountFlags;
		}

		public Task EnableAccount(CancellationToken cancellationToken)
			=> this.DisableAccountFlags(SamUserAccountFlags.Disabled, cancellationToken);
		public Task DisableAccount(CancellationToken cancellationToken)
			=> this.EnableAccountFlags(SamUserAccountFlags.Disabled, cancellationToken);
		public Task UnlockAccount(CancellationToken cancellationToken)
			=> this.DisableAccountFlags(SamUserAccountFlags.AutoLocked, cancellationToken);

		public Task Delete(CancellationToken cancellationToken)
			=> this._samClient.DeleteUser(this._handle, cancellationToken);
	}
}