using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto
{
	static class Sha384_512
	{
		internal static readonly ulong[] k = new ulong[]
		{
			0x428a2f98d728ae22, 0x7137449123ef65cd, 0xb5c0fbcfec4d3b2f, 0xe9b5dba58189dbbc,
			0x3956c25bf348b538, 0x59f111f1b605d019, 0x923f82a4af194f9b, 0xab1c5ed5da6d8118,
			0xd807aa98a3030242, 0x12835b0145706fbe, 0x243185be4ee4b28c, 0x550c7dc3d5ffb4e2,
			0x72be5d74f27b896f, 0x80deb1fe3b1696b1, 0x9bdc06a725c71235, 0xc19bf174cf692694,
			0xe49b69c19ef14ad2, 0xefbe4786384f25e3, 0x0fc19dc68b8cd5b5, 0x240ca1cc77ac9c65,
			0x2de92c6f592b0275, 0x4a7484aa6ea6e483, 0x5cb0a9dcbd41fbd4, 0x76f988da831153b5,
			0x983e5152ee66dfab, 0xa831c66d2db43210, 0xb00327c898fb213f, 0xbf597fc7beef0ee4,
			0xc6e00bf33da88fc2, 0xd5a79147930aa725, 0x06ca6351e003826f, 0x142929670a0e6e70,
			0x27b70a8546d22ffc, 0x2e1b21385c26c926, 0x4d2c6dfc5ac42aed, 0x53380d139d95b3df,
			0x650a73548baf63de, 0x766a0abb3c77b2a8, 0x81c2c92e47edaee6, 0x92722c851482353b,
			0xa2bfe8a14cf10364, 0xa81a664bbc423001, 0xc24b8b70d0f89791, 0xc76c51a30654be30,
			0xd192e819d6ef5218, 0xd69906245565a910, 0xf40e35855771202a, 0x106aa07032bbd1b8,
			0x19a4c116b8d2d0c8, 0x1e376c085141ab53, 0x2748774cdf8eeb99, 0x34b0bcb5e19b48a8,
			0x391c0cb3c5c95a63, 0x4ed8aa4ae3418acb, 0x5b9cca4f7763e373, 0x682e6ff3d6b2b8a3,
			0x748f82ee5defb2fc, 0x78a5636f43172f60, 0x84c87814a1f0ab72, 0x8cc702081a6439ec,
			0x90befffa23631e28, 0xa4506cebde82bde9, 0xbef9a3f7b2c67915, 0xc67178f2e372532b,
			0xca273eceea26619c, 0xd186b8c721c0c207, 0xeada7dd6cde0eb1e, 0xf57d4f7fee6ed178,
			0x06f067aa72176fba, 0x0a637dc5a2c898a6, 0x113f9804bef90dae, 0x1b710b35131c471b,
			0x28db77f523047d84, 0x32caab7b40c72493, 0x3c9ebe0a15c9bebc, 0x431d67c49c100d4c,
			0x4cc5d4becb3e42b6, 0x597f299cfc657e2a, 0x5fcb6fab3ad6faec, 0x6c44198c4a475817
		};

		internal static ulong ch(ulong x, ulong y, ulong z)
			=> (x & y) ^ (~x & z);
		internal static ulong maj(ulong x, ulong y, ulong z)
			=> (x & y) ^ (x & z) ^ (y & z);
		internal static ulong bsig0(ulong x)
			=> BitHelper.RotateRight(x, 28) ^ BitHelper.RotateRight(x, 34) ^ BitHelper.RotateRight(x, 39);
		internal static ulong bsig1(ulong x)
			=> BitHelper.RotateRight(x, 14) ^ BitHelper.RotateRight(x, 18) ^ BitHelper.RotateRight(x, 41);
		internal static ulong ssig0(ulong x)
			=> BitHelper.RotateRight(x, 1) ^ BitHelper.RotateRight(x, 8) ^ (x >> 7);
		internal static ulong ssig1(ulong x)
			=> BitHelper.RotateRight(x, 19) ^ BitHelper.RotateRight(x, 61) ^ (x >> 6);
	}

	interface ISha2LargePolicy
	{
		void InitializeState(ref Sha2LargeState state);
		int DigestSize { get; }
	}
	internal unsafe struct Sha2LargeContext<TPolicy> : IHashContext, IHashBuffer
		where TPolicy : struct, ISha2LargePolicy
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Sha2LargeState _state;

		private const int BufferWordCount = 80;
		internal fixed ulong _buffer[BufferWordCount];
		private Span<ulong> W
		{
			get
			{
				fixed (ulong* pBlock = this._buffer)
				{
					return new Span<ulong>(pBlock, BufferWordCount);
				}
			}
		}

		public Span<byte> InputBuffer
		{
			get
			{
				fixed (ulong* pBlock = this._buffer)
				{
					return new Span<byte>((byte*)pBlock, InputBlockSizeBytes);
				}
			}
		}


		public int DigestSizeBytes => new TPolicy().DigestSize;
		public int InputBlockSizeBytes => Sha512.BlockSize;

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
					Sha384_512.ssig1(w[i - 2])
					+ w[i - 7]
					+ Sha384_512.ssig0(w[i - 15])
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

			for (int i = 0; i < BufferWordCount; i++)
			{
				var t1 = h
					+ Sha384_512.bsig1(e)
					+ Sha384_512.ch(e, f, g)
					+ Sha384_512.k[i]
					+ w[i];
				var t2 = Sha384_512.bsig0(a) + Sha384_512.maj(a, b, c);

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
			fixed (ulong* pBuf = &this._buffer[Sha512.BlockSize / 8 - 2])
			{
				*(long*)pBuf = 0;
				*(long*)(pBuf + 1) = ((cbPlaintext));
			}
		}

		internal unsafe void MarkEnd()
		{
			fixed (ulong* pBuf = this._buffer)
			{
				byte* pBytes = (byte*)pBuf;
				pBytes[this.WriteIndex++] = 0x80;
			}
		}

		internal unsafe void ZeroBufferBytes(int startIndex, int count)
		{
			fixed (ulong* pBuf = this._buffer)
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

			Debug.Assert(this.WriteIndex < Sha512.BlockSize);

			this.MarkEnd();

			if (this.WriteIndex < (Sha512.BlockSize - 16))
			{
				this.ZeroBufferBytes(this.WriteIndex, (Sha512.BlockSize - 16) - this.WriteIndex);
			}
			else
			{
				this.ZeroBufferBytes(this.WriteIndex, Sha512.BlockSize - this.WriteIndex);
				this.HashBuffer();
				this.ZeroBufferBytes(0, Sha512.BlockSize - 16);
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
				ref Sha2LargeState digest = ref *(Sha2LargeState*)pDigest;
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

	public class Sha384 : SlimHashAlgorithm<Sha224Context>
	{
		public const int BlockSize = 1024 / 8;
		public static int DigestSize => 384 / 8;
	}

	public class Sha512 : SlimHashAlgorithm<Sha256Context>
	{
		public const int BlockSize = 1024 / 8;
		public static int DigestSize => Sha2LargeState.StructSize;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Sha2LargeState
	{
		public static unsafe int StructSize => sizeof(Sha2LargeState);

		internal ulong h0;
		internal ulong h1;
		internal ulong h2;
		internal ulong h3;
		internal ulong h4;
		internal ulong h5;
		internal ulong h6;
		internal ulong h7;

		internal const ulong InitialWord384_0 = 0xcbbb9d5dc1059ed8;
		internal const ulong InitialWord384_1 = 0x629a292a367cd507;
		internal const ulong InitialWord384_2 = 0x9159015a3070dd17;
		internal const ulong InitialWord384_3 = 0x152fecd8f70e5939;
		internal const ulong InitialWord384_4 = 0x67332667ffc00b31;
		internal const ulong InitialWord384_5 = 0x8eb44a8768581511;
		internal const ulong InitialWord384_6 = 0xdb0c2e0d64f98fa7;
		internal const ulong InitialWord384_7 = 0x47b5481dbefa4fa4;

		internal void Initialize384()
		{
			this.h0 = InitialWord384_0;
			this.h1 = InitialWord384_1;
			this.h2 = InitialWord384_2;
			this.h3 = InitialWord384_3;
			this.h4 = InitialWord384_4;
			this.h5 = InitialWord384_5;
			this.h6 = InitialWord384_6;
			this.h7 = InitialWord384_7;
		}

		internal const ulong InitialWord512_0 = 0x6a09e667f3bcc908;
		internal const ulong InitialWord512_1 = 0xbb67ae8584caa73b;
		internal const ulong InitialWord512_2 = 0x3c6ef372fe94f82b;
		internal const ulong InitialWord512_3 = 0xa54ff53a5f1d36f1;
		internal const ulong InitialWord512_4 = 0x510e527fade682d1;
		internal const ulong InitialWord512_5 = 0x9b05688c2b3e6c1f;
		internal const ulong InitialWord512_6 = 0x1f83d9abfb41bd6b;
		internal const ulong InitialWord512_7 = 0x5be0cd19137e2179;

		internal void Initialize512()
		{
			this.h0 = InitialWord512_0;
			this.h1 = InitialWord512_1;
			this.h2 = InitialWord512_2;
			this.h3 = InitialWord512_3;
			this.h4 = InitialWord512_4;
			this.h5 = InitialWord512_5;
			this.h6 = InitialWord512_6;
			this.h7 = InitialWord512_7;
		}
	}

}
