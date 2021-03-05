using System.Threading;
using Titanis.Crypto;

namespace Titanis.Security.Ntlm
{
	struct NtlmCryptoContext
	{
		internal Buffer128 sessionKey;
		internal Buffer128 signKeyC2S;
		internal Buffer128 signKeyS2C;
		internal Rc4Context sealKeyC2S;
		internal Rc4Context sealKeyS2C;

		internal Rc4Context sealKeyC2SOrig;
		internal Rc4Context sealKeyS2COrig;

		internal void SetCryptoContext(NtlmAuthResult authResult)
		{
			this.sessionKey = authResult.exportedSessionKey;
			this.signKeyC2S = authResult.signKeyC2S;
			this.signKeyS2C = authResult.signKeyS2C;

			if (authResult.shortSealKey)
			{
				this.sealKeyC2SOrig = new Rc4Context(authResult.sealKeyC2S.AsSpan().Slice(0, 8));
				this.sealKeyS2COrig = new Rc4Context(authResult.sealKeyS2C.AsSpan().Slice(0, 8));
			}
			else
			{
				this.sealKeyC2SOrig = new Rc4Context(authResult.sealKeyC2S.AsSpan());
				this.sealKeyS2COrig = new Rc4Context(authResult.sealKeyS2C.AsSpan());
			}

			this.ResetC2S();
			this.ResetS2C();
			this._seqNbrC2S = -1;
			this._seqNbrS2C = -1;
		}

		internal void ResetC2S()
		{
			this.sealKeyC2S = this.sealKeyC2SOrig;
		}

		internal void ResetS2C()
		{
			this.sealKeyS2C = this.sealKeyS2COrig;
		}

		private int _seqNbrC2S;
		public uint GetNextSeqNbrC2S()
		{
			return (uint)Interlocked.Increment(ref this._seqNbrC2S);
		}
		private int _seqNbrS2C;
		public uint GetNextSeqNbrS2C()
		{
			return (uint)Interlocked.Increment(ref this._seqNbrS2C);
		}
	}

}
