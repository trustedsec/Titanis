using System;
using System.Text;
using System.Threading;
using Titanis.IO;

namespace Titanis.DceRpc
{
	internal abstract class NdrEncoderBase : RpcEncoder
	{
		internal NdrEncoderBase(ByteWriter writer, RpcCallContext callContext)
			: base(writer, callContext)
		{

		}

		// [C706] § 14.3.11 - Top-level Pointers
		public virtual int NativeAlignment => 4;
		public sealed override void Align(NdrAlignment alignment)
		{
			if (alignment != NdrAlignment.None)
			{
				if (alignment == NdrAlignment.NativePtr)
				{
					this._writer.Align(this.NativeAlignment);
				}
				else
				{
					this._writer.Align((int)alignment);
				}
			}
		}
		// [C706] § 14.2.3 - Booleans
		public sealed override void WriteValue(bool v)
		{
			this._writer.WriteByte(v ? (byte)1 : (byte)0);
		}
		// [C706] § 14.2.4 - Characters
		public sealed override void WriteValue(byte v)
		{
			this._writer.WriteByte(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (small)
		public sealed override void WriteValue(sbyte v)
		{
			this._writer.WriteSByte(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (short)
		public sealed override void WriteValue(short v)
		{
			this._writer.Align(2);
			this._writer.WriteInt16LE(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (short)
		public sealed override void WriteValue(ushort v)
		{
			this._writer.Align(2);
			this._writer.WriteUInt16LE(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override void WriteValue(int v)
		{
			this._writer.Align(4);
			this._writer.WriteInt32LE(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (long)
		public sealed override void WriteValue(uint v)
		{
			this._writer.Align(4);
			this._writer.WriteUInt32LE(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (hyper)
		public sealed override void WriteValue(long v)
		{
			this._writer.Align(8);
			this._writer.WriteInt64LE(v);
		}
		// [C706] § 14.2.5 - Integers and Enumerated Types (hyper)
		public sealed override void WriteValue(ulong v)
		{
			this._writer.Align(8);
			this._writer.WriteUInt64LE(v);
		}
		// [C706] § 14.2.6.1 - Floating-point Numbers (IEEE)
		public sealed override void WriteValue(float v)
		{
			this._writer.Align(4);
			this._writer.WriteSingle(v);
		}
		// [C706] § 14.2.6.1 - Floating-point Numbers (IEEE)
		public sealed override void WriteValue(double v)
		{
			this._writer.Align(8);
			this._writer.WriteDouble(v);
		}

		public unsafe sealed override void WriteContextHandle(RpcContextHandle hctx)
		{
			this._writer.Align(4);
			fixed (byte* pData = this._writer.Consume(NdrContextHandle.StructSize))
			{
				*(NdrContextHandle*)pData = hctx.contextId;
			}
		}
		public unsafe sealed override void WriteValue(Guid guid)
		{
			this._writer.Align(4);
			fixed (byte* pData = this._writer.Consume(sizeof(Guid)))
			{
				*(Guid*)pData = guid;
			}
		}
		public unsafe sealed override void WriteValue(RpcInterfaceId id)
		{
			this._writer.WriteSyntaxId(id.syntaxId);
		}

		#region Arrays
		protected abstract void WriteArrayDimension(int value);
		// [C706] § 14.3.3.2 - Uni-dimensional Conformant Arrays
		public sealed override void WriteArrayHeader<T>(T[] array)
		{
			this.WriteArrayDimension(array.Length);
		}

		// [C706] § 14.3.3.3 - Uni-dimensional Varying Arrays
		// [C706] § 14.3.3.4 - Uni-dimensional Conformant-varying Arrays
		public sealed override void WriteArrayHeader<T>(ArraySegment<T> array, bool conformant)
		{
			if (conformant)
			{
				this.WriteArrayDimension(array.Array.Length);
			}
			this.WriteArrayDimension(array.Offset);
			this.WriteArrayDimension(array.Count);
		}
		#endregion
		#region Pipe
		public override void WritePipeChunkHeader<T>(T[] chunk)
		{
			ArgumentNullException.ThrowIfNull(chunk);
			this.WriteArrayDimension(chunk.Length);
		}

		public override void WritePipeChunkTrailer<T>(T[] chunk)
		{
			// Do nothing
		}

		public override void WritePipeEnd()
		{
			this.WriteArrayDimension(0);
		}
		#endregion
		//public override void WriteValue(IntPtr v)
		//{
		//	// IntPtr will throw if the value is out of range
		//	this.WriteValue(v.ToInt32());
		//}
		//public override void WriteValue(UIntPtr v)
		//{
		//	this.WriteValue(v.ToUInt32());
		//}

		// [MS-RPCE] § 2.2.4.1.1 - wchar_t
		public sealed override void WriteValue(char v)
		{
			this.WriteValue((ushort)v);
		}

		// [C706] § 14.3.4 - Strings
		protected void WriteString(string str, Encoding encoding)
		{
			if (str == null)
			{
				this.WriteArrayDimension(0);
			}
			else
			{
				str += '\0';
				this.WriteArrayDimension(str.Length);
				this.WriteArrayDimension(0);
				this.WriteArrayDimension(str.Length);
				byte[] bytes = encoding.GetBytes(str);
				this._writer.WriteBytes(bytes);
			}
		}

		public sealed override void WriteCharString(string str)
		{
			this.WriteString(str, Encoding.UTF8);
		}

		public sealed override void WriteWideCharString(string str)
		{
			this.WriteString(str, Encoding.Unicode);
		}

		// [C706] § 14.3.10 - Pointers
		protected abstract void WriteReferentId(long refId);
		// [C706] § 14.3.10 - Pointers
		public sealed override void WritePointer<T>(RpcPointer<T>? ptr)
		{
			var refId = this.callContext.GetReferentIdFor(ptr);
			this.WriteReferentId(refId);
		}

		public sealed override void WriteInterfacePointer<T>(TypedObjref<T>? objref)
		{
			// TODO: Check referent table
			this.WriteUniqueReferentId(objref == null);
		}

		public sealed override void WriteInterfacePointerBody<T>(TypedObjref<T>? objref)
		{
			if (objref != null)
			{
				var dcom = this.callContext.dcom;
				if (dcom == null)
					throw new InvalidOperationException("The stream contains a marshaled OBJREF, but DCOM is not initialized.");

				dcom.EncodeObjref<T>(this, objref);
			}
		}

		// [C706] § 14.3.10 - Pointers
		public sealed override void WriteUniqueReferentId(bool isNull)
		{
			this.WriteReferentId(isNull ? 0 : this.callContext.GetUniqueReferentId());
		}

		protected abstract void WriteStructBody<T>(ref T struc, NdrAlignment alignment)
			where T : IRpcStruct;

		// [C706] § 14.3.7.1 - Structures Containing a Conformant Array
		public sealed override void WriteConformantStructHeader<T>(T struc)
		{
			this.Align(NdrAlignment.NativePtr);
			struc.EncodeHeader(this);
		}
		// [C706] § 14.3.6 - Structures
		public sealed override void WriteStructDeferral<T>(T struc)
		{
			struc.EncodeDeferrals(this);
		}
		// [C706] § 14.3.8 - Unions
		public sealed override void WriteUnion<T>(T struc)
		{
			struc.Encode(this);
		}

		// [C706] § 14.3.6 - Structures
		public sealed override void WriteFixedStruct<T>(T struc, NdrAlignment alignment)
		{
			this.WriteStructBody(ref struc, alignment);
		}

		// [C706] § 14.3.7.1 - Structures Containing a Conformant Array
		public sealed override void WriteConformantStruct<T>(T struc, NdrAlignment alignment)
		{
			struc.EncodeHeader(this);
			this.WriteStructBody(ref struc, alignment);
			struc.EncodeConformantArrayField(this);
		}
	}

	//internal sealed class NdrEncoder : NdrEncoderBase
	//{
	//	internal NdrEncoder(ByteWriter writer, RpcCallContext callContext)
	//		: base(writer, callContext)
	//	{

	//	}
	//}

	internal sealed class MsrpcNdrEncoder : NdrEncoderBase
	{
		internal MsrpcNdrEncoder(ByteWriter writer, RpcCallContext callContext)
			: base(writer, callContext)
		{
		}

		// [C706] § 14.3.3.2 - Uni-dimensional Conformant Arrays
		// [C706] § 14.3.3.3 - Uni-dimensional Varying Arrays
		// [C706] § 14.3.3.4 - Uni-dimensional Conformant-varying Arrays
		protected sealed override void WriteArrayDimension(int value)
		{
			this.WriteValue(value);
		}

		// [C706] § 14.3.10 - Pointers
		protected sealed override void WriteReferentId(long refId)
		{
			this.WriteValue((int)refId);
		}

		// [C706] § 14.3.6 - Structures
		protected sealed override void WriteStructBody<T>(ref T struc, NdrAlignment alignment)
		{
			this.Align(alignment);
			struc.Encode(this);
		}
	}

	internal sealed class MsrpcNdr64Encoder : NdrEncoderBase
	{
		internal MsrpcNdr64Encoder(ByteWriter writer, RpcCallContext callContext)
			: base(writer, callContext)
		{

		}

		// [MS-RPCE] § 2.2.5.3.2.1 - Conformant Arrays
		// [MS-RPCE] § 2.2.5.3.2.2 - Varying Arrays
		// [MS-RPCE] § 2.2.5.3.2.3 - Conformant Varying Arrays
		protected sealed override void WriteArrayDimension(int value)
		{
			this.WriteValue((long)value);
		}

		// [MS-RPCE] § 2.2.5.3.5 - Pointers
		public sealed override int NativeAlignment => 8;

		// [C706] § 14.3.6 - Structures
		// [MS-RPCE] § 2.2.5.3.4.1 - Structure with Trailing Gap
		// [MS-RPCE] § 2.2.5.3.4.3 - Structure Containing a Conformant Varying Array
		protected sealed override void WriteStructBody<T>(ref T struc, NdrAlignment alignment)
		{
			this.Align(alignment);
			struc.Encode(this);
			this.Align(alignment);
		}

		// [MS-RPCE] § 2.2.5.3.5 - Pointers
		protected sealed override void WriteReferentId(long refId)
		{
			this.WriteValue(refId);
		}
	}
}