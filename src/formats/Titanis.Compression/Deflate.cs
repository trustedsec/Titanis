using System.Diagnostics;

namespace Titanis.Compression
{
	[Flags]
	enum DeflateBlockType
	{
		Uncompressed = 0,
		Fixed = 1,
		Dynamic = 2,
		Error = 3,
	}

	public static class Deflate
	{
		// [RFC 1951] § 3.2.7. Compression with dynamic Huffman codes (BTYPE=10)
		private static readonly byte[] CodelenIndexes = [16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15];

		#region Fixed trees
		private static CodeTree? fixedLitTree;
		private static CodeTree GetFixedLitTree() => (fixedLitTree ??= BuildFixedLitTree());
		private static CodeTree BuildFixedLitTree()
		{
			// Build fixed tree
			Span<byte> lengths = stackalloc byte[288];
			int i;
			for (i = 0; i < 144; i++)
				lengths[i] = 8;
			for (; i < 256; i++)
				lengths[i] = 9;
			for (; i < 280; i++)
				lengths[i] = 7;
			for (; i < 288; i++)
				lengths[i] = 8;

			var litTree = CodeTree.Build(lengths);
			return litTree;
		}

		private static CodeTree? fixedDistTree;
		private static CodeTree GetFixedDistTree() => (fixedDistTree ??= BuildFixedDistTree());
		private static CodeTree BuildFixedDistTree()
		{
			// Build fixed tree
			Span<byte> lengths = stackalloc byte[32];
			int i;
			for (i = 0; i < 32; i++)
				lengths[i] = 5;

			var litTree = CodeTree.Build(lengths);
			return litTree;
		}
		#endregion

		private static CodeTree BuildDynamicCodeTree(ref BitContext ctx, uint hclen)
		{
			Span<byte> codeLengths = stackalloc byte[19];
			for (int i = 0; i < hclen; i++)
			{
				codeLengths[CodelenIndexes[i]] = (byte)ctx.ReadBitsReversed(3);
			}

			CodeTree codeTree = CodeTree.Build(codeLengths);
			return codeTree;
		}

		public static int Inflate(ReadOnlySpan<byte> compressed, Span<byte> uncompressed, int writeIndex)
		{
			BitContext ctx = new BitContext(compressed)
			{
				sourceIndex = 0
			};

			while (true)
			{
				// [RFC 1951] § 3.2.3. Details of block format
				var bfinal = 0 != ctx.ReadBit();
				var btype = (DeflateBlockType)ctx.ReadBitsReversed(2);

				switch (btype)
				{
					case DeflateBlockType.Fixed:
						{
							CodeTree litTree = GetFixedLitTree();
							CodeTree distTree = BuildFixedDistTree();
							writeIndex = Expand(ref ctx, litTree, distTree, uncompressed, writeIndex);
						}
						break;
					case DeflateBlockType.Dynamic:
						// [RFC 1951] § 3.2.7. Compression with dynamic Huffman codes (BTYPE=10)
						{
							int hlit = (int)ctx.ReadBitsReversed(5) + 257;
							int hdist = (int)ctx.ReadBitsReversed(5) + 1;
							uint hclen = ctx.ReadBitsReversed(4) + 4;

							var codeTree = BuildDynamicCodeTree(ref ctx, hclen);

							var litTree = UnpackLengths(ref ctx, hlit, codeTree);
							var distTree = UnpackLengths(ref ctx, hdist, codeTree);
							writeIndex = Expand(ref ctx, litTree, distTree, uncompressed, writeIndex);
						}
						break;
					case DeflateBlockType.Uncompressed:
						{
							// [RFC 1951] § 3.2.4. Non-compressed blocks (BTYPE=00)
							if (ctx.bitIndex > 0)
							{
								ctx.sourceIndex++;
								ctx.bitIndex = 0;
							}

							var len = (ushort)ctx.ReadBitsReversed(16);
							var nlen = (ushort)ctx.ReadBitsReversed(16);

							if (len != (ushort)~nlen)
								throw new InvalidDataException($"len/nlen of uncompressed block don't match");

							ctx.source.Slice(ctx.sourceIndex, len).CopyTo(uncompressed.Slice(writeIndex, len));
							ctx.sourceIndex += len;
							writeIndex += len;
						}
						break;
					default:
						throw new InvalidDataException($"Encountered a bad block type near byte index {ctx.bitIndex}");
				}

				if (bfinal)
					break;
			}

			return writeIndex;
		}

