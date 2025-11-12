using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Titanis.Crypto
{
	/// <summary>
	/// Represents an implementation of a hash algorithm.
	/// </summary>
	public abstract class SlimHashAlgorithm
	{
		public static byte[] ComputeHmac<T>(ReadOnlySpan<byte> key, ReadOnlySpan<byte> input)
			where T : struct, IHashContext
		{
			int digestSize = T.StaticDigestSizeBytes;
			byte[] digestBuffer = new byte[digestSize];
			ComputeHmac<T>(key, input, digestBuffer);
			return digestBuffer;
		}

		internal const byte IPAD = 0x36;
		internal const byte OPAD = 0x5C;

		public static void ComputeHmac<T>(ReadOnlySpan<byte> key, ReadOnlySpan<byte> input, Span<byte> digestBuffer)
			where T : struct, IHashContext
		{
			HmacContext<T> ctx = new HmacContext<T>(key);
			ctx.Initialize();
			ctx.HashData(input);
			ctx.HashFinal(digestBuffer);
		}

		public static void ComputeHmacInner<T>(ReadOnlySpan<byte> key, ReadOnlySpan<byte> input, Span<byte> digestBuffer)
			where T : struct, IHashContext
		{
		}

		public static byte[] ComputeHash<T>(ReadOnlySpan<byte> input)
			where T : struct, IHashContext
		{
			int digestSize = T.StaticDigestSizeBytes;
			byte[] digestBuffer = new byte[digestSize];
			ComputeHash<T>(input, digestBuffer);
			return digestBuffer;
		}

		public static void ComputeHash<T>(ReadOnlySpan<byte> input, Span<byte> digestBuffer)
			where T : struct, IHashContext
		{
			T ctx = new T();
			ctx.Initialize();
			ctx.HashData(input);
			ctx.HashFinal(digestBuffer);
		}

		public static void HashData<T>(ReadOnlySpan<byte> input, ref T context)
			where T : IHashBuffer
		{
			int cbSize = input.Length;
			if (cbSize == 0)
				return;

			int ibStart = 0;

			context.InputSize += cbSize;
			while (cbSize > 0)
			{
				int cbFree = context.InputBlockSizeBytes - context.WriteIndex;
				Debug.Assert(cbFree > 0);

				int cbCopy = Math.Min(cbFree, cbSize);
				var inputBuffer = context.InputBuffer;
				input.Slice(ibStart, cbCopy).CopyTo(inputBuffer.Slice(context.WriteIndex, cbCopy));
				context.WriteIndex += cbCopy;

				cbSize -= cbCopy;
				ibStart += cbCopy;

				cbFree = context.InputBlockSizeBytes - context.WriteIndex;
				if (cbFree == 0)
				{
					context.HashBuffer();
				}
			}
		}

		public abstract void Initialize();
	}

	public class SlimHashAlgorithm<T> : SlimHashAlgorithm
		where T : struct, IHashContext
	{
		private T _context = new T();
		private bool _isInit;

		protected SlimHashAlgorithm()
		{
		}

		public override void Initialize()
		{
			this._context.Initialize();
			this._isInit = true;
		}

		protected void HashCore(ReadOnlySpan<byte> input)
		{
			if (!this._isInit)
				this.Initialize();

			this._context.HashData(input);
		}
	}

}
