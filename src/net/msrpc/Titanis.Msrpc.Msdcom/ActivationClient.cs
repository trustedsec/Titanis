using ms_dcom;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;

namespace Titanis.Msrpc.Msdcom
{
	using ms_dcom;
	using Titanis.Security;
	using Titanis.Winterop;

	class ActivationClient : RpcServiceClient<IActivationClientProxy>
	{
		// [MS-DCOM] <70>
		private const int WindowsClientImplLevel = 2;

		public async Task<ActivationResult> CreateInstance(
			Guid clsid,
			bool isClassFactory,
			COMVERSION version,
			Guid[] interfaceIDs,
			ushort[] protseqs,
			System.Threading.CancellationToken cancellationToken)
		{
			int[] qiResults = new int[interfaceIDs.Length];

			RpcPointer<ORPCTHAT> pThat = new DceRpc.RpcPointer<ORPCTHAT>();
			RpcPointer<ulong> pOxid = new DceRpc.RpcPointer<ulong>();
			RpcPointer<DceRpc.RpcPointer<DUALSTRINGARRAY>> pOxidBindings = new DceRpc.RpcPointer<DceRpc.RpcPointer<DUALSTRINGARRAY>>();
			RpcPointer<Guid> pipidRemUnknown = new DceRpc.RpcPointer<Guid>();
			RpcPointer<uint> pMinAuthLevel = new DceRpc.RpcPointer<uint>();
			RpcPointer<COMVERSION> pServerVersion = new DceRpc.RpcPointer<COMVERSION>();
			RpcPointer<int> pResultCode = new DceRpc.RpcPointer<int>();
			RpcPointer<DceRpc.RpcPointer<MInterfacePointer>[]> pInterfaces = new DceRpc.RpcPointer<DceRpc.RpcPointer<MInterfacePointer>[]>();
			RpcPointer<int[]> pQiResults = new DceRpc.RpcPointer<int[]>(qiResults);
			var result = (Win32ErrorCode)await this._proxy.RemoteActivation(
				new RpcPointer<ORPCTHIS>() { value = new ORPCTHIS() { version = version } },
				pThat,
				new RpcPointer<Guid>(clsid),
				null,
				null,
				WindowsClientImplLevel,
				isClassFactory ? 0xFFFFFFFFU : 0,
				(uint)interfaceIDs.Length,
				interfaceIDs,
				(ushort)protseqs.Length,
				protseqs,
				pOxid,
				pOxidBindings,
				pipidRemUnknown,
				pMinAuthLevel,
				pServerVersion,
				pResultCode,
				pInterfaces,
				pQiResults,
				cancellationToken).ConfigureAwait(false);

			result.CheckAndThrow();
			DcomClient.CheckHresultAndThrow((Hresult)pResultCode.value);

			ActivationResult actInfo = new ActivationResult()
			{
				Oxid = pOxid.value,
				OxidBinding = DualStringArray.FromIdl(pOxidBindings.value.value),
				IpidRemUnknown = pipidRemUnknown.value,
				AuthLevelHint = (RpcAuthLevel)pMinAuthLevel.value,
				ComVersion = pServerVersion.value,
				QueryInterfaceResults = ProcessQiResults(pQiResults.value, pInterfaces.value)
			};
			return actInfo;
		}

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

		internal static QueryInterfaceResult[] ProcessQiResults(int[] resultCodes, RpcPointer<MInterfacePointer>[] objrefs)
		{
			QueryInterfaceResult[] results = new QueryInterfaceResult[resultCodes.Length];
			for (int i = 0; i < resultCodes.Length; i++)
			{
				var resultCode = (Hresult)resultCodes[i];
				QueryInterfaceResult res = new QueryInterfaceResult(
					resultCode,
					(resultCode >= 0) ? Objref.Decode(objrefs[i].value) : null
					);
				results[i] = res;
			}

			return results;
		}
	}

	public class QueryInterfaceResult
	{
		public QueryInterfaceResult(Hresult resultCode, Objref? objref)
		{
			ResultCode = resultCode;
			Objref = objref;
		}

		public Hresult ResultCode { get; }
		public Objref? Objref { get; }
	}

	/// <summary>
	/// Describes the result of an activation request.
	/// </summary>
	public class ActivationResult
	{
		internal ActivationResult() { }

		public Guid IpidRemUnknown { get; internal set; }
		public ulong Oxid { get; internal set; }
		public DualStringArray OxidBinding { get; internal set; }
		public COMVERSION ComVersion { get; internal set; }
		public RpcAuthLevel AuthLevelHint { get; internal set; }
		public QueryInterfaceResult[] QueryInterfaceResults { get; internal set; }
	}
}
