using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Crypto;

namespace Titanis.Security.Ntlm
{
	public class NtlmHashCredential : NtlmCredential
	{
		public Buffer128 LMHash { get; set; }
		public Buffer128 NtlmHash { get; set; }

		public NtlmHashCredential(
			string userName,
			Buffer128 lmHash,
			Buffer128 ntlmHash
			)
			: base(userName)
		{
			this.LMHash = lmHash;
			this.NtlmHash = ntlmHash;
		}
		public NtlmHashCredential(
			string userName,
			string domain,
			Buffer128 lmHash,
			Buffer128 ntlmHash
			)
			: base(userName, domain)
		{
			this.LMHash = lmHash;
			this.NtlmHash = ntlmHash;
		}

		public override bool CanProvideResponseKeyLM => !this.LMHash.IsEmpty;
		public override bool CanProvideResponseKeyNT => !this.NtlmHash.IsEmpty;

		internal override Buffer128 GetResponseKeyLMv1() => this.LMHash;
		internal override Buffer128 GetResponseKeyNTv1() => this.NtlmHash;

		internal override Buffer128 GetResponseKeyLMv2() => this.GetResponseKeyNTv2();
		internal override Buffer128 GetResponseKeyNTv2() => Ntlm.NtowfV2(
			this.UserName,
			this.Domain,
			this.NtlmHash
			);

		//internal override NtlmResponseKeysV1 ComputeKeysV1()
		//{
		//	if (this.IsAnonymous)
		//	{
		//		// Anonymous
		//		return new NtlmResponseKeysV1();
		//	}
		//	else
		//	{
		//		return new NtlmResponseKeysV1
		//		{
		//			ResponseKeyNT = Ntlm.NtowfV1(this.Password),
		//			ResponseKeyLM = Ntlm.LmowfV1(this.Password)
		//		};
		//	}
		//}

		//internal override NtlmResponseKeysV2 ComputeKeysV2()
		//{
		//	if (this.IsAnonymous)
		//	{
		//		// Anonymous
		//		return new NtlmResponseKeysV2();
		//	}
		//	else
		//	{
		//		var responseKeyNT = Ntlm.NtowfV2(
		//			this.Password,
		//			this.UserName,
		//			this.Domain);
		//		return new NtlmResponseKeysV2
		//		{
		//			ResponseKeyNT = responseKeyNT,
		//			ResponseKeyLM = responseKeyNT
		//		};
		//	}
		//}
	}
}
