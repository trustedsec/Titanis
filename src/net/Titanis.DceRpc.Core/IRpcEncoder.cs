using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Provides functionality to encode RPC stub data.
	/// </summary>
	public interface IRpcEncoder
	{
		ByteWriter GetWriter();

		void Align(NdrAlignment alignment);
		void WriteValue(bool v);
		void WriteValue(sbyte v);
		void WriteValue(byte v);
		void WriteValue(short v);
		void WriteValue(ushort v);
		void WriteValue(int v);
		void WriteValue(uint v);
		void WriteValue(long v);
		void WriteValue(ulong v);
		void WriteValue(IntPtr n);
		void WriteValue(UIntPtr n);
		void WriteValue(float v);
		void WriteValue(double v);
		void WriteValue(char v);
		void WriteValue(Guid v);
		//void WriteValue(Tower2 v);
		void WriteValue(RpcInterfaceId v);

		void WriteContextHandle(RpcContextHandle hctx);
		void WritePointer<T>(RpcPointer<T>? ptr);
		void WriteInterfacePointer<T>(TypedObjref<T>? ptr) where T : class, IRpcObject;
		void WriteInterfacePointerBody<T>(TypedObjref<T>? ptr) where T : class, IRpcObject;
		void WriteArrayHeader<T>(T[] array);
		void WriteArrayHeader<T>(ArraySegment<T> array, bool conformant);
		void WritePipeChunkHeader<T>(T[] chunk);
		void WritePipeChunkTrailer<T>(T[] chunk);
		void WritePipeEnd();
		void WriteUnion<T>(T struc)
			where T : IRpcFixedStruct;
		void WriteFixedStruct<T>(T struc, NdrAlignment alignment)
			where T : IRpcFixedStruct;
		void WriteConformantStruct<T>(T struc, NdrAlignment alignment)
			where T : IRpcConformantStruct;
		void WriteConformantStructHeader<T>(T struc)
			where T : IRpcConformantStruct;
		void WriteStructDeferral<T>(T struc)
			where T : IRpcStruct;

		public void WriteUniqueReferentId(bool isNull);

		void WriteCharString(string str);
		void WriteWideCharString(string str);
	}

	public static class RpcEncoderExtensions
	{
		public static void WriteUnsignedCharString(this IRpcEncoder encoder, string str)
			=> encoder.WriteCharString(str);
	}
}
