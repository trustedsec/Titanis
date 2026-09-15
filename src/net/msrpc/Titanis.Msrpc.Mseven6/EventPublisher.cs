using MS_EVEN6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mseven6
{
	public class EventPublisherInfo
	{
		public Guid PublisherGuid { get; internal set; }
		public string ResourceFilePath { get; internal set; }
		public string ParameterFilePath { get; internal set; }
		public string MessageFilePath { get; internal set; }
		public string[] ChannelReferencePath { get; internal set; }
		public uint[] ChannelReferenceIndex { get; internal set; }
		public uint[] ChannelReferenceID { get; internal set; }
		public uint[] ChannelReferenceFlags { get; internal set; }
		public uint[] ChannelReferenceMessageID { get; internal set; }
	}

	internal partial class EventPublisher
	{
		private readonly EventLog6Client owner;
		internal readonly RpcPointer<RpcContextHandle> handle;

		public EventPublisherInfo Info { get; }

		internal EventPublisher(EventLog6Client owner, RpcPointer<RpcContextHandle> handle, EventPublisherInfo info)
		{
			this.owner = owner;
			this.handle = handle;
			Info = info;
		}

		public int GetChannelIdByPath(string channelPath)
		{
			var index = Array.IndexOf(this.Info.ChannelReferencePath, channelPath);
			if (index >= 0)
			{
				return (int)this.Info.ChannelReferenceID[index];
			}
			else
				return -1;
		}

		internal async Task<List<EventMetadata>> EnumMessages(CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> eventMetaDataEnum = new();
			var res = (Win32ErrorCode)await owner.ClientProxy.EvtRpcGetEventMetadataEnum(
				handle.value,
				0,
				null,
				eventMetaDataEnum,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
			try
			{
				List<EventMetadata> list = new List<EventMetadata>();
				while (true)
				{
					RpcPointer<uint> numReturned = new();
					RpcPointer<RpcPointer<MS_EVEN6.EvtRpcVariantList[]>> eventMetadataInstances = new();
					res = (Win32ErrorCode)await owner.ClientProxy.EvtRpcGetNextEventMetadata(
						eventMetaDataEnum.value,
						0,
						NativeConstants.MAX_RPC_EVENT_METADATA_COUNT,
						numReturned,
						eventMetadataInstances,
						cancellationToken
						).ConfigureAwait(false);
					res.CheckAndThrow();

					if (numReturned.value == 0)
						break;

					foreach (var evt in eventMetadataInstances.value.value)
					{
						EventMetadata info = new EventMetadata();

						var props = evt.props.value;
						for (int i = 0; i < props.Length; i++)
						{
							MS_EVEN6.EvtRpcVariant item = props[i];
							switch ((i, item.type))
							{
								case (0, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.EventId = (ushort)item.unnamed_1.uint32Val; break;
								case (1, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.Version = (byte)item.unnamed_1.uint32Val; break;
								case (2, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.ChannelId = (byte)item.unnamed_1.uint32Val; break;
								case (3, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.Level = (byte)item.unnamed_1.uint32Val; break;
								case (4, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.Opcode = (byte)item.unnamed_1.uint32Val; break;
								case (5, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.Task = (ushort)item.unnamed_1.uint32Val; break;
								case (6, EvtRpcVariantType.EvtRpcVarTypeUInt32): info.Keyword = item.unnamed_1.uint32Val; break;
								case (7, EvtRpcVariantType.EvtRpcVarTypeUInt64): info.MessageId = item.unnamed_1.uint64Val; break;
								case (8, EvtRpcVariantType.EvtRpcVarTypeString): info.Template = item.unnamed_1.stringVal.value; break;
								case (_, EvtRpcVariantType.EvtRpcVarTypeNull): break;
								default: break;
							}
						}

						list.Add(info);
					}
				}

				return list;
			}
			finally
			{
				await owner.ClientProxy.EvtRpcClose(eventMetaDataEnum, CancellationToken.None).ConfigureAwait(false);
			}
		}
	}

	partial class EventPublisher : IDisposable, IAsyncDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.owner.ClientProxy.EvtRpcClose(this.handle, CancellationToken.None).Wait();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async ValueTask DisposeAsync()
		{
			await owner.ClientProxy.EvtRpcClose(handle, CancellationToken.None).ConfigureAwait(false);
		}
	}
}
