# Sam
  Commands for interacting with a remote Security Accounts Manager

## Synopsis
```
Sam <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[aliasmembers](#sam-aliasmembers)|Gets the members of an alias|
|[enumaliases](#sam-enumaliases)|Enumerates aliases|
|[enumgroups](#sam-enumgroups)|Enumerates groups|
|[enumusers](#sam-enumusers)|Enumerates user accounts|


  For help on a subcommand, use `Sam <subcommand> -h`
# Sam aliasmembers
  Gets the members of an alias

## Synopsis
```
Sam aliasmembers [options] <ServerName> [ <AliasRidOrName> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|    -AliasRidOrName||&lt;String[]&gt;|Name or RID of alias|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AliasRidOrName||&lt;String[]&gt;|Name or RID of alias|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
||||  TreeTable|
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  DomainName|
||||  DomainSid|
||||  GroupName|
||||  GroupRid|
||||  MemberSid|
||||  MemberName|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DelegateTicket||&lt;String[]&gt;|Sends the tickets (and keys) to the target for delegation|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -Keytab||&lt;String&gt;|Name of keytab file|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|    -Tickets|-Ticket|&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -DebugLog|-vvv|&lt;SwitchParam&gt;|Prints debug messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|


## Details

  You may specify an alias either as a name, decimal RID, or hex RID prefixed
  with 0x.  You may specify multiple aliases.
  

## Examples

### Example 1 - Look up administrators

```
LUMON-FS1 -UserName LUMON\milchick -Password Br3@kr00m! -EncryptRpc 544
```

### Example 2 - Look up multiple aliases

```
LUMON-FS1 -UserName LUMON\milchick -Password Br3@kr00m! -EncryptRpc Administrators, "Backup Operators"
```

### Example 3 - Look up bad alias

```
LUMON-FS1 -UserName LUMON\milchick -Password Br3@kr00m! -EncryptRpc Administrators, "Backup Operators"
```
# Sam enumaliases
  Enumerates aliases

## Synopsis
```
Sam enumaliases [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
||||  TreeTable|
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  AccountName|
||||  Domain|
||||  AccountType|
||||  Id|
||||  Sid|
||||  MemberCount|
||||  AdminComment|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DelegateTicket||&lt;String[]&gt;|Sends the tickets (and keys) to the target for delegation|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -Keytab||&lt;String&gt;|Name of keytab file|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|    -Tickets|-Ticket|&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -DebugLog|-vvv|&lt;SwitchParam&gt;|Prints debug messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|


## Details

  Sam enumaliases attempts to query the general info and attributes for the
  groups returned by the server.
  

## Examples

### Example 1 - Enumerate all aliases

```
Sam enumaliases LUMON-FS1 -UserName milchick -Password Br3@kr00m!
```
# Sam enumgroups
  Enumerates groups

## Synopsis
```
Sam enumgroups [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
||||  TreeTable|
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  AccountName|
||||  Domain|
||||  AccountType|
||||  Id|
||||  Sid|
||||  Attributes|
||||  MemberCount|
||||  AdminComment|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DelegateTicket||&lt;String[]&gt;|Sends the tickets (and keys) to the target for delegation|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -Keytab||&lt;String&gt;|Name of keytab file|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|    -Tickets|-Ticket|&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -DebugLog|-vvv|&lt;SwitchParam&gt;|Prints debug messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|


## Details

  Sam enumgroups attempts to query the general info for the groups returned by
  the server.
  

## Examples

### Example 1 - Enumerate all groups

```
Sam enumgroups LUMON-DC1 -UserName milchick -Password Br3@kr00m!
```
# Sam enumusers
  Enumerates user accounts

## Synopsis
```
Sam enumusers [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
||||  TreeTable|
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  AccountName|
||||  Domain|
||||  AccountType|
||||  Id|
||||  Sid|
||||  FullName|
||||  AdminComment|
||||  PasswordLastSet|
||||  LastLogon|
||||  BadPasswordCount|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DelegateTicket||&lt;String[]&gt;|Sends the tickets (and keys) to the target for delegation|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -Keytab||&lt;String&gt;|Name of keytab file|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|    -Tickets|-Ticket|&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -DebugLog|-vvv|&lt;SwitchParam&gt;|Prints debug messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|


## Details

  Sam enumusers attempts to query the general and account info for the users
  returned by the server.
  

## Examples

### Example 1 - Enumerate all accounts

```
Sam enumusers LUMON-DC1 -UserName milchick -Password Br3@kr00m!
```
