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
		internal static ref readonly Smb2ErrorContextHeader ReadErrorCtxHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2ErrorContextHeader>(reader.Consume(Smb2ErrorContextHeader.StructSize))[0];

		internal static ref readonly Smb2CreateRequestBody ReadCreateReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CreateRequestBody>(reader.Consume(Smb2CreateRequestBody.StructSize))[0];

		internal static ref readonly Smb2IoctlRequestBody ReadIoctlReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2IoctlRequestBody>(reader.Consume(Smb2IoctlRequestBody.StructSize))[0];

		internal static ref readonly Smb2EchoRequestBody ReadEchoReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2EchoRequestBody>(reader.Consume(Smb2EchoRequestBody.StructSize))[0];

		internal static ref readonly Smb2EchoResponseBody ReadEchoRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2EchoResponseBody>(reader.Consume(Smb2EchoResponseBody.StructSize))[0];

		internal static ref readonly Smb2QueryDirRequestBody ReadQueryDirReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryDirRequestBody>(reader.Consume(Smb2QueryDirRequestBody.StructSize))[0];

		internal static ref readonly Smb2FlushRequestBody ReadFlushReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2FlushRequestBody>(reader.Consume(Smb2FlushRequestBody.StructSize))[0];

		internal static ref readonly Smb2SetInfoRequestBody ReadSetInfoHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2SetInfoRequestBody>(reader.Consume(Smb2SetInfoRequestBody.StructSize))[0];

		internal static ref readonly Smb2QueryInfoRequestBody ReadQueryInfoHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryInfoRequestBody>(reader.Consume(Smb2QueryInfoRequestBody.StructSize))[0];

		internal static ref readonly Smb2FlushResponseBody ReadFlushRespHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2FlushResponseBody>(reader.Consume(Smb2FlushResponseBody.StructSize))[0];

		internal static ref readonly Smb2CloseRequestBody ReadCloseReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2CloseRequestBody>(reader.Consume(Smb2CloseRequestBody.StructSize))[0];

		internal static ref readonly Smb2WriteRequestBody ReadWriteReqHdr(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2WriteRequestBody>(reader.Consume(Smb2WriteRequestBody.StructSize))[0];

		internal static ref readonly Smb2QueryInfoResponseBody ReadQueryInfoResponseBody(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, Smb2QueryInfoResponseBody>(reader.Consume(Smb2QueryInfoResponseBody.StructSize))[0];

		internal static ref readonly FileBasicInfoStruct ReadFileBasicInfo(this ByteMemoryReader reader)
			=> ref MemoryMarshal.Cast<byte, FileBasicInfoStruct>(reader.Consume(FileBasicInfoStruct.StructSize))[0];
		internal static List<Smb2NicInfo> ReadNicInfoList(this ByteMemoryReader reader)
		{
			List<Smb2NicInfo> list = new List<Smb2NicInfo>();

			int offStruc = reader.Position;
			int next = 0;
			do
			{
				offStruc += next;
				reader.Position = offStruc;
				var struc = reader.ReadPduStruct<Smb2NicInfoStruct>();
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
