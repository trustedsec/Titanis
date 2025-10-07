# Lsa
  Commands for interacting with the LSA

## Synopsis
```
Lsa <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[lookupsid](#lsa-lookupsid)|Translates one or more SIDs to their account names|
|[lookupname](#lsa-lookupname)|Gets the SID for one or more account names|
|[whoami](#lsa-whoami)|Gets the name and domain of the connected user|
|[enumaccounts](#lsa-enumaccounts)|Enumerates accounts|
|[enumprivaccounts](#lsa-enumprivaccounts)|Enumerates accounts that have a specific privilege or user right|
|[createaccount](#lsa-createaccount)|Creates an account|
|[getprivs](#lsa-getprivs)|Gets the privileges assigned to an account.|
|[getrights](#lsa-getrights)|Gets the user rights and privileges granted to an account|
|[getsysaccess](#lsa-getsysaccess)|Gets the system access rights granted to an account|
|[setsysaccess](#lsa-setsysaccess)|Sets the system access rights granted to an account|
|[addpriv](#lsa-addpriv)|Adds one or more privileges to an account|
|[rmpriv](#lsa-rmpriv)|Removes one or more privileges from an account|


  For help on a subcommand, use `Lsa &lt;subcommand&gt; -?`
# Lsa addpriv
  Adds one or more privileges to an account

## Synopsis
```
Lsa addpriv [options] <ServerName> <Privileges>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;Privileges&gt;||&lt;String[]&gt;|Names or values of the privileges to add|
||||**Possible values:**|
||||  SeCreateTokenPrivilege|
||||  SeAssignPrimaryTokenPrivilege|
||||  SeLockMemoryPrivilege|
||||  SeIncreaseQuotaPrivilege|
||||  SeMachineAccountPrivilege|
||||  SeTcbPrivilege|
||||  SeSecurityPrivilege|
||||  SeTakeOwnershipPrivilege|
||||  SeLoadDriverPrivilege|
||||  SeSystemProfilePrivilege|
||||  SeSystemtimePrivilege|
||||  SeProfileSingleProcessPrivilege|
||||  SeIncreaseBasePriorityPrivilege|
||||  SeCreatePagefilePrivilege|
||||  SeCreatePermanentPrivilege|
||||  SeBackupPrivilege|
||||  SeRestorePrivilege|
||||  SeShutdownPrivilege|
||||  SeDebugPrivilege|
||||  SeAuditPrivilege|
||||  SeSystemEnvironment|
||||  SeChangeNotifyPrivilege|
||||  SeRemoteShutdownPrivilege|
||||  SeUndockPrivilege|
||||  SeSyncAgentPrivilege|
||||  SeEnableDelegationPrivilege|
||||  SeManageVolumePrivilege|
||||  SeImpersonatePrivilege|
||||  SeCreateGlobalPrivilege|
||||  SeTrustedCredManAccessPrivilege|
||||  SeRelabelPrivilege|
||||  SeIncreaseWorkingSetPrivilege|
||||  SeTimeZonePrivilege|
||||  SeCreateSymbolicLinkPrivilege|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  Each privilege may be the symbolic name or the value, expressed as a 64-bit
  integer.  If the name is not a predefined privilege, Lsa addpriv resolves the
  name with the remote LSA.
  
  This command cannot be used to grant a user right.
  
  Note that the LSA tracks accounts separate from the SAM.  Even for local
  accounts, you make need to create the LSA account first.
  

## Examples

### Example 1 - Add SeLoadDriverPrivilege and SeTcbPrivilege to Administrators

```
Lsa addpriv LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeLoadDriver SeTcb
```

### Example 2 - Add SeLoadDriverPrivilege and SeTcbPrivilege to S-1-5-32-646

```
Lsa addpriv LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 SeLoadDriver SeTcb
```
# Lsa createaccount
  Creates an account

