using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.4 SMB2 NEGOTIATE Response
	sealed class Smb2NegotiateResponse : Smb2Pdu<Smb2NegotiateResponseBody>
	{
		internal byte[] secToken => this.body.secToken;
		internal PreauthHashAlgorithm[]? hashAlgs;
		internal byte[]? preauthSalt;
		internal SigningAlgorithm[]? signingAlgs;
		internal Cipher[]? cipherAlgs;
		internal CompressionCaps compressionCaps;
		internal CompressionAlgorithm[]? compressionAlgs;
		internal string serverNetName;
		internal TransportCaps TransportCapabilities;
		internal RdmaTransformId[] rdmaTransforms;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Negotiate;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Negotiate;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 65;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			ref Smb2NegotiateResponseBody body = ref this.body;
			body = reader.ReadPduStruct<Smb2NegotiateResponseBody>();
			if (body.dialect >= Smb2Dialect.Smb3_1_1 && body.negCtxList != null)
			{
				foreach (var ctx in body.negCtxList)
				{
					ctx.ctx.ApplyTo(this);
				}
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2NegotiateResponseBody body)
		{
			throw new NotImplementedException();
		}
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2NegotiateResponseBody : ISmb2PduStruct
	{
		public unsafe static short StructSize => (short)sizeof(Smb2NegotiateResponseBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2SecurityMode securityMode;
		internal Smb2Dialect dialect;
		internal short negotiateContextCount;
		internal Guid serverGuid;
		internal Smb2Capabilities caps;
		internal uint maxTransactSize;
		internal uint maxReadSize;
		internal uint maxWriteSize;
		internal long systemTime;
		internal ulong serverStartTime;
		internal ushort secBufferOffset;
		private long secBufferOffset2 => this.secBufferOffset - Smb2PduSyncHeader.StructSize;
		internal ushort secBufferLength;

		internal uint negotiateContextOffset;
		private long negotiateContextOffset2 => this.negotiateContextOffset - Smb2PduSyncHeader.StructSize;

		private readonly bool HasContexts => this.negotiateContextCount > 0;

		[PduOffset(nameof(secBufferOffset2))]
		[PduArraySize(nameof(secBufferLength))]
		internal byte[] secToken;

		// TODO: Only try deserializing if HasContexts (PduStruct limitation)
		//[PduConditional(nameof(HasContexts))]
		[PduArraySize(nameof(negotiateContextCount))]
		[PduOffset(nameof(negotiateContextOffset2))]
		internal Smb2NegotiateContextStruc[] negCtxList;
	}

	[PduStruct]
	[PduAlignment(8)]
	partial struct Smb2NegotiateContextStruc
	{
		internal Smb2NegotiateContextType contextType;
		internal short dataLength;
		internal int reserved;

		[PduField(ReadMethod = nameof(ReadContext), WriteMethod = nameof(WriteContext))]
		internal Smb2NegotiateContext ctx;

		private void WriteContext(ByteWriter writer, Smb2NegotiateContext ctx)
		{
			throw new NotImplementedException();
		}

		private Smb2NegotiateContext ReadContext(IByteSource source)
		{
			Smb2NegotiateContext ctx;

			var offContext = source.Position;
			switch (this.contextType)
			{
				case Smb2NegotiateContextType.PreauthIntegrityCaps:
					ctx = new PreauthIntegrityCapsContext();
					break;
				case Smb2NegotiateContextType.EncryptionCaps:
					ctx = new CipherCapsContext();
					break;
				case Smb2NegotiateContextType.SigningCaps:
					ctx = new SigningCapsContext();
					break;
				case Smb2NegotiateContextType.CompressionCaps:
					ctx = new CompressionCapsContext();
					break;
				case Smb2NegotiateContextType.NetName:
					ctx = new NetNameContext();
					break;
				case Smb2NegotiateContextType.TransportCaps:
					ctx = new TransportCapsContext();
					break;
				case Smb2NegotiateContextType.RdmaTransformCaps:
					ctx = new RdmaTransformCapsContext();
					break;
				default:
					ctx = null;
					break;
			}
			if (ctx == null)
			{
				// TODO: Alert on unknown context
			}
			else
			{
				ctx.ReadFrom(source, this.dataLength);
			}

			source.Position = offContext + this.dataLength;
			return ctx;
		}
	}
}
