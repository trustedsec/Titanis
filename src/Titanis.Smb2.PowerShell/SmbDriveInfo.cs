using System.Management.Automation;

namespace Titanis.Smb2.PowerShell
{
	abstract class SmbDriveInfo : PSDriveInfo
	{
		protected SmbDriveInfo(PSDriveInfo driveInfo) : base(driveInfo)
		{
		}
	}
	class SmbRootDriveInfo : PSDriveInfo
	{
		internal SmbRootDriveInfo(PSDriveInfo driveInfo)
			: base(driveInfo)
		{

		}
	}

	class SmbServerDriveInfo : PSDriveInfo
	{
		internal SmbServerDriveInfo(PSDriveInfo driveInfo)
			: base(driveInfo)
		{

		}
	}

	class SmbShareDriveInfo : PSDriveInfo
	{
		internal SmbShareDriveInfo(
			PSDriveInfo driveInfo,
			SmbProvider provider,
			UncPath uncPath,
			Smb2TreeConnect? share
			)
			: base(driveInfo)
		{
			this.Share = share;
		}

		public Smb2TreeConnect? Share { get; }
	}
}
