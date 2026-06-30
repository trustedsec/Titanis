

using System;
using System.IO;
using System.Runtime.CompilerServices;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies parameters for creating a file over SMB.
	/// </summary>
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

		public byte[]? SecurityDescriptor { get; set; }
		public byte[]? ExtendedAttributes { get; set; }

		public bool RequestDurableHandle { get; set; }
		public bool RequestMaximalAccess { get; set; }
		public bool QueryOnDiskId { get; set; }
		/// <remarks>
		/// Lease info is set when this parameter is passed to one of the <see cref="Smb2TreeConnect"/>
		/// methods if <see cref="OplockLevel"/> is set to <see cref="Smb2OplockLevel.Lease"/>.
		/// </remarks>
		internal Smb2LeaseInfo? LeaseInfo { get; set; }

		public long? AllocationSize { get; set; }
		public DateTime? TimeWarpToken { get; set; }

		public Smb2FileHandle? ReconnectDurableHandle { get; set; }

		public static Smb2CreateInfo Create(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = 0,
			Smb2FileAccessRights desiredAccess = 0,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.None,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.None,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.None,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = false,
			bool queryOnDiskId = false,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForCreateDirectory(
			Smb2Priority priority = Smb2Priority.CreateDir,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultCreateDirAccess,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.Normal,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.ReadWrite,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.CreateNew,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.SynchronousIoNonalert,
			Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = false,
			bool queryOnDiskId = false,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions | extraOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForOpenDirectory(
			Smb2Priority priority = Smb2Priority.OpenDir,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultOpenDirAccess,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.None,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.DefaultDirShare,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.OpenExisting,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert,
			Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = true,
			bool queryOnDiskId = true,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions | extraOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForRemoveDirectory(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultRemoveDirAccess,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.None,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.DefaultDirShare,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.OpenExisting,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = true,
			bool queryOnDiskId = true,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForDeleteFile(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultDeleteFileAccess,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.None,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.Delete,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.OpenExisting,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = true,
			bool queryOnDiskId = true,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForCreateFile(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultCreateAccess,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.Normal,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.ReadWrite,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.Supersede,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert,
			Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = false,
			bool queryOnDiskId = false,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions | extraOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		public static Smb2CreateInfo ForOpenFileRead(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = Smb2FileAccessRights.DefaultOpenReadAccess,
			// TODO: Normal or none?
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.Normal,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.Read,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.OpenExisting,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert,
			Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = false,
			bool queryOnDiskId = false,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions | extraOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};

		#region FileStream-style
		public static Smb2CreateInfo ForCreateOrOpenFile(FileMode mode, FileAccess access, FileShare share, Smb2FileCreateOptions extraOptions = Smb2FileCreateOptions.None) => ForCreateFile(
			createDisposition: mode switch
			{
				FileMode.Append => Smb2CreateDisposition.OpenOrCreate,
				FileMode.Open => Smb2CreateDisposition.OpenExisting,
				FileMode.CreateNew => Smb2CreateDisposition.CreateNew,
				FileMode.Create => Smb2CreateDisposition.OverwriteOrCreate,
				FileMode.OpenOrCreate => Smb2CreateDisposition.OpenOrCreate,

				FileMode.Truncate => throw new NotImplementedException(),

			},
			desiredAccess: access switch
			{
				FileAccess.Read => Smb2FileAccessRights.DefaultOpenReadAccess,
				FileAccess.Write => Smb2FileAccessRights.DefaultOpenWriteAccess,
				FileAccess.ReadWrite => Smb2FileAccessRights.DefaultOpenReadWriteAccess,
			},
			shareAccess: (Smb2ShareAccess)share,
			extraOptions: extraOptions
			);
		#endregion

		public static Smb2CreateInfo ForOpenPipe(
			Smb2Priority priority = 0,
			Smb2OplockLevel oplockLevel = Smb2OplockLevel.None,
			Smb2ImpersonationLevel impersonationLevel = Smb2ImpersonationLevel.Impersonation,
			Smb2FileAccessRights desiredAccess = (Smb2FileAccessRights)0x0012019f,
			Winterop.FileAttributes fileAttributes = Winterop.FileAttributes.None,
			Smb2ShareAccess shareAccess = Smb2ShareAccess.ReadWriteDelete,
			Smb2CreateDisposition createDisposition = Smb2CreateDisposition.OpenExisting,
			Smb2FileCreateOptions createOptions = Smb2FileCreateOptions.None,
			bool requestDurableHandle = false,
			bool requestMaximalAccess = false,
			bool queryOnDiskId = false,
			byte[]? securityDescriptor = null,
			byte[]? extendedAttributes = null,
			Smb2LeaseInfo? leaseInfo = null,
			long? allocationSize = null,
			DateTime? timeWarpToken = null,
			Smb2FileHandle? reconnectDurableHandle = null
			) =>
			new Smb2CreateInfo
			{
				Priority = priority,
				OplockLevel = oplockLevel,
				ImpersonationLevel = impersonationLevel,
				DesiredAccess = (uint)desiredAccess,
				FileAttributes = fileAttributes,
				ShareAccess = shareAccess,
				CreateDisposition = createDisposition,
				CreateOptions = createOptions,
				RequestDurableHandle = requestDurableHandle,
				RequestMaximalAccess = requestMaximalAccess,
				QueryOnDiskId = queryOnDiskId,
				SecurityDescriptor = securityDescriptor,
				ExtendedAttributes = extendedAttributes,
				LeaseInfo = leaseInfo,
				AllocationSize = allocationSize,
				TimeWarpToken = timeWarpToken,
				ReconnectDurableHandle = reconnectDurableHandle
			};
	}

	public class Smb2LeaseInfo
	{
		internal bool UseV2Struct { get; set; }
		public Guid LeaseKey { get; set; } = Guid.NewGuid();
		public Guid ParentLeaseKey { get; set; }
		public Smb2LeaseState LeaseState { get; set; }
	}
}
