Change Log

# 2025-11-03

* Kerberos
	* S4U2self and S4U2proxy ([MS-SFU])
		* S4U with user certificate
	* Renew a ticket
	* Change password / Set password [RFC 3244]
	* Select ticket by sequence number
	* Invert selection with `Kerb select`
	* DES CBC MD5 [RFC 3961]
	* Generate protocol keys (`Kerb s2k`)
* WMI
	* Delete operation
* New output formats
	* TSV
	* CSV
	* JSON
* RPC
	* IPv6 support
* Other
	* Commands support `-h` and `--help` (for zsh users)
	* User name universally supports DOMAIN\user and user@DOMAIN syntaxes

## Bugfixes

* Canceling RPC operation on closed stream no longer throws exception (@moscowchill)
* C# language version set to 12.0 on netstandard2.0 and netstandard2.1 projects (fixed build issue) (@moscowchill)

# 2025-10-07

* Added [build instructions](BUILD.md) for Linux and Windows
* Integrated SOCKS 5 support
* Kerberos enhancements including supporting KRB5CCNAME and cross-realm tickets
* Smb2Client `touch` command
* Smb2Client timestomp functionality for `put`
* Architectural enhancements for security and RPC