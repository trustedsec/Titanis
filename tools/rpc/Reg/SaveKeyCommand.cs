using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;
using Titanis.Cli.Registry;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <task category="Registry">Saves a registry key to a file on the remote host</task>
	[Command]
	[Description("Saves a key to a file")]
	[OutputRecordType(typeof(RegistryItem))]
	internal class SaveKeyCommand : RegistryKeyCommand
	{
		[Parameter(20)]
		[Mandatory]
		[Description("Name of file to save to")]
		public string FileName { get; set; }

		[Parameter]
		[Description("Format of save file")]
		public RegistrySaveFormat Format { get; set; }

		protected override RegistryAccessRights RequiredKeyAccess => RegistryAccessRights.EnumerateSubkeys;

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			if (this.Format == 0)
				this.Format = RegistrySaveFormat.Latest;

			await key.SaveKey(this.FileName, this.Format, cancellationToken);

			return 0;
		}
	}
}
