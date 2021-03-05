# Command Line Syntax

While each tool defines its own set of parameters, all tools within the Titanis suite use a shared command line parser.  This provides a uniform command line experience across the tools.  To view the command line syntax for a tool, invoke the tool with the switch `-?` by itself.  This instructs the tool to print its documentation.  This includes all the parameters, a description of what the tool does, and usually one or more examples of how to use the tool.  The documentation begins with the synopsis, which shows a list of mandatory commands.  For example, here's the synopsis for `Kerb tgsreq`

```
SYNOPSIS
  Kerb tgsreq [options] -Kdc <String> -Tgt <String> -OutputFileName <String> <ServicePrincipalName[]>
```

The parts enclosed in `<` and `>` are placeholders for the parameters values and usually indicate what type of value the parameter requires.  Placeholders ending with `[]` indicate that the parameter is a [list parameter](#list-parameters) that accepts multiple values.

Parameters included in the synopsis are mandatory and must be specified on the command line.  If you omit a mandatory parameter, the command will not execute and will instead print the help text followed by one or more errors regarding the missing parameters.

The synopsis is followed by a list of all parameters, including both mandatory and optional parameters.  The parameters are grouped by category where appropriate and are listed with their name, aliases, placeholder, and usually have a short description of how the parameter is used.  If a parameter is listed without a placeholder, it is a [switch parameter](#switch-parameters).

# Parameter Names and Positional Parameters

All parameters have a name and may be specified on the command line with its name preceded with a dash.  Except for switch parameters, you must provide the value after the parameter name, separated either by a space or a `:`.  For example, to specify the parameter `UserName` for the tool `tool`, you may use:

```
tool -UserName milchick
```

or

```
tool -UserName:milchick
```

If the parameter value contains a space, it must be escaped according to the rules of the shell.  For Windows, this means enclosing the value in quotes.  For Linux, this usually means either enclosing the value in quotes or preceding each space character with a backslash (`\`) to escape it.

Some parameters may be specified by their position within the command line and do not require the name.  These are called `positional paremeters` and may be specified on the command line as a value without a name.  Positional parameters must be specified in the order indicated in the synopsis.  If the last positional parameter is a list parameter (indicated by `[]` in the placeholder), then all remaining values on the command line are assigned to this parameter.  This means that named parameters cannot be specified after the last positional parameter, since the command line parser will instead treat the parameter name as a value.

## Switch Parameters
A switch parameter is a special type of parameter that does not require a value.  A switch parameter represents a boolean input such as yes/no, on/off, or true/false.  Most switch parameters are treated as `false` if not specified, and `true` if specified.  To explicitly specify the value of a switch, follow the switch name with `:<value>`.  For example:

```
tool -Proxiable:false
```

If a command treats a switch as true by default, use the above notation to set it to false.  Explicitly specifying a switch value may also be helpful for documentation and logging to indicate that you intended for the value to be false and it wasn't missed or forgotten.

## List Parameters
Some parameters accept multiple values, indicated with the presence of `[]` in the placeholder.  To specify multiple values for a named parameter, enter the values with a trailing comma.  For example:

```
tool -Multi value1, value2, value3
```

The above example specifies the values `value1`, `value2`, and `value3` for the parameter named `Multi`.  The command line parser strips the trailing comma befor assigning the value; the comma is simply an indicator that more values follow.

## HexString Parameters
Some parameters accept a value in the form of a hexadecimal string, which is indicated by the placeholder `<HexString>`.  Specify the value as a string of hexadecimal digits with no prefix.  For example, to specify the bytes { 1, 2, 3, 4 } for a parameter named `HexStringParam`:

```
tool -HexStringParam 01020304
```

## Blob Parameters
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

## UncPath Parameters

Some commands accept a UNC path as a parameter.  A UNC path has the following syntax:

```\\<server name or address>[:<port>][\share[\path]]```

The server name or address is required.  When connecting to a server, the server name serves a dual role, both to identify the server to the network layer as well as to negotiate the application protocol.  Most commands accept an IP address in place of the server name, but note that this IP address is sent to the server.  If you are using a SOCKS proxy or other indirect method to connect to the server, this means your intermediate IP will be sent to the target, which may serve as an indication of compromise.  Most commands support a companion argument `-HostAddress` to specify the network address to connect to.  The provided `<server name>` is still used in the application protocol, but the network layer connects to the `-HostAddress` value.

Some commands require the share and path while other commands may only require the share name or just the server name.  Consult the documentation for more information.

### Alternative Syntax

The use of backslashes in a UNC path may prove problematic for Linux users, since many shells interpret the backslash character as an escape character.  This requires you to double the backslashes on the command line, resulting in paths like this:

```
\\\\SERVER1\\Users\\milchick
```

To alleviate this, Titanis tools support an alternative syntax that replaces the backslashes with slashes:

```
//SERVER1/Users/milchick
```

When parsing such a path, the command line parser replaces the slashes with backslashes for you.

Although this alternative syntax is only useful on Linux, it may be used on the Windows variants.  This provides a uniform command line experience when entering UNC paths.

## Parameter Groups
Although each tool does its own thing, there are a few cross-cutting concerns that affect multiple tools, such as logging and authentication.  To provide a uniform command line experience, the tools share implementation of parameter groups to handle these concerns.  For more information, consult the documentation for a particular parameter group.

* [Authentication Parameters](syntax-auth.md)