using ms_srvs;

namespace Titanis.Msrpc.Mswkst
{
	public class ConnectionInfo
	{

		internal ConnectionInfo(CONNECTION_INFO_1 entry)
		{
			this.Id = (int)entry.coni1_id;
			this.Type = (ShareType)entry.coni1_type;
			this.NumberOfOpens = (int)entry.coni1_num_opens;
			this.NumberOfUsers = (int)entry.coni1_num_users;
			this.ConnectedTimeSeconds = (int)entry.coni1_time;
			this.UserName = entry.coni1_username?.value;
			this.ShareName = entry.coni1_netname?.value;
		}

		public int Id { get; }
		public ShareType Type { get; }
		public int NumberOfOpens { get; }
		public int NumberOfUsers { get; }
		public int ConnectedTimeSeconds { get; }
		public string UserName { get; }
		public string ShareName { get; }
	}
}