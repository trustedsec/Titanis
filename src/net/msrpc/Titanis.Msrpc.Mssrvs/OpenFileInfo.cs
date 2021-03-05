using ms_srvs;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mswkst
{
	public class OpenFileInfo
	{
		internal OpenFileInfo(ref readonly FILE_INFO_2 entry)
		{
			this.Id = (int)entry.fi2_id;
		}

		internal OpenFileInfo(ref readonly FILE_INFO_3 entry)
		{
			this.Id = (int)entry.fi3_id;
			this.Permissions = (Smb2FileAccessRights)entry.fi3_permissions;
			this.LockCount = (int)entry.fi3_num_locks;
			this.Path = entry.fi3_pathname?.value;
			this.UserName = entry.fi3_username?.value;
		}

		public int Id { get; }
		public Smb2FileAccessRights Permissions { get; }
		public int LockCount { get; }
		public string? Path { get; }
		public string? UserName { get; }
	}
}