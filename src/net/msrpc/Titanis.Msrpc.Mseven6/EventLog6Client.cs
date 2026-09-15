using MS_EVEN6;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Mseven6.BinXml;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mseven6
{
	public class EventLog6Client : RpcServiceClient<MS_EVEN6.IEventServiceClientProxy>
	{
		// [MS-EVEN6] § 1.9 Standards Assignments
		public override string? WellKnownPipeName => "Eventlog";

		// [MS-EVEN6] § 2.1.2 Client
		public override bool SupportsDynamicTcp => true;

		// [MS-EVEN6] § 2.1.2 Client
		public override bool RequiresEncryptionOverTcp => true;

		// [MS-EVEN6] § 3.1.4.12 EvtRpcRegisterLogQuery (Opnum 5)
		enum EventQuerySource
		{
			Channel = 1,
			FilePath = 2,
		}

		internal MS_EVEN6.IEventServiceClientProxy ClientProxy => this._proxy;

		public void Test(byte[] eventData)
		{
			var dot = BinXmlDocument.Read(eventData);
		}


		public const int MaxEventsPerPage = NativeConstants.MAX_RPC_RECORD_COUNT;

		public async Task<EventLogReader> Query(
			string channel,
			string query,
			int pageSize,
			int lcid,
			EventLogReaderOptions options,
			CancellationToken cancellationToken)
		{
			if (pageSize <= 0)
				throw new ArgumentOutOfRangeException(nameof(pageSize));

			DceRpc.RpcPointer<DceRpc.RpcContextHandle> handle = new();
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> opControl = new();
			DceRpc.RpcPointer<uint> queryChannelInfoSize = new();
			DceRpc.RpcPointer<DceRpc.RpcPointer<MS_EVEN6.EvtRpcQueryChannelInfo[]>> queryChannelInfo = new();
			DceRpc.RpcPointer<MS_EVEN6.RpcInfo> error = new();
			var res = (Win32ErrorCode)await _proxy.EvtRpcRegisterLogQuery(
				channel,
				query,
				(uint)EventQuerySource.Channel,
				handle,
				opControl,
				queryChannelInfoSize,
				queryChannelInfo,
				error,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new EventLogReader(this, handle.value, pageSize, lcid, options);
		}

		internal async Task<EventPublisher> GetPublisher(string name, int lcid, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<MS_EVEN6.EvtRpcVariantList> pubMetadataProps = new();
			DceRpc.RpcPointer<DceRpc.RpcContextHandle> pubMetadata = new();
			var res = (Win32ErrorCode)await _proxy.EvtRpcGetPublisherMetadata(
				name,
				null,
				(uint)lcid,
				0,
				pubMetadataProps,
				pubMetadata,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			Dictionary<string, object> props = new Dictionary<string, object>();
			EventPublisherInfo info = new EventPublisherInfo();
			if (pubMetadataProps.value.count > 0)
			{
				for (int i = 0; i < pubMetadataProps.value.props.value.Length; i++)
				{
					MS_EVEN6.EvtRpcVariant item = pubMetadataProps.value.props.value[i];
					switch ((i, item.type))
					{
						case (0, EvtRpcVariantType.EvtRpcVarTypeGuid): info.PublisherGuid = item.unnamed_1.guidVal.value; break;
						case (1, EvtRpcVariantType.EvtRpcVarTypeString): info.ResourceFilePath = item.unnamed_1.stringVal.value; break;
						case (2, EvtRpcVariantType.EvtRpcVarTypeString): info.ParameterFilePath = item.unnamed_1.stringVal.value; break;
						case (3, EvtRpcVariantType.EvtRpcVarTypeString): info.MessageFilePath = item.unnamed_1.stringVal.value; break;
						case (7, EvtRpcVariantType.EvtRpcVarTypeStringArray): info.ChannelReferencePath = Array.ConvertAll(item.unnamed_1.stringArray.ptr.value, r => r.value); break;
						case (8, EvtRpcVariantType.EvtRpcVarTypeUInt32Array): info.ChannelReferenceIndex = item.unnamed_1.uint32Array.ptr.value; break;
						case (9, EvtRpcVariantType.EvtRpcVarTypeUInt32Array): info.ChannelReferenceID = item.unnamed_1.uint32Array.ptr.value; break;
						case (10, EvtRpcVariantType.EvtRpcVarTypeUInt32Array): info.ChannelReferenceFlags = item.unnamed_1.uint32Array.ptr.value; break;
						case (11, EvtRpcVariantType.EvtRpcVarTypeUInt32Array): info.ChannelReferenceMessageID = item.unnamed_1.uint32Array.ptr.value; break;
						case (_, EvtRpcVariantType.EvtRpcVarTypeNull): break;
						default: break;
					}
				}
				//Debug.Assert(info.ChannelReferenceIndex.Length < 1 || info.ChannelReferenceIndex[0] == 0);
			}

			return new EventPublisher(this, pubMetadata, info);
		}
	}
}
