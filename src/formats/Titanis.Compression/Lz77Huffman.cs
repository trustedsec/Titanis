using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Titanis.Compression
{
	public static class Lz77Huffman
	{
		ref struct LzhuffContext
		{
			internal readonly ReadOnlySpan<byte> compressed;
			internal int readIndex;
			internal uint bits;
			internal int extra;
			internal BitContext ctx;

			internal LzhuffContext(ReadOnlySpan<byte> compressed)
			{
				this.compressed = compressed;
				this.readIndex = 256;
				this.bits = BinaryPrimitives.ReadUInt16BigEndian(compressed.Slice(this.readIndex, 2));
				this.readIndex += 2;
				this.bits |= (uint)BinaryPrimitives.ReadUInt16BigEndian(compressed.Slice(this.readIndex, 2)) << 16;
				this.readIndex += 2;
				this.extra = 16;

				this.ctx = new BitContext(MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref this.bits, 1)))
				{
					bitFlip = 1
				};
			}
			internal void Advance()
			{
				while (ctx.sourceIndex > 0)
				{
					this.bits >>= 8;
					this.extra -= 8;
					ctx.sourceIndex--;
				}
				// Fill BitContext buffer
				while (((this.extra - this.ctx.bitIndex) < 0) && (this.readIndex < compressed.Length))
				{
					this.bits |= (uint)BinaryPrimitives.ReadUInt16BigEndian(compressed.Slice(this.readIndex, 2)) << (16 + this.extra);
					this.readIndex += 2;
					this.extra += 16;
				}
			}
		}

		public static void Decompress(ReadOnlySpan<byte> compressed, Span<byte> uncompressed, int writeIndex)
		{
			if (compressed.Length < 256)
				throw new InvalidDataException($"The buffer is too small to contain LZ77+Huffman compressed data.");

			// Unpack lengths
			CodeTree codeTree;
			{
				ReadOnlySpan<byte> table = compressed.Slice(0, 256);
				Span<byte> freqs = stackalloc byte[512];
				for (int i = 0; i < table.Length; i++)
				{
					var b = table[i];
					freqs[i * 2] = (byte)(b & 0x0F);
					freqs[i * 2 + 1] = (byte)(b >> 4);
				}
				codeTree = CodeTree.Build(freqs);
			}

			LzhuffContext ctx2 = new LzhuffContext(compressed);

#if DEBUG
			int prevWriteIndex;
			int prevCode = 0;
#endif
			while (ctx2.readIndex < compressed.Length)
			{

				var code = codeTree.Read(ref ctx2.ctx);
				ctx2.Advance();

#if DEBUG
				prevWriteIndex = writeIndex;
				prevCode = code;
#endif

				if (code <= 0xFF)
				{
					uncompressed[writeIndex] = (byte)code;
					writeIndex++;
				}
				else if ((code == 0x100) && (writeIndex == uncompressed.Length))
				{
					break;
				}
				else
				{
					code -= 256;
					var matchLength = code % 16;
					var offbits = code / 16;

					if (matchLength == 15)
					{
						matchLength = compressed[ctx2.readIndex];
						ctx2.readIndex++;
						if (matchLength == 255)
						{
							matchLength = BinaryPrimitives.ReadUInt16LittleEndian(compressed.Slice(ctx2.readIndex, 2));
							ctx2.readIndex += 2;
							if (matchLength < 15)
								throw new InvalidDataException($"Invalid match length at byte offset {ctx2.readIndex - 2}.");
						}
						else
							matchLength += 15;
					}

					matchLength += 3;
					var matchOffset = (int)ctx2.ctx.ReadBits(offbits);
					matchOffset += (1 << offbits);
					ctx2.Advance();

					while (matchLength > 0)
					{
						uncompressed[writeIndex] = uncompressed[writeIndex - matchOffset];
						writeIndex++;

						matchLength--;
					}
				}
			}
			Debug.Assert(writeIndex == uncompressed.Length);
		}
	}
}
