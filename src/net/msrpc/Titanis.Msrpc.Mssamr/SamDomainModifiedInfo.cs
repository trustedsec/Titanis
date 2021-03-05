using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamDomainModifiedInfo
	{
		private DOMAIN_MODIFIED_INFORMATION info;

		internal SamDomainModifiedInfo(DOMAIN_MODIFIED_INFORMATION info)
		{
			this.info = info;
		}

		public long Usn => this.info.DomainModifiedCount.AsInt64();
		public DateTime CreationTime => DateTime.FromFileTime(this.info.CreationTime.AsInt64());
	}
}