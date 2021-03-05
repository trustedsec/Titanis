using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.PowerShell
{
	[Cmdlet(VerbsCommunications.Connect, "SmbServer")]
	public class ConnectSmbServer : PSCmdlet
	{
		[Parameter(Mandatory = true)]
		public string ServerName { get; set; }

		[Parameter]
		public string? RemoteAddress { get; set; }

		[Parameter]
		public int RemotePort { get; set; }

		protected override void ProcessRecord()
		{
			var prov = (SmbProviderInfo)this.SessionState.Provider.GetOne(SmbProvider.ProviderName);
		}
	}
}
