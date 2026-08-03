# Sddl
Works with security descriptors represented in SDDL

## Synopsis
```
Sddl <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[describe](#sddl-describe)**|Describes a security descriptor|
|**[lookupguid](#sddl-lookupguid)**|Looks up a GUID representing an AD extended right, property, or property set|
|**[lookupwks](#sddl-lookupwks)**|Looks up a well-known SID|


For help on a subcommand, use `Sddl <subcommand> -h`
# Sddl describe
Describes a security descriptor

## Synopsis
**Sddl describe** [*options*] &lt;*SddlOrHex*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*SddlOrHex*&gt;||&lt;*SecurityDescriptor[]*&gt;|Security descriptor in hex or SDDL notation|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-ObjectType**||&lt;*SecurityObjectType*&gt;|Type of object|
||||Possible values:|
||||  **File**|
||||  **Directory**|
||||  **RegistryKey**|
||||  **SamServer**|
||||  **SamDomain**|
||||  **SamGroup**|
||||  **SamAlias**|
||||  **SamUserAccount**|
||||  **DirectoryObject**|
||||  **Scm**|
||||  **Service**|
|    **-PrintHex**||&lt;*SwitchParam*&gt;|Prints the binary form as a string of hex digits|
|    **-PrintSddl**||&lt;*SwitchParam*&gt;|Prints the SDDL form|


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
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Details

This command accepts one or more security descriptors.  Each security
descriptor may be specified either in the SDDL form, or in the binary form as a
series of hex digits.  The -ObjectType specifies how the bits are translated to
specific permissions.  If no object type is specified, it is assumed to be for
a file.

Specifying -PrintHex or -PrintSddl effectively allows you to convert between
the SDDL and binary form of a security descriptor.



## Examples

### Example 1 - Describe a security descriptor of a registry key

```
Sddl describe O:BAG:SYD:PAI(A;CI;KA;;;BA)(A;CI;KR;;;AU)(A;CI;KA;;;LS)(A;CI;KA;;;NS)(A;CI;KR;;;IU)(A;CI;KA;;;SY) -ObjectType RegistryKey
```

### Example 2 - Describe a binary security descriptor on a file

```
Sddl describe 010004805800000068000000000000001400000002004400030000000000140003000000010100000000000504000000000014000700000001010000000000050a00000000001400030000000101000000000005120000000102000000000005200000002002000001020000000000052000000020020000
```
# Sddl lookupguid
Looks up a GUID representing an AD extended right, property, or property set

## Synopsis
**Sddl lookupguid** [*options*] &lt;*Guid*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Guid*&gt;||&lt;*Guid[]*&gt;|GUID of interest|


## Options


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
||||  **Guid**|
||||  **Kind**|
||||  **Name**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Examples

### Example 1 - Look up Logon Information and Account Restrictions property sets

```
Sddl lookupguid 5f202010-79a5-11d0-9020-00c04fc2d4cf, 4c164200-20c0-11d0-a768-00aa006e0529
```
# Sddl lookupwks
Looks up a well-known SID

## Synopsis
**Sddl lookupwks** [*options*] &lt;*SidOrWks*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*SidOrWks*&gt;||&lt;*SecurityIdentifier[]*&gt;|SID or WKS of interest|


## Options


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
||||  **Wks**|
||||  **Sid**|
|    **-OutputHeaders**||&lt;*SwitchParam*&gt;|Print headers for table/list/CSV/TSV styles|
||||  Default: True|
|    **-Verbose**|**-V**|&lt;*SwitchParam*&gt;|Prints verbose messages|


## Examples

### Example 1 - Looks up a SID and WKS

```
Sddl lookupwks DA, S-1-18-1
```

### Example 2 - Looks up a domain placeholder SID

```
Sddl lookupwks S-1-5-21-<domain>-512
```
