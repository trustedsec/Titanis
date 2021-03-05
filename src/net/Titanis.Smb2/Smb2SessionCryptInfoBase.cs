using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using Titanis.Crypto;
using Titanis.Smb2.Pdus;

namespace Titanis.Smb2
{
	class Smb2ChannelBindingInfo
	{
		internal Smb2ChannelBindingInfo(
			ushort channelId,
			Smb2Connection connection,
			byte[]? signingKey,
			ValidateNegotiateInfo? validateNegotiateInfo
			)
		{
			this.channelId = channelId;
			this.connection = connection;
			this.signingKey = signingKey;
			this.validateNegotiateInfo = validateNegotiateInfo;
		}

		internal readonly ushort channelId;
		internal readonly Smb2Connection connection;
		internal readonly byte[] signingKey;
		internal ValidateNegotiateInfo? validateNegotiateInfo;
	}

	abstract class Smb2SessionCryptInfoBase
	{
		public bool ShouldSign { get; }
		public virtual bool SupportsEncryption => false;
		public virtual int NonceSize => 0;
		public ushort ChannelId => 0;

		protected Smb2SessionCryptInfoBase(bool shouldSign)
		{
			this.ShouldSign = shouldSign;
		}

		internal abstract void Sign(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			ulong messageId,
			Smb2SignFlags flags,
			Span<byte> signature
			);

		internal void Verify(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			ulong messageId,
			Smb2SignFlags flags,
			Span<byte> signature
			)
		{
			var sigBytes = signature.ToArray();
			signature.Fill(0);

			this.Sign(signingKey, message, messageId, flags, signature);

			if (!sigBytes.SequenceEqual(signature.ToArray()))
				throw new SecurityException("The message signature is invalid.");
		}

		protected void SignHmacSha256(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			Span<byte> signature)
		{
			HMACSHA256 hashalg = new HMACSHA256(signingKey);
			Span<byte> digest = new Span<byte>(hashalg.ComputeHash(message.ToArray()));
			digest[..Smb2PduSyncHeader.SigSize].CopyTo(signature);
		}

		internal virtual void Encrypt(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> message,
			ReadOnlySpan<byte> authData,
			Span<byte> encryptedBuf,
			Span<byte> authTag)
			=> throw new NotImplementedException();

		internal virtual void Decrypt(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> message,
			ReadOnlySpan<byte> authData,
			Span<byte> encryptedBuf,
			Span<byte> authTag)
			=> throw new NotImplementedException();
	}


	class Smb2SessionCryptInfo : Smb2SessionCryptInfoBase
	{
		internal Smb2SessionCryptInfo(bool shouldSign)
			: base(shouldSign)
		{
		}

		internal override void Sign(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			ulong messageId,
			Smb2SignFlags flags,
			Span<byte> signature)
		{
			SignHmacSha256(signingKey, message, signature);
		}
	}

	sealed class Smb3SessionCryptInfo : Smb2SessionCryptInfoBase
	{
		private readonly byte[] encryptionKey;
		private readonly byte[] decryptionKey;
		private readonly SigningAlgorithm signingAlgorithm;
		private readonly Cipher cipher;

		public override bool SupportsEncryption => (this.cipher != Cipher.None);

		internal Smb3SessionCryptInfo(
			byte[] sessionKey,
			byte[] encryptionKey,
			byte[] decryptionKey,
			SigningAlgorithm signingAlgorithm,
			Cipher cipher,
			bool shouldSign
			)
			: base(shouldSign)
		{
			this.encryptionKey = encryptionKey;
			this.decryptionKey = decryptionKey;
			this.signingAlgorithm = signingAlgorithm;
			this.cipher = cipher;
		}

		public sealed override int NonceSize =>
			this.cipher switch
			{
				Cipher.Aes128Ccm or Cipher.Aes256Ccm => 11,
				Cipher.Aes128Gcm or Cipher.Aes256Gcm => 12,
				_ => 0
			};

