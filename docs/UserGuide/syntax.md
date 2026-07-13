While each tool defines its own set of parameters, all tools within the Titanis suite use a shared command line parser.  This provides a uniform command line experience across the toolset.  This section explains the common features of this command line parser.

Titanis tools are generally named after the protocol that they implement.  Most of them implement **subcommands** corresponding to the various operations within the protocol.


# Getting Help

Every command accepts `-h` to print documentation about how to use the command.  For example, here is the documentation printed by `Smb2Client`:

```
milchick@COBEL-WKS:~$ Smb2Client -h
  Performs operations on an SMB2 server.

SYNOPSIS
---------
  Smb2Client <subcommand>

Subcommands
------------
  enumnics       Queries the server for a list of network interfaces.
  enumopenfiles  Lists files open on the server.
  enumsessions   Lists active sessions on the server.
  enumshares     Lists shares on the server
  enumsnapshots  Lists the available snapshots for a file or directory.
  enumstreams    Lists the data streams of a file or directory.
  get            Gets the contents of a file.
  ls             Lists the contents of a directory (including named pipes).
  mkdir          Creates a directory.
  mklink         Creates a symbolic link.
  mount          Creates a mount point or junction.
  mountfs        Mounts an SMB2 server or share to the local file system.
  put            Sends a file to the server.
  rm             Deletes a file.
  rmdir          Deletes a directory.
  touch          Updates the timestamps or attributes of a file or directory on an SMB share.
  umount         Unmounts a mount point.
  watch          Watches for modifications to a directory or subtree.

  For help on a subcommand, use `Smb2Client <subcommand> -h
```

The first line provides a simple description of the tool.  This is followed by a synopsis of the command line syntax, listing the various parameters accepted by the command.

For tools that implement subcommands, the documentation lists the supported subcommands, along with a description of each.  For help on a subcommand, specify the subcommand as the first argument, followed by `-h`:

```
milchick@COBEL-WKS:~$ Smb2Client ls -h
 INFO:   Lists the contents of a directory (including named pipes).

SYNOPSIS
---------
  Smb2Client ls [options] <UncPath>

PARAMETERS
-----------
  <UncPath>  <UNC path>  The UNC path of the target

OPTIONS
--------
...


DETAILS
--------
...

