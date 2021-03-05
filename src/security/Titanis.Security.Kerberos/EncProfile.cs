using System;
using System.Buffers.Binary;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Titanis.Crypto;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Base class for Kerberos encryption profiles.
	/// </summary>
	/// <remarks>
	/// This class fulfills the role of an encryption algorithm profile described
	/// in [RFC 3961] § 2
	/// </remarks>
	public abstract class EncProfile
	{
		internal readonly static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		/// <summary>
		/// Gets random bytes.
		/// </summary>
		/// <param name="buffer"></param>
		public static void GetRandomBytes(Span<byte> buffer)
		{
			//#if DEBUG
			//			buffer.Fill(0xAA);
			//#else
			rng.GetBytes(buffer);
			//#endif
		}

		/// <summary>
		/// Converts a sequence of bytes into a key.
		/// </summary>
		/// <param name="input">Input bytes</param>
		/// <returns>The key data</returns>
		/// <remarks>
		/// Please appreciate the randomness of each byte equally without showing favor to any particular byte.
		/// </remarks>
		public byte[] RandomToKey(ReadOnlySpan<byte> input)
		{
			byte[] keybuf = new byte[this.KeySizeBytes];
			this.RandomToKey(input, keybuf);
			return keybuf;
		}
		public abstract void RandomToKey(ReadOnlySpan<byte> input, Span<byte> keyBuffer);
		/// <summary>
		/// Generates a subkey.
		/// </summary>
		/// <returns>A <see cref="SessionKey"/></returns>
		public SessionKey GenerateSubkey()
		{
			Debug.Assert(this.KeySizeBytes > 0);
			Span<byte> key = stackalloc byte[this.KeySizeBytes];
			GetRandomBytes(key);

			return new SessionKey(
				this,
				this.RandomToKey(key)
				);
		}

		/// <summary>
		/// Gets the size that a plaintext must be padded to for encryption.
		/// </summary>
		/// <remarks>
		/// A value of <c>1</c> indicates a stream cipher.
		/// </remarks>
		public abstract int MessageBlockSize { get; }
		/// <summary>
		/// Gets the key size, in bits.
		/// </summary>
		public abstract int KeyBits { get; }
		/// <summary>
		/// Gets the key size, in bytes.
		/// </summary>
		public int KeySizeBytes => this.KeyBits / 8;
		/// <summary>
		/// Gets the checksum size, in bytes.
		/// </summary>
		public abstract int ChecksumSizeBytes { get; }

		/// <summary>
		/// Gets the size of the SignToken, in bytes.
		/// </summary>
		// [RFC 4121] § 4.2.6.1 - MIC Tokens
		public abstract int SignTokenSize {get;}

		/// <summary>
		/// Gets the size required for the sealing token header.
		/// </summary>
		// [RFC 4121] § 4.2.4 - Encryption and Checksum Operations
		// [RFC 4121] § 4.2.6.2 - Wrap Tokens
		public abstract int SealHeaderSize { get; }
		/// <summary>
		/// Gets the size required for the sealing token trailer.
		/// </summary>
		// [RFC 4121] § 4.2.4 - Encryption and Checksum Operations
		// [RFC 4121] § 4.2.6.2 - Wrap Tokens
		public abstract int SealTrailerSize { get; }

		/// <summary>
		/// Gets the size of the header required for encryption, in bytes.
		/// </summary>
		public abstract int CipherHeaderSizeBytes { get; }
		/// <summary>
		/// Gets the size of the trailer required for encryption, in bytes.
		/// </summary>
		public virtual int CipherTrailerSizeBytes => 0;

		/// <summary>
		/// Gets the <see cref="EType"/> value for this encryption profile.
		/// </summary>
		internal abstract EType EType { get; }
		/// <summary>
		/// Converts a string to a key.
		/// </summary>
		/// <param name="str">String</param>
		/// <param name="salt">Salt</param>
		/// <returns>A <see cref="SessionKey"/> generated from the provided values</returns>
		public virtual SessionKey StringToKey(string str, string salt)
			=> new SessionKey(this, this.StringToKey(Encoding.UTF8.GetBytes(str), Encoding.UTF8.GetBytes(salt)));
		/// <summary>
		/// Converts a string to a key.
		/// </summary>
		/// <param name="str">String</param>
		/// <param name="salt">Salt</param>
		/// <returns>A <see cref="SessionKey"/> generated from the provided values</returns>
		public virtual SessionKey StringToKey(string str, ReadOnlySpan<byte> salt)
			=> new SessionKey(this, this.StringToKey(Encoding.UTF8.GetBytes(str), salt));
		/// <summary>
		/// Converts a string to a key.
		/// </summary>
		/// <param name="str">String</param>
		/// <param name="salt">Salt</param>
		/// <returns>A <see cref="SessionKey"/> generated from the provided values</returns>
		public abstract byte[] StringToKey(ReadOnlySpan<byte> str, ReadOnlySpan<byte> salt);

		protected abstract int SpecificKeySizeBytes { get; }
		/// <summary>
		/// Derives a specific key from a protocol key.
		/// </summary>
		/// <param name="protocolKey">Protocol key</param>
		/// <param name="usage"><see cref="KeyUsage"/> value</param>
		/// <param name="intent"><see cref="KeyIntent"/> value</param>
		/// <returns>A byte array of the specific key bytes</returns>
		protected abstract void DeriveSpecificKey(ReadOnlySpan<byte> protocolKey, KeyUsage usage, KeyIntent intent, Span<byte> specificKeyBuffer);

		internal SessionKey CreateSessionKey(EncryptionKey ekey)
		{
			// Don't actually need the EncryptionKey, just the keyvalue
			// However, keys are conveyed using EncryptionKey, and this allows
			// reuse of the object instead of creating a new one.

			Debug.Assert((EType)ekey.keytype == this.EType);

			var bytes = ekey.keyvalue;
			if (bytes.Length != this.KeySizeBytes)
				throw new ArgumentException("The provided key data does not match the size of key required by this encryption profile.", nameof(bytes));

			return new SessionKey(this, ekey);
		}

		#region Checksum
		#region ComputeChecksum
		public void ComputeChecksum(
			ReadOnlySpan<byte> protocolKey,
			KeyUsage usage,
			KeyIntent intent,
			ReadOnlySpan<byte> confounder,
			in SecBufferList buffers,
			ReadOnlySpan<byte> micTokenHeader,
			Span<byte> checksum
			)
		{
			if (checksum.Length < this.ChecksumSizeBytes)
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(checksum));

			scoped Span<byte> specificKeyBuf;
			scoped ReadOnlySpan<byte> specificKey;
			if (usage != 0)
			{
				specificKeyBuf = stackalloc byte[this.SpecificKeySizeBytes];
				specificKey = specificKeyBuf;
				this.DeriveSpecificKey(protocolKey, usage, intent, specificKeyBuf);
			}
			else
				specificKey = protocolKey;

			this.ComputeChecksum(specificKey, confounder, buffers, micTokenHeader, checksum);
		}

		protected abstract void ComputeChecksum(
			ReadOnlySpan<byte> specificKey,
			ReadOnlySpan<byte> confounder,
			in SecBufferList bufferList,
			ReadOnlySpan<byte> micTokenHeader,
			Span<byte> checksum
			);
		#endregion

		#region VerifyChecksum
		public void VerifyChecksum(
			ReadOnlySpan<byte> protocolKey,
			KeyUsage usage,
			KeyIntent intent,
			in SecBufferList buffers,
			ReadOnlySpan<byte> trailer,
			ReadOnlySpan<byte> checksum
			)
		{
			if (checksum.Length < this.ChecksumSizeBytes)
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(checksum));

			scoped Span<byte> specificKeyBuf;
			scoped ReadOnlySpan<byte> specificKey;
			if (usage != 0)
			{
				specificKeyBuf = stackalloc byte[this.SpecificKeySizeBytes];
				specificKey = specificKeyBuf;
				this.DeriveSpecificKey(protocolKey, usage, intent, specificKeyBuf);
			}
			else
				specificKey = protocolKey;

			this.VerifyChecksum(specificKey, default, buffers, trailer, checksum);
		}
		protected void VerifyChecksum(
			ReadOnlySpan<byte> specificKey,
			ReadOnlySpan<byte> header,
			in SecBufferList bufferList,
			ReadOnlySpan<byte> trailer,
			ReadOnlySpan<byte> checksum
			)
		{
			if (checksum.Length < this.ChecksumSizeBytes)
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(checksum));

			Span<byte> computedChecksum = stackalloc byte[this.ChecksumSizeBytes];
			this.ComputeChecksum(specificKey, header, bufferList, trailer, computedChecksum);

			CompareChecksums(checksum, computedChecksum);
		}

		protected static void CompareChecksums(ReadOnlySpan<byte> checksum, Span<byte> computedChecksum)
		{
			for (int i = 0; i < checksum.Length; i++)
			{
				if (computedChecksum[i] != checksum[i])
					throw new SecurityException(Messages.Krb5_InvalidChecksum);
			}
		}
		#endregion
		#endregion

		/// <summary>
		/// Encrypts data.
		/// </summary>
		/// <param name="protocolKey">Protocol key</param>
		/// <param name="usage">Key usage</param>
		/// <param name="plaintext">Data to encrypt</param>
		/// <returns>A buffer containing the encrypted data</returns>
		public byte[] Encrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			ReadOnlySpan<byte> plaintext)
		{
			if (0 != (plaintext.Length % this.MessageBlockSize))
				throw new ArgumentException("The message size is not a multiple of MessageBlockSize.  It must be padded.", nameof(plaintext));

			byte[] cipherBuf = new byte[this.CipherHeaderSizeBytes + plaintext.Length + this.CipherTrailerSizeBytes];
			var plaintextBuf = cipherBuf.Slice(this.CipherHeaderSizeBytes, plaintext.Length);
			plaintext.CopyTo(plaintextBuf);

			this.Encrypt(
				protocolKey, usage,
				cipherBuf.Slice(0, this.CipherHeaderSizeBytes),
				SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(plaintextBuf)),
				cipherBuf.Slice(this.CipherHeaderSizeBytes + plaintext.Length, this.CipherTrailerSizeBytes)
				);

