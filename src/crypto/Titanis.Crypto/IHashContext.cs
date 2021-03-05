using System;

namespace Titanis.Crypto
{
	public interface IHashContext
	{
		int DigestSizeBytes { get; }
		int InputBlockSizeBytes { get; }
		void Initialize();
		void HashData(ReadOnlySpan<byte> block);
		void HashFinal(Span<byte> digestBuffer);
	}

}
