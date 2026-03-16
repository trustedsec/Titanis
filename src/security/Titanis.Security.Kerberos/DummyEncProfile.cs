using System;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents an unsupported encryption profile.
	/// </summary>
	/// <remarks>
	/// This class stands in when loading a ticket from a file with an EType that is not supported by this implementation.  It offers no encryption functionality, but allows the key to be loaded, displayed, and saved.
	/// </remarks>
	internal class DummyEncProfile : EncProfile
	{
		public DummyEncProfile(EType etype)
		{
			this.EType = etype;
		}

		public override bool IsValidKeySizeBytes(int byteCount) => true;

		public override int MessageBlockSizeBytes => throw new NotImplementedException();

		public override int KeyBits => throw new NotImplementedException();

		public override int KeyGenerationSeedSizeBytes => throw new NotImplementedException();

		public override int ChecksumSizeBytes => throw new NotImplementedException();

		public override int SignTokenSize => throw new NotImplementedException();

		public override int CipherHeaderSizeBytes => throw new NotImplementedException();

		public override EType EType { get; }

		protected override int SpecificKeySizeBytes => throw new NotImplementedException();

		internal override EncChecksumType ChecksumType => throw new NotImplementedException();

		public override void Decrypt(ReadOnlySpan<byte> protocolKey, KeyUsage usage, Span<byte> header, in SecBufferList buffers, Span<byte> trailer)
		{
			throw new NotImplementedException();
		}

		public override void Encrypt(ReadOnlySpan<byte> protocolKey, KeyUsage usage, Span<byte> header, in SecBufferList buffers, Span<byte> trailer)
		{
			throw new NotImplementedException();
		}

		public override void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			throw new NotImplementedException();
		}

		public override void RandomToKey(ReadOnlySpan<byte> input, Span<byte> keyBuffer)
		{
			throw new NotImplementedException();
		}

		public override byte[] StringToKey(ReadOnlySpan<byte> str, ReadOnlySpan<byte> salt)
		{
			throw new NotImplementedException();
		}

		protected override void ComputeChecksum(ReadOnlySpan<byte> specificKey, ReadOnlySpan<byte> confounder, in SecBufferList bufferList, ReadOnlySpan<byte> micTokenHeader, Span<byte> checksum)
		{
			throw new NotImplementedException();
		}

		protected override void DeriveSpecificKey(ReadOnlySpan<byte> protocolKey, KeyUsage usage, KeyIntent intent, Span<byte> specificKeyBuffer)
		{
			throw new NotImplementedException();
		}

		internal override void SealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}

		internal override void UnsealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}
	}
}