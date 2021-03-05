using ms_samr;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mssamr
{
	public sealed class Sam : SamObject
	{
		internal Sam(SamClient samClient, RpcContextHandle pHandle)
			: base(samClient, pHandle)
		{
		}

		public async Task<SamDomain> OpenDomainAsync(string name, SamDomainAccess access, CancellationToken cancellationToken)
		{
			var domainSid = (await this._samClient.LookupDomain(this._handle, name, cancellationToken).ConfigureAwait(false)).value;
			return await this._samClient.OpenDomain(this._handle, domainSid, access, cancellationToken).ConfigureAwait(false);
		}

		public Task<List<SamEntry>> GetDomains(CancellationToken cancellationToken)
			=> this._samClient.EnumDomains(this._handle, cancellationToken);
	}
}