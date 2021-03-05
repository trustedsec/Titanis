using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	public class DomainInfo
	{
		internal DomainInfo(string name, SecurityIdentifier sid)
		{
			Name = name;
			Sid = sid;
		}

		public string Name { get; set; }
		public SecurityIdentifier Sid { get; set; }
	}
}
