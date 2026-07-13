# Kerberos

Most of the common authentication scenarios may be handled directly by the
commands.  The `Kerb` command supports more advanced scenarios, allowing you to
fine-tune the parameters of the interactions with the KDC.

1. [Requesting an Initial Ticket](#requesting-an-initial-ticket)
1. [Requesting a Service Ticket](#requesting-a-service-ticket)
1. [Renewing a Ticket](#renewing-a-ticket)
1. [Kerberoasting Support](#kerberoasting-support)
1. [Managing Ticket Files](#managing-ticket-files)

# Requesting an Initial Ticket

Kerberos authentication begins with the Authentication Server (AS exchange.  The
user present credentials in the form of a password or key to prove their
identity.  In reply, the AS sends a ticket-granting ticket (TGT) which the user
may use to request service tickets.

To request a ticket:

| With... | Use syntax... | Notes |
|-|-|-|
| Password | `Kerb asreq <user>@<realm> -Kdc <kdc address> -Password <password> -OutputFileName <file name>` | Be sure to escape any special characters in the password according to your shell. |
| NTLM hash | `Kerb asreq <user>@<realm> -Kdc <kdc address> -NtlmHash <hex-encoded hash> -OutputFileName <file name>` | |
| AES key (128- or 256-bit) | `Kerb asreq <user>@<realm> -Kdc <kdc address> -AesKey <hex-encoded key> -OutputFileName <file name>` | AES128 and AES256 are distinguished by the length of the key provided. |

If a ticket cache is set using the environment variable `KRB5CCNAME` then
`-OutputFileName` is not required; `Kerb` appends the ticket to the ticket cache.

By default, `Kerb asreq` sends a request with the following characteristics:
1. A target SPN of `krbtgt/<realm>`
1. KDC options: Forwardable, Renewable, Canonicalize, RenewableOk
1. An end time and renewable-till time of 10 hours from now
1. All available encryption types supported by the password or key.

To change the target SPN of the request, use `-Spn`.  For example, to request an
initial ticket to `kadmin/changepw`, assuming `KRB5CCNAME` is set:

```
Kerb asreq milchick@LUMON.IND 10.66.0.11 -Password Br3@kr00m! -Spn kadmin/changepw
```

You may also specify a user or computer name:

```
Kerb asreq milchick@LUMON.IND 10.66.0.11 -Password Br3@kr00m! -Spn LUMON-DC1$
```

# Requesting a Service Ticket

Once you have a ticket-granting ticket (TGT), you may use this ticket to request
a service ticket:

```
Kerb tgsreq -Tgt milchick-tgt.kirbi -Kdc 10.66.0.11 cifs/LUMON-FS1
```

As with `asreq`, the target may be specified as a user or computer:

```
Kerb tgsreq -Tgt milchick-tgt.kirbi -Kdc 10.66.0.11 LUMON-FS1$
```

You may request multiple tickets by specifying multiple SPNs:

```
Kerb tgsreq -Tgt milchick-tgt.kirbi -Kdc 10.66.0.11 cifs/LUMON-FS1 host/LUMON-FS1 RestrictedKrbHost/LUMON-FS1 LUMON-FS1$
```

# Renewing a Ticket
Use `Kerb renew` to renew a ticket.

To renew all tickets within a file:

```
Kerb renew -Ticket <filename> <kdc> -OutputFileName <output filename>
```

To renew tickets in the cache:

```
Kerb renew -TicketCache <filename> <kdc> -OutputFileName <output filename> -TargetSpn <spn1> [, <spns> ...]
```


# Kerberoasting Support

While Titanis does not kerberoasting, it does facilitate kerberoasting by
providing the ticket hash and hashcat method number as fields on the ticket.
These fields are not displayed by default; you must specify these fields with
`-OutputFields`:

| Field | Description |
|-|-|
| `TgsrepHashcatMethod` | Hash type to specify for the `-m` option |
| `TicketHash` | Ticket hash for hash file |

For example:

```
Kerb select -From milchick.kirbi -OutputFields UserName, TargetSpn, TgsrepHashcatMethod, TicketHash -OutputStyle List
```

When requesting the ticket with either `asreq` or `tgsreq`, use `-EncTypes` to
request an encryption type that is preferable to crack.

# Managing Ticket Files

Use the `Kerb select` command to display, convert, split, and combine ticket files.

Specify one or more source files with `-From`.  The files may be either `.kirbi`
or `.ccache` format.  If `-From` is not specified and `KRB5CCNAME` is set,
Titanis uses the ticket cache as the source.

If desired, specify filters to select only certain tickets.  Use `-Invert` to
select all tickets that do not match the filters.  Use `-SeqNbr` to select
tickets either by specific or range of sequence numbers.  Note that the sequence
numbers reflect the ticket's position within the cache and are not a part of the
ticket themselves.

To write the tickets to another file, specify the file with `-OutputFileName`.
For a new file, Titanis determines the file format based on the extension; a
file name ending in `.ccache` is written as a ccache file, while everything
else is written as a kirbi file.  You may append the tickets to a file by
specifying the existing file and specifying `-Append`.  The file format of the
existing file is retained, regardless of any file name extension.
