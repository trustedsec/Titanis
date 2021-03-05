using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public enum DomainServerRole
	{
		Backup = 2,
		Primary = 3,
	}

	public enum DomainServerEnableState
	{
		Enabled = 1,
		Disabled
	}

	public class SamDomainGeneralInfo
	{
		private readonly SAMPR_DOMAIN_GENERAL_INFORMATION info;

		internal SamDomainGeneralInfo(in SAMPR_DOMAIN_GENERAL_INFORMATION info)
		{
			this.info = info;
		}

		public TimeSpan ForceLogOff => TimeSpan.FromTicks(-this.info.ForceLogoff.AsInt64());
		public string OemInfo => this.info.OemInformation.AsString();
		public string DomainName => this.info.DomainName.AsString();
		public string ReplicaSourceNodeName => this.info.ReplicaSourceNodeName.AsString();
		public long Usn => this.info.DomainModifiedCount.AsInt64();
		public DomainServerRole ServerRole => (DomainServerRole)this.info.DomainServerRole;
		public DomainServerEnableState ServerEnableState => (DomainServerEnableState)this.info.DomainServerState;
		public bool UasCompatibility => 0 != this.info.UasCompatibilityRequired;
		public long UserCount => this.info.UserCount;
		public long GroupCount => this.info.GroupCount;
		public long AliasCount => this.info.AliasCount;
	}
}