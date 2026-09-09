using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;

namespace Titanis.Msrpc.Mstsch
{
	public class AtServiceClient : RpcServiceClient<atsvc.atsvcClientProxy>
	{
		// [MS-TSCH] § 1.9 Standards Assignments
		public override string? WellKnownPipeName => "atsvc";
	}
}
