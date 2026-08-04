# Enumerate Shares
```
Smb2Client enumshares lumon-fs1
```

# Query NICs
```
Smb2Client enumnics //lumon-fs1
```

# List a Directory
```
Smb2Client ls //lumon-fs1/admin$
```

## For a Single File
```
Smb2Client ls //lumon-fs1/admin$/explorer.exe
```

## For a Wildcard Pattern
```
Smb2Client ls //lumon-fs1/admin$/*.exe
```


## Recursion
Recursion is controlled by **-Depth**.  Either specify a maximum depth to recurse to, or specify **-1** for no depth limit.
```
Smb2Client ls //lumon-fs1/mdr -Depth -1
```

# List Named Pipes
```
Smb2Client ls //lumon-fs1/ipc$
```

# Watch a Directory for Changes

## Single Directory Level
```
Smb2Client watch //lumon-fs1/admin$
```

## Recursive
```
Smb2Client watch //lumon-fs1/admin$ -Recursive
```

## Watch for Pipes
Since named pipes are treated as file system, you can also watch for changes to named pipes, which may indicate services starting or stopping:
```
Smb2Client watch //lumon-fs1/ipc$
```

# Get Files

## Single File to Console
To print a single file, name the file but do not specify a destination.
```
Smb2Client get //lumon-fs1/admin$/win.ini
```

## Download Single File to Disk
To print a single file, name the file and specify a destination to save the file to.
```
Smb2Client get //lumon-fs1/admin$/win.ini local-win.ini
```

## Download Single File to Disk
To print a single file, name the file and specify a destination to save the file to.
```
Smb2Client get //lumon-fs1/admin$/win.ini local-win.ini
```
