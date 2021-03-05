using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;

namespace Titanis.DceRpc.Server
{
	public abstract class RpcObjectStub : RpcServiceStub
	{
		public override async Task DispatchAsync(
			ushort opnum,
			Guid objectId,
			RpcDecoder stubData,
			RpcEncoder responseData,
			CancellationToken cancellationToken
			)
		{
			OperationImplFunc[] dispatchtable = this.DispatchTable;
			var opMethod = dispatchtable[opnum];

			var orptThis = stubData.ReadFixedStruct<ms_dcom.ORPCTHIS>(NdrAlignment.NativePtr);

			await opMethod(stubData, responseData, cancellationToken).ConfigureAwait(false);
			// TODO: Send response
			throw new NotImplementedException();
		}
	}
}
