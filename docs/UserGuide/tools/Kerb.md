# Kerb
Commands for working with Kerberos authentication

## Synopsis
```
Kerb <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[asreq](#kerb-asreq)**|Requests a TGT from the KDC.|
|**[changepw](#kerb-changepw)**|Changes an account password|
|**[forge](#kerb-forge)**|Forges a ticket|
|**[getasinfo](#kerb-getasinfo)**|Gets server time and encryption types (with salts) for a user account.|
|**[keytab](#kerb-keytab)**|Display and edit keytab files|
|**[renew](#kerb-renew)**|Renews a ticket|
|**[s2k](#kerb-s2k)**|Generates a protocol key from a string, such as a password|
|**[select](#kerb-select)**|Selects and displays tickets from a file.|
|**[setpw](#kerb-setpw)**|Sets the password of (another) account|
|**[tgsreq](#kerb-tgsreq)**|Requests a ticket from the KDC.|


For help on a subcommand, use `Kerb <subcommand> -h`
# Kerb asreq
Requests a TGT from the KDC.

## Synopsis
**Kerb asreq** [*options*] &lt;*UserName*&gt; &lt;*Kdc*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*UserName*&gt;||&lt;*UserPrincipalName*&gt;|Name of user (no domain)|
|&lt;*Kdc*&gt;||&lt;*EndPoint*&gt;|Host name or address of KDC|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Target**||&lt;*SecurityPrincipalName[]*&gt;|SPNs to request ticket(s) for|
|    **-TicketComment**||&lt;*String*&gt;|Comment to associate with ticket|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AesKey**||&lt;*HexString*&gt;|AES 128 key|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing armor ticket|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-EncTypes**||&lt;*EType[]*&gt;|Encryption types to request in response|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-EndTime**||&lt;*DateTime*&gt;|End time|
|**-F**, **-Forwardable**||&lt;*SwitchParam*&gt;|Requests a forwardable ticket|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|**-N**, **-NtlmHash**||&lt;*HexString*&gt;|NTLM hash (hex-encoded, no colons)|
|    **-Password**||&lt;*String*&gt;|Password|
|    **-Postdate**||&lt;*DateTime*&gt;|Requests a postdated ticket with the specified start date|
|    **-Proxiable**||&lt;*SwitchParam*&gt;|Requests a proxiable ticket|
|    **-Realm**||&lt;*String*&gt;|Name of realm (domain)|
|    **-Renewable**||&lt;*SwitchParam*&gt;|Requests a renewable ticket|
|    **-RenewableOk**||&lt;*SwitchParam*&gt;|Accepts a renewable ticket if the end time is over the limit|
|    **-RenewTill**||&lt;*DateTime*&gt;|Requests a ticket renewable until the specified time (implies -Renewable)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|
|**-W**, **-Workstation**||&lt;*String*&gt;|Name of client workstation|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|**-S**, **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Append**||&lt;*SwitchParam*&gt;|Appends to the output file, if it exists|
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
|    **-OutputFileName**||&lt;*FileSpec*&gt;|Name of file to write ticket to|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrites the output file, if it exists|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


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

### Example 1 - Requesting a TGT with a user name / password

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 2 - Requesting a TGT with a UPN / password

```
Kerb asreq -UserName milchick@LUMON.IND -Password Br3@kr00m! -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 3 - Requesting a TGT with PKINIT

```
Kerb asreq -UserName milchick@LUMON.IND -UserCert milchick.pfx -UserKeyPassword password -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 4 - Requesting a TGT with a password request Rc4Hmac

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Rc4Hmac -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 5 - Requesting a TGT with a password request AES 128 or AES 256

```
Kerb asreq -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96 -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 6 - Requesting a TGT with an NTLM Hash

