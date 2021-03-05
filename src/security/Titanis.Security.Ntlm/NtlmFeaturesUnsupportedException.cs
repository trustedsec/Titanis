using System;

namespace Titanis.Security.Ntlm
{
	internal class NtlmFeaturesUnsupportedException : Exception
	{
		private NegotiateFlags badAnonFlags;

		public NtlmFeaturesUnsupportedException()
		{
		}

		public NtlmFeaturesUnsupportedException(string message) : base(message)
		{
		}

		public NtlmFeaturesUnsupportedException(NegotiateFlags badAnonFlags, string message)
			:base(message)
		{
			this.badAnonFlags = badAnonFlags;
		}

		public NtlmFeaturesUnsupportedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}