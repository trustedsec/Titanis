using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Crypto
{
	public unsafe struct Md5Context : IHashContext, IHashBuffer
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Md5State _state;
		internal fixed uint _block[Md5.BlockSize / sizeof(uint)];
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

		public int DigestSizeBytes => Md5State.StructSize;
		public int InputBlockSizeBytes => Md5.BlockSize;

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
			Md5State state = this._state;

			// Round 1
			/*
			a = b + ((a + F(b,c,d) + X[k] + T[i]) <<< s)

			[ABCD  0  7  1]  [DABC  1 12  2]  [CDAB  2 17  3]  [BCDA  3 22  4]
			[ABCD  4  7  5]  [DABC  5 12  6]  [CDAB  6 17  7]  [BCDA  7 22  8]
			[ABCD  8  7  9]  [DABC  9 12 10]  [CDAB 10 17 11]  [BCDA 11 22 12]
			[ABCD 12  7 13]  [DABC 13 12 14]  [CDAB 14 17 15]  [BCDA 15 22 16]
			*/
			for (int i = 0; i < 16; i += 4)
			{
				state.a = Round1(state.a, state.b, state.c, state.d, 0 + i, 7);
				state.d = Round1(state.d, state.a, state.b, state.c, 1 + i, 12);
				state.c = Round1(state.c, state.d, state.a, state.b, 2 + i, 17);
				state.b = Round1(state.b, state.c, state.d, state.a, 3 + i, 22);
			}

			// Round 2
			/*
			a = b + ((a + G(b,c,d) + X[k] + T[i]) <<< s)

			[ABCD  1  5 17]  [DABC  6  9 18]  [CDAB 11 14 19]  [BCDA  0 20 20]
			[ABCD  5  5 21]  [DABC 10  9 22]  [CDAB 15 14 23]  [BCDA  4 20 24]
			[ABCD  9  5 25]  [DABC 14  9 26]  [CDAB  3 14 27]  [BCDA  8 20 28]
			[ABCD 13  5 29]  [DABC  2  9 30]  [CDAB  7 14 31]  [BCDA 12 20 32]
			*/
			for (int i = 0; i < 16; i += 4)
			{
				int j = i + 1;

				state.a = Round2(state.a, state.b, state.c, state.d, (0 + j) % 16, 5, i + 16);
				state.d = Round2(state.d, state.a, state.b, state.c, (5 + j) % 16, 9, i + 17);
				state.c = Round2(state.c, state.d, state.a, state.b, (10 + j) % 16, 14, i + 18);
				state.b = Round2(state.b, state.c, state.d, state.a, (15 + j) % 16, 20, i + 19);
			}

			// Round 3
			/*
			a = b + ((a + H(b,c,d) + X[k] + T[i]) <<< s)

			[ABCD  5  4 33]  [DABC  8 11 34]  [CDAB 11 16 35]  [BCDA 14 23 36]
			[ABCD  1  4 37]  [DABC  4 11 38]  [CDAB  7 16 39]  [BCDA 10 23 40]
			[ABCD 13  4 41]  [DABC  0 11 42]  [CDAB  3 16 43]  [BCDA  6 23 44]
			[ABCD  9  4 45]  [DABC 12 11 46]  [CDAB 15 16 47]  [BCDA  2 23 48]
			*/
			for (int i = 0; i < 16; i += 4)
			{
				int j = (5 + (i * 3));

				state.a = Round3(state.a, state.b, state.c, state.d, (0 + j) % 16, 4, i + 32);
				state.d = Round3(state.d, state.a, state.b, state.c, (3 + j) % 16, 11, i + 33);
				state.c = Round3(state.c, state.d, state.a, state.b, (6 + j) % 16, 16, i + 34);
				state.b = Round3(state.b, state.c, state.d, state.a, (9 + j) % 16, 23, i + 35);
			}

			// Round 4
			/*
			a = b + ((a + I(b,c,d) + X[k] + T[i]) <<< s)

			[ABCD  0  6 49]  [DABC  7 10 50]  [CDAB 14 15 51]  [BCDA  5 21 52]
			[ABCD 12  6 53]  [DABC  3 10 54]  [CDAB 10 15 55]  [BCDA  1 21 56]
			[ABCD  8  6 57]  [DABC 15 10 58]  [CDAB  6 15 59]  [BCDA 13 21 60]
			[ABCD  4  6 61]  [DABC 11 10 62]  [CDAB  2 15 63]  [BCDA  9 21 64]
			*/
			for (int i = 0; i < 16; i += 4)
			{
				int j = (i * 7) % 16;

				state.a = Round4(state.a, state.b, state.c, state.d, (0 + j) % 16, 6, i + 48);
				state.d = Round4(state.d, state.a, state.b, state.c, (7 + j) % 16, 10, i + 49);
				state.c = Round4(state.c, state.d, state.a, state.b, (14 + j) % 16, 15, i + 50);
				state.b = Round4(state.b, state.c, state.d, state.a, (21 + j) % 16, 21, i + 51);
			}

			this._state.a += state.a;
			this._state.b += state.b;
			this._state.c += state.c;
			this._state.d += state.d;

			this.WriteIndex = 0;
		}

		internal unsafe void SetLength(long cbPlaintext)
		{
			fixed (uint* pBuf = &this._block[Md5.BlockSize / 4 - 2])
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
				throw new ArgumentException(Messages.Md5_InvalidBufferSize);

			Debug.Assert(this.WriteIndex < Md5.BlockSize);

			this.MarkEnd();
			if (this.WriteIndex >= Md5.BlockSize)
				this.HashBuffer();

			if (this.WriteIndex < (Md5.BlockSize - 8))
			{
				this.ZeroBufferBytes(this.WriteIndex, (Md5.BlockSize - 8) - this.WriteIndex);
			}
			else
			{
				int writeIndex2 = this.WriteIndex;
				this.ZeroBufferBytes(this.WriteIndex, Md5.BlockSize - this.WriteIndex);
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
				*(Md5State*)pDigest = this._state;
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
			return (x & z) | (y & ~z);
		}
		private static uint H(uint x, uint y, uint z)
		{
			return (x ^ y ^ z);
		}
		private static uint I(uint x, uint y, uint z)
		{
			return y ^ (x | ~z);
		}

		private uint Round1(uint a, uint b, uint c, uint d, int k, int s)
		{
			return b + BitHelper.RotateLeft((a + F(b, c, d) + this._block[k] + Md5.T[k]), s);
		}

		private uint Round2(uint a, uint b, uint c, uint d, int k, int s, int i)
		{
			return b + BitHelper.RotateLeft((a + G(b, c, d) + this._block[k] + Md5.T[i]), s);
		}

		private uint Round3(uint a, uint b, uint c, uint d, int k, int s, int i)
		{
			return b + BitHelper.RotateLeft((a + H(b, c, d) + this._block[k] + Md5.T[i]), s);
		}

		private uint Round4(uint a, uint b, uint c, uint d, int k, int s, int i)
		{
			return b + BitHelper.RotateLeft((a + I(b, c, d) + this._block[k] + Md5.T[i]), s);
		}

	}

	public class Md5 : SlimHashAlgorithm<Md5Context>
	{
		public const int BlockSize = 512 / 8;
		public static int DigestSize => Md5State.StructSize;

		internal readonly static uint[] T = CreateSineTable();

		private static uint[] CreateSineTable()
		{
			uint[] t = new uint[64];
			for (int i = 0; i < 64; i++)
			{
				t[i] = (uint)(4294967296 * Math.Abs(Math.Sin(i + 1)));
			}
			return t;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Md5State
	{
		public static unsafe int StructSize => sizeof(Md5State);

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
