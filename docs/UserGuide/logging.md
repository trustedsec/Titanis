# Log Level
During execution, a tool writes status information to STDERR in the form of log messages.  Each message is assigned a log level indicating the severity of the message.  By default, only messages with a level of `Info` or higher are printed.  To control the level of messages written, use `-LogLevel`.  You can also use `-v` for **Verbose** and `-vv` for **Diagnostic**.

# Log Format
Use `-ConsoleLogFormat` to control how log messages appear on the console:

|Option|Description|Example|
|-|-|-|
| `Text` | Unstructured free-form text with the severity, category, and message text | `[Kerberos] DIAG: Requesting TGT for realm LUMON.IND for user milchick (nonce=-40593729)` |
| `TextWithTimestamp` | Similar to `Text` with the addition of an ISO-formatted timestamp | `[2025-10-23T16:14:09.6977870Z][Kerberos] DIAG: Requesting TGT for realm LUMON.IND for user milchick (nonce=-40593729)` |
| `Json` | Serios of JSON objects | `{"Severity":"Diagnostic","SeverityValue":-200,"Source":"Kerberos","MessageId":0,"MessageText":"Requesting TGT for realm LUMON.IND for user milchick (nonce=-525351544)","Parameters":null}` |
