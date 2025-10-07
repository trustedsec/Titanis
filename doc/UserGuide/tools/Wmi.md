# Wmi
  Commands for interacting with the Windows Management Instrumentation service

## Synopsis
```
Wmi <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[query](#wmi-query)|Executes a WMI query|
|[backup](#wmi-backup)|Backs up the WMI repository|
|[restore](#wmi-restore)|Restores the WMI repository|
|[lsns](#wmi-lsns)|Lists the available namespaces within a namespace.|
|[lsclass](#wmi-lsclass)|Lists the classes within a namespace.|
|[lsprop](#wmi-lsprop)|Lists the properties of a class or object.|
|[lsmethod](#wmi-lsmethod)|Lists the methods of a class or object.|
|[get](#wmi-get)|Gets an object with a WMI path|
|[exec](#wmi-exec)|Executes a command on a remote system via WMI|
|[invoke](#wmi-invoke)|Invokes a method on a WMI class or object|


  For help on a subcommand, use `Wmi &lt;subcommand&gt; -?`
# Wmi backup
  Backs up the WMI repository

## Synopsis
```
Wmi backup [options] <ServerName> <FileName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;FileName&gt;||&lt;String&gt;|Name of the file to write the backup to|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Examples

### Example 1 - Back up to C:\wmibackup.bak

```
Wmi backup -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```
# Wmi exec
  Executes a command on a remote system via WMI

## Synopsis
```
Wmi exec [options] <ServerName> <CommandLine>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;CommandLine&gt;||&lt;String&gt;|Command line to execute|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -CaptureOutput||&lt;SwitchParam&gt;|Redirects STDOUR and STDERR to a file|
||||  Default: True|
|    -WorkingDir||&lt;String&gt;|Sets the working directory for the new process|
|    -CmdCall||&lt;SwitchParam&gt;|Prepends 'cmd /q /c' to the command|
||||  Default: True|
|    -Wait||&lt;SwitchParam&gt;|Waits for the command to complete|
||||  Default: True|
|    -PollInterval||&lt;Duration&gt;|Polling interval|
||||  Default: 1s|
|    -EnvironmentVariables||&lt;String[]&gt;|Environment variables to pass to the command|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|    -RequireSigning|-signreq|&lt;SwitchParam&gt;|Requires packets to be signed|
|    -RequireSecureNegotiate||&lt;SwitchParam&gt;|Requires the client to authenticate the negotiation|
|    -EncryptSmb||&lt;SwitchParam&gt;|Requires an encrypted connection|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
|    -UseTcp6Only|-6|&lt;SwitchParam&gt;|Only use TCP over IPv6 endpoint|
|    -UseTcp4Only|-4|&lt;SwitchParam&gt;|Only use TCP over IPv4 endpoint|


### Client Behavior

|Name|Aliases|Value|Description|
|-|-|-|-|
|-F, -FollowDfs||&lt;SwitchParam&gt;|Checks for and follows DFS referrals (default=true)|
|    -DfsReferralBufferSize||&lt;Int32&gt;|Specifies the size for the DFS referral buffer (default=4096)|


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

  This command uses WMI Win32_Process.Create to execute a command line,
  optionally capturing the output and waiting for the executed program to exit.
  
  Both -CaptureOutput and -CmdCall are enabled by default.  To disable them,
  specify -CaptureOutput:off or -CmdCall:off
  
  Use -PollInterval to specify the polling interval for checking output as well
  as the Win32_ProcessTrace query.  Specify the value as a number followed by one
  of [ ms, s, m, h ] specifying the unit.
  
  To specify environment variables for the started process, specify
  -EnvironmentVariables followed by a list of &lt;name&gt;=&lt;value&gt; pairs, separated by
  commas.  For example, to specify two variables named VAR1 and VAR2:
  `-EnvironmentVariables VAR1=value1, VAR2=value2`
  
  -CaptureOutput redirects STDOUT and STDERR to a file using the redirection
  provided by CMD.EXE and therefore requires -CmdCall as well.  Wmi exec
  generates a file name using a new GUID and creates this file in
  `C:\Windows\Temp` using SMB.  It periodically checks the file for updates using
  the interval specified by -PollInterval.  Any updates are fetched and printed
  to STDOUT.
  
  While the command is running, Wmi exec uses Win32_ProcessTrace to monitor the
  started process and its child processes.  Once the root process of the tree
  exits, Wmi exec exits, returning the exit status returned by the remote
  process.
  
  Use Ctrl+C to terminate the remote process.  When -CmdCall is enabled, the
  first child process is terminated (that isn't named `conhost.exe`).
  
  

## Examples

### Example 1 - Running a simple command

```
Wmi exec -UserName milchick -Password Br3@kr00m! LUMON-DC1 -Verbose SystemInfo.exe
```

### Example 2 - Specifying an environment variable

```
Wmi exec -UserName milchick -Password Br3@kr00m! LUMON-DC1 -Verbose "ECHO %MYVAR%" -EnvironmentVariables MYVAR=me
```

### Example 3 - Specifying a polling interval

```
Wmi exec -UserName milchick -Password Br3@kr00m! LUMON-DC1 -PollInterval 100ms -Verbose "PING -t localhost"
```
# Wmi get
  Gets an object with a WMI path

## Synopsis
```
Wmi get [options] <ServerName> <ObjectPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPath&gt;||&lt;String[]&gt;|Path of object to get|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  RelativePath|
||||  ObjectFlags|
||||  ObjectType|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Details

  The object path is specified relative to the namespace.
  
  Since the command line parser strips double quotes, use single quotes to
  delimit strings.  Single quotes are converted to double quotes before sending
  the request to WMI.
  

## Examples

### Example 1 - Gets the Win32_Process class

