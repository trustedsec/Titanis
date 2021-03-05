using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.32 SMB2 IOCTL Response
	sealed class Smb2IoctlResponse : Smb2Pdu<Smb2IoctlResponseBody>
	{
		internal Memory<byte> inputBuffer;
		internal Memory<byte> outputBuffer;

		public int InputResponseSize => this.body.inputCount;
		public int OutputResponseSize => this.body.outputCount;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Ioctl;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 49;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2IoctlResponseBody body = ref reader.ReadIoctlRespHdr();
			this.body = body;

			var req = this.request as Smb2IoctlRequest;

			if (body.inputCount > 0)
			{
				reader.Position = offPdu + body.inputOffset;
				var buffer =
					(req != null) ? req.inputResponseBuffer.Slice(0, Math.Min(body.inputCount, req.inputResponseBuffer.Length))
					: new byte[body.inputCount];
				reader.Consume(body.inputCount).CopyTo(buffer.Span);
				this.inputBuffer = buffer;
			}
			else
			{
				this.inputBuffer = Array.Empty<byte>();
			}

			if (body.outputCount > 0)
			{
				reader.Position = offPdu + body.outputOffset;
				var buffer =
					(req != null) ? req.outputResponseBuffer.Slice(0, Math.Min(body.outputCount, req.outputResponseBuffer.Length))
					: new byte[body.outputCount];
				reader.Consume(buffer.Length).CopyTo(buffer.Span);
				this.outputBuffer = buffer;
			}
			else
			{
				this.outputBuffer = Array.Empty<byte>();
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2IoctlResponseBody body)
		{
			int offData = (Smb2PduAsyncHeader.StructSize + Smb2IoctlResponseBody.StructSize);
			if (this.inputBuffer.Length > 0)
			{
				body.inputOffset = offData;
				body.inputCount = this.inputBuffer.Length;
				offData += this.inputBuffer.Length;
			}
			if (this.inputBuffer.Length > 0)
			{
				offData = BinaryHelper.Align(offData, 8);
				body.inputOffset = offData;
				body.inputCount = this.outputBuffer.Length;
			}

			writer.WriteIoctlRespHdr(body);

			if (this.inputBuffer.Length > 0)
				writer.WriteBytes(this.inputBuffer.Span);
			if (this.outputBuffer.Length > 0)
			{
				writer.SetPosition(offData + Smb2PduAsyncHeader.StructSize);
				writer.WriteBytes(this.inputBuffer.Span);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2IoctlResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2IoctlResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;

		internal uint ctlCode;
		internal Smb2FileHandle fileHandle;
		internal int inputOffset;
		internal int inputCount;
		internal int outputOffset;
		internal int outputCount;
		internal int flags;
		internal uint reserved2;
	}
}
