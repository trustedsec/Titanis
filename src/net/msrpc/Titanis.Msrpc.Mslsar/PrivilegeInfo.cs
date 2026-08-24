using System;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mslsar
{
	[Flags]
	public enum PrivilegeAttributes : uint
	{
		None = 0,
		EnabledByDefault = 1,
		Enabled = 2,
	}

	public class PrivilegeInfo : IWantServerName
	{
		public PrivilegeInfo(SecurityIdentifier? accountSid, Privilege privilege, PrivilegeAttributes attributes)
		{
			this.AccountSid = accountSid;
			this.Privilege = privilege;
			this.Attributes = attributes;
		}
		public PrivilegeInfo(SecurityIdentifier? accountSid, Privilege privilege, string privilegeName, PrivilegeAttributes attributes)
		{
			this.AccountSid = accountSid;
			this.Privilege = privilege;
			this.Attributes = attributes;
			this.PrivilegeName = privilegeName;
		}

		public string? ServerName { get; set; }
		public SecurityIdentifier AccountSid { get; set; }
		public Privilege Privilege { get; }
		public PrivilegeAttributes Attributes { get; }
		public string? PrivilegeName { get; }
		public PrivilegeInfo WithPrivilegeName(string? privilegeName)
		{
			return new PrivilegeInfo(this.AccountSid, this.Privilege, privilegeName, this.Attributes)
			{
				ServerName = this.ServerName
			};
		}
	}
}
