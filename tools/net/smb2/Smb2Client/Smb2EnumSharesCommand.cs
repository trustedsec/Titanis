using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Cli
{
	/// <task category="Enumeration">Enumerate the shares of an SMB server</task>
	[Command]
	[OutputRecordType(typeof(ShareInfo))]
	[Description("Lists shares on the server")]
	internal sealed class Smb2EnumSharesCommand : ServerServiceRpcCommand
	{
		[Parameter]
		[Description("Which level of detail to query")]
		public ShareInfoLevel Level { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.Level == 0)
				this.Level = ShareInfoLevel.Level503;
		}

		protected sealed override async Task<int> RunAsync(ServerServiceClient srvs, CancellationToken cancellationToken)
		{
			var shares = await srvs.GetShares(@"\\" + this.ServerName, this.Level, this.BufferSize, cancellationToken);

			this.WriteRecords(shares);

			return 0;
		}
	}
}
