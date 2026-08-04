These commands generally require privileged access, either as an administrator or backup operator.

# Get Syskey
Prints the syskey as hex to stdout.
```
Reg syskey -ServerName lumon-fs1
```

# Dump SAM
This command gets a list of user accounts with their NTLM hashes.
```
Reg dumpsam lumon-fs1 -BackupSemantics
```

# Dump LSA Secrets
Most secrets are printed as hex-encoded strings.  Some of them are known to be text, such as $MACHINE.ACC and service account passwords.  Titanis decodes these as strings and prints them as *INFO* messages.  Since the returned strings are quite long, it is best to print the output using the *List* style.
```
Reg dumplsasecrets lumon-fs1 -BackupSemantics -ConsoleOutputStyle List
```
