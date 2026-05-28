# Scm
  Provides functionality for interacting with the service control manager on a
  remote Windows system

## Synopsis
```
Scm <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[create](#scm-create)|Creates and optionally starts a new service|
|[delete](#scm-delete)|Deletes a service|
|[qtriggers](#scm-qtriggers)|Queries the status of a service|
|[query](#scm-query)|Queries the status of a service|
|[start](#scm-start)|Starts a service|
|[stop](#scm-stop)|Stops a service|


  For help on a subcommand, use `Scm <subcommand> -h`
# Scm create
  Creates and optionally starts a new service

## Synopsis
```
Scm create [options] <ServerName> <ServiceName> [ <BinPath> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;ServiceName&gt;||&lt;String&gt;|Name of service to create|
|&lt;BinPath&gt;||&lt;String&gt;|Service command line|


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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -Dependencies|-deps|&lt;String[]&gt;|List of services this service depends on|
|    -DisplayName||&lt;String&gt;|Service display name|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ErrorControl||&lt;ServiceErrorControl&gt;|Error control|
||||  Default: 1|
||||**Possible values:**|
||||  Ignore|
||||  Normal|
||||  Severe|
||||  Critical|
|    -LoadOrderGroup||&lt;String&gt;|Load order group|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -ServiceType||&lt;ServiceTypes&gt;|Type of service|
||||  Default: 16|
||||**Possible values:**|
||||  None|
||||  KernelDriver|
||||  FileSystemDriver|
||||  OwnProcess|
||||  SharedProcess|
||||  All|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -Start||&lt;SwitchParam&gt;|Start the service once created|
|    -StartName||&lt;String&gt;|Name of user account to run service as|
||||  Default: LocalSystem|
|    -StartPassword||&lt;String&gt;|Password of service account|
|    -StartType||&lt;ServiceStartType&gt;|Service start type|
||||  Default: 3|
||||**Possible values:**|
||||  Boot|
||||  System|
||||  Auto|
||||  Demand|
||||  Disabled|
|    -Tag||&lt;Int32&gt;|Unique tag within the load order group|
||||  Default: 0|


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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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


## Examples

### Example 1 - Create and start a service

```
Scm create LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice -DisplayName "My Service" C:\windows\system32\cmd.exe -Start
```
# Scm delete
  Deletes a service

## Synopsis
```
Scm delete [options] <ServerName> <ServiceName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;ServiceName&gt;||&lt;String&gt;|Name of the service|


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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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


## Examples

### Example 1 - Delete a service

```
Scm delete LUMON-DC1 -UserName milchick -Password Br3@kr00m! myservice
```
# Scm qtriggers
  Queries the status of a service

## Synopsis
```
Scm qtriggers [options] <ServerName> <ServiceName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;ServiceName&gt;||&lt;String[]&gt;|Names of services to query (* for all)|


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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  ServiceName|
||||  TriggerType|
||||  TriggerTypeDescription|
||||  Action|
||||  Data0|
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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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

# Scm query
  Queries the status of a service

## Synopsis
```
Scm query [options] <ServerName>
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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  ServiceName|
||||  DisplayName|
||||  ServiceType|
||||  State|
||||  Win32ExitCode|
||||  SpecificExitCode|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -RpcCallTimeout||&lt;Duration&gt;|Time to wait for RPC calls|
|    -RpcConnectTimeout||&lt;Duration&gt;|Time to wait for RPC connections|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -States||&lt;ServiceStates[]&gt;|Filter by service state|
||||**Possible values:**|
||||  None|
||||  Active|
||||  Inactive|
||||  All|
|    -Types||&lt;ServiceTypes[]&gt;|Filter by service type|
||||**Possible values:**|
||||  None|
||||  KernelDriver|
||||  FileSystemDriver|
||||  OwnProcess|
||||  SharedProcess|
||||  All|


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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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


## Examples

### Example 1 - Query all services (NTLM)

```
Scm query lumon-fs1 -UserName milchick@LUMON -Password Br3@kr00m!
```

### Example 2 - Query all services (Kerberos)

```
Scm query lumon-fs1 -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1
```
# Scm start
  Starts a service

## Synopsis
```
Scm start [options] <ServerName> <ServiceName> [ <ServiceArgs> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;ServiceName&gt;||&lt;String&gt;|Name of the service|
|&lt;ServiceArgs&gt;||&lt;String[]&gt;|Optional arguments to pass to service|


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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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


## Examples

### Example 1 - Start a service

```
Scm start LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice
```

### Example 2 - Start a service with arguments

```
Scm start LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice arg1 arg2 arg3
```
# Scm stop
  Stops a service

## Synopsis
```
Scm stop [options] <ServerName> <ServiceName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|
|&lt;ServiceName&gt;||&lt;String&gt;|Name of the service|


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
|    -Delegate||&lt;SwitchParam&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|-K, -Kdc||&lt;host-or-ip:port&gt;|KDC endpoint|
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


## Examples

### Example 1 - Stop a service

```
Scm stop LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice
```
