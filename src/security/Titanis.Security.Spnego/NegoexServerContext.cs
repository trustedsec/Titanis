using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;

namespace Titanis.Security.Spnego
{
	public class NegoexServerContext : AuthServerContext
	{
		public override Asn1Oid MechOid => NegoexClientContext.NegoexOid;

		public override bool IsComplete => false;

		public override bool HasSessionKey => throw new NotImplementedException();

		public override int SessionKeySize => throw new NotImplementedException();

		public override SecurityCapabilities NegotiatedCapabilities => SecurityCapabilities.None;

		public override SecurityCapabilities SupportedCapabilities => throw new NotImplementedException();

		public override int GetWrapTokenSize(WrapOptions options)
		{
			throw new NotImplementedException();
		}

		public override void IncrementRecvSeqNbr()
		{
			throw new NotImplementedException();
		}

		public override void SealMessage(in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}

		public override void SignMessage(in MessageSignParams signParams, MessageSignOptions options)
		{
			throw new NotImplementedException();
		}

		public override void UnsealMessage(in MessageSealParams unsealParams)
		{
			throw new NotImplementedException();
		}

		public override void VerifyMessage(in MessageVerifyParams verifyParams, MessageSignOptions options)
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> AcceptImpl()
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> AcceptImpl(ReadOnlySpan<byte> token)
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			throw new NotImplementedException();
		}
	}
}
