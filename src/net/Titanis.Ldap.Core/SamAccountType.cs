using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	// [MS-SAMR] § 2.2.1.9 ACCOUNT_TYPE Values
	public enum SamAccountType
	{
		DomainObject = 0,
		Group = 0x10000000,
		NonSecurityGroup = 0x10000001,
		Alias = 0x20000000,
		NonSecurityAlias = 0x20000001,
		User = 0x30000000,
		Machine = 0x30000001,
		TrustAccount = 0x30000002,
		AppBasicGroup = 0x40000000,
		AppQueryGroup = 0x40000001,
	}
}
