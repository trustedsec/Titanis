using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	public enum KeyIntent
	{
		Checksum = 0x99,
		Encryption = 0xAA,
		Integrity = 0x55,
	}
}
