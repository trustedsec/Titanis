namespace Titanis.DceRpc.WireProtocol
{
	public enum BindRejectReason : ushort
	{
		// [C706]
		Unspecified = 0,
		TemporaryCongestion = 1,
		LocalLimitExceeded = 2,
		CalledPaddrUnknown = 3,
		ProtocolVersionNotSupported = 4,
		DefaultContextNotSupported = 5,
		UserdataNotReadable = 6,
		NoPsapAvailable = 7,

		// [MS-RPCE] § 2.2.2.5 - New Reasons for Bind Rejection
		AuthenticationTypeNotRecognized = 8,
		InvalidChecksum = 9,
	}
}
