All commands within Titanis that authenticate over the network accept a
uniform set of authentication parameters that supports a variety of
authentication scenarios.  When building an authentication context, Titanis
attempts to build a context for both NTLM and Kerberos.  If both contexts are
available, Titanis wraps them in an SP-Nego context.  If only one context is
available, Titanis does not wrap it in SP-Nego unless the application protocol
requires it (such as SMB2).

Commands have built-in support for Kerberos; you do not need to request tickets prior to running a command.

Other than the system clock, Titanis uses only the information in the command
line and certain environment variables for authentication; it does not use any 
information from your current session, such as the name of your user account
or the name of your workstation.  Use `-Workstation` to specify the name to
report as your workstation during authentication (NTLM and Kerberos).

# Quick Reference
## Notes
1. All commands accept tickets as either a .kirbi or .ccache file.  No conversion is necessary.
1. The `-TicketCache` parameter may be specified either explicitly on the command line or taken from the `KRB5CCNAME` environment variable.
	1. Titanis commands work best if you maintain a ticket cache per user and set `KRB5CCNAME` to this file.
	1. Tickets are requested as necessary and added to the ticket file and reused in subsequent commands.
	1. If a ticket file contains tickets for multiple users, use `-UserName` to filter which tickets are used.
	1. Titanis skips tickets that are outside their validity period.
1. Ticket files specified with `-Tgt` or `-Ticket` are never modified.
	1. If a file contains both a TGT and a service ticket, you may specify the same file for both parameters.
1. If you specify a ticket file (`-Tgt`, `-Ticket`, or `-TicketCache`) without specifying a user name or realm, Titanis searches for the first usable ticket.
	1. 
1. Ticket files specified with `-TicketCache` are updated to include any tickets requested during authentication.
	1. If the file doesn't exist, it is created.  If the extension is `.ccache` it is created as a ccache file; otherwise as a kirbi file.
	1. If it does exist, Titanis detects and retains the format of the file regardless of the extension.
	1. If the file is modified outside of the Titanis command (detected by last modified time) while the Titanis command is running, Titanis does NOT overwrite the file.
1. If a command requires multiple tickets, it will use the tickets in the specified ticket files and request missing tickets from the KDC (if specified).
1. Titanis automatically handles inter-realm referrals.
	1. It determines the KDC of the next realm by resolving the realm name using DNS; it does not query for the SRV record.
1. When a certificate is specified, the certificate may be in either `.pfx` or `.pem` format.  If the `.pem` file does not contain the key, there is a corresponding -XxKey argument (e.g. `-UserCert` and `-UserKey`).


