using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto
{
	[InlineArray(Sha1Context.BlockWordCount)]
	struct Sha1BlockWords
	{
		internal uint w;
	}

	public struct Sha1Context : IHashContext, IHashBuffer
	{
		public long InputSize { get; set; }
		public int WriteIndex { get; set; }

		internal Sha1State _state;

		internal const int BlockWordCount = 80;
		internal Sha1BlockWords _block;
		public Span<byte> InputBuffer => MemoryMarshal.Cast<uint, byte>(this.W);

		private Span<uint> W => MemoryMarshal.CreateSpan(ref this._block.w, BlockWordCount);

		public int DigestSizeBytes => Sha1State.StructSize;
		public static int StaticDigestSizeBytes => Sha1State.StructSize;
		public int InputBlockSizeBytes => Sha1.BlockSize;

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
			Sha1State state = this._state;

			//          W(t) = S^1(W(t-3) XOR W(t-8) XOR W(t-14) XOR W(t-16))
			var w = this.W;
			for (int i = 0; i < 16; i++)
			{
				w[i] = BinaryPrimitives.ReverseEndianness(w[i]);
			}
			for (int t = 16; t < BlockWordCount; t++)
			{
				w[t] = BitOperations.RotateLeft(w[t - 3] ^ w[t - 8] ^ w[t - 14] ^ w[t - 16], 1);
			}

			for (int t = 0; t < 20; t++)
			{
				var temp = BitOperations.RotateLeft(state.a, 5) + F0(state.b, state.c, state.d) + state.e + w[t] + K0;
				state.e = state.d;
				state.d = state.c;
				state.c = BitOperations.RotateLeft(state.b, 30);
				state.b = state.a;
				state.a = temp;
			}
			for (int t = 20; t < 40; t++)
			{
				var temp = BitOperations.RotateLeft(state.a, 5) + F1(state.b, state.c, state.d) + state.e + w[t] + K1;
				state.e = state.d;
				state.d = state.c;
				state.c = BitOperations.RotateLeft(state.b, 30);
				state.b = state.a;
				state.a = temp;
			}
			for (int t = 40; t < 60; t++)
			{
				var temp = BitOperations.RotateLeft(state.a, 5) + F2(state.b, state.c, state.d) + state.e + w[t] + K2;
				state.e = state.d;
				state.d = state.c;
				state.c = BitOperations.RotateLeft(state.b, 30);
				state.b = state.a;
				state.a = temp;
			}
			for (int t = 60; t < BlockWordCount; t++)
			{
				var temp = BitOperations.RotateLeft(state.a, 5) + F3(state.b, state.c, state.d) + state.e + w[t] + K3;
				state.e = state.d;
				state.d = state.c;
				state.c = BitOperations.RotateLeft(state.b, 30);
				state.b = state.a;
				state.a = temp;
			}

			this._state.a += state.a;
			this._state.b += state.b;
			this._state.c += state.c;
			this._state.d += state.d;
			this._state.e += state.e;

			this.WriteIndex = 0;
		}

		internal void SetLength(long cbPlaintext)
		{
			cbPlaintext *= 8;
			BinaryPrimitives.WriteInt64BigEndian(this.InputBuffer.Slice(Sha1.BlockSize - 8, 8), cbPlaintext);
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

			Debug.Assert(this.WriteIndex < Sha1.BlockSize);

			this.MarkEnd();

			if (this.WriteIndex <= (Sha1.BlockSize - 8))
			{
				this.ZeroBufferBytes(this.WriteIndex, (Sha1.BlockSize - 8) - this.WriteIndex);
			}
			else
			{
				this.ZeroBufferBytes(this.WriteIndex, Sha1.BlockSize - this.WriteIndex);
				this.HashBuffer();
				this.ZeroBufferBytes(0, Sha1.BlockSize - 8);
			}

			this.SetLength(this.InputSize);
			this.HashBuffer();

			GetDigest(digestBuffer);
		}

		private void GetDigest(Span<byte> digestBuffer)
		{
			Debug.Assert(digestBuffer.Length >= this.DigestSizeBytes);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(0, 4), this._state.a);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(4, 4), this._state.b);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(8, 4), this._state.c);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(12, 4), this._state.d);
			BinaryPrimitives.WriteUInt32BigEndian(digestBuffer.Slice(16, 4), this._state.e);
		}


		//private static uint F(int t, uint b, uint c, uint d)
		//{
		//	return (t / 20) switch
		//	{
		//		0 => F0(b, c, d),
		//		1 => F1(b, c, d),
		//		2 => F2(b, c, d),
		//		3 => F3(b, c, d),
		//	};
		//}

		private const int K0 = 0x5A827999;
		private const int K1 = 0x6ED9EBA1;
		private const uint K2 = 0x8F1BBCDC;
		private const uint K3 = 0xCA62C1D6;

		//private static uint K(int t)
		//{
		//	return (t / 20) switch
		//	{
		//		0 => K0,
		//		1 => K1,
		//		2 => K2,
		//		3 => K3
		//	};
		//}
		private static uint F0(uint b, uint c, uint d)
		{
			return (b & c) | (~b & d);
		}
		private static uint F1(uint b, uint c, uint d)
		{
			return (b ^ c ^ d);
		}
		private static uint F2(uint b, uint c, uint d)
		{
			return (b & c) | (b & d) | (c & d);
		}
		private static uint F3(uint b, uint c, uint d)
		{
			return (b ^ c ^ d);
		}

	}

	public class Sha1 : SlimHashAlgorithm<Sha1Context>
	{
		public const int BlockSize = 512 / 8;
		public static int DigestSize => Sha1State.StructSize;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Sha1State
	{
		public const int StructSize = 4 * 5;

		internal uint a;
		internal uint b;
		internal uint c;
		internal uint d;
		internal uint e;

		internal const uint InitialWord0 = 0x67452301;
		internal const uint InitialWord1 = 0xEFCDAB89;
		internal const uint InitialWord2 = 0x98badcfe;
		internal const uint InitialWord3 = 0x10325476;
		internal const uint InitialWord4 = 0xC3D2E1F0;

		internal void Initialize()
		{
			this.a = InitialWord0;
			this.b = InitialWord1;
			this.c = InitialWord2;
			this.d = InitialWord3;
			this.e = InitialWord4;
		}
	}

}
