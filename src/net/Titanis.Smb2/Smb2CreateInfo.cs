

using System;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	public class Smb2CreateInfo
	{
		public Smb2CreateInfo()
		{
			this.body = new Pdus.Smb2CreateRequestBody
			{
				oplockLevel = Smb2OplockLevel.None,
				impLevel = Smb2ImpersonationLevel.Identification,
			};
		}
		internal Smb2CreateInfo(in Pdus.Smb2CreateRequestBody body)
		{
			this.body = body;
		}

		internal Pdus.Smb2CreateRequestBody body;

		public Smb2Priority Priority { get; set; }

		public Smb2OplockLevel OplockLevel
		{
			get => this.body.oplockLevel;
			set => this.body.oplockLevel = value;
		}

		public Smb2ImpersonationLevel ImpersonationLevel
		{
			get => this.body.impLevel;
			set => this.body.impLevel = value;
		}
		//public Smb2CreateFlags CreateFlags
		//{
		//	get => this.hdr.createFlags;
		//	set => this.hdr.createFlags = value;
		//}
		//public ulong reserved
		//{
		//	get => this.hdr.reserved;
		//	set => this.hdr.reserved = value;
		//}
		public uint DesiredAccess
		{
			get => this.body.desiredAccess;
			set => this.body.desiredAccess = value;
		}
		public Winterop.FileAttributes FileAttributes
		{
			get => this.body.fileAttributes;
			set => this.body.fileAttributes = value;
		}
		public Smb2ShareAccess ShareAccess
		{
			get => this.body.shareAccess;
			set => this.body.shareAccess = value;
		}
		public Smb2CreateDisposition CreateDisposition
		{
			get => this.body.createDisp;
			set => this.body.createDisp = value;
		}
		public Smb2FileCreateOptions CreateOptions
		{
			get => this.body.createOptions;
			set => this.body.createOptions = value;
		}

		public byte[] SecurityDescriptor { get; set; }
		public byte[] ExtendedAttributes { get; set; }

		public bool RequestDurableHandle { get; set; }
		public bool RequestMaximalAccess { get; set; }
		public bool QueryOnDiskId { get; set; }
		/// <summary>
		/// Lease info is automatically populated when requested by <see cref="OplockLevel"/>
		/// </summary>
		internal Smb2LeaseInfo? LeaseInfo { get; set; }

		public long? AllocationSize { get; set; }
		public DateTime? TimeWarpToken { get; set; }

		public Smb2FileHandle? ReconnectDurableHandle { get; set; }
	}

	public class Smb2LeaseInfo
	{
        internal bool UseV2Struct { get; set; }
        public Guid LeaseKey { get; set; } = Guid.NewGuid();
		public Guid ParentLeaseKey { get; set; }
		public Smb2LeaseState LeaseState { get; set; }
	}
}
