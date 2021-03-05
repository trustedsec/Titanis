using ms_srvs;
using System;
using System.ComponentModel;
using System.Security.AccessControl;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mswkst
{
	[Flags]
	public enum ShareFlags
	{
		None = 0,
		Dfs = 1,
		DfsRoot = 2,
		RestrictExclusiveOpens = 0x100,
		ForceSharedDelete = 0x200,
		AllowNamespaceCaching = 0x400,
		AccessBasedDirectoryEnum = 0x800,
		ForceLevel2Oplock = 0x1000,
		EnableHash = 0x2000,
		EnableContinuousAvailability = 0x4000,
		EncryptData = 0x8000,
		CompressData = 0x00100000,
	}

	public class ShareInfo
	{
		[DisplayName("Name")]
		public string? ShareName { get; set; }
		[DisplayName("Server")]
		public string? ServerName { get; set; }
		[DisplayName("Type")]
		public ShareType ShareType { get; set; }
		public string? Remark { get; set; }
		public SharePermissions Permissions { get; set; }
		[DisplayName("Max. Uses")]
		[DisplayAlignment(DisplayAlignment.Right)]
		public int MaxUses { get; set; }
		[DisplayName("Current Uses")]
		[DisplayAlignment(DisplayAlignment.Right)]
		public int CurrentUses { get; set; }
		public string? Path { get; set; }
		[Browsable(false)]
		public string? Password { get; set; }
		[DisplayName("Sec. Desc.")]
		public string? SecurityDescriptorSddl { get; }
		public ShareFlags Flags { get; set; }

		public ShareInfo()
		{

		}

		internal ShareInfo(ref readonly SHARE_INFO_1 info)
		{
			this.ShareName = info.shi1_netname?.value;
			this.ShareType = (ShareType)info.shi1_type;
			this.Remark = info.shi1_remark?.value;
		}

		internal ShareInfo(ref readonly SHARE_INFO_2 info)
		{
			this.ShareName = info.shi2_netname?.value;
			this.ShareType = (ShareType)info.shi2_type;
			this.Remark = info.shi2_remark?.value;
			this.Permissions = (SharePermissions)info.shi2_permissions;
			this.MaxUses = (int)info.shi2_max_uses;
			this.CurrentUses = (int)info.shi2_current_uses;
			this.Path = info.shi2_path?.value;
			this.Password = info.shi2_passwd?.value;
		}

		internal ShareInfo(ref readonly SHARE_INFO_501 info)
		{
			this.ShareName = info.shi501_netname?.value;
			this.ShareType = (ShareType)info.shi501_type;
			this.Remark = info.shi501_remark?.value;
			this.Flags = (ShareFlags)info.shi501_flags;
		}

		internal ShareInfo(ref readonly SHARE_INFO_502_I info)
		{
			this.ShareName = info.shi502_netname?.value;
			this.ShareType = (ShareType)info.shi502_type;
			this.Remark = info.shi502_remark?.value;
			this.Permissions = (SharePermissions)info.shi502_permissions;
			this.MaxUses = (int)info.shi502_max_uses;
			this.CurrentUses = (int)info.shi502_current_uses;
			this.Path = info.shi502_path?.value;
			this.Password = info.shi502_passwd?.value;

			this.SecurityDescriptorSddl = SecurityHelpers.ConvertSdBytesToSddl(info.shi502_security_descriptor?.value);
		}

		internal ShareInfo(ref readonly SHARE_INFO_503_I info)
		{
			this.ShareName = info.shi503_netname?.value;
			this.ShareType = (ShareType)info.shi503_type;
			this.Remark = info.shi503_remark?.value;
			this.Permissions = (SharePermissions)info.shi503_permissions;
			this.MaxUses = (int)info.shi503_max_uses;
			this.CurrentUses = (int)info.shi503_current_uses;
			this.Path = info.shi503_path?.value;
			this.Password = info.shi503_passwd?.value;
			this.ServerName = info.shi503_servername?.value;

			this.SecurityDescriptorSddl = SecurityHelpers.ConvertSdBytesToSddl(info.shi503_security_descriptor?.value);
		}

		internal ShareInfo(ref readonly SHARE_INFO_1004 info)
		{
			this.Remark = info.shi1004_remark?.value;
		}

		internal ShareInfo(ref readonly SHARE_INFO_1005 info)
		{
			this.Flags = (ShareFlags)info.shi1005_flags;
		}

		internal ShareInfo(ref readonly SHARE_INFO_1006 info)
		{
			this.MaxUses = (int)info.shi1006_max_uses;
		}
	}

	class SecurityHelpers
	{
		public static string? ConvertSdBytesToSddl(byte[]? bytes)
		{
			if (bytes != null)
			{
				SecurityDescriptor sd = new SecurityDescriptor(bytes);
				return sd.ToSddlString(SecurityDescriptorSections.All);
			}
			else
				return null;
		}
	}
}