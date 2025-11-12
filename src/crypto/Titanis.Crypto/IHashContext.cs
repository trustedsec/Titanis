using System;

namespace Titanis.Crypto
{
	public interface IHashContext
	{
		int DigestSizeBytes { get; }
		static abstract int StaticDigestSizeBytes { get; }
		int InputBlockSizeBytes { get; }
		void Initialize();
		void HashData(ReadOnlySpan<byte> input);
		void HashFinal(Span<byte> digestBuffer);
	}

}
