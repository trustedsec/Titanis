namespace Titanis.DceRpc.WireProtocol
{
	public enum RpcFaultCode : uint
	{
		// [MS-RPCE] § 3.2.3.5.1 - Failure Semantics
		AccessDenied = 5,

		OpnumRange = 0x1C010002,
	}
}
