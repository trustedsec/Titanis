using System.ComponentModel;
using Titanis;
using Titanis.Cli;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Cli.Registry;

namespace Wmi.Registry
{
	/// <task category="WMI;Registry">Delete registry keys and values</task>
	[Command]
	[Description("Deletes one or more registry keys and/or values")]
	[DetailedHelpText(@"This command accepts one or more key/value specifications, allowing multiple keys and/or values to be deleted in a single execution
Keys are specified as:

  <root>\<key>

or

  <root>/<key>

The initial path separator following the root is interpreted as the path separator.  When using the second syntax, all `/` in the path are interpreted as path separators and replaced with `\` before sending to the remote server.  If you intend to include a `/` in a key name, you must use the first syntax.  To specify a root key itself, follow the root key name with a slash with no key name

Values are specified by their name, and must follow the key they are contained within.

To delete a key, specify its name and do not follow it with any value names.  Key deletion is recursive, and requires -DeleteKeys to be specified with the command.

By default, deletion stops on the first encountered error. There is no automated rollback.  If you would like to continue attempting to delete values even after an error occurs specify -ContinueOnError.
")]
	[Example(@"Delete the registry key HKLM\Software\MyApp and all subkeys under it", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\MyApp -DeleteKeys")]
	[Example(@"Delete the registry value 'InstallPath', 'Version' and 'Company Name' under HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\MyApp InstallPath Version ""Company Name""", @"HKLM\Software\MyApp is not deleted, just the values ""InstallPath"", ""Version"" and ""Company Name""")]
	[Example(@"Delete the registry key HKLM\Software\MyApp, HKLM\Software\YourApp and the values 'InstallPath', 'Version', and 'Company Name' under HKLM\Software\TheirApp", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\YourApp HKLM\Software\TheirApp InstallPath Version ""Company Name"" HKLM\Software\MyApp",
		@"Fully deletes the registry keys ""YourApp"" and ""MyApp"".  ""TheirApp"" is not deleted in its entirety, only ""InstallPath"", ""Version"", and ""Company Name"" are deleted.")]
	internal class RegistryDeleteCommand : WmiRegistryCommandBase
	{
		[Parameter(20)]
		[Description("Keys and values to set")]
		[ValueNameOnly]
		//[Mandatory]
		public RegistryItemSpec[] Items { get; set; }

		[Parameter]
		[Description("Delete keys that have no values specified")]
		public SwitchParam DeleteKeys { get; set; }




		private List<RegistryKeyGroup> _keys;
		private uint _keysDeleted = 0;
		private uint _valuesDeleted = 0;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			var planner = new RegistryPlanner(this.Log);
			var initialKey = new RegistryKeySpec(keyPath.Root, keyPath.KeyPath, null);
			initialKey.Accept(planner);
			if (this.Items != null)
			{
				foreach (var item in this.Items)
				{
					item.Accept(planner);
				}
			}

			this._keys = planner.keys;
			//Unlike reg we MUST use well defined types, there is no "other" for us
			foreach (var keyGroup in this._keys)
			{
				if (keyGroup.values.Count == 0 && !DeleteKeys.IsSet)
				{
					context.LogError($"{keyGroup.key.Root}\\{keyGroup.key.KeyPath} specified without values. Either add -DeleteKeys or specify the values under this key to remove");
				}
			}
		}

		private async Task<Win32Exception?> DeleteSingleKey(object stdregprov, RegistryPath key)
		{
			dynamic registry = stdregprov;
			WriteDiagnostic($"Deleting key '{keyPath}'");
			try
			{
				((Win32ErrorCode)(await registry.DeleteKey((uint)key.Root, key.KeyPath)).ReturnValue).CheckAndThrow();
				WriteVerbose($"Deleted key '{keyPath}'");
				_keysDeleted++;
				return null;
			}
			catch (Win32Exception ex)
			{
				return ex;
			}
		}

		private async Task<bool> DeleteKey(object stdregprov, RegistryPath key, CancellationToken cancellationToken)
		{
			dynamic registry = stdregprov;
			uint predefKey = (uint)key.Root;
			if (cancellationToken.IsCancellationRequested)
			{
				return false;
			}
			var deleteStatus = await DeleteSingleKey(stdregprov, key).ConfigureAwait(false);
			if (deleteStatus == null)
			{
				return true;
			}
			//If the deletion didn't work we'll check for subkeys and try again.
			WriteDiagnostic($"Enumerating subkeys of {key} for recursive deletion");
			string[] names;
			try
			{
				var keyNames = await registry.EnumKey(predefKey, key.KeyPath);
				((Win32ErrorCode)keyNames.ReturnValue).CheckAndThrow();
				names = ((Array?)keyNames?.sNames)?.OfType<string>().ToArray() ?? Array.Empty<string>();
			}
			catch (Win32Exception ex)
			{
				WriteError($"Failed to enumerate subkeys of {key}: {ex}");
				if (!ContinueOnError.IsSet)
				{
					throw;
				}
				return false;
			}
			foreach (string name in names)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return false;
				}
				var subKey = key.Append(name);
				WriteDiagnostic($"Recursively deleting subkey '{subKey}'");
				try
				{
					_ = await DeleteKey(stdregprov, subKey, cancellationToken);
				}
				catch (Win32Exception)
				{
					if (!ContinueOnError.IsSet)
					{
						throw;
					}
					// Continue with next subkey
				}
			}
			deleteStatus = await DeleteSingleKey(stdregprov, key);
			if (deleteStatus != null)
			{
				if (!ContinueOnError.IsSet)
				{
					throw deleteStatus;
				}
				this.WriteError($"Failed to delete key '{key}': {deleteStatus}");
				return false;
			}
			return true;
		}

		protected override async Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken)
		{
			foreach (var keyGroup in this._keys)
			{
				var keySpec = keyGroup.key;
				if (string.IsNullOrEmpty(keySpec.KeyPath) && keyGroup.values.Count == 0)
				{
					WriteWarning($"Can't delete a root key, skipping");
					continue;
				}
				if (keyGroup.values.Count == 0)
				{
					WriteDiagnostic($"Deleting key {keySpec.Root}");
					uint deleteCount = _keysDeleted;
					var result = await DeleteKey((object)registry, new RegistryPath(keySpec.Root, keySpec.KeyPath), cancellationToken);
					if (!result && deleteCount != _keysDeleted)
					{
						WriteWarning($"Delete of {keySpec} was partially completed. Validate its state if necessary.");
					}
					else if (result)
					{
						WriteVerbose($"Deleted key {keySpec}");
					}

				}

				foreach (var valueSpec in keyGroup.values)
				{
					string valueName = (valueSpec.ValueName == null || valueSpec.ValueName == string.Empty) ? "" : valueSpec.ValueName;
					uint rootAsUint = (uint)keySpec.Root;
					try
					{
						this.WriteDiagnostic($"Deleting value {keySpec} {valueName}");
						((Win32ErrorCode)(await registry.DeleteValue((uint)keySpec.Root, keySpec.KeyPath, valueName).ConfigureAwait(false)).ReturnValue).CheckAndThrow();
						_valuesDeleted++;
						WriteVerbose($"Deleted value {keySpec} {valueName}");
					}
					catch (Win32Exception ex)
					{
						if (ContinueOnError.IsSet)
						{
							WriteWarning($"Failed to delete value {keySpec} {valueName} : {ex.Message}");
						}
						else
						{
							throw;
						}
					}
				}
			}
			WriteMessage($"Deleted {_keysDeleted} keys.");
			WriteMessage($"Deleted {_valuesDeleted} of the requested values.");

			return 0;
		}
	}
}
