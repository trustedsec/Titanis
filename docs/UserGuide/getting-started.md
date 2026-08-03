Titanis offers several command line tools for working with network protocols.  Most of the tools have a name resembling the protocol they work with.  In this tutorial, you'll learn how to use **Smb2Client**, which uses the Windows protocol for file sharing.

To view help for any command, use **-h**:

```
Smb2Client -h
```

This prints a brief description of the tool, followed by its syntax (synopsis).

# Subcommands

*Smb2Client*, like most Titanis commands, exposes functionality as a series of subcommands.  Let's start by enumerating the shares with **enumshares**.  As before, to view documentation, use **-h**:

* For a list of all commands and subcommands, see the [Tool Index](tools/index.md)

```
$ Smb2Client enumshares -h

  Lists shares on the server

SYNOPSIS
---------
  Smb2Client enumshares [options] <ServerName>
...
```

The synopsis indicates that the command accepts one required parameter, `-ServerName`.  Following the synopsis are the various parameters and options that the command accepts.  If you enter an invalid parameter, or omit a required parameter, Titanis prints the documentation for the command, followed by an error describing the missing or invalid parameters.  The documentation usually provides examples for commond scenarios to get you started.  Let's continue with `Smb2Client enumshares`:

# Your First Command
```
$Smb2Client enumshares lumon-dc1 -UserName milchick -Password 'Br3@kr00m!'
 INFO: Smb2Client Version 0.9.20260730084805.Q

Name      Type              Remark               Permissions  Max. Uses  Current Uses  Path                                             Password  Sec. Desc.
--------  ----------------  -------------------  -----------  ---------  ------------  -----------------------------------------------  --------  --------------------------------------------------
ADMIN$              Hidden  Remote Admin                None         -1             0  C:\WINDOWS
C$                  Hidden  Default share               None         -1             0  C:\
IPC$      TypeMask, Hidden  Remote IPC                  None         -1             3
NETLOGON              Disk  Logon server share          None         -1             0  C:\WINDOWS\SYSVOL\sysvol\corp.lumon.ind\SCRIPTS            O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)
SYSVOL                Disk  Logon server share          None         -1             1  C:\WINDOWS\SYSVOL\sysvol                                   O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)(A;;FA;;;AU)```
```

This command uses NTLM authentication with user name **milchick** and password **Br3@kr00m!**.  For a list of supported authentication scenarios and their associated syntaxes, see the [Authentication Guide](syntax-auth.md).

# Output
Most commands print the output as a series of records in a table.  You can change the format of the output, as well as the fields it contains, with **-ConsoleOutputStyle** and **-OutputFields**.

```
$ Smb2Client enumshares lumon-dc1 -UserName milchick -Password 'Br3@kr00m!' -ConsoleOutputStyle List -OutputFields ShareName, ShareType
 INFO: Smb2Client Version 0.9.20260730084805.Q

Name: ADMIN$
Type: Hidden

Name: C$
Type: Hidden

Name: IPC$
Type: TypeMask, Hidden

Name: NETLOGON
Type: Disk

Name: SYSVOL
Type: Disk
```

For a list of available output fields, use **-h** to print the documentation, then look at the options for **-OutputFields**.  For more details on customizing output, see [Customizing Output](output.md).

# Logging
In addition to primary command output, Titanis commands print additional details as log messages.  This includes significant events such as network connections or authentication.  You can change the logging verbosity with **-Verbose** or **-Diagnostic**, or their abbreviations **-v** and **-vv**, respectively.

Similar to console output, Titanis commands can produce log output in different formats.

```
$  Smb2Client enumshares lumon-dc1 -UserName milchick -Password 'Br3@kr00m!' -vv -ConsoleLogFormat TextWithTimestamp
[2026-08-03T18:36:05.3440506Z] INFO: Smb2Client Version 0.9.20260730084805.Q

[2026-08-03T18:36:05.3921606Z][Smb2Client] DIAG: The Titanis Smb2Client is connecting to \\lumon-dc1 at Unspecified/lumon-dc1:445
        Client GUID  : d2bdd6f1-0ace-4b09-a88b-92260a00920d
        Capabilities : Dfs, Leasing, LargeMtu, MultiChannel, PersistentHandles, DirectoryLeasing, Encryption
        Security mode: SigningEnabled
[2026-08-03T18:36:05.4737192Z][PlatformNameResolver] DIAG: System DNS resolved lumon-dc1 as [ 10.66.0.11 ]
[2026-08-03T18:36:05.4740912Z][PlatformNameResolver] VERBOSE: Resolved lumon-dc1 with [ 10.66.0.11 ]
[2026-08-03T18:36:05.5353058Z][Smb2Client] DIAG: The Titanis Smb2Client has connected to \\lumon-dc1 at Unspecified/lumon-dc1:445
...
```

For more information on logging, see [Customizing Logging](logging.md)

# Parameter Defaults
Due to the number of available options, Titanis command lines can become quite long, with some parameters being repeated with each invocation.  To help with this, Titanis commands accept parameter defaults as environment variables.  To specify a default value for any named parameter, set an environment variable with the name **TITANIS_DEFAULT_**<*name*>.

Set the commonly used parameters like this:
```
export TITANIS_DEFAULT_USERNAME=milchick
export TITANIS_DEFAULT_PASSWORD=Br3@kr00m\!
export TITANIS_DEFAULT_VVV=on
export TITANIS_DEFAULT_CONSOLELOGFORMAT=TextWithTimestamp
```

Then enter the same command, but without the parameters:
```
$ Smb2Client enumshares lumon-dc1
 INFO: Importing default for 'ConsoleLogFormat': TextWithTimestamp
 INFO: Importing default for 'UserName': milchick
 INFO: Importing default for 'Password': Br3@kr00m!
[2026-08-03T18:41:27.2736563Z] INFO: Smb2Client Version 0.9.20260730084805.Q

Name      Type              Remark               Permissions  Max. Uses  Current Uses  Path                                             Password  Sec. Desc.
--------  ----------------  -------------------  -----------  ---------  ------------  -----------------------------------------------  --------  --------------------------------------------------
ADMIN$              Hidden  Remote Admin                None         -1             0  C:\WINDOWS
C$                  Hidden  Default share               None         -1             0  C:\
IPC$      TypeMask, Hidden  Remote IPC                  None         -1             2
NETLOGON              Disk  Logon server share          None         -1             0  C:\WINDOWS\SYSVOL\sysvol\corp.lumon.ind\SCRIPTS            O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)
SYSVOL                Disk  Logon server share          None         -1             0  C:\WINDOWS\SYSVOL\sysvol                                   O:BAG:SYD:(A;;RCFRFX;;;WD)(A;;FA;;;BA)(A;;FA;;;AU)

```

Titanis prints messages letting you know that certain parameter values were imported from the environment.  This aids in logging, screenshots, and troubleshooting.

For more information, see [Parameter Defaults](param-defaults.md)

# Onward
* For more details on common syntax features, see [Syntax Basics](syntax.md)
* For details on completion features, see [Using Command Line Completion](comp.md)
* To learn how to perform specific tasks, see [How To](HowTo/) section.