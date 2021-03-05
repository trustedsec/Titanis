using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.IO;

namespace Titanis.DceRpc
{
	public abstract class RpcDecoder : IRpcDecoder
	{
		internal RpcDecoder(RpcCallContext callContext)
		{
			this._callContext = callContext;
		}

		internal readonly RpcCallContext _callContext;

		public abstract ByteMemoryReader GetStubData();

		public abstract void Align(NdrAlignment alignment);
		public abstract bool ReadBoolean();
		public abstract sbyte ReadSByte();
		public abstract byte ReadByte();
		public abstract short ReadInt16();
		public abstract ushort ReadUInt16();
		public abstract int ReadInt32();
		public abstract uint ReadUInt32();
		public abstract long ReadInt64();
		public abstract ulong ReadUInt64();
		public IntPtr ReadInt3264() => new IntPtr(this.ReadInt32());
		public UIntPtr ReadUInt3264() => new UIntPtr(this.ReadUInt32());
		public abstract float ReadFloat();
		public abstract double ReadDouble();
		public abstract byte ReadChar();
		public abstract char ReadWideChar();
		public abstract Guid ReadUuid();
		public abstract Tower ReadTower();
		public abstract RpcInterfaceId ReadRpcInterfaceId();

		/// <inheritdoc/>
		public abstract long ReadReferentId();
		public abstract RpcPointer<T>? ReadUniquePointer<T>();
		public abstract RpcPointer<T>? ReadFullPointer<T>();
		public abstract TypedObjref<T>? ReadInterfacePointer<T>() where T : class, IRpcObject;
		public abstract void ReadInterfacePointer<T>(TypedObjref<T>? objref) where T : class, IRpcObject;
		public abstract RpcPointer<T> ReadOutFullPointer<T>(RpcPointer<T> ptr);
		public abstract RpcPointer<T> ReadOutUniquePointer<T>(RpcPointer<T> ptr);
		public abstract RpcContextHandle ReadContextHandle();
		public abstract T[] ReadArrayHeader<T>();
		public abstract ArraySegment<T> ReadArraySegmentHeader<T>();
		public abstract ArraySegment<T> ReadArraySegmentHeader<T>(int fixedSize);
		public abstract RpcPipe<T> ReadPipeHeader<T>();
		public abstract T[]? ReadPipeChunkHeader<T>();
		public abstract void ReadPipeChunkTrailer<T>(T[] chunk);
		public abstract T ReadUnion<T>() where T : IRpcFixedStruct, new();
		public abstract T ReadFixedStruct<T>(NdrAlignment alignment) where T : IRpcFixedStruct, new();
		public abstract T ReadConformantStruct<T>(NdrAlignment alignment) where T : IRpcConformantStruct, new();
		public abstract T ReadConformantStructBody<T>(ref T struc, NdrAlignment alignment) where T : IRpcConformantStruct, new();
		public abstract T ReadConformantStructHeader<T>() where T : IRpcConformantStruct, new();
		public abstract void ReadStructDeferral<T>(ref T struc) where T : IRpcStruct;

		public abstract string ReadCharString();
		public abstract string ReadWideCharString();


		#region Serialization

		// [MS-RPC3] § 2.2.6 - Type Serialization Version 1
		public T DeserializeType1<T>(Func<RpcDecoder, T> decodeFunc)
		{
			ArgumentNullException.ThrowIfNull(decodeFunc);

			var reader = this.GetStubData();

			reader.Align(8);
			var commonHeader = reader.ReadPduStruct<CommonTypeHeader>();
			if (
				commonHeader.version != 1
				|| commonHeader.commonHeaderLength != 8
				|| commonHeader.filler != 0xCCCCCCCC
				)
				throw new InvalidDataException("The stream did not contain a valid CommonTypeHeader.");

			if (commonHeader.endianness != 0x10)
				throw new NotSupportedException("The stream contains big-endian data which is not supported by this implementation.");

			int offPrivateHeader = reader.Position;
			var privateHdr = reader.ReadPduStruct<PrivateHeader>();

			int offObject = reader.Position;
			T result = decodeFunc(this);
			this.Align(NdrAlignment._8Byte);

			int offEnd = (int)(offObject + privateHdr.objectBufferLength);
			Debug.Assert(reader.Position <= offEnd);
			reader.Position = offEnd;

			return result;
		}
		#endregion
	}
}