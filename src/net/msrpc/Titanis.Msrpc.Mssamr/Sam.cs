using ms_samr;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	/// <summary>
	/// Represents the Security Account Manager database.
	/// </summary>
	/// <seealso cref="SamClient.Connect(SamServerAccessRights, string, CancellationToken)"/>
	public sealed class Sam : SamObject
	{
		internal Sam(SamClient samClient, RpcContextHandle pHandle)
			: base(samClient, pHandle)
		{
		}

		/// <summary>
		/// List of characters that may not appear in account names
		/// </summary>
		public const string InvalidAccountChars = @"""/\[]:|<>+=;?,*";

		/// <summary>
		/// Opens a domain by name.
		/// </summary>
		/// <param name="name">Name of domain</param>
		/// <param name="access">Required access</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="SamDomain"/> representing the domain</returns>
		/// <remarks>
		/// The domain name is first resolved to a SID.
		/// </remarks>
		public async Task<SamDomain> OpenDomainAsync(string name, SamDomainAccessRights access, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(name);

			var domainSid = (await this._samClient.LookupDomain(this._handle, name, cancellationToken).ConfigureAwait(false)).value;
			return await this.OpenDomainInternal(domainSid.ToSid(), name, access, cancellationToken).ConfigureAwait(false);
		}

		public async Task<SecurityIdentifier> LookupDomain(string name, CancellationToken cancellationToken)
		{
			var domainSid = (await this._samClient.LookupDomain(this._handle, name, cancellationToken).ConfigureAwait(false)).value;
			return domainSid.ToSid();
		}

		public async Task<SamDomain> OpenDomainAsync(SecurityIdentifier domainSid, SamDomainAccessRights access, CancellationToken cancellationToken)
		{
			return await OpenDomainInternal(domainSid, null, access, cancellationToken).ConfigureAwait(false);
		}

		private async Task<SamDomain> OpenDomainInternal(SecurityIdentifier domainSid, string? name, SamDomainAccessRights access, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(domainSid);

			var hDomain = await this._samClient.OpenDomain(this._handle, domainSid.ToRpcSid(), access, cancellationToken).ConfigureAwait(false);

			return new SamDomain(this._samClient, hDomain, domainSid, name);
		}

		public Task<List<SamEntry>> GetDomains(CancellationToken cancellationToken)
			=> this._samClient.EnumDomains(this._handle, cancellationToken);
	}
}