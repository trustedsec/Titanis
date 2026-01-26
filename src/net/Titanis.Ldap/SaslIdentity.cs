using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	// [RFC 4513] § 5.2.1.8.  SASL Authorization Identities
	public enum SaslIdentityKind
	{
		None = 0,
		Unknown,
		UserName,
		DistinguishedName
	}
	// [RFC 4513] § 5.2.1.8.  SASL Authorization Identities
	public struct SaslIdentity
	{
		public SaslIdentity(string? saslString)
		{
			this.SaslString = saslString;

			if (string.IsNullOrEmpty(saslString))
			{
				this.Kind = SaslIdentityKind.None;
			}
			else
			{
				int isep = saslString.IndexOf(':');
				if (isep <= 0)
				{
					this.Kind = SaslIdentityKind.Unknown;
				}
				else
				{
					string prefix = saslString.Substring(0, isep);
					this.PrincipalName = saslString.Substring(isep + 1);
					this.Kind = prefix switch
					{
						"u" => SaslIdentityKind.UserName,
						"dn" => SaslIdentityKind.DistinguishedName,
						_ => SaslIdentityKind.Unknown,
					};
				}
			}
		}

		public string? SaslString { get; }
		public string? PrincipalName { get; }
		public SaslIdentityKind Kind { get; }

		public override string ToString() => this.SaslString;
	}
}
