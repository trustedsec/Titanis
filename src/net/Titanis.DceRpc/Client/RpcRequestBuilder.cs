using System;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.Server;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;
using Titanis.Security;

namespace Titanis.DceRpc.Client
{
	public class RpcRequestBuilder : IRpcRequestBuilder
	{
		private readonly ushort _opnum;
		private RpcEncoder _stubData;
		public RpcEncoder StubData => this._stubData;
		IRpcEncoder IRpcRequestBuilder.StubData => this.StubData;
		internal readonly RpcCallContext callContext;
		public Guid? ObjectId { get; }
		private int _offCallData;

		internal RpcRequestBuilder(
			ushort opnum,
			RpcEncoding encoding,
			RpcCallContext callContext,
			Guid? objectId)
		{
			this._opnum = opnum;
			this._stubData = encoding.CreateEncoder(callContext);
			this.callContext = callContext;

			this.ObjectId = objectId;

			int cbReserve = PduHeader.PduStructSize + (objectId.HasValue ? RequestPduHeader.StructSizeWithObjectId : RequestPduHeader.StructSize);
			this._offCallData = cbReserve;
			this._stubData.GetWriter().Advance(cbReserve);
		}

		private Span<byte> GetCallData()
		{
			var data = this._stubData.GetWriter().GetData();
			return data.Span.Slice(this._offCallData);
		}

		internal ByteWriter Complete(
			RpcBindContext context
			)
		{
			var writer = this._stubData.GetWriter();
			var allocHint = writer.Position - this._offCallData;

			var pos = writer.Position;
			writer.SetPosition(PduHeader.PduStructSize);
			writer.WriteRequestPduHeader(new RequestPduHeader
			{
				alloc_hint = (ushort)allocHint,
				p_cont_id = (ushort)context.contextId,
				opnum = this._opnum
			});
			if (this.ObjectId.HasValue)
				writer.WriteGuid(this.ObjectId.Value);

			writer.SetPosition(pos);

			return writer;
		}
	}
}