# Ldap
  Performs LDAP operations

## Synopsis
```
Ldap <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|[search](#ldap-search)|Searches the directory by name|
|[query](#ldap-query)|Queries the directory|
|[watch](#ldap-watch)|Watches for changes to an object or subtree|
|[schema](#ldap-schema)|Gets the schema|
|[listsyntax](#ldap-listsyntax)|Lists AD syntaxes|
|[namedbits](#ldap-namedbits)|Prints the bits with symbolic names|
|[add](#ldap-add)|Adds an object to the directory|
|[addou](#ldap-addou)|Adds a new organizational unit|
|[adduser](#ldap-adduser)|Adds a new user|
|[addcomputer](#ldap-addcomputer)|Adds a computer account to the directory|
|[mod](#ldap-mod)|Modifies an object in the directory|
|[moduser](#ldap-moduser)|Modifies a directory entry|
|[whoami](#ldap-whoami)|Gets the name of the authenticated user|


  For help on a subcommand, use `Ldap <subcommand> -h`
# Ldap add
  Adds an object to the directory

## Synopsis
```
Ldap add [options] -ObjectClass <String> -ObjectName <String[]> <ServerName> <ObjectName> <ObjectClass>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|    -ObjectClass||&lt;String&gt;|Object class of object to add|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ObjectClass||&lt;String&gt;|Object class of object to add|
|    -Attributes||&lt;AttributeChangeSpec[]&gt;|Attributes to set as name=value pars|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap addcomputer
  Adds a computer account to the directory

## Synopsis
```
Ldap addcomputer [options] -ObjectName <String[]> <ServerName> <ObjectName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NewPassword||&lt;String&gt;|Password of new account|
|    -LogonName||&lt;String&gt;|User name for auth requests|
|    -DisplayName||&lt;String&gt;|Display name for user|
|    -UserCerts||&lt;String[]&gt;|Names of files containing certificates to associate with the user|
|    -Os||&lt;String&gt;|Name of installed operating system|
|    -OsVersion||&lt;String&gt;|Version of installed operating system|
|-M, -MemberOf||&lt;String[]&gt;|Groups to make the user a member of|
|    -Attributes||&lt;AttributeChangeSpec[]&gt;|Attributes to set as name=value pars|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap addou
  Adds a new organizational unit

## Synopsis
```
Ldap addou [options] -ObjectName <String[]> <ServerName> <ObjectName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Attributes||&lt;AttributeChangeSpec[]&gt;|Attributes to set as name=value pars|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap adduser
  Adds a new user

