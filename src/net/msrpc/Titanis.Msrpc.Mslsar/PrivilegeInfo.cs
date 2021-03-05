using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mslsar
{
	[Flags]
	public enum PrivilegeAttributes : uint
	{
		None = 0,
		EnabledByDefault = 1,
		Enabled = 2,
	}

	public class PrivilegeInfo
	{
		public PrivilegeInfo(Privilege privilege, PrivilegeAttributes attributes)
		{
			this.Privilege = privilege;
			this.Attributes = attributes;
		}
		public PrivilegeInfo(Privilege privilege, string privilegeName, PrivilegeAttributes attributes)
		{
			this.Privilege = privilege;
			this.Attributes = attributes;
			this.PrivilegeName = privilegeName;
		}
		public Privilege Privilege { get; }
		public PrivilegeAttributes Attributes { get; }
		public string? PrivilegeName { get; }
		public PrivilegeInfo WithPrivilegeName(string? privilegeName)
		{
			return new PrivilegeInfo(this.Privilege, privilegeName, this.Attributes);
		}
	}
}
