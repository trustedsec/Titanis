using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-DFSC] § 2.2.2
	class DfsGetReferralInfoRequest
	{
		public DfsGetReferralInfoRequest(ushort maxReferralLevel, string path)
		{
			MaxReferralLevel = maxReferralLevel;
			Path = path;
		}

		public ushort MaxReferralLevel { get; }
		public string Path { get; }

		internal void WriteTo(ByteWriter writer)
		{
			writer.WriteUInt16LE(this.MaxReferralLevel);
			writer.WriteStringUni(this.Path);
			writer.WriteUInt16LE(0);
		}
	}

	// [MS-DFSC] § 2.2.5
	abstract class DfsReferral
	{
		internal abstract void ReadFrom(ByteMemoryReader reader, int position);
		internal abstract DfsReferralEntry GetInfo();

		public ushort VersionNumber { get; set; }
		public ushort Size { get; set; }
	}

	// [MS-DFSC] § 2.2.5.1
	public enum DfsServerType : ushort
	{
		NonRoot = 0,
		Root = 1,
	}

	// [MS-DFSC] § 2.2.5.1
	class DfsReferralV1 : DfsReferral
	{
		public DfsServerType ServerType { get; set; }
		public ushort Flags { get; set; }
		public string ShareName { get; set; }

		internal override DfsReferralEntry GetInfo()
			=> new DfsReferralEntry(this.ServerType, UncPath.Parse(this.ShareName));

		internal override void ReadFrom(ByteMemoryReader reader, int position)
		{
			this.ServerType = (DfsServerType)reader.ReadUInt16LE();
			this.Flags = reader.ReadUInt16LE();

			this.ShareName = reader.ReadZStringUni();
		}
	}

	// [MS-DFSC] § 2.2.5.2
	class DfsReferralV2 : DfsReferral
	{
		public DfsServerType ServerType { get; set; }
		public ushort Flags { get; set; }
		public int Proximity { get; set; }
		public int TimeToLive { get; set; }
		public string DfsPath { get; set; }
		public string DfsAltPath { get; set; }
		public string NetworkAddress { get; set; }

		internal override DfsReferralEntry GetInfo()
			=> new DfsReferralEntry(this.ServerType, UncPath.Parse(this.NetworkAddress))
			{
				DfsPath = UncPath.Parse(this.DfsPath),
				DfsAltPath = UncPath.Parse(this.DfsAltPath),
			};

		internal override void ReadFrom(ByteMemoryReader reader, int position)
		{
			this.ServerType = (DfsServerType)reader.ReadUInt16LE();
			this.Flags = reader.ReadUInt16LE();
			this.Proximity = reader.ReadInt32LE();
			this.TimeToLive = reader.ReadInt32LE();

			int pathOffset = reader.ReadUInt16LE();
			int altPathOffset = reader.ReadUInt16LE();
			int netAddressOffset = reader.ReadUInt16LE();

			reader.Position = position + pathOffset;
			this.DfsPath = reader.ReadZStringUni();

			reader.Position = position + altPathOffset;
			this.DfsAltPath = reader.ReadZStringUni();

			reader.Position = position + netAddressOffset;
			this.NetworkAddress = reader.ReadZStringUni();
		}
	}

	// [MS-DFSC] § 2.2.5.3
	[Flags]
	enum DfsReferralEntryFlags : ushort
	{
		None = 0,
		NameListReferral = 2,
		TargetSetBoundary = 4,
	}

	// [MS-DFSC] § 2.2.5.3
	class DfsReferralV3 : DfsReferral
	{
		public DfsServerType ServerType { get; set; }
		public DfsReferralEntryFlags Flags { get; set; }
		public int Ttl { get; set; }
		public string DfsPath { get; set; }
		public string DfsAltPath { get; set; }
		public string NetworkAddress { get; set; }
		public Guid ServiceSiteGuid { get; set; }

		internal override DfsReferralEntry GetInfo()
			=> new DfsReferralEntry(this.ServerType, UncPath.Parse(this.NetworkAddress))
			{
				DfsPath = UncPath.Parse(this.DfsPath),
				DfsAltPath = UncPath.Parse(this.DfsAltPath),
				Ttl = this.Ttl,
				SiteServiceGuid = this.ServiceSiteGuid
			};

		internal override void ReadFrom(ByteMemoryReader reader, int position)
		{
			this.ServerType = (DfsServerType)reader.ReadUInt16LE();
			this.Flags = (DfsReferralEntryFlags)reader.ReadUInt16LE();
			this.Ttl = reader.ReadInt32LE();

			if (0 == (this.Flags & DfsReferralEntryFlags.NameListReferral))
			{
				int pathOffset = reader.ReadUInt16LE();
				int altPathOffset = reader.ReadUInt16LE();
				int netAddressOffset = reader.ReadUInt16LE();

				this.ServiceSiteGuid = reader.ReadGuid();

				reader.Position = position + pathOffset;
				this.DfsPath = reader.ReadZStringUni();

				reader.Position = position + altPathOffset;
				this.DfsAltPath = reader.ReadZStringUni();

				reader.Position = position + netAddressOffset;
				this.NetworkAddress = reader.ReadZStringUni();
			}
			else
			{
				int specialNameOffset = reader.ReadUInt16LE();
				int expandedNameCount = reader.ReadUInt16LE();
				int expandedNameOffset = reader.ReadUInt16LE();
				_ = reader.ReadUInt16LE();  // Padding


			}
		}
	}

	// [MS-DFSC] § 2.2.4
	class DfsGetReferralInfoResponse
	{
		public ushort PathConsumed { get; set; }
		public ushort NumberOfReferrals { get; set; }
		public DfsReferralFlags Flags { get; set; }
		public DfsReferral[] Referrals { get; set; }

		public DfsReferralEntry[] GetInfos()
		{
			return Array.ConvertAll(this.Referrals, r => r.GetInfo());
		}

		internal void ReadFrom(ByteMemoryReader reader)
		{
			this.PathConsumed = reader.ReadUInt16LE();
			this.NumberOfReferrals = reader.ReadUInt16LE();
			this.Flags = (DfsReferralFlags)reader.ReadInt32LE();

			DfsReferral[] referrals = new DfsReferral[this.NumberOfReferrals];
			for (int i = 0; i < referrals.Length; i++)
			{
				var offset = reader.Position;
				ushort version = reader.ReadUInt16LE();
				DfsReferral referral = version switch
				{
					1 => new DfsReferralV1(),
					2 => new DfsReferralV2(),
					3 or 4 => new DfsReferralV3(),
				};
				referrals[i] = referral;

				referral.VersionNumber = version;
				referral.Size = reader.ReadUInt16LE();
				referral.ReadFrom(reader, offset);

				reader.Position = offset + referral.Size;

			}
			this.Referrals = referrals;
		}
	}
}
