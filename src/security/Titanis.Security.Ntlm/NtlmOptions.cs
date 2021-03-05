using System;

namespace Titanis.Security.Ntlm
{
	[Flags]
	public enum NtlmOptions
	{
		None = 0,

		NoLMResponseNTLMv1 = (1 << 0),
		ClientRequire128bitEncryption = (1 << 1),
		[Obsolete("Use SecurityCapabilities instead.", true)]
		RequiresIntegrity = (1 << 2),
		[Obsolete("Use SecurityCapabilities instead.", true)]
		RequiresReplayDetect = (1 << 3),
		[Obsolete("Use SecurityCapabilities instead.", true)]
		RequiresSequenceDetect = (1 << 4),
		[Obsolete("Use SecurityCapabilities instead.", true)]
		RequiresConfidentiality = (1 << 5),
		SupportsDatagram = (1 << 6),
		IdentityOnly = (1 << 7),
		HasUnverifiedTargetName = (1 << 8),

		UseNtlmV2 = (1 << 9),

		[Obsolete("Use SecurityCapabilities instead.", true)]
		IntegrityMask =
			RequiresIntegrity
			| RequiresReplayDetect
			| RequiresSequenceDetect
	}

}
