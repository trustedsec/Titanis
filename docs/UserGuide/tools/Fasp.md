# Fasp
Interacts with the Windows Firewall service

## Synopsis
```
Fasp <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[addrule](#fasp-addrule)**|Adds a firewall rule|
|**[enumrules](#fasp-enumrules)**|Enumerates firewall rules|
|**[query](#fasp-query)**|Queries firewall rules|


For help on a subcommand, use `Fasp <subcommand> -h`
# Fasp addrule
Adds a firewall rule

## Synopsis
**Fasp addrule** [*options*] &lt;*ServerName*&gt; &lt;*RuleName*&gt; &lt;*Action*&gt; &lt;*Direction*&gt; &lt;*Protocol*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|
|&lt;*RuleName*&gt;||&lt;*String*&gt;|Rule name|
|&lt;*Action*&gt;||&lt;*FirewallRuleAction*&gt;|Action performed by rule|
||||Possible values:|
||||  **Invalid**|
||||  **AllowBypass**|
||||  **Block**|
||||  **Allow**|
|&lt;*Direction*&gt;||&lt;*FirewallRuleDirection*&gt;|Traffic direction|
||||Possible values:|
||||  **In**|
||||  **Out**|
|&lt;*Protocol*&gt;||&lt;*IpProtocolNumber*&gt;|Protocol rule applies to|
||||Possible values:|
||||  **IP**|
||||  **IPv6HopByHopOptions**|
||||  **Icmp**|
||||  **Igmp**|
||||  **Ggp**|
||||  **IPv4**|
||||  **Tcp**|
||||  **Pup**|
||||  **Udp**|
||||  **Idp**|
||||  **IPv6**|
||||  **IPv6RoutingHeader**|
||||  **IPv6FragmentHeader**|
||||  **IPSecEncapsulatingSecurityPayload**|
||||  **IPSecAuthenticationHeader**|
||||  **IcmpV6**|
||||  **IPv6NoNextHeader**|
||||  **IPv6DestinationOptions**|
||||  **ND**|
||||  **Raw**|
||||  **Unspecified**|
||||  **Ipx**|
||||  **Spx**|
||||  **SpxII**|
||||  **Any**|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-Description**||&lt;*String*&gt;|Rule description|
|    **-InterfaceId**||&lt;*Guid[]*&gt;|Interface ID(s)|
|    **-InterfaceType**||&lt;*InterfaceTypes[]*&gt;|Interface types|
||||Possible values:|
||||  **All**|
||||  **Lan**|
||||  **Wireless**|
||||  **RemoteAccess**|
|    **-LocalPorts**||&lt;*NumberOrRange[]*&gt;|Local ports|
|    **-Profiles**||&lt;*FirewallProfiles[]*&gt;|Firewall profiles rule applies to|
||||Possible values:|
||||  **Invalid**|
||||  **Domain**|
||||  **Standard**|
||||  **Private**|
||||  **Public**|
||||  **All**|
||||  **Current**|
||||  **None**|
|    **-RemotePorts**||&lt;*NumberOrRange[]*&gt;|Remote ports|
|    **-RuleId**||&lt;*String*&gt;|Rule ID|
|    **-Store**||&lt;*FirewallStoreType*&gt;|Rule store to add to|
||||Possible values:|
||||  **Invalid**|
||||  **GroupPolicyRsop**|
||||  **Local**|
||||  **Dynamic**|
||||  **GroupPolicy**|
||||  **Defaults**|


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

# Fasp enumrules
Enumerates firewall rules

## Synopsis
**Fasp enumrules** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ProfileFilter**||&lt;*FirewallProfiles[]*&gt;|Profile filters|
||||Possible values:|
||||  **Invalid**|
||||  **Domain**|
||||  **Standard**|
||||  **Private**|
||||  **Public**|
||||  **All**|
||||  **Current**|
||||  **None**|
|    **-StatusFilter**||&lt;*FirewallRuleStatus[]*&gt;|Status filters|
||||Possible values:|
||||  **None**|
||||  **Ok**|
||||  **PartiallyIgnored**|
||||  **Ignored**|
||||  **ParsingError**|
||||  **SemanticError**|
||||  **RuntimeError**|
||||  **Error**|
||||  **All**|
|    **-Store**||&lt;*FirewallStoreType*&gt;|Rule store to enumerate|
||||Possible values:|
||||  **Invalid**|
||||  **GroupPolicyRsop**|
||||  **Local**|
||||  **Dynamic**|
||||  **GroupPolicy**|
||||  **Defaults**|


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
||||  **RuleId**|
||||  **SchemaVersion**|
||||  **Name**|
||||  **Description**|
||||  **Action**|
||||  **Profiles**|
||||  **Direction**|
||||  **IpProtocol**|
||||  **LocalPortList**|
||||  **RemotePortList**|
||||  **LocalApplication**|
||||  **LocalService**|
||||  **Interfaces**|
||||  **InterfaceTypes**|
||||  **Status**|
||||  **RemoteMachines**|
||||  **RemoteUsers**|
||||  **EmbeddedContext**|
||||  **Origin**|
||||  **GpoName**|
||||  **Flags**|
||||  **IcmpTypeCodeList**|
||||  **Metadata**|
||||  **EnforcementStates**|
||||  **LocalUserAuthorizationList**|
||||  **PackageId**|
||||  **LocalUserOwner**|
||||  **TrustTupleKeywords**|
||||  **OnNetworkNames**|
||||  **SecurityRealmId**|
||||  **RemoteOutServerNames**|
||||  **FullyQualifiedBinaryName**|
||||  **CompartmentId**|
||||  **ProviderContextKey**|
||||  **RemoteDynamicKeywordAddresses**|
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

# Fasp query
Queries firewall rules

## Synopsis
**Fasp query** [*options*] &lt;*ServerName*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ServerName*&gt;||&lt;*ServerSpec[]*&gt;|RPC server to interact with|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ByAppPath**||&lt;*String*&gt;|App path to query for|
|    **-ByDirection**||&lt;*FirewallRuleDirection*&gt;|Direction to query for|
||||Possible values:|
||||  **In**|
||||  **Out**|
|    **-ByGroup**||&lt;*String*&gt;|Group to query for|
|    **-ByLocalPort**||&lt;*UInt16*&gt;|Local port to query for|
|    **-ByProfile**||&lt;*FirewallProfiles[]*&gt;|Rule profile to query for|
||||Possible values:|
||||  **Invalid**|
||||  **Domain**|
||||  **Standard**|
||||  **Private**|
||||  **Public**|
||||  **All**|
||||  **Current**|
||||  **None**|
|    **-ByProtocol**||&lt;*IpProtocolNumber*&gt;|Protocol to query for|
||||Possible values:|
||||  **IP**|
||||  **IPv6HopByHopOptions**|
||||  **Icmp**|
||||  **Igmp**|
||||  **Ggp**|
||||  **IPv4**|
||||  **Tcp**|
||||  **Pup**|
||||  **Udp**|
||||  **Idp**|
||||  **IPv6**|
||||  **IPv6RoutingHeader**|
||||  **IPv6FragmentHeader**|
||||  **IPSecEncapsulatingSecurityPayload**|
||||  **IPSecAuthenticationHeader**|
||||  **IcmpV6**|
||||  **IPv6NoNextHeader**|
||||  **IPv6DestinationOptions**|
||||  **ND**|
||||  **Raw**|
||||  **Unspecified**|
||||  **Ipx**|
||||  **Spx**|
||||  **SpxII**|
||||  **Any**|
|    **-ByRemotePort**||&lt;*UInt16*&gt;|Remote port to query for|
|    **-ByRuleId**||&lt;*String*&gt;|Rule ID to query for|
|    **-ByService**||&lt;*String*&gt;|Service to query for|
|    **-ByStatus**||&lt;*FirewallRuleStatus[]*&gt;|Rule status to query for|
||||Possible values:|
||||  **None**|
||||  **Ok**|
||||  **PartiallyIgnored**|
||||  **Ignored**|
||||  **ParsingError**|
||||  **SemanticError**|
||||  **RuntimeError**|
||||  **Error**|
||||  **All**|
|**-M**, **-MatchType**||&lt;*QueryMatchType*&gt;|Match type|
||||Possible values:|
||||  **TrafficMatch**|
||||  **Equal**|


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
||||  **RuleId**|
||||  **SchemaVersion**|
||||  **Name**|
||||  **Description**|
||||  **Action**|
||||  **Profiles**|
||||  **Direction**|
||||  **IpProtocol**|
||||  **LocalPortList**|
||||  **RemotePortList**|
||||  **LocalApplication**|
||||  **LocalService**|
||||  **Interfaces**|
||||  **InterfaceTypes**|
||||  **Status**|
||||  **RemoteMachines**|
||||  **RemoteUsers**|
||||  **EmbeddedContext**|
||||  **Origin**|
||||  **GpoName**|
||||  **Flags**|
||||  **IcmpTypeCodeList**|
||||  **Metadata**|
||||  **EnforcementStates**|
||||  **LocalUserAuthorizationList**|
||||  **PackageId**|
||||  **LocalUserOwner**|
||||  **TrustTupleKeywords**|
||||  **OnNetworkNames**|
||||  **SecurityRealmId**|
||||  **RemoteOutServerNames**|
||||  **FullyQualifiedBinaryName**|
||||  **CompartmentId**|
||||  **ProviderContextKey**|
||||  **RemoteDynamicKeywordAddresses**|
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

