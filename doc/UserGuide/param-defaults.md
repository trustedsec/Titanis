# Parameter Defaults

Titanis includes features that allow you to specify the default values for common parameters.

# Command-specific Options
To specify options that are always included in the invocation of a command set
an environment variable of the form `<command>_OPTIONS` where '<command>' is
the name of the command.  When a command is invoked, Titanis checks for
this environment variable.  If found, the value is tokenized and the arguments
are prepended to other arguments to the command.

For example, to set default options for Kerb to
`-vv`, set `KERB_OPTIONS=-vv`.

For tools that implement subcommands, the command names are concatenated with
an intervening `_`.  For example, for `Smb2Client ls`, use
`SMB2CLIENT_LS_OPTIONS`.

When a tool imports options in this way, it prints the imported options

```
INFO: Using options from environment SMB2CLIENT_LS_OPTIONS: -vv
```
# Parameter-specific Defaults
Since parameter names are consistent across commands, you may also set a
global default value for a parameter regardless of the command.  To do so,
set an environment variable of the form `TITANIS_DEFAULT_<param>` where
`<param>` is the full name of the parameter.

For example, to set the default log level:

Linux:
```
export TITANIS_DEFAULT_LOGLEVEL=diagnostic
```

Windows:
```
set TITANIS_DEFAULT_LOGLEVEL=diagnostic
```

When the command imports the parameter set in this way, it prints a message
similar to the following:

```
INFO: Importing default for 'LogLevel': diagnostic
```

# KRB5CCNAME Support
Commands that accept authentication parameters accept a parameter named
`-TicketCache` that takes the name of file containing Kerberos tickets.  This
parameter accepts a default value using either of the methods described above.
In addition, it also checks the environment variable `KRB5CCNAME`.

If the file doesn't exist, it is created if a ticket is requested.  The file
may either be a `.kirbi` or `.ccache`.
