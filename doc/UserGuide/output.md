# Tool Output

When a tool executes, it provides both primary output as well as status information.  Primary output contains the results of the command and is written to STDOUT, while status information describes logging information describing the operation of the tool.

Primary output is written to STDOUT in the form of records.  Each record may be printed in one of 3 styles: list, table, or free-form.  The `List` style print each field on a separate line as `Name: Value` pairs, with a blank line between records.  The `Table` style prints the output as a table with the field names as column headings and each record as a row in the table.  `Freeform` represents each record as unstructured text.  Some commands that deal with raw data use the `Raw` format which is printed to STDOUT as a stream of bytes.  Use `-ConsoleOutputStyle` to specify the style of the output.

For commands that output structured data, use `-OutputFields` to specify which fields to include in the output.  This affects the operation of some commands; if the field is not part of the default field set, this may cause the tool to make another request to get the additional information.  This is indicated in the tool documentation.  Check the tool documentation for the list of supported field names.

# Logging
During execution, a tool writes status information to STDERR in the form of log messages.  Each message is assigned a log level indicating the severity of the message.  By default, only messages with a level of `Info` or higher are printed.  To control the level of messages written, use `-LogLevel`.  You can also use `-v` for `Verbose` and `-vv` for `Diagnostic'.

The log messages may be written in one of the following formats:
* `Text` - Unstructured free-form text with the severity, category, and message text
* `TextWithTimestamp` - Same as `Text` with the addition of an ISO-formatted timestamp
* `Json` - Serios of JSON objects

Use `-ConsoleLogFormat` to control how log messages appear on the console.
