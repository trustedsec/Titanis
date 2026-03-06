# Dcom
  Utility for working with DCOM

## Synopsis
```
Dcom <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[invoke](#dcom-invoke)|Invokes a method on an OLE automation object over DCOM|


  For help on a subcommand, use `Dcom <subcommand> -h`
# Dcom invoke
  Invokes a method on an OLE automation object over DCOM

## Synopsis
```
Dcom invoke [options] -Clsid <Guid> -MethodName <String> <ServerName> <Clsid> <MethodName> [ <Arguments> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of the server to connect to|
|    -Clsid||&lt;Guid&gt;|CLSID of object to activate|
|-M, -MethodName||&lt;String&gt;|Name of method to invoke|
|    -Arguments||&lt;String[]&gt;|Arguments to pass to the method|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-E, -EncryptRpc||&lt;SwitchParam&gt;|Encrypts RPC messages|
|    -Clsid||&lt;Guid&gt;|CLSID of object to activate|
|-M, -MethodName||&lt;String&gt;|Name of method to invoke|
|    -Arguments||&lt;String[]&gt;|Arguments to pass to the method|
|    -ConsoleOutputStyle|-OutputStyle|&lt;OutputStyle&gt;|Determines the output style|
||||**Possible values:**|
||||  Freeform|
||||  Raw|
||||  Table|
||||  List|
||||  Csv|
||||  Tsv|
||||  Json|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
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

