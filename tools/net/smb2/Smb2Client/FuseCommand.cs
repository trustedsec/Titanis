using System.ComponentModel;
using Titanis.Cli;
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
	[Parameter(After = nameof(UncPath))]
	[Description("Path of mountpoint in local filesystem")]
	public string Mountpoint { get; set; }

	[Parameter]
	[Description("UID of mount")]
	public uint? Uid { get; set; }

	[Parameter]
	[Description("GID of mount")]
	public uint? Gid { get; set; }

	[Parameter]
	[Description("Mount as read/write")]
	public SwitchParam ReadWrite { get; set; }

	protected override Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken)
	{
		var rpcClient = this.CreateRpcClient();
		var mountInfo = new SmbMountInfo()
		{
			uid = this.Uid ?? NativeMethods.geteuid(),
			gid = this.Gid ?? NativeMethods.getegid(),
			defaultDirAccess = PosixFileMode.Mode777,
			defaultFileAccess = PosixFileMode.Mode777,
			smbClient = client
		};

		IFuseNode rootNode;
		var uncPath = this.UncPath;
		if (string.IsNullOrEmpty(uncPath.ShareName))
			rootNode = new ServerRootNode(mountInfo, rpcClient, uncPath);
		else if (string.IsNullOrEmpty(uncPath.ShareRelativePath))
			rootNode = new ShareNode(mountInfo, uncPath, (uncPath.ShareName.Equals(Smb2Client.IpcName, StringComparison.OrdinalIgnoreCase)) ? ShareType.Ipc : ShareType.Disk, null);
		else
			rootNode = new SharedDirNode(mountInfo, uncPath, new Smb2DirEntry()
			{
				FileName = Path.GetFileName(uncPath.ShareRelativePath)
			});

		FuseMount.Mount(this.Mountpoint, rootNode, this.Log, this.ReadWrite.IsSet, cancellationToken, ["Smb2mount"]);
		return Task.FromResult(0);
	}
}
