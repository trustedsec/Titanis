Change Log

# Prerelease

* Dsrep
	* addsidhist
	* writengckey, readngckey
	* crackname
	* Topology and replicaiton:
		* sites
		* roles
		* domains
		* partitions
		* gcs
		* neighbors
		* repsto
		* cursors
		* objmetadata
		* attrmetadata
		* queue
* SSPI support (Windows only)
* NDR64 enabled by default across the toolset
	* Use `-OfferNdr64:no` to disable

## Minor Enhancements
* CSV supports multi-value output (semicolon-delimited)
* LDAP
	* If no -OutputFields, it requests 1.1 to prevent all attributes from being returned
	* Filter attributes optionally replaced with OIDs (for evasion)
* Enum argument parser is case-insensitive


# 2026-08-05
* Usability enhancements
	* Distribution includes .deb package
	* Completions for bash and zsh
	* How-To documentation
	* man pages, searchable with `man -k <keyword>`
* Registry
	* Enhanced querying, searching, and recursion
	* Export to .reg file
	* Delete key
* Wmi reg - Access the registry via Wmi

## Minor Enhancements Bugfixes
* Fixed problem with Wmi exec hanging while waiting for output
* Ldap supports syntaxes used by rootDSE attributes

# 2026-07-16
* Kerberos
	* FAST / armoring / compound identities (`Kerb asreq|tgsreq -ArmorTicket`)
	* Ticket forging (`Kerb forge`)
		* Specify values for PAC fields
* Directory Replication
	* Supports replication for either a single object or an entire domain
	* Supports compression (MS-ZIP and Xpress)
	* Better version negotiation
* SMB3
	* Supports compression

## Minor
* SMB/RPC are safe for multithreading
* Support for REG_EXPAND_SZ registry value type
* SAM LookupDomain

# 2026-06-09

## Enhancements

* (experimental) libfuse-based mounting for
	* Smb2Client
	* Ldap
	* Wmi
* Dsrep
	* performs additional DES decryption to unicodePwd and other secrets
	* Export all keys to a .keytab
* Kerberos
	* All tools accept `-Keytab` for Kerberos authentication
	* Delegation with `-Delegate` and `-DelegateTicket`
* Dcom
	* Standalone activation accepting filenames
* LDAP
	* -ExtendedDN
	* TTL-DNs for expiring group memberships (Privileged Access Management)
	* `rm` to delete entries
	* Accepts standard extensible match syntax
	* Relative date filter syntax (e.g. `(attr<=Today-5d)`)
	* `-AllowOnBehalfOf`

## Minor Enhancements and Fixes

* `-RpcCallTimeout` and `-RpcConnectTimeout` parameters to configure RPC timeouts
* Accepts forged tickets from Impacket with improper nametype encoding
* Smb2Client handles larger files (the chunking had issues)
* DACL serialization fixed
* LDAP handles DNs with a line break (e.g. conflict DNs)
* Concurrent RPC calls (for Dsrep -Parallelize)
* Smb2Client `watch` defaults to List style output.
* Dcom serialization fix (fixes FQDNs)
* Accept `-Param^ filename` syntax to import arguments from file contents

# 2026-04-13

This releases focuses on some architectural and usability enhancements.

* `Dsrep`: support for [MS-DRSR] `dcinfo` and `replicate` secrets.
* `Ldap lspart` shorthand to list partitions within a forest.
* `Ldap` parses and displays dnsRecord data, enabling DNS enumeration.
* `Kerb keytab list` to list keys in a keytab file.
* `Dcom invoke` supports dotted-property syntax.
* `-OutputStyle TreeTable` output for Smb2Client ls and Ldap commands.
* `Smb2Client enumshares` revert to request lower levels if higher levels can't be retrieved.
* Logging and log schema enhancements.

## Bug fixes
* `-Ticket`, `-TicketCache` work without `-Kdc` and `-UserName`.
* Ldap converts timestamps both in queries and with standalone `timestamp` command
* Fixed NTLM over LDAPS to not request encryption.
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