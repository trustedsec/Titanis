namespace Titanis.DceRpc.WireProtocol
{
	enum ProviderReason : short
	{
		ReasonNotSpecified = 0,
		AbstractSyntaxNotSupported,
		ProposedTransferSyntaxesNotSupported,
		LocalLimitExceeded
	}
}
