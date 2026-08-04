# Whoami
The Active Directory implementation provides a few ways to get information about your identity.

## LDAP Whoami
```
Ldap whoami lumon-dc1
```

## RootDSE Query
This query returns your user name in <*domain*>\\<*user*> syntax, along with a list of your groups as SIDs.  Note that this list is based on your Kerberos ticket, not on the contents of the directory.
```
Ldap query lumon-dc1 -SearchBase ""  -ConsoleOutputStyle List -OutputFields msDS-PrincipalName, tokenGroups
```

# Search for a User

```
Ldap search lumon-dc1 milchick
```

Connects to **lumon-dc1** to search for objects with a name-like field beginning with **milchick**

# Accounts with Stale Passwords
To find accounts with a password that hasn't changed within the last 60 days:

```
Ldap query lumon-dc1 -OutputFields pwdLastSet  '(pwdLastSet<=Today-60d)'
```

# Query SPN Mappings
```
Ldap query lumon-dc1 '(&(!(userAccountControl|=Disabled))(servicePrincipalName=*))' -OutputField samAccountName, servicePrincipalName
```

# Accounts with RBCD
```
Ldap query lumon-dc1 '(&(msDS-AllowedToDelegateTo=*)(userAccountControl&=TrustedForS4U)(!(userAccountControl|=Disabled)))' -OutputFields samAccountName, msDS-AllowedToDelegateTo
```

# Query Transitive Group Members

## By Group SID
This command queries all transitive (nested) members of **Administrators** by its well-known SID.
```
Ldap query lumon-dc1 '(memberOf*=<SID=S-1-5-32-544>)'
```
## By Group DN
This command queries all transitive (nested) members of **Administrators** by its DN.
```
Ldap query lumon-dc1 '(memberOf*=CN=Administrators,CN=Builtin,DC=corp,DC=lumon,DC=ind)'
```

# Add/Remove SPN Mapping

## Add
```
Ldap moduser lumon-dc1 "CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind" servicePrincipalName+=host/testuser
```

## Remove
```
Ldap moduser lumon-dc1 "CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind" servicePrincipalName-=host/testuser
```

# Add Certificate Credential
In this example, the certificate is in a file named **milchick.cer**.
```
Ldap moduser lumon-dc1 "CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind" userCertificate:file+=milchick.cer
```

# Change or Reset Password
Changing and resetting a password differ in that changing a password requires you to specify the old password with **-OldPassword**.

## Change
If you know the old password:
```
Ldap moduser lumon-dc1 "CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind" -OldPassword 'newuserpassword' -NewPassword 'The work is mysterious and important!'
```

## Remove
If you don't know the old password:
```
Ldap moduser lumon-dc1 "CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind" -NewPassword 'The work is mysterious and important!'
```

# Add Member to Group
To add a member to a group, you must first have the DN of the group and the member.
```
Ldap mod lumon-dc1 "CN=Administrators,CN=Builtin,DC=corp,DC=lumon,DC=ind" 'member+=CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind'
```

# Query Membership Expiration
To include the TTL in a query, specify **-LinkTtl** and query the membership field (either **member** or **memberOf**).  Titanis prints the TTL returned by the server as part of the field.

## By User
```
$ Ldap search lumon-dc1 testuser -LinkTtl -OutputFields samAccountName, memberOf

sAMAccountName  memberOf
--------------  --------------------------------------------------------------
testuser        <TTL=486>,CN=Administrators,CN=Builtin,DC=corp,DC=lumon,DC=ind
```

## By Group
```
$ Ldap search lumon-dc1 Administrators -LinkTtl -OutputFields samAccountName, member

sAMAccountName  member
--------------  ---------------------------------------------------------------------------
Administrators  <TTL=365>,CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind
Administrators  CN=Domain Admins,CN=Users,DC=corp,DC=lumon,DC=ind
Administrators  CN=Enterprise Admins,CN=Users,DC=corp,DC=lumon,DC=ind
Administrators  CN=Administrator,CN=Users,DC=corp,DC=lumon,DC=ind
```


# Add Expiring Group Membership
To add a member to a group, you must first have the DN of the group and the member.  Specify the timeout after **TTL=** in seconds.
```
Ldap mod lumon-dc1 "CN=Administrators,CN=Builtin,DC=corp,DC=lumon,DC=ind" 'member+=<TTL=10,CN=testuser,OU=Severed Floor,OU=Kier\, PE,DC=corp,DC=lumon,DC=ind>'
```

If the user is already a member of the group, the existing membership is set to expire.  You can effectively delete an existing membership by setting TTL=1.

