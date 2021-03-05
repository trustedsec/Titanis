using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Builds a request for an RPC call.
	/// </summary>
	/// <remarks>
	/// This interface in primarily used by code generated for proxy methods.
	/// </remarks>
	public interface IRpcRequestBuilder
	{
		IRpcEncoder StubData { get; }
	}
}
