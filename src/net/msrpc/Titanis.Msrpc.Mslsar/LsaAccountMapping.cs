
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaAccountMapping
	{
		public string AccountName { get; set; }
		public LsaNameType NameType { get; internal set; }
		public string? DomainName { get; internal set; }
		public SecurityIdentifier? DomainSid { get; internal set; }
		public uint? AccountRid { get; internal set; }
		public SecurityIdentifier? AccountSid { get; internal set; }
	}
}