| If you have... | then use... | Supports | Notes |
|-|-|-|-|
| Nothing | `-Anonymous` | NTLM | Use with `-vv` to get the domain and computer name of a server.
| User name with... | `-UserName` &lt;username&gt; ||The domain is inferred from the NTLM_CHALLENGE message.|
| &nbsp; ...password | `-Password` &lt;password&gt; |NTLM|Be sure to escape special characters as required by your shell.|
| &nbsp; ...NTLM hash | `-NtlmHash` &lt;hex-encoded hash&gt; |NTLM|Use just the NTLM hash; no colons|
|User name and domain with...| `-UserName` &lt;domain&gt;\\&lt;username&gt; ||You may need to escape the backslash depending on your shell.  The domain name may be either the NetBIOS name or FQDN. |
| | `-UserName` &lt;username&gt;@&lt;domain&gt; |||
| | `-UserName` &lt;username&gt; -UserDomain &lt;domain&gt; |||
| &nbsp; ...password | `-Password` &lt;password&gt; |NTLM, Kerberos|Be sure to escape special characters as required by your shell.|
| &nbsp; ...NTLM hash | `-NtlmHash` &lt;hex-encoded hash&gt; |NTLM, Kerberos (RC4 HMAC only)|Use just the NTLM hash; no colons.|
| &nbsp; ...AES 128 key | `-AesKey` &lt;hex-encoded hash&gt; |Kerberos (AES128)|AES 128 and AES 256 are distinguished by the size of the key.|
| &nbsp; ...AES 256 key | `-AesKey` &lt;hex-encoded hash&gt; |Kerberos (AES256)||
| &nbsp; ...keytab file | `-Keytab` &lt;keytab file&gt; |Kerberos|All keys with newest kvno used|
|Ticket-granting ticket and KDC| `-Tgt` &lt;TGT file name&gt; `-Kdc` &lt;endpoint&gt;|Kerberos|Titanis requests the necessary service tickets from the KDC.|
|| `-TicketCache` &lt;TGT file name&gt; `-Kdc` &lt;endpoint&gt;|Kerberos|Titanis requests the necessary service tickets from the KDC.|
|Service ticket| `-Ticket` &lt;ticket file name&gt;|Kerberos|The SPN of the ticket must match what the command requires.|
|| `-TicketCache` &lt;ticket file name&gt;|Kerberos|The SPN of the ticket must match what the command requires.|
|Certificate file as a ...| | | |
| &nbsp; ... .pfx with certificate and key | `-UserCert` &lt;.pfx file&gt; `-UserKeyPassword` &lt;passphrase&gt;| Kerberos | `-UserKeyPassword` is used to decrypt the file and is only required if the file is encrypted. |
| &nbsp; ... .pem with certificate and key | `-UserCert` &lt;.pem file&gt; `-UserKeyPassword` &lt;passphrase&gt;| Kerberos | `-UserKeyPassword` is used to decrypt the file and is only required if the file is encrypted. |
| &nbsp; ... .pem with certificate and separate .key | `-UserCert` &lt;.pem file&gt; `-UserKey` &lt;.key file&gt; `-UserKeyPassword` &lt;passphrase&gt;| Kerberos | `-UserKeyPassword` is used to decrypt the file and is only required if the file is encrypted. |


The Authentication parameter group defines parameters that specify how the tool is to authenticate with the target.  Most protocols exchange security tokens to build a security context to authenticate the user and provide message security services, such as signing and sealing.  The parameters specify how to build this security context.  Titanis supports the following security protocols:

* NTLM
* Kerberos
* SPNEGO

Some protocols require a specific security protocol.  For example, SMB2 requires SPNEGO, which itself may enclose both NTLM and Kerberos.  Some protocols, such as RPC, will accept a number of security protocols along with a field in the header that specifies how to interpret the tokens.  Titanis will use the provided parameters to build the appropriate type of security context.  If the parameters support multiple security protocols and the application protocol supports SPNEGO, Titanis prepares a security context for each supported security protocol and wraps them in an SPNEGO context.

In general, to use Kerberos, you must specify the KDC address with `-Kdc`.  If no ticket is in the cache, Titanis will attempt to contact the KDC to request a ticket.


## Other Parameters

|Parameter|Description|
|-|-|
|`-Workstation <name>`|Name of the workstation to send along with the authentication request.  Windows uses this to evaluate logon restrictions and usually includes it in the event log record for the authentication request.|
|`-NtlmVersion <m.n.b.r>`|Version number to send in NTLM|

# Ticket Files

Titanis handles both `.kirbi` and `.ccache` files.  When loading the file, Titanis determines the format based on the contents.  When creating a file, Titanis checks for the `.ccache` extension, and if present, writes the file as a `.ccache` file; otherwise, as a `.kirbi` file.  When overwriting or appending to a file, Titanis retains the format of the original file, regardless of the extension.

In general, Titanis works best when a ticket file contains tickets from a single identity.  If a ticket file contains tickets for multiple users, use `-UserName` and/or `-UserDomain` to specify the user name and/or domain.  Titanis uses these parameters to filter the list of tickets, and only uses tickets matching these values.

## `-Tgt`, `-Ticket`, and `-TicketCache`

Titanis accepts ticket files through multiple parameters.

