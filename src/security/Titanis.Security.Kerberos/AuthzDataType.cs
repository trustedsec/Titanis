using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	enum AuthzDataType
	{
		IfRelevant = 1,
		KdcIssued = 4,
		AndOr = 5,
		MandatoryForKdc = 8,

		// [RFC 6113] § 6.3
		AuthenticationStrength = 70,
		FxFastArmor = 71,
		FxFastUsed = 72,
	}
}
