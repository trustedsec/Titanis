namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies the authentication level of an RPC channel.
	/// </summary>
	// [MS-RPCE] § 2.2.1.1.8 - Authentication Levels
	public enum RpcAuthLevel : sbyte
	{
		Default = 0,
		None = 1,
		Connect = 2,
		Call = 3,
		Packet = 4,
		PacketIntegrity = 5,
		PacketPrivacy = 6,

		// Titanis-specific
		// Uses the configured default value (if set), otherwise Default
		ConfiguredDefault = -1,
	}
}
