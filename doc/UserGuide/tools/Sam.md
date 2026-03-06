# Sam
  Commands for interacting with a remote Security Accounts Manager

## Synopsis
```
Sam <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[enumusers](#sam-enumusers)|Enumerates user accounts|
|[enumgroups](#sam-enumgroups)|Enumerates groups|
|[enumaliases](#sam-enumaliases)|Enumerates aliases|


  For help on a subcommand, use `Sam <subcommand> -h`
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
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
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
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


## Details

  Sam enumaliases attempts to query the general info and attributes for the
  groups returned by the server.
  

## Examples

### Example 1 - Enumerate all aliases

```
Sam enumaliases LUMON-DC1 -UserName milchick -Password Br3@kr00m!
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
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
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
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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
|    -ContinueOnError||&lt;SwitchParam&gt;|Continue even if errors occur|
||||  Default: True|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
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
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat|-LogFormat|&lt;LogFormat&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||**Possible values:**|
||||  Text|
||||  TextWithTimestamp|
||||  Json|
|    -Verbose|-V|&lt;SwitchParam&gt;|Prints verbose messages|
|    -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|    -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Anonymous||&lt;SwitchParam&gt;|Uses anonymous login|
|    -UserName|-u|&lt;UserPrincipalName&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -DesKey||&lt;HexString&gt;|DES key|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
|    -S4UserName||&lt;UserPrincipalName&gt;|Name of user to impersonate with S4U|
|    -U2UserName||&lt;UserPrincipalName&gt;|User name to request TGT for U2U|
|    -S4UserCert||&lt;String&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    -S4ProxyService||&lt;SecurityPrincipalName&gt;|Name of service to proxy through|
|    -UserCert||&lt;String&gt;|Name of file containing user's certificate (for PKINIT)|
|    -UserKey||&lt;String&gt;|Name of file containing user's key (for PKINIT)|
|    -UserKeyPassword||&lt;String&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Workstation|-w|&lt;String&gt;|Name of workstation to send with NTLM authentication|
|    -NtlmVersion||&lt;Version&gt;|NTLM version number (a.b.c.d)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|
|    -Dialects||&lt;Smb2Dialect[]&gt;|List of SMB2 dialects to negotiate|
||||**Possible values:**|
||||  Smb2_0_2|
||||  Smb2_1|
||||  Smb3_0|
||||  Smb3_0_2|
||||  Smb3_1_1|
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


## Details

  Sam enumusers attempts to query the general and account info for the users
  returned by the server.
  

## Examples

### Example 1 - Enumerate all accounts

```
Sam enumusers LUMON-DC1 -UserName milchick -Password Br3@kr00m!
```
