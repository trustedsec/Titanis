# Smb2Client
  Performs operations on an SMB2 server.

## Synopsis
```
Smb2Client <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[ls](#smb2client-ls)|Lists the contents of a directory (including named pipes).|
|[get](#smb2client-get)|Gets the contents of a file.|
|[put](#smb2client-put)|Sends a file to the server.|
|[watch](#smb2client-watch)|Watches for modifications to a directory or subtree.|
|[enumnics](#smb2client-enumnics)|Queries the server for a list of network interfaces.|
|[mklink](#smb2client-mklink)|Creates a symbolic link.|
|[mount](#smb2client-mount)|Creates a mount point or junction.|
|[umount](#smb2client-umount)|Unmounts a mount point.|
|[mkdir](#smb2client-mkdir)|Creates a directory.|
|[rmdir](#smb2client-rmdir)|Deletes a directory.|
|[rm](#smb2client-rm)|Deletes a file.|
|[touch](#smb2client-touch)|Updates the timestamps or attributes of a file or directory on an SMB share.|
|[enumshares](#smb2client-enumshares)|Lists shares on the server|
|[enumopenfiles](#smb2client-enumopenfiles)|Lists files open on the server.|
|[enumsessions](#smb2client-enumsessions)|Lists active sessions on the server.|
|[enumsnapshots](#smb2client-enumsnapshots)|Lists the available snapshots for a file or directory.|
|[enumstreams](#smb2client-enumstreams)|Lists the data streams of a file or directory.|


  For help on a subcommand, use `Smb2Client &lt;subcommand&gt; -?`
# Smb2Client enumnics
  Queries the server for a list of network interfaces.

## Synopsis
```
Smb2Client enumnics [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  InterfaceIndex|
||||  Capabilities|
||||  LinkSpeed|
||||  AddressFamily|
||||  EndPoint|
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


## Details

  Clients generally use this functionality to establish multiple channels.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client enumnics resolves
  it either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is
  not specified, &lt;server&gt; is resolved.  In either case, the resulting IP
  addresses may be filtered by address family using -4 or -6.  In either case,
  &lt;server&gt; is used to refer to the server within the SMB2 protocol.
  
  Smb2Client enumnics supports both NTLM and Kerberos authentication.  The
  -UserName and -Password parameters apply to both NTLM and Kerberos.  You may
  also specify -NtlmHash for NTLM.  Each authentication mechanism has a variety
  of parameters to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client enumnics applies the same filtering
  rules for -4 and -6.
  
# Smb2Client enumopenfiles
  Lists files open on the server.

## Synopsis
```
Smb2Client enumopenfiles [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Level||&lt;OpenFileInfoLevel&gt;|Which level of detail to query|
||||**Possible values:**|
||||  Level2|
||||  Level3|
|    -OpenBy||&lt;String&gt;|Select files open by this user|
|    -BasePath||&lt;String&gt;|Select files starting with this path|
|    -BufferSize||&lt;Int32&gt;|Max size for response buffer|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Id|
||||  Permissions|
||||  LockCount|
||||  Path|
||||  UserName|
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

  Use -OpenBy to filter the results by the name of the user that has the file
  open.  The filtering is performed by the server.  The user name should be only
  the name of the user without the domain.  Including the domain will generate a
  warning and likely cause the all results to be filtered out.
  
  Use -BasePath to specify the path to filter by.  The filtering is performed by
  the server, which checks whether the path of each open file starts with the
  base path.  This must be an absolute path to succeed.  For files, the path must
  start with X: where X is the drive letter.  To search for named pipes, the path
  must begin with a backslash (e.g. \srvsvc).  Since -BasePath uses string
  filtering, the path need not be a valid object.  For example, filtering by
  'C:\Win' would include results under C:\Windows.  Wildcards are not supported.
  
# Smb2Client enumsessions
  Lists active sessions on the server.

## Synopsis
```
Smb2Client enumsessions [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Level||&lt;SessionInfoLevel&gt;|Which level of detail to query|
||||**Possible values:**|
||||  Level0|
||||  Level1|
||||  Level2|
||||  Level10|
||||  Level502|
|    -ClientComputer||&lt;String&gt;|Select sessions belonging to this user|
|    -ClientUserName||&lt;String&gt;|Select sessions connected to by this computer|
|-B, -BufferSize||&lt;Int32&gt;|Max size for response buffer|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  ClientName|
||||  UserName|
||||  NumberOfOpens|
||||  ConnectedTime|
||||  IdleTime|
||||  UserFlags|
||||  ClientType|
||||  Transport|
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

  Use -ClientComputer and -ClientUserName to filter the results.  The filtering
  is processed on the server.  The protocol requires the computer name to begin
  with \\, even for IP addresses.  However, if you neglect to do this, Smb2Client
  enumsessions prepends \\ to the name you provide.  The user name provided to
  -ClientUserName should not contain a domain.  If it does, you'll receive a
  warning, but the value will still be passed to the server.
  
# Smb2Client enumshares
  Lists shares on the server

## Synopsis
```
Smb2Client enumshares [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Level||&lt;ShareInfoLevel&gt;|Which level of detail to query|
||||**Possible values:**|
||||  Level1|
||||  Level2|
||||  Level501|
||||  Level502|
||||  Level503|
|-B, -BufferSize||&lt;Int32&gt;|Max size for response buffer|
|    -Spnego||&lt;SwitchParam&gt;|Uses SP-NEGO for authentication|
|    -AuthEpm||&lt;SwitchParam&gt;|Authenticates EP mapper requests|
|    -EncryptEpm||&lt;SwitchParam&gt;|Encrypts EP mappend requests|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -PreferSmb||&lt;SwitchParam&gt;|If the interface supports named pipes, attempt to connect over the named pipe instead of TCP|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  ShareName|
||||  ServerName|
||||  ShareType|
||||  Remark|
||||  Permissions|
||||  MaxUses|
||||  CurrentUses|
||||  Path|
||||  SecurityDescriptorSddl|
||||  Flags|
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

# Smb2Client enumsnapshots
  Lists the available snapshots for a file or directory.

## Synopsis
```
Smb2Client enumsnapshots [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Token|
||||  Timestamp|
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


## Details

  
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client enumsnapshots
  resolves it either as an IP address (IPv4 or IPv6) or a DNS name.  If
  -HostAddress is not specified, &lt;server&gt; is resolved.  In either case, the
  resulting IP addresses may be filtered by address family using -4 or -6.  In
  either case, &lt;server&gt; is used to refer to the server within the SMB2 protocol.
  
  Smb2Client enumsnapshots supports both NTLM and Kerberos authentication.  The
  -UserName and -Password parameters apply to both NTLM and Kerberos.  You may
  also specify -NtlmHash for NTLM.  Each authentication mechanism has a variety
  of parameters to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client enumsnapshots applies the same
  filtering rules for -4 and -6.
  
# Smb2Client enumstreams
  Lists the data streams of a file or directory.

## Synopsis
```
Smb2Client enumstreams [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Name|
||||  Size|
||||  AllocationSize|
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


## Details

  
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client enumstreams
  resolves it either as an IP address (IPv4 or IPv6) or a DNS name.  If
  -HostAddress is not specified, &lt;server&gt; is resolved.  In either case, the
  resulting IP addresses may be filtered by address family using -4 or -6.  In
  either case, &lt;server&gt; is used to refer to the server within the SMB2 protocol.
  
  Smb2Client enumstreams supports both NTLM and Kerberos authentication.  The
  -UserName and -Password parameters apply to both NTLM and Kerberos.  You may
  also specify -NtlmHash for NTLM.  Each authentication mechanism has a variety
  of parameters to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client enumstreams applies the same filtering
  rules for -4 and -6.
  
# Smb2Client get
  Gets the contents of a file.

## Synopsis
```
Smb2Client get [options] <UncPath> [ <DestinationFileName> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|
|&lt;DestinationFileName&gt;||&lt;String&gt;|Name of local file to write|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ChunkSize||&lt;Int32&gt;|Size of chunks to copy|
|    -Unbuffered||&lt;SwitchParam&gt;|Reads the data directly from storage|
|    -Compress||&lt;SwitchParam&gt;|Requests the server compress the data|
|    -Depth||&lt;Int32&gt;|Depth of directory tree to traverse (default = 0 [no recursion], -1 = no limit)|
|-Q, -QueryBufferSize||&lt;Int32&gt;|Specifies the buffer size for querying the directory listing (for recursive operations).|
|    -TreeOnly||&lt;SwitchParam&gt;|Only copies the directory structure, but not the files.|
|-O, -Overwrite||&lt;SwitchParam&gt;|Overwrites existing local files|
|    -ContinueOnError||&lt;SwitchParam&gt;|Continues copying after an error occurs|
|    -TimeWarpToken||&lt;TimeWarpToken&gt;|Snapshot version, either as a date/time or a @GMT token|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  The &lt;UncPath&gt; parameter specifies the source file or directory to get via SMB. 
  The &lt;UncPath&gt; may also include a wildcard pattern (with * or ?).
  
  When copying a file, &lt;DestinationFileName&gt; is optional.  If
  &lt;DestinationFileName&gt; is specified, Smb2Client get fetches the file and writes
  it to the destination file.  If no destination file is specified, Smb2Client
  get writes the file to the console.
  
  When copying a directory, &lt;DestinationFileName&gt; is required and must indicate a
  directory that already exists or is to be created.  Smb2Client get traverses
  the directory tree, optionally filtering the contents by the wildcard specified
  in &lt;UncPath&gt;.  The traversal is limited by -Depth, where a depth of 0 indicates
  that only objects in the source directory are copied and a depth of -1
  indicates no practical limit.  This includes files and directories in the sense
  that the subdirectories are created locally but not populated.  Smb2Client get
  does not exclude empty directories, nor does it skip hidden files.
  
  When copying the file, Smb2Client get fetches the file one chunk at a time. 
  The default chunk size is 32,768 bytes, which loosely resembles using the COPY
  command on a command prompt.  Use -ChunkSize to override the chunk size.  The
  chunk size must not exceed the MaxReadSize of the server.  You can view this by
  including the -Verbose switch.
  
  When writing a file locally, Smb2Client get applies the CreationTimeUtc and
  LastWriteTimeUtc timestamps as well as applying the same file attributes as the
  source file.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client get resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client get supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client get applies the same filtering rules
  for -4 and -6.
  

## Examples

### Example 1 - Print a file to the terminal

```
Smb2Client get \\SERVER\Share\File.txt -u DOMAIN\User -p Password
```
  Copies File.txt from SERVER and prints to the terminal

### Example 2 - Copies a file

```
Smb2Client get \\SERVER\Share\File.txt LocalFile.txt -u DOMAIN\User -p Password
```
  Copies File.txt from SERVER and saves it to a local file named LocalFile.txt

### Example 3 - Copies a directory tree of *.txt files

```
Smb2Client get \\SERVER\Share\*.txt LocalTextFiles -depth 20 -u DOMAIN\User -p Password
```
  Copies all files matching *.txt from a directory tree, up to a depth of 20,
  into a local directory named LocalTextFiles

### Example 4 - Copies a directory tree structure (no files)

```
Smb2Client get \\SERVER\Share\Departments Departments -depth -1 -u DOMAIN\User -p Password
```
  Copies the directory structure with an unlimited depth without copying any
  files
# Smb2Client ls
  Lists the contents of a directory (including named pipes).

## Synopsis
```
Smb2Client ls [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-Q, -QueryBufferSize||&lt;Int32&gt;|Specifies the buffer size for querying the directory listing.|
|    -Depth||&lt;Int32&gt;|Sets the depth limit for a recursive listing (default = 0 [no recursion], -1 = no limit)|
|    -TimeWarpToken||&lt;TimeWarpToken&gt;|Snapshot version, either as a date/time or a @GMT token|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  RelativePath|
||||  FileIndex|
||||  CreationTime|
||||  LastAccessTime|
||||  LastWriteTime|
||||  LastChangeTime|
||||  Size|
||||  SizeOnDisk|
||||  FileAttributes|
||||  EaSize|
||||  ShortName|
||||  FileId|
||||  ReparseTag|
||||  LinkTarget|
||||  SecurityDescriptorSddl|
||||  Owner|
||||  Group|
||||  Dacl|
||||  Sacl|
||||  MaxAccess|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  Smb2Client ls sends a request to the SMB server for a directory listing.  If
  &lt;UncPath&gt; is a directory, the contents of the directory are printed.  If
  &lt;UncPath&gt; is a file, the directory information for that file is printed.  If
  &lt;UncPath&gt; end with a wildcard pattern, only files matching the pattern are
  printed.  
  You may specify which fields to retrieve and print with the -Fields parameter. 
  If any of the selected fields are not contained in the directory listing, an
  additional QUERY_INFO request is sent for each file.  The default field set
  contains LinkTarget, which is not contained in this default set.  When
  requestinf the additional fields, the access mask is calculated depending on
  the fields requested.  For example, requesting the SACL requires
  ACCESS_SYSTEM_SECURITY; other security fields require READ_CONTROL.  If this
  access check fails, none of the fields in that request are retrieved.  The
  MaxAccess field sends a MAXIMAL_ACCESS_REQUEST when opening the file to
  retrieve additional info but does not itself result in an additional
  QUERY_INFO.  A failure to retrieve the additional details does not result in an
  error, but the fields values are omitted from the output.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client ls resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client ls supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client ls applies the same filtering rules for
  -4 and -6.
  

## Examples

### Example 1 - Listing the contents of a share

```
Smb2Client ls \\SERVER\Share -u User -ud DOMAIN -p password
```

### Example 2 - Listing named pipes

```
Smb2Client ls \\SERVER\IPC$ -u User -ud DOMAIN -p password
```

### Example 3 - Listing the contents of a share with an alternate host name

```
Smb2Client ls \\SERVER\Share -ha fileserver.domain.local -u User -ud DOMAIN -p password
```
  In this example, the command line specifies a host name differing from the
  server name to resolve for connecting to the server.

### Example 4 - Listing the contents of a share with an alternate host address

```
Smb2Client ls \\SERVER\Share -ha 10.0.0.1 -u User -ud DOMAIN -p password
```
  In this example, the command line specifies the host address explicitly so that
  SERVER does not need to be resolved.

### Example 5 - Passing the hash

```
Smb2Client ls \\SERVER\Share -u User -ud DOMAIN -NtlmHash 8846F7EAEE8FB117AD06BDD830B7586C
```
  This command line provides the password as an NTLM hash.

### Example 6 - Customizing NTLM

```
Smb2Client ls \\SERVER\Share -u User -ud DOMAIN -p password -ntlmver 10.0.0.0 -w MYWORKSTATION
```
  This command line specifies a different NTLM version and workstation name to
  send during authentication.

### Example 7 - Using Kerberos with a password

```
Smb2Client ls \\SERVER\Share -u User -ud DOMAIN -p password -Kdc 10.0.0.10
```
  This command line specifies credentials along with the -Kdc option specifying
  the KDC to request a ticket from.
# Smb2Client mkdir
  Creates a directory.

## Synopsis
```
Smb2Client mkdir [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Parents||&lt;SwitchParam&gt;|Create parent directories|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  By default, parent directories are not created.  If the directory already
  exists, the server returns STATUS_OBJECT_NAME_COLLISION.  Smb2Client mkdir only
  supports creating a single level directory at a time.  That is, if you try to
  create \\SERVER\Share\Dir1\Subdir2\Subdir3, then \\SERVER\Share\Dir1\Subdir2
  must exist or the server returns STATUS_OBJECT_PATH_NOT_FOUND.
  
  If you specify -Parents, Smb2Client mkdir attempts to create the directory with
  the full path, as above.  If the server returns STATUS_OBJECT_PATH_NOT_FOUND,
  it then starts at the root of the share and checks for each directory, creating
  if necessary.  If the specified directory already exists, no error is
  generated.  This does result in more traffic, as each directory level generates
  a CREATE request.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client mkdir resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client mkdir supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client mkdir applies the same filtering rules
  for -4 and -6.
  
# Smb2Client mklink
  Creates a symbolic link.

## Synopsis
```
Smb2Client mklink [options] <UncPath> <TargetPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|
|&lt;TargetPath&gt;||&lt;String&gt;|Path of the symbolic link target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -PrintPath||&lt;String&gt;|The path to display to the user in directory listings (defaults to &lt;TargetPath&gt;)|
|    -Directory||&lt;SwitchParam&gt;|Creates the symlink as a directory|
|    -Relative||&lt;SwitchParam&gt;|Create the link as a relative path|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  By default, &lt;TargetPath&gt; is interpreted as a full path, regardless of the form.
   Use -Relative to specify a relative link.  For an absolute path to function
  properly, the actual link path must begin with `\??\`. If you don't specify
  this, it is added for you without a warning.
  
  If an object already exists &lt;UncPath&gt;, it must be of the correct type.  That
  is, it must be a file for a file link or a directory to a directory link.  If
  it does not exist, it is created.
  
  A symlink contains two components: the substitution path and the print path. 
  The substitution path specifies the target of the link, whereas the print path
  is displayed to the user in directory listings.  They do not have to correlate.
   By default, Smb2Client mklink sets the print path to &lt;TargetPath&gt;, before
  prepending `\??\` (if applicable).  Use -PrintPath to explicitly set the print
  path.
  
  Smb2Client mklink cannot be used to create junctions.  For this, use mount.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client mklink resolves
  it either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is
  not specified, &lt;server&gt; is resolved.  In either case, the resulting IP
  addresses may be filtered by address family using -4 or -6.  In either case,
  &lt;server&gt; is used to refer to the server within the SMB2 protocol.
  
  Smb2Client mklink supports both NTLM and Kerberos authentication.  The
  -UserName and -Password parameters apply to both NTLM and Kerberos.  You may
  also specify -NtlmHash for NTLM.  Each authentication mechanism has a variety
  of parameters to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client mklink applies the same filtering rules
  for -4 and -6.
  

## Examples

### Example 1 - Creates a symlink to a file

```
Smb2Client mklink \\SERVER\Share\Symlink -Relative ActualFile.txt
```
  Creates a symlink at \\SERVER\Share\Symlink that points to ActualFile.txt
  within the same directory

### Example 2 - Creates a symlink to a directory

```
Smb2Client mklink \\SERVER\Share\SymlinkDir -Directory -Relative ActualDir
```
  Creates a symlink directory at \\SERVER\Share\SymlinkDir that points to
  ActualFir within the same directory

### Example 3 - Creates a symlink to a directory with a different label

```
Smb2Client mklink \\SERVER\Share\SymlinkDir -Directory -Relative ActualDir -PrintName "Not ActualDir"
```
  Creates a symlink directory at \\SERVER\Share\SymlinkDir that points to
  ActualFir within the same directory, but a directory listing prints "Not
  ActualDir"
# Smb2Client mount
  Creates a mount point or junction.

## Synopsis
```
Smb2Client mount [options] <UncPath> <TargetPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|
|&lt;TargetPath&gt;||&lt;String&gt;|Path of the target volume or directory|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -PrintPath||&lt;String&gt;|The path to display to the user in directory listings (defaults to &lt;TargetPath&gt;)|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  The TargetPath may be either a volume or directory.  For volumes, the target
  path should start with \??\ and end with a trailing backslash.  To mount a
  directory (a.k.a. junction), the target path should specify a full path
  starting with X:\ where X is the drive letter.  Smb2Client mount checks the
  path and issues a warning if the path doesn't look quite right, but this does
  not generate an error and will pass the path unaltered to the server.
  
  A mount point contains two components: the substitution path and the print
  path.  The substitution path specifies the target of the, whereas the print
  path is displayed to the user in directory listings.  They do not have to
  correlate.  By default, Smb2Client mount sets the print path to &lt;TargetPath&gt;,
  before prepending `\??\` (if applicable).  Use -PrintPath to explicitly set the
  print path.
  
  Smb2Client mount does not require that the directory specified by &lt;UncPath&gt;
  already exists, and will create the directory if it doesn't exist.  If
  &lt;UncPath&gt;specifies a file instead of a directory, the server will likely fail
  the operation.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client mount resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client mount supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client mount applies the same filtering rules
  for -4 and -6.
  

## Examples

### Example 1 - Creates a volume mount point

```
Smb2Client mount \\SERVER\Share\MountPoint \??\Volume{12345678-1234-1234-1234-123456789ABC}\
```
  Creates a mount point at \\SERVER\Share\MountPoint that points to the volume
  {12345678-1234-1234-1234-123456789ABC}

### Example 2 - Creates a junction

```
Smb2Client mount \\SERVER\Share\Junction \??\C:\WINDOWS
```
  Creates a junction at \\SERVER\Share\Junction that points to C:\WINDOWS on the
  remote system

### Example 3 - Creates a junction with a different label

```
Smb2Client mount \\SERVER\Share\Junction \??\C:\WINDOWS -PrintName "Not Windows"
```
  Creates a junction at \\SERVER\Share\Junction that points to C:\WINDOWS on the
  remote system, but prints the link as Not Windows in a directory listing.
# Smb2Client put
  Sends a file to the server.

## Synopsis
```
Smb2Client put [options] [ <SourceFileName> ] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;SourceFileName&gt;||&lt;String&gt;|Name of local file to send|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ChunkSize||&lt;Int32&gt;|Size of chunks to copy|
||||  Default: 32768|
|    -TimestampsFrom||&lt;UncPath&gt;|UNC Path of remote file to copy Creation, LastAccess, LastWrite and Change Time from.|
|    -CreateTimestamp||&lt;DateTime&gt;|Create time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.|
|    -LastAccessTimestamp||&lt;DateTime&gt;|Last access time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.|
|    -LastWriteTimestamp||&lt;DateTime&gt;|Last write time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.|
|    -ChangeTimestamp||&lt;DateTime&gt;|Change time to set on the file (UTC).  If specified, overrides any timestamps copied from source or remote file.|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  The &lt;UncPath&gt; parameter specifies the target file to write to via SMB.  If
  &lt;SourceFileName&gt; is specified, Smb2Client put opens the file and writes it to
  the destination file on the server.  If no source file is specified, Smb2Client
  put accepts input from the console.
  
  When copying the file, Smb2Client put fetches the file one chunk at a time. 
  The default chunk size is 32,768 bytes, which loosely resembles using the COPY
  command on a command prompt.  Use -ChunkSize to override the chunk size.
  
  When specifying -UseBackupSemantics the server will check if the user has
  SeBackupPrivilege and SeRestorePrivilege.  If they do they are allowed to
  bypass security checks when accessing files. Notably bypassing security checks
  does not bypass file locks or a file marked with the read-only attribute.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client put resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client put supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client put applies the same filtering rules
  for -4 and -6.
  
# Smb2Client rm
  Deletes a file.

## Synopsis
```
Smb2Client rm [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


|Name|Aliases|Value|Description|
|-|-|-|-|
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


## Details

  
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client rm resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client rm supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client rm applies the same filtering rules for
  -4 and -6.
  
# Smb2Client rmdir
  Deletes a directory.

## Synopsis
```
Smb2Client rmdir [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


|Name|Aliases|Value|Description|
|-|-|-|-|
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


## Details

  
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client rmdir resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client rmdir supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client rmdir applies the same filtering rules
  for -4 and -6.
  
# Smb2Client touch
  Updates the timestamps or attributes of a file or directory on an SMB share.

## Synopsis
```
Smb2Client touch [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -CreateTimestamp||&lt;DateTime&gt;|Create time to set on the file (UTC)|
|    -LastAccessTimestamp||&lt;DateTime&gt;|Last access time to set on the file (UTC)|
|    -LastWriteTimestamp||&lt;DateTime&gt;|Last write time to set on the file (UTC)|
|    -ChangeTimestamp||&lt;DateTime&gt;|Change time to set on the file (UTC)|
|    -SetAttributes||&lt;FileAttributeSpec&gt;|File attributes to set on the file or directory. Accepts Formats: RHSATFMCOIEVX (string), 28312 (int), 0x80 (hex). See Detailed help for meaning|
|    -UpdateAttributes||&lt;String&gt;|File attributes modifications|
|    -TimestampsFrom||&lt;UncPath&gt;|UNC Path of remote file to copy timestamps from|
|    -CopyFileAttributes||&lt;SwitchParam&gt;|If specified, also copy file attributes from TimestampsFrom|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  RelativePath|
||||  FileIndex|
||||  CreationTime|
||||  LastAccessTime|
||||  LastWriteTime|
||||  LastChangeTime|
||||  Size|
||||  SizeOnDisk|
||||  FileAttributes|
||||  EaSize|
||||  ShortName|
||||  FileId|
||||  ReparseTag|
||||  LinkTarget|
||||  SecurityDescriptorSddl|
||||  Owner|
||||  Group|
||||  Dacl|
||||  Sacl|
||||  MaxAccess|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  This command may be used with any object within an SMB share, such as files,
  directories, or pipes.
  
  If the file specified at the UNC path does not exist, it will be created.
  If the file specified at the UNC path does exist, the directory entry for the
  file is updated.
  
  Any -*Timestamp arguments will override timestamps taken from -TimestampsFrom.
  Only timestamps provided with a -*Timestamp argument or those taken from
  -TimestampsFrom are used.  No default values are provided and last accessed
  time is not updated automatically when this tool is used.
  
  -SetAttributes accepts either a numeric value or a string of attribute codes
  (below) such as:
  * RHSA
  * 128
  * 0x80
  You may provide any arbitrary integer or hex value, although the server may
  reject it as an invalid parameter if it does not consist of known parameters.
  
  -UpdateAttributes accepts a string of attribute codes preceded by a + or -
  indicating
  whether to set or clear the following attributes.  Examples:
  * +HST-IO   Add Hidden, System, Temporary and remove Not content indexed,
  Offline
  * +HST      Add Hidden, System, Temporary
  * -H        Remove Hidden
  
  If -CopyFileAttributes is set, it is used as the base modified by
  -UpdateAttributes; otherwise, the original files values are used as the base
  value.  If no UpdateAttributes are specified the copied attributes are used as
  is.
  
  Attempting to remove attributes that are not already set is not an error; they
  simply remain unset.
  
  The letters available for use in the attribute parameters are as follows:
  
   R Read-only
   H Hidden
   S System
   A Archive
   T Temporary
   F Sparse
   M Reparse point
   C Compressed
   O Offline
   I Not content indexed
   E Encrypted
   V Integrity
   X No scrub
  
  
  In order to clear all attributes set on a file, specify a -SetAttributes value
  of ""
   
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client touch resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client touch supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client touch applies the same filtering rules
  for -4 and -6.
  

## Examples

### Example 1 - Set hidden and Read-Only attributes on a file, overriding any previous state

```
Smb2Client touch \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -SetAttributes RH
```

### Example 2 - Add Hidden and remove Archive and System attributes from a file

```
Smb2Client touch \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -UpdateAttributes +H-AS
```

### Example 3 - Set the last access time on a file

```
Smb2Client touch \\SERVER\Share\file.txt -u User -ud DOMAIN -p password -LastAccessTimestamp "09/30/2023 13:23:55"
```

### Example 4 - Copy timestamps and attributes from another file

```
Smb2Client touch \\SERVER\Share\file.txt -u User -ud DOMAIN -p password  -TimestampsFrom \\SERVER\Share\otherfile.txt
```
# Smb2Client umount
  Unmounts a mount point.

## Synopsis
```
Smb2Client umount [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -RemoveDirectory||&lt;SwitchParam&gt;|Deletes the directory after unmounting|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  Smb2Client umount removes the mount point information from &lt;UncPath&gt;.  Although
  &lt;UncPath&gt; should be a directory, it doesn't have to be; therefore Smb2Client
  umount may be used to remove mount point info from a file as well, should the
  need arise.
  
  By default, the directory specified to by &lt;UncPath&gt; is left intact.  Specify
  -RemoveDirectory to remove it.  Specifically, this sets the DeleteOnClose flag
  on the object, which may or may not succeed.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client umount resolves
  it either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is
  not specified, &lt;server&gt; is resolved.  In either case, the resulting IP
  addresses may be filtered by address family using -4 or -6.  In either case,
  &lt;server&gt; is used to refer to the server within the SMB2 protocol.
  
  Smb2Client umount supports both NTLM and Kerberos authentication.  The
  -UserName and -Password parameters apply to both NTLM and Kerberos.  You may
  also specify -NtlmHash for NTLM.  Each authentication mechanism has a variety
  of parameters to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client umount applies the same filtering rules
  for -4 and -6.
  
# Smb2Client watch
  Watches for modifications to a directory or subtree.

## Synopsis
```
Smb2Client watch [options] <UncPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;UncPath&gt;||&lt;UNC path&gt;|The UNC path of the target|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Recursive||&lt;SwitchParam&gt;|Watches the entire subtree|
|-B, -BufferSize||&lt;Int32&gt;|Buffer size (default = 2048)|
|    -ContinueOnErrors||&lt;SwitchParam&gt;|Continue watching for changes if an error occurs|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Action|
||||  FileName|
||||  OldFileName|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -EncryptShare||&lt;SwitchParam&gt;|Encrypts PDUs for the target share|
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
|    -UseBackupSemantics||&lt;SwitchParam&gt;|Opens remote resource with backup semantics|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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


## Details

  Use -Recursive to watch for changes to the entire subtree.  Otherwise, only
  objects within &lt;UncPath&gt; are reported.  Note that changing an object within a
  subdirectory changes the subdirectory, so the subdirectory will be reported
  ever if -Recursive is not specified.
  
  When requesting change notifications, Smb2Client watch must specify a maximum
  size for the output buffer.  The default is 2048 to reflect what Windows
  Explorer uses.  This is not sufficient for all cases.  Use -BufferSize to
  change the maximum size.
  
  If an error occurs, by default, Smb2Client watch exits.  Use -ContinueOnErrors
  to ignore the error and continue waiting for notifications.
  
  Monitoring continues either until an error occurs (unless -ContinueOnErrors is
  specified), or until the user presses Ctrl+C.
  
  The &lt;UncPath&gt; parameter specifies the target of the command using the format
  \\&lt;server&gt;[:&lt;port&gt;]\&lt;share&gt;[\&lt;path&gt;] where &lt;port&gt; is an integer specifying the
  port to connect to.  If -HostAddress is specified, Smb2Client watch resolves it
  either as an IP address (IPv4 or IPv6) or a DNS name.  If -HostAddress is not
  specified, &lt;server&gt; is resolved.  In either case, the resulting IP addresses
  may be filtered by address family using -4 or -6.  In either case, &lt;server&gt; is
  used to refer to the server within the SMB2 protocol.
  
  Smb2Client watch supports both NTLM and Kerberos authentication.  The -UserName
  and -Password parameters apply to both NTLM and Kerberos.  You may also specify
  -NtlmHash for NTLM.  Each authentication mechanism has a variety of parameters
  to customize the negotiation.
  
  Kerberos is only enabled if you specify -kdc with the name or address of the
  KDC.  If you specify a name, Smb2Client watch applies the same filtering rules
  for -4 and -6.
  
