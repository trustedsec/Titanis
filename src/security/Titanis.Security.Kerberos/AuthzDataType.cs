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
	}
}