		internal sealed override void Sign(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			ulong messageId,
			Smb2SignFlags flags,
			Span<byte> signature)
		{
			switch (this.signingAlgorithm)
			{
				case SigningAlgorithm.HmacSha256:
					this.SignHmacSha256(signingKey, message, signature);
					break;
				case SigningAlgorithm.AesCmac:
					SignAesCmac(signingKey, message, signature);
					break;
				case SigningAlgorithm.AesGmac:
					SignAesGmac(signingKey, message, messageId, flags, signature);
					break;
				default:
					break;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct GmacNonce
		{
			internal ulong messageId;
			internal Smb2SignFlags flags;
		}

		private void SignAesGmac(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			ulong messageId,
			Smb2SignFlags flags,
			Span<byte> signature)
		{
			GmacNonce nonce = new GmacNonce
			{
				messageId = messageId,
				flags = flags
			};
			var nonceBytes = MemoryMarshal.Cast<GmacNonce, byte>(MemoryMarshal.CreateReadOnlySpan(ref nonce, 1));
			AesGcm gcm = new AesGcm(signingKey);
			gcm.Encrypt(nonceBytes, new ReadOnlySpan<byte>(), new Span<byte>(), signature, message);
		}

		private void SignAesCmac(
			byte[] signingKey,
			ReadOnlySpan<byte> message,
			Span<byte> signature)
		{
			byte[] sigbuf = new byte[16];
			Aes128Cmac.Sign(signingKey, message, sigbuf);
			sigbuf.CopyTo(signature);
		}


		private void EncryptAesCcm(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> message,
			ReadOnlySpan<byte> authData,
			Span<byte> encryptedBuf,
			Span<byte> authTag)
		{
			AesCcm ccm = new AesCcm(this.encryptionKey);
			ccm.Encrypt(nonce, message, encryptedBuf, authTag, authData);
		}

		private void EncryptAesGcm(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> message,
			ReadOnlySpan<byte> authData,
			Span<byte> encryptedBuf,
			Span<byte> authTag)
		{
			AesGcm gcm = new AesGcm(this.encryptionKey);
			gcm.Encrypt(nonce, message, encryptedBuf, authTag, authData);
		}

		internal override void Encrypt(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> message,
			ReadOnlySpan<byte> authData,
			Span<byte> encryptedBuf,
			Span<byte> authTag)
		{
			switch (this.cipher)
			{
				case Cipher.Aes128Ccm:
				case Cipher.Aes256Ccm:
					this.EncryptAesCcm(nonce, message, authData, encryptedBuf, authTag);
					break;
				case Cipher.Aes128Gcm:
				case Cipher.Aes256Gcm:
					this.EncryptAesGcm(nonce, message, authData, encryptedBuf, authTag);
					break;
				default:
					throw new InvalidOperationException("No cipher configured.");
			}
		}


		private void DecryptAesCcm(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> ciphertext,
			ReadOnlySpan<byte> tag,
			Span<byte> plaintextBuf,
			Span<byte> authData)
		{
			AesCcm ccm = new AesCcm(this.decryptionKey);
			ccm.Decrypt(nonce, ciphertext, tag, plaintextBuf, authData);
		}

		private void DecryptAesGcm(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> ciphertext,
			ReadOnlySpan<byte> tag,
			Span<byte> plaintextBuf,
			Span<byte> authData)
		{
			AesGcm gcm = new AesGcm(this.decryptionKey);
			gcm.Decrypt(nonce, ciphertext, tag, plaintextBuf, authData);
		}

		internal override void Decrypt(
			ReadOnlySpan<byte> nonce,
			ReadOnlySpan<byte> ciphertext,
			ReadOnlySpan<byte> tag,
			Span<byte> plaintextBuf,
			Span<byte> authData)
		{
			switch (this.cipher)
			{
				case Cipher.Aes128Ccm:
				case Cipher.Aes256Ccm:
					this.DecryptAesCcm(nonce, ciphertext, tag, plaintextBuf, authData);
					break;
				case Cipher.Aes128Gcm:
				case Cipher.Aes256Gcm:
					this.DecryptAesGcm(nonce, ciphertext, tag, plaintextBuf, authData);
					break;
				default:
					throw new InvalidOperationException("No cipher configured.");
			}
		}
	}
}