EXAMPLES
---------
...
```

The parts enclosed in `<` and `>` are placeholders for the parameters values and usually indicate what type of value the parameter requires.  Placeholders ending with `[]` indicate that the parameter is a [list parameter](#list-parameters) that accepts multiple values.

Parameters included in the synopsis are mandatory and must be specified on the command line.  If you omit a mandatory parameter, the command will not execute and will instead print the help text followed by one or more errors depicting the missing parameters.

The synopsis is followed by a list of all parameters, including both mandatory and optional parameters.  The parameters are grouped by category where appropriate and are listed with their name, aliases, placeholder, and usually have a short description of how the parameter is used.  If a parameter is listed without a placeholder, it is a [switch parameter](#switch-parameters).

The **Details** section usually contains implementation details or various use cases for the command.

The **Examples** section lists examples that demonstrate how to use the tool with various parameters.

# Parameter Names and Positional Parameters

All parameters have a name and may be specified on the command line with its name preceded with a dash.  Except for switch parameters, you must provide one or more values after the parameter name, separated either by a space or a `:`.  For example, to specify the parameter `UserName` for the tool `Smb2Client`, you may use:

```
tool -UserName milchick
```

or

```
tool -UserName:milchick
```

If the parameter value contains a space, it must be escaped according to the rules of the shell.  For Windows, this means enclosing the value in quotes.  For Linux, this usually means either enclosing the value in quotes or preceding each space character with a backslash (`\`) to escape it.

Some parameters may be specified by their position within the command line and do not require the name.  These are called `positional paremeters` and may be specified on the command line as a value without a name.  Positional parameters must be specified in the order indicated in the synopsis.  If the last positional parameter is a list parameter (indicated by `[]` in the placeholder), then all remaining values on the command line are assigned to this parameter.  This means that named parameters cannot be specified after the last positional parameter, since the command line parser will instead treat the parameter name as a value.

# Switch Parameters
A switch parameter is a special type of parameter that does not require a value.  A switch parameter represents a boolean input such as yes/no, on/off, or true/false.  Most switch parameters are treated as `false` if not specified, and `true` if specified.  To explicitly specify the value of a switch, follow the switch name with `:<value>`.  For example:

```
tool -Proxiable:false
```

If a command treats a switch as `true` by default, use the above notation to set it to false.  Explicitly specifying a switch value may also be helpful for documentation and logging to indicate that you intended for the value to be false and it wasn't missed or forgotten.

# List Parameters
Some parameters accept multiple values, indicated with the presence of `[]` in the placeholder.  To specify multiple values for a named parameter, enter the values with a trailing comma.  For example:

```
tool -Multi value1, value2, value3
```

The above example specifies the values `value1`, `value2`, and `value3` for the parameter named `Multi`.  The command line parser strips the trailing comma before assigning the value; the comma is simply an indicator that more values follow.

NOTE: If you are running Titanis tools with PowerShell, the PowerShell parser strips the commas before sending the command line to the tool.  In this case, you must escape the commas with '^'.  For example, `tool -Multi value1^, value2^, value3`

# HexString Parameters
Some parameters accept a value in the form of a hexadecimal string, which is indicated by the placeholder `<HexString>`.  Specify the value as a string of hexadecimal digits with no prefix.  For example, to specify the bytes { 1, 2, 3, 4 } for a parameter named `HexStringParam`:

```
tool -HexStringParam 01020304
```

# Blob Parameters
Blob parameters accept a binary value that may be specified as either a hexadecimal string, a Base64 string, or as a file.  To specify the value of a parameter named `BlobParam`:

As a hexadecimal string:

```
tool -BlobParam hex:41424344
```

As a Base64 string:

```
tool -BlobParam b64:QUJDRA==
```

As a file named `hash.bin`:

```
tool -BlobParam hash.bin
```

When using the Base64 syntax, the parser accepts both the normal syntax (RFC 4648) as well as the URL-friendly variant.  The value doesn't require the trailing padding in the form of `=` signs.

# UncPath Parameters

Some commands accept a UNC path as a parameter.  A UNC path has the following syntax:

```\\<server name or address>[:<port>][\share[\path]]```

The server name or address is required.  When connecting to a server, the server name specified in the UNC path serves a few roles: to specify the host to connect to (e.g. DNS resolution), to form the SPN for authentication, and to negotiate the application protocol.  Most commands accept an IP address in place of the server name.  This facilitates the network connection, but this will generally fail to yield a valid SPN, and may fail the application protocol negotiation, as Titanis doesn't have a host name (only the IP) to negotiate with the server.  If you are using a SOCKS proxy or other indirect method to connect to the server, this means your intermediate IP will be sent to the target, which may serve as an indication of compromise.  Most commands support a companion argument `-HostAddress` to specify the network address to connect to.  The server name provided in the UNC path is still used in the application protocol and for authentication, but the network layer connects to the `-HostAddress` value.  Note that the value provided to `-HostAddress` may either be an IP address or a host name.

Some commands require the share and path while other commands may only require the share name or just the server name.  Consult the documentation of the command for more information.

## Alternative Syntax

The use of backslashes in a UNC path may prove problematic for Linux users, since many shells interpret the backslash character as an escape character.  This requires you to double the backslashes on the command line, resulting in paths like this:

```
\\\\SERVER1\\Users\\milchick
```

To alleviate this, Titanis tools support an alternative syntax for UNC paths that accepts slashes instead of backslashes:

```
//SERVER1/Users/milchick
```

When parsing such a UNC path, the command line parser replaces the slashes with backslashes for you.

Although this alternative syntax is only useful on Linux, it is supported by all platforms, including Windows.  This provides a uniform command line experience when entering UNC paths.

# See Also
* [Authentication Parameters](syntax-auth.md)