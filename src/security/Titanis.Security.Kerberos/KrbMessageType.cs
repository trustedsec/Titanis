using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	enum KrbMessageType
	{
		Asreq = 10,
		Asrep = 11,
		Tgsreq = 12,
		Tgsrep = 13,
		Apreq = 14,
		Aprep = 15,
		Cred = 22,
	}
}
