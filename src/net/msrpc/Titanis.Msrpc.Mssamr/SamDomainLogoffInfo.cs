using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamDomainLogoffInfo
	{
		private readonly DOMAIN_LOGOFF_INFORMATION info;

		internal SamDomainLogoffInfo(in DOMAIN_LOGOFF_INFORMATION info)
		{
			this.info = info;
		}

		public TimeSpan ForceLogOff => TimeSpan.FromTicks(-this.info.ForceLogoff.AsInt64());
	}
}