## Synopsis
```
Ldap adduser [options] -ObjectName <String[]> <ServerName> <ObjectName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -NewPassword||&lt;String&gt;|Password of new account|
|    -LogonName||&lt;String&gt;|User name for auth requests|
|    -GivenName||&lt;String&gt;|Given name (first name)|
|    -Surname||&lt;String&gt;|Surname (last name)|
|    -DisplayName||&lt;String&gt;|Display name for user|
|    -UserCerts||&lt;String[]&gt;|Names of files containing certificates to associate with the user|
|-M, -MemberOf||&lt;String[]&gt;|Groups to make the user a member of|
|    -Attributes||&lt;AttributeChangeSpec[]&gt;|Attributes to set as name=value pars|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|    -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap listsyntax
  Lists AD syntaxes

## Synopsis
```
Ldap listsyntax [options]
```

## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
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
||||  EqualityContract|
||||  syntaxKey|
||||  memberName|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|


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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


## Details

  A syntax describes the format of data within an attribute value and specifies
  how the raw bytes are decoded into the logical value.
  
# Ldap mod
  Modifies an object in the directory

## Synopsis
```
Ldap mod [options] -ObjectName <String[]> <ServerName> <ObjectName> [ <Changes> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|    -Changes||&lt;AttributeChangeSpec[]&gt;|Changes to make as name?=value|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Changes||&lt;AttributeChangeSpec[]&gt;|Changes to make as name?=value|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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


## Examples

### Example 1 - Add a certificate to an account

```
Ldap mod LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m! ALLENTOWN$  userCertificate:file+=allentown.cer
```
  This command authenticates as milchick, loads the certificate from the file
  allentown.cer, and associates it with the ALLENTOWN$ account.

### Example 2 - Adding resource-based constrained delegate to a computer account

```
Ldap mod LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m!  Stealth$ msDS-AllowedToDelegateTo+=HOST/ALLENTOWN, msDS-AllowedToDelegateTo+=cifs/ALLENTOWN
```
  This command authenticates as milchick and allows the STEALTH$ account to
  delegate to ALLENTOWN for the `cifs` and `host` SPNs.
# Ldap moduser
  Modifies a directory entry

## Synopsis
```
Ldap moduser [options] -ObjectName <String[]> <ServerName> <ObjectName> [ <Changes> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|    -Changes||&lt;AttributeChangeSpec[]&gt;|Changes to make as name?=value|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -OldPassword||&lt;String&gt;|Old password (for password change)|
|    -NewPassword||&lt;String&gt;|New password (for password change or reset)|
|    -Changes||&lt;AttributeChangeSpec[]&gt;|Changes to make as name?=value|
|    -ObjectName||&lt;String[]&gt;|Names or DNs of objects to create|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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


## Details

  Specify attribute changes as a series of name?=value pairs where ?= is:
  
    +=   Add a value
    -=   Remove a value
    =    Replace all values
  
  For example:
  
  	servicePrincipleName+=HOST/ALLENTOWN   # Adds the SPN
  	servicePrincipleName-=HOST/ALLENTOWN   # Removes the SPN
  	servicePrincipleName=HOST/ALLENTOWN   # Replaces all SPNs
  
  To add or remove multiple values, specify each value as a separate name?=value
  pair:
  
  	# Adds 3 SPNs
  	servicePrincipleName+=HOST/ALLENTOWN servicePrincipleName+=cifs/ALLENTOWN
  servicePrincipleName+=RestrictedKrbHost/ALLENTOWN
  
  By default, the attribute values are parsed according to their syntax.  For
  numeric attributes with bitflags, you may use the named bits, separating
  multiple bit names with a comma.  For example, to set the encryption types for
  an account:
  
  	msDS-SupportedEncryptionTypes=Aes128CtsHmacSha1_96,Aes256CtsHmacSha1_96
  
  Use the `namedbits` command to view a list of supported attributes with
  bitflags.
  
  
  
  You may specify multiple operations for the same attribute within a single
  command line.  Each operation is sent to the LDAP server as part of the
  modification request, in the order specified on the command line.  Note that
  consecutive changes to the same attribute with the same operation are combined.
   IN the above example, all 3 SPNs are added in a single operation.
  
  
  
  
# Ldap namedbits
  Prints the bits with symbolic names

## Synopsis
```
Ldap namedbits [options] [ <Attribute> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;Attribute&gt;||&lt;String[]&gt;|Attribute(s) to print (default is all)|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
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
||||  EqualityContract|
||||  Attribute|
||||  Name|
||||  Value|
||||  HexValue|
|    -OutputHeaders||&lt;SwitchParam&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|


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
|-D, -Diagnostic|-vv|&lt;SwitchParam&gt;|Prints diagnostic messages|
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|

# Ldap query
  Queries the directory

## Synopsis
```
Ldap query [options] <ServerName> [ <Filter> ]
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -Filter||&lt;String&gt;|LDAP query|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -Filter||&lt;String&gt;|LDAP query|
|    -SearchBase||&lt;LdapDistinguishedName[]&gt;|DN of search root (default is domain root)|
|    -Scope||&lt;LdapSearchScope&gt;|Scope of search|
||||**Possible values:**|
||||  BaseObject|
||||  Base|
||||  SingleLevel|
||||  WholeSubtree|
||||  Subtree|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch per page|
||||  Default: 100|
|    -IncludeDeleted||&lt;SwitchParam&gt;|Includes delete items (but not recycled)|
|    -IncludeRecycled||&lt;SwitchParam&gt;|Includes deleted and recycled items|
|    -IncludeDeletedLinks||&lt;SwitchParam&gt;|Includes links to deleted items|
|    -DirSync||&lt;HexString&gt;|Only return changes since [cookie]|
|-R, -RecordLimit||&lt;Int32&gt;|Max number of records to return|
|    -FollowReferrals||&lt;SwitchParam&gt;|Follows referrals|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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


## Details

  Ldap query issues a query to an LDAP server.  Use -OutputFields to specify the
  names of the attributes to retrieve; by default, only the DN of the entries is
  printed.
  
  If no search base is provided, Ldap query uses the root of the domain.
  
  -SearchBase supports these special names:
  
  * DomainRoot - the default domain naming context of the server
  * ForestRoot - the forest root naming context
  * ConfigRoot - the configuration naming context
  * SchemaRoot - the schema naming context
  * RootDse - The root entry
  
  -Filter accepts an LDAP query.  An LDAP query consists of one or more
  assertions of the form
  
  	(&lt;attr&gt; &lt;op&gt; &lt;value&gt;)
  
  where &lt;op&gt; is one of:
    =   (exact match, has attribute, or matches substring)
    ~=  (approximate match)
    &lt;=  (less or equal)
    &gt;=  (greater or equal)
    &amp;=  (has all bits) (LDAP_MATCHING_RULE_BIT_AND)
    |=  (has one or more bits) (LDAP_MATCHING_RULE_BIT_OR)
    *=  (transitive match) (LDAP_MATCHING_RULE_TRANSITIVE_EVAL)
  
  NOTE: Active Directory treats `=` and `~=` the same, although the queries are
  represented differently on the wire.
  NOTE: `&=`, `|=`, and `*=` are extensions implemented by Active Directory.
  
  To invert a filter and return objects that do not meet the criteria, prepend a
  `!`.  For example, to return disabled accounts:
  
  To query objects with an attribute, use `=*`.  For example, to query objects
  with a servicePrincipalName, use:
  
    (servicePrincipalName=*)
  
  To combine multiple assertions, specify a `&` (all must match) or `|` (at least
  one must match) followed by multiple filter clauses, surrounding the entire
  expression with `(` and `)`.  For example:
  
    (&amp;(attr1=value)(attr2=value)(attr3=value))
  
  A few of the fields support named bits.  Use the `namedbits` command for a list
  of supported attributes and bit names.
  
  
  NOTE: Although not strictly required, it is a good idea to surround the filter
  with quotes to avoid having to escape special characters.
  

## Examples

### Example 1 - Find User with Logon Name 'milchick'

```
Ldap query LUMON-DC1 '(samAccountName=milchick)' -OutputFields distinguishedName, objectSid
```

### Example 2 - Find Objects with SPNs

```
Ldap query LUMON-DC1 '(servicePrincipalName=*)' -OutputFields distinguishedName, objectSid, servicePrincipalName
```

### Example 3 - Query rootDse with no authentication

```
Ldap query LUMON-DC1 -OutputFields * -OutputStyle List
```

### Example 4 - Query for accounts trusted for unconstrained delegation

```
Ldap query LUMON-DC1 -OutputFields * "(userAccountControl|=TrustedForDelegation)"
```

### Example 5 - Query for accounts trusted for S4U2self

```
Ldap query LUMON-DC1 -OutputFields * "(userAccountControl|=TrustedForS4U2self)"
```

### Example 6 - Query for accounts trusted for constrained delegation

```
Ldap query LUMON-DC1 -OutputFields * "(msDS-AllowedToDelegateTo=*)"
```
# Ldap schema
  Gets the schema

## Synopsis
```
Ldap schema [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap search
  Searches the directory by name

## Synopsis
```
Ldap search [options] -SearchName <String[]> <ServerName> <SearchName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|
|    -SearchName||&lt;String[]&gt;|Name to search for|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -SearchName||&lt;String[]&gt;|Name to search for|
|    -SearchBase||&lt;LdapDistinguishedName[]&gt;|DN of search root (default is domain root)|
|    -Scope||&lt;LdapSearchScope&gt;|Scope of search|
||||**Possible values:**|
||||  BaseObject|
||||  Base|
||||  SingleLevel|
||||  WholeSubtree|
||||  Subtree|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch per page|
||||  Default: 100|
|    -IncludeDeleted||&lt;SwitchParam&gt;|Includes delete items (but not recycled)|
|    -IncludeRecycled||&lt;SwitchParam&gt;|Includes deleted and recycled items|
|    -IncludeDeletedLinks||&lt;SwitchParam&gt;|Includes links to deleted items|
|    -DirSync||&lt;HexString&gt;|Only return changes since [cookie]|
|-R, -RecordLimit||&lt;Int32&gt;|Max number of records to return|
|-F, -FollowReferrals||&lt;SwitchParam&gt;|Follows referrals|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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


## Details

  Ldap search uses the ANR feature of Active Directory to find objects where any
  designated name-like field begins with a search string.
  
  To request items that match exactly (rather than those beginning with) a search
  term, prepend `=` to the search term.
  
  Other substring searches (contains or begins with) are not supported; wildcards
  will be interpreted literally.
  
  Note that these rules are observed and enforced by Active Directory; Ldap
  search merely sends what you give it.
  

## Examples

### Example 1 - Search for accounts beginning with `admin`

```
Ldap search admin
```

### Example 2 - Search for accounts matching `milchick` exactly

```
Ldap search =milchick
```
# Ldap watch
  Watches for changes to an object or subtree

## Synopsis
```
Ldap watch [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -SearchBase||&lt;LdapDistinguishedName[]&gt;|DN of search root (default is domain root)|
|    -Scope||&lt;LdapSearchScope&gt;|Scope of search|
||||**Possible values:**|
||||  BaseObject|
||||  Base|
||||  SingleLevel|
||||  WholeSubtree|
||||  Subtree|
|    -PageSize||&lt;Int32&gt;|Number of results to fetch per page|
||||  Default: 100|
|    -IncludeDeleted||&lt;SwitchParam&gt;|Includes delete items (but not recycled)|
|    -IncludeRecycled||&lt;SwitchParam&gt;|Includes deleted and recycled items|
|    -IncludeDeletedLinks||&lt;SwitchParam&gt;|Includes links to deleted items|
|    -DirSync||&lt;HexString&gt;|Only return changes since [cookie]|
|-R, -RecordLimit||&lt;Int32&gt;|Max number of records to return|
|-F, -FollowReferrals||&lt;SwitchParam&gt;|Follows referrals|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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

# Ldap whoami
  Gets the name of the authenticated user

## Synopsis
```
Ldap whoami [options] <ServerName>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;ServerName&gt;||&lt;String&gt;|Name of LDAP server|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-G, -Gc||&lt;SwitchParam&gt;|Global Catalog server|
|    -Ssl||&lt;SwitchParam&gt;|Use SSL|
|    -SslCert||&lt;String&gt;|Name of PEM or PFX certificate file|
|    -SslKeyFile||&lt;String&gt;|Name of PFX file for SSL authentication|
|    -SslKeyPassword||&lt;String&gt;|Password for -SslCert or -SslKeyFile|
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
||||  SaslString|
||||  PrincipalName|
||||  Kind|
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

