Some parameters are common to several Titanis commands.  For example, all commands accept parameters affecting the format of the output.  Commands requiring authentication support the same arguments.  For some of these parameters, especially those for authentication, you will frequently use the same value, resulting in repitition across command lines.

To simplify this, Titanis allows you to specify the default value of these paramaters by setting environment variables.  This means you don't have to repeatedly specify the parameter in every command line, and you avoid the risk of mistyping the parameter.

To specify the default value for any parameter, set an environment variable with a name of the form `TITANIS_DEFAULT_<name>`, replacing `<name>` with the name of the parameter in all caps.

For example, to set authentication parameter and use them with `Smb2Client enumshares`:
```
milchick@COBEL-WKS:~$ export TITANIS_DEFAULT_USERNAME=milchick@LUMON TITANIS_DEFAULT_KDC=LUMON-DC1 TITANIS_DEFAULT_PASSWORD=Br3@kr00m\!
milchick@COBEL-WKS:~$ Smb2Client enumshares LUMON-DC1
 INFO: Importing default for 'Kdc': LUMON-DC1
 INFO: Importing default for 'Password': Br3@kr00m!
 INFO: Importing default for 'UserName': milchick@LUMON
 INFO: Smb2Client Version 0.9.20260626111734.Q

Name      Type              Remark               Permissions  Max. Uses  Current Uses  Path                                             Password  Sec. Desc.
--------  ----------------  -------------------  -----------  ---------  ------------  -----------------------------------------------  --------  --------------------------------------------------
ADMIN$              Hidden  Remote Admin                None         -1             0  C:\WINDOWS
C$                  Hidden  Default share               None         -1             0  C:\
IPC$      TypeMask, Hidden  Remote IPC                  None         -1             2
NETLOGON              Disk  Logon server share          None         -1             0  C:\WINDOWS\SYSVOL\sysvol\corp.lumon.ind\SCRIPTS            O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)
SYSVOL                Disk  Logon server share          None         -1             1  C:\WINDOWS\SYSVOL\sysvol                                   O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)(A;;FA;;;AU)
```

After parsing the command line, Titanis checks for environment variables matching any of the parameters that weren't specified on the command line.  For any matching variables, Titanis prints a message informing you that the parameter was set to this value.  This aides in logging.

Since Titanis only imports defaults for parameters that are not specified on the command line, any parameter you specify on the command line will override this default.  For example:

```
milchick@COBEL-WKS:~$ Smb2Client enumshares LUMON-DC1 -UserName marks@LUMON -Password She\'s@live\!\!
 INFO: Importing default for 'Kdc': LUMON-DC1
 INFO: Smb2Client Version 0.9.20260626111734.Q

 WARN: Field retrieving info for level Level502: ERROR_ACCESS_DENIED (0x00000005): Access is denied. (code=0x00000005)
Name      Type              Remark               Permissions  Max. Uses  Current Uses  Path  Password  Sec. Desc.
--------  ----------------  -------------------  -----------  ---------  ------------  ----  --------  ----------
ADMIN$              Hidden  Remote Admin                None          0             0
C$                  Hidden  Default share               None          0             0
IPC$      TypeMask, Hidden  Remote IPC                  None          0             0
NETLOGON              Disk  Logon server share          None          0             0
SYSVOL                Disk  Logon server share          None          0             0
```

In the above example, the command line specifies the user name and password for `marks`, so Titanis does not use the environment variables.  Since `-Kdc` is not specified on the command line, it still imports the default.

# Command-Specific Options
To specify options that are always included in the invocation of a command, set an environment variable with a name of the form `<command>_OPTIONS`, where '<command>' is the name of the command in all caps.  For subcommands, use the pattern `<command>_<subcommand>_OPTIONS`.  When a command is invoked, Titanis checks for an environment variable matching this pattern and if found, the value is tokenized and the arguments are prepended to the command line.

For example, to set default options for `Smb2Client enumshares` to `-vv`, set `SMB2CLIENT_ENUMSHARES_OPTIONS=-vv`.

```
milchick@COBEL-WKS:~$ export SMB2CLIENT_ENUMSHARES_OPTIONS=-vv
milchick@COBEL-WKS:~$ Smb2Client enumshares LUMON-DC1
 INFO: Using options from environment SMB2CLIENT_ENUMSHARES_OPTIONS: -vv
 INFO: Importing default for 'Kdc': LUMON-DC1
 INFO: Importing default for 'Password': Br3@kr00m!
 INFO: Importing default for 'UserName': milchick@LUMON
 INFO: Smb2Client Version 0.9.20260626111734.Q

[Smb2Client] DIAG: The Titanis Smb2Client is connecting to \\LUMON-DC1 at Unspecified/LUMON-DC1:445
        Client GUID  : 2b4e2c09-ee32-4bd6-b211-e45485b05b79
        Capabilities : Dfs, Leasing, LargeMtu, MultiChannel, PersistentHandles, DirectoryLeasing, Encryption
        Security mode: SigningEnabled
...
```

When Titanis imports options from the environment, it prints a message indicating the imported values.

This approach may be used along with parameter defaults.  However, note that using the `<command>_OPTIONS` pattern overrides the defaults; Titanis interprets this as if you specified the `<command>_OPTIONS` value directly on the command line.  This also means that you cannot override their value on the command line; you'll get an error that you specified the parameter multiple times.

# KRB5CCNAME Support
Commands that accept authentication parameters accept a parameter named `-TicketCache` that takes the name of file containing Kerberos tickets.  This parameter accepts a default value using either of the methods described above.  In addition, it also checks the environment variable `KRB5CCNAME`.  If the file doesn't exist, it is created if a ticket is requested.  The file may either be a `.kirbi` or `.ccache`.

# Parameter Profiles
If you find that you are commonly specifying the same defaults across sessions, consider creating a parameter profile.  To do this, simply create a text file that specifies these defaults, then import the profile using `source`:

```
milchick@COBEL-WKS:~$ cat milchick.profile
unset ${!TITANIS_DEFAULT_*}
export KRB5CCNAME=~/milchick.ccache
export TITANIS_DEFAULT_KDC=LUMON-DC1
export TITANIS_DEFAULT_WORKSTATION=milchick-wks
export TITANIS_DEFAULT_USERNAME=milchick@LUMON
export TITANIS_DEFAULT_PASSWORD=Br3@kr00m\!
milchick@COBEL-WKS:~$ source milchick.profile
``` 

The first line of this profile clears any existing defaults, and then sets the appropriate defaults for the user `milchick`.