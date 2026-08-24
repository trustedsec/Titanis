using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	public class UserRightInfo : IWantServerName
	{
		public UserRightInfo(SecurityIdentifier? accountSid, string name)
		{
			this.AccountSid = accountSid;
			this.Name = name;
		}

		public string? ServerName { get; set; }
		public SecurityIdentifier? AccountSid { get; }
		public string? Name { get; set; }
	}
}
