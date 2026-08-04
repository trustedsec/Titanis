# Domain Controller Notes
By default, Titanis connects to the dynamic TCP endpoint for the LSA service.  On domain controllers, this usually results in **ERROR_ACCESS_DENIED**.  To work around this, specify `-PreferSmb` to connect to the named pipe instead.

# Whoami
```
Lsa whoami lumon-fs1
```

The result is printed as <*domain*>\\<*user*>

# Lookup SID for Name
This command accepts domain users, local users, or well-known security principals.
```
Lsa lookupname lumon-dc1 milchick, administrator, system -PreferSmb
```

# Lookup Name for SID
```
Lsa lookupsid lumon-dc1 S-1-5-21-1718252288-3416168337-1457700507-1104, S-1-5-18 -PreferSmb
```

# Enumerate Policy Accounts
This command enumerates accounts for which a policy set.  This is not a list of all accounts on the computer.

```
Lsa enumaccounts lumon-dc1 -PreferSmb
```

# List Privileged Accounts
This command queries the policy with accounts that hold a specified privilege.  Only explicitly-specified privileges are concerned.  Group membership is not considered.
```
Lsa enumprivaccounts lumon-fs1 -Privilege SeBackupPrivilege
```

# Query Account Privileges
Queries the privileges assigned to a policy account.  Group membership is not considered.

## By Name
```
Lsa getprivs lumon-fs1 -ByName Administrator
```

## By SID
```
Lsa getprivs lumon-fs1 -BySid S-1-5-32-544
```

# Create a Policy Account
```
Lsa createaccount lumon-fs1 S-1-5-21-1718252288-3416168337-1457700507-1104
```

# Grant a Privilege
Before granting a privilege to a policy account, the policy account must exist.

Note that this command does not include system access rights, such as SeInteractiveLogonRight, SeNetworkLogonRight.  Use either `getsysaccess` or `getrights` for this.

## By Name
```
Lsa addpriv lumon-fs1 -Privileges SeBackupPrivilege -ByName milchick
```

## By SID
```
Lsa addpriv lumon-fs1 -Privileges SeBackupPrivilege -BySid S-1-5-21-1718252288-3416168337-1457700507-1104
```


# Revoke a Privilege

## By Name
```
Lsa rmpriv lumon-fs1 -Privileges SeBackupPrivilege -ByName milchick
```

## By SID
```
Lsa rmpriv lumon-fs1 -Privileges SeBackupPrivilege -BySid S-1-5-21-1718252288-3416168337-1457700507-1104
```

# Get System Access Rights

## By Name
```
Lsa getsysaccess lumon-fs1 -ByName milchick
```

## By SID
```
Lsa getsysaccess lumon-fs1 -BySid S-1-5-21-1718252288-3416168337-1457700507-1104
```

# Set System Access Rights
By default, this command adds the specified rights to the rights already granted to a policy account.  To remove other rights, specify `-Reset`.

## By Name
```
Lsa setsysaccess lumon-fs1 SeInteractiveLogonRight, SeNetworkLogonRight -ByName milchick
```

## By SID
```
Lsa setsysaccess lumon-fs1 SeInteractiveLogonRight, SeNetworkLogonRight -BySid S-1-5-21-1718252288-3416168337-1457700507-1104
```


## Reset Existing Rights
```
Lsa setsysaccess lumon-fs1 0 -Reset -ByName milchick
```

# Get User Rights
This command returns both privileges and system access rights granted to a policy account.

## By Name
```
Lsa getrights -ServerName lumon-fs1  -ByName milchick
```

## By SID
```
Lsa getrights -ServerName lumon-fs1  -BySid S-1-5-21-1718252288-3416168337-1457700507-1104
```
