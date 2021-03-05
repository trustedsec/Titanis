using System;
using System.Buffers.Binary;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.Crypto;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Base implementation for RC4-HMAC.
	/// </summary>
	// [RFC 4757]
	public abstract class Rc4HmacBase : EncProfile
	{

		public const int Rc4HmacProtocolKeySizeBytes = 128 / 8;
		public const int Rc4HmacSpecificKeySizeBytes = Rc4HmacProtocolKeySizeBytes * 2;

		/// <inheritdoc/>
		public sealed override int KeyBits => Rc4HmacProtocolKeySizeBytes * 8;
		/// <inheritdoc/>
		public sealed override int MessageBlockSize => 1;
		/// <inheritdoc/>
		protected sealed override int SpecificKeySizeBytes => Rc4HmacSpecificKeySizeBytes;
		public sealed override int SignTokenSize => WrapToken.StructSize;

		[InlineArray(Rc4HmacProtocolKeySizeBytes)]
		private protected struct SpecificKey
		{
			public byte key;
		}
		private protected struct Keys
		{
			internal SpecificKey k1;
			internal SpecificKey k2;
		}

		/// <inheritdoc/>
		public sealed override void RandomToKey(ReadOnlySpan<byte> input, Span<byte> keyBuffer)
		{
			// TODO: Check buffers
			input.CopyTo(keyBuffer);
		}

		const int ConfounderSizeBytes = 8;
		const int EncryptionChecksumSizeBytes = 16;

		private const int Rc4HmacHeaderSize = ConfounderSizeBytes + EncryptionChecksumSizeBytes;

		/// <inheritdoc/>
		/// <remarks>
		/// This implementation returns <c>8</c>.
		/// </remarks>
		public sealed override int CipherHeaderSizeBytes => Rc4HmacHeaderSize;

		/// <inheritdoc/>
		public sealed override int SealHeaderSize => 0;
		/// <inheritdoc/>
		public sealed override int SealTrailerSize => 0;

		/// <summary>
		/// Describes the structure of a sealed message.
		/// </summary>
		struct CiphertextStruct
		{
			private unsafe fixed byte checksum[16];
			private unsafe fixed byte confounder[8];
			private unsafe fixed byte data[1];
		}
		/// <inheritdoc/>
		public sealed override void Encrypt(
			ReadOnlySpan<byte> protocolKey,
			KeyUsage usage,
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer)
		{
			if (header.Length != Rc4HmacHeaderSize)
				throw new ArgumentException("The provided header is not the correct size.", nameof(header));
			if (trailer.Length != 0)
				throw new ArgumentException("The provided MAC buffer is not the correct size.", nameof(trailer));

			var confounder = header.Slice(ChecksumSizeBytes, ConfounderSizeBytes);
			GetRandomBytes(confounder);

			int messageSize = ConfounderSizeBytes + buffers.TotalPrivacyLength;
			byte[] cipherbuf = new byte[messageSize];
			confounder.CopyTo(cipherbuf);
			buffers.CopySectionTo(MessageSecBufferOptions.Privacy, 0, cipherbuf.Slice(ConfounderSizeBytes));

			// Compute checksum
			var checksum = header.Slice(0, ChecksumSizeBytes);
			Keys keys = new Keys();
			this.DeriveKey(protocolKey, usage, ref keys);
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				keys.k2,
				cipherbuf,
				checksum);
			//this.ComputeChecksum(keys.k2, confounder, in buffers, default, checksum);

			Span<byte> k3 = stackalloc byte[Rc4HmacProtocolKeySizeBytes];
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				keys.k1,
				checksum,
				k3);

			Rc4Context ctx = new Rc4Context(k3);
			ctx.Transform(cipherbuf, cipherbuf);
			cipherbuf.SliceReadOnly(0, ConfounderSizeBytes).CopyTo(confounder);
			buffers.CopySectionFrom(cipherbuf.Slice(ConfounderSizeBytes), MessageSecBufferOptions.Privacy, 0);
		}

		/// <inheritdoc/>
		public sealed override void Decrypt(ReadOnlySpan<byte> protocolKey, KeyUsage usage, Span<byte> header, in SecBufferList buffers, Span<byte> trailer)
		{
			const int HeaderSize = EncryptionChecksumSizeBytes + ConfounderSizeBytes;
			if (header.Length != HeaderSize)
				throw new ArgumentException("The ciphertext buffer is too short.", nameof(header));

			var checksum = header.Slice(0, ChecksumSizeBytes);
			Keys keys = new Keys();
			this.DeriveKey(protocolKey, usage, ref keys);
			Span<byte> k3 = stackalloc byte[Rc4HmacProtocolKeySizeBytes];
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				keys.k1,
				checksum,
				k3
				);

			var confounder = header.Slice(ChecksumSizeBytes, ConfounderSizeBytes);

			int messageSize = ConfounderSizeBytes + buffers.TotalPrivacyLength;
			byte[] buf = new byte[messageSize];
			confounder.CopyTo(buf);
			buffers.CopySectionTo(MessageSecBufferOptions.Privacy, 0, buf.Slice(ConfounderSizeBytes));

			Rc4Context ctx = new Rc4Context(k3);
			ctx.Transform(buf, buf);

			Span<byte> computedChecksum = stackalloc byte[ChecksumSizeBytes];
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				keys.k2,
				buf,
				computedChecksum);
			CompareChecksums(checksum, computedChecksum);

			buffers.CopySectionFrom(buf.Slice(ConfounderSizeBytes), MessageSecBufferOptions.Privacy, 0);
		}

		/// <inheritdoc/>
		public sealed override int ChecksumSizeBytes => 128 / 8;

		/// <inheritdoc/>
		protected sealed override void DeriveSpecificKey(
			ReadOnlySpan<byte> protocolKey,
			KeyUsage usage,
			KeyIntent intent,
			Span<byte> specificKeyBuffer)
		{
			Keys keys = MemoryMarshal.AsRef<Keys>(specificKeyBuffer);
			this.DeriveKey(protocolKey, usage, ref keys);
		}

		/// <summary>
		/// Gets the message type constant.
		/// </summary>
		/// <param name="usage">Key usage</param>
		/// <returns>Message type constant</returns>
		// [RFC 4757] § 3. Basic Operation
		private static int GetT(KeyUsage usage)
			=> usage switch
			{

				// 1.
				KeyUsage.AsreqPaEncTimestamp => 1,
				// 2.
				KeyUsage.Asrep_Tgsrep_Ticket => 2,
				// 3.
				KeyUsage.AsrepEncPart => 8,
				// 4.
				KeyUsage.TgsReq_KdcReqBody_AuthData_SessionKey => 4,
				// 5.
				KeyUsage.TgsReq_KdcReqBody_AuthData_AuthSubkey => 5,
				// 6.
				KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey => 6,
				// 7.
				KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey => 7,
				// 8.
				KeyUsage.TgsrepEncPart_SessionKey => 8,
				// 9.
				KeyUsage.TgsrepEncPart_AuthSubkeyKey => 8,


				// 10.
				KeyUsage.ApreqAuthChecksum_AppSessionKey => 10,
				// 11.
				KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey => 11,
				// 12.
				KeyUsage.APRep_EncPart => 12,
				// 13.
				KeyUsage.Priv => 13,
				// 14.
				KeyUsage.Cred => 14,
				// 15.
				KeyUsage.Safe or KeyUsage.InitiatorSign or KeyUsage.AcceptorSign => 15,
				_ => throw new ArgumentOutOfRangeException(nameof(usage))
			};

		/// <inheritdoc/>
		protected sealed override void ComputeChecksum(
			ReadOnlySpan<byte> specificKey,
			ReadOnlySpan<byte> confounder,
			in SecBufferList bufferList,
			ReadOnlySpan<byte> micTokenHeader,
			Span<byte> checksum
			)
		{
			Md5Context ctx = new Md5Context();
			ctx.Initialize();
			ctx.HashData(confounder);
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				var buf = bufferList.GetBuffer(i);
				if (buf.ShouldSign)
					ctx.HashData(buf.ReadOnlySpan);
			}
			ctx.HashData(micTokenHeader);

			ctx.HashFinal(checksum);
		}

		/// <summary>
		/// Derives keys from a protocol key.
		/// </summary>
		/// <param name="protocolKey">Protocol key</param>
		/// <param name="usage"></param>
		/// <returns></returns>
		private protected void DeriveKey(ReadOnlySpan<byte> protocolKey, KeyUsage usage, ref Keys keyBuffer)
			=> this.DeriveKey(protocolKey, GetT(usage), ref keyBuffer);
		private protected abstract void DeriveKey(ReadOnlySpan<byte> protocolKey, int T, ref Keys keyBuffer);

		/// <inheritdoc/>
		public sealed override SessionKey StringToKey(string str, ReadOnlySpan<byte> salt)
			=> new SessionKey(this, this.StringToKey(Encoding.Unicode.GetBytes(str), salt));

		/// <inheritdoc/>
		public sealed override byte[] StringToKey(ReadOnlySpan<byte> str, ReadOnlySpan<byte> salt)
		{
			return SlimHashAlgorithm.ComputeHash<Md4Context>(str);
		}

		#region Signing


		[InlineArray(8)]
		struct WrapMic
		{
			public byte b;
		}
		[InlineArray(8)]
		struct SeqNbrBuffer
		{
			public byte b;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct WrapToken
		{
			public const ushort ValidTokID = 0x0101;

			public static unsafe int StructSize => sizeof(WrapToken);

			public ushort tokID;
			public short alg;
			public uint filler;
			public SeqNbrBuffer seqnbr;
			public WrapMic checksum;
		}

		internal sealed override void SignMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSignParams signParams)
		{
			if (signParams.MacBuffer.Length != WrapToken.StructSize)
				throw new ArgumentException("The MAC buffer is not the correct size.");

			ref var token = ref MemoryMarshal.AsRef<WrapToken>(signParams.MacBuffer);
			token = new WrapToken
			{
				tokID = WrapToken.ValidTokID,
				alg = IPAddress.HostToNetworkOrder((short)0x1100),
				filler = 0xFFFFFFFF,
			};

			ref uint seqnbrP1 = ref MemoryMarshal.AsRef<uint>(token.seqnbr);
			ref uint seqnbrP2 = ref MemoryMarshal.AsRef<uint>(((Span<byte>)token.seqnbr).Slice(4, 4));
			ref ulong seqNbr64 = ref MemoryMarshal.AsRef<ulong>(token.seqnbr);
			seqnbrP1 = (uint)IPAddress.HostToNetworkOrder((int)seqNbr);
			seqnbrP2 = 0xFFFFFFFF;

			Span<byte> ksign = stackalloc byte[Rc4HmacProtocolKeySizeBytes];
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				sessionKey,
				Encoding.ASCII.GetBytes("signaturekey\0"),
				ksign);

			Span<byte> checksumBuf = stackalloc byte[16];
			Span<byte> salt = stackalloc byte[8 + 4];
			signParams.MacBuffer.Slice(0, 8).CopyTo(salt);
			salt[8 + 0] = 15;
			this.ComputeChecksum(default, salt, signParams.bufferList, default, checksumBuf);
			checksumBuf.Slice(0, 8).CopyTo(token.checksum);

			Span<byte> kseq = stackalloc byte[16];
			this.DeriveSeqnbrKey(sessionKey, kseq);

			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				kseq,
				token.checksum,
				kseq);

			Rc4.Transform(kseq, token.seqnbr);
		}

		private void DeriveSeqnbrKey(ReadOnlySpan<byte> kss, Span<byte> kseq)
		{
			Span<byte> zero = stackalloc byte[4];
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				kss,
				zero,
				kseq);
		}
		#endregion

		/// <inheritdoc/>
		internal sealed override void SealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		internal sealed override void UnsealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}
	}

	// [RFC 4757]
	public sealed class Rc4Hmac : Rc4HmacBase
	{
		internal sealed override EType EType => EType.Rc4Hmac;

		private protected sealed override void DeriveKey(ReadOnlySpan<byte> protoKey, int T, ref Keys keyBuffer)
		{
			Span<byte> usageBuf = stackalloc byte[4];
			BinaryPrimitives.WriteInt32LittleEndian(usageBuf, T);
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				protoKey,
				MemoryMarshal.AsBytes(usageBuf),
				keyBuffer.k1
				);
			for (int i = 0; i < Rc4HmacProtocolKeySizeBytes; i++)
			{
				keyBuffer.k2[i] = keyBuffer.k1[i];
			}
		}
	}

	// [RFC 4757]
	public sealed class Rc4HmacExp : Rc4HmacBase
	{
		/// <inheritdoc/>
		internal sealed override EType EType => EType.Rc4HmacExp;

		// [RFC 4757] § 5. Encryption Types
		private static readonly byte[] FortyBits = new byte[] {
			(byte)'f', (byte)'o', (byte)'r', (byte)'t', (byte)'y', (byte)'b', (byte)'i', (byte)'t', (byte)'s',
			0,0,0,0,0
		};

		/// <inheritdoc/>
		private protected sealed override void DeriveKey(ReadOnlySpan<byte> protoKey, int T, ref Keys keyBuffer)
		{
			Span<byte> seed = stackalloc byte[14];
			FortyBits.CopyTo(seed);
			BinaryPrimitives.WriteInt32LittleEndian(seed.Slice(10, 4), T);
			SlimHashAlgorithm.ComputeHmac<Md5Context>(
				protoKey,
				seed,
				keyBuffer.k2
				);
			Span<byte> K1 = keyBuffer.k1;
			for (int i = 0; i < 7; i++)
			{
				K1[i] = keyBuffer.k2[i];
			}
			for (int i = 7; i < K1.Length; i++)
			{
				K1[i] = 0xAB;
			}
		}
	}
}