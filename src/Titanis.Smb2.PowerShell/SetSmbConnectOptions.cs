using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.PowerShell
{
	public abstract class SmbCmdlet : PSCmdlet
	{
		protected override void ProcessRecord()
		{
			var smb = (SmbProviderInfo)this.SessionState.Provider.GetOne(SmbProvider.ProviderName);
			this.ProcessRecord(smb);
		}

		protected abstract void ProcessRecord(SmbProviderInfo smb);
	}

	[Cmdlet(VerbsCommon.Set, "SmbConnectOptions")]
	public class SetSmbConnectOptions : SmbCmdlet, IDynamicParameters
	{
		[Parameter(Position = 0)]
		public string? ServerName { get; set; }

		private SmbConnectionParameters _parms = new SmbConnectionParameters();
		public object GetDynamicParameters()
			=> this._parms;

		protected override void ProcessRecord(SmbProviderInfo smb)
		{
			if (string.IsNullOrEmpty(this.ServerName))
			{
				this.WriteVerbose("Setting default connection parameters (no server specified)");
				smb.DefaultConnectParameters = this._parms.MergeOnto(SmbConnectionParameters.GetDefault());
			}
			else
			{
				var oldParms = smb.GetConnectParametersFor(this.ServerName, true);
				var parms = this._parms;
				parms = parms.MergeOnto(SmbConnectionParameters.GetDefault());

				smb.SetConnectParameters(this.ServerName, parms);
			}
		}
	}
}
