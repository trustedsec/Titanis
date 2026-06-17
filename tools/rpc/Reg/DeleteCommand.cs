using System.ComponentModel;
using Titanis.Cli;
using Titanis.Cli.Registry;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <task category="Registry">Delete registry keys and values</task>
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
	internal class DeleteCommand : RegistryCommand
	{
		[Parameter(20)]
		[Description("Keys and values to delete")]
		[ValueNameOnly]
		//[Mandatory]
		public RegistryItemSpec[] Items { get; set; }

		[Parameter]
		[Description("Delete keys that have no values specified")]
		public SwitchParam DeleteKeys { get; set; }

		[Parameter]
		[Description("Continue even if a deletion fails")]
		public SwitchParam ContinueOnError { get; set; }

		private List<RegistryKeyGroup> _keys;
		private uint _keysDeleted = 0;
		private uint _valuesDeleted = 0;
		//CachedRoots
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
			foreach (var keyGroup in this._keys)
			{
				if (keyGroup.values.Count == 0 && !DeleteKeys.IsSet)
				{
					context.LogError($"{keyGroup.key.Root}\\{keyGroup.key.KeyPath} specified without values. Either add -DeleteKeys or specify the values under this key to remove");
				}
			}

		}

		private async Task<Win32Exception?> DeleteSingleKey(RegistryKey key, string subKeyPath, CancellationToken cancellationToken)
		{
			string target = RegistryPath.Combine(key.KeyPath, subKeyPath) ?? "";
			WriteDiagnostic($"Deleting key {target}");
			try
			{
				await key.DeleteKey(subKeyPath, cancellationToken).ConfigureAwait(false);
				WriteVerbose($"Deleted key '{target}'");
				_keysDeleted++;
				return null;
			}
			catch (Win32Exception ex)
			{
				return ex;
			}
		}

		private async Task<bool> DeleteKey(RegistryKey key, string subkeyPath, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return false;
			}
			var deleteStatus = await DeleteSingleKey(key, subkeyPath, cancellationToken);
			if (deleteStatus == null)
			{
				return true;
			}
			WriteDiagnostic($"Enumerating subkeys of {key} for recursive deletion");
			RegistryKey EnumKey;
			try
			{
				EnumKey = await key.OpenSubkey(subkeyPath, RegistryAccessRights.EnumerateSubkeys | RegistryAccessRights.QueryValue | RegistryAccessRights.Delete, KeyOptions, cancellationToken).ConfigureAwait(false);
			}
			catch (Win32Exception ex)
			{
				WriteWarning($"Failed to open {subkeyPath} for subkey enumeration");
				if (!ContinueOnError.IsSet)
				{
					throw;
				}
				//If we're continuing, Lets retry the open without Delete rights, while the deletion as a whole won't work this allows us to still try getting as much coverage as we can.
				try
				{
					EnumKey = await key.OpenSubkey(subkeyPath, RegistryAccessRights.EnumerateSubkeys | RegistryAccessRights.QueryValue, KeyOptions, cancellationToken).ConfigureAwait(false);
				}
				catch (Win32Exception)
				{
					return false;
				}
			}
			await using (EnumKey)
			{
				var subKeyNames = await EnumKey.GetSubkeyNames(cancellationToken).ToArray(cancellationToken);
				foreach (var subKeyName in subKeyNames)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						return false;
					}
					try
					{
						_ = await DeleteKey(EnumKey, subKeyName.KeyName, cancellationToken);
					}
					catch (Win32Exception ex) when ((Win32ErrorCode)ex.NativeErrorCode == Win32ErrorCode.ERROR_FILE_NOT_FOUND)
					{
						WriteVerbose($"{EnumKey}\\{subKeyName.KeyName} was not found");
					}
					catch (Exception)
					{
						if (!ContinueOnError.IsSet)
						{
							throw;
						}
						// Continue with next subkey
					}
				}
			}

			deleteStatus = await DeleteSingleKey(key, subkeyPath, cancellationToken).ConfigureAwait(false);
			if (deleteStatus != null)
			{
				if (!ContinueOnError.IsSet)
				{
					throw deleteStatus;
				}
				this.WriteError($"Failed to delete key '{RegistryPath.Combine(key.KeyPath, subkeyPath)}' : {deleteStatus}");
				return false;
			}
			return true;
		}

		protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			foreach (var keyGroup in this._keys)
			{
				var keySpec = keyGroup.key;
				RegistryKey rootKey;
				if (string.IsNullOrEmpty(keySpec.KeyPath) && keyGroup.values.Count == 0)
				{
					WriteWarning($"Can't delete a root key, skipping");
					continue;
				}

				if (!_cachedRoots.ContainsKey(keySpec.Root))
				{
					rootKey = await client.OpenRootKey(keySpec.Root, RegistryAccessRights.Delete | RegistryAccessRights.SetValue, cancellationToken).ConfigureAwait(false);
					_cachedRoots.Add(keySpec.Root, rootKey);
				}
				else
				{
					rootKey = _cachedRoots[keySpec.Root];
				}

				if (keyGroup.values.Count == 0)
				{
					WriteDiagnostic($"Deleting key {keySpec}");
					uint deleteCount = _keysDeleted;
					var result = await DeleteKey(rootKey, keySpec.KeyPath!, cancellationToken);
					if (!result && deleteCount != _keysDeleted)
					{
						WriteWarning($"Delete of {keySpec} was partially completed. Validate its state if necessary.");
					}
					else if (result)
					{
						WriteVerbose($"Deleted key {keySpec}");
					}
				}
				else
				{
					RegistryKey baseKey;
					if (string.IsNullOrEmpty(keySpec.KeyPath))
					{
						baseKey = rootKey;
					}
					else
					{
						WriteDiagnostic($"Opening subkey {keySpec} to delete contained values");
						try
						{
							baseKey = await rootKey.OpenSubkey(keySpec.KeyPath!, RegistryAccessRights.SetValue, KeyOptions, cancellationToken).ConfigureAwait(false);
						}
						catch (Win32Exception ex)
						{
							if (ContinueOnError.IsSet)
							{
								WriteWarning($"Failed to open {keySpec} : {ex.Message}");
								continue;
							}
							else
							{
								throw;
							}
						}
					}

					foreach (var valueSpec in keyGroup.values)
					{
						try
						{
							await baseKey.DeleteValue(valueSpec.ValueName, cancellationToken).ConfigureAwait(false);
							_valuesDeleted++;
						}
						catch (Win32Exception ex)
						{
							if (ContinueOnError.IsSet)
							{
								WriteWarning($"Failed to delete value {keySpec} {valueSpec.ValueName} : {ex.Message}");
							}
							else
							{
								throw;
							}
						}
					}

					if (baseKey != rootKey)
					{
						baseKey.Dispose();
					}
				}
			}
			foreach (var item in this._cachedRoots)
			{
				item.Value.Dispose();
			}

			WriteMessage($"Deleted {_keysDeleted} keys.");
			WriteMessage($"Deleted {_valuesDeleted} of the requested values.");
			return 0;
		}
	}
}
