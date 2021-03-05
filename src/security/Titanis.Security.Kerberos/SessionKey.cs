using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Titanis.Asn1.Serialization;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a session key.
	/// </summary>
	public class SessionKey
	{
		/// <summary>
		/// Initializes a new <see cref="SessionKey"/>.
		/// </summary>
		/// <param name="encryptionProfile">Encryption profile</param>
		/// <param name="keyData">Key bytes</param>
		public SessionKey(EncProfile encryptionProfile, byte[] keyData)
		{
			ArgumentNullException.ThrowIfNull(encryptionProfile);
			ArgumentNullException.ThrowIfNull(keyData);

			this.EncryptionProfile = encryptionProfile;
			this.key = Structs.EncryptionKey(encryptionProfile.EType, keyData);
		}

		internal SessionKey(EncProfile encryptionProfile, Asn1.KerberosV5Spec2.EncryptionKey key)
		{
			this.EncryptionProfile = encryptionProfile;
			this.key = key;
		}

		/// <summary>
		/// Gets the encryption profile used by the key.
		/// </summary>
		public EncProfile EncryptionProfile { get; }
		public EType EType => this.EncryptionProfile.EType;

		internal readonly Asn1.KerberosV5Spec2.EncryptionKey key;
		internal byte[] KeyBytes => this.key.keyvalue;

		public Memory<byte> Encrypt(KeyUsage usage, Span<byte> data)
			=> this.EncryptionProfile.Encrypt(this.KeyBytes, usage, data);

		internal Titanis.Security.Kerberos.Asn1.KerberosV5Spec2.EncryptedData EncryptAndWrap(
			KeyUsage usage,
			Span<byte> data)
			=> Structs.EncryptedData(this.EType, this.Encrypt(usage, data).ToArray());

		internal ReadOnlyMemory<byte> Decrypt(KeyUsage usage, EncryptedData edata)
		{
			if ((EType)edata.etype != this.EncryptionProfile.EType)
				throw new ArgumentException("The EType of the encrypted data does not match the EType of this key.", nameof(edata));

			var encProfile = this.EncryptionProfile;
			var cipher = edata.cipher;
			var cbBody = cipher.Length - encProfile.CipherHeaderSizeBytes - encProfile.CipherTrailerSizeBytes;
			Memory<byte> message = cipher.AsMemory().Slice(encProfile.CipherHeaderSizeBytes, cbBody);
			this.EncryptionProfile.Decrypt(
				this.KeyBytes, usage,
				cipher.Slice(0, encProfile.CipherHeaderSizeBytes),
				SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(message.Span)),
				cipher.Slice(encProfile.CipherHeaderSizeBytes + cbBody, encProfile.CipherTrailerSizeBytes)
				);

			return message;
		}

		internal EncryptedData EncryptTlv<T>(
			KeyUsage usage,
			T obj)
			where T : IAsn1DerEncodableTlv
		{
			var bytes = Asn1DerEncoder.EncodeTlv(obj);
			bytes = this.EncryptionProfile.Encrypt(
				this.KeyBytes,
				usage,
				bytes.Span);

#if DEBUG
			var bytesCopy = bytes.ToArray();
			this.EncryptionProfile.Decrypt(this.KeyBytes, usage, bytesCopy);
#endif

			return Structs.EncryptedData(this.EType, bytes.ToArray());
		}

		internal T DecryptTlv<T>(
			KeyUsage usage,
			EncryptedData edata
			)
			where T : IAsn1DerEncodableTlv, new()
		{
			if (this.EType != (EType)edata.etype)
				throw new ArgumentException("The data cannot be decrypted with this key because the encryption profile does not match.");

			Span<byte> data = edata.cipher;
			EncProfile encProfile = this.EncryptionProfile;
			var message = encProfile.Decrypt(this.KeyBytes, usage, data);

			// TODO: Once ASN.1 components can use spans, remove ToArray
			T obj = Asn1DerDecoder.Decode<T>(message.ToArray());
			return obj;
		}

		#region Message security
		internal void SignMessage(
			KeyUsage usage,
			uint seqNbr,
			WrapFlags flags,
			in MessageSignParams signParams)
			=> this.EncryptionProfile.SignMessage(
				this.KeyBytes,
				usage,
				seqNbr,
				flags,
				in signParams
				);

		internal void VerifySignature(KeyUsage usage, uint seqNbr, WrapFlags flags, MessageVerifyParams verifyParams)
			=> this.EncryptionProfile.VerifySignature(this.KeyBytes, usage, seqNbr, flags, in verifyParams);

		internal void SealMessage(KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
#if DEBUG
			ref readonly var buffers = ref sealParams.bufferList;
			bool hasTestCopy = false;
			byte[] plaintextCopy = sealParams.bufferList.ToArray(MessageSecBufferOptions.Integrity);
			if (buffers.BufferCount <= 3)
			{
				hasTestCopy = true;
			}
#endif

			this.EncryptionProfile.SealMessage(this.KeyBytes, usage, seqNbr, flags, in sealParams);

#if DEBUG
			if (hasTestCopy)
			{
				var encryptedCopy = sealParams.bufferList.DeepCopy(MessageSecBufferOptions.Integrity);

				byte[] headerCopy = sealParams.Header.ToArray();
				byte[] trailerCopy = sealParams.Trailer.ToArray();
				this.UnsealMessage(usage, seqNbr, flags,
					new MessageSealParams(
						headerCopy,
						encryptedCopy,
						trailerCopy
					));
				var unencryptedCopy = encryptedCopy.ToArray(MessageSecBufferOptions.Integrity);

				Debug.Assert(unencryptedCopy.SequenceEqual(plaintextCopy));
			}
#endif
		}

		internal void UnsealMessage(KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams unsealParams)
			=> this.EncryptionProfile.UnsealMessage(this.KeyBytes, usage, seqNbr, flags, in unsealParams);
		#endregion

	}
}
