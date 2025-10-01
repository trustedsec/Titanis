using ms_dcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Security;

namespace Titanis.Msrpc.Msdcom
{
	internal class ObjectExporterClient : RpcServiceClient<IObjectExporterClientProxy>
	{

		// [MS-DCOM] § 1.9
		/// <inheritdoc/>
		public sealed override bool SupportsDynamicTcp => true;
		// [MS-DCOM] § 1.9
		/// <inheritdoc/>
		public sealed override int WellKnownTcpPort => 135;
		// [MS-DCOM] § 3.2.4.1.1.2
		/// <inheritdoc/>
		public sealed override string? ServiceClass => ServiceClassNames.RpcSs;
		// [MS-DCOM] § 2.2
		/// <inheritdoc/>
		public sealed override bool SupportsNdr64 => false;

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
