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
	[OutputRecordType(typeof(ShareInfo))]
	[Command(HelpText = "Creates a new share")]
	internal sealed class Smb2NewShareCommand : ServerServiceRpcCommand
	{
		protected sealed override Task<int> RunAsync(ServerServiceClient client, CancellationToken cancellationToken)
		{
			//await srvs.AddShare(new ShareInfo(), cancellationToken);
			// TODO: Implement NewShare
			throw new NotImplementedException();
		}
	}
}
