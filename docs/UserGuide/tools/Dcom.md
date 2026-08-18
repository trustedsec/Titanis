# Dcom
Utility for working with DCOM

## Synopsis
```
Dcom <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[activate](#dcom-activate)**|Activates an object over DCOM|
|**[invoke](#dcom-invoke)**|Invokes a method on an OLE automation object over DCOM|


For help on a subcommand, use `Dcom <subcommand> -h`
# Dcom activate
Activates an object over DCOM

## Synopsis
**Dcom activate** [*options*] &lt;*ServerName*&gt; &lt;*Clsid*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|Name of the server to connect to|
|&lt;*Clsid*&gt;||&lt;*Guid*&gt;|CLSID of object to activate|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-FileName**||&lt;*String*&gt;|Name of file to activate|


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

# Dcom invoke
Invokes a method on an OLE automation object over DCOM

## Synopsis
**Dcom invoke** [*options*] &lt;*ServerName*&gt; &lt;*Clsid*&gt; &lt;*MethodName*&gt; [ &lt;*Arguments*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|Name of the server to connect to|
|&lt;*Clsid*&gt;||&lt;*Guid*&gt;|CLSID of object to activate|
|&lt;*MethodName*&gt;||&lt;*String*&gt;|Name of method to invoke|
|&lt;*Arguments*&gt;||&lt;*String[]*&gt;|Arguments to pass to the method|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-FileName**||&lt;*String*&gt;|Name of file to activate|


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


## Details

Dcom invoke activates the object with the specified CLSID and attempts to
invoke the specified method.  The arguments are not parsed locally and are all
passed as strings.  It is up to the server to coerce them to the correct type. 
Most implementations rely on OLE automation to do this.

The -MethodName may specify either a method or a property.  If it is a
property, the value of the property is retrieved and printed.

If the method is specified as a dot-separated multi-part name, this is
interpreted as a property path.  The properties are retrieved one by one.  The
last part is interpreted as the actual name of the method to invoke on the
resulting object.


## Examples

### Example 1 - Invoke MMC20 ExecuteShellCommand

```
Dcom invoke LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! 49B2791A-B1AE-4C90-9B8E-E860BA07F889 Document.ActiveView.ExecuteShellCommand "cmd.exe" C:\ " /c whoami" ""
```
The CLSID corresponds to MMC20.Application.  This object is activated, then the
properties Document and retrieved ActiveView, and finally ExecuteShellCommand
is executed on the ActiveView object.

### Example 2 - Invoke MMC20 ExecuteShellCommand with FQDN

```
Dcom invoke LUMON-FS1.lumon.ind -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 49B2791A-B1AE-4C90-9B8E-E860BA07F889 Document.ActiveView.ExecuteShellCommand "cmd.exe" C:\ " /c whoami" ""
```
The CLSID corresponds to MMC20.Application.  This object is activated, then the
properties Document and retrieved ActiveView, and finally ExecuteShellCommand
is executed on the ActiveView object.
