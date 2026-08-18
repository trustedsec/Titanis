# Dsrep
Interacts with Directory Replication Service

## Synopsis
```
Dsrep <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[addsidhist](#dsrep-addsidhist)**|Adds SID history from one principal to another|
|**[attrmetadata](#dsrep-attrmetadata)**|Gets metadata for attribute link values|
|**[crackname](#dsrep-crackname)**|Cracks a name|
|**[cursors](#dsrep-cursors)**|Gets replication cursor info|
|**[dcinfo](#dsrep-dcinfo)**|Gets information on domain controllers|
|**[domains](#dsrep-domains)**|List domains in a forest|
|**[gcs](#dsrep-gcs)**|List GCs in a forest|
|**[kccfailures](#dsrep-kccfailures)**|Gets KCC failure information|
|**[neighbors](#dsrep-neighbors)**|Gets replication (repsFrom) neighbors|
|**[objmetadata](#dsrep-objmetadata)**|Gets object metadata|
|**[partitions](#dsrep-partitions)**|List partitions (naming contexts) in a forest|
|**[queue](#dsrep-queue)**|Gets pending replication operations|
|**[readngckey](#dsrep-readngckey)**|Gets the msDS-KeyCredentialLink on an object.|
|**[rep](#dsrep-rep)**|Requests replica changes|
|**[repnc](#dsrep-repnc)**|Replicates a naming context|
|**[repsto](#dsrep-repsto)**|Gets replication (repsTo) neighbors|
|**[roles](#dsrep-roles)**|List roles in a forest|
|**[sites](#dsrep-sites)**|List FSMO roles in a forest|
|**[utdvec](#dsrep-utdvec)**|Gets up-to-date vector info|
|**[writengckey](#dsrep-writengckey)**|Updates msDS-KeyCredentialLink on an object.|


For help on a subcommand, use `Dsrep <subcommand> -h`
# Dsrep addsidhist
Adds SID history from one principal to another

## Synopsis
**Dsrep addsidhist** [*options*] &lt;*ServerName*&gt; &lt;*SourceUser*&gt; &lt;*DestinationUser*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*SourceUser*&gt;||&lt;*UserPrincipalName*&gt;|Source user (must include domain)|
|&lt;*DestinationUser*&gt;||&lt;*UserPrincipalName*&gt;|Destination user (must include domain)|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-CheckSecure**||&lt;*SwitchParam*&gt;|Checks whether the channel is secure|
|    **-DeleteSource**||&lt;*SwitchParam*&gt;|Deletes the source object|
|    **-SourceAuthUser**||&lt;*UserPrincipalName*&gt;|User name to authenticate to source DC|
|    **-SourceDc**||&lt;*String*&gt;|Source domain controller|
|    **-SourcePassword**||&lt;*String*&gt;|User name to authenticate to source DC|


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

# Dsrep attrmetadata
Gets metadata for attribute link values

## Synopsis
**Dsrep attrmetadata** [*options*] &lt;*ServerName*&gt; [ &lt;*Domain*&gt; ] &lt;*Attribute*&gt; &lt;*Value*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Domain*&gt;||&lt;*LdapDistinguishedName*&gt;|Domain|
|&lt;*Attribute*&gt;||&lt;*String*&gt;|Attribute to retrieve|
|&lt;*Value*&gt;||&lt;*String*&gt;|Value to retrieve|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **AttributeName**|
||||  **ObjectDn**|
||||  **Data**|
||||  **DeletedTime**|
||||  **CreatedTime**|
||||  **Version**|
||||  **LastOriginatingChange**|
||||  **LastOriginatingDsaInvocationId**|
||||  **OriginatingChangeUsr**|
||||  **LocalChangeUsn**|
||||  **LastOriginatingDsaDn**|
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

# Dsrep crackname
Cracks a name

## Synopsis
**Dsrep crackname** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-ByCn**||&lt;*String[]*&gt;|Canonical name(s) to resolve|
|    **-ByDisplayName**||&lt;*String[]*&gt;|Display name(s) to resolve|
|    **-ByObjectDn**||&lt;*LdapDistinguishedName[]*&gt;|Object DN(s) to resolve|
|    **-ByObjectGuid**||&lt;*Guid[]*&gt;|Object GUID(s) to resolve|
|    **-BySamAccountName**||&lt;*String[]*&gt;|SAM account name(s) to resolve|
|    **-BySchemaGuid**||&lt;*Guid[]*&gt;|Schema GUID(s) to resolve|
|    **-BySid**||&lt;*String[]*&gt;|Security identifier(s) to resolve|
|    **-BySpn**||&lt;*String[]*&gt;|Service principal name(s) to resolve|
|    **-ByUpn**||&lt;*String[]*&gt;|User principal name(s) to resolve|
|    **-DesiredFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|


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
||||  **OfferedName**|
||||  **OfferedFormat**|
||||  **CrackedDomain**|
||||  **CrackedName**|
||||  **ResultFormat**|
||||  **Status**|
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

# Dsrep cursors
Gets replication cursor info

## Synopsis
**Dsrep cursors** [*options*] &lt;*ServerName*&gt; &lt;*Domain*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Domain*&gt;||&lt;*LdapDistinguishedName*&gt;|Domain DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-Level**||&lt;*DsrepCursorLevel*&gt;|Cursor info level|
||||Possible values:|
||||  **Cursor**|
||||  **Cursor2**|
||||  **Cursor3**|


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
||||  **SourceDsaInvocationId**|
||||  **AttributeFilterUsn**|
||||  **LastSyncSuccessTime**|
||||  **SourceDsa**|
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

# Dsrep dcinfo
Gets information on domain controllers

## Synopsis
**Dsrep dcinfo** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **NetbiosName**|
||||  **DnsHostName**|
||||  **SiteName**|
||||  **SiteObjectName**|
||||  **ComputerObjectName**|
||||  **ServerObjectName**|
||||  **NtdsDsaObjectName**|
||||  **IsPdc**|
||||  **IsDsEnabled**|
||||  **IsGc**|
||||  **SiteObjectGuid**|
||||  **ComputerObjectGuid**|
||||  **ServerObjectGuid**|
||||  **NtdsDsaObjectGuid**|
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

# Dsrep domains
List domains in a forest

## Synopsis
**Dsrep domains** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-NameFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|
|    **-Site**||&lt;*String*&gt;|Site to limit search to|


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

# Dsrep gcs
List GCs in a forest

## Synopsis
**Dsrep gcs** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-NameFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|


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

# Dsrep kccfailures
Gets KCC failure information

## Synopsis
**Dsrep kccfailures** [*options*]** -Kind** &lt;*DsrepKccFailureKind* &gt; &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-Kind**||&lt;*DsrepKccFailureKind*&gt;|Failure kind|
||||Possible values:|
||||  **Connect**|
||||  **Link**|


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
||||  **DsaDn**|
||||  **DsaObjectGuid**|
||||  **FirstFailureTime**|
||||  **FailureCount**|
||||  **LastResult**|
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

# Dsrep neighbors
Gets replication (repsFrom) neighbors

## Synopsis
**Dsrep neighbors** [*options*] &lt;*ServerName*&gt; [ &lt;*Domain*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Domain*&gt;||&lt;*LdapDistinguishedName*&gt;|Domain DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **NamingContext**|
||||  **NeighborDsaName**|
||||  **NeighborDsaAddress**|
||||  **AsyncIntersiteTransport**|
||||  **ReplicaFlags**|
||||  **NamingContextGuid**|
||||  **NeighborDsaObjectGuid**|
||||  **NeighborDsaInvocationId**|
||||  **AsyncIntersiteTransportObjectGuid**|
||||  **LastObjectChangeUsn**|
||||  **AttributeFilterUsn**|
||||  **LastSyncSuccessTime**|
||||  **LastSyntAttemptTime**|
||||  **LastSyncResult**|
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

# Dsrep objmetadata
Gets object metadata

## Synopsis
**Dsrep objmetadata** [*options*] &lt;*ServerName*&gt; &lt;*Object*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Object*&gt;||&lt;*LdapDistinguishedName*&gt;|Object DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-Level**||&lt;*DsrepObjectMetadataLevel*&gt;|Object metadata info level|
||||Possible values:|
||||  **Metadata**|
||||  **Metadata2**|


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
||||  **AttributeName**|
||||  **Version**|
||||  **DateTime**|
||||  **LastOriginatingDsaInvocationId**|
||||  **OriginatingChangeUsn**|
||||  **LocalChangeUsn**|
||||  **LastOriginatingDsa**|
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

# Dsrep partitions
List partitions (naming contexts) in a forest

## Synopsis
**Dsrep partitions** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-NameFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|


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

# Dsrep queue
Gets pending replication operations

## Synopsis
**Dsrep queue** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **EnqueuedTime**|
||||  **SerialNumber**|
||||  **Priority**|
||||  **OpType**|
||||  **Options**|
||||  **NamingContextDn**|
||||  **DsaDn**|
||||  **DsaAddress**|
||||  **NamingContextId**|
||||  **DsaId**|
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

# Dsrep readngckey
Gets the msDS-KeyCredentialLink on an object.

## Synopsis
**Dsrep readngckey** [*options*] &lt;*ServerName*&gt; &lt;*Account*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Account*&gt;||&lt;*LdapDistinguishedName[]*&gt;|Target account DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **Account**|
||||  **Key**|
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

# Dsrep rep
Requests replica changes

## Synopsis
**Dsrep rep** [*options*] &lt;*ServerName*&gt; [ &lt;*ObjectName*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*ObjectName*&gt;||&lt;*DsobjSpec[]*&gt;|DN, GUID, or SID of object to retrieve|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-ChunkObjectLimit**||&lt;*Int32*&gt;|Max number of objects per chunk (approx.)|
||||  Default: 1000|
|    **-ChunkSizeLimit**||&lt;*Int32*&gt;|Max bytes per chunk (approx.)|
||||  Default: 10485760|
|    **-ExportKeytab**||&lt;*FileSpec*&gt;|Name of keytab file to export to|
|    **-FromUsnvec**||&lt;*UsnVector*&gt;|Starting USN vector (as 48 hex bytes)|
|    **-Parallelize**||&lt;*Int32*&gt;|Number of parallel requests|
||||  Default: 1|


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

This command uses [MS-DRSR] to query attributes of an object by SID, GUID,
distinguished name, LDAP query, or object name.

In addition to the standard attributes defined by Active Directory, you may
query the special attributes kerberosKeys, kerberosOldKeys, or
cleartextPassword.  When one of these attributes is specified, Dsrep rep
implicitly queries supplementalCredentials and unpacks the credentials
contained within.

# Dsrep repnc
Replicates a naming context

## Synopsis
**Dsrep repnc** [*options*] &lt;*ServerName*&gt; [ &lt;*NamingContext*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*NamingContext*&gt;||&lt;*LdapDistinguishedName[]*&gt;|DN of naming contexts (partitions) to replicate|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-ChunkObjectLimit**||&lt;*Int32*&gt;|Max number of objects per chunk (approx.)|
||||  Default: 1000|
|    **-ChunkSizeLimit**||&lt;*Int32*&gt;|Max bytes per chunk (approx.)|
||||  Default: 10485760|
|    **-ExportKeytab**||&lt;*FileSpec*&gt;|Name of keytab file to export to|
|    **-FromUsnvec**||&lt;*UsnVector*&gt;|Starting USN vector (as 48 hex bytes)|


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

This command uses [MS-DRSR] to query attributes of an object by SID, GUID,
distinguished name, LDAP query, or object name.

In addition to the standard attributes defined by Active Directory, you may
query the special attributes kerberosKeys, kerberosOldKeys, or
cleartextPassword.  When one of these attributes is specified, Dsrep repnc
implicitly queries supplementalCredentials and unpacks the credentials
contained within.

# Dsrep repsto
Gets replication (repsTo) neighbors

## Synopsis
**Dsrep repsto** [*options*] &lt;*ServerName*&gt; [ &lt;*Domain*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Domain*&gt;||&lt;*LdapDistinguishedName*&gt;|Domain DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **NamingContext**|
||||  **NeighborDsaName**|
||||  **NeighborDsaAddress**|
||||  **AsyncIntersiteTransport**|
||||  **ReplicaFlags**|
||||  **NamingContextGuid**|
||||  **NeighborDsaObjectGuid**|
||||  **NeighborDsaInvocationId**|
||||  **AsyncIntersiteTransportObjectGuid**|
||||  **LastObjectChangeUsn**|
||||  **AttributeFilterUsn**|
||||  **LastSyncSuccessTime**|
||||  **LastSyntAttemptTime**|
||||  **LastSyncResult**|
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

# Dsrep roles
List roles in a forest

## Synopsis
**Dsrep roles** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-NameFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|


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

# Dsrep sites
List FSMO roles in a forest

## Synopsis
**Dsrep sites** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|
|    **-NameFormat**||&lt;*DsCrackNameResultFormat*&gt;|Format of name to print|
||||Possible values:|
||||  **StringSidName**|
||||  **UpnForLogon**|
||||  **Unknown**|
||||  **Fqdn1779**|
||||  **SamAccountName**|
||||  **DisplayName**|
||||  **UniqueIdName**|
||||  **CanonicalName**|
||||  **UserPrincipalName**|
||||  **CanonicalNameEx**|
||||  **ServicePrincipalName**|
||||  **SidOrSidHistory**|
||||  **DnsDomainName**|


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

# Dsrep utdvec
Gets up-to-date vector info

## Synopsis
**Dsrep utdvec** [*options*] &lt;*ServerName*&gt; &lt;*Domain*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Domain*&gt;||&lt;*LdapDistinguishedName*&gt;|Domain DN|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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
||||  **DsaGuid**|
||||  **HighPropertyUpdateUsn**|
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

# Dsrep writengckey
Updates msDS-KeyCredentialLink on an object.

## Synopsis
**Dsrep writengckey** [*options*] &lt;*ServerName*&gt; &lt;*Account*&gt; &lt;*KeyBytes*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*String*&gt;|RPC server to interact with|
|&lt;*Account*&gt;||&lt;*LdapDistinguishedName*&gt;|Target account DN|
|&lt;*KeyBytes*&gt;||&lt;*HexString*&gt;|Key, as a hex string|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Accept2003Deflate**||&lt;*SwitchParam*&gt;|Accept data compressed with Windows Server 2003 Deflate|
||||  Default: True|


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

