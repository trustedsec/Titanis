# Enumerate Users
```
Sam enumusers -ServerName lumon-fs1
```

# Enumerate Groups and Aliases
While the user interface makes no distinction, to the SAM, there is a distinction between aliases and groups.  Built-in "groups", such as Administrators, are actually aliases.  On a local system, locally created groups are also aliases.  On a domain controller, domain-local groups are actually aliases, while global and universal groups are treated as groups to the SAM.

## Aliases
```
Sam enumaliases lumon-fs1
```

## Groups
```
Sam enumgroups lumon-fs1
```

