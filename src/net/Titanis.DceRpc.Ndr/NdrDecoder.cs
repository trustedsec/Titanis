using System;
using System.IO;
using System.Text;
using dceidl;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.IO;
using System.Diagnostics;

namespace Titanis.DceRpc
{
	internal abstract class NdrDecoderBase : RpcDecoder
	{
		private ByteMemoryReader stubData;
		private int _readIndex;

		protected NdrDecoderBase(ByteMemoryReader stubData, RpcCallContext callContext)
			: base(callContext)
		{
			if (stubData is null)
				throw new ArgumentNullException(nameof(stubData));

			this.stubData = stubData;
		}

		public override ByteMemoryReader GetStubData() => this.stubData;

		// [C706] § 14.3.11 - Top-level Pointers
		public virtual int NativeAlignment => 4;
		public sealed override void Align(NdrAlignment alignment)
		{
			if (alignment != NdrAlignment.None)
			{
				if (alignment == NdrAlignment.NativePtr)
				{
					this.stubData.Align(this.NativeAlignment);
				}
				else
				{
					this.stubData.Align((int)alignment);
				}
			}
		}
		protected void Align(int align)
		{
			this.stubData.Align(align);
		}

		// [C706] § 14.2.3 - Booleans
		public sealed override bool ReadBoolean()
		{
			return this.ReadByte() != 0;
		}
		// [C706] § 14.2.4 - Characters
		public sealed override sbyte ReadSByte()
		{
			return this.stubData.ReadSByte();
		}
		// [C706] § 14.2.4 - Characters
		public sealed override byte ReadByte()
		{
			return this.stubData.ReadByte();
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (short)
		public sealed override short ReadInt16()
		{
			this.Align(2);
			return this.stubData.ReadInt16();
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (short)
		public sealed override ushort ReadUInt16()
		{
			this.Align(2);
			return this.stubData.ReadUInt16();
		}

		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override int ReadInt32()
		{
			this.Align(4);
			return this.stubData.ReadInt32();
		}

		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override uint ReadUInt32()
		{
			this.Align(4);
			return this.stubData.ReadUInt32();
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override long ReadInt64()
		{
			this.Align(8);
			return this.stubData.ReadInt64();
		}

		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override ulong ReadUInt64()
		{
			this.Align(8);
			return this.stubData.ReadUInt64();
		}

		// [C706] § 14.2.6.1 - Floating-point Numbers (IEEE)
		public sealed override float ReadFloat()
		{
			this.Align(4);
			return this.stubData.ReadSingle();
		}
		// [C706] § 14.2.6.1 - Floating-point Numbers (IEEE)
		public sealed override double ReadDouble()
		{
			this.Align(8);
			return this.stubData.ReadDouble();
		}

		//public override IntPtr ReadInt3264()
		//{
		//	return new IntPtr(this.ReadInt32());
		//}

		//public override UIntPtr ReadUInt3264()
		//{
		//	return new UIntPtr(this.ReadUInt32());
		//}

		// [C706] § 14.2.4 - Characters
		public sealed override byte ReadChar()
		{
			return this.stubData.ReadByte();
		}

		// [MS-RPCE] § 2.2.4.1.1 - wchar_t
		public sealed override char ReadWideChar()
		{
			this.Align(2);
			return (char)this.stubData.ReadUInt16();
		}

		public sealed override RpcPointer<T>? ReadUniquePointer<T>()
		{
			var refId = this.ReadReferentId();
			return refId != 0 ? new RpcPointer<T>() { referentId = refId } : null;
		}

		public sealed override RpcPointer<T>? ReadFullPointer<T>()
		{
			var refId = this.ReadReferentId();
			if (refId != 0)
			{
				RpcPointer<T> ptr = (RpcPointer<T>)this._callContext.Resolve(refId);
				if (ptr == null)
				{
					ptr = new RpcPointer<T>() { referentId = refId };
					this._callContext.AddPointer(refId, ptr);
				}
				return ptr;
			}
			else
			{
				return null;
			}
		}

		public sealed override TypedObjref<T>? ReadInterfacePointer<T>()
		{
			// TODO: Check referent table
			var refId = this.ReadReferentId();
			return (refId == 0) ? null : new TypedObjref<T>();
		}

		public sealed override void ReadInterfacePointer<T>(TypedObjref<T>? objref)
		{
			if (objref != null)
			{
				var dcom = this._callContext.dcom;
				if (dcom == null)
					throw new InvalidOperationException("The stream contains a marshaled OBJREF, but DCOM is not initialized.");

				// [MS-DCOM] § 2.2.16 - PMInterfacePoniter
				if (objref != null)
				{
					var marshalData = dcom.DecodeObjref<T>(this);
					if (marshalData == null)
						throw new InvalidDataException("Unable to read marshal data for the object reference.");
					objref.SetMarshalData(marshalData);
				}
			}
		}

		/// <inheritdoc/>
		public sealed override RpcPointer<T>? ReadOutUniquePointer<T>(RpcPointer<T>? ptr)
		{
			var refId = this.ReadReferentId();
			if (refId != 0)
			{
				if (ptr != null)
				{
					ptr.referentId = refId;
					return ptr;
				}
				else
				{
					return new RpcPointer<T>() { referentId = refId };
				}
			}
			else
			{
				return null;
			}
		}

		/// <inheritdoc/>
		public sealed override RpcPointer<T>? ReadOutFullPointer<T>(RpcPointer<T>? ptr)
		{
			var refId = this.ReadReferentId();
			if (refId != 0)
			{
				if (ptr != null)
				{
					// TODO: Check for existing pointer
					ptr.referentId = refId;
					return ptr;
				}
				else
				{
					return new RpcPointer<T>() { referentId = refId };
				}
			}
			else
			{
				return null;
			}
		}

		#region Arrays
		protected abstract int ReadArrayDimension();

		// [C706] § 14.3.3.2 - Uni-dimensional Conformant Arrays
		public sealed override T[] ReadArrayHeader<T>()
		{
			var count = this.ReadArrayDimension();
			if (count == -1)
				count = 0;

			return (count == 0) ? Array.Empty<T>() : new T[count];
		}
		// [C706] § 14.3.3.4 - Uni-dimensional Conformant-varying Arrays
		public sealed override ArraySegment<T> ReadArraySegmentHeader<T>()
		{
			int maxCount = this.ReadArrayDimension();
			var arr = (maxCount == 0) ? Array.Empty<T>() : new T[maxCount];
			int offset = this.ReadArrayDimension();
			int actualCount = this.ReadArrayDimension();
			return new ArraySegment<T>(arr, offset, actualCount);
		}
		// [C706] § 14.3.3.3 - Uni-dimensional Varying Arrays
		public sealed override ArraySegment<T> ReadArraySegmentHeader<T>(int fixedSize)
		{
			var arr = new T[fixedSize];
			int offset = this.ReadArrayDimension();
			int actualCount = this.ReadArrayDimension();
			return new ArraySegment<T>(arr, offset, actualCount);
		}
		public sealed override RpcPipe<T> ReadPipeHeader<T>()
		{
			return new RpcPipe<T>();
		}

		public sealed override T[]? ReadPipeChunkHeader<T>()
		{
			var size = this.ReadArrayDimension();
			if (size == 0)
			{
				return null;
			}
			else
			{
				var chunk = new T[size];
				return chunk;
			}
		}

		public override void ReadPipeChunkTrailer<T>(T[] chunk)
		{
			// Do nothing
		}
		#endregion

		#region Structs
		protected abstract void ReadStructBody<T>(ref T struc, NdrAlignment alignment)
			where T : IRpcStruct;

		public override T ReadFixedStruct<T>(NdrAlignment alignment)
		{
			T struc = new T();
			this.ReadStructBody(ref struc, alignment);
			return struc;
		}

		public override T ReadConformantStruct<T>(NdrAlignment alignment)
		{
			T struc = new T();
			struc.DecodeHeader(this);
			this.ReadStructBody(ref struc, alignment);
			struc.DecodeConformantArrayField(this);
			return struc;
		}

		public override T ReadConformantStructHeader<T>()
		{
			T struc = new T();
			struc.DecodeHeader(this);
			return struc;
		}

		public override T ReadConformantStructBody<T>(ref T struc, NdrAlignment alignment)
		{
			this.ReadStructBody<T>(ref struc, alignment);
			this.Align(alignment);
			return struc;
		}

		public sealed override void ReadStructDeferral<T>(ref T struc)
		{
			struc.DecodeDeferrals(this);
		}
		#endregion

		public override T ReadUnion<T>()
		{
			T struc = new T();
			struc.Decode(this);
			return struc;
		}

		public unsafe sealed override RpcContextHandle ReadContextHandle()
		{
			this.Align(4);
			fixed (byte* pBuf = this.stubData.Consume(NdrContextHandle.StructSize))
			{
				return new RpcContextHandle(*(NdrContextHandle*)pBuf);
			}
		}

		public unsafe sealed override Guid ReadUuid()
		{
			this.Align(4);
			fixed (byte* pBuf = this.stubData.Consume(sizeof(Guid)))
			{
				return *(Guid*)pBuf;
			}
		}

		public unsafe sealed override Tower ReadTower()
		{
			return new Tower(this.ReadConformantStruct<twr_t>(NdrAlignment._4Byte));
		}

		public unsafe sealed override RpcInterfaceId ReadRpcInterfaceId()
		{
			return new RpcInterfaceId(this.stubData.ReadSyntaxId());
		}

		public sealed override string ReadWideCharString()
		{
			int count = this.ReadArrayDimension();
			int offset = this.ReadArrayDimension();
			int length = this.ReadArrayDimension();
			if (length > 1)
			{
				string str = Encoding.Unicode.GetString(this.stubData.Consume(length * 2).Slice(0, (length - 1) * 2));
				return str;
			}
			else
			{
				if (length == 1)
					// Eat null terminator
					this.stubData.Advance(2);
				return string.Empty;
			}
		}

		public sealed override string ReadCharString()
		{
			int count = this.ReadArrayDimension();
			int offset = this.ReadArrayDimension();
			int length = this.ReadArrayDimension();
			if (length > 1)
			{
				string str = Encoding.Unicode.GetString(this.stubData.Consume(length).Slice(0, length - 1));
				return str;
			}
			else
			{
				if (length == 1)
					// Eat null terminator
					this.stubData.Advance(1);
				return string.Empty;
			}
		}
	}

	//internal sealed class NdrDecoder : NdrDecoderBase
	//{
	//	public NdrDecoder(ByteMemoryReader stubData, RpcCallContext callContext) : base(stubData, callContext)
	//	{
	//	}
	//}
	internal class MsrpcNdrDecoder : NdrDecoderBase
	{
		public MsrpcNdrDecoder(ByteMemoryReader stubData, RpcCallContext callContext) : base(stubData, callContext)
		{
		}

		// [C706] § 14.3.10 - Pointers
		public sealed override long ReadReferentId()
		{
			var id = this.ReadInt32();
			return id;
		}

		// [C706] § 14.3.6 - Structures
		protected sealed override void ReadStructBody<T>(ref T struc, NdrAlignment alignment)
		{
			this.Align(alignment);
			struc.Decode(this);
		}

		// [C706] § 14.3.3.2 - Uni-dimensional Conformant Arrays
		// [C706] § 14.3.3.3 - Uni-dimensional Varying Arrays
		// [C706] § 14.3.3.4 - Uni-dimensional Conformant-varying Arrays
		protected sealed override int ReadArrayDimension()
		{
			return this.ReadInt32();
		}
	}
	internal sealed class MsrpcNdr64Decoder : NdrDecoderBase
	{
		public MsrpcNdr64Decoder(ByteMemoryReader stubData, RpcCallContext callContext) : base(stubData, callContext)
		{
		}
		// [MS-RPCE] § 2.2.5.3.5 - Pointers
		public override int NativeAlignment => 8;

		// [MS-RPCE] § 2.2.5.3.2.1 - Conformant Arrays
		// [MS-RPCE] § 2.2.5.3.2.2 - Varying Arrays
		// [MS-RPCE] § 2.2.5.3.2.3 - Conformant Varying Arrays
		protected sealed override int ReadArrayDimension()
		{
			var dim = this.ReadInt64();
			if (dim > int.MaxValue)
				throw new NotSupportedException("The array dimension exceeds the limit supported by this implementation.");

			return (int)dim;
		}

		// [MS-RPCE] § 2.2.5.3.5 - Pointers
		public override long ReadReferentId()
		{
			return this.ReadInt64();
		}


		// [C706] § 14.3.6 - Structures
		// [MS-RPCE] § 2.2.5.3.4.1 - Structure with Trailing Gap
		// [MS-RPCE] § 2.2.5.3.4.3 - Structure Containing a Conformant Varying Array
		protected sealed override void ReadStructBody<T>(ref T struc, NdrAlignment alignment)
		{
			this.Align(alignment);
			struc.Decode(this);
			this.Align(alignment);
		}

		//public override IntPtr ReadInt3264()
		//{
		//	return new IntPtr(this.ReadInt64());
		//}
		//public override UIntPtr ReadUInt3264()
		//{
		//	return new UIntPtr(this.ReadUInt64());
		//}
	}
}
