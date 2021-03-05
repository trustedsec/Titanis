using System.Runtime.InteropServices;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	static class Smb2Writer
	{
		internal static unsafe int Alloc(this ByteWriter writer, int cb)
		{
			int pos = writer.Position;
			writer.Consume(cb);
			return pos;
		}

		// [MS-SMB2] § 2.1 - Transport
		internal static unsafe int AllocDSHeader(this ByteWriter writer)
			=> writer.Alloc(sizeof(int));

		internal static unsafe void Write(this ByteWriter writer, in Smb2PduSyncHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2PduSyncHeader.StructSize))
			{
				*(Smb2PduSyncHeader*)pStruc = hdr;
			}
		}
		internal static unsafe void Write(this ByteWriter writer, in Smb2PduAsyncHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2PduAsyncHeader.StructSize))
			{
				*(Smb2PduAsyncHeader*)pStruc = hdr;
			}
		}

		internal static unsafe void Write(this ByteWriter writer, in Smb2NegotiateRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2NegotiateRequestBody.StructSize))
			{
				*(Smb2NegotiateRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe int AllocNegReqHdr(this ByteWriter writer)
			=> writer.Alloc(Smb2NegotiateRequestBody.StructSize);

		internal static unsafe void WriteNegReqHdr(this ByteWriter writer, in Smb2NegotiateRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2NegotiateRequestBody.StructSize))
			{
				*(Smb2NegotiateRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe int AllocNegContextHdr(this ByteWriter writer)
			=> writer.Alloc(Smb2NegotiateContextHeader.StructSize);

		internal static unsafe void WriteNegContextHdr(this ByteWriter writer, in Smb2NegotiateContextHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2NegotiateContextHeader.StructSize))
			{
				*(Smb2NegotiateContextHeader*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteErrorRespHdr(this ByteWriter writer, in Smb2ErrorResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2ErrorResponseBody.StructSize))
			{
				*(Smb2ErrorResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteSessionReqHdr(this ByteWriter writer, in Smb2SessionRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2SessionRequestBody.StructSize))
			{
				*(Smb2SessionRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteSessionRespHdr(this ByteWriter writer, in Smb2SessionResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2SessionResponseBody.StructSize))
			{
				*(Smb2SessionResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteLogoffReqHdr(this ByteWriter writer, in Smb2LogoffRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2LogoffRequestBody.StructSize))
			{
				*(Smb2LogoffRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteLogoffRespHdr(this ByteWriter writer, in Smb2LogoffResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2LogoffResponseBody.StructSize))
			{
				*(Smb2LogoffResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteTreeConnectReqBody(this ByteWriter writer, in Smb2TreeConnectRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2TreeConnectRequestBody.StructSize))
			{
				*(Smb2TreeConnectRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteTreeConnectRespHdr(this ByteWriter writer, in Smb2TreeConnectResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2TreeConnectResponseBody.StructSize))
			{
				*(Smb2TreeConnectResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteCreateReqHdr(this ByteWriter writer, in Smb2CreateRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2CreateRequestBody.StructSize))
			{
				ref Smb2CreateRequestBody pHdr = ref *(Smb2CreateRequestBody*)pStruc;
				pHdr = hdr;
			}
		}

		internal static unsafe void WriteCreateRespHdr(this ByteWriter writer, in Smb2CreateResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2CreateResponseBody.StructSize))
			{
				*(Smb2CreateResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteIoctlReqHdr(this ByteWriter writer, in Smb2IoctlRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2IoctlRequestBody.StructSize))
			{
				*(Smb2IoctlRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteIoctlRespHdr(this ByteWriter writer, in Smb2IoctlResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2IoctlResponseBody.StructSize))
			{
				*(Smb2IoctlResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteFlushReqHdr(this ByteWriter writer, in Smb2FlushRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2FlushRequestBody.StructSize))
			{
				*(Smb2FlushRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteFlushRespHdr(this ByteWriter writer, in Smb2FlushResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2FlushResponseBody.StructSize))
			{
				*(Smb2FlushResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteChangeNotifyReqHdr(this ByteWriter writer, in Smb2ChangeNotifyRequestHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2ChangeNotifyRequestHeader.StructSize))
			{
				*(Smb2ChangeNotifyRequestHeader*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteFileNotifyInfoReqHdr(this ByteWriter writer, in FileNotifyInfoHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(FileNotifyInfoHeader.StructSize))
			{
				*(FileNotifyInfoHeader*)pStruc = hdr;
			}
		}

		internal static unsafe ref Smb2ChangeNotifyResponseHeader WriteChangeNotifyRespHdr(this ByteWriter writer, in Smb2ChangeNotifyResponseHeader hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2ChangeNotifyResponseHeader.StructSize))
			{
				ref Smb2ChangeNotifyResponseHeader pHdr = ref *(Smb2ChangeNotifyResponseHeader*)pStruc;
				pHdr = hdr;
				return ref pHdr;
			}
		}

		internal static unsafe void WriteQueryDirReqHdr(this ByteWriter writer, in Smb2QueryDirRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2QueryDirRequestBody.StructSize))
			{
				*(Smb2QueryDirRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteQueryDirRespHdr(this ByteWriter writer, in Smb2QueryDirResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2QueryDirResponseBody.StructSize))
			{
				*(Smb2QueryDirResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteEchoReqHdr(this ByteWriter writer, in Smb2EchoRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2EchoRequestBody.StructSize))
			{
				*(Smb2EchoRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteEchoRespHdr(this ByteWriter writer, in Smb2EchoResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2EchoResponseBody.StructSize))
			{
				*(Smb2EchoResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteCloseReqHdr(this ByteWriter writer, in Smb2CloseRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2CloseRequestBody.StructSize))
			{
				*(Smb2CloseRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteCloseRespHdr(this ByteWriter writer, in Smb2CloseResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2CloseResponseBody.StructSize))
			{
				*(Smb2CloseResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteReadReqHdr(this ByteWriter writer, in Smb2ReadRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2ReadRequestBody.StructSize))
			{
				*(Smb2ReadRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteReadRespHdr(this ByteWriter writer, in Smb2ReadResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2ReadResponseBody.StructSize))
			{
				*(Smb2ReadResponseBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteWriteReqHdr(this ByteWriter writer, in Smb2WriteRequestBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2WriteRequestBody.StructSize))
			{
				*(Smb2WriteRequestBody*)pStruc = hdr;
			}
		}

		internal static unsafe void WriteWriteRespHdr(this ByteWriter writer, in Smb2WriteResponseBody hdr)
		{
			fixed (byte* pStruc = writer.Consume(Smb2WriteResponseBody.StructSize))
			{
				*(Smb2WriteResponseBody*)pStruc = hdr;
			}
		}

		internal static void Write(this ByteWriter writer, in Smb2OplockBreakBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2OplockBreakBody>(writer.Consume(Smb2OplockBreakBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2LeaseBreakBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2LeaseBreakBody>(writer.Consume(Smb2LeaseBreakBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2LeaseBreakAckBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2LeaseBreakAckBody>(writer.Consume(Smb2LeaseBreakAckBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2OplockBreakAckHeader hdr)
			=> MemoryMarshal.Cast<byte, Smb2OplockBreakAckHeader>(writer.Consume(Smb2OplockBreakAckHeader.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2SetInfoRequestBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2SetInfoRequestBody>(writer.Consume(Smb2SetInfoRequestBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2CancelRequestBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2CancelRequestBody>(writer.Consume(Smb2CancelRequestBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in ValidateNegotiateInfoFixed hdr)
			=> MemoryMarshal.Cast<byte, ValidateNegotiateInfoFixed>(writer.Consume(ValidateNegotiateInfoFixed.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2SetInfoResponseBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2SetInfoResponseBody>(writer.Consume(Smb2SetInfoResponseBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2QueryInfoRequestBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2QueryInfoRequestBody>(writer.Consume(Smb2QueryInfoRequestBody.StructSize))[0] = hdr;

		internal static void Write(this ByteWriter writer, in Smb2QueryInfoResponseBody hdr)
			=> MemoryMarshal.Cast<byte, Smb2QueryInfoResponseBody>(writer.Consume(Smb2QueryInfoResponseBody.StructSize))[0] = hdr;
	}
}
