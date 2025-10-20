using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Winterop;

namespace Wmi.Registry
{
	[Description("Delete a registry key or value")]
	[Example(@"Delete the registry key HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp")]
	[Example(@"Delete all values under the registry key HKLM\Software\MyApp, leaving the empty key", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -DeleteAllValues")]
	[Example(@"Delete the registry value 'InstallPath' under HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -ValueName InstallPath")]
	[Example(@"Delete the registry key HKLM\Software\MyApp and all subkeys under it", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -Recursive")]
	internal class RegistryDeleteCommand : WmiRegistryCommandBase
	{
		[Parameter]
		[Description("Name of value to delete (defaults to delete all)")]
		public string? ValueName { get; set; }

		[Parameter]
		[Description("Delete (default) value")]
		public SwitchParam ValueEmpty { get; set; }

		[Parameter]
		[Description("Delete all values")]
		public SwitchParam DeleteAllValues { get; set; }

		[Parameter]
		[Description("Recursively delete all subkeys and values")]
		public SwitchParam Recursive { get; set; }

		[Parameter]
		[Description("Continue deleting subkeys even if errors are encountered")]
		public SwitchParam ContinueOnError { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			if ((
					(ValueName != null ? 1 : 0)
					+ (ValueEmpty.IsSet ? 1 : 0)
					+ (DeleteAllValues.IsSet ? 1 : 0)
					+ (Recursive.IsSet ? 1 : 0)
				) > 1)
			{
				context.LogError($"Only one of -{nameof(ValueName)}, -{nameof(ValueEmpty)}, -{nameof(DeleteAllValues)} or -{nameof(Recursive)} can be specified.");
			}
			if (ValueEmpty.IsSet)
			{
				ValueName = string.Empty;
			}
		}

		internal record DeleteResults
		{
			internal bool ErrorOccured { get; set; }
			internal int KeysSeen { get; set; }
			internal int KeysDeleted { get; set; }

			internal DeleteResults()
			{
				ErrorOccured = false;
				KeysSeen = 0;
				KeysDeleted = 0;
			}
		}

		protected private async Task DeleteKey(dynamic registry, uint hive, string keyPath, CancellationToken cancellationToken, bool recursive, DeleteResults results)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}
			if (recursive)
			{
				this.WriteVerbose($"Enumerating subkeys of '{keyPath}' for recursive deletion");
				string[] names;
				try
				{
					var keyNames = await registry.EnumKey(hive, keyPath);
					((Win32ErrorCode)keyNames.ReturnValue).CheckAndThrow();
					names = ((Array?)keyNames?.sNames)?.OfType<string>() ?? Array.Empty<string>();
				}
				catch (Win32Exception ex)
				{
					this.WriteError($"Failed to enumerate subkeys of '{keyPath}': {ex}");
					results.ErrorOccured = true;
					if (!ContinueOnError.IsSet)
					{
						throw;
					}
					return;
				}
				results.KeysSeen += names.Length;
				foreach (string name in names)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						return;
					}
					string subKeyPath = $"{keyPath}\\{name}";
					this.WriteDiagnostic($"Recursively deleting subkey '{subKeyPath}'");
					try
					{
						await DeleteKey(registry, hive, subKeyPath, cancellationToken, true, results);
					}
					catch (Win32Exception)
					{
						results.ErrorOccured = true;
						if (!ContinueOnError.IsSet)
						{
							throw;
						}
						// Continue with next subkey
					}
				}
			}
			this.WriteVerbose($"Deleting key '{keyPath}'");
			try
			{
				((Win32ErrorCode)(await registry.DeleteKey(hive, keyPath)).ReturnValue).CheckAndThrow();
				results.KeysDeleted++;
				return;
			}
			catch (Win32Exception ex)
			{
				results.ErrorOccured = true;
				this.WriteError($"Failed to delete key '{keyPath}': {ex}");
				if (!ContinueOnError.IsSet)
				{
					throw;
				}
				return;
			}
		}

		protected override async Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken)
		{
			var rootAsUint = (uint)KeyPath.Root;

			if (ValueName != null)
			{
				this.WriteDiagnostic($"Deleting registry value '{KeyPath} : {ValueDisplayName(ValueName)}'");
				((Win32ErrorCode)(await registry.DeleteValue(rootAsUint, KeyPath.KeyPath, ValueName)).ReturnValue).CheckAndThrow();
				this.WriteMessage($"Deleted registry value '{KeyPath} : {ValueDisplayName(ValueName)}'");
				return 0;
			}
			else if (DeleteAllValues.IsSet)
			{
				this.WriteDiagnostic($"Enumerating registry values under '{KeyPath}'");
				var values = await registry.EnumValues(rootAsUint, KeyPath.KeyPath);
				((Win32ErrorCode)values.ReturnValue).CheckAndThrow();
				bool error = false;
				object[] items = values?.sNames ?? Array.Empty<string>();
				if (items.Length == 0)
				{
					this.WriteMessage($"No registry values found under '{KeyPath}'");
					return 0;
				}
				int valuesDeleted = 0;
				foreach (string val in items)
				{
					this.WriteDiagnostic($"Deleting value '{ValueDisplayName(val)}' under '{KeyPath}'");
					try
					{
						((Win32ErrorCode)(await registry.DeleteValue(rootAsUint, KeyPath.KeyPath, val)).ReturnValue).CheckAndThrow();
						valuesDeleted++;
						this.WriteVerbose($"Deleted registry value '{KeyPath} : {ValueDisplayName(val)}'");
					}
					catch (Win32Exception ex)
					{
						error = true;
						this.WriteError($"Failed to delete registry value '{KeyPath} : {ValueDisplayName(val)}': {ex}");
						if ((ContinueOnError.IsSet))
						{
							continue;
						}
						else
						{
							throw;
						}
					}
				}
				if (error)
				{
					this.WriteWarning($"One or more errors occurred while deleting values under '{KeyPath}'");
					this.WriteMessage($"Deleted {valuesDeleted} registry values under '{KeyPath}'");
					return 1;
				}
				else
				{
					this.WriteMessage($"Deleted all ({valuesDeleted}) registry values under '{KeyPath}'");
					return 0;
				}

			}
			else
			{
				var deleteResults = new DeleteResults();
				await DeleteKey(registry, rootAsUint, KeyPath.KeyPath, cancellationToken, Recursive.IsSet, deleteResults);
				if (cancellationToken.IsCancellationRequested)
				{
					this.WriteWarning($"Operation cancelled.  Deleted {deleteResults.KeysDeleted} prior to cancellation.");
					return 1;
				}
				if (deleteResults.ErrorOccured)
				{
					if (Recursive.IsSet)
					{
						this.WriteMessage($"Deleted {deleteResults.KeysDeleted} of {deleteResults.KeysSeen} observed subkeys under '{KeyPath}'");
					}
					else
					{
						this.WriteMessage($"Failed to delete registry key '{KeyPath}'");
					}
					return 1;
				}
				else
				{
					if (Recursive.IsSet)
					{
						this.WriteMessage($"Deleted {deleteResults.KeysDeleted} keys starting at '{KeyPath}'");
					}
					else
					{
						this.WriteMessage($"Deleted registry key '{KeyPath}'");
					}
				}
				return 0;
			}
		}
	}
}
