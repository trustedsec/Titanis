using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.3 SMB2 NEGOTIATE Request
	sealed class Smb2NegotiateRequest : Smb2Pdu<Smb2NegotiateRequestBody>
	{
		internal Smb2NegotiateRequest(Smb2Dialect[] dialects)
		{
			Debug.Assert(!dialects.IsNullOrEmpty());

			this.Dialects = dialects;
		}

		public Smb2Dialect[] Dialects { get; private set; }
		public Smb2NegotiateContext[] contexts;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Negotiate;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Negotiate;

		/// <inheritdoc/>
		protected override ushort ValidBodySize => 36;
		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2NegotiateRequestBody hdr)
		{
			int startPos = writer.Position - Smb2PduSyncHeader.StructSize;
			int offHdr = writer.AllocNegReqHdr();

			hdr.dialectCount = (short)this.Dialects.Length;
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<Smb2Dialect, ushort>(this.Dialects));

			if (this.contexts != null)
			{
				writer.Align(8, startPos);

				hdr.negotiateContextOffset = (writer.Position - startPos);
				hdr.negotiateContextCount = (short)this.contexts.Length;

				foreach (var context in this.contexts)
				{
					writer.Align(8, startPos);

					int offCtxHdr = writer.AllocNegContextHdr();

					int offContextData = writer.Position;
					context.WriteTo(writer);

					int savePos = writer.Position;
					writer.SetPosition(offCtxHdr);
					writer.WriteNegContextHdr(new Smb2NegotiateContextHeader
					{
						contextType = context.ContextType,
						dataLength = (short)(savePos - offContextData)
					});
					writer.SetPosition(savePos);
				}
			}

			{
				int savePos = writer.Position;
				writer.SetPosition(offHdr);
				writer.WriteNegReqHdr(hdr);
				writer.SetPosition(savePos);
			}
		}

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			throw new NotImplementedException();
		}
	}

	// REF: [MS-SMB2] § 2.2.3 SMB2 NEGOTIATE Request
	// https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-smb2/e14db7ff-763a-4263-8b10-0c3944f52fc5
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2NegotiateRequestBody : ISmb2PduStruct
	{
		public unsafe static ushort StructSize => (ushort)sizeof(Smb2NegotiateRequestBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal short dialectCount;
		internal Smb2SecurityMode securityMode;
		internal short reserved;
		internal Smb2Capabilities caps;
		internal Guid clientGuid;
		//internal ulong clientStartTime;
		internal int negotiateContextOffset;
		internal short negotiateContextCount;
		internal short reserved2;
	}

	abstract class Smb2NegotiateContext
	{
		internal abstract Smb2NegotiateContextType ContextType { get; }

		internal abstract void ApplyTo(Smb2NegotiateResponse neg);
		internal abstract void ReadFrom(ByteMemoryReader reader, short dataLength);
		internal abstract void WriteTo(ByteWriter writer);
	}

	class PreauthIntegrityCapsContext : Smb2NegotiateContext
	{
		internal PreauthIntegrityCapsContext()
		{

		}
		internal PreauthIntegrityCapsContext(PreauthHashAlgorithm[] algorithms, byte[] salt)
		{
			if (algorithms.IsNullOrEmpty()) throw new ArgumentNullException(nameof(algorithms));
			if (algorithms.Length > ushort.MaxValue)
				throw new ArgumentException($"The number of algorithms exceeded the limit.", nameof(algorithms));

			this.Algorithms = algorithms;
			this.Salt = salt;
		}


		internal PreauthHashAlgorithm[] Algorithms { get; private set; }
		internal byte[] Salt { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.PreauthIntegrityCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			neg.hashAlgs = this.Algorithms;
			neg.preauthSalt = this.Salt;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			int cAlgs = reader.ReadUInt16();
			int cbSalt = reader.ReadUInt16();

			this.Algorithms = MemoryMarshal.Cast<byte, PreauthHashAlgorithm>(reader.Consume(2 * cAlgs)).ToArray();

			if (cbSalt > 0)
				this.Salt = reader.ReadBytes(cbSalt);
		}

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteUInt16LE((ushort)this.Algorithms.Length);
			writer.WriteUInt16LE((ushort)(this.Salt != null ? Salt.Length : 0));
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<PreauthHashAlgorithm, ushort>(this.Algorithms));
			if (this.Salt != null)
				writer.WriteBytes(this.Salt);
		}
	}

	sealed class SigningCapsContext : Smb2NegotiateContext
	{
		internal SigningCapsContext()
		{
		}

		public SigningCapsContext(SigningAlgorithm[] algorithms)
		{
			if (algorithms.IsNullOrEmpty()) throw new ArgumentNullException(nameof(algorithms));
			if (algorithms.Length > ushort.MaxValue)
				throw new ArgumentException($"The number of signing algorithms exceeded the limit.", nameof(algorithms));

			this.Algorithms = algorithms;
		}

		internal SigningAlgorithm[] Algorithms { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.SigningCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			if (this.Algorithms == null)
				throw new InvalidOperationException();

			neg.signingAlgs = this.Algorithms;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			int cAlg = reader.ReadUInt16();
			this.Algorithms = MemoryMarshal.Cast<byte, SigningAlgorithm>(reader.Consume(2 * cAlg)).ToArray();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			if (this.Algorithms == null)
				throw new InvalidOperationException();

			writer.WriteUInt16LE((ushort)this.Algorithms.Length);
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<SigningAlgorithm, ushort>(this.Algorithms));
		}
	}

	sealed class CipherCapsContext : Smb2NegotiateContext
	{
		internal CipherCapsContext()
		{
		}

		public CipherCapsContext(Cipher[] ciphers)
		{
			if (ciphers.IsNullOrEmpty()) throw new ArgumentNullException(nameof(ciphers));
			if (ciphers.Length > ushort.MaxValue)
				throw new ArgumentException($"The number of ciphers exceeded the limit.", nameof(ciphers));

			this.Ciphers = ciphers;
		}

		internal Cipher[] Ciphers { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.EncryptionCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			if (this.Ciphers == null)
				throw new InvalidOperationException();

			neg.cipherAlgs = this.Ciphers;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			int cAlg = reader.ReadUInt16();
			this.Ciphers = MemoryMarshal.Cast<byte, Cipher>(reader.Consume(2 * cAlg)).ToArray();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			if (this.Ciphers == null)
				throw new InvalidOperationException();

			writer.WriteUInt16LE((ushort)this.Ciphers.Length);
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<Cipher, ushort>(this.Ciphers));
		}
	}

	enum CompressionAlg : ushort
	{
		None = 0,
		Lznt1,
		Lz77 = 2,
		Lz77_Huffman = 3,
		PatternV1 = 4,
		LZ4 = 5
	}

	class CompressionCapsContext : Smb2NegotiateContext
	{
		internal CompressionCapsContext() { }
		public CompressionCapsContext(CompressionCaps caps, CompressionAlgorithm[] algorithms)
		{
			if (algorithms is null) throw new ArgumentNullException(nameof(algorithms));

			this.Capabilities = caps;
			this.Algorithms = algorithms;
		}

		internal CompressionCaps Capabilities { get; private set; }
		internal CompressionAlgorithm[] Algorithms { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.CompressionCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			neg.compressionCaps = this.Capabilities;
			neg.compressionAlgs = this.Algorithms;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			int cAlgs = reader.ReadUInt16();
			reader.Advance(2);
			this.Capabilities = (CompressionCaps)reader.ReadUInt32();
			this.Algorithms = MemoryMarshal.Cast<byte, CompressionAlgorithm>(reader.Consume(2 * cAlgs)).ToArray();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteUInt16LE((ushort)this.Algorithms.Length);
			writer.Advance(2);
			writer.WriteUInt32LE((uint)this.Capabilities);
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<CompressionAlgorithm, ushort>(this.Algorithms));
		}
	}

	class NetNameContext : Smb2NegotiateContext
	{
		public NetNameContext()
		{

		}
		public NetNameContext(string netName)
		{
			this.NetName = netName;
		}
		internal string NetName { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.NetName;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			neg.serverNetName = this.NetName;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			this.NetName = Encoding.Unicode.GetString(reader.Consume(dataLength));
		}

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteStringUni(this.NetName);
		}
	}

	[Flags]
	enum TransportCaps : uint
	{
		None = 0,
		TransportLevelSecurity = 1,
	}

	class TransportCapsContext : Smb2NegotiateContext
	{
		public TransportCapsContext() { }
		public TransportCapsContext(TransportCaps caps) { this.Capabilities = caps; }

		internal TransportCaps Capabilities { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.TransportCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			neg.TransportCapabilities = this.Capabilities;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			this.Capabilities = (TransportCaps)reader.ReadUInt32();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteUInt32LE((uint)this.Capabilities);
		}
	}
	class RdmaTransformCapsContext : Smb2NegotiateContext
	{
		public RdmaTransformCapsContext() { }
		public RdmaTransformCapsContext(RdmaTransformId[] transforms)
		{
			if (transforms is null) throw new ArgumentNullException(nameof(transforms));
			this.Transforms = transforms;
		}

		internal RdmaTransformId[]? Transforms { get; private set; }

		internal override Smb2NegotiateContextType ContextType => Smb2NegotiateContextType.RdmaTransformCaps;

		internal override void ApplyTo(Smb2NegotiateResponse neg)
		{
			neg.rdmaTransforms = this.Transforms;
		}

		internal override void ReadFrom(ByteMemoryReader reader, short dataLength)
		{
			int cAlgs = reader.ReadUInt16();
			reader.Advance(6);
			this.Transforms = MemoryMarshal.Cast<byte, RdmaTransformId>(reader.Consume(2 * cAlgs)).ToArray();
		}

		internal override void WriteTo(ByteWriter writer)
		{
			writer.WriteUInt16LE((ushort)this.Transforms.Length);
			writer.Advance(6);
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<RdmaTransformId, ushort>(this.Transforms));
		}
	}

	enum Smb2NegotiateContextType : ushort
	{
		PreauthIntegrityCaps = 1,
		EncryptionCaps = 2,
		CompressionCaps = 3,
		NetName = 5,
		TransportCaps = 6,
		RdmaTransformCaps = 7,
		SigningCaps = 8,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2NegotiateContextHeader
	{
		public unsafe static int StructSize => sizeof(Smb2NegotiateContextHeader);

		internal Smb2NegotiateContextType contextType;
		internal short dataLength;
		internal int reserved;
	}
}
