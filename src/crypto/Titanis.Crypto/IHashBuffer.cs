using System;

namespace Titanis.Crypto
{
	public interface IHashBuffer
	{
		long InputSize { get; set; }
		int WriteIndex { get; set; }
		int InputBlockSizeBytes { get; }
		Span<byte> InputBuffer { get; }
		void HashBuffer();
	}

}