* `-Tgt` specifies the file containing the TGT to use if Titanis needs to request a service ticket.  If the file contains other tickets besides a TGT, they are ignored without error.
* `-Ticket` specifies a file containing the service tickets to use.
* `-TicketCache` specifies a file to check for all tickets (TGT and service tickets).  In addition, any new tickets Titanis requests are appended to this file.

Titanis supports `$KRB5CCNAME` as a default for `-TicketCache` for interoperability with other tools.

# SPN Overrides

When authenticating to a network service, Titanis constructs the service principal name using the service class of the protocol and the server name.  For example, when authenticating to SMB on LUMON-DC1, Titanis constructs the SPN `cifs/LUMON-DC1`.

If the server name is specified as an IP address, Titanis uses the IP address to construct the SPN, which generally fails, unless an administrator has specifically configured the SPN mappings to include the IP addresses.  Instead, specify the service name as the host name, and use the `-HostAddress` parameter to specify the network address to connect to.  This may either be an IP address, or a host name that can be resolved by the local DNS resolver (or SOCKS proxy, if using with `-Socks5`).

Sometimes, you may want to use a ticket with a mismatched SPN.  To allow this, Titanis provides the `-SpnOverride` parameter.  This parameter accepts a list of arguments of the form `class/host=override-class/override-host` or `class/host~=override-class/override-host`.  After Titanis constructs the SPN, but before selecting a ticket, it checks the list of SPN overrides.  If a match is found, Titanis replaces the constructed SPN with the override values.  

For example, let's say you have a ticket for `host/LUMON-DC1` that you want to use to authenticate to SMB:

```
Smb2Client ls //LUMON-DC1/C$ -Ticket dc1-host.ccache -SpnOverride cifs/LUMON-DC1=host/LUMON-DC1
```

You may specify a wildcard `*` for either the service class, the host, or both.  For example, to override the SPN for all tickets for LUMON-DC1:

```
Smb2Client ls //LUMON-DC1/C$ -Ticket dc1-host.ccache -SpnOverride */LUMON-DC1=host/LUMON-DC1
```

When using the syntax with `=`, Titanis uses the override values for subsequent TGS-REQ and AP-REQ messages.

When using `~=`, Titanis only uses the override SPN for the TGS-REQ or to search the ticket cache, but still uses the constructed SPN for the AP-REQ.

```
Smb2Client ls //LUMON-DC1/C$ -Ticket dc1-host.ccache -SpnOverride */LUMON-DC1~=host/LUMON-DC1
```

In this example, Titanis would search for (or request) a ticket with the SPN `host/LUMON-DC`, but send the ticket to SMB as `cifs/LUMON-DC1`.

# Inter-realm Referrals

When authenticating to a resource that is in a domain other than your user domain, you must authenticate with a ticket issued by a KDC in the resource's domain.

To request an inter-realm ticket, specify the server name as the FQDN.  Titanis constructs the SPN with the FQDN as the host name and sends a TGS-REQ to the KDC.  The KDC replies with TGT to another domain (either the resource domain itself, or a domain that is closer to it).  Titanis interprets this as an **inter-realm referral* and requests a ticket from the referred domain.

In a Windows environment, ensure `-Kdc` specifies a Global Catalog server.  Only Global Catalog servers have knowledge of accounts in other domains.  Requesting an inter-realm ticket from a KDC that isn't a GC will likely result in `KDC_ERR_S_PRINCIPAL_UNKNOWN`.

The referral contains the FQDN of the next domain.  Titanis resolves the FQDN of the next domain and uses it as the KDC when requesting the next ticket.  In Windows DNS, the FQDN of the domain contains host records that resolve to the DCs.  Titanis does not query for SRV records as Windows does.

In complex environments, you may have to traverse multiple domains to arrive at the resource domain.  Titanis does this automatically.

Specify `-vv` on the command line to instruct the command to print referral information.

# PK-INIT

To authenticate with PK-INIT, specify a certificate file (and a key file, if separate).  Titanis supports PEM files, PKCS 12 (.pfx) and .cer files.

