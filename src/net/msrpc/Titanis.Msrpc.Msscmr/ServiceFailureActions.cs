using System;

namespace Titanis.Msrpc.Msscmr
{
	public class ServiceFailureActions
	{
		public TimeSpan ResetPeriod { get; set; }
		public string RebootMessage { get; set; }
		public ServiceFailureAction[] Actions { get; set; }
	}
}