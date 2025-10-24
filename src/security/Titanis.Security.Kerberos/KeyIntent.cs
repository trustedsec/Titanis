using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	public enum KeyIntent : byte
	{
		Checksum = 0x99,
		Encryption = 0xAA,
		Integrity = 0x55,
	}
}
