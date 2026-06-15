using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Compression
{
	public class Mszip
	{
		/// <summary>
		/// Inflates a chunk compressed with MS-ZIP.
		/// </summary>
		/// <param name="compressed">The compressed bytes, beginning with MS-ZIP signature 0x43 0x4B</param>
		/// <param name="uncompressed">Buffer to hold uncompressed data</param>
		/// <param name="writeIndex">Write index within <paramref name="uncompressed"/></param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException">Invalid compressed data encountered</exception>
		/// <remarks>
		/// This method processes compressed data from <paramref name="compressed"/> until in encounters a block marked as final, and writes the uncompressed data to <paramref name="uncompressed"/> starting at <paramref name="writeIndex"/>.  Note that when decompressing multiple chunks, a chunk may refer to a sequence in the uncompressed data of a previous chunk.  Therefore, <paramref name="uncompressed"/> should include the uncompressed output of previous blocks.
		/// </remarks>
		public static int Decompress(ReadOnlySpan<byte> compressed, Span<byte> uncompressed, int writeIndex)
		{
			// [MS-MCI] § 2 - Structures
			if (!(compressed.Length >= 2 && compressed[0] == 0x43 && compressed[1] == 0x4B))
				throw new InvalidDataException($"The MSZIP block does not begin the correct signature.");

			return Deflate.Inflate(compressed.Slice(2), uncompressed, writeIndex);
		}
	}
}
