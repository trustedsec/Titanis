using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Compression
{
	public static class Lz77
	{
		public static void Decompress(ReadOnlySpan<byte> compressed, Span<byte> uncompressed, int writeIndex)
		{
			int readIndex = 0;
			int maskBits = 0;
			uint mask = 0;
			bool hasSharedNibble = false;
			byte sharedNibble = 0;
			while (readIndex < compressed.Length)
			{
#if DEBUG
				var startOffset = readIndex;
				if (readIndex == 0x000018cb)
					;
#endif
				if (maskBits == 0)
				{
					mask = BinaryPrimitives.ReadUInt32LittleEndian(compressed.Slice(readIndex, 4));
					readIndex += 4;
					maskBits = 32;
					continue;
				}

				if ((int)mask >= 0)
				{
					uncompressed[writeIndex] = compressed[readIndex];
					readIndex++;
					writeIndex++;
				}
				else
				{
					var n = BinaryPrimitives.ReadUInt16LittleEndian(compressed.Slice(readIndex, 2));
					readIndex += 2;

					var matchLength = (n & 0x07);
					var matchOffset = (n >> 3) + 1;
					Debug.Assert(matchOffset > 0);

					if (matchLength == 7)
					{
						if (!hasSharedNibble)
						{
							sharedNibble = compressed[readIndex];
							readIndex++;
							matchLength = sharedNibble & 0x0F;
							hasSharedNibble = true;
						}
						else
						{
							matchLength = sharedNibble >> 4;
							hasSharedNibble = false;
						}

						if (matchLength == 0x0F)
						{
							matchLength = compressed[readIndex];
							readIndex++;

							if (matchLength == 0xFF)
							{
								matchLength = BinaryPrimitives.ReadUInt16LittleEndian(compressed.Slice(readIndex, 2));
								readIndex += 2;
								if (matchLength == 0)
								{
									matchLength = BinaryPrimitives.ReadInt32LittleEndian(compressed.Slice(readIndex, 4));
									readIndex += 4;
									if (matchLength < 15 + 7)
									{
										throw new InvalidDataException($"Invalid length {matchLength} encountered at offset {readIndex} in the compressed data stream.");
									}
								}

								matchLength -= 15 + 7;
							}

							matchLength += 15;
						}

						matchLength += 7;
					}
					matchLength += 3;

#if DEBUG
					var origMatchLength = matchLength;
#endif
					while (matchLength > 0)
					{
						uncompressed[writeIndex] = uncompressed[writeIndex - matchOffset];
						writeIndex++;
						matchLength--;
					}
				}

				mask <<= 1;
				maskBits--;
			}

			Debug.Assert(writeIndex == uncompressed.Length);
		}
	}
}
