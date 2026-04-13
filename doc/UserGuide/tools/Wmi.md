# Wmi
  Commands for interacting with the Windows Management Instrumentation service

## Synopsis
```
Wmi <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[backup](#wmi-backup)|Backs up the WMI repository|
|[delete](#wmi-delete)|Deletes a WMI object|
|[exec](#wmi-exec)|Executes a command on a remote system via WMI|
|[get](#wmi-get)|Gets an object with a WMI path|
|[invoke](#wmi-invoke)|Invokes a method on a WMI class or object|
|[lsclass](#wmi-lsclass)|Lists the classes within a namespace.|
|[lsmethod](#wmi-lsmethod)|Lists the methods of a class or object.|
|[lsns](#wmi-lsns)|Lists the available namespaces within a namespace.|
|[lsprop](#wmi-lsprop)|Lists the properties of a class or object.|
|[query](#wmi-query)|Executes a WMI query|
|[restore](#wmi-restore)|Restores the WMI repository|


  For help on a subcommand, use `Wmi <subcommand> -h`
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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

### Example 1 - Back up to C:\wmibackup.bak

```
Wmi backup -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```
# Wmi delete
  Deletes a WMI object

## Synopsis
```
Wmi delete [options] <ServerName> <ObjectPathOrWqlQuery>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPathOrWqlQuery&gt;||&lt;String[]&gt;|Path to object or WQL query of objects to invoke on|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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

### Example 1 - Terminate a process by PID

```
Wmi delete -UserName milchick -Password Br3@kr00m! LUMON-DC1 Win32_Process.Handle=8008
```

### Example 2 - Terminate a process by name

```
Wmi delete -UserName milchick -Password Br3@kr00m! LUMON-DC1 "SELECT * FROM Win32_Process WHERE Caption='REGEDIT.EXE'"
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
|    -AuthProxy||&lt;EndPoint&gt;|Endpoint of auth proxy|
|    -CaptureOutput||&lt;SwitchParam&gt;|Redirects STDOUR and STDERR to a file|
||||  Default: True|
|    -CmdCall||&lt;SwitchParam&gt;|Prepends 'cmd /q /c' to the command|
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
||||  TreeTable|
|    -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -EnvironmentVariables||&lt;String[]&gt;|Environment variables to pass to the command|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PollInterval||&lt;Duration&gt;|Polling interval|
||||  Default: 1s|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -Wait||&lt;SwitchParam&gt;|Waits for the command to complete|
||||  Default: True|
|    -WorkingDir||&lt;String&gt;|Sets the working directory for the new process|


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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  RelativePath|
||||  ObjectFlags|
||||  ObjectType|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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
|&lt;ObjectPathOrWqlQuery&gt;||&lt;String[]&gt;|Path to object or WQL query of objects to invoke on|
|&lt;Method&gt;||&lt;String&gt;|Method to invoke|
|&lt;Arguments&gt;||&lt;String[]&gt;|Arguments to pass to the method|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -SkipParams||&lt;String[]&gt;|List of parameters to skip|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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

  For each object, Wmi invoke looks up the specified method and parses/coerces
  the command line arguments after the method name as arguments to the WMI
  method.
  
  To pass an array of values to a WMI method, enter each element as a separate
  command line argument (separated by spaces) with [ before the first element and
  ] after the last argument.  For example, to invoke this method:
  
  	void WmiMethod(string argFirst, int[] values, string argLast)
  
  you would enter:
  
  	Wmi invoke ... WmiMethod "first arg" [ 1 2 3 4 5 ] "last arg"
  
  

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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
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
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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

# Wmi lsmethod
  Lists the methods of a class or object.

## Synopsis
```
Wmi lsmethod [options] <ServerName> <ObjectPathOrWqlQuery>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|&lt;ObjectPathOrWqlQuery&gt;||&lt;String[]&gt;|Path to object or WQL query of objects to invoke on|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
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
||||  Id|
||||  IsInputParameter|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  PropertyType|
||||  DefaultValue|
||||  RuntimeType|
||||  ElementType|
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
||||  Id|
||||  IsInputParameter|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Locale||&lt;String&gt;|Locale|
||||  Default: en-US|
|    -Namespace||&lt;String&gt;|Namespace to query|
||||  Default: root\\cimv2|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch at a time|
||||  Default: 10|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
|    -SpnOverride||&lt;SpnMapping[]&gt;|Specifies an SPN override|
|    -WithQualifiers||&lt;String[]&gt;|Filter qualifiers|


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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -ForceShutdown||&lt;SwitchParam&gt;|Forces any active clients to shut down|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    -Socks5||&lt;host-or-ip:port&gt;|End point of SOCKS 5 server to use|
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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -HostAddress|-ha|&lt;String[]&gt;|Network address(es) of the server|
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

### Example 1 - Restore from C:\wmibackup.bak

```
Wmi restore -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```

### Example 2 - Restore from C:\wmibackup.bak, shutting down clients

```
Wmi restore -ForceShutdown -UserName milchick -Password Br3@kr00m! LUMON-FS1 C:\wmibackup.bak
```
