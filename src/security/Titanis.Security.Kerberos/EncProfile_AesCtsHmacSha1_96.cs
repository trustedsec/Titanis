using System;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Implements common functionality for AES-CTS HMAC-SHA1-96 profiles.
	/// </summary>
	/// <seealso cref="EncProfile_Aes128CtsHmacSha1_96"/>
	/// <seealso cref="EncProfile_Aes256CtsHmacSha1_96"/>
	// [RFC 3962]
	public abstract class EncProfile_AesCtsHmacSha1_96 : BasicEncProfile
	{
		private const int AesBlockSize = 128 / 8;
		public const int Sha1HashSizeBytes = 96 / 8;

		/// <inheritdoc/>
		public sealed override int CipherBlockSizeBytes => AesBlockSize;
		/// <inheritdoc/>
		public sealed override int MessageBlockSize => 1;


		/// <inheritdoc/>
		public sealed override int ChecksumSizeBytes => Sha1HashSizeBytes;

		private static readonly byte[] _defaultSalt = Encoding.UTF8.GetBytes("kerberos");
		//public override byte[] DefaultSalt => _defaultSalt;

		private static readonly byte[] EmptyIV = new byte[AesBlockSize];

		/// <summary>
		/// Creates a <see cref="SymmetricAlgorithm"/> initialized for use with this profile.
		/// </summary>
		/// <returns>A new <see cref="SymmetricAlgorithm"/> with no key</returns>
		/// <remarks>
		/// The caller must set <see cref="SymmetricAlgorithm.Key"/> before using the returned <see cref="SymmetricAlgorithm"/>.
		/// </remarks>
		private protected SymmetricAlgorithm CreateCipher(
			CipherMode mode,
			byte[] key)
		{
			// TODO: Eliminate this allocation and the byte[] for the key
			Aes aes = Aes.Create();
			aes.BlockSize = AesBlockSize * 8;
			aes.IV = EmptyIV;
			aes.Padding = PaddingMode.None;
			aes.Mode = mode;
			aes.Key = key;
			return aes;
		}

		/// <inheritdoc/>
		protected sealed override void ComputeChecksum(
			ReadOnlySpan<byte> specificKey,
			ReadOnlySpan<byte> confounder,
			in SecBufferList bufferList,
			ReadOnlySpan<byte> micTokenHeader,
			Span<byte> checksum)
		{
			// TODO: Do this without creating a byte array
			HMACSHA1 hmacalg = new HMACSHA1(specificKey.ToArray());

			// TODO: Do this without temporary array
			// Set up message buffer
			int messageSize = confounder.Length + bufferList.TotalIntegrityLength + micTokenHeader.Length;
			byte[] toSign = new byte[messageSize];
			confounder.CopyTo(toSign);
			int cbMessage = bufferList.CopySectionTo(MessageSecBufferOptions.Integrity, 0, toSign.Slice(confounder.Length));
			micTokenHeader.CopyTo(toSign.Slice(confounder.Length + cbMessage));

			var hash = hmacalg.ComputeHash(toSign);
			hash.SliceReadOnly(0, Sha1HashSizeBytes).CopyTo(checksum);
		}

		protected sealed override void DecryptCore(
			ReadOnlySpan<byte> specificKey,
			Span<byte> confounder,
			in SecBufferList ciphertext
			)
		{
			var cipher = this.CreateCipher(CipherMode.ECB, specificKey.ToArray());
			var decryptHandler = new DecryptCtsHandler(cipher);
			DecryptCts(
				confounder,
				in ciphertext,
				ref decryptHandler);
		}

		protected sealed override void EncryptBlock(ReadOnlySpan<byte> key, Span<byte> bytes)
		{
			var aes = this.CreateCipher(CipherMode.ECB, key.ToArray());
			aes.EncryptEcb(bytes, bytes, PaddingMode.None);
		}

		/// <inheritdoc/>
		protected sealed override void EncryptCore(ReadOnlySpan<byte> cipherKey, Span<byte> confounder, in SecBufferList bufferList)
		{
			var cipher = this.CreateCipher(CipherMode.CBC, cipherKey.ToArray());
			Span<byte> cbcBuffer = stackalloc byte[cipher.BlockSize / 8];
			EncryptCtsHandler handler = new EncryptCtsHandler(cipher, cbcBuffer);
			BasicEncProfile.EncryptCts(
				confounder,
				in bufferList,
				ref handler
				);
		}

		/// <inheritdoc/>
		public sealed override void RandomToKey(ReadOnlySpan<byte> input, Span<byte> keyBuffer)
		{
			// TODO: Check sizes
			input.CopyTo(keyBuffer);
		}

		internal int iterations = 4096;
		/// <inheritdoc/>
		public sealed override int Iterations => this.iterations;
	}

	public sealed class EncProfile_Aes128CtsHmacSha1_96 : EncProfile_AesCtsHmacSha1_96
	{
		internal sealed override EType EType => EType.Aes128CtsHmacSha1_96;
		/// <inheritdoc/>
		public sealed override int KeyBits => 128;
	}

	public sealed class EncProfile_Aes256CtsHmacSha1_96 : EncProfile_AesCtsHmacSha1_96
	{
		internal sealed override EType EType => EType.Aes256CtsHmacSha1_96;
		/// <inheritdoc/>
		public sealed override int KeyBits => 256;
	}
}
