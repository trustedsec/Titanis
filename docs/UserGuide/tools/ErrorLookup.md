# ErrorLookup
Looks up error codes

## Synopsis
**ErrorLookup** [*options*] &lt;*ErrorCode*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*ErrorCode*&gt;||&lt;*String[]*&gt;|Error code (decimal, hex, name)|


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
||||Possible values:|
||||  **Code**|
||||  **Kind**|
||||  **SymbolicName**|
||||  **Message**|
||||  **CodeHex**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|

