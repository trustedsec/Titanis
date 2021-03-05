using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Provides functionality to decode RPC stub data.
	/// </summary>
	public interface IRpcDecoder
	{
		ByteMemoryReader GetStubData();

		void Align(NdrAlignment alignment);
		bool ReadBoolean();
		sbyte ReadSByte();
		byte ReadByte();
		short ReadInt16();
		ushort ReadUInt16();
		int ReadInt32();
		uint ReadUInt32();
		long ReadInt64();
		ulong ReadUInt64();
		IntPtr ReadInt3264();
		UIntPtr ReadUInt3264();
		float ReadFloat();
		double ReadDouble();
		byte ReadChar();
		char ReadWideChar();
		Guid ReadUuid();
		//Tower ReadTower();
		RpcInterfaceId ReadRpcInterfaceId();

		/// <summary>
		/// Reads a pointer referent ID.
		/// </summary>
		/// <returns>The referent ID</returns>
		/// <remarks>
		/// This is typed as a <see cref="long"/> to accommodate NDR64.
		/// </remarks>
		// [C706] § 14.3.10
		long ReadReferentId();
		[Obsolete("Use either ReadUniquePointer or ReadFullPointer")]
		RpcPointer<T>? ReadPointer<T>() => this.ReadFullPointer<T>();
		RpcPointer<T> ReadUniquePointer<T>();
		RpcPointer<T>? ReadFullPointer<T>();
		/// <summary>
		/// Decodes an interface pointer.
		/// </summary>
		/// <typeparam name="T">Interface type</typeparam>
		/// <returns>A <see cref="TypedObjref{T}"/> representing the pointer, or <see langword="null"/> if the decoded pointer is null.</returns>
		/// <remarks>
		/// This method reads the pointer itself, not the contents.
		/// Call <see cref="ReadInterfacePointer{T}(TypedObjref{T}?)"/> to read the marshal data for the pointer.
		/// </remarks>
		TypedObjref<T>? ReadInterfacePointer<T>() where T : class, IRpcObject;
		/// <summary>
		/// Decodes the marshal data for an interface pointer.
		/// </summary>
		/// <typeparam name="T">Interface type</typeparam>
		/// <param name="objref"><see cref="TypedObjref{T}"/> read by <see cref="ReadInterfacePointer{T}()"/></param>
		/// <remarks>
		/// This method reads the referent of a pointer read by <see cref="ReadInterfacePointer{T}()"/>.
		/// Passing <see langword="null"/> for <paramref name="objref"/> is not an error,
		/// and results in no data being read.
		/// </remarks>
		void ReadInterfacePointer<T>(TypedObjref<T>? objref) where T : class, IRpcObject;
		/// <summary>
		/// Reads a parameter as an <see langword="out"/> parameter.
		/// </summary>
		/// <param name="ptr">Pointer argument</param>
		/// <returns>A <see cref="RpcPointer{T}"/></returns>
		/// <remarks>
		/// When a method accepts an <see langword="out"/>-only parameter,
		/// the caller may pass <see langword="out"/> to discard the result, which
		/// would cause deserialization of the referent to fail.  This method gracefully
		/// replaces a missing pointer so that the referent may be deserialized,
		/// even if it is then discarded.
		/// </remarks>
		RpcPointer<T> ReadOutPointer<T>(RpcPointer<T> ptr) => this.ReadOutFullPointer(ptr);
		RpcPointer<T> ReadOutUniquePointer<T>(RpcPointer<T> ptr);
		RpcPointer<T> ReadOutFullPointer<T>(RpcPointer<T> ptr);
		RpcContextHandle ReadContextHandle();
		T[] ReadArrayHeader<T>();
		ArraySegment<T> ReadArraySegmentHeader<T>();
		ArraySegment<T> ReadArraySegmentHeader<T>(int fixedSize);
		RpcPipe<T> ReadPipeHeader<T>();
		T[]? ReadPipeChunkHeader<T>();
		void ReadPipeChunkTrailer<T>(T[] chunk);
		T ReadUnion<T>() where T : IRpcFixedStruct, new();
		T ReadFixedStruct<T>(NdrAlignment alignment) where T : IRpcFixedStruct, new();
		T ReadConformantStruct<T>(NdrAlignment alignment) where T : IRpcConformantStruct, new();
		T ReadConformantStructBody<T>(ref T struc, NdrAlignment alignment) where T : IRpcConformantStruct, new();
		T ReadConformantStructHeader<T>() where T : IRpcConformantStruct, new();
		void ReadStructDeferral<T>(ref T struc) where T : IRpcStruct;

		string ReadCharString();
		string ReadWideCharString();
	}

	/// <summary>
	/// Provides extension methods for <see cref="IRpcDecoder"/>.
	/// </summary>
	public static class RpcDecoderExtensions
	{
		public static string ReadUnsignedCharString(this IRpcDecoder decoder)
			=> decoder.ReadCharString();
		public static byte ReadUnsignedChar(this IRpcDecoder decoder)
			=> decoder.ReadChar();
	}
}
