using System;

namespace Titanis.Smb2
{
	public enum CompressionAlgorithm : ushort
	{
		None = 0,
		Lznt1 = 1,
		Lz77 = 2,
		Lz77_Huffman = 3,
		Pattern_V1 = 4,
	}
}
