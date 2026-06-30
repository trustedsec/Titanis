using System.ComponentModel;
using System.Threading;
using Titanis.Cli;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2.Cli
{
	/// <summary>
	/// Base class from SMB2 tree commands
	/// </summary>
	/// <remarks>
	/// A tree command operates on a share.  <see cref="ValidateParameters"/>
	/// is extended to ensure that <see cref="Smb2CommandBase.UncPath"/>
	/// specifies a share name.
	/// </remarks>
	public abstract class Smb2TreeCommand : Smb2CommandBase
	{
		[Parameter]
		[Category(ParameterCategories.Connection)]
		[Description("Encrypts PDUs for the target share")]
		public SwitchParam EncryptShare { get; set; }

		[Parameter]
		[Category(ParameterCategories.ClientBehavior)]
		[Description("Opens remote resource with backup semantics")]
		[Alias("UseBackupSemantics")]
		public SwitchParam BackupSemantics { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (string.IsNullOrEmpty(this.ShareName))
				context.LogError(nameof(ShareName), "The UNC path must include a share name.");
		}

		/// <summary>
		/// Modifies the specified file creation options to include the "Open for Backup Intent" flag  if backup semantics are
		/// enabled.
		/// </summary>
		/// <returns>The modified file creation options, including the "Open for Backup Intent" flag if  backup semantics are enabled;
		/// otherwise, the original options.</returns>
		protected Smb2FileCreateOptions GetExtraCreateOptions()
		{
			Smb2FileCreateOptions options = 0;
			if (this.BackupSemantics.IsSet)
				options |= Smb2FileCreateOptions.OpenForBackupIntent;
			return options;
		}

		protected Smb2CreateInfo GetCreateDirectoryCreateInfo() =>

			new Smb2CreateInfo
			{
				Priority = Smb2Priority.CreateDir,
				CreateDisposition = Smb2CreateDisposition.CreateNew,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateDirAccess,
				ShareAccess = Smb2ShareAccess.ReadWrite,
				FileAttributes = Winterop.FileAttributes.Normal,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.SynchronousIoNonalert | this.GetExtraCreateOptions(),
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			};


		protected Smb2CreateInfo GetOpenDirectoryCreateInfo() => new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.OpenExisting,
				Priority = Smb2Priority.OpenDir,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | GetExtraCreateOptions(),
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				OplockLevel = Smb2OplockLevel.Lease,
		};





		protected Smb2CreateInfo GetRemoveDirectoryCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.OpenExisting,
			Priority = 0,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultRemoveDirAccess,
			ShareAccess = Smb2ShareAccess.DefaultDirShare,
			FileAttributes = Winterop.FileAttributes.None,
			CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose | GetExtraCreateOptions(),
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			RequestMaximalAccess = true,
			QueryOnDiskId = true,
			OplockLevel = Smb2OplockLevel.Lease,
		};


		protected Smb2CreateInfo GetDeleteFileCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.OpenExisting,
			Priority = 0,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultDeleteFileAccess,
			ShareAccess = Smb2ShareAccess.Delete,
			FileAttributes = 0,
			CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose | GetExtraCreateOptions(),
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			RequestMaximalAccess = true,
			QueryOnDiskId = true,
			OplockLevel = Smb2OplockLevel.Lease,
		};

		[Obsolete(null, true)]
		protected Smb2CreateInfo GetCreateFileCreateInfo(Winterop.FileAttributes attributes) => new Smb2CreateInfo
		{
			//CreateDisposition = Smb2CreateDisposition.Supersede,
			//DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateAccess,
			//ShareAccess = Smb2ShareAccess.ReadWrite,
			FileAttributes = attributes,
			//ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			//CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | GetExtraCreateOptions()
		};

		[Obsolete(null, true)]
		protected Smb2CreateInfo GetOpenFileCreateInfo() => new Smb2CreateInfo
		{
			//CreateDisposition = Smb2CreateDisposition.OpenExisting,
			//DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenReadAccess,
			//ShareAccess = Smb2ShareAccess.Read,
			//ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			//CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | GetExtraCreateOptions(),
			//FileAttributes = Winterop.FileAttributes.Normal
		}; //has the same default options as Create
	}
}
