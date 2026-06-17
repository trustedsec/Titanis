using System.ComponentModel;
using Titanis;
using Titanis.Winterop;


namespace Titanis.Winterop.Registry
{
	public interface IRegistrySearchCallback
	{
		void OnKeyMatch(RegistryPath keyPath);
		void OnValueMatch(RegistryPath keyPath, RegistryValueInfo value);
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

			Queue<(IRegistryKey, int)> keysToProcess = new Queue<(IRegistryKey, int)>();
			keysToProcess.Enqueue((registryKey, 0));
			bool includeValues = (filter.Options & RegistrySearchOptions.SearchTargetMask & ~RegistrySearchOptions.SearchKeyNames) != 0;
			bool first = true;

			while (keysToProcess.TryDequeue(out var entry) && !cancellationToken.IsCancellationRequested)
			{
				var (key, currentDepth) = entry;
				//Export gets screwed if we find a key as a subkey and then later process its values, so now we check the key for matching in the same step we would process its values
				if (!first)
				{
					if (filter.SearchKeyNames && filter.Matches(key.KeyName))
					{
						searchCallback.OnKeyMatch(RegistryPath.Parse(key.KeyPath));
					}
				}
				//We don't want to match on our root / first key
				first = false;
				if (includeValues)
				{
					try
					{
						log?.WriteDiagnostic($"Enumerating values under {key}.");
						await foreach (var item in key.GetValues(true, cancellationToken).ConfigureAwait(false))
						{
							ProcessRegistryValue(key, item);
						}
					}
					catch (Win32Exception ex)
					{
						log?.WriteWarning($"Failed to enumerate values under {key}: {ex.Message}");
						continue;
					}
				}
				// Subkeys
				if (filter.IsRecursive || filter.SearchKeyNames)
				{
					log?.WriteDiagnostic($"Enumerating keys under {key}.");
					try
					{
						await foreach (var subkeyName in key.GetSubkeyNames(cancellationToken).ConfigureAwait(false))
						{
							//Matches for these are checked when we would grab there values
							if (currentDepth < filter.MaxDepth)
							{
								try
								{
									var subkey = await key.OpenSubkey(subkeyName.KeyName, Security.RegistryAccessRights.EnumerateSubkeys | Security.RegistryAccessRights.KeyRead, RegistryKeyOptions.None, cancellationToken).ConfigureAwait(false);
									keysToProcess.Enqueue((subkey, currentDepth + 1));
								}
								catch (Exception ex)
								{
									log?.WriteError($"Error opening {key.KeyPath}\\{subkeyName}: {ex.Message}");
								}
							}
							//We won't be grabbing the values, so we need to process these subkeys for matches now.
							else
							{
								if (filter.SearchKeyNames && filter.Matches(subkeyName.KeyName))
								{
									searchCallback.OnKeyMatch(RegistryPath.Parse(RegistryPath.Combine(key.KeyPath, subkeyName.KeyName)));
								}
							}
						}
					}
					catch (Win32Exception ex)
					{
						log?.WriteWarning($"Failed to enumerate keys under {key.KeyPath}: {ex}");
					}
				}
			}
		}

		private void ProcessRegistryValue(
			IRegistryKey key,
			RegistryValueInfo value
			)
		{
			var log = this.log;

			// Apply type filter
			if (!filter.MatchesType(value.ValueType))
				return;

			// Apply name filter
			if (!filter.MatchesName(value.Name))
				return;
			{


				// First check value name and key name, since those are easy
				bool matches = !filter.HasSearchFilter;
				// Check value name
				if (!matches && filter.SearchValueNames)
					matches = filter.SearchValueNames && filter.Matches(value.Name);
				// Search data
				if (!matches && value.Bytes != null)
				{
					matches = filter.DataSearchMatches(RegistryData.CreateRegValue(value));
				}

				if (matches)
				{
					searchCallback.OnValueMatch(RegistryPath.Parse(key.KeyPath), value);
				}
			}
		}
	}
}