```
Kerb asreq -UserName milchick -NtlmHash B406A01772D0AD225D7B1C67DD81496F -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 7 - Requesting a TGT with an AES 128 key

```
Kerb asreq -UserName milchick -AesKey c5673764957bc2839e367ba7b82f32e1 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```

### Example 8 - Requesting a TGT with an AES 256 key

```
Kerb asreq -UserName milchick -AesKey 76332deee4296dcb20200888630755268e605c8576e50ff38db2d8b92351f4e4 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite
```
# Kerb changepw
Changes an account password

## Synopsis
**Kerb changepw** [*options*] &lt;*UserName*&gt; &lt;*Kdc*&gt; &lt;*NewPassword*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*UserName*&gt;||&lt;*UserPrincipalName*&gt;|Name of user (no domain)|
|&lt;*Kdc*&gt;||&lt;*EndPoint*&gt;|Host name or address of KDC|
|&lt;*NewPassword*&gt;||&lt;*String*&gt;|New password to set|


## Options


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|**-A**, **-AesKey**||&lt;*HexString*&gt;|AES 128 key|
|    **-DesKey**||&lt;*HexString*&gt;|DES key|
|    **-Keytab**||&lt;*FileSpec*&gt;|Name of keytab file|
|    **-NtlmHash**||&lt;*HexString*&gt;|NTLM hash (hex-encoded, no colons)|
|**-P**, **-Password**||&lt;*String*&gt;|Password|
|**-R**, **-Realm**||&lt;*String*&gt;|Name of realm (domain)|
|    **-UserCert**||&lt;*FileSpec*&gt;|Name of file containing user's certificate (for PKINIT)|
|    **-UserKey**||&lt;*FileSpec*&gt;|Name of file containing user's key (for PKINIT)|
|    **-UserKeyPassword**||&lt;*String*&gt;|Password to decrypt file containing user's key (for PKINIT)|
|**-W**, **-Workstation**||&lt;*String*&gt;|Name of client workstation|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|**-S**, **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


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


## Details

Kerb changepw uses the Kerberos Change Password protocol and can only be used
to change the password of the authenticating user.  To set the password of
another user, use the `setpw` command.

This protocol requires an initial ticket.  That is, it requires a ticket from
an ASREQ/ASREP exchange and not from a TGSREQ/TGSREP exchange.  Therefore, this
command requires credentials and does not accept a ticket as a parameter.  The
`setpw` command does not have this restriction and accepts a ticket as a
parameter.


## Examples

### Example 1 - milchick changing his own password

```
Kerb changepw milchick@LUMON 10.66.0.11 -Password EradicateFolly! Br3@kr00m!
```
# Kerb forge
Forges a ticket

## Synopsis
**Kerb forge** [*options*]** -TicketEType** &lt;*EType* &gt;** -ServerKey** &lt;*HexString* &gt;** -UserSid** &lt;*SecurityIdentifier* &gt;** -UserName** &lt;*UserPrincipalName* &gt; &lt;*Target*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Target*&gt;||&lt;*SecurityPrincipalName[]*&gt;|Target SPN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-DomainRids**||&lt;*UInt32[]*&gt;|Group RIDs, relative to the user domain|
|**-E**, **-ExtraSids**||&lt;*SecurityIdentifier[]*&gt;|Extra group SIDs|
|**-F**, **-FullName**||&lt;*String*&gt;|User's full name|
|    **-HomeDirectory**||&lt;*String*&gt;|UNC path of home directory|
|    **-HomeDrive**||&lt;*String*&gt;|Home Drive (e.g. H:)|
|    **-KdcEType**||&lt;*EType*&gt;|KDC key type|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-KdcKey**||&lt;*HexString*&gt;|Key to sign the ticket and PAC with|
|    **-LogonScript**||&lt;*String*&gt;|UNC path of logon script|
|    **-LogonServer**||&lt;*String*&gt;|Name of logon server|
|    **-PrimaryGroupId**||&lt;*UInt32*&gt;|Primary group ID|
|    **-ProfilePath**||&lt;*String*&gt;|UNC path of user profile|
|    **-Realm**||&lt;*String*&gt;|Ticket realm|
|    **-ResourceDomainSid**||&lt;*SecurityIdentifier*&gt;|Domain of SID containing resource|
|    **-ResourceGroupRids**||&lt;*UInt32[]*&gt;|Group RIDs, relative to the resource domain|
|    **-ServerKey**||&lt;*HexString*&gt;|Key of server to receive the ticket|
|    **-ServiceRealm**||&lt;*String*&gt;|Service realm|
|    **-TicketEType**||&lt;*EType*&gt;|Ticket encryption type|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-UserDomain**||&lt;*String*&gt;|Logon domain (NetBIOS) of the user|
|    **-UserName**||&lt;*UserPrincipalName*&gt;|User name|
|    **-UserRealm**||&lt;*String*&gt;|Logon domain (FQDN) of the user|
|    **-UserSid**||&lt;*SecurityIdentifier*&gt;|User SID|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|**-W**, **-Workstation**||&lt;*String*&gt;|Name of client workstation|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|**-A**, **-Append**||&lt;*SwitchParam*&gt;|Appends to the output file, if it exists|
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
|    **-OutputFileName**||&lt;*FileSpec*&gt;|Name of file to write ticket to|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrites the output file, if it exists|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Details

The forged ticket includes a PAC signed with 

# Kerb getasinfo
Gets server time and encryption types (with salts) for a user account.

## Synopsis
**Kerb getasinfo** [*options*] &lt;*UserName*&gt; &lt;*Kdc*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*UserName*&gt;||&lt;*UserPrincipalName*&gt;|Name of user (no domain)|
|&lt;*Kdc*&gt;||&lt;*String*&gt;|Host name or address of KDC|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|**-E**, **-EncTypes**||&lt;*EType[]*&gt;|ETypes to request|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|**-R**, **-Realm**||&lt;*String*&gt;|Name of realm (domain)|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|**-S**, **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


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
||||  **EType**|
||||  **SaltText**|
||||  **SaltHex**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


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


## Examples

### Example 1 - Get AS info for milchick

```
Kerb getasinfo milchick@LUMON 10.66.0.11
```
# Kerb keytab
Display and edit keytab files

## Synopsis
```
Kerb keytab <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[list](#kerb keytab-list)**|Lists the entries in a keytab file|


For help on a subcommand, use `Kerb keytab <subcommand> -h`
# Kerb renew
Renews a ticket

## Synopsis
**Kerb renew** [*options*] &lt;*Kdc*&gt; [ &lt;*TargetSpn*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Kdc*&gt;||&lt;*EndPoint*&gt;|Host name or address of KDC|
|&lt;*TargetSpn*&gt;||&lt;*SecurityPrincipalName[]*&gt;|SPNs to renew tickets for|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-TicketComment**||&lt;*String*&gt;|Comment to associate with ticket|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing armor ticket|
|**-E**, **-EndTime**||&lt;*DateTime*&gt;|End time|
|**-F**, **-Forwardable**||&lt;*SwitchParam*&gt;|Requests a forwardable ticket|
|    **-Postdate**||&lt;*DateTime*&gt;|Requests a postdated ticket with the specified start date|
|    **-Proxiable**||&lt;*SwitchParam*&gt;|Requests a proxiable ticket|
|    **-Renewable**||&lt;*SwitchParam*&gt;|Requests a renewable ticket|
|    **-RenewableOk**||&lt;*SwitchParam*&gt;|Accepts a renewable ticket if the end time is over the limit|
|    **-RenewTill**||&lt;*DateTime*&gt;|Requests a ticket renewable until the specified time (implies -Renewable)|
|    **-Ticket**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|**-W**, **-Workstation**||&lt;*String*&gt;|Name of client workstation|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|**-S**, **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Append**||&lt;*SwitchParam*&gt;|Appends to the output file, if it exists|
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
|    **-OutputFileName**||&lt;*FileSpec*&gt;|Name of file to write ticket to|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrites the output file, if it exists|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Details

This command sends a request to the TGS to renew the source ticket.  You may
provide the source ticket to renew either with -Ticket or -TicketCache.  For
-TicketCache, -TargetSpn is required; for -Ticket, -TargetSpn is optional.  If
you specify both -Ticket and -TicketCache, Kerb renew only loads source tickets
from -Ticket and only uses -TicketCache for output.

If you specify -TargetSpn with one or more SPNs, Kerb renew only renews tickets
matching one of the specified SPNs.



## Examples

### Example 1 - Renewing all tickets in a file

```
Kerb renew -Ticket milchick-lumon-fs1.kirbi 10.66.0.11 -OutputFileName milchick-lumon-fs1.kirbi -Overwrite
```

### Example 2 - Renewing tickets from cache

```
Kerb renew -TicketCache milchick.ccache 10.66.0.11 -TargetSpn host/lumon-fs1, cifs/lumon-fs1
```
# Kerb s2k
Generates a protocol key from a string, such as a password

## Synopsis
**Kerb s2k** [*options*] &lt;*Password*&gt; [ &lt;*Salt*&gt; ] [ &lt;*EncType*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Password*&gt;||&lt;*String*&gt;|String, such as the password|
|&lt;*Salt*&gt;||&lt;*String*&gt;|Salt as a string|
|&lt;*EncType*&gt;||&lt;*EType[]*&gt;|Encryption types to generate for|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ContinueOnError**||&lt;*SwitchParam*&gt;|Continue even if errors occur|


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
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
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
||||  **EType**|
||||  **KeyText**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Details

When authenticating with a password, Kerberos internally generates a protocol
key from the password and the accompanying salt using the String-to-key
function defined for each encryption profile.  For Windows domains, the salt
for a user account is usually the FQDN of the domain in uppercase followed by
the account name.  Specifically, the salt is composed of the domain and SAM
account name at the time of the last password is changed.  Therefore, if an
account has been renamed, the salt retains the old account name until the user
changes the password again.

NOTE: Be sure to read the above regarding salts.  Using the wrong salt has the
same effect as using the wrong password and may result in account lockout.

You may use `Kerb getasinfo` to get the salt for an account.

For more details, see [MS-KILE] § 3.1.1.2

The domain name used for the salt must be the FQDN of the domain, not the
shorter NetBIOS name.



## Examples

### Example 1 - Generate keys for milchick in domain LUMON.IND

```
Kerb s2k Br3@kr00m! LUMON.INDseth
```

### Example 2 - Generate AES keys for milchick in domain LUMON.IND

```
Kerb s2k Br3@kr00m! LUMON.INDseth -EncType Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96
```

### Example 3 - Generate keys for computer ALLENTOWN$ in domain LUMON.IND

```
Kerb s2k password LUMON.INDhostallentown.lumon.ind
```
# Kerb select
Selects and displays tickets from a file.

## Synopsis
**Kerb select** [*options*] [ &lt;*From*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*From*&gt;||&lt;*FileSpec[]*&gt;|File names or patterns|


## Options


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
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-Into**||&lt;*FileSpec*&gt;|Target file name|
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
||||  **SourceFileName**|
||||  **SeqNbr**|
||||  **Comment**|
||||  **ClientName**|
||||  **ClientRealm**|
||||  **TicketRealm**|
||||  **TargetSpn**|
||||  **ServiceClass**|
||||  **ServiceInstance**|
||||  **ServiceRealm**|
||||  **KdcOptions**|
||||  **EndTime**|
||||  **StartTime**|
||||  **RenewTill**|
||||  **AsrepKeyText**|
||||  **TicketKeyText**|
||||  **SupportedEncryptionTypes**|
||||  **SessionEType**|
||||  **SessionKeyText**|
||||  **TicketEType**|
||||  **TgsrepHashcatMethod**|
||||  **TicketHash**|
||||  **IsCurrent**|
||||  **SecurityGroups**|
||||  **NtlmHashText**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrites target file if it exists|
|**-P**, **-PrintAuthData**||&lt;*SwitchParam*&gt;|Prints ticket authorization data (if decrypted)|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### Ticket Decryption

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ServicePassword**||&lt;*String[]*&gt;|Password for service account|
|    **-ServiceSalt**||&lt;*String[]*&gt;|Salt for service account|
|    **-TicketKey**||&lt;*HexString[]*&gt;|Key to decrypt the ticket|


### Ticket Filter

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Current**||&lt;*SwitchParam*&gt;|Only select tickets currently valid|
|    **-InvertMatch**||&lt;*SwitchParam*&gt;|Invert match; select whatever doesn't match|
|    **-MatchingClientName**||&lt;*String[]*&gt;|Regex of client name to match|
|    **-MatchingSessionEType**||&lt;*EType[]*&gt;|Filter for session key encryption type|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-MatchingSpn**||&lt;*String[]*&gt;|Regex of SPN to match|
|    **-MatchingTicketEType**||&lt;*EType[]*&gt;|Filter for ticket encryption type|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-SeqNbr**||&lt;*NumberOrRange[]*&gt;|Seq. nbr. or range|


### Ticket Source

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|


## Details

This command reads tickets from one or more files (.kirbi or .ccache),
optionally filters them, and optionally writes the results to another file
(either .kirbi or .ccache).  It can be used to inspect files, convert files,
combine files, or remove tickets from files.

The command accepts both -TicketCache and -From to specify one or more files to
read tickets from.  If -From is specified, -TicketCache is ignored.  This is to
facilitate the use of $KRB5CCNAME.  If this environment variable is set, you
don't need to specify -From.  If you specify -From, this expresses your desire
to ignore the ticket cache.

Specify the source files using -From.  You may specify multiple files and
multiple wildcard patterns.  Kerb select reads all files from the tickets and
applies any filters specified before printing the tickets to the screen.  If
you specify -Into, the results are written to the file you specify.  Use
-Overwrite to overwrite the outptu file if it already exists.


## Examples

### Example 1 - Print tickets from all milchick*.ccache files

```
Kerb select -From milchick*.ccache
```

### Example 2 - Combine tickets from all milchick*.kirbi files

```
Kerb select -From milchick*.ccache -Into all-milchick.ccache
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

### Example 7 - Print only tickets #1, 3-5, 7+

```
Kerb select -From milchick*.kirbi -SeqNbr 1, 3-5, 7-*
```
# Kerb setpw
Sets the password of (another) account

## Synopsis
**Kerb setpw** [*options*] &lt;*TargetAccount*&gt; &lt;*NewPassword*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*TargetAccount*&gt;||&lt;*UserPrincipalName*&gt;|Optional name of account to set password of|
|&lt;*NewPassword*&gt;||&lt;*String*&gt;|New password to set|


## Options


### Authentication

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Anonymous**||&lt;*SwitchParam*&gt;|Uses anonymous login|
|    **-AuthProxy**||&lt;*EndPoint*&gt;|Endpoint of auth proxy|
|    **-Delegate**||&lt;*SwitchParam*&gt;|Requests delegation (sends TGT and key for Kerberos)|
|    **-NtlmHash**||&lt;*hexadecimal hash*&gt;|NTLM hash for NTLM authentication|
|    **-Password**|**-p**|&lt;*String*&gt;|Password to authenticate with|
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


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


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


## Details

Kerb setpw uses the Windows 2000 Kerberos Change Password protocol (RFC 3244)
and can be used to change the password of a user account that may or may not be
the same as the authenticating user.  This service does not require an initial
ticket and is more flexible than `changepw`.


## Examples

### Example 1 - milchick setting his own password

```
Kerb setpw -UserName milchick@LUMON -Kdc 10.66.0.11 -Password Br3@kr00m! milchick@lumon.ind EradicateFolly!
```

### Example 2 - milchick setting password for marks

```
Kerb setpw -UserName milchick@LUMON -Kdc 10.66.0.11 -Password Br3@kr00m! marks@lumon.ind SafelySituated
```
# Kerb tgsreq
Requests a ticket from the KDC.

## Synopsis
**Kerb tgsreq** [*options*] &lt;*Kdc*&gt; &lt;*Target*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Kdc*&gt;||&lt;*EndPoint*&gt;|Host name or address of KDC|
|&lt;*Target*&gt;||&lt;*SecurityPrincipalName[]*&gt;|SPN(s) to request ticket(s) for|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Forwarded**||&lt;*SwitchParam*&gt;|Requests a forwarded ticket|
|    **-Realm**||&lt;*String*&gt;|Realm of the KDC|
|    **-S4ProxyService**||&lt;*SecurityPrincipalName*&gt;|Name of service account with S4U2proxy|
|    **-S4UserName**||&lt;*UserPrincipalName*&gt;|Name of user to impersonate with S4U|
|    **-ServicePassword**||&lt;*String*&gt;|Password for service account (for decrypting authorization data)|
|    **-TicketComment**||&lt;*String*&gt;|Comment to associate with ticket|
|    **-U2uTicket**||&lt;*FileSpec*&gt;|Name of file containing U2U ticket|


### Authentication (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ArmorTicket**||&lt;*FileSpec*&gt;|Name of file containing armor ticket|
|    **-EncTypes**||&lt;*EType[]*&gt;|Encryption types to request in response|
||||Possible values:|
||||  **DesCbcMd5**|
||||  **DesCbcCrc**|
||||  **Rc4Hmac**|
||||  **Rc4HmacExp**|
||||  **Aes128CtsHmacSha1_96**|
||||  **Aes256CtsHmacSha1_96**|
||||  **DsaWithSha1**|
||||  **Md5WithRsa**|
||||  **Sha1WithRsa**|
||||  **Rc2Cbc**|
||||  **Rsa**|
||||  **RsaesOaep**|
||||  **DesEde3Cbc**|
|    **-EndTime**||&lt;*DateTime*&gt;|End time|
|    **-Forwardable**||&lt;*SwitchParam*&gt;|Requests a forwardable ticket|
|    **-Postdate**||&lt;*DateTime*&gt;|Requests a postdated ticket with the specified start date|
|    **-Proxiable**||&lt;*SwitchParam*&gt;|Requests a proxiable ticket|
|    **-Renewable**||&lt;*SwitchParam*&gt;|Requests a renewable ticket|
|    **-RenewableOk**||&lt;*SwitchParam*&gt;|Accepts a renewable ticket if the end time is over the limit|
|    **-RenewTill**||&lt;*DateTime*&gt;|Requests a ticket renewable until the specified time (implies -Renewable)|
|    **-S4UserCert**||&lt;*FileSpec*&gt;|Name of file containing a certificate of a user to impersonate with S4U|
|    **-Tgt**||&lt;*FileSpec*&gt;|Name of file containing a ticket-granting ticket (.kirbi or ccache)|
|    **-TicketCache**||&lt;*FileSpec*&gt;|Name of ticket cache file|
|**-W**, **-Workstation**||&lt;*String*&gt;|Name of client workstation|


### Connection

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-HostAddress**|**-ha**|&lt;*String[]*&gt;|Network address(es) of the server|
|    **-Socks5**||&lt;*host-or-ip:port*&gt;|End point of SOCKS 5 server to use|
|    **-UseTcp4Only**|**-4**|&lt;*SwitchParam*&gt;|Only use TCP over IPv4 endpoint|
|    **-UseTcp6Only**|**-6**|&lt;*SwitchParam*&gt;|Only use TCP over IPv6 endpoint|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Append**||&lt;*SwitchParam*&gt;|Appends to the output file, if it exists|
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
|    **-OutputFileName**||&lt;*FileSpec*&gt;|Name of file to write ticket to|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Overwrite**||&lt;*SwitchParam*&gt;|Overwrites the output file, if it exists|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


### Ticket Authorization Data (Kerberos)

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-AsrepKey**||&lt;*HexString*&gt;|Encryption key from AS-REP (for decryption NTLM hash)|
|    **-ServiceSalt**||&lt;*String*&gt;|Salt for service account (for decrypting authorization data)|


## Details

This command sends a TGS-REQ to the KDC to request a ticket.

The target may either be specified as a service principal name of the form
&lt;class&gt;/&lt;instance&gt; or as the name of the account itself.  For machine accounts,
the $ is optional.  For instance, instead of host/LUMON-FS1, you may simply use
LUMON-FS1$ or LUMON-FS1

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

### Example 2 - Requesting a ticket for LUMON-FS1

```
Kerb tgsreq -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi
```

### Example 3 - Requesting a ticket for SMB and Host

```
Kerb tgsreq -Kdc 10.66.0.11 -Tgt milchick-tgt.kirbi cifs/LUMON-FS1, HOST/LUMON-FS1 -OutputFile milchick-LUMON-FS1.kirbi
```

### Example 4 - Requesting a U2U ticket

```
Kerb tgsreq -Kdc 10.66.0.11 -v -Tgt allentown-tgt.kirbi -Overwrite -U2u allentown-tgt.kirbi -OutputFileName allentown-u2u.kirbi host/allentown
```

### Example 5 - Requesting a U2U ticket and extracting NTLM hash

```
Kerb tgsreq -Kdc 10.66.0.11 -v -Tgt allentown-tgt.kirbi -Overwrite -U2u allentown-tgt.kirbi -OutputFileName allentown-u2u.kirbi host/allentown -AsrepKey 82d4ab5873cbfda126e00c28edb5bd97b6451aa06a291d85173e6fc4ed4aacee
```
# Kerb keytab list
Lists the entries in a keytab file

## Synopsis
**Kerb keytab list** [*options*] &lt;*Keytab*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Keytab*&gt;||&lt;*String*&gt;|Name of keytab file|


## Options


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
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
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
||||  **Principal**|
||||  **Realm**|
||||  **Timestamp**|
||||  **Kvno**|
||||  **EType**|
||||  **KeyText**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|

