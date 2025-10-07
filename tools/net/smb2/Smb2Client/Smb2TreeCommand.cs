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
	abstract class Smb2TreeCommand : Smb2CommandBase
	{
		[Parameter]
		[Category(ParameterCategories.Connection)]
		[Description("Encrypts PDUs for the target share")]
		public SwitchParam EncryptShare { get; set; }

		[Parameter]
		[Category(ParameterCategories.ClientBehavior)]
		[Description("Opens remote resource with backup semantics")]
		public SwitchParam UseBackupSemantics { get; set; }

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
		/// <param name="options">The initial file creation options to modify.</param>
		/// <returns>The modified file creation options, including the "Open for Backup Intent" flag if  backup semantics are enabled;
		/// otherwise, the original options.</returns>
		protected Smb2FileCreateOptions GetCreateOptions(Smb2FileCreateOptions options)
		{
			if (this.UseBackupSemantics.IsSet)
				options |= Smb2FileCreateOptions.OpenForBackupIntent;
			return options;
		}

		protected Smb2CreateInfo GetCreateDirectoryCreateInfo() =>

			new Smb2CreateInfo
			{
				Priority = Smb2Priority.CreateDir,
				CreateDisposition = Smb2CreateDisposition.Create,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateDirAccess,
				ShareAccess = Smb2ShareAccess.ReadWrite,
				FileAttributes = Winterop.FileAttributes.Normal,
				CreateOptions = GetCreateOptions(Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.SynchronousIoNonalert),
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			};


		protected Smb2CreateInfo GetOpenDirectoryCreateInfo() => new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Open,
				Priority = Smb2Priority.OpenDir,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = GetCreateOptions(Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert),
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				OplockLevel = Smb2OplockLevel.Lease,
		};





		protected Smb2CreateInfo GetRemoveDirectoryCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.Open,
			Priority = 0,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultRemoveDirAccess,
			ShareAccess = Smb2ShareAccess.DefaultDirShare,
			FileAttributes = Winterop.FileAttributes.None,
			CreateOptions = GetCreateOptions(Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose),
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			RequestMaximalAccess = true,
			QueryOnDiskId = true,
			OplockLevel = Smb2OplockLevel.Lease,
		};


		protected Smb2CreateInfo GetDeleteFileCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.Open,
			Priority = 0,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultDeleteFileAccess,
			ShareAccess = Smb2ShareAccess.Delete,
			FileAttributes = 0,
			CreateOptions = GetCreateOptions(Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose),
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			RequestMaximalAccess = true,
			QueryOnDiskId = true,
			OplockLevel = Smb2OplockLevel.Lease,
		};
	
		protected Smb2CreateInfo GetCreateFileCreateInfo(Winterop.FileAttributes attributes) => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.Supersede,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultCreateAccess,
			ShareAccess = Smb2ShareAccess.ReadWrite,
			FileAttributes = attributes,
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			CreateOptions = GetCreateOptions(Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert)
		};

		protected Smb2CreateInfo GetOpenFileCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.Open,
			DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenReadAccess,
			ShareAccess = Smb2ShareAccess.Read,
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			CreateOptions = GetCreateOptions(Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert),
			FileAttributes = Winterop.FileAttributes.Normal
		}; //has the same default options as Create

		protected Smb2CreateInfo GetPipeFileCreateInfo() => new Smb2CreateInfo
		{
			CreateDisposition = Smb2CreateDisposition.Open,
			ShareAccess = Smb2ShareAccess.ReadWriteDelete,
			FileAttributes = 0,
			DesiredAccess = (uint)0x0012019f,
			ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
			CreateOptions = GetCreateOptions(Smb2FileCreateOptions.None)
		};
}
}
