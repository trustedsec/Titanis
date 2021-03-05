using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.DceRpc.Client;
using Titanis.IO;

namespace Titanis.DceRpc.WireProtocol
{
	static class RpcPduWriter
	{
		internal static ByteWriter Create()
		{
			var writer = new ByteWriter(PduHeader.PduStructSize + 0x10);
			writer.Advance(PduHeader.PduStructSize);
			return writer;
		}

		internal static void WriteAuth3(
			this ByteWriter writer,
			uint randomPad
			)
		{
			writer.WriteUInt32LE(randomPad);
		}

		internal static unsafe void WriteRequestPduHeader(this ByteWriter writer, RequestPduHeader header)
		{
			fixed (byte* pByte = writer.Consume(RequestPduHeader.StructSize))
			{
				RequestPduHeader* pStruc = (RequestPduHeader*)(pByte);
				*pStruc = header;
			}
		}

		public static unsafe void WritePduHeader(this ByteWriter writer, in PduHeader hdr)
		{
			fixed (byte* pByte = writer.Consume(PduHeader.PduStructSize))
			{
				PduHeader* pStruc = (PduHeader*)(pByte);
				*pStruc = hdr;
			}
		}

		internal static void WriteBindNak(this ByteWriter writer, BindNakPdu bindnak)
		{
			writer.WriteBindNakHeader(bindnak.header);
		}
		internal static unsafe void WriteBindNakHeader(this ByteWriter writer, BindNakPduHeader header)
		{
			fixed (byte* pByte = writer.Consume(BindNakPduHeader.StructSize))
			{
				BindNakPduHeader* pStruc = (BindNakPduHeader*)(pByte);
				*pStruc = header;
			}
		}

		public static void WriteBindAck(this ByteWriter writer, BindAckPdu bindack)
		{
			Debug.Assert(bindack != null);

			writer.WriteBindAckHeader(bindack.header);
			writer.WritePortAny(bindack.secondaryAddress);

			writer.Align(4);
			writer.WriteContextResults(bindack.contextResults);

			// TODO: Write auth token
		}

		private static void WriteContextResults(this ByteWriter writer, PresContextResult[] contextResults)
		{
			if (contextResults == null || contextResults.Length == 0)
			{
				ContextResultListHeader hdr = new ContextResultListHeader();
				writer.WriteContextResultListHeader(hdr);
			}
			else
			{
				ContextResultListHeader hdr = new ContextResultListHeader { n_results = (byte)contextResults.Length };
				writer.WriteContextResultListHeader(hdr);
				foreach (var result in contextResults)
				{
					writer.WriteContextResult(result);
				}
			}
		}

		private static unsafe void WriteContextResult(this ByteWriter writer, in PresContextResult result)
		{
			fixed (byte* pByte = writer.Consume(PresContextResult.StructSize))
			{
				PresContextResult* pStruc = (PresContextResult*)(pByte);
				*pStruc = result;
			}
		}

		private static unsafe void WriteContextResultListHeader(this ByteWriter writer, ContextResultListHeader hdr)
		{
			fixed (byte* pByte = writer.Consume(ContextResultListHeader.StructSize))
			{
				ContextResultListHeader* pStruc = (ContextResultListHeader*)(pByte);
				*pStruc = hdr;
			}
		}
		private static unsafe void WriteBindAckHeader(this ByteWriter writer, BindAckPduHeader header)
		{
			fixed (byte* pByte = writer.Consume(BindAckPduHeader.StructSize))
			{
				BindAckPduHeader* pStruc = (BindAckPduHeader*)(pByte);
				*pStruc = header;
			}
		}
		private static unsafe void WritePortAny(this ByteWriter writer, PortAny port)
		{
			if (port.spec == null)
			{
				PortAnyHeader hdr = new PortAnyHeader();
				writer.WritePortAnyHeader(hdr);
			}
			else
			{
				// TODO: Ensure spec data fits within ushort
				PortAnyHeader hdr = new PortAnyHeader() { length = (ushort)port.spec.Length };
				writer.WritePortAnyHeader(hdr);
				writer.WriteBytes(port.spec);
			}
		}

		private static unsafe void WritePortAnyHeader(this ByteWriter writer, PortAnyHeader hdr)
		{
			fixed (byte* pByte = writer.Consume(PortAnyHeader.StructSize))
			{
				PortAnyHeader* pStruc = (PortAnyHeader*)(pByte);
				*pStruc = hdr;
			}
		}

		internal static void WriteFloor(SyntaxId syntaxId)
		{

		}

		internal static void WriteAuthVerifier(this ByteWriter writer, AuthVerifierHeader hdr, ReadOnlySpan<byte> token)
		{
			writer.WritePduStruct(hdr);
			writer.WriteBytes(token);
		}

		// [MS-RPCE] § 2.2.2.13 - Verification Trailer
		// [MS-RPCE] § 2.2.2.13.1 - rpc_sec_verification_trailer
		internal static unsafe void WriteVerifyTrailerSig(this ByteWriter writer)
		{
			writer.Align(VerifyTrailerSignature.Alignment);
			writer.WriteUInt64LE(VerifyTrailerSignature.ValidSignature);
		}

		internal static unsafe void WriteVerifyTrailerPContext(
			this ByteWriter writer,
			in VerifyTrailerPContext trailer)
		{
			fixed (byte* pBuf = writer.Consume(VerifyTrailerPContext.StructSize))
			{
				*(VerifyTrailerPContext*)pBuf = trailer;
			}
		}
	}
}
