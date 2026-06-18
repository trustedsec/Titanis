using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Compression
{
	public static class Lznt1
	{
		public static void Decompress(ReadOnlySpan<byte> compressed, Span<byte> uncompressed, int writeIndex)
		{
			int readIndex = 0;
			while (readIndex < compressed.Length)
			{
				var hdr = BinaryPrimitives.ReadUInt16LittleEndian(compressed.Slice(readIndex, 2));
				if (hdr == 0)
					break;

				var sig = (hdr >> 12) & 0x07;
				if (sig != 3)
					throw new InvalidDataException($"Block starting at byte offset {readIndex} contains an invalid header.");

				readIndex += 2;
				int chunkStartIndex = writeIndex;

				var length = (hdr & 0xFFF) + 3;
				bool isCompressed = ((short)hdr < 0);
				if (!isCompressed)
				{
					var flagByte = compressed[readIndex];
					readIndex++;
					for (int i = 8 - 1; i >= 0; i--)
					{
						bool isComp = (flagByte & 1) != 0;
						flagByte >>= 1;

						if (!isComp)
						{
							uncompressed[writeIndex] = compressed[readIndex];
							writeIndex++;
							readIndex++;
						}
						else
						{
							var diff = (writeIndex - chunkStartIndex);
							int m = Math.Min(12, Math.Max(4, BitOperations.Log2((uint)diff - 1) + 1));
							var dl = BinaryPrimitives.ReadUInt16LittleEndian(compressed.Slice(readIndex, 2));
							readIndex += 2;

							m = 16 - m;
							var l = (dl & ((1 << m) - 1));
							var d = dl >> m;

							throw new NotImplementedException();
						}
					}
				}
				else
				{
					length++;
					compressed.Slice(readIndex, length).CopyTo(uncompressed.Slice(writeIndex, length));
					readIndex += length;
					writeIndex += length;
				}
			}
		}
	}
}
