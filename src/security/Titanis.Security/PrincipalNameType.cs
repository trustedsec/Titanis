using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security
{
	// [RFC 4120] 6.2 § Principal Names
	public enum PrincipalNameType : int	// Underlying type used by CCache serializer
	{
		Unknown = 0,
		Principal = 1,
		ServiceInstance = 2,
		ServiceHost = 3,
		ServiceXHost = 4,
		UniqueId = 5,
		X500Principal = 6,
		SmtpName = 7,
		Enterprise = 10,
	}
}
