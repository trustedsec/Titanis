[View the User Guide on the documentation site](https://trustedsec.github.io/Titanis/UserGuide/)

# Introduction
Titanis is a library of protocol implementations and command line utilities, written in C#, for interacting with Windows environments.  It uses .NET 8 and is cross-platform (Windows and Linux).  Some of the protocols implemented:

* SMB2 ([MS-SMB2](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-SMB2/%5bMS-SMB2%5d.pdf))
	* SMB 2.x and 3.x up to 3.1.1
	* Message security features such as signing and encryption
	* Includes support for filesystem control codes ([MS-FSCC](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-FSCC/%5bMS-FSCC%5d.pdf))
	* DFS links ([MS-DFSC](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-DFSC/%5bMS-DFSC%5d.pdf))
* MSRPC ([MS-RPCE](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-RPCE/%5bMS-RPCE%5d.pdf))
	* Endpoint mapper
	* DCOM ([MS-DCOM](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-DCOM/%5bMS-DCOM%5d.pdf))
	* Directory Replication ([MS-DRSR](https://winprotocoldoc.z19.web.core.windows.net/MS-DRSR/%5bMS-DRSR%5d.pdf))
		* Tested with Windows Server 2008 and Windows Server 2025
		* Supports both MS-ZIP and Xpress compression
		* Supports replication of individual objects or entire partitions
	* EFS ([MS-EFSR](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-EFSR/%5bMS-EFSR%5d.pdf))
	* LSA ([MS-LSAD](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-LSAD/%5bMS-LSAD%5d.pdf) and [MS-LSAT](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-LSAT/%5bMS-LSAT%5d.pdf))
	* Remote Registry ([MS-RRP](https://winprotocoldoc.z19.web.core.windows.net/MS-RRP/%5bMS-RRP%5d.pdf))
		* Includes support for backup semantics
		* Includes SAM dump functionality
	* Security Accounts Manager ([MS-SAMR](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-SAMR/%5bMS-SAMR%5d.pdf))
	* Service Control Manager ([MS-SCMR](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-SCMR/%5bMS-SCMR%5d.pdf))
	* Server service ([MS-SRVS](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-SRVS/%5bMS-SRVS%5d.pdf))
	* WMI ([MS-WMI](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-WMI/%5bMS-WMI%5d.pdf) and [MS-WMIO](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-WMIO/%5bMS-WMIO%5d.pdf))
	* Directory replication ([MS-DRSR]https://winprotocoldoc.z19.web.core.windows.net/MS-DRSR/%5bMS-DRSR%5d.pdf))
* LDAP ([RFC 4511](https://datatracker.ietf.org/doc/html/rfc4511) and parts of [MS-ADTS](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-ADTS/%5bMS-ADTS%5d.pdf))
* Security
	* NTLM ([MS-NLMP](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-NLMP/%5bMS-NLMP%5d.pdf))
	* Kerberos ([RFC4120](https://datatracker.ietf.org/doc/html/rfc4120) and [MS-KILE](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-KILE/%5bMS-KILE%5d.pdf))
		* Inter-realm referrals
		* PKINIT ([MS-PKCA](https://winprotocoldoc.z19.web.core.windows.net/MS-PKCA/%5bMS-PKCA%5d.pdf) and [RFC 4556](https://datatracker.ietf.org/doc/html/rfc4556))
		* S4U ([MS-SFU](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-WMI/%5bMS-WMI%5d.pdf))
			* S4U2self
			* S4U2proxy
		* Encryption profiles
			* DES CBC MD5 ([RFC3961](https://datatracker.ietf.org/doc/html/rfc3961)) (AS only)
			* RC4-HMAC ([RFC4757](https://datatracker.ietf.org/doc/html/rfc4757))
			* AES 128/256 ([RFC3962](https://datatracker.ietf.org/doc/html/rfc3962))
			* Support for .kirbi and [ccache](https://web.mit.edu/kerberos/krb5-1.21/doc/formats/ccache_file_format.html) files
			* Support for [keytab](https://web.mit.edu/kerberos/krb5-devel/doc/formats/keytab_file_format.html) files
		* Change / set password ([RFC3244](https://datatracker.ietf.org/doc/html/rfc3244))
	* SP-NEGO ([MS-SPNG](https://winprotocoldocs-bhdugrdyduf5h2e4.b02.azurefd.net/MS-SPNG/%5bMS-SPNG%5d.pdf))
* Integrated SOCKS 5 support (`-Socks5` parameter) ([RFC1928](https://datatracker.ietf.org/doc/html/rfc1928))


For recent changes, see the [change log](CHANGELOG.md)

For an overview on supported authentication scenarios, see [Authentication](docs/UserGuide/syntax-auth.md) - Describes how to control authentication with parameters


The toolset implements callbacks and logging features to integrate into your operational environment.

For a list of command line tools and tasks you can perform with them, check the [Tool Index](docs/UserGuide/tools/index.md)

# Target Audience
*  **Security researchers** - Research how Windows reacts to various types of requests
*  **Pentesters** - Perform actions and test whether mitigations are enabled and functioning properly
*  **System administrators** - Perform administrative tasks

# Getting Started

[Build Instructions](BUILD.md)

If you are a user, see the [User Guide](docs/UserGuide/index.md) for a list of command line utilities and how to use them.

If you are a developer, see the [Developer Guide](docs/DevGuide/index.md) for information on how to enhance the code base.

# Planned Enhancements
* Task Scheduler support ([MS-TSCH](https://winprotocoldoc.z19.web.core.windows.net/MS-TSCH/[MS-TSCH].pdf))
* Integrated SOCKS 4a

# Project Organization
*  **doc/** - Project documentation
*  **src/** - Source code for protocol implementations
*  **test/** - Unit tests
*  **tools/** - Standalone command line tools
*  **samples/** - Sample code that demonstrates how to use the libraries

Within Titanis.sln, the projects are grouped into the following functional areas:
*  **Base** - Utilities used by other components
*  **Crypto** - Implementation of cryptographic algorithms
*  **Formats** - Implementations for reading and writing various formats, such as ASN.1
*  **Protocols** - Network protocol implementations
*  **Security** - Security protocol implementations, such as NTLM and Kerberos
*  **Test** - Unit tests
*  **Tools** - Standalone command line tools
*  **_Build** - Files relevant to the build process.

Several of the projects contain a file named `References.txt` that references the specifications consulted during development.  The source code contains references to these specifications, usually along with the relevant section number.
