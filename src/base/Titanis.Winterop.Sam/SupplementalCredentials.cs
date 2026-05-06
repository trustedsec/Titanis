
namespace Titanis.Winterop.SamServer
{
	public class SupplementalCredentials
	{
		public byte[][] WDigestHashes { get; internal set; }
		public KerberosKeyInfo[]? KerberosKeys { get; internal set; }
		public KerberosKeyInfo[]? KerberosOldKeys { get; internal set; }
		public string CleartextPassword { get; internal set; }
		public byte[] NtlmStrongNtowf { get; internal set; }
		public byte[] KerberosSalt { get; internal set; }
	}
}