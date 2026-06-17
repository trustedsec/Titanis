using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;
using Titanis.Cli.Registry;

namespace Titanis.Msrpc.Msrrp.Cli
{

	/// <task category="Registry">Set a value in a registry key</task>
	/// <task category="Registry">Create a registry key</task>
	[Command]
	[Description("Sets one or more values in a registry key")]
	[DetailedHelpText(@"This command accepts one or more key/value specifications, allowing multiple keys to be created and multiple values to be set.  When a key name is encountered, the key is created, and subsequent values are set in this key.  Once the next key name is encountered, the previous key is closed, and the new one created.  Specifying the same key name multiple times causes the key to be closed and reopened.

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

| Encoding | Description                                     | Examples   |
|----------|-------------------------------------------------|------------|
| C        | UTF-16 with C-style escapes                     | 0123b5     |
| Cz       | UTF-16 with C-style escapes (null terminated)   | 0123b5     |
| Hex      | Hex-encoded bytes                               | 0123b5     |
| Dword    | Decimal, hex (0x prefix), or binary (0b prefix) | 42         |
|          | (encoded as little-endian)                      | 0x2A       |
|          |                                                 | 0b101010   |
| DwordBE  | Same as Dword but encoded as big-endian         | 42         |
| File     | Name of file to load data from                  | ./data.bin |
| Sddl     | SDDL converted to binary form                   |            |
| Utf16    | String with C-style escapes                     | Test\r\n   |
| Utf16z   | String with C-style escapes, null terminated    | Test\r\n   |

The only difference between Utf16 and Utf16z is that Utf16z ensures the string ends with a null terminator.  When `file` is used, the data is loaded from the file as-is, regardless of the value type.  This means using `file` with SZ or MULTI_SZ will not convert an ASCII file to UTF-16, nor strip the byte order mark (if present), nor convert newlines to \0 separators; the file must be prepared and formatted properly before running this command.


Default encodings for value types:

| Value Type       | Default Encoding |
|------------------|------------------|
| (any numeric)    | Hex              |
| BINARY           | Hex              |
| DWORD            | Dword            |
| DWORD_BIG_ENDIAN | DwordBE          |
| EXPAND_SZ        | Utf16z           |
| MULTI_SZ         | Utf16            |
| QWORD            | Qword            |
| SZ               | Utf16z           |
| (other)          | Binary           |

")]
	[Example("Setting a few values", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment sz:=DefaultValueData dword:DwordValue=42 binary;sddl:ValueContainingPermissions=O:BAG:BAD:(A;;0x1F;;;AU)")]
	[Example("Setting values in multiple keys", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment/Key1 sz:=This-is-in-key-1 HKCU/SOFTWARE/Experiment/Key2 sz:=DefaultValueData-Key2")]
	[Example("Setting a value with a numeric-specified type", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment 2:ExpandStringWithNumericType=ABCD1234 2;utf16z:ExpandStringWithNumericTypeAsUtf16z=Set-as-a-normal-string", "The type of the value is specified as a number.  Even though it corresponds to REG_EXPAND_SZ, the default encoding is assumed to be hex.  This can be overridden to specify it as a UTF-16 string or any other encoding")]
	[Example("Setting a mismatched values", @"{0} LUMON-FS1 HKCU/SOFTWARE/Experiment sz:=DefaultValueData dword:DwordValue=42 binary;dword:DwordAsBinary=42 dword;hex:BinaryAsDword=DF00529F dword;hex:IncompleteDword=2A none:NoneValueWithData=1234ABCD", "This example demonstrates mixing different encodings with different value types.  Some of them are logically invalid, but still permitting by the Registry API.")]
	[Example("Setting DCOM properties", @"{0} LUMON-FS1 HKLM/SOFTWARE/Classes/AppID/{00000000-1234-0000-0000-000000000000} sz:=MyDcomApp binary;sddl:LaunchPermissions=O:BAG:BAD:(A;;0x1F;;;AU) HKLM/SOFTWARE/Classes/CLSID/{00000000-1234-0000-0000-000000000000} sz:=ComponentClass sz:AppId={00000000-1234-0000-0000-000000000000}")]
	[Example("Setting a value on a root key", @"{0} LUMON-FS1 HKCU/ sz:SomeValue=data")]
	internal partial class SetValueCommand : RegistryCommand
	{
		[Parameter(20)]
		[Description("Keys and values to set")]
		//[Mandatory]
		public RegistryItemSpec[] Items { get; set; }

		private List<RegistryKeyGroup> _keys;
		private Dictionary<PredefinedKey, RegistryKey> _cachedRoots = new Dictionary<PredefinedKey, RegistryKey>();

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			var planner = new RegistryPlanner(this.Log);
			foreach (var item in this.Items)
			{
				item.Accept(planner);
			}

			this._keys = planner.keys;
		}

		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var options = this.KeyOptions;
			var rootAccess = this.BackupSemantics.IsSet ? RegistryAccessRights.QueryValue : RegistryAccessRights.CreateSubkey;



			foreach (var keyGroup in this._keys)
			{
				if (keyGroup.access == RegistryAccessRights.None)
					keyGroup.access = RegistryAccessRights.ReadControl;

				RegistryKey? key = null;
				{
					var keySpec = keyGroup.key;
					if (string.IsNullOrEmpty(keyGroup.key.KeyPath))
					{
						if (!_cachedRoots.ContainsKey(keySpec.Root))
						{
							// This targets the root itself
							this.WriteDiagnostic($"Opening root {keySpec.Root}");
							key = await client.OpenRootKey(keySpec.Root, keyGroup.access, cancellationToken);
							_cachedRoots.Add(keySpec.Root, key);
						}
						else
						{
							key = _cachedRoots[keySpec.Root];
						}
					}
					else
					{
						this.WriteDiagnostic($"Opening root {keySpec.Root}");
						RegistryKey rootKey;
						if (!_cachedRoots.ContainsKey(keySpec.Root))
						{
							bool isSubkey = keySpec.KeyPath.Contains('\\');
							try
							{
								rootKey = await client.OpenRootKey(keySpec.Root, isSubkey ? RegistryAccessRights.EnumerateSubkeys : rootAccess, cancellationToken);
							}
							catch (Win32Exception ex) when ((Win32ErrorCode)ex.NativeErrorCode == Win32ErrorCode.ERROR_ACCESS_DENIED)
							{
								// Try again without Create access; this will still allow the user to set values in an existing key
								rootKey = await client.OpenRootKey(keySpec.Root, RegistryAccessRights.EnumerateSubkeys, cancellationToken);
							}
							_cachedRoots.Add(keySpec.Root, rootKey);
						}
						else
						{
							rootKey = _cachedRoots[keySpec.Root];
						}


						this.WriteDiagnostic($"Creating subkey {keySpec}");
						key = await rootKey.CreateSubkey(keySpec.KeyPath, keyGroup.access, options, cancellationToken);
						this.WriteMessage($"Created subkey {keySpec}");

					}
				}

				await using (key)
				{
					foreach (var valueSpec in keyGroup.values)
					{
						this.WriteDiagnostic($"Setting value '{valueSpec.ValueName}' to {valueSpec.ValueData.ToHexString()}");
						await key.SetValue(valueSpec.ValueName, valueSpec.ValueType, valueSpec.ValueData, cancellationToken);
						this.WriteMessage($"Set Value {keyGroup.key} {valueSpec.ValueName}");
					}
				}
			}
			foreach (var item in this._cachedRoots)
			{
				item.Value.Dispose();
			}
			return 0;
		}
	}



}
