using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.33 SMB2 QUERY_DIRECTORY Request
	sealed class Smb2QueryDirRequest : Smb2Pdu<Smb2QueryDirRequestBody>
	{
		internal string searchPattern;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.QueryDirectory;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 33;
		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.outputBufferLength;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			ref readonly Smb2QueryDirRequestBody body = ref reader.ReadQueryDirReqHdr();
			this.body = body;
			if (body.fileNameLength > 0)
			{
				reader.Position = offPdu + body.fileNameOffset;
				this.searchPattern = Encoding.Unicode.GetString(reader.ReadBytes(body.fileNameLength));
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2QueryDirRequestBody body)
		{
			if (this.searchPattern != null)
			{
				body.fileNameOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2QueryDirRequestBody.StructSize);
				body.fileNameLength = (ushort)Encoding.Unicode.GetByteCount(this.searchPattern);
			}
			else
			{
				body.fileNameOffset = 0;
				body.fileNameLength = 0;
			}

			writer.WriteQueryDirReqHdr(body);
			if (this.searchPattern != null)
				writer.WriteStringUni(this.searchPattern);
		}
	}

	enum Smb2DirEntryInfoClass : byte
	{
		DirInfo = 1,
		FullDirInfo = 2,
		FullDirInfoId = 0x26,
		BothDirInfo = 0x03,
		BothDirInfoId = 0x25,
		NamesInfo = 0x0C
	}

	[Flags]
	enum Smb2QueryDirFlags : byte
	{
		None = 0,

		RestartScans = 1,
		ReturnSingleEntry = 2,
		IndexSpecified = 4,
		Reopen = 0x10
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2QueryDirRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2QueryDirRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2DirEntryInfoClass infoClass;
		internal Smb2QueryDirFlags flags;
		internal uint fileIndex;
		internal Smb2FileHandle dirHandle;
		internal ushort fileNameOffset;
		internal ushort fileNameLength;
		internal int outputBufferLength;
	}


}
