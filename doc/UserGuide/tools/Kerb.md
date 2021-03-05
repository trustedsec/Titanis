# Kerb
  Commands for working with Kerberos authentication

## Synopsis
```
Kerb <subcommand>
```
### Subcommands

|Command|Description|
|-|-|
|[getasinfo](#kerb-getasinfo)|Gets server time and encryption types (with salts) for a user account.|
|[asreq](#kerb-asreq)|Requests a TGT from the KDC.|
|[tgsreq](#kerb-tgsreq)|Requests a ticket from the KDC.|
|[select](#kerb-select)|Selects and displays tickets from a file.|


  For help on a subcommand, use `Kerb &lt;subcommand&gt; -?`
# Kerb asreq
  Requests a TGT from the KDC.

## Synopsis
```
Kerb asreq [options] -UserName <String> -Realm <String> -Kdc <String> -OutputFileName <String>
```

## Options


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|-U, -UserName||&lt;String&gt;|Name of user (no domain)|
|    -Password||&lt;String&gt;|Password|
|-N, -NtlmHash||&lt;HexString&gt;|NTLM hash (hex-encoded, no colons)|
|    -Aes128Key||&lt;HexString&gt;|AES 128 key|
|    -Aes256Key||&lt;HexString&gt;|AES 256 key|
|    -EncTypes||&lt;EType[]&gt;|Encryption types to request in response|
||||**Possible values:**|
||||  Aes128CtsHmacSha1_96|
||||  Aes256CtsHmacSha1_96|
||||  Rc4Hmac|
||||  Rc4HmacExp|
||||  DesCbcMd5|
||||  DesCbcCrc|
|    -Realm||&lt;String&gt;|Name of realm (domain)|
|-K, -Kdc||&lt;String&gt;|Host name or address of KDC|
|-F, -Forwardable||&lt;SwitchParam&gt;|Requests a forwardable ticket|
|    -Proxiable||&lt;SwitchParam&gt;|Requests a forwardable ticket|
|    -Postdate||&lt;DateTime&gt;|Requests a postdated ticket with the specified start date|
|    -RenewTill||&lt;DateTime&gt;|Requests a ticket renewable until the specified time|
|    -EndTime||&lt;DateTime&gt;|End time|
|    -RenewableOk||&lt;SwitchParam&gt;|Accepts a renewable ticket if the end time is over the limit|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|-S, -Spn||&lt;ServicePrincipalName&gt;|Service principal name to request ticket for|
|    -OutputFileName||&lt;String&gt;|Name of file to write ticket to|
|    -Overwrite||&lt;SwitchParam&gt;|Overwrites the output file, if it exists|
|    -Append||&lt;SwitchParam&gt;|Appends to the output file, if it exists|
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
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  UserName|
||||  UserRealm|
||||  TicketRealm|
||||  Spn|
||||  ServiceClass|
||||  Host|
||||  ServiceRealm|
||||  KdcOptions|
||||  EndTime|
||||  StartTime|
||||  RenewTill|
||||  EType|
||||  SessionKeyText|
||||  TicketEncryptionType|
||||  TgsrepHashcatMethod|
||||  TicketHash|
||||  IsCurrent|


## Details

  This command sends an AS-REQ to the KDC to request a ticket-granting ticket.
  
  The command line must include either a password or a hex-encoded key that is
  used both for preauthentication as well as to decrypt the response.  When
  specifying the NTLM hash, specify just the NTLM portion with no colon.
  
  The provided credential determines the encryption type of the response.  If you
  provide a password then all encryption types supported by Kerb asreq are
  presented.  To override this, use -EncTypes to specify a list of encryption
  types to accept.  Note that this only effects the encryption used in the
  response and not the preauthorization data.
  
  Dates/times are interpreted as local time unless otherwise specified.  If only
  a time is supplied, the assumed date is today.
  
  Note that the ticket flags and time parameters affect the request sent to the
  KDC.  However, the KDC is free to ignore them; specifying an option doesn't
  guarantee that the ticket will have the requested option.
  
  If you don't specify any options for the ticket, Kerb asreq uses default
  values, requesting a ticket that expires 10 hours from now with the options
  Canonicalize, RenewableOk, Renewable, and Forwardable.  If any options are
  specified, then no default values are applied and only the options specified
  are used.
  
  

## Examples

### Example 1 - Requesting a TGT with a password

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
### Example 2 - Requesting a TGT with a password request Rc4Hmac

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Rc4Hmac -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
### Example 3 - Requesting a TGT with a password request AES 128 or AES 256

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96 -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
### Example 4 - Requesting a TGT with an NTLM Hash

```
Kerb asreq -UserName milchick -NtlmHash B406A01772D0AD225D7B1C67DD81496F -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
### Example 5 - Requesting a TGT with an AES 128 key

```
Kerb asreq -UserName milchick -Aes c5673764957bc2839e367ba7b82f32e1 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
### Example 6 - Requesting a TGT with an AES 256 key

```
Kerb asreq -UserName milchick -Aes 76332deee4296dcb20200888630755268e605c8576e50ff38db2d8b92351f4e4 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
# Kerb getasinfo
  Gets server time and encryption types (with salts) for a user account.

## Synopsis
```
Kerb getasinfo [options] -UserName <String> -Realm <String> -Kdc <String>
```

## Options


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|-U, -UserName||&lt;String&gt;|Name of user (no domain)|
|-R, -Realm||&lt;String&gt;|Name of realm (domain)|
|-K, -Kdc||&lt;String&gt;|Host name or address of KDC|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|-O, -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  EType|
||||  SaltText|


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
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


## Details

  This command sends an AS-REQ to the KDC for a user and checks the response. 
  Typically, the KDC response with an error indicating that preauthentication is
  required along with its time and valid encryption wypes for the specified
  account.  This command analyzes that error response and prints the information.
  
  
  If the account does not exist or the realm name is wrong, the KDC returns an
  error indicating this and does not provide preauthentication info.
  
  If the user exists but does not require preauthentication, the KDC will instead
  reply with a TGT without providing encryption types.  In that case, use the
  requesttgt command to analyze the ticket.
  
# Kerb select
  Selects and displays tickets from a file.

## Synopsis
```
Kerb select [options] <From>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;From&gt;||&lt;String[]&gt;|File names or patterns|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|-I, -Into||&lt;String&gt;|Target file name|
|    -Current||&lt;SwitchParam&gt;|Only select tickets currently valid|
|    -MatchingUserName||&lt;String[]&gt;|Regex of user name to match|
|    -MatchingSpn||&lt;String[]&gt;|Regex of SPN to match|
|    -MatchingEncType||&lt;EType[]&gt;|Filter for encryption type|
||||**Possible values:**|
||||  Aes128CtsHmacSha1_96|
||||  Aes256CtsHmacSha1_96|
||||  Rc4Hmac|
||||  Rc4HmacExp|
||||  DesCbcMd5|
||||  DesCbcCrc|
|    -Overwrite||&lt;SwitchParam&gt;|Overwrites target file if it exists|
|-T, -TicketKey||&lt;HexString&gt;|Key used to decrypt the ticket|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  UserName|
||||  UserRealm|
||||  TicketRealm|
||||  Spn|
||||  ServiceClass|
||||  Host|
||||  ServiceRealm|
||||  KdcOptions|
||||  EndTime|
||||  StartTime|
||||  RenewTill|
||||  SessionKey|
||||  EType|
||||  SessionKeyText|
||||  TicketEncryptionType|
||||  TgsrepHashcatMethod|
||||  TicketHash|
||||  IsCurrent|


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
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


## Details

  This command reads tickets from one or more files (.kirbi or .ccache),
  optionally filters them, and optionally writes the results to another file
  (either .kirbi or .ccache).  It can be used to inspect files, convert files,
  combine files, or remove tickets from files.
  
  Specify the source files using -From.  You may specify multiple files and
  multiple wildcard patterns.  Kerb select reads all files from the tickets and
  applies any filters specified before printing the tickets to the screen.  If
  you specify -Into, the results are written to the file you specify.  Use
  -Overwrite to overwrite the outptu file if it already exists.
  

## Examples

### Example 1 - Print tickets from all mlichick*.kirbi files

```
Kerb select -From milchick*.kirbi
```
### Example 2 - Combine tickets from all mlichick*.kirbi files

```
Kerb select -From milchick*.kirbi -Into all-milchick.kirbi
```
### Example 3 - Print only current tickets from all mlichick*.kirbi files

```
Kerb select -From milchick*.kirbi -Current
```
### Example 4 - Print only TGTs

```
Kerb select -From milchick*.kirbi -MatchingSpn krbtgt/.*
```
### Example 5 - Print only tickets for CIFS

```
Kerb select -From milchick*.kirbi -MatchingSpn cifs/.*
```
### Example 6 - Print only tickets targeting LUMON-FS1

```
Kerb select -From milchick*.kirbi -MatchingSpn .*/LUMON-FS1
```
# Kerb tgsreq
  Requests a ticket from the KDC.

## Synopsis
```
Kerb tgsreq [options] -Kdc <String> -Tgt <String> -OutputFileName <String> <Targets>
```

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;Targets&gt;||&lt;ServicePrincipalName[]&gt;|SPNs to request tickets for|


## Options


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|-K, -Kdc||&lt;String&gt;|Host name or address of KDC|
|    -Tgt||&lt;String&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    -EncTypes||&lt;EType[]&gt;|Encryption types to request in response|
||||**Possible values:**|
||||  Aes128CtsHmacSha1_96|
||||  Aes256CtsHmacSha1_96|
||||  Rc4Hmac|
||||  Rc4HmacExp|
||||  DesCbcMd5|
||||  DesCbcCrc|
|-F, -Forwardable||&lt;SwitchParam&gt;|Requests a forwardable ticket|
|    -Proxiable||&lt;SwitchParam&gt;|Requests a forwardable ticket|
|    -Postdate||&lt;DateTime&gt;|Requests a postdated ticket with the specified start date|
|    -RenewTill||&lt;DateTime&gt;|Requests a ticket renewable until the specified time|
|    -EndTime||&lt;DateTime&gt;|End time|
|    -RenewableOk||&lt;SwitchParam&gt;|Accepts a renewable ticket if the end time is over the limit|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    -OutputFileName||&lt;String&gt;|Name of file to write ticket to|
|    -Overwrite||&lt;SwitchParam&gt;|Overwrites the output file, if it exists|
|-A, -Append||&lt;SwitchParam&gt;|Appends to the output file, if it exists|
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
|-H, -HumanReadable||&lt;SwitchParam&gt;|Formats file sizes as human-readable values|


|Name|Aliases|Value|Description|
|-|-|-|-|
|    -ConsoleOutputStyle||&lt;OutputStyle&gt;|Determines the output style|
|    -OutputFields||&lt;String[]&gt;|Fields to display in output|
||||**Possible values:**|
||||  UserName|
||||  UserRealm|
||||  TicketRealm|
||||  Spn|
||||  ServiceClass|
||||  Host|
||||  ServiceRealm|
||||  KdcOptions|
||||  EndTime|
||||  StartTime|
||||  RenewTill|
||||  SessionKey|
||||  EType|
||||  SessionKeyText|
||||  TicketEncryptionType|
||||  TgsrepHashcatMethod|
||||  TicketHash|
||||  IsCurrent|


## Details

  This command sends a TGS-REQ to the KDC to request a ticket.
  
  The command line must include either a password or a hex-encoded key that is
  used both for pre-authentication as well as to decrypt the response.  When
  specifying the NTLM hash, specify just the NTLM portion with no colon.
  
  By default, all supported encryption types are sent in the request.  To limit
  this, use the -EncTypes parameter to specify which encryption types to request
  from the server.
  

## Examples

### Example 1 - Requesting a ticket for SMB

```
Kerb tgsreq -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi
```
### Example 2 - Requesting a ticket for SMB and Host

```
Kerb tgsreq -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1, HOST/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi
```
