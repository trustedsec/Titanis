using System;

namespace Titanis.Crypto
{
	public partial struct Sha384Context : IHashContext, IHashBuffer
	{
		internal Sha2LargeContext<Sha384Policy> ctx;

		public int DigestSizeBytes => 384 / 8;

		public int InputBlockSizeBytes => this.ctx.InputBlockSizeBytes;

		public long InputSize { get => this.ctx.InputSize; set => ((IHashBuffer)this.ctx).InputSize = value; }
		public int WriteIndex { get => this.ctx.WriteIndex; set => ((IHashBuffer)this.ctx).WriteIndex = value; }

		public Span<byte> InputBuffer => this.ctx.InputBuffer;

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

	struct Sha384Policy: ISha2LargePolicy
	{
		public int DigestSize => 384 / 8;

		void ISha2LargePolicy.InitializeState(ref Sha2LargeState state)
		{
			state.Initialize384();
		}
	}
}
