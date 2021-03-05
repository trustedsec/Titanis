using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Crypto
{
	public unsafe struct Md4Context : IHashContext, IHashBuffer
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Md4State _state;
		internal fixed uint _block[Md4.BlockSize / sizeof(uint)];
		Span<byte> IHashBuffer.InputBuffer
		{
			get
			{
				fixed (uint* pBlock = this._block)
				{
					return new Span<byte>((byte*)pBlock, InputBlockSizeBytes);
				}
			}
		}

		public int DigestSizeBytes => Md4State.StructSize;
		public int InputBlockSizeBytes => Md4.BlockSize;

		public void Initialize()
		{
			this.WriteIndex = 0;
			this.InputSize = 0;
			this._state.Initialize();
		}

		public void HashData(ReadOnlySpan<byte> input)
		{
			SlimHashAlgorithm.HashData(input, ref this);
		}

		void IHashBuffer.HashBuffer() => this.HashBuffer();
		internal void HashBuffer()
		{
			Md4State state = this._state;

			// Round 1
			/*
			a = (a + F(b,c,d) + X[k]) <<< s

			[ABCD  0  3]  [DABC  1  7]  [CDAB  2 11]  [BCDA  3 19]
			[ABCD  4  3]  [DABC  5  7]  [CDAB  6 11]  [BCDA  7 19]
			[ABCD  8  3]  [DABC  9  7]  [CDAB 10 11]  [BCDA 11 19]
			[ABCD 12  3]  [DABC 13  7]  [CDAB 14 11]  [BCDA 15 19]
			*/
			for (int i = 0; i < 16; i += 4)
			{
				state.a = Round1(state.a, state.b, state.c, state.d, 0 + i, 3);
				state.d = Round1(state.d, state.a, state.b, state.c, 1 + i, 7);
				state.c = Round1(state.c, state.d, state.a, state.b, 2 + i, 11);
				state.b = Round1(state.b, state.c, state.d, state.a, 3 + i, 19);
			}

			// Round 2
			/*
			a = (a + G(b,c,d) + X[k] + 5A827999) <<< s

			[ABCD  0  3]  [DABC  4  5]  [CDAB  8  9]  [BCDA 12 13]
			[ABCD  1  3]  [DABC  5  5]  [CDAB  9  9]  [BCDA 13 13]
			[ABCD  2  3]  [DABC  6  5]  [CDAB 10  9]  [BCDA 14 13]
			[ABCD  3  3]  [DABC  7  5]  [CDAB 11  9]  [BCDA 15 13]
			*/
			for (int i = 0; i < 4; i++)
			{
				state.a = Round2(state.a, state.b, state.c, state.d, 0 + i, 3);
				state.d = Round2(state.d, state.a, state.b, state.c, 4 + i, 5);
				state.c = Round2(state.c, state.d, state.a, state.b, 8 + i, 9);
				state.b = Round2(state.b, state.c, state.d, state.a, 12 + i, 13);
			}

			// Round 3
			/*
			a = (a + H(b,c,d) + X[k] + 6ED9EBA1) <<< s

			[ABCD  0  3]  [DABC  8  9]  [CDAB  4 11]  [BCDA 12 15]
			[ABCD  2  3]  [DABC 10  9]  [CDAB  6 11]  [BCDA 14 15]
			[ABCD  1  3]  [DABC  9  9]  [CDAB  5 11]  [BCDA 13 15]
			[ABCD  3  3]  [DABC 11  9]  [CDAB  7 11]  [BCDA 15 15]
			*/
			for (int i = 0; i < 4; i++)
			{
				int j = i switch
				{
					0 => 0,
					1 => 2,
					2 => 1,
					3 => 3,
				};

				state.a = Round3(state.a, state.b, state.c, state.d, 0 + j, 3);
				state.d = Round3(state.d, state.a, state.b, state.c, 8 + j, 9);
				state.c = Round3(state.c, state.d, state.a, state.b, 4 + j, 11);
				state.b = Round3(state.b, state.c, state.d, state.a, 12 + j, 15);
			}

			this._state.a += state.a;
			this._state.b += state.b;
			this._state.c += state.c;
			this._state.d += state.d;

			this.WriteIndex = 0;
		}

		internal unsafe void SetLength(long cbPlaintext)
		{
			fixed (uint* pBuf = &this._block[Md4.BlockSize / 4 - 2])
			{
				*(long*)pBuf = (this.InputSize * 8);
			}
		}

		internal unsafe void MarkEnd()
		{
			fixed (uint* pBuf = this._block)
			{
				byte* pBytes = (byte*)pBuf;
				pBytes[this.WriteIndex++] = 0x80;
			}
		}

		internal unsafe void ZeroBufferBytes(int startIndex, int count)
		{
			fixed (uint* pBuf = this._block)
			{
				byte* pBytes = (byte*)pBuf;
				pBytes += startIndex;
				for (int i = 0; i < count; i++)
				{
					pBytes[i] = 0;
				}
			}
		}

		public void HashFinal(Span<byte> digestBuffer)
		{
			if (digestBuffer.Length < this.DigestSizeBytes)
				throw new ArgumentException(Messages.Md4_InvalidBufferSize);

			Debug.Assert(this.WriteIndex < Md4.BlockSize);

			this.MarkEnd();
			if (this.WriteIndex >= Md4.BlockSize)
				this.HashBuffer();

			if (this.WriteIndex < (Md4.BlockSize - 8))
			{
				this.ZeroBufferBytes(this.WriteIndex, (Md4.BlockSize - 8) - this.WriteIndex);
			}
			else
			{
				int writeIndex2 = this.WriteIndex;
				this.ZeroBufferBytes(this.WriteIndex, Md4.BlockSize - this.WriteIndex);
				this.HashBuffer();
				this.ZeroBufferBytes(0, writeIndex2);
			}

			this.SetLength(this.InputSize);
			this.HashBuffer();

			GetDigest(digestBuffer);
		}

		private unsafe void GetDigest(Span<byte> digestBuffer)
		{
			Debug.Assert(digestBuffer.Length >= this.DigestSizeBytes);
			fixed (byte* pDigest = digestBuffer)
			{
				*(Md4State*)pDigest = this._state;
			}
		}

		/*
        F(X,Y,Z) = XY v not(X) Z
        G(X,Y,Z) = XY v XZ v YZ
        H(X,Y,Z) = X xor Y xor Z
		*/

		private static uint F(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}
		private static uint G(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}
		private static uint H(uint x, uint y, uint z)
		{
			return (x ^ y ^ z);
		}

		private uint Round1(uint a, uint b, uint c, uint d, int k, int s)
		{
			return BitHelper.RotateLeft((a + F(b, c, d) + this._block[k]), s);
		}

		private uint Round2(uint a, uint b, uint c, uint d, int k, int s)
		{
			return BitHelper.RotateLeft((a + G(b, c, d) + this._block[k] + 0x5A827999), s);
		}

		private uint Round3(uint a, uint b, uint c, uint d, int k, int s)
		{
			return BitHelper.RotateLeft((a + H(b, c, d) + this._block[k] + 0x6ED9EBA1), s);
		}
	}

	public class Md4 : SlimHashAlgorithm<Md4Context>
	{
		public const int BlockSize = 512 / 8;
		public static int DigestSize => Md4State.StructSize;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Md4State
	{
		public static unsafe int StructSize => sizeof(Md4State);

		internal uint a;
		internal uint b;
		internal uint c;
		internal uint d;

		internal const uint InitialWord0 = 0x67452301;
		internal const uint InitialWord1 = 0xefcdab89;
		internal const uint InitialWord2 = 0x98badcfe;
		internal const uint InitialWord3 = 0x10325476;

		internal void Initialize()
		{
			this.a = InitialWord0;
			this.b = InitialWord1;
			this.c = InitialWord2;
			this.d = InitialWord3;
		}
	}

}
