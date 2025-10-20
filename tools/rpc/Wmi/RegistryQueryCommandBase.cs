using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis;
using Titanis.Asn1.Metadata;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Wmi.Registry
{
	/// <summary>
	/// Implements registry query functionality based off the semantics of reg.exe
	/// </summary>
	[DetailedHelpResource(typeof(Messages), nameof(Messages.wmi_base_query_Detailed))]
	[OutputFieldFormat(nameof(RegistryEntry.ValueName), null, typeof(ValueNameFormatter))]
	internal abstract class RegistryQueryCommandBase : WmiRegistryCommandBase
	{
		[Parameter]
		[Description($"Value name to query")]
		public string? ValueName { get; set; }

		[Parameter]
		[Description("Query default value")]
		[Alias("ve")]
		public SwitchParam ValueEmpty { get; set; }

		[Parameter]
		[Description("Query key and all subkeys")]
		[Alias("s")]
		public SwitchParam Recursive { get; set; }

		//UNDONE: Through WMI we cannot set the separator for REG_MULTI_SZ values, so this parameter is not implemented
		//[Parameter]
		//[Description("Separator character for REG_MULTI_SZ")]
		//[Alias("se")]
		//public char Separator { get; set; }


		//This can be an:
		// - Integer number (not hex)
		// - string using * (zero or more) or ?  (exactly one) wildcards
		[Parameter]
		[Description("Data or pattern to search for")]
		[Alias("f")]
		public string? SearchPattern { get; set; }

		[Parameter]
		[Description("Search key names")]
		[Alias("k")]
		public SwitchParam KeySearch { get; set; }

		[Parameter]
		[Description("Search value data")]
		[Alias("d")]
		public SwitchParam DataSearch { get; set; }

		[Parameter]
		[Description("Search value names")]
		public SwitchParam ValueSearch { get; set; }

		[Parameter]
		[Description("Case sensitive search")]
		[Alias("c")]
		public SwitchParam CaseSensitive { get; set; }

		[Parameter]
		[Description("Match exactly (no patterns)")]
		[Alias("e")]
		public SwitchParam Exact { get; set; }

		[Parameter]
		[Description("Filter value data type")]
		[Alias("t")]
		public RegistryValueKind? Type { get; set; }

        //TODO: WMI StdRegProv GetSecurityDescriptor does not currently work as expected.
        //[Parameter]
        //[Description("Queries key security descriptors")]
        //[Alias("sec")]
        //public SwitchParam GetSecurity { get; set; }

        private bool searchValues;
		private bool searchKeys;
		private bool searchData;
		private bool useExact;
		private string? printValueName;
		private ulong? integerSearchValue;
		Regex? searchPatternRegex;
		private (RegistryPath, SecurityDescriptor)? cachedSecurityDescriptor;


		protected abstract void WriteRegistryRecord(RegistryEntry entry);

		protected virtual void OnCommandComplete()
		{
			//No-op
		}

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (Exact.IsSet && string.IsNullOrEmpty(SearchPattern))
			{
				context.LogError($"-{nameof(Exact)} options require -{nameof(SearchPattern)} to be specified.");
			}

			if (ValueEmpty.IsSet && !string.IsNullOrEmpty(ValueName))
			{
				context.LogError($"The -{nameof(ValueEmpty)} option is mutually exclusive with -{nameof(ValueName)}.");
			}
			else
			{
				if (ValueEmpty.IsSet)
				{
					ValueName = string.Empty;
					printValueName = DefaultValueName;
				}
				else
				{
					printValueName = ValueName;
				}
			}

			//While reg.exe allows both a search pattern and a value name to be specified, the search pattern is ignored in that case, so we will error out here
			if (!string.IsNullOrEmpty(SearchPattern) && (!string.IsNullOrEmpty(ValueName)))
			{
				context.LogError($"The -{nameof(SearchPattern)} option cannot be used together with -{nameof(ValueEmpty)} or -{nameof(ValueName)}.");
			}

			//ValueName or ValueEmpty was specified.  This is the same as a SearchPattern of the valuename with Exact and SearchValues set
			if (ValueName != null)
			{
				if (DataSearch.IsSet || KeySearch.IsSet || ValueSearch.IsSet)
				{
					context.LogError($"The -{nameof(SearchPattern)}, -{nameof(DataSearch)}, -{nameof(KeySearch)} and -{nameof(ValueSearch)} options can not be used with -{nameof(ValueName)} or -{nameof(ValueEmpty)}");
				}
				useExact = true;
				SearchPattern = ValueName;
				searchValues = true;
				searchKeys = false;
				searchData = false;
			}
			else
			{
				useExact = Exact.IsSet;
				//We can't search without a search pattern
				if ((DataSearch.IsSet || KeySearch.IsSet || ValueSearch.IsSet) && string.IsNullOrEmpty(SearchPattern))
				{
					context.LogError($"The -{nameof(SearchPattern)} option must be specified when using -{nameof(DataSearch)}, -{nameof(KeySearch)} or -{nameof(ValueSearch)}.");
				}

				//if searching and value / key / data is not specified reg.exe defaults to key search so we do that here
				if (!(DataSearch.IsSet || KeySearch.IsSet || ValueSearch.IsSet) && !string.IsNullOrEmpty(SearchPattern))
				{
					searchKeys = true;
					searchData = false;
					searchValues = false;
					this.WriteDiagnostic("Defaulting to key search.");
				}
				else
				{
					searchValues = ValueSearch.IsSet;
					searchKeys = KeySearch.IsSet;
					searchData = DataSearch.IsSet;
				}

				if (!string.IsNullOrEmpty(SearchPattern))
				{
					if (ulong.TryParse(SearchPattern, NumberStyles.Integer, CultureInfo.InvariantCulture, out var val))
					{
						integerSearchValue = val;
					}
					else
					{
						integerSearchValue = null;
					}
					if (!useExact)
					{
						string regexPattern = ".*" + Regex.Escape(SearchPattern).Replace(@"\*", ".*").Replace(@"\?", ".") + ".*";
						searchPatternRegex = new Regex(regexPattern, CaseSensitive.IsSet ? RegexOptions.None : RegexOptions.IgnoreCase);
					}
				}
			}
		}

		private bool StringSearchMatches(string str)
		{
			if (useExact)
			{
				return str.Equals(SearchPattern, CaseSensitive.IsSet ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase);
			}
			else
			{
				return searchPatternRegex!.IsMatch(str);
			}
		}

		private bool DataSearchMatches(RegistryData data)
		{
			if (!searchData) return true;

			if (integerSearchValue is not null && data is IHaveUInt64Value uint64Value)
			{
				return uint64Value.UInt64Value == integerSearchValue.Value;
			}

			return StringSearchMatches(data.UntypedValue?.ToString());
		}

		private async Task<RegistryEntry?> GetRegistryValue(dynamic objreg, RegistryPath path, string valueName, RegistryValueKind regType, bool SearchData)
		{
			dynamic registry = objreg;
			var hive = (uint)KeyPath.Root;
			dynamic registryValue;
			try
			{
				registryValue = await ((Task<WmiInstanceObject>)(regType switch
				{
					RegistryValueKind.REG_SZ => registry.GetStringValue(hive, path.KeyPath, valueName),
					RegistryValueKind.REG_EXPAND_SZ => registry.GetExpandedStringValue(hive, path.KeyPath, valueName),
					RegistryValueKind.REG_DWORD => registry.GetDWORDValue(hive, path.KeyPath, valueName),
					RegistryValueKind.REG_MULTI_SZ => registry.GetMultiStringValue(hive, path.KeyPath, valueName),
					RegistryValueKind.REG_QWORD => registry.GetQWORDValue(hive, path.KeyPath, valueName),
					RegistryValueKind.REG_BINARY => registry.GetBinaryValue(hive, path.KeyPath, valueName),
					_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, $"Unsupported registry value type '{regType}' for value '{valueName}'"),
				})).ConfigureAwait(false);

				((Hresult)(registryValue).ReturnValue).CheckAndThrow();
			}
			catch (Win32Exception ex)
			{
				this.WriteWarning($"Failed to get value {ValueDisplayName(valueName)} under {path}: {ex.Message}");
				return null;
			}
			var data = regType switch
			{
				RegistryValueKind.REG_SZ => RegistryData.CreateString((string)registryValue.sValue),
				RegistryValueKind.REG_EXPAND_SZ => RegistryData.CreateExpandableString((string)registryValue.sValue),
				RegistryValueKind.REG_BINARY => RegistryData.CreateBinary(Array.ConvertAll((object[])registryValue.uValue, r => (byte)r)),
				RegistryValueKind.REG_DWORD => RegistryData.CreateDword((uint)registryValue.uValue),
				RegistryValueKind.REG_MULTI_SZ => RegistryData.CreateRegMultiString(Array.ConvertAll((object[])registryValue.sValue, r => (string)r)),
				RegistryValueKind.REG_QWORD => RegistryData.CreateDword((ulong)registryValue.uValue),
				_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, $"Unsupported registry value type '{regType}' for value '{valueName}'")
			};
			if (SearchData && !DataSearchMatches(data))
			{
				return null;
			}

			//TODO: WMI StdRegProv GetSecurityDescriptor does not currently work as expected.
			//if(this.GetSecurity.IsSet)
			//{
			//	if (cachedSecurityDescriptor is null || cachedSecurityDescriptor.Value.Item1 != path)
			//	{
			//		try
			//		{
			//			//why does these calls return 0x8004101D
			//			this.WriteDiagnostic($"Getting security descriptor for {RegistryPath}.");
			//			var result = (await registry.GetSecurityDescriptor(hive, RegistryPath.KeyPath)).ConfigureAwait(false);
			//			var sdResult = result.Descriptor;
			//			entry.SecurityDescriptor = sdResult;
			//			cachedSecurityDescriptor = (path, sdResult);
			//		}
			//		catch (Win32Exception ex)
			//		{
			//			this.WriteVerbose($"Failed to get Security Descriptor for {RegistryPath}: {ex.Message}");
			//		}
			//	}
			//	else
			//	{
			//		entry.SecurityDescriptor = cachedSecurityDescriptor.Value.Item2;
			//	}
			//}
			return new RegistryEntry(path, valueName, data);
		}


		//If the value is specified, we are querying a specific value
		//If a value is not specified We print all values under the key, and all keys under the key.
		//Type filter applies in all cases when specified (we will return none if the type doesn't match a specific value specified)
		protected override async Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken)
		{
			object objreg = registry; //dynamics shouldn't be passed into function calls if we can avoid it for performance reasons.
			if (!(await registry.CheckAccess((uint)KeyPath.Root, KeyPath.KeyPath, (uint)(RegistryAccessRights.KeyQueryValue | RegistryAccessRights.KeyEnumerateSubKey)).ConfigureAwait(false)).bGranted)
			{
				this.WriteError($"{KeyPath} either does not exist, or access is denied.");
				return 0;
			}
			Queue<RegistryPath> keysToProcess = new Queue<RegistryPath>();
			keysToProcess.Enqueue(KeyPath);
			bool topLevel = true;
			while (keysToProcess.TryDequeue(out var key) && !cancellationToken.IsCancellationRequested)
			{
				if (this.Recursive.IsSet || topLevel)
				{
					topLevel = false;
					//Only enumerate values / data if needed
					if (SearchPattern == null || searchValues || searchData)
					{
						string[]? sNames;
						int[]? Types;
						try
						{
							var regEntries = await registry.EnumValues((uint)key.Root, key.KeyPath).ConfigureAwait(false);
							((Win32ErrorCode)regEntries.ReturnValue).CheckAndThrow();
							sNames = ((object[]?)regEntries.sNames).OfType<string>();
							Types = ((Array)regEntries.Types).OfType<int>();
						}
						catch (Win32Exception ex)
						{
							this.WriteWarning($"Failed to enumerate values under {key}: {ex.Message}");
							continue;
						}
						if (sNames is not null)
						{
							this.WriteDiagnostic($"Enumerating {sNames.Length} values under {key}.");
							for (int i = 0; i < sNames.Length && !cancellationToken.IsCancellationRequested; i++)
							{

								if ((this.Type == null || this.Type == (RegistryValueKind)Types[i]))
								{
									bool hasValueMatch = (searchValues && StringSearchMatches(sNames[i]));
									if (SearchPattern == null || hasValueMatch || searchData)
									{
										var regEntry = await GetRegistryValue(objreg, key, sNames[i], (RegistryValueKind)Types[i], !hasValueMatch).ConfigureAwait(false);
										if (regEntry != null)
										{
											this.WriteRegistryRecord(regEntry);
										}
									}
								}
							}
						}

					}
					this.WriteDiagnostic($"Enumerating keys under {key}.");
					string[]? subkeyNames;
					try
					{
						var regKeys = (await registry.EnumKey((uint)key.Root, key.KeyPath).ConfigureAwait(false));
						((Win32ErrorCode)regKeys.ReturnValue).CheckAndThrow();
						subkeyNames = ((System.Array)regKeys.sNames)?.OfType<string>();
					}
					catch (Win32Exception ex)
					{
						this.WriteWarning($"Failed to enumerate keys under {key}: {ex}");
						subkeyNames = null;
					}
					if (subkeyNames is not null)
					{
						foreach (string subkey in subkeyNames)
						{
							keysToProcess.Enqueue(new RegistryPath(KeyPath.ServerName, key.Root, $"{key.KeyPath}\\{subkey}"));
						}
					}

				}
				//We only want to print empty keys when we're not searching, or if our search is explicitly for keys
				if (SearchPattern == null || (searchKeys == true && StringSearchMatches(key.KeyName)))
				{
					this.WriteRegistryRecord(new RegistryEntry(key));
				}
			}
			OnCommandComplete();
			return 0;


		}
	}

	static class ArrayExtensions
	{
		public static T[]? OfType<T>(this Array? array)
		{
			if (array is null)
				return null;

			T[] converted = new T[array.Length];
			int writeIndex = 0;
			for (int i = 0; i < array.Length; i++)
			{
				var elem = array.GetValue(i);
				if (elem is T t)
					converted[writeIndex++] = t;
			}

			if (converted.Length != writeIndex)
				Array.Resize(ref converted, writeIndex);

			return converted;
		}
	}
}
