using System.ComponentModel;
using Titanis.Cli;
using Titanis.Cli.Fuse;
using Titanis.DceRpc.Client;
using Titanis.Linterop.Fuse;
using Titanis.Msrpc.Mswkst;
using Titanis.Smb2.Fusion;

namespace Titanis.Smb2.Cli;

partial class Program : MultiCommand { }

[Command]
[Description("Mounts an SMB2 server or share to the local file system.")]
[DetailedHelpText(@"The UNC path may either be a server, a share, or a path within a share.  If the UNC path is a server, the directory listing enumerates shares on the server.

The filesystem is mounted as read-only unless -ReadWrite is specified.

The directory listings use the UID and GID of the current user (obtained using getuid() and getegid()) and a mode of r-xr-xr-x or rwxr-xr-x, dependening on whether -ReadWrite is specified.  To retrieve the actual owner and DACL, print the extended attributes titanis.smb2.file.ownersid and titanis.smb2.file.dacltext.  For example:

	getfattr -n titanis.smb2.file.ownersid smbmount

Files within IPC$ are presented as sockets.
")]
internal class FuseCommand : Smb2CommandBase
{
	[ParameterGroup(ParameterGroupOptions.Required)]
	public FuseParameterGroup FuseParameters { get; set; }

	[Parameter]
	[Description("Mount the file system using backup semantics")]
	public SwitchParam BackupSemantics { get; set; }

	protected override Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
	{
		var fuseParams = this.FuseParameters;
		var rpcClient = this.CreateRpcClient();
		var mountInfo = new SmbMountInfo()
		{
			uid = fuseParams.Uid ?? NativeMethods.geteuid(),
			gid = fuseParams.Gid ?? NativeMethods.getegid(),
			defaultDirAccess = PosixFileMode.Mode777,
			defaultFileAccess = PosixFileMode.Mode777,
			smbClient = client,
			extraCreateOptions = this.BackupSemantics.IsSet ? Smb2FileCreateOptions.OpenForBackupIntent : Smb2FileCreateOptions.None,
		};

		IFuseNode rootNode;
		var uncPath = this.UncPath;
		if (string.IsNullOrEmpty(uncPath.ShareName))
			rootNode = new ServerRootNode(mountInfo, rpcClient, uncPath);
		else if (!uncPath.HasShareRelativePath)
			rootNode = new ShareNode(mountInfo, uncPath, (uncPath.ShareName.Equals(Smb2Client.IpcName, StringComparison.OrdinalIgnoreCase)) ? ShareTypeFlags.Ipc : ShareTypeFlags.Disk, null);
		else
			rootNode = new SharedDirNode(mountInfo, uncPath, new Smb2DirEntry()
			{
				FileName = Path.GetFileName(uncPath.ShareRelativePath)
			});

		FuseMount.Mount(fuseParams.Mountpoint, rootNode, this.Log, fuseParams.ReadWrite.IsSet, cancellationToken, ["Smb2mount"]);
		return Task.FromResult(0);
	}
}
