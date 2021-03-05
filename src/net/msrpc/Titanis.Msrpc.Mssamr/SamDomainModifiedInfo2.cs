using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamDomainModifiedInfo2
	{
		private DOMAIN_MODIFIED_INFORMATION2 info;

		internal SamDomainModifiedInfo2(DOMAIN_MODIFIED_INFORMATION2 info)
		{
			this.info = info;
		}

		public long Usn => this.info.DomainModifiedCount.AsInt64();
		public DateTime CreationTime => DateTime.FromFileTime(this.info.CreationTime.AsInt64());
		public long LastPromotionUsn => this.info.ModifiedCountAtLastPromotion.AsInt64();
	}
}