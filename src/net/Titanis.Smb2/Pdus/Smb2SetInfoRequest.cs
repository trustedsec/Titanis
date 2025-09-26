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

	/// <summary>
	/// Describes file information.
	/// </summary>
	public abstract class Smb2FileInfo
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
	/// <summary>
	/// Describes basic file information.
	/// </summary>
	public sealed class FileBasicInfo : Smb2FileInfo
	{
		internal FileBasicInfo(
			DateTime? creationTime,
			DateTime? lastAccessTime,
			DateTime? lastWriteTime,
			DateTime? changeTime,
			FileAttributes attributes
			)
		{
			this.struc.creationTime = creationTime.HasValue ? creationTime.Value.ToFileTimeUtc() : 0;
			this.struc.lastAccessTime = lastAccessTime.HasValue ? lastAccessTime.Value.ToFileTimeUtc() : 0;
			this.struc.lastWriteTime = lastWriteTime.HasValue ? lastWriteTime.Value.ToFileTimeUtc() : 0;
			this.struc.changeTime = changeTime.HasValue ? changeTime.Value.ToFileTimeUtc() : 0;
			this.struc.attributes = attributes;
		}

		internal FileBasicInfo(in FileBasicInfoStruct struc) => this.struc = struc;

		internal sealed override Smb2FileInfoType InfoType => Smb2FileInfoType.File;
		internal sealed override FileInfoClass InfoClass => FileInfoClass.BasicInfo;

		internal FileBasicInfoStruct struc;

		// TODO: Copy field descriptions from [MS-FSCC]
		public DateTime CreationTime => DateTime.FromFileTimeUtc(this.struc.creationTime);
		public DateTime LastAccessTime => DateTime.FromFileTimeUtc(this.struc.lastAccessTime);
		public DateTime LastWriteTime => DateTime.FromFileTimeUtc(this.struc.lastWriteTime);
		public DateTime ChangeTime => DateTime.FromFileTimeUtc(this.struc.changeTime);
		public FileAttributes Attributes => this.struc.attributes;
		//public int reserved => this.struc.reserved;

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteInt64LE(this.struc.creationTime);
			writer.WriteInt64LE(this.struc.lastAccessTime);
			writer.WriteInt64LE(this.struc.lastWriteTime);
			writer.WriteInt64LE(this.struc.changeTime);
			writer.WriteInt32LE((int)this.struc.attributes);
			writer.WriteInt32LE(this.struc.reserved);
		}
	}

	struct FileBasicInfoStruct
	{
		public FileBasicInfoStruct(
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
		public static unsafe int StructSize => sizeof(FileBasicInfoStruct);

		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public FileAttributes attributes;
		public int reserved;
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
