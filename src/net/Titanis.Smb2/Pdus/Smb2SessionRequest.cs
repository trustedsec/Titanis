using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.5 SMB2 SESSION_SETUP Request
	sealed class Smb2SessionRequest : Smb2Pdu<Smb2SessionRequestBody>
	{
		internal byte[] secToken;

		internal override Smb2Command Command => Smb2Command.SessionSetup;
		internal override Smb2Priority Priority => Smb2Priority.SessionSetup;

		internal HashAlgorithm preauthHashAlg;
		internal byte[] preauthIntegrityValue;
		internal byte[] preauthIntegrityValue_resp;

		internal override void OnSending(Span<byte> pduBytes)
		{
			if (preauthHashAlg != null)
				this.preauthIntegrityValue = Smb2Connection.UpdatePreauthHash(
					this.preauthIntegrityValue,
					this.preauthHashAlg,
					pduBytes);
		}

		internal override void OnResponse(Smb2Message msg)
		{
			if (preauthHashAlg != null)
				this.preauthIntegrityValue_resp = Smb2Connection.UpdatePreauthHash(
					this.preauthIntegrityValue,
					this.preauthHashAlg,
					msg.pduBytes.Span);
		}

		internal override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values


			ref readonly Smb2SessionRequestBody hdr = ref reader.ReadSessionReqHdr();
			this.body = hdr;
			if (hdr.secBufferLength > 0)
			{
				reader.Position = offPdu + hdr.secBufferOffset;
				this.secToken = reader.ReadBytes(hdr.secBufferLength);
			}
		}

		protected override ushort ValidBodySize => 25;
		internal override void WriteTo(ByteWriter writer, ref Smb2SessionRequestBody body)
		{
			if (this.secToken != null)
			{
				body.secBufferOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2SessionRequestBody.StructSize);
				body.secBufferLength = (ushort)this.secToken.Length;
			}
			else
			{
				body.secBufferOffset = 0;
				body.secBufferLength = 0;
			}

			writer.WriteSessionReqHdr(body);
			if (this.secToken != null)
				writer.WriteBytes(this.secToken);
		}
	}

	[Flags]
	enum Smb2SessionReqFlags : byte
	{
		None = 0,

		Binding = 1,
	}

	[Flags]
	enum Smb2SessionCaps : uint
	{
		None = 0,

		Dfs = 1,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2SessionRequestBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2SessionRequestBody);

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal Smb2SessionReqFlags flags;
		internal byte securityMode;
		internal Smb2SessionCaps caps;
		internal int channel;
		internal ushort secBufferOffset;
		internal ushort secBufferLength;
		internal ulong prevSessionId;
	}
}
