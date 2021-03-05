using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc.Server
{
	/// <summary>
	/// Defines the signature for methods implementing an operation stub.
	/// </summary>
	/// <param name="stubData">Stub data</param>
	/// <param name="responseData">Encoder to encode response data</param>
	/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
	/// <returns></returns>
	public delegate Task OperationImplFunc(IRpcDecoder stubData, IRpcEncoder responseData, CancellationToken cancellationToken);
}
