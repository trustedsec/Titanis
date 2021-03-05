using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using Titanis.IO;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Base class for RPC encoding implementations.
	/// </summary>
	public abstract class RpcEncoding
	{
		public abstract Guid InterfaceUuid { get; }
		public abstract RpcVersion InterfaceVersion { get; }

		public abstract RpcDecoder CreateDecoder(ByteMemoryReader stubData, RpcCallContext? callContext);
		public abstract RpcEncoder CreateEncoder(ByteWriter writer, RpcCallContext? callContext);

		public RpcEncoder CreateEncoder(RpcCallContext? callContext)
		{
			var writer = new ByteWriter();
			return CreateEncoder(writer, callContext);
		}

		//public static NdrEncoding DcerpcNdr { get; } = new NdrEncoding();
		public static MsrpcNdrEncoding MsrpcNdr { get; } = new MsrpcNdrEncoding();
		public static MsrpcNdr64Encoding MsrpcNdr64 { get; } = new MsrpcNdr64Encoding();

		/// <summary>
		/// Gets the <see cref="RpcEncoding"/> for a specified syntax.
		/// </summary>
		/// <param name="syntaxId">Transfer syntax</param>
		/// <returns>The <see cref="RpcEncoding"/> that matches <paramref name="syntaxId"/></returns>
		/// <exception cref="ArgumentException"></exception>
		public static RpcEncoding GetEncoding(SyntaxId syntaxId)
		{
			//if (syntaxId == NdrEncoding.NdrSyntaxId)
			//{
			//	return DcerpcNdr;
			//}
			if (syntaxId == MsrpcSyntaxId)
			{
				return MsrpcNdr;
			}
			else if (syntaxId == MsrpcNdr64SyntaxId)
			{
				return MsrpcNdr64;
			}
			else
			{
				throw new ArgumentException(Messages.Rpc_UnknownEncoding);
			}
		}

		public static readonly Guid NdrUuid = Guid.Parse("8a885d04-1ceb-11c9-9fe8-08002b104860");
		public static SyntaxId NdrSyntaxId => new SyntaxId(NdrUuid, new RpcVersion(1, 0));
		public static SyntaxId MsrpcSyntaxId => new SyntaxId(NdrUuid, new RpcVersion(2, 0));

		public static readonly Guid Ndr64Uuid = Guid.Parse("71710533-BEBA-4937-8319-B5DBEF9CCC36");
		public static SyntaxId MsrpcNdr64SyntaxId => new SyntaxId(Ndr64Uuid, new RpcVersion(1, 0));
	}

	///// <summary>
	///// Implements NDR encoding as specified in <c>[C706]</c>.
	///// </summary>
	///// <seealso cref="RpcEncoding.DcerpcNdr"/>
	//public sealed class NdrEncoding : RpcEncoding
	//{
	//	public sealed override Guid InterfaceUuid => NdrUuid;
	//	public sealed override RpcVersion InterfaceVersion => new RpcVersion(1, 0);

	//	public sealed override RpcDecoder CreateDecoder(ByteMemoryReader stubData, RpcCallContext callContext)
	//	{
	//		var decoder = new NdrDecoder(stubData, callContext);
	//		return decoder;
	//	}

	//	public sealed override RpcEncoder CreateEncoder(ByteWriter writer, RpcCallContext callContext)
	//	{
	//		var encoder = new NdrEncoder(writer, callContext);
	//		return encoder;
	//	}
	//}

	/// <summary>
	/// Implements MSRPC NDR encoding as specified in <c>[MS-RPCE]</c>.
	/// </summary>
	/// <seealso cref="RpcEncoding.MsrpcNdr"/>
	public sealed class MsrpcNdrEncoding : RpcEncoding
	{
		internal MsrpcNdrEncoding() { }

		public sealed override Guid InterfaceUuid => RpcEncoding.NdrUuid;
		public sealed override RpcVersion InterfaceVersion => new RpcVersion(2, 0);

		public sealed override RpcDecoder CreateDecoder(ByteMemoryReader stubData, RpcCallContext callContext)
		{
			var decoder = new MsrpcNdrDecoder(stubData, callContext);
			return decoder;
		}

		public sealed override RpcEncoder CreateEncoder(ByteWriter writer, RpcCallContext callContext)
		{
			var encoder = new MsrpcNdrEncoder(writer, callContext);
			return encoder;
		}
	}

	/// <summary>
	/// Implements MSRPC NDR64 encoding as specified in <c>[MS-RPCE]</c>.
	/// </summary>
	/// <seealso cref="RpcEncoding.MsrpcNdr"/>
	public sealed class MsrpcNdr64Encoding : RpcEncoding
	{
		internal MsrpcNdr64Encoding() { }

		public sealed override Guid InterfaceUuid => RpcEncoding.NdrUuid;
		public sealed override RpcVersion InterfaceVersion => new RpcVersion(2, 0);

		public sealed override RpcDecoder CreateDecoder(ByteMemoryReader stubData, RpcCallContext callContext)
		{
			var decoder = new MsrpcNdr64Decoder(stubData, callContext);
			return decoder;
		}

		public sealed override RpcEncoder CreateEncoder(ByteWriter writer, RpcCallContext callContext)
		{
			var encoder = new MsrpcNdr64Encoder(writer, callContext);
			return encoder;
		}
	}

	// [MS-RPCE] § 2.2.6.1 - Common Type Header for the Serialization Stream
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct CommonTypeHeader
	{
		internal byte version;
		internal byte endianness;
		internal ushort commonHeaderLength;
		internal uint filler;
	}

	// [MS-RPCE] § 2.2.6.2 -Private Header for Constructed Type
	[PduStruct]
	partial struct PrivateHeader
	{
		internal uint objectBufferLength;
		internal WindowsBuiltInRole filler;
	}
}
