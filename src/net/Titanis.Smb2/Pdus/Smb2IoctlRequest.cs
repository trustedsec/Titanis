using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.31 SMB2 IOCTL Request
	sealed class Smb2IoctlRequest : Smb2Pdu<Smb2IoctlRequestBody>
	{
		internal ReadOnlyMemory<byte> inputBuffer;
		internal ReadOnlyMemory<byte> outputBuffer;

		// These are used when reading the IoctlResponse
		internal Memory<byte> inputResponseBuffer;
		internal Memory<byte> outputResponseBuffer;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Ioctl;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 57;

		/// <inheritdoc/>
		internal sealed override int SendPayloadSize => this.inputBuffer.Length + this.outputBuffer.Length;
		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.maxOutputResponse;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;
			ref readonly Smb2IoctlRequestBody body = ref reader.ReadIoctlReqHdr();
			this.body = body;

			if (body.inputCount > 0)
			{
				reader.Position = offPdu + body.inputOffset;
				this.inputBuffer = reader.ReadBytes((int)body.inputOffset);
			}
			if (body.outputCount > 0)
			{
				reader.Position = offPdu + body.outputOffset;
				this.outputBuffer = reader.ReadBytes((int)body.outputOffset);
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2IoctlRequestBody body)
		{
			int offData = (Smb2PduAsyncHeader.StructSize + Smb2IoctlRequestBody.StructSize);
			if (this.inputBuffer.Length > 0)
			{
				body.inputOffset = offData;
				body.inputCount = (uint)this.inputBuffer.Length;
				offData += this.inputBuffer.Length;
			}
			if (this.outputBuffer.Length > 0)
			{
				offData = BinaryHelper.Align(offData, 8);
				body.outputOffset = offData;
				body.outputCount = (uint)this.outputBuffer.Length;
			}
			else
			{
				// [MS-SMB2] <64>
				body.outputOffset = body.inputOffset;
			}

			writer.WriteIoctlReqHdr(body);

			if (this.inputBuffer.Length > 0)
				writer.WriteBytes(this.inputBuffer.Span);
			if (this.outputBuffer.Length > 0)
			{
				writer.SetPosition(offData + Smb2PduAsyncHeader.StructSize);
				writer.WriteBytes(this.inputBuffer.Span);
			}
		}
	}

	enum Smb2IoctlOptions : uint
	{
		IsIoctl = 0,
		IsFsctl = 1
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2IoctlRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2IoctlRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;

		internal uint ctlCode;
		internal Smb2FileHandle fileHandle;
		internal int inputOffset;
		internal uint inputCount;
		internal int maxInputResponse;
		internal int outputOffset;
		internal uint outputCount;
		internal int maxOutputResponse;
		internal Smb2IoctlOptions flags;
		internal uint reserved2;
	}
}
