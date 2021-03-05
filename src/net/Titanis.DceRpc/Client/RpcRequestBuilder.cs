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
		private Guid _objectId;
		private int _offCallData;

		internal bool HasObjectId { get; }

		internal RpcRequestBuilder(
			ushort opnum,
			RpcEncoding encoding,
			RpcCallContext callContext,
			bool withObjectId = false)
		{
			this._opnum = opnum;
			this._stubData = encoding.CreateEncoder(callContext);
			this.callContext = callContext;

			int cbReserve = PduHeader.PduStructSize + (withObjectId ? RequestPduHeader.StructSizeWithObjectId : RequestPduHeader.StructSize);
			this._offCallData = cbReserve;
			this._stubData.GetWriter().Advance(cbReserve);
		}

		internal RpcRequestBuilder(
			ushort opnum,
			RpcEncoding encoding,
			RpcCallContext callContext,
			Guid objectId)
			: this(opnum, encoding, callContext, true)
		{
			this._objectId = objectId;
			this.HasObjectId = true;
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
			if (this.HasObjectId)
				writer.WriteGuid(this._objectId);

			writer.SetPosition(pos);

			return writer;
		}
	}
}