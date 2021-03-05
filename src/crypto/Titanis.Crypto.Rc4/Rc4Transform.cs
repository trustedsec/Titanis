using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;

namespace Titanis.Crypto
{
	unsafe struct SBox
	{
		const int SBoxSize = 256;
		private fixed byte _s[SBoxSize];
		internal unsafe Span<byte> S
		{
			get
			{
				fixed (byte* pSBox = this._s)
				{
					return new Span<byte>(pSBox, SBoxSize);
				}
			}
		}
	}

	internal class Rc4Transform : ICryptoTransform
	{
		internal Rc4Transform(ReadOnlySpan<byte> key)
		{
			Debug.Assert(Rc4.IsKeyValid(key));

			this._context.Initialize(key);
		}

		public bool CanReuseTransform => false;

		public bool CanTransformMultipleBlocks => true;

		public int InputBlockSize => throw new System.NotImplementedException();

		public int OutputBlockSize => throw new System.NotImplementedException();

		public void Dispose()
		{
		}

		private Rc4Context _context;

		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this._context.Transform(
				new ReadOnlySpan<byte>(inputBuffer, inputOffset, inputCount),
				new Span<byte>(outputBuffer, outputOffset, inputCount)
				);

			return inputCount;
		}

		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] final = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, final, 0);
			return final;
		}
	}
}