using ms_dcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;

namespace Titanis.Msrpc.Msdcom
{
	internal class ObjectExporterClient : RpcServiceClient<IObjectExporterClientProxy>
	{
		public async Task<ObjectExporterServerInfo> GetServerInfo(CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<COMVERSION> pComVersion = new DceRpc.RpcPointer<COMVERSION>();
			DceRpc.RpcPointer<DceRpc.RpcPointer<DUALSTRINGARRAY>> pBindings = new DceRpc.RpcPointer<DceRpc.RpcPointer<DUALSTRINGARRAY>>();
			await this._proxy.ServerAlive2(
				pComVersion,
				pBindings,
				new RpcPointer<uint>(),
				cancellationToken
				).ConfigureAwait(false);

			return new ObjectExporterServerInfo(
				pComVersion.value,
				DualStringArray.FromIdl(pBindings.value.value)
			);
		}
	}

	public class ObjectExporterServerInfo
	{
		internal ObjectExporterServerInfo(COMVERSION version, DualStringArray bindings)
		{
			Version = version;
			Bindings = bindings;
		}

		public COMVERSION Version { get; set; }
		public DualStringArray Bindings { get; set; }
	}
}