#if DEBUG
			byte[] copy = (byte[])cipherBuf.Clone();
			var decrypted = this.Decrypt(protocolKey, usage, copy);
			Debug.Assert(decrypted.ToArray().SequenceEqual(plaintext.ToArray()));
#endif

			return cipherBuf;
		}
		public abstract void Encrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer
			);

		public Span<byte> Decrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			Span<byte> cipher
			)
		{
			var message = cipher.Slice(this.CipherHeaderSizeBytes, cipher.Length - this.CipherHeaderSizeBytes - this.CipherTrailerSizeBytes);
			var header = cipher.Slice(0, this.CipherHeaderSizeBytes);
			var trailer = cipher.Slice(this.CipherHeaderSizeBytes + message.Length);
			this.Decrypt(protocolKey, usage, header, SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(message)), trailer);
			return message;
		}
		/// <summary>
		/// Decrypts ciphertext in-place.
		/// </summary>
		/// <param name="protocolKey">Protocol key</param>
		/// <param name="usage">Key usage</param>
		/// <param name="header">Cipher header</param>
		/// <param name="buffers">Buffers containing ciphertext</param>
		/// <param name="trailer">Cipher trailer</param>
		public abstract void Decrypt(
			ReadOnlySpan<byte> protocolKey, KeyUsage usage,
			Span<byte> header,
			in SecBufferList buffers,
			Span<byte> trailer
			);

		#region Message security
		#region Signing
		// [RFC 4121] § 4.2.4. Encryption and Checksum Operations
		internal virtual void SignMessage(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSignParams signParams)
		{
			Debug.Assert(0 == (flags & WrapFlags.Sealed));
			Debug.Assert(usage is KeyUsage.InitiatorSign or KeyUsage.AcceptorSign);

			if (signParams.MacBuffer.Length != this.SignTokenSize)
				// TODO: Update error to reflect exact match requirement
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(signParams));

			var clearTokenBuffer = signParams.MacBuffer.Slice(0, SignToken.StructSize);
			ref SignToken clearToken = ref MemoryMarshal.AsRef<SignToken>(clearTokenBuffer);
			clearToken = new SignToken
			{
				tokID = WrapTokenType.Sign,
				flags = flags,
				filler_FF = 0xFF,
				filler2_FF = 0xFFFFFFFF,
				seqNbr = (ulong)IPAddress.NetworkToHostOrder((long)(ulong)seqNbr)
			};

			var checksumBuffer = signParams.MacBuffer.Slice(SignToken.StructSize, this.ChecksumSizeBytes);
			this.ComputeChecksum(
				sessionKey, usage, KeyIntent.Checksum,
				default,
				signParams.bufferList,
				clearTokenBuffer,
				checksumBuffer);
		}

		// [RFC 4121] § 4.2.4. Encryption and Checksum Operations
		internal void VerifySignature(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageVerifyParams verifyParams)
		{
			Debug.Assert(0 == (flags & WrapFlags.Sealed));
			Debug.Assert(usage is KeyUsage.InitiatorSign or KeyUsage.AcceptorSign);

			if (verifyParams.MacBuffer.Length < this.SignTokenSize)
				throw new ArgumentException(Messages.Krb5_ChecksumBufferTooSmall, nameof(verifyParams));

			var clearTokenBuffer = verifyParams.MacBuffer.Slice(0, SignToken.StructSize);
			ref readonly SignToken clearToken = ref MemoryMarshal.AsRef<SignToken>(clearTokenBuffer);
			bool isValid =
				(clearToken.tokID == WrapTokenType.Sign)
				&& (clearToken.seqNbr == (ulong)IPAddress.NetworkToHostOrder((long)(ulong)seqNbr))
				;
			if (!isValid)
				throw new SecurityException(Messages.Krb5_InvalidChecksum);

			var checksumBuffer = verifyParams.MacBuffer.Slice(SignToken.StructSize, this.ChecksumSizeBytes);

			// [RFC 3961] 5.4. Checksum Profiles Based on Simplified Profile
			this.VerifyChecksum(sessionKey, usage, KeyIntent.Checksum, verifyParams.bufferList, clearTokenBuffer, checksumBuffer);
		}
		#endregion

		internal abstract void SealMessage(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSealParams sealParams);

		internal abstract void UnsealMessage(
			ReadOnlySpan<byte> sessionKey,
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSealParams sealParams
			);
		#endregion
	}
}