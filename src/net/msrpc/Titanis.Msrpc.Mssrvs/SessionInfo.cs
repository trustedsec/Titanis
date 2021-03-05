using ms_srvs;
using System;

namespace Titanis.Msrpc.Mswkst
{
	[Flags]
	public enum SessionUserFlags
	{
		None = 0,

		Guest = 1,
		Unencrypted = 2
	}

	public class SessionInfo
	{
		internal SessionInfo(ref readonly SESSION_INFO_0 entry)
		{
			this.ClientName = entry.sesi0_cname?.value;
		}

		internal SessionInfo(ref readonly SESSION_INFO_1 entry)
		{
			this.ClientName = entry.sesi1_cname?.value;
			this.UserName = entry.sesi1_username?.value;
			this.NumberOfOpens = entry.sesi1_num_opens;
			this.ConnectedTime = (int)entry.sesi1_time;
			this.IdleTime = (int)entry.sesi1_idle_time;
			this.UserFlags = (SessionUserFlags)entry.sesi1_user_flags;
		}

		internal SessionInfo(ref readonly SESSION_INFO_2 entry)
		{
			this.ClientName = entry.sesi2_cname?.value;
			this.UserName = entry.sesi2_username?.value;
			this.NumberOfOpens = entry.sesi2_num_opens;
			this.ConnectedTime = (int)entry.sesi2_time;
			this.IdleTime = (int)entry.sesi2_idle_time;
			this.UserFlags = (SessionUserFlags)entry.sesi2_user_flags;
			this.ClientType = entry.sesi2_cltype_name?.value;
		}

		internal SessionInfo(ref readonly SESSION_INFO_10 entry)
		{
			this.ClientName = entry.sesi10_cname?.value;
			this.UserName = entry.sesi10_username?.value;
			this.ConnectedTime = (int)entry.sesi10_time;
			this.IdleTime = (int)entry.sesi10_idle_time;
		}

		internal SessionInfo(ref readonly SESSION_INFO_502 entry)
		{
			this.ClientName = entry.sesi502_cname?.value;
			this.UserName = entry.sesi502_username?.value;
			this.NumberOfOpens = entry.sesi502_num_opens;
			this.ConnectedTime = (int)entry.sesi502_time;
			this.IdleTime = (int)entry.sesi502_idle_time;
			this.UserFlags = (SessionUserFlags)entry.sesi502_user_flags;
			this.ClientType = entry.sesi502_cltype_name?.value;
			this.Transport = entry.sesi502_transport?.value;
		}

		public string? ClientName { get; }
		public string? UserName { get; }
		public uint NumberOfOpens { get; }
		public int ConnectedTime { get; }
		public int IdleTime { get; }
		public SessionUserFlags UserFlags { get; }
		public string? ClientType { get; }
		public string? Transport { get; }
	}
}