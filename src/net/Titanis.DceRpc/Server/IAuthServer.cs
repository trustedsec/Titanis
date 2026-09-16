using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.DceRpc.Server
{
	public interface IAuthServer
	{
		AuthServerContext CreateContext(RpcAuthType authType);
	}
}
