using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Ntlm
{
	public abstract class NtlmCredential
	{
		internal static readonly byte[] EmptyBytes = new byte[0];

		//internal static readonly byte[] EmptyLMResponse = new byte[1];

		public string UserName { get; }
		public string? Domain { get; }

		public static readonly NtlmCredential Anonymous = new NtlmPasswordCredential(string.Empty, string.Empty);

		protected NtlmCredential(
			string userName
			)
		{
			if (userName == null)
				throw new ArgumentNullException(nameof(userName));

			this.UserName = userName;
		}

		protected NtlmCredential(
			string userName,
			string? domain
			)
		{
			this.UserName = userName;
			this.Domain = domain;
		}

		public virtual bool IsAnonymous => false;

		public abstract bool CanProvideResponseKeyLM { get; }
		public abstract bool CanProvideResponseKeyNT { get; }

		internal abstract Buffer128 GetResponseKeyLMv1();
		internal abstract Buffer128 GetResponseKeyNTv1();

		internal abstract Buffer128 GetResponseKeyLMv2();
		internal abstract Buffer128 GetResponseKeyNTv2();

		internal NtlmAuthRecord ToAuthRecord()
			=> new NtlmAuthRecord(
				this.CanProvideResponseKeyLM ? this.GetResponseKeyLMv1() : new Buffer128(),
				this.CanProvideResponseKeyNT ? this.GetResponseKeyNTv1() : new Buffer128(),
				(this.CanProvideResponseKeyLM ? NtlmAuthRecordOptions.HasLmKey : 0)
				| (this.CanProvideResponseKeyNT ? NtlmAuthRecordOptions.HasNtKey : 0)
				);
	}
}
