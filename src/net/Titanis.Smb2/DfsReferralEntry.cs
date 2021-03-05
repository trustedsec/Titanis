using System;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	// [MS-DFSC] § 2.2.4
	/// <summary>
	/// Specifies the type of DFS server.
	/// </summary>
	[Flags]
	public enum DfsReferralFlags
	{
		None = 0,

		ReferralServers = 1,
		StorageServers = 2,
		TargetFailback = 4,
	}

	/// <summary>
	/// Provides referral information for a DFS share.
	/// </summary>
	/// <seealso cref="Smb2TreeConnect.QueryDfsReferrals(UncPath, System.Threading.CancellationToken)"/>
	public class DfsReferral
	{
		internal DfsReferral(DfsReferralFlags flags, int pathConsumed, DfsReferralEntry[] entries)
		{
			this.Flags = flags;
			this.PathConsumed = pathConsumed;
			this.Entries = entries;
		}

		public DfsReferralFlags Flags { get; }
		public int PathConsumed { get; }
		public DfsReferralEntry[] Entries { get; }
	}

	/// <summary>
	/// Provides referral information for a DFS share.
	/// </summary>
	/// <seealso cref="Smb2TreeConnect.QueryDfsReferrals(UncPath, System.Threading.CancellationToken)"/>
	public class DfsReferralEntry
	{
		public DfsReferralEntry(DfsServerType serverType, UncPath target)
		{
			this.ServerType = serverType;
			this.DfsTarget = target;
		}

		public DfsServerType ServerType { get; }
		public UncPath DfsTarget { get; }
		public UncPath? DfsPath { get; internal set; }
		public UncPath? DfsAltPath { get; internal set; }
		public int Ttl { get; internal set; }
		public Guid SiteServiceGuid { get; internal set; }
	}
}
