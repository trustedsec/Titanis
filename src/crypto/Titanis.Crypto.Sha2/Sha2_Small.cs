using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto
{
	static class Sha224_256
	{
		internal static readonly uint[] k = new uint[]
		{
			0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
			0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
			0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
			0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
			0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
			0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
			0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
			0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
		};

		internal static uint ch(uint x, uint y, uint z)
			=> (x & y) ^ (~x & z);
		internal static uint maj(uint x, uint y, uint z)
			=> (x & y) ^ (x & z) ^ (y & z);
		internal static uint bsig0(uint x)
			=> BitHelper.RotateRight(x, 2) ^ BitHelper.RotateRight(x, 13) ^ BitHelper.RotateRight(x, 22);
		internal static uint bsig1(uint x)
			=> BitHelper.RotateRight(x, 6) ^ BitHelper.RotateRight(x, 11) ^ BitHelper.RotateRight(x, 25);
		internal static uint ssig0(uint x)
			=> BitHelper.RotateRight(x, 7) ^ BitHelper.RotateRight(x, 18) ^ (x >> 3);
		internal static uint ssig1(uint x)
			=> BitHelper.RotateRight(x, 17) ^ BitHelper.RotateRight(x, 19) ^ (x >> 10);
	}

	interface ISha2SmallPolicy
	{
		void InitializeState(ref Sha2SmallState state);
		int DigestSize { get; }
	}
	internal unsafe struct Sha2SmallContext<TPolicy> : IHashContext, IHashBuffer
		where TPolicy : struct, ISha2SmallPolicy
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Sha2SmallState _state;

		private const int BufferWordCount = 64;
		internal fixed uint _buffer[BufferWordCount];
		private Span<uint> W
		{
			get
			{
				fixed (uint* pBlock = this._buffer)
				{
					return new Span<uint>(pBlock, BufferWordCount);
				}
			}
		}

		public Span<byte> InputBuffer
		{
			get
			{
				fixed (uint* pBlock = this._buffer)
				{
					return new Span<byte>((byte*)pBlock, InputBlockSizeBytes);
				}
			}
		}


		public int DigestSizeBytes => new TPolicy().DigestSize;
		public int InputBlockSizeBytes => Sha2_Small.BlockSize;

		public void Initialize()
		{
			this.WriteIndex = 0;
			this.InputSize = 0;
			new TPolicy().InitializeState(ref this._state);
		}

		public void HashData(ReadOnlySpan<byte> input)
		{
			SlimHashAlgorithm.HashData(input, ref this);
		}


		void IHashBuffer.HashBuffer() => this.HashBuffer();
		internal void HashBuffer()
		{
			var w = this.W;
			for (int i = 0; i < 16; i++)
			{
				w[i] = BinaryPrimitives.ReverseEndianness(w[i]);
			}

			for (int i = 16; i < BufferWordCount; i++)
			{
				w[i] =
					Sha224_256.ssig1(w[i - 2])
					+ w[i - 7]
					+ Sha224_256.ssig0(w[i - 15])
					+ w[i - 16];
			}

			var a = this._state.h0;
			var b = this._state.h1;
			var c = this._state.h2;
			var d = this._state.h3;
			var e = this._state.h4;
			var f = this._state.h5;
			var g = this._state.h6;
			var h = this._state.h7;

			for (int i = 0; i < 64; i++)
			{
				var t1 = h
					+ Sha224_256.bsig1(e)
					+ Sha224_256.ch(e, f, g)
					+ Sha224_256.k[i]
					+ w[i];
				var t2 = Sha224_256.bsig0(a) + Sha224_256.maj(a, b, c);

				h = g;
				g = f;
				f = e;
				e = d + t1;
				d = c;
				c = b;
				b = a;
				a = t1 + t2;
			}

			this._state.h0 += a;
			this._state.h1 += b;
			this._state.h2 += c;
			this._state.h3 += d;
			this._state.h4 += e;
			this._state.h5 += f;
			this._state.h6 += g;
			this._state.h7 += h;

			this.WriteIndex = 0;
		}

		internal unsafe void SetLength(long cbPlaintext)
		{
			cbPlaintext *= 8;
			cbPlaintext = BinaryPrimitives.ReverseEndianness(cbPlaintext);
			//cbPlaintext = (cbPlaintext >> 32) | (cbPlaintext << 32);
			fixed (uint* pBuf = &this._buffer[Sha2_Small.BlockSize / 4 - 2])
			{
				*(long*)pBuf = ((cbPlaintext));
			}
		}

		internal unsafe void MarkEnd()
		{
			fixed (uint* pBuf = this._buffer)
			{
				byte* pBytes = (byte*)pBuf;
				pBytes[this.WriteIndex++] = 0x80;
			}
		}

		internal unsafe void ZeroBufferBytes(int startIndex, int count)
		{
			fixed (uint* pBuf = this._buffer)
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
				throw new ArgumentException(Messages.Sha1_InvalidBufferSize);

			Debug.Assert(this.WriteIndex < Sha2_Small.BlockSize);

			this.MarkEnd();

			if (this.WriteIndex < (Sha2_Small.BlockSize - 8))
			{
				this.ZeroBufferBytes(this.WriteIndex, (Sha2_Small.BlockSize - 8) - this.WriteIndex);
			}
			else
			{
				this.ZeroBufferBytes(this.WriteIndex, Sha2_Small.BlockSize - this.WriteIndex);
				this.HashBuffer();
				this.ZeroBufferBytes(0, Sha2_Small.BlockSize - 8);
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
				ref Sha2SmallState digest = ref *(Sha2SmallState*)pDigest;
				digest.h0 = BinaryPrimitives.ReverseEndianness(this._state.h0);
				digest.h1 = BinaryPrimitives.ReverseEndianness(this._state.h1);
				digest.h2 = BinaryPrimitives.ReverseEndianness(this._state.h2);
				digest.h3 = BinaryPrimitives.ReverseEndianness(this._state.h3);
				digest.h4 = BinaryPrimitives.ReverseEndianness(this._state.h4);
				digest.h5 = BinaryPrimitives.ReverseEndianness(this._state.h5);
				digest.h6 = BinaryPrimitives.ReverseEndianness(this._state.h6);
				digest.h7 = BinaryPrimitives.ReverseEndianness(this._state.h7);
			}
		}
	}

	public class Sha224 : SlimHashAlgorithm<Sha224Context>
	{
		public const int BlockSize = 512 / 8;
		public static int DigestSize => Sha2SmallState.StructSize - 4;
	}

	public class Sha2_Small : SlimHashAlgorithm<Sha256Context>
	{
		public const int BlockSize = 512 / 8;
		public static int DigestSize => Sha2SmallState.StructSize;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Sha2SmallState
	{
		public static unsafe int StructSize => sizeof(Sha2SmallState);

		internal uint h0;
		internal uint h1;
		internal uint h2;
		internal uint h3;
		internal uint h4;
		internal uint h5;
		internal uint h6;
		internal uint h7;

		internal const uint InitialWord224_0 = 0xc1059ed8;
		internal const uint InitialWord224_1 = 0x367cd507;
		internal const uint InitialWord224_2 = 0x3070dd17;
		internal const uint InitialWord224_3 = 0xf70e5939;
		internal const uint InitialWord224_4 = 0xffc00b31;
		internal const uint InitialWord224_5 = 0x68581511;
		internal const uint InitialWord224_6 = 0x64f98fa7;
		internal const uint InitialWord224_7 = 0xbefa4fa4;

		internal void Initialize224()
		{
			this.h0 = InitialWord224_0;
			this.h1 = InitialWord224_1;
			this.h2 = InitialWord224_2;
			this.h3 = InitialWord224_3;
			this.h4 = InitialWord224_4;
			this.h5 = InitialWord224_5;
			this.h6 = InitialWord224_6;
			this.h7 = InitialWord224_7;
		}

		internal const uint InitialWord256_0 = 0x6a09e667;
		internal const uint InitialWord256_1 = 0xbb67ae85;
		internal const uint InitialWord256_2 = 0x3c6ef372;
		internal const uint InitialWord256_3 = 0xa54ff53a;
		internal const uint InitialWord256_4 = 0x510e527f;
		internal const uint InitialWord256_5 = 0x9b05688c;
		internal const uint InitialWord256_6 = 0x1f83d9ab;
		internal const uint InitialWord256_7 = 0x5be0cd19;

		internal void Initialize256()
		{
			this.h0 = InitialWord256_0;
			this.h1 = InitialWord256_1;
			this.h2 = InitialWord256_2;
			this.h3 = InitialWord256_3;
			this.h4 = InitialWord256_4;
			this.h5 = InitialWord256_5;
			this.h6 = InitialWord256_6;
			this.h7 = InitialWord256_7;
		}
	}

}
