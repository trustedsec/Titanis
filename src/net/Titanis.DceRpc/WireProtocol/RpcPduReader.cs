using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Titanis.IO;

namespace Titanis.DceRpc.WireProtocol
{
	static class RpcPduReader
	{
		internal static unsafe AuthVerifier ReadAuthVerifier(this ByteMemoryReader reader, int authLength)
		{
			return new AuthVerifier(
				reader.ReadPduStruct<AuthVerifierHeader>(),
				reader.ReadBytes(authLength)
			);
		}

		internal static BindNakPdu ReadBindNak(this ByteMemoryReader reader)
		{
			BindNakPdu pdu = new BindNakPdu(reader.ReadBindNakHeader());
			return pdu;
		}

		internal static BindAckPdu ReadBindAck(this ByteMemoryReader reader)
		{
			BindAckPdu pdu = new BindAckPdu(reader.ReadBindAckHeader(), reader.ReadPortAny());
			reader.Align(4);
			ContextResultListHeader resultListHeader = reader.ReadContextResultListHeader();
			int contextCount = resultListHeader.n_results;
			PresContextResult[] contexts = new PresContextResult[contextCount];
			pdu.contextResults = contexts;
			for (int i = 0; i < contextCount; i++)
			{
				contexts[i] = reader.ReadPresContextResult();
			}

			return pdu;
		}

		public static unsafe ContextResultListHeader ReadContextResultListHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(ContextResultListHeader.StructSize))
			{
				ContextResultListHeader* pStruc = (ContextResultListHeader*)(pBuf);
				return *pStruc;
			}
		}

		public static unsafe PortAnyHeader ReadPortAnyHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(PortAnyHeader.StructSize))
			{
				PortAnyHeader* pStruc = (PortAnyHeader*)(pBuf);
				return *pStruc;
			}
		}

		internal static PortAny ReadPortAny(this ByteMemoryReader reader)
		{
			var hdr = reader.ReadPortAnyHeader();
			return new PortAny(reader.ReadBytes(hdr.length));
		}

		internal static RequestPdu ReadRequest(this ByteMemoryReader reader, bool hasObjectId)
		{
			RequestPdu pdu = new RequestPdu(reader.ReadRequestHeader());
			if (hasObjectId)
				pdu.ObjectId = reader.ReadGuid();
			pdu.StubData = reader.Remaining;

			return pdu;
		}

		internal static FaultPdu ReadFaultPdu(this ByteMemoryReader reader)
		{
			FaultPdu pdu = new FaultPdu(reader.ReadFaultHeader());
			pdu.StubData = reader.Remaining;

			return pdu;
		}

		internal static ResponsePdu ReadResponsePdu(this ByteMemoryReader reader)
		{
			ResponsePdu pdu = new ResponsePdu(reader.ReadResponseHeader());
			pdu.StubData = reader.Remaining;

			return pdu;
		}

		public static unsafe Guid ReadGuid(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(sizeof(Guid)))
			{
				Guid* pStruc = (Guid*)(pBuf);
				return *pStruc;
			}
		}

		public static unsafe RequestPduHeader ReadRequestHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(RequestPduHeader.StructSize))
			{
				RequestPduHeader* pStruc = (RequestPduHeader*)(pBuf);
				return *pStruc;
			}
		}

		public static unsafe ResponsePduHeader ReadResponseHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(ResponsePduHeader.StructSize))
			{
				ResponsePduHeader* pStruc = (ResponsePduHeader*)(pBuf);
				return *pStruc;
			}
		}

		public static unsafe FaultPduHeader ReadFaultHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(FaultPduHeader.StructSize))
			{
				FaultPduHeader* pStruc = (FaultPduHeader*)(pBuf);
				return *pStruc;
			}
		}

		public static unsafe PresContextResult ReadPresContextResult(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(PresContextResult.StructSize))
			{
				PresContextResult* pStruc = (PresContextResult*)(pBuf);
				return *pStruc;
			}
		}

		private static unsafe BindAckPduHeader ReadBindAckHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(BindAckPduHeader.StructSize))
			{
				BindAckPduHeader* pStruc = (BindAckPduHeader*)(pBuf);
				return *pStruc;
			}
		}

		private static unsafe BindNakPduHeader ReadBindNakHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(BindNakPduHeader.StructSize))
			{
				BindNakPduHeader* pStruc = (BindNakPduHeader*)(pBuf);
				return *pStruc;
			}
		}

		internal static unsafe ushort ReadFragLength(ReadOnlySpan<byte> buffer)
		{
			if (buffer.Length < PduHeader.PduStructSize)
				return 0;

			fixed (byte* pBuf = buffer)
			{
				PduHeader* pStruc = (PduHeader*)(pBuf);
				return pStruc->fragLength;
			}
		}

		internal static unsafe PduType ReadPduType(ReadOnlySpan<byte> buffer)
		{
			if (buffer.Length < PduHeader.PduStructSize)
				return 0;

			fixed (byte* pBuf = buffer)
			{
				PduHeader* pStruc = (PduHeader*)(pBuf);
				return pStruc->ptype;
			}
		}
	}
}
