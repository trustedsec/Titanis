# Authentication Parameters

The Authentication parameter group defines parameters that specify how the tool is to authenticate with the target.  Most protocols exchange security tokens to build a security context to authenticate the user and provide message security services, such as signing and sealing.  The parameters specify how to build this security context.  Titanis supports the following security protocols:

* NTLM
* Kerberos
* SPNEGO

Some protocols require a specific security protocol.  For example, SMB2 requires SPNEGO, which itself may enclose both NTLM and Kerberos.  Some protocols, such as RPC, will accept a number of security protocols along with a field in the header that specifies how to interpret the tokens.  Titanis will use the provided parameters to build the appropriate type of security context.  If the parameters support multiple security protocols and the application protocol supports SPNEGO, Titanis prepares a security context for each supported security protocol and wraps them in an SPNEGO context.

In general, to use Kerberos, you must specify the KDC address with `-Kdc`.  Titanis will attempt to contact the KDC to request a ticket.


## Common Parameters

|Parameter|Description|
|-|-|
|`-Workstation <name>`|Name of the workstation to send along with the authentication request.  Windows uses this to evaluate logon restrictions and usually includes it in the event log record for the authentication request.|
|`-KdcPort <port>`|Port to connect to KDC, if different from 88|
|`-NtlmVersion <m.n.b.r>`|Version number to send in NTLM|

## Anonymous Authentication

### Supports
* NTLM

### Syntax
```
-Anonymous
```

To use anonymous authentication, specify the `-Anonymous` switch.  Anonymous authentication is only supported by NTLM.

## User Name and Password

### Supports
* NTLM
* Kerberos (all encryption profiles)

### Syntax
```
-UserName <user name> -Password <password> [ -Kdc <kdc address> ]
```

To authenticate with a user name and password, specify the user name and password with `-UserName` and `-Password` respectively.  Note that it is generally good practice to enclose the password in quotes so that thet shell does not interpret any of the special characters and mangle the password.

To specify the domain of the user, here are your options:
* Specify the domain with `-UserDomain`
* Include the domain as part of the username: `-UserName LUMON\milchick`
* Include the domain as part of the username: `-UserName milchick@lumon.ind`

To enable Kerberos, specify the address of the KDC with `-Kdc`.

## User Name and NTLM Hash

### Supports
* NTLM
* Kerberos (Rc4Hmac only)

### Syntax
```
-UserName <user name> -NtlmHash <hex string> [ -Kdc <kdc address> ]
```

To authenticate with a user name and the NTLM hash, specify the user name and hash with `-UserName` and `-NtlmHash` respectively.  Enter the hash as a hexadecimal string without a prefix or colon.

To enable Kerberos, specify the address of the KDC with `-Kdc`.  Titanis will attempt to authenticate using Rc4Hmac.  This may be blocked by domain policy.

## User Name and AES Key

### Supports
* Kerberos (Aes128 or Aes256 only)

### Syntax
```
-UserName <user name> -AesKey <hex string> -Kdc <kdc address>
```

To authenticate with a user name and AES key, specify the user name and key with `-UserName` and `-AesKey` respectively.  Enter the key as a hexadecimal string without a prefix or colon.  Titanis determines whether to use AES 128 or AES 256 based on the length of the key provided.

## Ticket-Granting Ticket

### Supports
* Kerberos (all encryption profiles)

### Syntax
```
-Tgt <ticket file> -Kdc <kdc address> [ -UserName <user name> ] [ -UserDomain <domain> ]
```

To authenticate with a TGT, specify the name of the file (either .kirbi or ccache) containing the TGT with `-Tgt` and the address of the KDC with `-Kdc`.  Titanis loads the tickets from `<ticket file>` and searches for a ticket with service class `krbtgt` and optionally filters the list of tickets that match `-UserName` and/or `-UserDomain`, if provided.  It then contacts the TGS to request a ticket for the target service.

## Pass-the-Ticket

### Supports
* Kerberos (all encryption profiles)

### Syntax
```
-Ticket <ticket file> [ -UserName <user name> ] [ -UserDomain <domain> ]
```

To authenticate with a ticket, specify the name of the file (either .kirbi or ccache) containing the ticket with `-Ticket`.  Titanis loads the tickets from `<ticket file>` and searches for a ticket matching the service class and host of the target service and optionally filters the list of tickets that match `-UserName` and/or `-UserDomain`, if provided.
