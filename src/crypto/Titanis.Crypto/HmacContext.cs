using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Crypto
{
	public ref struct HmacContext<THashContext>
		where THashContext : struct, IHashContext
	{
		private THashContext _hashContext;
		private ReadOnlySpan<byte> _key;

		public int DigestSizeBytes => this._hashContext.DigestSizeBytes;
		public int InputBlockSizeBytes => this._hashContext.InputBlockSizeBytes;

		public HmacContext(ReadOnlySpan<byte> key)
		{
			this._hashContext = new THashContext();
			this._key = key;
		}

		public void Initialize()
		{
			ref THashContext ctx = ref this._hashContext;

			var key = this._key;
			Span<byte> keybuf = stackalloc byte[ctx.InputBlockSizeBytes];
			if (key.Length > ctx.InputBlockSizeBytes)
			{
				ctx.Initialize();
				ctx.HashData(key);
				ctx.HashFinal(keybuf);
			}
			else
			{
				key.CopyTo(keybuf);
			}

			// Inner
			const byte IPAD = 0x36;
			for (int i = 0; i < ctx.InputBlockSizeBytes; i++)
			{
				keybuf[i] ^= IPAD;
			}
			ctx.Initialize();
			ctx.HashData(keybuf);
		}

		public void HashData(scoped ReadOnlySpan<byte> block)
		{
			this._hashContext.HashData(block);
		}

		public void HashFinal(Span<byte> digestBuffer)
		{
			ref THashContext ctx = ref this._hashContext;

			ctx.HashFinal(digestBuffer);

			// Outer
			Span<byte> keybuf = stackalloc byte[ctx.InputBlockSizeBytes];
			var key = this._key;
			if (key.Length > ctx.InputBlockSizeBytes)
			{
				ctx.Initialize();
				ctx.HashData(key);
				ctx.HashFinal(keybuf);
			}
			else
			{
				key.CopyTo(keybuf);
			}

			for (int i = 0; i < ctx.InputBlockSizeBytes; i++)
			{
				keybuf[i] ^= SlimHashAlgorithm.OPAD;
			}

			ctx.Initialize();
			ctx.HashData(keybuf);
			ctx.HashData(digestBuffer);
			ctx.HashFinal(digestBuffer);
		}
	}
}
