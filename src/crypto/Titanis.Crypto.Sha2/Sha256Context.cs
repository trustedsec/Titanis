using System;

namespace Titanis.Crypto
{
	public partial struct Sha256Context : IHashContext, IHashBuffer
	{
		internal Sha2SmallContext<Sha256Policy> ctx;

		public int DigestSizeBytes => 256 / 8;

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

	struct Sha256Policy: ISha2SmallPolicy
	{
		public int DigestSize => 256 / 8;

		void ISha2SmallPolicy.InitializeState(ref Sha2SmallState state)
		{
			state.Initialize256();
		}
	}
}
