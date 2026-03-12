using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;

namespace Titanis.Security.Spnego
{
	public class NegoexClientContext : AuthClientContext
	{
		public static readonly Asn1Oid NegoexOid = new Asn1Oid("1.3.6.1.4.1.311.2.2.30");
		public override Asn1Oid MechOid => NegoexOid;

		public override string UserName => throw new NotImplementedException();

		public override SecurityPrincipalName? TargetSpn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public override bool IsComplete => throw new NotImplementedException();

		public override ReadOnlySpan<byte> Token => throw new NotImplementedException();

		public override bool HasSessionKey => throw new NotImplementedException();

		public override int SessionKeySize => throw new NotImplementedException();

		public override SecurityCapabilities NegotiatedCapabilities => throw new NotImplementedException();

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

		protected override ReadOnlySpan<byte> GetSessionKeyImpl()
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> InitializeImpl()
		{
			throw new NotImplementedException();
		}

		protected override ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token)
		{
			throw new NotImplementedException();
		}
	}
}
