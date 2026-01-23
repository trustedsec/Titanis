using System.ComponentModel;
using Titanis;
using Titanis.Winterop;

namespace Titanis.Winterop.Registry
{
	public interface IRegistrySearchCallback
	{
		void OnKeyMatch(RegistryPath keyPath);
		void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData);
	}

	public class RegistrySearcher
	{
		public RegistrySearcher(
			IRegistrySearchCallback searchCallback,
			RegistrySearchFilter filter,
			ILog? log)
		{
			ArgumentNullException.ThrowIfNull(searchCallback);
			ArgumentNullException.ThrowIfNull(filter);
			this.searchCallback = searchCallback;
			this.filter = filter;
			this.log = log;
		}

		private readonly IRegistrySearchCallback searchCallback;
		private readonly RegistrySearchFilter filter;
		private readonly ILog? log;

		public async Task DoSearch(IRegistryKey registryKey, CancellationToken cancellationToken)
		{
			var filter = this.filter;
			var subtreeRootKeyPath = registryKey.KeyPath;

			Queue<IRegistryKey> keysToProcess = new Queue<IRegistryKey>();
			keysToProcess.Enqueue(registryKey);
			bool includeValues = (filter.Options & RegistrySearchOptions.SearchTargetMask & ~RegistrySearchOptions.SearchKeyNames) != 0;
			while (keysToProcess.TryDequeue(out var key) && !cancellationToken.IsCancellationRequested)
			{
				//Only enumerate values / data if needed
				if (includeValues)
				{
					try
					{
						log?.WriteDiagnostic($"Enumerating values under {key}.");
						await key.EnumerateValues(
							(n, t) => ShouldRetrieveData(filter, n, t),
							(name, kind, data) =>
							{
								if (data != null)
								{
									ProcessRegistryValue(
										key.KeyPath,
										name,
										kind,
										data
										);
								}
							},
							RegistryKeyEnumerateOptions.ContinueOnException | RegistryKeyEnumerateOptions.PassExceptionToIterator,
							cancellationToken).ConfigureAwait(false);
					}
					catch (Win32Exception ex)
					{
						log.WriteWarning($"Failed to enumerate values under {key}: {ex.Message}");
						continue;
					}
				}

				// Subkeys
				if (filter.IsRecursive || filter.SearchKeyNames)
				{
					log?.WriteDiagnostic($"Enumerating keys under {key}.");
					string[]? subkeyNames;
					try
					{
						subkeyNames = await key.GetSubkeyNames(cancellationToken).ConfigureAwait(false);
					}
					catch (Win32Exception ex)
					{
						log?.WriteWarning($"Failed to enumerate keys under {key}: {ex}");
						subkeyNames = null;
					}

					if (subkeyNames != null)
					{
						if (filter.SearchKeyNames)
						{
							foreach (var keyName in subkeyNames)
							{
								bool keyMatches = filter.SearchKeyNames && filter.Matches(key.KeyName);
								if (filter.SearchKeyNames)
									searchCallback.OnKeyMatch(key.KeyPath);
							}
						}

						if (this.filter.IsRecursive)
						{
							foreach (string subkeyName in subkeyNames)
							{
								try
								{
									var subkey = await key.OpenSubkey(subkeyName, cancellationToken).ConfigureAwait(false);
									keysToProcess.Enqueue(subkey);
								}
								catch (Exception ex)
								{
									log?.WriteError($"Error opening {key.KeyPath}\\subkeyName: {ex.Message}");
								}
							}
						}
					}
				}
			}
		}

		private static bool ShouldRetrieveData(RegistrySearchFilter filter, string name, RegistryValueKind kind)
			=> filter.MatchesName(name) && filter.MatchesType(kind);


		internal void ProcessRegistryValue(
			RegistryPath keyPath,
			string valueName,
			RegistryValueKind valueKind,
			object data
			)
		{
			var log = this.log;

			// Apply type filter
			if (!filter.MatchesType(valueKind))
				return;

			// Apply name filter
			if (!filter.MatchesName(valueName))
				return;

			if (data is Exception ex)
				log.WriteWarning($"Failed to get value '{valueName}' under {keyPath}: {ex.Message}");
			else
			{
				var valueData = (valueKind, data) switch
				{
					(RegistryValueKind.REG_SZ, string str) => RegistryData.CreateString(str),
					(RegistryValueKind.REG_EXPAND_SZ, string str) => RegistryData.CreateExpandableString(str),
					(RegistryValueKind.REG_BINARY, byte[] bytes) => RegistryData.CreateBinary(bytes),
					(RegistryValueKind.REG_DWORD, uint ui4) => RegistryData.CreateDword(ui4),
					(RegistryValueKind.REG_MULTI_SZ, string[] strs) => RegistryData.CreateRegMultiString(strs),
					(RegistryValueKind.REG_QWORD, ulong ui8) => RegistryData.CreateDword(ui8),
					_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, $"Unsupported registry value type '{valueKind}' for value '{valueName}'")
				};

				// First check value name and key name, since those are easy
				bool matches = !filter.HasSearchFilter;
				// Check value name
				if (!matches && filter.SearchValueNames)
					matches = filter.SearchValueNames && filter.Matches(valueName);
				// Check key name
				if (!matches && filter.SearchKeyNames)
					matches = filter.SearchKeyNames && filter.Matches(keyPath.KeyPath);
				// Search data
				if (!matches)
					matches = filter.DataSearchMatches(valueData);

				if (matches)
					//TODO: WMI StdRegProv GetSecurityDescriptor does not currently work as expected.

					#region GetSecurity stuff
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
					#endregion

					searchCallback.OnValueMatch(keyPath, valueName, valueKind, valueData);
			}
		}
	}
}
