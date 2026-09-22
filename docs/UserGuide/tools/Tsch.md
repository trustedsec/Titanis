# Tsch
Interacts with the Task Scheduler service

## Synopsis
```
Tsch <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[add](#tsch-add)**|Adds a scheduled task|
|**[enumfolders](#tsch-enumfolders)**|Gets a list of task folders|
|**[enumtasks](#tsch-enumtasks)**|Gets a list of tasks within folder|
|**[get](#tsch-get)**|Gets a task definition|
|**[start](#tsch-start)**|Starts a task|
|**[stop](#tsch-stop)**|Stops a task|
|**[stopinstance](#tsch-stopinstance)**|Stops a running task instance|


For help on a subcommand, use `Tsch <subcommand> -h`
# Tsch add
Adds a scheduled task

## Synopsis
**Tsch add** [*options*]** -Exec** &lt;*String* &gt;** -RunAs** &lt;*String* &gt;** -RunAsPassword** &lt;*String* &gt; &lt;*ServerName*&gt; [ &lt;*TaskPath*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*TaskPath*&gt;||&lt;*String*&gt;|Task path|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|**-F**, **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|


### Task

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Author**||&lt;*String*&gt;|Task author|
|    **-Description**||&lt;*String*&gt;|Task description|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrite existing task|


### Task Actions

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Args**||&lt;*String*&gt;|Arguments (for -Exec)|
|    **-Exec**||&lt;*String*&gt;|Execute command|
|    **-WorkingDir**||&lt;*String*&gt;|Working directory (for -Exec)|


### Task Principal

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogonType**||&lt;*TaskLogonType*&gt;|Task logon type|
||||Possible values:|
||||  **None**|
||||  **Password**|
||||  **S4u**|
||||  **Interactive**|
||||  **Group**|
||||  **ServiceAccount**|
||||  **InteractiveTokenOrPassword**|
|    **-RunAs**||&lt;*String*&gt;|Run task as principal|
|    **-RunAsPassword**||&lt;*String*&gt;|Password for -RunAs principal|


### Task Settings

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AllowHardTerminate**||&lt;*SwitchParam*&gt;|Allow hard terminate|
||||  Default: True|
|    **-AllowStartOnDemand**||&lt;*SwitchParam*&gt;|Allow start on-demand|
||||  Default: True|
|    **-DeleteExpiredAfter**||&lt;*Duration*&gt;|Delete expired task after duration|
||||  Default: 0s|
|    **-DisallowStartIfOnBattery**||&lt;*SwitchParam*&gt;|Disallow start if on battery|
||||  Default: True|
|    **-DisallowStartOnRemoteAppSession**||&lt;*SwitchParam*&gt;|Disallow start on remote app session|
||||  Default: False|
|    **-Enabled**||&lt;*SwitchParam*&gt;|Wake to run|
||||  Default: True|
|    **-ExecutionTimeLimit**||&lt;*Duration*&gt;|Execution time limit|
||||  Default: 72h|
|    **-Hidden**||&lt;*SwitchParam*&gt;|Hidden|
||||  Default: False|
|**-M**, **-MultipleInstancePolicy**||&lt;*multipleInstancesPolicyType*&gt;|Allow multiple instances|
||||Possible values:|
||||  **Parallel**|
||||  **Queue**|
||||  **IgnoreNew**|
||||  **StopExisting**|
|    **-NetworkProfile**||&lt;*String*&gt;|Network profile|
|    **-Priority**||&lt;*SByte*&gt;|Task priority|
||||  Default: 7|
|    **-RestartOnFailureCount**||&lt;*Byte*&gt;|Restart on failure count|
|    **-RestartOnFailureInterval**||&lt;*Duration*&gt;|Restart on failure interval|
|    **-RunOnlyIfIdle**||&lt;*SwitchParam*&gt;|Run only if idle|
||||  Default: False|
|    **-RunOnlyIfNetworkAvailable**||&lt;*SwitchParam*&gt;|Run only if network available|
||||  Default: False|
|    **-StartWhenAvailable**||&lt;*SwitchParam*&gt;|Start when available|
||||  Default: False|
|    **-StopIfGoingOnBatteries**||&lt;*SwitchParam*&gt;|Stop if going on batteries|
||||  Default: True|
|    **-UseUnifiedSchedulingEngine**||&lt;*SwitchParam*&gt;|Use unified scheduling engine|
||||  Default: False|
|    **-WakeToRun**||&lt;*SwitchParam*&gt;|Wake to run|
||||  Default: False|


### Task Triggers

|Name|Aliases|Value|Description|
|-|-|-|-|
|**-B**, **-BootDelay**||&lt;*Duration*&gt;|Delay after boot trigger|
|    **-LogonDelay**||&lt;*Duration*&gt;|Delay after logon trigger|
|    **-LogonUser**||&lt;*String*&gt;|User ID of logon trigger|
|    **-OnBoot**||&lt;*SwitchParam*&gt;|Trigger on boot|
|    **-Once**||&lt;*SwitchParam*&gt;|Trigger at -TriggerStartBoundary|
|    **-OnceDelay**||&lt;*Duration*&gt;|Random delay for -Once after boot trigger|
|    **-OnIdle**||&lt;*SwitchParam*&gt;|Trigger on idle|
|    **-OnLogon**||&lt;*SwitchParam*&gt;|Trigger on logon|
|    **-OnSessionStateChange**||&lt;*sessionStateChangeType*&gt;|Trigger on session state change|
||||Possible values:|
||||  **ConsoleConnect**|
||||  **ConsoleDisconnect**|
||||  **RemoteConnect**|
||||  **RemoteDisconnect**|
||||  **SessionLock**|
||||  **SessionUnlock**|
|    **-SessionStateDelay**||&lt;*Duration*&gt;|Delay after session state trigger|
|    **-SessionUser**||&lt;*String*&gt;|User ID of session state trigger|
|    **-TriggerEndBoundary**||&lt;*DateTime*&gt;|Trigger end boundary|
|    **-TriggerExecutionTimeLimit**||&lt;*Duration*&gt;|Trigger execution time limit|
||||  Default: 72h|
|    **-TriggerStartBoundary**||&lt;*DateTime*&gt;|Trigger start boundary|

# Tsch enumfolders
Gets a list of task folders

## Synopsis
**Tsch enumfolders** [*options*] &lt;*ServerName*&gt; [ &lt;*FolderPath*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*FolderPath*&gt;||&lt;*String[]*&gt;|Folder path to enumerate|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-PageSize**||&lt;*Int32*&gt;|Number of tasks to retrieve per call|
||||  Default: 100|
|    **-Recursive**||&lt;*SwitchParam*&gt;|Recurse into subfolders|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|    **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

# Tsch enumtasks
Gets a list of tasks within folder

## Synopsis
**Tsch enumtasks** [*options*] &lt;*ServerName*&gt; [ &lt;*FolderPath*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*FolderPath*&gt;||&lt;*String[]*&gt;|Folder path to enumerate|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-PageSize**||&lt;*Int32*&gt;|Number of tasks to retrieve per call|
||||  Default: 100|
|    **-Recursive**||&lt;*SwitchParam*&gt;|Recurse into subfolders|


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|    **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
||||Possible values:|
||||  **ServerName**|
||||  **Name**|
||||  **FolderPath**|
||||  **Actions**|
||||  **Triggers**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

# Tsch get
Gets a task definition

## Synopsis
**Tsch get** [*options*] &lt;*ServerName*&gt; [ &lt;*TaskPath*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*TaskPath*&gt;||&lt;*String[]*&gt;|Path of task|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|**-F**, **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
||||Possible values:|
||||  **ServerName**|
||||  **Name**|
||||  **FolderPath**|
||||  **Actions**|
||||  **Triggers**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

# Tsch start
Starts a task

## Synopsis
**Tsch start** [*options*] &lt;*ServerName*&gt; &lt;*TaskPath*&gt; [ &lt;*AsUser*&gt; ] [ &lt;*SessionId*&gt; ] [ &lt;*RunAsSelf*&gt; ] [ &lt;*TaskArgs*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*TaskPath*&gt;||&lt;*String[]*&gt;|Path of task|
|&lt;*AsUser*&gt;||&lt;*String*&gt;|Runs the task as a specific user (name or SID)|
|&lt;*SessionId*&gt;||&lt;*Int32*&gt;|Terminal Server session to run task in|
|&lt;*RunAsSelf*&gt;||&lt;*SwitchParam*&gt;|Run task as authenticated user|
|&lt;*TaskArgs*&gt;||&lt;*String[]*&gt;|Task start arguments|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|**-F**, **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
||||Possible values:|
||||  **ServerName**|
||||  **TaskPath**|
||||  **InstanceGuid**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

# Tsch stop
Stops a task

## Synopsis
**Tsch stop** [*options*] &lt;*ServerName*&gt; &lt;*TaskPath*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*TaskPath*&gt;||&lt;*String[]*&gt;|Path of task|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|**-F**, **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

# Tsch stopinstance
Stops a running task instance

## Synopsis
**Tsch stopinstance** [*options*] &lt;*ServerName*&gt; &lt;*InstanceGuid*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*InstanceGuid*&gt;||&lt;*Guid[]*&gt;|Instance(s) to stop|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
|    **-PasswordBytes**||&lt;*HexString*&gt;|Password to authenticate with (as bytes)|
|    **-Sspi**||&lt;*SwitchParam*&gt;|Uses SSPI authentication (Windows only)|
|    **-UserDomain**|**-ud**|&lt;*String*&gt;|Domain of user to authenticate with|
|    **-UserName**|**-u**|&lt;*UserPrincipalName*&gt;|User name to authenticate with, not including the domain|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES key (128 or 256)|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing the armor ticket|
|    **-DelegateTicket**||&lt;*FileSpec[]*&gt;|Sends the tickets (and keys) to the target for delegation|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Kdc**||&lt;*host-or-ip:port*&gt;|KDC endpoint|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service to proxy through|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-SpnOverride**||&lt;*SpnMapping[]*&gt;|Specifies an SPN override|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-Tickets**|**-Ticket**|&lt;*FileSpec[]*&gt;|Name of file containing service tickets (.kirbi or ccache)|
|    **-U2UserName**||&lt;*UserPrincipalName*&gt;|User name to request TGT for U2U|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|


### Authentication (NTLM)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-NtlmVersion**||&lt;*Version*&gt;|NTLM version number (a.b.c.d)|
|    **-Workstation**|**-w**|&lt;*String*&gt;|Name of workstation to send with NTLM authentication|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DfsReferralBufferSize**||&lt;*Int32*&gt;|Specifies the size for the DFS referral buffer (default=4096)|
||||  Default: 4096|
|**-F**, **-FollowDfs**||&lt;*SwitchParam*&gt;|Checks for and follows DFS referrals (default=true)|
||||  Default: True|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Dialects**||&lt;*Smb2Dialect[]*&gt;|List of SMB2 dialects to negotiate|
||||Possible values:|
||||  **Smb2_0_2**|
||||  **Smb2_1**|
||||  **Smb3_0**|
||||  **Smb3_0_2**|
||||  **Smb3_1_1**|
|    **-EncryptSmb**||&lt;*SwitchParam*&gt;|Requires an encrypted connection|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-RequireSecureNegotiate**||&lt;*SwitchParam*&gt;|Requires the client to authenticate the negotiation|
|    **-RequireSigning**|**-signreq**|&lt;*SwitchParam*&gt;|Requires packets to be signed|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Error Handling

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continues executing even if an error occurs|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### RPC

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AuthEpm**||&lt;*SwitchParam*&gt;|Authenticates EP mapper requests|
|    **-EncryptEpm**||&lt;*SwitchParam*&gt;|Encrypts EP mappend requests|
|    **-EncryptRpc**||&lt;*SwitchParam*&gt;|Encrypts RPC messages|
|    **-OfferNdr**||&lt;*SwitchParam*&gt;|Offers the NDR transfer syntax|
||||  Default: True|
|    **-OfferNdr64**||&lt;*SwitchParam*&gt;|Offers the NDR64 transfer syntax|
||||  Default: True|
|    **-PreferSmb**||&lt;*SwitchParam*&gt;|If the interface supports named pipes, attempt to connect over the named pipe
instead of TCP|
|    **-RpcCallTimeout**||&lt;*Duration*&gt;|Time to wait for RPC calls|
|    **-RpcConnectTimeout**||&lt;*Duration*&gt;|Time to wait for RPC connections|
|    **-Spnego**||&lt;*SwitchParam*&gt;|Uses SP-NEGO for authentication|

