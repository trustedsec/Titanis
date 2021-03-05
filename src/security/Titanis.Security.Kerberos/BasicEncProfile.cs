using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	public abstract class BasicEncProfile : EncProfile
	{
		/// <summary>
		/// Gets the size of a cipher block, in bytes.
		/// </summary>
		public abstract int CipherBlockSizeBytes { get; }

		/// <summary>
		/// Gets the number of iterations to execute for key derivation.
		/// </summary>
		public abstract int Iterations { get; }

		/// <inheritdoc/>
		// [RFC 3961] 5.3. Cryptosystem Profile Based on Simplified Profile
		// This is for the confounder
		public sealed override int CipherHeaderSizeBytes => this.CipherBlockSizeBytes;

		/// <inheritdoc/>
		// [RFC 3961] 5.3. Cryptosystem Profile Based on Simplified Profile
		// This is for the checksum
		public sealed override int CipherTrailerSizeBytes => this.ChecksumSizeBytes;

		/// <inheritdoc/>
		public sealed override int SignTokenSize => WrapToken.StructSize + this.ChecksumSizeBytes;

		/// <inheritdoc/>
		protected sealed override int SpecificKeySizeBytes => this.KeySizeBytes;

		// [RFC 4121] § 4.2.4 - Encryption and Checksum Operations
		// [RFC 4121] § 4.2.6.2 - Wrap Tokens
		// [MS-KILE] § 3.4.5.4.1 - Kerberos Binding of GSS_WrapEx()
		/// <inheritdoc/>
		public sealed override int SealHeaderSize => WrapToken.StructSize + this.CipherHeaderSizeBytes /* confounder */;
		/// <inheritdoc/>
		// [RFC 4121] § 4.2.4 - Encryption and Checksum Operations
		// [RFC 4121] § 4.2.6.2 - Wrap Tokens
		// [MS-KILE] § 3.4.5.4.1 - Kerberos Binding of GSS_WrapEx()
		public sealed override int SealTrailerSize => this.CipherBlockSizeBytes + WrapToken.StructSize + this.CipherTrailerSizeBytes;

		#region Key derivation
		private void DeriveKey(ReadOnlySpan<byte> baseKey, ReadOnlySpan<byte> salt, Span<byte> keyBuffer)
			=> this.DR(baseKey, salt, keyBuffer);

		// [RFC 3961] § 5.1. - A Key Derivation Function
		internal void DeriveKey(ReadOnlySpan<byte> baseKey, KeyUsage usage, KeyIntent intent, Span<byte> keyBuffer)
		{
			Span<byte> salt = stackalloc byte[5];
			System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(salt, (int)usage);
			salt[4] = (byte)intent;
			this.DeriveKey(baseKey, salt, keyBuffer);
		}

		// [RFC 3961] § 5.1. - A Key Derivation Function
		protected void DK(ReadOnlySpan<byte> protoKey, ReadOnlySpan<byte> constant, Span<byte> keyBuffer)
		{
			DR(protoKey, constant, keyBuffer);
			RandomToKey(keyBuffer, keyBuffer);
		}

		// [RFC 3961] § 5.1. - A Key Derivation Function
		protected void DR(ReadOnlySpan<byte> protoKey, ReadOnlySpan<byte> constant, Span<byte> keyBuffer)
		{
			int CipherBlockSize = this.CipherBlockSizeBytes;

			Span<byte> keySeed = stackalloc byte[CipherBlockSize];
			NFold.DeriveKey(constant, keySeed);

			var keySize = this.KeySizeBytes;

			// protoKey and keyBuffer may be the same buffer, so don't overwrite
			Span<byte> output = stackalloc byte[KeySizeBytes];
			for (int i = 0; i < keySize; i += CipherBlockSize)
			{
				this.EncryptBlock(protoKey, keySeed);
				int cb = Math.Min(CipherBlockSize, keySize - i);
				keySeed.Slice(0, cb).CopyTo(output.Slice(i, cb));
			}

			output.CopyTo(keyBuffer);
		}

		protected sealed override void DeriveSpecificKey(
			ReadOnlySpan<byte> protocolKey,
			KeyUsage usage,
			KeyIntent intent,
			Span<byte> specificKeyBuffer
			)
			=> this.DeriveKey(protocolKey, usage, intent, specificKeyBuffer);

		public sealed override byte[] StringToKey(ReadOnlySpan<byte> str, ReadOnlySpan<byte> salt)
		{
			Rfc2898DeriveBytes pbkdf = new Rfc2898DeriveBytes(
				str.ToArray(),
				salt.ToArray(),
				this.Iterations
				);
			byte[] key = pbkdf.GetBytes(this.KeySizeBytes);
			DK(key, Encoding.UTF8.GetBytes("kerberos"), key);
			return key;
		}
		#endregion

		#region Encrypt

		protected abstract void EncryptBlock(ReadOnlySpan<byte> key, Span<byte> bytes);
		/// <summary>
		/// Encrypts data using the underlying cryptographic algorithm.
		/// </summary>
		/// <param name="cipherKey"></param>
		/// <param name="confounder"></param>
		/// <param name="bufferList"></param>
		protected abstract void EncryptCore(
			ReadOnlySpan<byte> cipherKey,
			Span<byte> confounder,
			in SecBufferList bufferList
			);
		public sealed override void Encrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer
			)
		{
			// [RFC 3961] 5.3. Cryptosystem Profile Based on Simplified Profile

			if (header.Length != this.CipherHeaderSizeBytes)
				throw new ArgumentException("The header provided for encryption must be the length of CipherHeaderSize", nameof(header));
			if (trailer.Length != this.CipherTrailerSizeBytes)
				throw new ArgumentException("The trailer provided for encryption must be the length of CipherTrailerSize", nameof(trailer));

			// header is the confounder
			GetRandomBytes(header);

			// Checksum
			Span<byte> hashKey = stackalloc byte[this.SpecificKeySizeBytes];
			this.DeriveKey(protocolKey, usage, KeyIntent.Integrity, hashKey);
			this.ComputeChecksum(hashKey, header, buffers, default, trailer);

			// Encryption
			Span<byte> cipherKey = stackalloc byte[this.SpecificKeySizeBytes];
			this.DeriveKey(protocolKey, usage, KeyIntent.Encryption, cipherKey);
			this.EncryptCore(cipherKey, header, buffers);
		}

		public static void EncryptCts<THandler>(
			Span<byte> confounder,
			in SecBufferList bufferList,
			ref THandler handler)
			where THandler : struct, ICryptHandler
		{
			// [RFC 3962] § 5
			// Of note, this RFC requires the last 2 blocks be swapped regardless of size

			Debug.Assert(confounder.Length == handler.BlockSizeBytes);

			int messageSize = confounder.Length + bufferList.TotalPrivacyLength;
			if (messageSize < handler.BlockSizeBytes)
				throw new NotImplementedException();

			// TODO: Transform in-place without byte[]
			// Set up message buffer
			byte[] cipherBuf = new byte[messageSize];
			confounder.CopyTo(cipherBuf);
			bufferList.CopySectionTo(MessageSecBufferOptions.Privacy, 0, cipherBuf.Slice(handler.BlockSizeBytes));

			if (cipherBuf.Length <= handler.BlockSizeBytes)
			{
				// TODO: This shouldn't happen with the confounder
				handler.TransformBlock(cipherBuf, cipherBuf);
			}
			else
			{
				// The '- 1' treats the last block as a partial block so that it gets swapped
				int fullBlockCount = (cipherBuf.Length - 1) / handler.BlockSizeBytes;

				// Chop off the last full block where "last block" excludes the real last block
				// if the data is an even multiple of the cipher block size, thus leaving
				// 2 blocks remaining
				int cbFull = handler.BlockSizeBytes * (fullBlockCount - 1);

				// Encrypt up to last 2 blocks
				if (cbFull > 0)
					handler.TransformBlock(cipherBuf.Slice(0, cbFull), cipherBuf.Slice(0, cbFull));

				// Calculate the size of the tail
				int sizeOfTail = (cipherBuf.Length % handler.BlockSizeBytes);
				if (sizeOfTail == 0)
					// Since the tail mail be a full block
					sizeOfTail = handler.BlockSizeBytes;

				// Penult
				var penult = cipherBuf.Slice(cbFull, handler.BlockSizeBytes);
				handler.TransformBlock(penult, penult);

				// Stage the tail
				int offTail = cipherBuf.Length - sizeOfTail;
				Span<byte> tailBuf = stackalloc byte[handler.BlockSizeBytes];
				cipherBuf.Slice(offTail, sizeOfTail).CopyTo(tailBuf);

				// Write the penult
				penult.Slice(0, sizeOfTail).CopyTo(cipherBuf.Slice(offTail, sizeOfTail));

				// Encrypt the block with the stolen data
				handler.TransformBlock(tailBuf, penult);
			}

			cipherBuf.Slice(0, confounder.Length).CopyTo(confounder);
			bufferList.CopySectionFrom(cipherBuf.Slice(confounder.Length), MessageSecBufferOptions.Privacy, 0);
		}
		#endregion

		public sealed override void Decrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer
			)
		{
			if (
				(header.Length < this.CipherHeaderSizeBytes)
				|| (trailer.Length < this.CipherTrailerSizeBytes)
				)
				throw new SecurityException(Messages.Krb5_InvalidCiphertext);


			// Encryption
			Span<byte> cipherKey = stackalloc byte[this.SpecificKeySizeBytes];
			this.DeriveKey(protocolKey, usage, KeyIntent.Encryption, cipherKey);
			this.DecryptCore(cipherKey, header, buffers);

			// Checksum
			Span<byte> hashKey = stackalloc byte[this.SpecificKeySizeBytes];
			this.DeriveKey(protocolKey, usage, KeyIntent.Integrity, hashKey);
			this.VerifyChecksum(hashKey, header, buffers, default, trailer);
		}

		protected abstract void DecryptCore(
			ReadOnlySpan<byte> specificKey,
			Span<byte> confounder,
			in SecBufferList ciphertext);


		public static void DecryptCts<THandler>(
			Span<byte> confounder,
			in SecBufferList bufferList,
			ref THandler handler
			)
			where THandler : struct, ICryptHandler
		{
			Debug.Assert(confounder.Length == handler.BlockSizeBytes);

			int messageSize = confounder.Length + bufferList.TotalPrivacyLength;
			if (messageSize < handler.BlockSizeBytes)
				throw new NotImplementedException();

			// TODO: Do this without copying to a temporary array
			// Set up message buffer
			byte[] cipherBuf = new byte[messageSize];
			confounder.CopyTo(cipherBuf);
			bufferList.CopySectionTo(MessageSecBufferOptions.Privacy, 0, cipherBuf.Slice(confounder.Length));

			if (cipherBuf.Length <= handler.BlockSizeBytes)
			{
				//byte[] plaintext = new byte[count];
				//transform.TransformBlock(ciphertext, plaintext);
				//return plaintext;
				throw new NotImplementedException();
			}
			else
			{
				// The -1 below handles the case where the plaintext is a multiple of the cipher block size
				int fullBlockCount = (cipherBuf.Length - 1) / handler.BlockSizeBytes;

				int cbFull = handler.BlockSizeBytes * (fullBlockCount - 1);

				Span<byte> cbcState = stackalloc byte[handler.BlockSizeBytes];
				Span<byte> cbcState2 = stackalloc byte[handler.BlockSizeBytes];

				// Up to last 2 blocks
				int cbTransformed = 0;
				for (; cbTransformed < cbFull; cbTransformed += handler.BlockSizeBytes)
				{
					var cipher = cipherBuf.Slice(cbTransformed, handler.BlockSizeBytes);
					cipher.CopyTo(cbcState2);

					handler.TransformBlock(cipher, cipher);
					XorBlocks(handler.BlockSizeBytes, cbcState, cipher);
					cbcState2.CopyTo(cbcState);
				}

				int sizeOfTail = messageSize % handler.BlockSizeBytes;
				if (sizeOfTail == 0)
					sizeOfTail = handler.BlockSizeBytes;

				// Decrypt the penult which is actually the tail
				Span<byte> penult = cipherBuf.Slice(cbTransformed, handler.BlockSizeBytes);
				Span<byte> tailBuf = stackalloc byte[handler.BlockSizeBytes];
				handler.TransformBlock(penult, tailBuf);

				// XOR tailBuf with the tail, which is actually the penult cipher
				var offTail = messageSize - sizeOfTail;
				var tail = cipherBuf.Slice(offTail, sizeOfTail);
				for (int i = 0; i < sizeOfTail; i++)
				{
					tailBuf[i] ^= tail[i];
				}

				// tailBuf now contains the fully-decrypted tail
				// Assemble the penult ciphertext
				Span<byte> penultBuf = cbcState2;
				tail.CopyTo(penultBuf);
				tailBuf.Slice(sizeOfTail).CopyTo(penultBuf.Slice(sizeOfTail));

				// Decrypt penult
				handler.TransformBlock(penultBuf, penult);
				XorBlocks(handler.BlockSizeBytes, cbcState, penult);
				// Penult is done

				// Deploy tail
				tailBuf.Slice(0, sizeOfTail).CopyTo(tail);
			}

			cipherBuf.Slice(0, confounder.Length).CopyTo(confounder);
			bufferList.CopySectionFrom(cipherBuf.Slice(confounder.Length), MessageSecBufferOptions.Privacy, 0);
		}

		private static void XorBlocks(
			int blockSize,
			Span<byte> cbcState,
			Span<byte> dataBlock)
		{
			if (blockSize == 16)
			{
				var vec = Vector128.Create<byte>(dataBlock);
				vec ^= Vector128.Create<byte>(cbcState);
				vec.CopyTo(dataBlock);
			}
			else
			{
				for (int iByte = 0; iByte < blockSize; iByte++)
				{
					dataBlock[iByte] ^= cbcState[iByte];
				}
			}
		}




		#region Message security
		// [RFC 4121] § 4.2.4. Encryption and Checksum Operations
		internal sealed override void SealMessage(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSealParams sealParams)
		{
			Debug.Assert(0 != (flags & WrapFlags.Sealed));
			Debug.Assert(usage is KeyUsage.InitiatorSeal or KeyUsage.AcceptorSeal);

			// TODO: Incorrect error message
			//if (sealParams.Header.Length < this.SealHeaderSize)
			//	throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(sealParams));
			if (sealParams.Trailer.Length < (this.SealTrailerSize + this.SealHeaderSize))
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(sealParams));

			ref WrapToken clearToken = ref MemoryMarshal.AsRef<WrapToken>(sealParams.Trailer.Slice(0, WrapToken.StructSize));

			var cbBlock = this.CipherBlockSizeBytes;

			var wrapTrailer = sealParams.Trailer.Slice(WrapToken.StructSize, cbBlock + WrapToken.StructSize);
			var checksum = sealParams.Trailer.Slice(WrapToken.StructSize + wrapTrailer.Length, this.ChecksumSizeBytes);
			var confounder = sealParams.Trailer.Slice(WrapToken.StructSize + wrapTrailer.Length + checksum.Length, cbBlock);

			ref WrapToken encToken = ref MemoryMarshal.AsRef<WrapToken>(wrapTrailer.Slice(cbBlock, WrapToken.StructSize));
			encToken = new WrapToken
			{
				TokID = WrapTokenType.Wrap,
				flags = flags,
				filler_FF = 0xFF,
				ExtraCount = (ushort)cbBlock,
				Rrc = 0,
				SeqNbr = seqNbr
			};
			clearToken = encToken;
			clearToken.Rrc = (ushort)(cbBlock + this.ChecksumSizeBytes);

			var combined = sealParams.bufferList.WithCombined(
				default,
				SecBuffer.PrivacyWithIntegrity(wrapTrailer)
				);
			this.Encrypt(sessionKey, usage, confounder, combined, checksum);
		}

		internal sealed override void UnsealMessage(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSealParams sealParams)
		{
			Debug.Assert(0 != (flags & WrapFlags.Sealed));
			Debug.Assert(usage is KeyUsage.InitiatorSeal or KeyUsage.AcceptorSeal);
			Debug.Assert((usage == KeyUsage.AcceptorSeal) == (0 != (flags & WrapFlags.SentByAcceptor)));

			if (sealParams.Trailer.Length < this.SealTrailerSize)
				// TODO: Message
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(sealParams));

			//ref WrapToken clearToken = ref MemoryMarshal.AsRef<WrapToken>((sealParams.Header.Length == 0)
			//	? sealParams.Trailer.Slice(0, WrapToken.StructSize)
			//	: sealParams.Header.Slice(0, WrapToken.StructSize)
			//	);
			ref WrapToken clearToken = ref MemoryMarshal.AsRef<WrapToken>(sealParams.Trailer.Slice(0, WrapToken.StructSize));

			// First handle the WrapToken
			var ec = clearToken.ExtraCount;
			bool isClearTokenValid =
				(clearToken.TokID == WrapTokenType.Wrap)
				&& (clearToken.flags == flags)
				&& (clearToken.filler_FF == 0xFF)
				&& (ec == this.CipherBlockSizeBytes)
				&& (clearToken.Rrc == (clearToken.ExtraCount + this.ChecksumSizeBytes))
				&& (clearToken.SeqNbr == seqNbr)
				;
			if (!isClearTokenValid)
				throw new SecurityException(Messages.Krb5_InvalidSealedMessage);

			var cbBlock = this.CipherBlockSizeBytes;

			if (sealParams.Trailer.Length < (WrapToken.StructSize /* the cealr one */ + ec + WrapToken.StructSize /* the encrypted one */ + this.ChecksumSizeBytes + cbBlock /* confounder */))
				// TODO: Message
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(sealParams));

			// Because of [MS-KILE] and [MS-RPCE], here is the actual layout
			//   Plaintext WrapToken
			//   Padding <EC>
			//   Encrypted WrapToken
			//   Checksum
			//   Confounder

			var wrapTrailer = sealParams.Trailer.Slice(WrapToken.StructSize, ec + WrapToken.StructSize);
			var checksum = sealParams.Trailer.Slice(WrapToken.StructSize + wrapTrailer.Length, this.ChecksumSizeBytes);
			var confounder = sealParams.Trailer.Slice(WrapToken.StructSize + wrapTrailer.Length + checksum.Length, cbBlock);

			var combined = sealParams.bufferList.WithCombined(
				default,
				SecBuffer.PrivacyWithIntegrity(wrapTrailer)
				);
			this.Decrypt(sessionKey, usage, confounder, combined, checksum);

			ref WrapToken encToken = ref MemoryMarshal.AsRef<WrapToken>(wrapTrailer.Slice(ec, WrapToken.StructSize));
			bool isEncTokenValid =
				(encToken.TokID == WrapTokenType.Wrap)
				&& (encToken.flags == clearToken.flags)
				&& (encToken.filler_FF == 0xFF)
				&& (encToken.ExtraCount == ec)
				&& (encToken.SeqNbr == seqNbr)
				&& (encToken.Rrc == 0)
				;
			if (!isClearTokenValid)
				throw new SecurityException(Messages.Krb5_InvalidSealedMessage);
		}
		#endregion
	}

	[InlineArray(16)]
	struct CipherBlock16
	{
		public byte b;
	}

	public struct EncryptCtsHandler : ICryptHandler
	{
		internal readonly SymmetricAlgorithm algo;
		private CipherBlock16 cbcBuffer;

		public EncryptCtsHandler(SymmetricAlgorithm algo, Span<byte> cbcBuffer)
		{
			Debug.Assert((cbcBuffer.Length * 8) == algo.BlockSize);
			this.algo = algo;
		}

		public int BlockSizeBytes => this.algo.BlockSize / 8;

		public void TransformBlock(ReadOnlySpan<byte> source, Span<byte> target)
		{
			Debug.Assert(source.Length == target.Length);
			Debug.Assert(0 == (source.Length % this.BlockSizeBytes));
			int cbEncrypted = this.algo.EncryptCbc(source, this.cbcBuffer, target, PaddingMode.None);
			Debug.Assert(cbEncrypted == source.Length);

			if (target.Length > 0)
				target.Slice(target.Length - this.BlockSizeBytes, this.BlockSizeBytes).CopyTo(this.cbcBuffer);
		}
	}

	public struct DecryptCtsHandler : ICryptHandler
	{
		internal readonly SymmetricAlgorithm algo;

		public DecryptCtsHandler(SymmetricAlgorithm algo)
		{
			Debug.Assert(algo.Mode == CipherMode.ECB);
			this.algo = algo;
		}

		public int BlockSizeBytes => this.algo.BlockSize / 8;

		public void TransformBlock(ReadOnlySpan<byte> source, Span<byte> target)
		{
			this.algo.DecryptEcb(source, target, PaddingMode.None);
		}
	}
}
