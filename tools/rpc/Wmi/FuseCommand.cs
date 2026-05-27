using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli.Fuse;
using Titanis.Linterop.Fuse;
using Titanis.Msrpc.Mswmi;
using Titanis.Msrpc.Mswmi.Fusion;

namespace Titanis.Cli.WmiTool;

[Description("Mounts a WMI namespace as a file system")]
internal class FuseCommand : WmiNamespaceCommandBase
{
	[ParameterGroup(ParameterGroupOptions.Required)]
	public FuseParameterGroup FuseParameters { get; set; }

	protected override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		var fuseParams = this.FuseParameters;

		await Task.Yield();

		var rootNode = new WmiNamespaceNode(new Msrpc.Mswmi.Fusion.WmiMountInfo
		{
			wmiClient = ns.Client,
			locale = this.Locale,
			uid = fuseParams.Uid ?? Linterop.Fuse.NativeMethods.geteuid(),
			gid = fuseParams.Gid ?? Linterop.Fuse.NativeMethods.getegid(),
		}, this.Namespace, ".", ns);

		FuseMount.Mount(fuseParams.Mountpoint, rootNode, this.Log, fuseParams.ReadWrite.IsSet, cancellationToken, ["WmiMount"]);

		return 0;
	}
}
