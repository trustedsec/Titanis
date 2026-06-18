using System.Diagnostics;

namespace Titanis.Compression
{
	ref struct BitContext
	{
		internal BitContext(ReadOnlySpan<byte> source)
		{
			this.source = source;
		}

		internal ReadOnlySpan<byte> source;
		internal int sourceIndex;
		internal int bitIndex;
		internal int bitFlip;

		public uint ReadBits(int count)
		{
			Debug.Assert(count <= 32);

			uint m = 0;
			while (count > 0)
			{
				uint bit = ReadBit();
				m <<= 1;
				m |= bit;
				count--;
			}

			return m;
		}

		public uint ReadBitsReversed(int count)
		{
			Debug.Assert(count <= 32);

			uint m = 0;
			for (int i = 0; i < count; i++)
			{
				uint bit = ReadBit();
				m |= (bit << i);
			}

			return m;
		}

		internal uint ReadBit()
		{
			var bit = (this.source[this.sourceIndex] >> ((this.bitFlip == 0) ? this.bitIndex : (7 - this.bitIndex))) & 1;

			this.bitIndex++;
			if (this.bitIndex == 8)
			{
				this.bitIndex = 0;
				this.sourceIndex++;
			}

			return (uint)bit;
		}
	}
}
