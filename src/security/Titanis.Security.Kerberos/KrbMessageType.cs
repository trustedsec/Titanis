using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	// [RFC 4120] § 5.10.  Application Tag Numbers
	enum KrbMessageType : byte
	{
		Asreq = 10,
		Asrep = 11,
		Tgsreq = 12,
		Tgsrep = 13,
		Apreq = 14,
		Aprep = 15,
		Priv = 21,
		Cred = 22,

		Error = 30,

		Tgtreq = 16,
		Tgtrep = 17,

		// [MS-SFU] § 2.2.1
		PaForUser = 17,
	}
}
