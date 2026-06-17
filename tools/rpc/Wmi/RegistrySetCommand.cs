using Microsoft.Win32;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Security.Kerberos;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Cli.Registry;
using Titanis;
using Titanis.Winterop.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wmi.Registry
{

	/// <task category="Registry">Set a value in a registry key</task>
	/// <task category="Registry">Create a registry key</task>
	[Command]
	[Description("Sets one or more values in a registry key")]
	[DetailedHelpText(@"This command accepts one or more key/value specifications, allowing multiple keys to be created and multiple values to be set.  When a key name is encountered, the key is created, and subsequent values are set in this key.

Keys are specified as:

  <root>\<key>

or

  <root>/<key>

The initial path separator following the root is interpreted as the path separator.  When using the second syntax, all `/` in the path are interpreted as path separators and replaced with `\` before sending to the remote server.  If you intend to include a `/` in a key name, you must use the first syntax.  To specify a root key itself, follow the root key name with a slash with no key name

Values are specified as:

  <type>[;<encoding>]:[<value>]=<data>

The <type> may be specified either as a number (decimal or hex), or as one of the familiar REG_ values (with or without the `REG_` prefix).

The value name is interpreted as a C-style string, interpreting character escapes.  Since the `=` denotes the end of the value name and the beginning of <data>, you must escape `=` in the value name with a preceding backslash.  To specify the default value in a key, omit <value> altogether.  That is, to set the default value on a key to `whatever`:

  sz:=whatever

The format of <data> depends on the encoding.  The encoding may be specified after the value type.  If no encoding is specified, the default encoding for the value type is assumed (table below).

| Encoding   | Description                                        | Examples    |
|------------|----------------------------------------------------|-------------|
| C          | UTF-16 with C-style escapes                        | Test\r\n    |
| Cz         | UTF-16 with C-style escapes (null terminated)      | Test\r\n    |
| Hex        | Hex-encoded bytes                                  | 0123b5      |
| Dword      | Decimal, hex (0x prefix), or binary (0b prefix)    | 42          |
|            | (encoded as little-endian)                         | 0x2A        |
|            |                                                    | 0b101010    |
| DwordBE    | Same as Dword but encoded as big-endian            | 42          |
| File       | Name of file to load data from                     | ./data.bin  |
| Sddl       | SDDL converted to binary form                      |             |
| Utf16      | String						                      | Test        |
| Utf16z     | String, null terminated						      | Test        |
| Multi[sep] | Multi String with <sep> as a separator (default ,) | A,Multi str |

The only difference between Utf16 and Utf16z is that Utf16z ensures the string ends with a null terminator.  When `file` is used, the data is loaded from the file as-is, regardless of the value type.  This means using `file` with SZ or MULTI_SZ will not convert an ASCII file to UTF-16, nor strip the byte order mark (if present), nor convert newlines to \0 separators; the file must be prepared and formatted properly before running this command.
When using Multi you can change the separator from , by specifying it directly after Multi.  For example Multi^ uses ^ to separate each string.


Default encodings for value types:

| Value Type       | Default Encoding |
|------------------|------------------|
| BINARY           | Hex              |
| DWORD            | Dword            |
| EXPAND_SZ        | Utf16z           |
| MULTI_SZ         | Utf16            |
| QWORD            | Qword            |
| SZ               | Utf16z           |

NOTE: Here are some restrictions imposed by the WMI provider

WMI does not allow you to set or retrieve values other REG_BINARY, REG_DWORD, REG_EXPAND_SZ, REG_MULTI_SZ,
REG_SZ, REG_QWORD. Doing so will result in a validation error.  
Numeric types will always be sent as the appropriate number of bytes.
String values are always terminated with exactly one null terminator, regardless of how the string is specified.
Keys under HKCU are only accessible if the user profile for the impersonated user is already loaded; WMI does not load the user's profile. If the profile is not previously loaded, WMI returns the error ERROR_INVALID_PARAMETER
If you require more flexibility consider using the Titanis Reg tool.
")]
	[Example("Setting a few values", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment sz:=DefaultValueData dword:DwordValue=42 binary;sddl:ValueContainingPermissions=O:BAG:BAD:(A;;0x1F;;;AU)")]
	[Example("Setting values in multiple keys", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment/Key1 sz:=This-is-in-key-1 HKCU/SOFTWARE/Experiment/Key2 sz:=DefaultValueData-Key2")]
	[Example("Setting a value with a numeric-specified type", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment 2:ExpandStringWithNumericType=ABCD1234 2;utf16z:ExpandStringWithNumericTypeAsUtf16z=Set-as-a-normal-string", "The type of the value is specified as a number.  Even though it corresponds to REG_EXPAND_SZ, the default encoding is assumed to be hex.  This can be overridden to specify it as a UTF-16 string or any other encoding")]
	[Example("Setting a mismatched values", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment sz:=DefaultValueData dword:DwordValue=42 binary;dword:DwordAsBinary=42 dword;hex:BinaryAsDword=DF00529F sz;hex:hexString=410042004300", "This example demonstrates mixing different encodings with different value types.")]
	[Example("Setting DCOM properties", @"{0} LUMON-FS1 HKLM/SOFTWARE/Classes/AppID/{00000000-1234-0000-0000-000000000000} sz:=MyDcomApp binary;sddl:LaunchPermissions=O:BAG:BAD:(A;;0x1F;;;AU) HKLM/SOFTWARE/Classes/CLSID/{00000000-1234-0000-0000-000000000000} sz:=ComponentClass sz:AppId={00000000-1234-0000-0000-000000000000}")]
	[Example("Setting a value on a root key", @"{0} LUMON-FS1 HKCU/ sz:SomeValue=data")]
	[Example("Create a key with no values", @"{0} LUMON-FS1 HKLM/SOFTWARE/MDR")]
	[Example("Create a multi string value", @"{0} LUMON-FS1 HKLM/SOFTWARE/MDR ""multi_sz:Tempers=Woe,Dread,Frolic,Malice""")]
	internal class RegistrySetCommand : WmiRegistryCommandBase
	{
		[Parameter(20)]
		[Description("Keys and values to set")]
		//[Mandatory]
		public RegistryItemSpec[] Items { get; set; }


		private List<RegistryKeyGroup> _keys;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			var planner = new RegistryPlanner(this.Log);
			var initialKey = new RegistryKeySpec(keyPath.Root, keyPath.KeyPath, null);
			initialKey.Accept(planner);
			foreach (var item in this.Items)
			{
				item.Accept(planner);
			}

			this._keys = planner.keys;
			//Unlike reg we MUST use well defined types, there is no "other" for us
			foreach (var keyGroup in this._keys)
			{
				foreach (var valueSpec in keyGroup.values)
				{
					if ((int)valueSpec.ValueType < 1 || ((int)valueSpec.ValueType > 7 && (int)valueSpec.ValueType != 11))
					{
						context.LogError($"{valueSpec.ValueName} has an unsupported Value Type of {valueSpec.ValueType}.  Use a standard type for registry interactions over WMI.");
					}
					if (valueSpec.ValueType == RegistryValueType.DwordBE)
					{
						context.LogError($"{valueSpec.ValueName} has an unsupported Value Type of {valueSpec.ValueType}.  Use DWORD (LE instead of BE) as WMI does not support DwordBE");
					}
				}
			}

		}

		protected override async Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken)
		{
			foreach (var keyGroup in this._keys)
			{


				var keySpec = keyGroup.key;
				if (!string.IsNullOrEmpty(keyGroup.key.KeyPath))
				{
					this.WriteDiagnostic($"Creating subkey {keySpec}");
					//This does not return ERROR_ALREADY_EXISTS if the key already exists, it simply returns ERROR_SUCCESS for both create and exists
					((Win32ErrorCode)(await registry.CreateKey((uint)keySpec.Root, keySpec.KeyPath).ConfigureAwait(false)).ReturnValue).CheckAndThrow();
					this.WriteMessage($"Created subkey {keySpec}");
				}

				foreach (var valueSpec in keyGroup.values)
				{
					string valueName = (valueSpec.ValueName == null || valueSpec.ValueName == string.Empty) ? "" : valueSpec.ValueName;
					uint rootAsUint = (uint)keySpec.Root;


					this.WriteDiagnostic($"Setting value '{valueSpec.ValueName}' to {valueSpec.ValueData.ToHexString()}");
					//What we have is data that has undergone whatever encoding the user asked for or was read from a file
					//We need to shift that into a matching type before using it.
					RegistryData d = valueSpec.ValueType switch
					{
						RegistryValueType.String => RegistryData.CreateString(Encoding.Unicode.GetString(valueSpec.ValueData)),
						RegistryValueType.ExpandString => RegistryData.CreateExpandableString(Encoding.Unicode.GetString(valueSpec.ValueData)),
						RegistryValueType.Binary => RegistryData.CreateBinary(valueSpec.ValueData),
						RegistryValueType.DwordLE => RegistryData.CreateDword(BinaryPrimitives.ReadUInt32LittleEndian(valueSpec.ValueData)),
						RegistryValueType.MultiString => RegistryData.CreateRegMultiString(valueSpec.ValueData),
						RegistryValueType.Qword => RegistryData.CreateQword(BinaryPrimitives.ReadUInt64LittleEndian(valueSpec.ValueData)),
						_ => throw new InvalidProgramException("Validation should prevent this error")
					};

					((Win32ErrorCode)(d switch
					{
						RegistryString t => (await registry.SetStringValue(rootAsUint, keySpec.KeyPath, valueName, t.Value)).ReturnValue,
						RegistryExpandableString t => (await registry.SetExpandedStringValue(rootAsUint, keySpec.KeyPath, valueName, t.Value)).ReturnValue,
						RegistryBinary t => (await registry.SetBinaryValue(rootAsUint, keySpec.KeyPath, valueName, t.Bytes)).ReturnValue,
						RegistryDword t => (await registry.SetDWORDValue(rootAsUint, keySpec.KeyPath, valueName, t.Value)).ReturnValue,
						RegistryMultiString t => (await registry.SetMultiStringValue(rootAsUint, keySpec.KeyPath, valueName, t.Strings.ToArray())).ReturnValue,
						RegistryQword t => (await registry.SetQWORDValue(rootAsUint, keySpec.KeyPath, valueName, t.Value)).ReturnValue,
						_ => throw new InvalidProgramException("Validation should prevent this error")
					}
						)).CheckAndThrow();
					this.WriteMessage($"Set Value {keySpec} {valueName}");
				}

			}

			return 0;
		}
	}
}
