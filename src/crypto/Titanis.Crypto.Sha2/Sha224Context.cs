using System;

namespace Titanis.Crypto
{
	public struct Sha224Context : IHashContext, IHashBuffer
	{
		internal Sha2SmallContext<Sha224Policy> ctx;

		public int DigestSizeBytes => 224 / 8;

		public int InputBlockSizeBytes => this.ctx.InputBlockSizeBytes;

		public long InputSize { get => this.ctx.InputSize; set => this.ctx.InputSize = value; }
		public int WriteIndex { get => this.ctx.WriteIndex; set => this.ctx.WriteIndex = value; }

		public Span<byte> InputBuffer => ((IHashBuffer)this.ctx).InputBuffer;

		public void HashBuffer()
		{
			this.ctx.HashBuffer();
		}

		public void HashData(ReadOnlySpan<byte> block)
		{
			this.ctx.HashData(block);
		}

		public void HashFinal(Span<byte> digestBuffer)
		{
			this.ctx.HashFinal(digestBuffer);
		}

		public void Initialize()
		{
			this.ctx.Initialize();
		}

	}

	struct Sha224Policy : ISha2SmallPolicy
	{
		public int DigestSize => 224 / 8;

		void ISha2SmallPolicy.InitializeState(ref Sha2SmallState state)
		{
			state.Initialize224();
		}

	}
}
