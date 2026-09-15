using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Titanis.DceRpc;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mseven6
{

	[Flags]
	public enum EventLogReaderOptions
	{
		None = 0,

		RenderMessages = 1,
	}

	public class EventLogReader
	{
		private readonly EventLog6Client owner;
		private readonly RpcContextHandle handle;
		private readonly int pageSize;
		private readonly EventLogReaderOptions options;
		private readonly int lcid;

		internal EventLogReader(
			EventLog6Client owner,
			DceRpc.RpcContextHandle handle,
			int pageSize,
			int lcid,
			EventLogReaderOptions options
			)
		{
			this.owner = owner;
			this.handle = handle;
			this.pageSize = pageSize;
			this.options = options;
			this.lcid = lcid;
		}

		private static BinXml.EventRecordBuilder builder = new();

		private Dictionary<string, EventPublisher> _publishers = new Dictionary<string, EventPublisher>();

		private EventRecord[]? _page;
		private int _pageIndex;

		public EventRecord Current { get; set; }

		public ValueTask<bool> ReadNext(CancellationToken cancellationToken) => this.ReadNext(Timeout.InfiniteTimeSpan, cancellationToken);
		public async ValueTask<bool> ReadNext(TimeSpan timeout, CancellationToken cancellationToken)
		{
			if (this._pageIndex < (this._page?.Length ?? 0))
			{
				this.Current = this._page[this._pageIndex++];
				return true;
			}
			else
			{
				this._page = await FetchNextPage(timeout, cancellationToken).ConfigureAwait(false);

				if (this._page.Length > 0)
				{
					this.Current = this._page[0];
					this._pageIndex = 1;
					return true;
				}
				else
					return false;
			}
		}
		private async Task<EventRecord[]> FetchNextPage(TimeSpan timeout, CancellationToken cancellationToken)
		{
			RpcPointer<uint> numActualRecords = new();
			RpcPointer<RpcPointer<uint[]>> eventDataIndices = new();
			RpcPointer<RpcPointer<uint[]>> eventDataSizes = new();
			RpcPointer<uint> resultBufferSize = new();
			RpcPointer<RpcPointer<byte[]>> resultBuffer = new();
			try
			{
				var res = (Win32ErrorCode)await owner.ClientProxy.EvtRpcQueryNext(
					this.handle,
					(uint)this.pageSize,
					(uint)timeout.TotalMilliseconds,
					0,
					numActualRecords,
					eventDataIndices,
					eventDataSizes,
					resultBufferSize,
					resultBuffer,
					cancellationToken
					).ConfigureAwait(false);
				res.CheckAndThrow();
			}
			catch (RpcFaultException ex) when (ex.Status == DceRpc.WireProtocol.RpcFaultCode.nca_s_fault_invalid_bound)
			{
				return [];
			}

			byte[] evtdescBytes = new EVENT_DESCRIPTOR().ToBytes();


			EventRecord[] records = new EventRecord[numActualRecords.value];
			for (int i = 0; i < records.Length; i++)
			{
				var readIndex = (int)eventDataIndices.value.value[i];
				var length = (int)eventDataSizes.value.value[i];

				var data = resultBuffer.value.value.AsMemory(readIndex, length);
				var reader = new ByteMemoryReader(data);
				var resultSet = reader.ReadPduStruct<ResultSet>();
				var doc = BinXml.BinXmlDocument.Read(resultSet.eventData);

				var rec = builder.BuildRecord(doc);

#if DEBUG
				var xml = rec.Xml;
#endif

				// EvtRpcMessageRender doesn't appear to work
				if (false && ((rec.ProviderName != null) && (0 != (this.options & EventLogReaderOptions.RenderMessages))))
				{
					if (!this._publishers.TryGetValue(rec.ProviderName, out var pub))
					{
						try
						{
							pub = await owner.GetPublisher(rec.ProviderName, this.lcid, cancellationToken).ConfigureAwait(false);
							this._publishers.Add(rec.ProviderName, pub);
							// UNDONE: Attempt to resolve event messages ahead of time
							//await pub.EnumMessages(cancellationToken).ConfigureAwait(false);
						}
						catch (Exception ex)
						{

						}
					}

					uint messageId;
					{
						ref EVENT_DESCRIPTOR evtdesc = ref MemoryMarshal.AsRef<EVENT_DESCRIPTOR>(evtdescBytes);
						evtdesc = new EVENT_DESCRIPTOR
						{
							Id = rec.EventId,
							Version = rec.Version,
							Channel = (byte)pub.GetChannelIdByPath(rec.Channel),
							Level = rec.Level,
							Opcode = rec.Opcode,
							Task = rec.Task,
							Keyword = (ulong)rec.Keywords,
						};
						messageId = rec.EventId;
					}

					RpcPointer<uint> actualSizeString = new();
					RpcPointer<uint> neededSizeString = new();
					RpcPointer<RpcPointer<byte[]>> pStr = new();
					RpcPointer<MS_EVEN6.RpcInfo> error = new();
					var variants = rec.Data.Select(r => r.variant).ToArray();
					var res = (Win32ErrorCode)await owner.ClientProxy.EvtRpcMessageRender(
						pub.handle.value,
						(uint)evtdescBytes.Length, evtdescBytes,
						messageId,
						new MS_EVEN6.EvtRpcVariantList
						{
							count = (uint)variants.Length,
							props = new RpcPointer<MS_EVEN6.EvtRpcVariant[]>(variants)
						},
						(uint)MessageRenderFlags.MessageId,
						0x20_0000,
						actualSizeString,
						neededSizeString,
						pStr,
						error,
						cancellationToken
						).ConfigureAwait(false);
					if (res == 0)
					{
						//res.CheckAndThrow();
						if (error.value.m_error == 0)
						{
							string str = Encoding.Unicode.GetString(pStr.value.value);
						}
						else if ((Win32ErrorCode)error.value.m_error != Win32ErrorCode.ERROR_INVALID_DATA)
						{

						}
					}
					else if (res != Win32ErrorCode.ERROR_INVALID_PARAMETER)
					{

					}
				}

				records[i] = rec;
			}

			return records;
		}
	}

	// [MS-EVEN6] § 3.1.4.31 EvtRpcMessageRender (Opnum 9)
	enum MessageRenderFlags
	{
		Event = 1,
		Level = 2,
		Task = 3,
		Opcode = 4,
		Keyword = 5,
		Channel = 6,
		Provider = 7,
		MessageId = 8
	}
}
