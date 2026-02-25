using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
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

		internal const int BufferWordCount = 64;

		internal static uint ch(uint x, uint y, uint z)
			=> (x & y) ^ (~x & z);
		internal static uint maj(uint x, uint y, uint z)
			=> (x & y) ^ (x & z) ^ (y & z);
		internal static uint bsig0(uint x)
			=> BitOperations.RotateRight(x, 2) ^ BitOperations.RotateRight(x, 13) ^ BitOperations.RotateRight(x, 22);
		internal static uint bsig1(uint x)
			=> BitOperations.RotateRight(x, 6) ^ BitOperations.RotateRight(x, 11) ^ BitOperations.RotateRight(x, 25);
		internal static uint ssig0(uint x)
			=> BitOperations.RotateRight(x, 7) ^ BitOperations.RotateRight(x, 18) ^ (x >> 3);
		internal static uint ssig1(uint x)
			=> BitOperations.RotateRight(x, 17) ^ BitOperations.RotateRight(x, 19) ^ (x >> 10);
	}

	interface ISha2SmallPolicy
	{
		void InitializeState(ref Sha2SmallState state);
		int DigestSize { get; }
	}

	[InlineArray(Sha224_256.BufferWordCount)]
	struct Sha2SmallBlock
	{
		internal uint w;
	}
	internal struct Sha2SmallContext<TPolicy> : IHashContext, IHashBuffer
		where TPolicy : struct, ISha2SmallPolicy
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Sha2SmallState _state;

		internal Sha2SmallBlock _buffer;
		private Span<uint> W => MemoryMarshal.CreateSpan<uint>(ref this._buffer.w, Sha224_256.BufferWordCount);

		public Span<byte> InputBuffer => MemoryMarshal.AsBytes(W);


		public int DigestSizeBytes => new TPolicy().DigestSize;
		public static int StaticDigestSizeBytes => new TPolicy().DigestSize;
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

			for (int i = 16; i < Sha224_256.BufferWordCount; i++)
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

		internal void SetLength(long cbPlaintext)
		{
			cbPlaintext *= 8;
			BinaryPrimitives.WriteInt64BigEndian(this.InputBuffer.Slice(Sha2_Small.BlockSize - 8, 8), cbPlaintext);
		}

		internal void MarkEnd()
		{
			this.InputBuffer[this.WriteIndex++] = 0x80;
		}

		internal void ZeroBufferBytes(int startIndex, int count)
		{
			var pBytes = this.InputBuffer.Slice(startIndex);
			for (int i = 0; i < count; i++)
			{
				pBytes[i] = 0;
			}
		}

		public void HashFinal(Span<byte> digestBuffer)
		{
			if (digestBuffer.Length < this.DigestSizeBytes)
				throw new ArgumentException(Messages.Sha1_InvalidBufferSize);

			Debug.Assert(this.WriteIndex < Sha2_Small.BlockSize);

			this.MarkEnd();

			if (this.WriteIndex <= (Sha2_Small.BlockSize - 8))
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

		private void GetDigest(Span<byte> digestBuffer)
		{
			Debug.Assert(digestBuffer.Length >= this.DigestSizeBytes);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x00, 4), this._state.h0);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x04, 4), this._state.h1);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x08, 4), this._state.h2);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x0C, 4), this._state.h3);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x10, 4), this._state.h4);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x14, 4), this._state.h5);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x18, 4), this._state.h6);
			if (digestBuffer.Length >= 0x20)
			{
				BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0x1C, 4), this._state.h7);
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
		public const int StructSize = 8 * 4;

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