## Synopsis
```
Lsa createaccount [options] <ServerName> <Sid>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;Sid&gt;||&lt;SecurityIdentifier&gt;|SID of account to create|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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


## Examples

### Example 1 - Create an account

```
Lsa createaccount LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-32-646
```
# Lsa enumaccounts
  Enumerates accounts

## Synopsis
```
Lsa enumaccounts [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Sid|
||||  AccountName|
||||  DomainName|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  By default, the output only includes the SIDs of the accounts.  Use
  -OutputFields if you want additional information such as the account or domain
  name.  The additional fields require another RPC call to the server.
  

## Examples

### Example 1 - Get account SIDs

```
Lsa enumaccounts LUMON-FS1 -UserName milchick -Password Br3@kr00m!
```

### Example 2 - Get account SIDs with account name and domain

```
Lsa enumaccounts LUMON-FS1 -UserName milchick -Password Br3@kr00m! -OutputFields Sid, AccountName, DomainName
```
# Lsa enumprivaccounts
  Enumerates accounts that have a specific privilege or user right

## Synopsis
```
Lsa enumprivaccounts [options] -Privilege <String> <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Privilege||&lt;String&gt;|Name of privilege or user right to check for|
||||**Possible values:**|
||||  SeCreateTokenPrivilege|
||||  SeAssignPrimaryTokenPrivilege|
||||  SeLockMemoryPrivilege|
||||  SeIncreaseQuotaPrivilege|
||||  SeMachineAccountPrivilege|
||||  SeTcbPrivilege|
||||  SeSecurityPrivilege|
||||  SeTakeOwnershipPrivilege|
||||  SeLoadDriverPrivilege|
||||  SeSystemProfilePrivilege|
||||  SeSystemtimePrivilege|
||||  SeProfileSingleProcessPrivilege|
||||  SeIncreaseBasePriorityPrivilege|
||||  SeCreatePagefilePrivilege|
||||  SeCreatePermanentPrivilege|
||||  SeBackupPrivilege|
||||  SeRestorePrivilege|
||||  SeShutdownPrivilege|
||||  SeDebugPrivilege|
||||  SeAuditPrivilege|
||||  SeSystemEnvironment|
||||  SeChangeNotifyPrivilege|
||||  SeRemoteShutdownPrivilege|
||||  SeUndockPrivilege|
||||  SeSyncAgentPrivilege|
||||  SeEnableDelegationPrivilege|
||||  SeManageVolumePrivilege|
||||  SeImpersonatePrivilege|
||||  SeCreateGlobalPrivilege|
||||  SeTrustedCredManAccessPrivilege|
||||  SeRelabelPrivilege|
||||  SeIncreaseWorkingSetPrivilege|
||||  SeTimeZonePrivilege|
||||  SeCreateSymbolicLinkPrivilege|
||||  None|
||||  SeInteractiveLogonRight|
||||  SeNetworkLogonRight|
||||  SeBatchLogonRight|
||||  SeServiceLogonRight|
||||  SeDenyInteractiveLogonRight|
||||  SeDenyNetworkLogonRight|
||||  SeDenyBatchLogonRight|
||||  SeDenyServiceLogonRight|
||||  SeRemoteInteractiveLogonRight|
||||  SeDenyRemoteInteractiveLogonRight|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Sid|
||||  AccountName|
||||  DomainName|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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


## Examples

### Example 1 - Enumerate accounts with SeInteractiveLogonRight

```
Lsa enumprivaccounts LUMON-FS1 -UserName milchick -Password Br3@kr00m! -Privilege SeInteractiveLogonRight
```
# Lsa getprivs
  Gets the privileges assigned to an account.

## Synopsis
```
Lsa getprivs [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Privilege|
||||  Attributes|
||||  PrivilegeName|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  One of -BySid or -ByName is required to specify the account to get the
  privileges for.  The account may be a user or group.
  
  By default, only the names of predefined privileges are resolved.  Te get the
  names of all privileges, use -OutputFields to specify PrivilegeName
  

## Examples

### Example 1 - Get privileges for account by SID

```
Lsa getprivs LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544
```

### Example 2 - Get privileges for account by name

```
Lsa getprivs LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrator
```

### Example 3 - Get privileges for account by name, look up privilege names

```
Lsa getprivs LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrator -OutputFields Privilege, PrivilegeName
```
# Lsa getrights
  Gets the user rights and privileges granted to an account

## Synopsis
```
Lsa getrights [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Name|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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


## Examples

### Example 1 - Get privileges and rights by SID

```
Lsa getrights LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544
```

### Example 2 - Get privileges and rights by name

```
Lsa getrights LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators
```
# Lsa getsysaccess
  Gets the system access rights granted to an account

## Synopsis
```
Lsa getsysaccess [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Value|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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


## Examples

### Example 1 - Get system access rights by SID

```
Lsa getsysaccess LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544
```

### Example 2 - Get system access rights by account name

```
Lsa getsysaccess LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators
```
# Lsa lookupname
  Gets the SID for one or more account names

## Synopsis
```
Lsa lookupname [options] <ServerName> <AccountNames>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;AccountNames&gt;||&lt;String[]&gt;|Names of accounts to look up|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  AccountName|
||||  NameType|
||||  DomainName|
||||  DomainSid|
||||  AccountRid|
||||  AccountSid|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  The command accepts multiple user names
  

## Examples

### Example 1 - Look up multiple names

```
Lsa lookupname LUMON-FS1 -UserName milchick -Password Br3@kr00m! marks milchick
```
# Lsa lookupsid
  Translates one or more SIDs to their account names

## Synopsis
```
Lsa lookupsid [options] <ServerName> <Sids>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;Sids&gt;||&lt;SecurityIdentifier[]&gt;|SIDs to look up|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  AccountName|
||||  NameType|
||||  DomainName|
||||  DomainSid|
||||  AccountRid|
||||  AccountSid|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  The command accepts multiple SIDs
  

## Examples

### Example 1 - Look up multiple names

```
Lsa lookupsid LUMON-FS1 -UserName milchick -Password Br3@kr00m! S-1-5-21-1752138614-393460150-3098146133-1103 S-1-5-21-1752138614-393460150-3098146133-1107
```
# Lsa rmpriv
  Removes one or more privileges from an account

## Synopsis
```
Lsa rmpriv [options] <ServerName> <Privileges>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;Privileges&gt;||&lt;String[]&gt;|Names or values of the privileges to add|
||||**Possible values:**|
||||  SeCreateTokenPrivilege|
||||  SeAssignPrimaryTokenPrivilege|
||||  SeLockMemoryPrivilege|
||||  SeIncreaseQuotaPrivilege|
||||  SeMachineAccountPrivilege|
||||  SeTcbPrivilege|
||||  SeSecurityPrivilege|
||||  SeTakeOwnershipPrivilege|
||||  SeLoadDriverPrivilege|
||||  SeSystemProfilePrivilege|
||||  SeSystemtimePrivilege|
||||  SeProfileSingleProcessPrivilege|
||||  SeIncreaseBasePriorityPrivilege|
||||  SeCreatePagefilePrivilege|
||||  SeCreatePermanentPrivilege|
||||  SeBackupPrivilege|
||||  SeRestorePrivilege|
||||  SeShutdownPrivilege|
||||  SeDebugPrivilege|
||||  SeAuditPrivilege|
||||  SeSystemEnvironment|
||||  SeChangeNotifyPrivilege|
||||  SeRemoteShutdownPrivilege|
||||  SeUndockPrivilege|
||||  SeSyncAgentPrivilege|
||||  SeEnableDelegationPrivilege|
||||  SeManageVolumePrivilege|
||||  SeImpersonatePrivilege|
||||  SeCreateGlobalPrivilege|
||||  SeTrustedCredManAccessPrivilege|
||||  SeRelabelPrivilege|
||||  SeIncreaseWorkingSetPrivilege|
||||  SeTimeZonePrivilege|
||||  SeCreateSymbolicLinkPrivilege|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  Each privilege may be the symbolic name or the value, expressed as a 64-bit
  integer.  If the name is not a predefined privilege, Lsa rmpriv resolves the
  name with the remote LSA.  For predefined privilege names (those in the help
  text), you are not required to append `Privilege` to the name.
  
  To remove all privileges, use `*`.  Note that you may have to escape this
  depending on which shell you are using.
  
  This command cannot be used to remove a user right.
  
  Note that the LSA tracks accounts separate from the SAM.  Even for local
  accounts, you make need to create the LSA account first.
  

## Examples

### Example 1 - Remove SeTcbPrivilege from Administrators

```
Lsa rmpriv LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeTcb
```

### Example 2 - Remove SeTcbPrivilege from S-1-5-32-646

```
Lsa rmpriv LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 SeTcb
```

### Example 3 - Remove all privileges from S-1-5-32-646

```
Lsa rmpriv LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-646 *
```
# Lsa setsysaccess
  Sets the system access rights granted to an account

## Synopsis
```
Lsa setsysaccess [options] <ServerName> <Rights>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;Rights&gt;||&lt;SystemAccessRights[]&gt;|Access rights to grant|
||||**Possible values:**|
||||  None|
||||  SeInteractiveLogonRight|
||||  SeNetworkLogonRight|
||||  SeBatchLogonRight|
||||  SeServiceLogonRight|
||||  SeDenyInteractiveLogonRight|
||||  SeDenyNetworkLogonRight|
||||  SeDenyBatchLogonRight|
||||  SeDenyServiceLogonRight|
||||  SeRemoteInteractiveLogonRight|
||||  SeDenyRemoteInteractiveLogonRight|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Reset||&lt;SwitchParam&gt;|Clears any rights already set on the account|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Value|
|    -BySid||&lt;SecurityIdentifier&gt;|SID of account|
|    -ByName||&lt;String&gt;|Account name|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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

  By default, the specified access rights are added to the rights already granted
  to the account.  Use -Reset to clear existing access rights and only grant the
  rights specified.  To reset all access rights currently granted, use -Reset and
  specify a single right of 0
  

## Examples

### Example 1 - Set system access rights by SID

```
Lsa setsysaccess LUMON-FS1 -UserName milchick -Password Br3@kr00m! -BySid S-1-5-32-544 SeBatchLogonRight
```

### Example 2 - Set system access rights by account name

```
Lsa setsysaccess LUMON-FS1 -UserName milchick -Password Br3@kr00m! -ByName Administrators SeBatchLogonRight
```
# Lsa whoami
  Gets the name and domain of the connected user

## Synopsis
```
Lsa whoami [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Text|
||||  UserName|
||||  Realm|
||||  NameType|
||||  NamePartCount|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-L, -LogLevel||&lt;LogMessageSeverity&gt;|Sets the lowest level of messages to log|
||||**Possible values:**|
||||  Debug|
||||  Diagnostic|
||||  Verbose|
||||  Info|
||||  Warning|
||||  Error|
||||  Critical|
|    -ConsoleLogFormat||&lt;LogFormat&gt;|Sets the format of log messages written to the console|
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
|    -UserName|-u|&lt;String&gt;|User name to authenticate with, not including the domain|
|    -UserDomain|-ud|&lt;String&gt;|Domain of user to authenticate with|
|    -Password|-p|&lt;String&gt;|Password to authenticate with|
|    -NtlmHash||&lt;hexadecimal hash&gt;|NTLM hash for NTLM authentication|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -AesKey||&lt;HexString&gt;|AES key (128 or 256)|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -Tickets||&lt;String[]&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    -TicketCache||&lt;String&gt;|Name of ticket cache file|
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|


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


## Examples

### Example 1 - Get connected user name

```
Lsa whoami LUMON-FS1 -UserName milchick -Password Br3@kr00m!
```
