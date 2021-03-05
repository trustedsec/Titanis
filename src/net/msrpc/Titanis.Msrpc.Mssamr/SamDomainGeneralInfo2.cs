using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamDomainGeneralInfo2
	{
		private readonly SAMPR_DOMAIN_GENERAL_INFORMATION2 info;

		internal SamDomainGeneralInfo2(in SAMPR_DOMAIN_GENERAL_INFORMATION2 info)
		{
			this.info = info;
		}

		public TimeSpan LockoutDuration => TimeSpan.FromTicks(-this.info.LockoutDuration.QuadPart);
		public TimeSpan LockoutObservationWindow => TimeSpan.FromTicks(-this.info.LockoutObservationWindow.QuadPart);
		public ushort LockoutThreshold => this.info.LockoutThreshold;
	}
}