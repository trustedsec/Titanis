using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.32 SMB2 IOCTL Response
	sealed class Smb2QueryInfoResponse : Smb2Pdu<Smb2QueryInfoResponseBody>
	{
		internal Memory<byte> outputBuffer;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.QueryInfo;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 9;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offBody = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2QueryInfoResponseBody body = ref reader.ReadQueryInfoResponseBody();
			this.body = body;

			var req = this.request as Smb2QueryInfoRequest;
			if (body.outputBufferLength > 0)
			{
				reader.Position = offBody + body.outputBufferOffset;

				Memory<byte> buffer =
					(req != null) ? req.outputBuffer.Slice(0, Math.Min(body.outputBufferLength, req.outputBuffer.Length))
					: new byte[body.outputBufferLength];
				reader.Consume(buffer.Length).CopyTo(buffer.Span);
				this.outputBuffer = buffer;
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2QueryInfoResponseBody body)
		{
			int offBody = (Smb2PduAsyncHeader.StructSize + Smb2IoctlResponseBody.StructSize);

			body.outputBufferOffset = (ushort)Smb2QueryInfoResponseBody.StructSize;
			body.outputBufferLength = (ushort)this.outputBuffer.Length;
			writer.Write(body);

			if (this.outputBuffer.Length > 0)
				writer.WriteBytes(this.outputBuffer.Span);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2QueryInfoResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2QueryInfoResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort outputBufferOffset;
		internal int outputBufferLength;
	}

	[PduByteOrder(PduByteOrder.LittleEndian)]
	public struct FileTime : IPduStruct
	{
		public DateTime Time;

		public void ReadFrom(IByteSource reader)
		{
			this.Time = DateTime.FromFileTimeUtc(reader.ReadInt64LE());
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
		{
			this.Time = DateTime.FromFileTimeUtc(byteOrder switch
			{
				PduByteOrder.BigEndian => reader.ReadInt64BE(),
				PduByteOrder.LittleEndian => reader.ReadInt64LE(),
				_ => throw new ArgumentOutOfRangeException(nameof(byteOrder))
			});
		}

		public void WriteTo(ByteWriter writer)
		{
			writer.WriteInt64LE(this.Time.ToFileTimeUtc());
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
		{
			switch (byteOrder)
			{
				case PduByteOrder.LittleEndian:
					writer.WriteInt64LE(this.Time.ToFileTimeUtc());
					break;
				case PduByteOrder.BigEndian:
					writer.WriteInt64BE(this.Time.ToFileTimeUtc());
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(byteOrder));
			}
		}
	}

	//[PduStruct]
	//partial class FileDirectoryInfo
	//{
	//	internal uint nextEntryOffset;
	//	internal uint fileIndex;
	//	internal FileTime creationTime;
	//	internal FileTime lastAccessTime;
	//	internal FileTime lastWriteTime;
	//	internal FileTime changeTime;
	//	internal long endOfFile;
	//	internal long allocationSize;
	//	internal FileAttributes fileAttributes;
	//	internal int nameLength;
	//	[PduString(CharSet.Unicode, nameof(nameLength))]
	//	internal string fileName;
	//}
}
