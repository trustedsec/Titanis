using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.37 SMB2 QUERY_INFO Request
	sealed class Smb2QueryInfoRequest : Smb2Pdu<Smb2QueryInfoRequestBody>
	{
		internal Smb2QueryInfoRequest(Smb2FileHandle fileId, Smb2QueryFileInfo queryInput, int outputBufferSize)
		{
			this._input = queryInput;
			this.body.fileId = fileId;
			this.body.outputBufferLength = outputBufferSize;
		}

		private readonly Smb2QueryFileInfo? _input;
		internal Memory<byte> outputBuffer;

		internal override Smb2Command Command => Smb2Command.QueryInfo;
		/// <inheritdoc/>
		internal sealed override int SendPayloadSize => this.body.inputBufferLength;
		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.outputBufferLength;

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			this.body = reader.ReadQueryInfoHdr();
		}

		public SecurityInfo Additional
		{
			get => this.body.additionalInfo;
			set => this.body.additionalInfo = value;
		}

		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 41;
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2QueryInfoRequestBody body)
		{
			var info = this._input;
			if (info == null)
				throw new InvalidOperationException();

			int offPdu = writer.Position - Smb2PduSyncHeader.StructSize;

			body.infoType = info.InfoType;
			body.fileInfoClass = info.InfoClass;

			int offHeader = writer.Position;
			writer.Write(in body);

			int offInfo = writer.Position;
			info.WriteTo(writer);

			ref var hdr = ref MemoryMarshal.Cast<byte, Smb2QueryInfoRequestBody>(writer.GetBuffer().Slice(offHeader, Smb2QueryInfoRequestBody.StructSize))[0];
			hdr.inputBufferLength = writer.Position - offInfo;

			// [MS-SMB2] § <75>
			hdr.inputBufferOffset = (ushort)(offInfo - offPdu);
		}
	}

	[Flags]
	enum Smb2QueryInfoFlags
	{
		None = 0,
		RestartScan = 1,
		ReturnSingleEntry = 2,
		IndexSpecified = 4,
	}

	// [MS-SMB2] § 2.2.37 SMB2 QUERY_INFO Request
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2QueryInfoRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2QueryInfoRequestBody);
		public ushort StructureSize { get => this.headerSize; set => this.headerSize = value; }

		internal ushort headerSize;
		internal Smb2FileInfoType infoType;
		internal FileInfoClass fileInfoClass;
		internal int outputBufferLength;
		internal ushort inputBufferOffset;
		internal ushort reserved;
		internal int inputBufferLength;
		internal SecurityInfo additionalInfo;
		internal Smb2QueryInfoFlags flags;
		internal Smb2FileHandle fileId;
	}

	abstract class Smb2QueryFileInfo
	{
		internal abstract Smb2FileInfoType InfoType { get; }
		internal abstract FileInfoClass InfoClass { get; }

		public static Smb2QueryFileInfo Generic(
			Smb2FileInfoType infoType,
			FileInfoClass infoClass)
			=> new Smb2GenericQueryFileInfo(infoType, infoClass);

		internal abstract void WriteTo(ByteWriter writer);
	}

	internal class Smb2GenericQueryFileInfo : Smb2QueryFileInfo
	{
		internal Smb2GenericQueryFileInfo(Smb2FileInfoType infoType, FileInfoClass infoClass)
		{
			this.InfoType = infoType;
			this.InfoClass = infoClass;
		}

		internal sealed override Smb2FileInfoType InfoType { get; }
		internal sealed override FileInfoClass InfoClass { get; }

		internal sealed override void WriteTo(ByteWriter writer)
		{
			// Nothing to write
		}
	}
}
