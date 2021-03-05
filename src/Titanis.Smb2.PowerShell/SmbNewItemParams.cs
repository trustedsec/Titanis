using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.PowerShell
{
	internal abstract class SmbNewItemParams
	{
		internal abstract Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken);
	}

	internal class SmbNewFileItemParams : SmbNewItemParams
	{
		internal override Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}

	internal class SmbNewDirectoryItemParams : SmbNewItemParams
	{
		internal override async Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken)
		{
			return await smb.CreateDirectoryAsync(uncPath, cancellationToken);
		}
	}

	internal class SmbMountPointItemParams : SmbNewItemParams
	{
		[Parameter(Position = 100, Mandatory = true)]
		public string MountPointTarget { get; set; }

		internal override async Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken)
		{
			var file = await smb.CreateFileAsync(uncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.WriteAttributes,
				FileAttributes = WinFileInfo.FileAttributes.Normal,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.OpenIf,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken);
			//if (file.CreateAction != Pdus.Smb2CreateAction.Created)
			//	this.WriteVerbose($"Directory {this.UncPath} already existed");
			//else
			//	this.WriteVerbose($"Directory {this.UncPath} created");

			await file.SetVolumeMountPointInfoAsync(this.MountPointTarget, this.MountPointTarget, cancellationToken);
			return file;
		}
	}

	internal class SmbSymlinkDirItemParams : SmbNewItemParams
	{
		[Parameter(Position = 100, Mandatory = true)]
		public string TargetPath { get; set; }

		[Parameter]
		public SwitchParameter Relative { get; set; }

		internal override async Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken)
		{
			var file = await smb.CreateFileAsync(uncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.WriteAttributes,
				FileAttributes = WinFileInfo.FileAttributes.Normal,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.OpenIf,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken);
			//if (file.CreateAction != Pdus.Smb2CreateAction.Created)
			//	this.WriteVerbose($"Directory {this.UncPath} already existed");
			//else
			//	this.WriteVerbose($"Directory {this.UncPath} created");

			await file.SetSymlinkInfoAsync(
				this.Relative ? WinFileInfo.SymbolicLinkFlags.RelativePath : WinFileInfo.SymbolicLinkFlags.FullPathName,
				this.TargetPath,
				this.TargetPath,
				cancellationToken);
			return file;
		}
	}

	internal class SmbSymlinkItemParams : SmbNewItemParams
	{
		[Parameter(Position = 100, Mandatory = true)]
		public string TargetPath { get; set; }

		[Parameter]
		public SwitchParameter Relative { get; set; }

		internal override async Task<Smb2OpenFileObjectBase> Create(Smb2Client smb, UncPath uncPath, CancellationToken cancellationToken)
		{
			var file = await smb.CreateFileAsync(uncPath, new Smb2CreateInfo
			{
				OplockLevel = Smb2OplockLevel.None,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				DesiredAccess = (uint)Smb2FileAccessRights.WriteAttributes,
				FileAttributes = WinFileInfo.FileAttributes.Normal,
				ShareAccess = Smb2ShareAccess.ReadWriteDelete,
				CreateDisposition = Smb2CreateDisposition.OpenIf,
				CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.OpenReparsePoint,
				RequestMaximalAccess = true,
				QueryOnDiskId = true
			}, FileAccess.Read, cancellationToken);
			//if (file.CreateAction != Pdus.Smb2CreateAction.Created)
			//	this.WriteVerbose($"Directory {this.UncPath} already existed");
			//else
			//	this.WriteVerbose($"Directory {this.UncPath} created");

			await file.SetSymlinkInfoAsync(
				this.Relative ? WinFileInfo.SymbolicLinkFlags.RelativePath : WinFileInfo.SymbolicLinkFlags.FullPathName,
				this.TargetPath,
				this.TargetPath,
				cancellationToken);
			return file;
		}
	}
}
