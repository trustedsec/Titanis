using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Crypto;

namespace Titanis.Security.Ntlm
{
	public enum NtlmCredentialOptions
	{
		None = 0,
		ProvideLM = 1,
		ProvideNtlm = 2,
		ProvideBoth = ProvideLM | ProvideNtlm
	}

	public class NtlmPasswordCredential : NtlmCredential
	{
		public string Password { get; set; }
		public NtlmCredentialOptions Options { get; set; } = NtlmCredentialOptions.ProvideBoth;

		public NtlmPasswordCredential(
			string userName,
			string password
			)
			: base(userName)
		{
			if (password == null)
				throw new ArgumentNullException(nameof(password));

			this.Password = password;
		}
		public NtlmPasswordCredential(
			string userName,
			string? domain,
			string password
			)
			: base(userName, domain)
		{
			if (password == null)
				throw new ArgumentNullException(nameof(password));

			this.Password = password;
		}

		public override bool IsAnonymous => string.IsNullOrEmpty(this.UserName);

		public override bool CanProvideResponseKeyLM => (0 != (this.Options & NtlmCredentialOptions.ProvideLM));
		public override bool CanProvideResponseKeyNT => (0 != (this.Options & NtlmCredentialOptions.ProvideNtlm));

		internal override Buffer128 GetResponseKeyLMv1() => Ntlm.LmowfV1(this.Password);
		internal override Buffer128 GetResponseKeyNTv1() => Ntlm.NtowfV1(this.Password);

		internal override Buffer128 GetResponseKeyLMv2() => this.GetResponseKeyNTv2();
		internal override Buffer128 GetResponseKeyNTv2() => Ntlm.NtowfV2(
			this.Password,
			this.UserName,
			this.Domain);

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
