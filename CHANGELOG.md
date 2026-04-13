Change Log

# 2026-04-13

This releases focuses on some architectural and usability enhancements.

* `Dsrep`: support for [MS-DRSR] `dcinfo` and `replicate` secrets.
* `Ldap lspart` shorthand to list partitions within a forest.
* `Ldap` parses and displays dnsRecord data, enabling DNS enumeration.
* `Kerb keytablist` to list keys in a keytab file.
* `Dcom invoke` supports dotted-property syntax.
* `-OutputStyle TreeTable` output for Smb2Client ls and Ldap commands.
* `Smb2Client enumshares` revert to request lower levels if higher levels can't be retrieved.
* Logging and log schema enhancements.

## Bug fixes
* `-Ticket`, `-TicketCache` work without `-Kdc` and `-UserName`.
* Ldap converts timestamps both in queries and with standalone `timestamp` command
* FIxed NTLM over LDAPS to not request encryption.
* Dcom trims server name to just the host part, as sending activation request with FQDN fails.
* Scm falls back to older API if the newer version isn't supported.

# 2026-03-09

* Kerberos
	* PKINIT
	* Ticket decryption and authorization data support
		* Includes getting NTLM hash from PKINIT
	* Supports U2U
		* Support for MS_PRINCIPAL
* RPC
	* [MS-RRP]
* LDAP
	* SSL and channel binding
	* Support for named bits
	* Shorthand notation for BIT_OR, BIT_AND, and TRANSITIVE_EVAL
* Security
	* Object security models for common object types
	* Parse and generate SDDL strings
	* SPN overriding
	* Better support for well-known security principals
* Cryptography
	* Diffie-Hellman MODP key exchange
* Reg command
	* SAM dump
	* Get system key
	* Access registry with backup semantics
* Dcom standalone utility
	* Activate and invoke on a remote automation object
* Sddl command
	* Describe SDDL descriptor
* CLI
	* Support for dynamic output fields
	* Relative parameter ordering

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