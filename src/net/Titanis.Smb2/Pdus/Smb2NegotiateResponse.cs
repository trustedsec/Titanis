using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.4 SMB2 NEGOTIATE Response
	sealed class Smb2NegotiateResponse : Smb2Pdu<Smb2NegotiateResponseBody>
	{
		internal byte[] secToken;
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
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			ref Smb2NegotiateResponseBody body = ref this.body;
			body = reader.ReadNegRespHdr();
			if (body.secBufferLength > 0)
			{
				reader.Position = (offPdu + body.secBufferOffset);
				this.secToken = reader.ReadBytes(body.secBufferLength);
			}

			if (body.dialect >= Smb2Dialect.Smb3_1_1)
			{
				if (body.negotiateContextCount > 0)
				{
					Smb2NegotiateContext[] negCtxList = new Smb2NegotiateContext[body.negotiateContextCount];
					for (int i = 0; i < body.negotiateContextCount; i++)
					{
						reader.Align(8, offPdu);

						ref readonly Smb2NegotiateContextHeader negHdr = ref reader.ReadNegCtxHdr();
						Smb2NegotiateContext ctx;

						int offContext = reader.Position;
						switch (negHdr.contextType)
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
							ctx.ReadFrom(reader, negHdr.dataLength);

							ctx.ApplyTo(this);
							negCtxList[i] = ctx;
						}

						reader.Position = offContext + negHdr.dataLength;
					}
				}
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2NegotiateResponseBody body)
		{
			throw new NotImplementedException();
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2NegotiateResponseBody : ISmb2PduStruct
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
		internal ushort secBufferLength;
		internal uint negotiateContextOffset;
	}
}