		private static int Expand(ref BitContext ctx, CodeTree litTree, CodeTree distTree, Span<byte> uncompressed, int writeIndex)
		{
			// [RFC 1951] § 3.2.5. Compressed blocks (length and distance codes)
			do
			{
				int symbol = litTree.Read(ref ctx);
				if (symbol < 256)
				{
					// This is a literal code
					uncompressed[writeIndex] = (byte)symbol;
					writeIndex++;
				}
				else if (symbol == 256)
					// END
					return writeIndex;
				else
				{
					// (L,D) codes

					int length;
					if (symbol == 285)
						length = 258;
					else if (symbol < 265)
						length = symbol - 257 + 3;
					else if (symbol < 285)
					{
						// This is a length code
						{
							// Extra bits
							// 265 <= n < 269 => 1
							// 269 <= n < 273 => 2
							// 273 <= n < 277 => 3
							// 277 <= n < 281 => 4
							// 281 <= n < 285 => 5
							var d = (symbol - 257);
							var extraBitCount = (d / 4) - 1;
							var extraLength = (int)ctx.ReadBitsReversed(extraBitCount);
							// Let d = n - 257
							// 265 <= n < 269 => 3 + (d%4) * 1<<1 + 2^3 + extra
							// 269 <= n < 273 => 3 + (d%4) * 1<<2 + 2^4 + extra
							// 273 <= n < 277 => 3 + (d%4) * 1<<3 + 2^5 + extra
							// 277 <= n < 281 => 3 + (d%4) * 1<<4 + 2^6 + extra
							// 281 <= n < 285 => 3 + (d%4) * 1<<5 + 2^7 + extra
							length = 3 + ((d % 4) * (1 << extraBitCount)) + (1 << (extraBitCount + 2)) + extraLength;
						}
					}
					else
						throw new InvalidDataException($"Invalid length code: {symbol}");

					int distCode = distTree.Read(ref ctx);

					int distance;
					if (distCode < 4)
						distance = distCode + 1;
					else if (distCode < 30)
					{
						var extraBitCount = (distCode / 2) - 1;
						var extraDist = (int)ctx.ReadBitsReversed(extraBitCount);
						distance = 1 + ((distCode % 2) * (1 << extraBitCount)) + (1 << (extraBitCount + 1)) + extraDist;
					}
					else
						throw new InvalidDataException($"Invalid distance code: {symbol}");
					int origLength = length;
					while (length > 0)
					{
						uncompressed[writeIndex] = uncompressed[writeIndex - distance];
						writeIndex++;

						length--;
					}
				}
			} while (true);
		}

		private static CodeTree UnpackLengths(ref BitContext ctx, int lengthCount, CodeTree codeTree)
		{
			Span<byte> lengths = stackalloc byte[lengthCount];

			// [RFC 1951] § 3.2.7. Compression with dynamic Huffman codes (BTYPE=10)
			for (int i = 0; i < lengths.Length;)
			{
				var symbol = codeTree.Read(ref ctx);
				Debug.Assert(symbol <= byte.MaxValue);
				if (symbol < 16)
				{
					lengths[i] = (byte)symbol;
					i++;
				}
				else
				{
					uint extraCount;
					byte dup;
					if (symbol == 16)
						(dup, extraCount) = (lengths[i - 1], ctx.ReadBitsReversed(2) + 3);
					else if (symbol == 17)
						(dup, extraCount) = (0, ctx.ReadBitsReversed(3) + 3);
					else if (symbol == 18)
						(dup, extraCount) = (0, ctx.ReadBitsReversed(7) + 11);
					else
						throw new InvalidDataException($"Invalid symbol encountered: {symbol}");

					while (extraCount > 0)
					{
						lengths[i] = dup;
						i++;
						extraCount--;
					}
				}
			}

			return CodeTree.Build(lengths);
		}

	}
}
