# Get DC Info
The rootDSE contains useful information about a domain controller.

```
Ldap query lumon-dc1 -SearchBase "" -ConsoleOutputStyle List -OutputFields ...
```

| Attribute | Description | Example |
|-|-|-|
| configurationNamingContext | DN of configuration NC | CN=Configuration,DC=corp,DC=lumon,DC=ind |
| defaultNamingContext | Domain hosted by DC | DC=corp,DC=lumon,DC=ind |
| domainControllerFunctionality | Domain functional level | 10 |
| domainFunctionality | Domain functional level | 10 |
| forestFunctionality | Forest functional level | 10 |
| isGlobalCatalogReady | Indicates a global catalog server | True |
| ldapServiceName | Service account name | corp.lumon.ind:lumon-dc1$@CORP.LUMON.IND |
| rootDomainNamingContext | Forest root | DC=corp,DC=lumon,DC=ind |
| schemaNamingContext | Schema root | CN=Schema,CN=Configuration,DC=corp,DC=lumon,DC=ind |

## Functional Levels
The functional levels determine whether certain schema attributes are available, and whether certain authentication features (like S4U) are available.

## Naming Contexts
A *naming context* is also referred to as a *partition*.  These attributes describe the topology of the forest.

#Domain Info
```
Ldap query lumon-dc1 -SearchBase DomainRoot -Scope Base -OutputFields \* -ConsoleOutputStyle List
```


# List Partitions
To get a list of partitions, including a list of all domains and DNS zones in the forest:
```
Ldap lspart lumon-dc1
```

The output includes the DN of the NC roots.

# Enumerate DNS Records
```
Ldap query lumon-dc1 '(dnsRecord=*)' -OutputFields EntryName, dc, dnsRecord
```
Titanis decodes the DNS record data and prints it in a human-readable form.

# List Sites
```
Ldap query lumon-dc1 -SearchBase ConfigRoot '(objectClass=site)'
```

# List Subnets
```
Ldap query lumon-dc1 -SearchBase ConfigRoot '(objectClass=subnet)' -OutputFields name
```

The name of each subnet object is the CIDR notation for the subnet it describes.
