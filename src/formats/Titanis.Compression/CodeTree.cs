using System.Diagnostics;

namespace Titanis.Compression
{
	class CodeTree
	{
		internal CodeTree()
		{

		}

		//internal CodeNode _root = new CodeNode();
		private CodeNodeRec[] _nodes = new CodeNodeRec[512];
		private int _nodeCount = 1;

		private const int MaxBits = 16;

		struct CodeNodeRec
		{
			public int leftIndexOrValue;
			public int rightIndexOrValue;

			internal bool IsTerminal => this.leftIndexOrValue <= 0 && this.rightIndexOrValue <= 0;
		}

		public static CodeTree Build(ReadOnlySpan<byte> codeLengths)
		{
			CodeTree tree = new CodeTree();
			Span<int> blCount = stackalloc int[MaxBits];
			for (int i = 0; i < codeLengths.Length; i++)
			{
				blCount[codeLengths[i]]++;
			}
			blCount[0] = 0;

			// [RFC 1951] § 3.2.2. Use of Huffman coding in the "deflate" format
			Span<ushort> nextCode = stackalloc ushort[MaxBits + 1];
			{
				ushort code = 0;
				for (int i = 1; i <= MaxBits; i++)
				{
					code = (ushort)((code + blCount[i - 1]) << 1);
					nextCode[i] = code;
				}
			}

			for (ushort n = 0; n < codeLengths.Length; n++)
			{
				byte len = codeLengths[n];
				if (len == 0)
					continue;

				var code = nextCode[len];
				tree.Add(code, n, len);
				nextCode[len]++;
			}

			return tree;
		}

		private int AllocNode()
		{
			var index = this._nodeCount;
			if (index >= this._nodes.Length)
			{
				Array.Resize(ref this._nodes, this._nodes.Length + 16);
			}

			this._nodeCount++;
			return index;
		}

		private void Add(ushort code, ushort n, byte length)
		{
			Debug.Assert(length > 0);

			var nodeIndex = 0;
			for (int i = length - 1; i >= 0; i--)
			{
				bool bit = (code & (1 << i)) != 0;
				ref var node = ref this._nodes[nodeIndex];
				ref var nodeIndexRef = ref (bit ? ref node.rightIndexOrValue : ref node.leftIndexOrValue);

				if (i == 0)
				{
					nodeIndexRef = ~n;
				}
				else
				{
					// The slot must not contain a value
					Debug.Assert(nodeIndexRef >= 0);

					if (nodeIndexRef == 0)
					{
						nodeIndex = this.AllocNode();
						nodeIndexRef = nodeIndex;
					}
					else
					{
						nodeIndex = nodeIndexRef;
					}
				}
			}
		}

		internal int Read(ref BitContext ctx)
		{
			var nodeIndex = 0;
			do
			{
				var b = 0 != ctx.ReadBit();
				ref var node = ref this._nodes[nodeIndex];
				nodeIndex = b ? node.rightIndexOrValue : node.leftIndexOrValue;
			} while (nodeIndex > 0);

			Debug.Assert(nodeIndex < 0);
			return (int)~nodeIndex;
		}
	}
}
