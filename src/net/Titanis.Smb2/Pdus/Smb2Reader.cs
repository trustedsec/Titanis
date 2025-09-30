using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	static class Smb2Reader
	{
		internal static ref readonly Smb2PduSyncHeader ReadSmb2PduSyncHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2PduSyncHeader>(reader.Consume(Smb2PduSyncHeader.StructSize))[0];

		internal static ref readonly Smb2NegotiateResponseBody ReadNegRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2NegotiateResponseBody>(reader.Consume(Smb2NegotiateResponseBody.StructSize))[0];

		internal static ref readonly Smb2NegotiateContextHeader ReadNegCtxHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2NegotiateContextHeader>(reader.Consume(Smb2NegotiateContextHeader.StructSize))[0];

		internal static ref readonly Smb2ErrorResponseBody ReadErrorRespBody(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ErrorResponseBody>(reader.Consume(Smb2ErrorResponseBody.StructSize))[0];

		internal static ref readonly Smb2ErrorContextHeader ReadErrorCtxHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ErrorContextHeader>(reader.Consume(Smb2ErrorContextHeader.StructSize))[0];

		internal static ref readonly Smb2SessionRequestBody ReadSessionReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2SessionRequestBody>(reader.Consume(Smb2SessionRequestBody.StructSize))[0];

		internal static ref readonly Smb2LogoffRequestBody ReadLogoffReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2LogoffRequestBody>(reader.Consume(Smb2LogoffRequestBody.StructSize))[0];

		internal static ref readonly Smb2LogoffResponseBody ReadLogoffRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2LogoffResponseBody>(reader.Consume(Smb2LogoffResponseBody.StructSize))[0];

		internal static ref readonly Smb2SessionResponseBody ReadSessionRespBody(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2SessionResponseBody>(reader.Consume(Smb2SessionResponseBody.StructSize))[0];

		internal static ref readonly Smb2TreeConnectRequestBody ReadTreeConnectReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2TreeConnectRequestBody>(reader.Consume(Smb2TreeConnectRequestBody.StructSize))[0];

		internal static ref readonly Smb2TreeConnectResponseBody ReadTreeConnectRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2TreeConnectResponseBody>(reader.Consume(Smb2TreeConnectResponseBody.StructSize))[0];

		internal static ref readonly Smb2CreateRequestBody ReadCreateReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CreateRequestBody>(reader.Consume(Smb2CreateRequestBody.StructSize))[0];

		internal static ref readonly Smb2CreateResponseBody ReadCreateRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CreateResponseBody>(reader.Consume(Smb2CreateResponseBody.StructSize))[0];

		internal static ref readonly Smb2CreateContextHeader ReadCreateContextHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CreateContextHeader>(reader.Consume(Smb2CreateContextHeader.StructSize))[0];

		internal static ref readonly Smb2IoctlRequestBody ReadIoctlReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2IoctlRequestBody>(reader.Consume(Smb2IoctlRequestBody.StructSize))[0];

		internal static ref readonly Smb2IoctlResponseBody ReadIoctlRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2IoctlResponseBody>(reader.Consume(Smb2IoctlResponseBody.StructSize))[0];

		internal static ref readonly Smb2EchoRequestBody ReadEchoReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2EchoRequestBody>(reader.Consume(Smb2EchoRequestBody.StructSize))[0];

		internal static ref readonly Smb2EchoResponseBody ReadEchoRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2EchoResponseBody>(reader.Consume(Smb2EchoResponseBody.StructSize))[0];

		internal static ref readonly Smb2QueryDirRequestBody ReadQueryDirReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryDirRequestBody>(reader.Consume(Smb2QueryDirRequestBody.StructSize))[0];

		internal static ref readonly Smb2QueryDirResponseBody ReadQueryDirRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryDirResponseBody>(reader.Consume(Smb2QueryDirResponseBody.StructSize))[0];

		internal static ref readonly Smb2FlushRequestBody ReadFlushReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2FlushRequestBody>(reader.Consume(Smb2FlushRequestBody.StructSize))[0];

		internal static ref readonly Smb2SetInfoRequestBody ReadSetInfoHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2SetInfoRequestBody>(reader.Consume(Smb2SetInfoRequestBody.StructSize))[0];

		internal static ref readonly Smb2QueryInfoRequestBody ReadQueryInfoHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryInfoRequestBody>(reader.Consume(Smb2QueryInfoRequestBody.StructSize))[0];

		internal static ref readonly Smb2FlushResponseBody ReadFlushRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2FlushResponseBody>(reader.Consume(Smb2FlushResponseBody.StructSize))[0];

		internal static ref readonly Smb2ChangeNotifyRequestHeader ReadChangeNotifyReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ChangeNotifyRequestHeader>(reader.Consume(Smb2ChangeNotifyRequestHeader.StructSize))[0];

		internal static ref readonly Smb2ChangeNotifyResponseHeader ReadChangeNotifyRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ChangeNotifyResponseHeader>(reader.Consume(Smb2ChangeNotifyResponseHeader.StructSize))[0];

		internal static ref readonly FileNotifyInfoHeader ReadFileNotifyInfoHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileNotifyInfoHeader>(reader.Consume(FileNotifyInfoHeader.StructSize))[0];

		internal static ref readonly Smb2CloseRequestBody ReadCloseReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CloseRequestBody>(reader.Consume(Smb2CloseRequestBody.StructSize))[0];

		internal static ref readonly Smb2CancelRequestBody ReadCancelReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CancelRequestBody>(reader.Consume(Smb2CancelRequestBody.StructSize))[0];

		internal static ref readonly Smb2CloseResponseBody ReadCloseRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CloseResponseBody>(reader.Consume(Smb2CloseResponseBody.StructSize))[0];

		internal static ref readonly Smb2ReadRequestBody ReadReadReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ReadRequestBody>(reader.Consume(Smb2ReadRequestBody.StructSize))[0];

		internal static ref readonly Smb2ReadResponseBody ReadReadRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ReadResponseBody>(reader.Consume(Smb2ReadResponseBody.StructSize))[0];

		internal static ref readonly Smb2WriteRequestBody ReadWriteReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2WriteRequestBody>(reader.Consume(Smb2WriteRequestBody.StructSize))[0];

		internal static ref readonly Smb2WriteResponseBody ReadWriteRespBody(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2WriteResponseBody>(reader.Consume(Smb2WriteResponseBody.StructSize))[0];

		internal static ref readonly Smb2NicInfoStruct ReadNicInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2NicInfoStruct>(reader.Consume(Smb2NicInfoStruct.StructSize))[0];

		internal static ref readonly Smb2QueryInfoResponseBody ReadQueryInfoResponseBody(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryInfoResponseBody>(reader.Consume(Smb2QueryInfoResponseBody.StructSize))[0];

		internal static ref readonly FileBasicInfoStruct ReadFileBasicInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileBasicInfoStruct>(reader.Consume(FileBasicInfoStruct.StructSize))[0];
		internal static unsafe List<Smb2NicInfo> ReadNicInfoList(this ByteMemoryReader reader)
		{
			List<Smb2NicInfo> list = new List<Smb2NicInfo>();

			int offStruc = reader.Position;
			int next = 0;
			do
			{
				offStruc += next;
				reader.Position = offStruc;
				ref readonly Smb2NicInfoStruct struc = ref reader.ReadNicInfo();
				list.Add(new Smb2NicInfo { info = struc });
				next = struc.next;
			} while (next > 0);

			return list;
		}




		internal static ref readonly FileBothDirectoryInfo ReadFileBothDirInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileBothDirectoryInfo>(reader.Consume(FileBothDirectoryInfo.StructSize))[0];

		internal static ref readonly FileDirectoryInfo ReadFileDirInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileDirectoryInfo>(reader.Consume(FileDirectoryInfo.StructSize))[0];

		internal static ref readonly FileNetworkOpenInfo ReadFileNetOpenInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileNetworkOpenInfo>(reader.Consume(FileNetworkOpenInfo.StructSize))[0];

		internal static ref readonly FileNamesInfo ReadFileNamesInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileNamesInfo>(reader.Consume(FileNamesInfo.StructSize))[0];

		internal static ref readonly FileFullDirectoryInfo ReadFileFullDirInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileFullDirectoryInfo>(reader.Consume(FileFullDirectoryInfo.StructSize))[0];

		internal static ref readonly FileIdFullDirectoryInfo ReadFileIdFullDirInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileIdFullDirectoryInfo>(reader.Consume(FileIdFullDirectoryInfo.StructSize))[0];

		internal static ref readonly FileIdBothDirectoryInfo ReadFileIdBothDirInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileIdBothDirectoryInfo>(reader.Consume(FileIdBothDirectoryInfo.StructSize))[0];

		internal static ref readonly Smb2LinkErrorHeader ReadLinkErrorHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2LinkErrorHeader>(reader.Consume(Smb2LinkErrorHeader.StructSize))[0];

		internal static ref readonly Smb2OplockBreakBody ReadOplockBreakHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2OplockBreakBody>(reader.Consume(Smb2OplockBreakBody.StructSize))[0];

		internal static ref readonly Smb2LeaseBreakBody ReadLeaseBreakHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2LeaseBreakBody>(reader.Consume(Smb2LeaseBreakBody.StructSize))[0];

		internal static ref readonly Smb2OplockBreakAckHeader ReadOplockBreakAckHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2OplockBreakAckHeader>(reader.Consume(Smb2OplockBreakAckHeader.StructSize))[0];

		internal static ref readonly Smb2LeaseBreakAckBody ReadLeaseBreakAckHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2LeaseBreakAckBody>(reader.Consume(Smb2LeaseBreakAckBody.StructSize))[0];

		internal static ref readonly Smb2SetInfoResponseBody ReadSmb2SetInfoResponseHeader(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2SetInfoResponseBody>(reader.Consume(Smb2SetInfoResponseBody.StructSize))[0];
	}
}