```
Wmi get -namespace root\cimv2 -UserName milchick -Password "Br3@kr00m!" LUMON-FS1 Win32_Process
```

### Example 2 - Gets the Win32_LogicalDisk for C:

```
Wmi get -namespace root\cimv2 -UserName milchick -Password "Br3@kr00m!" LUMON-FS1 Win32_LogicalDisk.DeviceID='C:
```
# Wmi invoke
  Invokes a method on a WMI class or object

## Synopsis
```
Wmi invoke [options] <ServerName> <ObjectPathOrWqlQuery> <Method> [ <Arguments> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPathOrWqlQuery&gt;||&lt;String&gt;|Path of class or object to inspect|
|&lt;Method&gt;||&lt;String&gt;|Method name|
|&lt;Arguments&gt;||&lt;String[]&gt;|Arguments to pass to the method|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Examples

### Example 1 - Start EXPLORER.EXE

```
Wmi invoke -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process Create C:\WINDOWS\explorer.exe
```

### Example 2 - Terminate a process by PID

```
Wmi invoke -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process.Handle=8008 Terminate
```

### Example 3 - Terminate a process by name

```
Wmi invoke -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-DC1 "SELECT * FROM Win32_Process WHERE Caption='REGEDIT.EXE'" Terminate
```
# Wmi lsclass
  Lists the classes within a namespace.

## Synopsis
```
Wmi lsclass [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  RelativePath|
||||  ClassPartBytes|
||||  HasMethodPart|
||||  ObjectFlags|
||||  Name|
||||  BaseClassName|
||||  NdValueTableLength|
||||  ValueTableLength|
||||  ObjectType|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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

# Wmi lsmethod
  Lists the methods of a class or object.

## Synopsis
```
Wmi lsmethod [options] <ServerName> <ObjectPath>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPath&gt;||&lt;String[]&gt;|Path of class or object to inspect|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  Flags|
||||  Signature|
||||  Name|
||||  ClassOfOrigin|
||||  QualifiersText|
||||  Subtype|
||||  SubtypeCode|
||||  PrivilegesText|
||||  IsReadOnly|
||||  ShortDescription|
||||  FullDescription|
||||  IsStatic|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Details

  You may specify multiple object paths.  Each object path may be a class or an
  instance.
  
  Use -WithQualifiers to filter by one or more qualifiers.  Each entry may either
  be a qualifier name or a name-value pair of the form &lt;name&gt;=&lt;value&gt;.  If only a
  name is specified, the filter matches if the qualifier is present with a value
  other than 'false'.  If the &lt;name&gt;=&lt;value&gt; syntax is used, the qualifier value
  must match using a case-insensitive string comparison.  If the qualifier has
  multiple values, only one value must match.
  

## Examples

### Example 1 - List the methods of the Win32_Process class

```
Wmi lsmethod -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 Win32_Process
```

### Example 2 - List only the static methods of the Win32_Process class

```
Wmi lsmethod -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers static Win32_Process
```

### Example 3 - List the methods of the Win32_Process class that require the SeDebugPrivilege

```
Wmi lsmethod -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers Privileges=SeDebugPrivilege Win32_Process
```
# Wmi lsns
  Lists the available namespaces within a namespace.

## Synopsis
```
Wmi lsns [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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

# Wmi lsprop
  Lists the properties of a class or object.

## Synopsis
```
Wmi lsprop [options] <ServerName> [ <ObjectPath> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPath&gt;||&lt;String[]&gt;|Path of class or object to inspect|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  PropertyType|
||||  DefaultValue|
||||  Name|
||||  ClassOfOrigin|
||||  QualifiersText|
||||  Subtype|
||||  SubtypeCode|
||||  PrivilegesText|
||||  IsReadOnly|
||||  ShortDescription|
||||  FullDescription|
||||  IsStatic|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Details

  You may specify multiple object paths.  Each object path may be a class or an
  instance.
  
  Use -WithQualifiers to filter by one or more qualifiers.  Each entry may either
  be a qualifier name or a name-value pair of the form &lt;name&gt;=&lt;value&gt;.  If only a
  name is specified, the filter matches if the qualifier is present with a value
  other than 'false'.  If the &lt;name&gt;=&lt;value&gt; syntax is used, the qualifier value
  must match using a case-insensitive string comparison.  If the qualifier has
  multiple values, only one value must match.
  

## Examples

### Example 1 - List the properties of the Win32_Process class

```
Wmi lsprop -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 Win32_Process
```

### Example 2 - List the properties of the Win32_Process class that require the SeDebugPrivilege

```
Wmi lsprop -namespace root\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers Privileges=SeDebugPrivilege Win32_Process
```
# Wmi query
  Executes a WMI query

## Synopsis
```
Wmi query [options] <ServerName> <Query>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;Query&gt;||&lt;String&gt;|WQL query to execute|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\cimv2|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Examples

### Example 1 - Query running processes with all fields

```
Wmi query LUMON-FS1 -UserName milchick -Password "Br3@kr00m!" "SELECT * FROM Win32_Process"
```

### Example 2 - Query running processes with select fields

```
Wmi query LUMON-FS1 -UserName milchick -Password "Br3@kr00m!" -OutputFields Caption, ProcessID, ParentProcessID  "SELECT * FROM Win32_Process"
```
# Wmi restore
  Restores the WMI repository

## Synopsis
```
Wmi restore [options] <ServerName> <FileName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;FileName&gt;||&lt;String&gt;|Name of the file to read the backup from|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ForceShutdown||&lt;SwitchParam&gt;|Forces any active clients to shut down|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
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


## Examples

### Example 1 - Restore from C:\wmibackup.bak

```
Wmi restore -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```

### Example 2 - Restore from C:\wmibackup.bak, shutting down clients

```
Wmi restore -ForceShutdown -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```
