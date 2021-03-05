using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	[Flags]
	public enum PasswordProperties : uint
	{
		None = 0,

		Complex = 1,
		NoAnonymousChange = 2,
		NoClearChange = 4,
		LockoutAdmins = 8,
		StoreCleartext = 0x10,
		RefusePasswordChange = 0x20,
	}

	public class SamDomainPasswordInfo
	{
		private readonly DOMAIN_PASSWORD_INFORMATION info;

		internal SamDomainPasswordInfo(DOMAIN_PASSWORD_INFORMATION info)
		{
			this.info = info;
		}

		public int MinPasswordLength => this.info.MinPasswordLength;
		public int PasswordHistoryLength => this.info.PasswordHistoryLength;
		public PasswordProperties PasswordProperties => (PasswordProperties)this.info.PasswordProperties;
		public TimeSpan MaxPasswordAge => TimeSpan.FromTicks(-this.info.MaxPasswordAge.AsInt64());
		public TimeSpan MinPasswordAge => TimeSpan.FromTicks(-this.info.MinPasswordAge.AsInt64());
	}
}