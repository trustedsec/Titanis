# Setting up Your Environment

While Titanis doesn't require any particular environment setup, here are some
recommendations that may help streamline your operation.

## Global Defaults

Set global defaults by setting the corresponding `TITANIS_DEFAULT_xxx` environment
variables.  Put the defaults in the appropriate `.profile` file for your shell.

Some variables to consider for global defaults:

* `TITANIS_DEFAULT_LOGLEVEL`
* `TITANIS_DEFAULT_LOGFORMAT`
* `TITANIS_DEFAULT_WORKSTATION`
* `TITANIS_DEFAULT_KDC`
* `TITANIS_DEFAULT_REALM`

## Managing Credentials

While Titanis supports different ticket management strategies, it works best
with maintaining one file per identity.  With this approach, set `KRB5CCNAME` to
a file specific to the corresponding identity.  Titanis will store tickets from
the KDC in this file and reuse them for subsequent commands.

For other strategies, use `-Tgt` to specify the ticket-granting ticket and
`-Ticket` to specify a file with the service tickets.  With this approach,
Titanis checks the file specified by `-Ticket` for service tickets.  If the file
does not contain a suitable ticket, Titanis sends a request to the TGS using the
specified `-Tgt` and `-Kdc`.  If these parameters are not provided, the command
fails.  Note that some commands require multiple service tickets; in these
cases, the tickets must be combined into one file and specified with `-Ticket`.
See [Kerberos](kerberos.md#managing-ticket-files) for details on managing ticket
files.

For each identity, create a `.cred` file with the following variables:

* `TITANIS_DEFAULT_USERNAME`
* `TITANIS_DEFAULT_PASSWORD`
* `TITANIS_DEFAULT_USERDOMAIN`
* `KRB5CCNAME`

For other authentication scenarios, see the [Authentication Guide](syntax-auth.md).