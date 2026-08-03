# Cert
Work with X.509 certificates

## Synopsis
```
Cert <subcommand>
```

### Subcommands

|Command|Description|
|-|-|
|**[selfcert](#cert-selfcert)**|Create a self-signed certificate|


For help on a subcommand, use `Cert <subcommand> -h`
# Cert selfcert
Create a self-signed certificate

## Synopsis
**Cert selfcert** [*options*]** -PfxFileName** &lt;*FileSpec* &gt; &lt;*Subject*&gt;

## Parameters

|Name|Aliases|Value|Description|
|-|-|-|-|
|&lt;*Subject*&gt;||&lt;*String*&gt;|Subject name as an X.500 string|


## Options


|Name|Aliases|Value|Description|
|-|-|-|-|
|    **-CertFileName**||&lt;*FileSpec*&gt;|Name of certificate file (.pem or .cer)|
|    **-HashAlgorithm**||&lt;*HashAlgorithm*&gt;|Hash algorithm|
||||Possible values:|
||||  **Md5**|
||||  **Sha1**|
||||  **Sha256**|
||||  **Sha384**|
||||  **Sha512**|
||||  **Sha3_256**|
||||  **Sha3_384**|
||||  **Sha3_512**|
|**-K**, **-KeySizeBits**||&lt;*Int32*&gt;|Size of key, in bits|
||||  Default: 2048|
|**-P**, **-PfxFileName**||&lt;*FileSpec*&gt;|Name of .pfx file|
|    **-SubjectAltName**||&lt;*String*&gt;|Subject alternate name|
|**-T**, **-TemplateFile**||&lt;*FileSpec*&gt;|Name of file containing certificate to copy|


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
|    **-HumanReadable**||&lt;*SwitchParam*&gt;|Formats file sizes as human-readable values|
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

