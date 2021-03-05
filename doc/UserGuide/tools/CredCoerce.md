# CredCoerce
  Sends RPC calls to coerce a system to authenticate to a remote system

## Synopsis
```
CredCoerce [options] -Techniques <ComponentSelector`1[]> <ServerName> <VictimPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of computer to coerce|
|&lt;VictimPath&gt;||&lt;String&gt;|Path to send in RPC call|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Techniques||&lt;ComponentSelector`1[]&gt;|List of coercion techniques to attempt|
||||**Possible values:**|
||||  *|
||||  Efs.OpenFile|
||||  Efs.EncryptFile|
||||  Efs.DecryptFile|
||||  Efs.QueryUsersOnFile|
||||  Efs.QueryRecoveryAgents|
||||  Efs.RemoveUsersFromFile|
||||  Efs.AddUsersToFile|
||||  Efs.FileKeyInfo|
||||  Efs.DuplicateEncryptionInfoFile|
||||  Efs.AddUsersToFileEx|
||||  Efs.FileKeyInfoEx|
||||  Efs.GetEncryptedFileMetadata|
||||  Efs.SetEncryptedFileMetadata|
||||  Efs.EncryptFileExSrv|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|


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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints verbose messages|
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
|    -Kdc||&lt;String&gt;|KDC address|
|    -KdcPort||&lt;Int32&gt;|KDC port|
||||  Default: 88|


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

