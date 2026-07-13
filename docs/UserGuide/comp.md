Titanis supports command-line completion for bash and zsh.  To get started, you must include the completion scripts in your environment.  The scripts are located in the **autocomp** directory of the source repository, with a directory for **bash** and another for **zsh**.

## Bash
For bash, simply source the completion scripts:
```
for f in $titanisdir/autocomp/bash/*; do source $f; done
```

## Zsh
For zsh, you must add the directory containing the completion scripts to $fpath before calling **compinit**.  This is usually done in **.zshrc**.
1. Edit **.zshrc** in your home directory.
1. Locate the line calling **compinit**.
1. Before this line, add a line similar to the following:
```
fpath+=(titanis/autocomp/zsh)
```
For changes to be effective, you must reinitialize completion.  The easiest way to do this is to restart zsh.


# Discovering Subcommands
Most Titanis commands offer functionality in the form of subcommands.  To list the available subcommands, position the cursor where you would type the subcommand, and press *TAB*:
```
Smb2Client <TAB>
```
This displays all available subcommands.

To filter the list of subcommands, enter a prefix before pressing *TAB*:
```
Smb2Client g<TAB>
```
This displays all subcommands beginning with *g*.

# Listing Positional Parameters
If you are unsure of what parameter a command expects in a particular position, position the cursor where the parameter should go:
```
Smb2Client get <TAB>
```
This inserts the name of the parameter for you.  Note that all parameters in Titanis commands may be specified by name, so you may enter the parameter value without deleting the parameter name.

# Listing Parameters by Name
To display a list of available parameters, enter **-** and press *TAB*:
```
Smb2Client get -<TAB>
```
To display a list of parameteris beginning with a prefix, enter the prefix before pressing *tab*:
```
Smb2Client get -User<TAB>
```
This displays all parameters beginning with **-User**.

Parameters that have already been specified are filtered out of the list.

# Listing Parameter Values
Some parameters only allow certain values.  To display a list of values, position the cursor where the parameter value would go and press *TAB*:
```
Smb2Client get -Dialects <TAB>
```
This displays a list of values accepted by **-Dialects**.

# Customizing Parameter Values
Some parameters, such as **-ServerName**, don't offer a list of values by default.  To specify your own, define an array variable named **TITANIS_PVLIST_**<*name*>.  For example, to provide a list of values for *-ServerName*:

```
TITANIS_PVLIST_SERVERNAME=(lumon-dc1 lumon-dc2 lumon-fs1)
```

## Reading Parameter Completions from Files
The general syntax for specifying parameter completions from a file:
**TITANIS_PVLIST_**<*name*>=( **^**<*filename*>**[;**<*field*>**[;**<*delimiter*>]] )

The *field* and *delimiter* are optional.  If no *field* is included, Titatins selects the entirce line.  If *delimiter* is not specified, Titanis uses **$IFS**.

For example, to use the static values above and read the values from **/etc/hosts**:

```
TITANIS_PVLIST_SERVERNAME=(lumon-dc1 lumon-dc2 lumon-fs1 "^/etc/hosts;2")
```

Now when providing completions for *-ServerName*, Titanis reads each line from **/etc/hosts**, splits the line on whitespace, and adds the value from the second field to the completion list.

If instead you want to use the third column from a CSV file named **servers.csv***

```
TITANIS_PVLIST_SERVERNAME=(lumon-dc1 lumon-dc2 lumon-fs1 "^serverlist.csv;3;,")
```