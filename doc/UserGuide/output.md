# Tool Output

When a tool executes, it provides both primary output as well as status
information.  Primary output contains the results of the command and is written
to STDOUT, while status information describes logging information describing
the operation of the tool.

Most commands write primary output to STDOUT in the form of records.  Each
record may be printed in one of several styles specified with `-OutputStyle`:

|Style|Description|
|-|-|
|Table|Each record in printed as a row within a table.|
|List|Each field of each record is printed on a separate line as `<field>: <value>` with a blank line between records.|
|Csv|Each record is printed as comma-separated values.|
|Tsv|Each record is printed as tab-separated values.|
|Json|Each record is printed as a JSON object within an array.|

Use `-OutputFields` to specify which fields to print.  The help text for the
command lists the fields supported by the commands.  Note that specifying some
fields may alter the behavior of the command.  For example, the command may
issue another request to the server to get the additional field.

Some commands that deal with raw data use the `Raw` format which is printed to STDOUT as a stream of bytes.

# Logging
During execution, a tool writes status information to STDERR in the form of log messages.  Each message is assigned a log level indicating the severity of the message.  By default, only messages with a level of `Info` or higher are printed.  To control the level of messages written, use `-LogLevel`.  You can also use `-v` for `Verbose` and `-vv` for `Diagnostic'.

Use `-LogFormat` to control how log messages appear on the console:

|Option|Description|Example|
|-|-|-|
| `Text` | Unstructured free-form text with the severity, category, and message text | `[Kerberos] DIAG: Requesting TGT for realm LUMON.IND for user milchick (nonce=-40593729)` |
| `TextWithTimestamp` | Similar to `Text` with the addition of an ISO-formatted timestamp | `[2025-10-23T16:14:09.6977870Z][Kerberos] DIAG: Requesting TGT for realm LUMON.IND for user milchick (nonce=-40593729)` |
| `Json` | Serios of JSON objects | `{"Severity":"Diagnostic","SeverityValue":-200,"Source":"Kerberos","MessageId":0,"MessageText":"Requesting TGT for realm LUMON.IND for user milchick (nonce=-525351544)","Parameters":null}` |