If the certificate and key are contained in the same file (.pfx or .pem):

```
Smb2Client ls //LUMON-DC1/C$ -UserCert milchick.pfx -UserKeyPassword password -Kdc LUMON-DC1
```

If the certificate and key are contained in separate files:

```
Smb2Client ls //LUMON-DC1/C$ -UserCert milchick.pem -UserKey milchick.key -UserKeyPassword password -Kdc LUMON-DC1
```

If the certificate contains a different UPN than the one specified on the command line, Titanis issues a warning, and uses the user name provided on the command line.

# Service for User (S4U)

Titanis commands integrate support for Service-for-user-to-self (S4U2self) and
Service-for-user-to-proxy (S4U2proxy) scenarios.  S4U allows you to use a
service account to obtain a service ticket for and impersonate a user account
when you don't have credentials for that user account.

| Parameter | Description | Note |
|-|-|-|
| `-S4UserName` &lt;user name&gt; | Name of user to impersonate | If the user name does not include the domain, then the domain of the service is assumed. |
| `-S4UserCert` &lt;certificate file name&gt; | X.509 certificate identifying user to impersonate |
| `-S4ProxyService` &lt;service account&gt; | Name of service account to proxy through | May be specified as &lt;class&gt;/&lt;host&gt; or &lt;account name&gt;

## Notes
1. Only one of `-S4UserName` or `-S4UserCert` are required for S4U.
	1. If both are specified, they must agree.
1. The presence of `-S4ProxyService` indicates S4U2proxy.
	1. Otherwise, S4U2self is used.
1. S4U may be combined with any Kerberos authentication scenario above.
	1. The provided credentials must be for the service account, not the user to impersonate.
	1. At each step in the sequence, Titanis checks the ticket files provided by `-Tgt`, `-Ticket`, and `-TicketCache` for the desired ticket.
	1. If the desired ticket is found, it is used, and the KDC is not contacted for that step.

To test S4U (or any authentication scenario), consider using `Lsa whoami`.  This
command prints name of the user that authenticates to it.  It accepts the usual
authentication parameters.

## S4U2self Sequence

The full S4U2self sequence is as follows:
1. Request TGT for `-UserName` using credential specified by `-Password`, `-NtlmHash`, or `-AesKey`.
1. Use TGT to request a service ticket to the service required by the command.
1. Use S4U2self to request a ticket for the user specified by `-S4User*` to the service required by the command.

## S4U2proxy Sequence

The full S4U2proxy sequence is as follows:
1. Request TGT for `-UserName` using credential specified by `-Password`, `-NtlmHash`, or `-AesKey`.
1. Use TGT to request a service ticket to the service specified by `-S4ProxyService`.
1. Use S4U2self  to request a ticket for the user specified by `-S4User*` to the service specified by `-S4ProxyService`.
1. Use S4U2proxy to request a ticket for the user specified by `-S4User*` to the service required by the command.

## Example

Let's say you have a TGT for COBEL-WKS$ and wish to impersonate user `milchick`.

```
Smb2Client ls \\LUMON-FS1\C$ -UserName allentown@LUMON -Password password -Kdc 10.66.0.11 -S4UserName milchick -S4ProxyService host/allentown
```

This command:
1. Requests a TGT for user `allentown` (or retrieves one from the cache)
1. Requests a service ticket for user `milchick` to the proxy service `host/allentown` using S4U2self.
1. Using the S4U2self ticket from above, requests a service ticket for user `milchick` to `cifs/LUMON-FS1`.

## Notes
1. You may specify one or both of the `-S4User*` parameters.  If you specify both `-S4UserName` and `-S4UserCert`, they must match; otherwise, the KDC will likely fail the request.
1. During testing, when specifying `-S4UserCert`, the DC takes considerably longer to respond (around 10 seconds).
1. The name supplied to `-S4ProxyService` may be of the form `<class>/<instance>` or simply the service account name.
