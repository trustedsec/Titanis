When a tool executes, it provides both primary output as well as status
information.  Primary output contains the results of the command and is written
to STDOUT, while status information describes logging information describing
the operation of the tool.  This article focuses on the primary output.  For details on logging, see [Customizing Logging](logging.md)

Most commands write primary output to STDOUT in the form of records.  Each
record contains one or more fields and may be printed in one of several styles specified with `-OutputStyle`:

|Style|Description|
|-|-|
|Table|Each record in printed as a row within a table.|
|List|Each field of each record is printed on a separate line as `<field>: <value>` with a blank line between records.|
|Csv|Each record is printed as comma-separated values.|
|Tsv|Each record is printed as tab-separated values.|
|Json|Each record is printed as a JSON object within an array.|

Some commands (e.g. `Smb2Client get`) that deal with raw data use the `Raw` format which is printed to STDOUT as a stream of bytes.

# Output Fields
Use `-OutputFields` to specify which fields to print.  The help text for the
command lists the fields supported by the commands.  Note that specifying some
fields may alter the behavior of the command.  For example, the command may
issue another request to the server to get the additional field.  To specify multiple output fields, specify each field after `-OutputFields`:

For example, to list named pipes with their security descriptors, use:
```
Smb2Client ls //lumon-dc1/ipc$ -OutputFields FileName, SecurityDescriptorSddl
```

In this case, the command must make an extra call to the server to get the security descriptor.

Specifying an invalid field name is not an error, but no value will be printed.

## Dynamic Fields
Some commands, such as `Wmi query` or `Ldap query`, offer dynamic fields that are determined at runtime.  This usually affects the request to the server.  In these cases, the help text does not include a list of output fields.

# Headers
By default, the **Table** and **List** styles output headers for the fields.  To prevent this, specify `-OutputHeaders:no`  This is often useful when piping the output to another command.
