using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.39
	sealed class Smb2SetInfoRequest : Smb2Pdu<Smb2SetInfoRequestBody>
	{
		internal Smb2SetInfoRequest(Smb2FileHandle fileId, Smb2FileInfo info)
		{
			this._info = info;
			this.body.fileId = fileId;
		}

		private readonly Smb2FileInfo _info;

		internal override Smb2Command Command => Smb2Command.SetInfo;
		/// <inheritdoc/>
		internal sealed override int SendPayloadSize => this.body.bufferLength;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadSetInfoHdr();
		}

		protected override ushort ValidBodySize => 33;
		internal override void WriteTo(ByteWriter writer, ref Smb2SetInfoRequestBody body)
		{
			var info = this._info;
			if (info == null)
				throw new InvalidOperationException();

			int offPdu = writer.Position - Smb2PduSyncHeader.StructSize;

			body.infoType = info.InfoType;
			body.fileInfoClass = info.InfoClass;

			int offHeader = writer.Position;
			writer.Write(body);

			int offInfo = writer.Position;
			info.WriteTo(writer);

			ref Smb2SetInfoRequestBody hdr = ref MemoryMarshal.Cast<byte, Smb2SetInfoRequestBody>(writer.GetBuffer().Slice(offHeader, Smb2SetInfoRequestBody.StructSize))[0];
			hdr.bufferLength = writer.Position - offInfo;
			hdr.bufferOffset = (ushort)(offInfo - offPdu);
		}
	}

	// [MS-SMB2] § 2.2.37 SMB2 QUERY_INFO Request
	enum Smb2FileInfoType : byte
	{
		File = 1,
		FileSystem = 2,
		Security = 3,
		Quota = 4,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2SetInfoRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2SetInfoRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2FileInfoType infoType;
		internal FileInfoClass fileInfoClass;
		internal int bufferLength;
		internal ushort bufferOffset;
		internal ushort reserved;
		internal SecurityInfo additionalInfo;
		internal Smb2FileHandle fileId;
	}

	abstract class Smb2FileInfo
	{
		internal abstract Smb2FileInfoType InfoType { get; }
		internal abstract FileInfoClass InfoClass { get; }

		internal abstract void WriteTo(ByteWriter writer);
	}

	// [MS-FSCC] § 2.4.4
	sealed class FileAllocInfo : Smb2FileInfo
	{
		internal FileAllocInfo(long size)
		{
			this.size = size;
		}

		internal sealed override Smb2FileInfoType InfoType => Smb2FileInfoType.File;
		internal sealed override FileInfoClass InfoClass => FileInfoClass.AllocationInfo;

		internal long size;

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteInt64LE(this.size);
		}
	}

	// [MS-FSCC] § 2.4.7
	sealed class FileBasicInfo : Smb2FileInfo
	{
		internal FileBasicInfo(
			DateTime? creationTime,
			DateTime? lastAccessTime,
			DateTime? lastWriteTime,
			DateTime? changeTime,
			FileAttributes attributes
			)
		{
			this.creationTime = creationTime.HasValue ? creationTime.Value.ToFileTimeUtc() : 0;
			this.lastAccessTime = lastAccessTime.HasValue ? lastAccessTime.Value.ToFileTimeUtc() : 0;
			this.lastWriteTime = lastWriteTime.HasValue ? lastWriteTime.Value.ToFileTimeUtc() : 0;
			this.changeTime = changeTime.HasValue ? changeTime.Value.ToFileTimeUtc() : 0;
			this.attributes = attributes;
		}

		internal sealed override Smb2FileInfoType InfoType => Smb2FileInfoType.File;
		internal sealed override FileInfoClass InfoClass => FileInfoClass.BasicInfo;

		internal long creationTime;
		internal long lastAccessTime;
		internal long lastWriteTime;
		internal long changeTime;
		internal FileAttributes attributes;
		internal int reserved;

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteInt64LE(this.creationTime);
			writer.WriteInt64LE(this.lastAccessTime);
			writer.WriteInt64LE(this.lastWriteTime);
			writer.WriteInt64LE(this.changeTime);
			writer.WriteInt32LE((int)this.attributes);
			writer.WriteInt32LE(this.reserved);
		}
	}

	// [MS-FSCC] § 2.4.13
	sealed class EndOfFileInfo : Smb2FileInfo
	{
		internal EndOfFileInfo(long size)
		{
			this.size = size;
		}

		internal sealed override Smb2FileInfoType InfoType => Smb2FileInfoType.File;
		internal sealed override FileInfoClass InfoClass => FileInfoClass.EndOfFileInfo;

		internal long size;

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteInt64LE(this.size);
		}
	}
}
