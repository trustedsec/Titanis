using System;
using System.Buffers.Binary;
using System.Diagnostics;
using Titanis.IO;

namespace Titanis.DceRpc
{
	public abstract class RpcEncoder : IRpcEncoder
	{
		protected readonly ByteWriter _writer;
		internal RpcCallContext callContext { get; private set; }

		private protected RpcEncoder(ByteWriter writer, RpcCallContext callContext)
		{
			if (writer is null)
				throw new ArgumentNullException(nameof(writer));
			this._writer = writer;
			this.callContext = callContext;
		}

		public ByteWriter GetWriter() => this._writer;

		public abstract void Align(NdrAlignment alignment);
		public abstract void WriteValue(bool v);
		public abstract void WriteValue(sbyte v);
		public abstract void WriteValue(byte v);
		public abstract void WriteValue(short v);
		public abstract void WriteValue(ushort v);
		public abstract void WriteValue(int v);
		public abstract void WriteValue(uint v);
		public void WriteValue(IntPtr n) => this.WriteValue(n.ToInt32());
		public void WriteValue(UIntPtr n) => this.WriteValue(n.ToUInt32());
		public abstract void WriteValue(long v);
		public abstract void WriteValue(ulong v);
		public abstract void WriteValue(float v);
		public abstract void WriteValue(double v);
		public abstract void WriteValue(char v);
		public abstract void WriteValue(Guid v);
		public abstract void WriteValue(RpcInterfaceId v);

		public abstract void WriteContextHandle(RpcContextHandle hctx);
		public abstract void WritePointer<T>(RpcPointer<T>? ptr);
		public abstract void WriteInterfacePointer<T>(TypedObjref<T>? ptr) where T : class, IRpcObject;
		public abstract void WriteInterfacePointerBody<T>(TypedObjref<T>? ptr) where T : class, IRpcObject;
		public abstract void WriteArrayHeader<T>(T[] array);
		public abstract void WriteArrayHeader<T>(ArraySegment<T> array, bool conformant);
		public abstract void WritePipeChunkHeader<T>(T[] chunk);
		public abstract void WritePipeChunkTrailer<T>(T[] chunk);
		public abstract void WritePipeEnd();
		public abstract void WriteUnion<T>(T struc)
			where T : IRpcFixedStruct;
		public abstract void WriteFixedStruct<T>(T struc, NdrAlignment alignment)
			where T : IRpcFixedStruct;
		public abstract void WriteConformantStruct<T>(T struc, NdrAlignment alignment)
			where T : IRpcConformantStruct;
		public abstract void WriteConformantStructHeader<T>(T struc)
			where T : IRpcConformantStruct;
		public abstract void WriteStructDeferral<T>(T struc)
			where T : IRpcStruct;

		public abstract void WriteUniqueReferentId(bool isNull);

		public abstract void WriteCharString(string str);
		public abstract void WriteWideCharString(string str);



		// [MS-RPC3] § 2.2.6 - Type Serialization Version 1
		public void SerializeType1(Action<RpcEncoder> encodeFunc)
		{
			var writer = this._writer;

			writer.Align(8);
			writer.WritePduStruct(new CommonTypeHeader
			{
				version = 1,
				endianness = 0x10,  // LE
				commonHeaderLength = 8,
				filler = 0xCCCCCCCC
			});

			int offPrivateHeader = writer.Position;
			writer.WritePduStruct(new PrivateHeader
			{
				objectBufferLength = 0,
				filler = 0
			});

			int offObject = writer.Position;
			encodeFunc(this);
			this.Align(NdrAlignment._8Byte);

			int offEnd = writer.Position;
			writer.SetPosition(offPrivateHeader);
			writer.WriteInt32LE(offEnd - offObject);
			writer.SetPosition(offEnd);
		}

		// [MS-RPC3] § 2.2.6 - Type Serialization Version 1
		public void WriteSer1CommonHeader()
		{
			const int LittleEndian = 0x10;

			ByteWriter writer = this._writer;

			writer.Align(8);

			// [MS-RPCE] § 2.2.61 - Common Type Header for Serialization Stream
			writer.WriteByte(1);    // Version
			writer.WriteByte(LittleEndian);
			writer.WriteUInt16LE(8);    // Header length
			writer.WriteUInt32LE(0xCCCCCCCC);

			//privateHeader = default;
			//if (isConstructed)
			//{
			//	privateHeader = writer.Consume(8);
			//}

			//offContent = writer.Position;
		}

		//// [MS-RPCE] § 2.2.61 - Common Type Header for Serialization Stream
		public void SerializeFixedStruct<TStruct>(ref TStruct struc, NdrAlignment alignment)
			where TStruct : IRpcFixedStruct
		{
			var writer = this._writer;
			writer.Align(8);
			var header = writer.Consume(8);

			int offContent = writer.Position;

			this.WriteFixedStruct(struc, alignment);
			this.WriteStructDeferral(struc);
			writer.Align(8);

			int offEnd = writer.Position;
			int cbContent = offEnd - offContent;
			Debug.Assert(0 == (cbContent % 8));

			BinaryPrimitives.WriteInt32LittleEndian(header, cbContent);
		}
		//// [MS-RPCE] § 2.2.61 - Common Type Header for Serialization Stream
		public void SerializeConformantStruct<TStruct>(ref TStruct struc, NdrAlignment alignment)
			where TStruct : IRpcConformantStruct
		{
			var writer = this._writer;
			writer.Align(8);
			var header = writer.Consume(8);

			int offContent = writer.Position;

			this.WriteConformantStruct(struc, alignment);
			this.WriteStructDeferral(struc);
			writer.Align(8);

			int offEnd = writer.Position;
			int cbContent = offEnd - offContent;
			Debug.Assert(0 == (cbContent % 8));

			BinaryPrimitives.WriteInt32LittleEndian(header, cbContent);
		}
	}
}