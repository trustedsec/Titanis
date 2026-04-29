using System.ComponentModel;

namespace Titanis.Cli.Fuse
{
	public class FuseParameterGroup : ParameterGroupBase
	{
		[Parameter(10)]
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
	}
}
