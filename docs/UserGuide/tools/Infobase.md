# Infobase
Peform tasks with infobase files

## Synopsis
```
Infobase <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[analyze](#infobase-analyze)**|Analyzes attack paths|
|**[history](#infobase-history)**|Prints command history|
|**[query](#infobase-query)**|Queries item data|


For help on a subcommand, use `Infobase <subcommand> -h`
# Infobase analyze
Analyzes attack paths

## Synopsis
**Infobase analyze** [*options*] &lt;*InfoBase*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*InfoBase*&gt;||&lt;*FileSpec*&gt;|Infobase file name|


## Options


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogBase**||&lt;*FileSpec*&gt;|Infobase file to log results to|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|

# Infobase history
Prints command history

## Synopsis
**Infobase history** [*options*]

## Options


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
||||Possible values:|
||||  **CommandName**|
||||  **CommandLine**|
||||  **StartTime**|
||||  **EndTime**|
||||  **Duration**|
||||  **ExitCode**|
||||  **ExitCodeHex**|
||||  **ErrorDetails**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|

# Infobase query
Queries item data

## Synopsis
**Infobase query** [*options*] [ &lt;*ObjectClass*&gt; ]

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ObjectClass*&gt;||&lt;*String*&gt;|Object class to query for|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|**-A**, **-ActionId**||&lt;*Int32*&gt;|Command to query results for|


### Logging

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-LogAttributes**||&lt;*String[]*&gt;|Nema=Value pairs to associate with log entries|
|    **-LogComment**||&lt;*String*&gt;|Comment to associate with log entries|
|    **-LogPartition**||&lt;*String*&gt;|Partition to associate log entries with|


### Output

|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ConsoleLogFormat**|**-LogFormat**|&lt;*LogFormat*&gt;|Sets the format of log messages written to the console|
||||  Default: 0|
||||Possible values:|
||||  **Text**|
||||  **TextWithTimestamp**|
||||  **Json**|
|    **-ConsoleOutputStyle**|**-OutputStyle**|&lt;*OutputStyle*&gt;|Determines the output style|
||||Possible values:|
||||  **Freeform**|
||||  **Raw**|
||||  **Table**|
||||  **List**|
||||  **Csv**|
||||  **Tsv**|
||||  **Json**|
||||  **TreeTable**|
|    **-DebugLog**|**-vvv**|&lt;*SwitchParam*&gt;|Prints debug messages|
|    **-Diagnostic**|**-vv**|&lt;*SwitchParam*&gt;|Prints diagnostic messages|
|**-H**, **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
|    **-LogLevel**||&lt;*LogMessageSeverity*&gt;|Sets the lowest level of messages to log|
||||Possible values:|
||||  **Debug**|
||||  **Diagnostic**|
||||  **Verbose**|
||||  **Info**|
||||  **Warning**|
||||  **Error**|
||||  **Critical**|
|    **-OutputFields**||&lt;*String[]*&gt;|Fields to display in output|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|

