using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Titanis;

namespace Titanis.Winterop.Registry
{
	[Flags]
	public enum RegistrySearchOptions
	{
		None = 0,

		SearchKeyNames = 1,
		SearchValueNames = 2,
		SearchData = 4,
		SearchTargetMask = SearchKeyNames | SearchValueNames | SearchData,

		//IsRecursive = 8,  Recursion is controlled by MaxDepth
		IgnoreCase = 0x10,
		MatchWholeName = 0x20,
		MatchPattern = 0x40,
	}

	public class RegistrySearchFilter
	{
		public RegistrySearchFilter(
			ImmutableArray<string> valueNameFilters,
			ImmutableArray<RegistryValueType> typeFilter,
			ImmutableArray<string> searchTexts,
			RegistrySearchOptions options,
			int maxDepth
			)
		{
			ValueNames = valueNameFilters;
			Options = options;
			TypeFilters = typeFilter;
			MaxDepth = maxDepth;

			if (!searchTexts.IsDefaultOrEmpty)
			{
				SearchTexts = searchTexts;

				if (0 != (options & RegistrySearchOptions.MatchPattern))
				{
					var patterns = ImmutableArray.CreateBuilder<WildcardPattern>(searchTexts.Length);
					List<ulong>? intFilters = null;

					for (int i = 0; i < searchTexts.Length; i++)
					{
						string? searchText = searchTexts[i];
						if (ulong.TryParse(searchText, out var ui64)
							|| searchText.StartsWith("0x") && ulong.TryParse(searchText.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier, null, out ui64))
						{
							(intFilters ??= new List<ulong>()).Add(ui64);
						}

						if (0 == (options & RegistrySearchOptions.MatchWholeName))
						{
							if (!searchText.StartsWith('*'))
								searchText = '*' + searchText;
							if (!searchText.EndsWith('*'))
								searchText += '*';
						}
						patterns.Add(new WildcardPattern(searchText));
					}

					_patterns = patterns.ToImmutable();
					_integerValues = intFilters?.ToArray();
				}
			}
		}

		#region Value name filter
		public ImmutableArray<string> ValueNames { get; }
		public bool HasValueNameFilter => !ValueNames.IsDefaultOrEmpty;
		public bool MatchesName(string name)
		{
			return !HasValueNameFilter || ValueNames.Any(r => r.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
		#endregion

		#region Type filter
		public ImmutableArray<RegistryValueType> TypeFilters { get; set; }
		public bool HasTypeFilter => !TypeFilters.IsDefaultOrEmpty;
		public bool MatchesType(RegistryValueType kind)
		{
			return !HasTypeFilter || TypeFilters.Contains(kind);
		}
		#endregion

		public ImmutableArray<string> SearchTexts { get; set; }
		public bool HasSearchFilter => !SearchTexts.IsDefaultOrEmpty;

		private ImmutableArray<WildcardPattern> _patterns;
		private ulong[]? _integerValues;

		public int MaxDepth { get; }

		public RegistrySearchOptions Options { get; set; }
		public bool SearchKeyNames => 0 != (Options & RegistrySearchOptions.SearchKeyNames);
		public bool SearchValueNames => 0 != (Options & RegistrySearchOptions.SearchValueNames);
		public bool SearchData => 0 != (Options & RegistrySearchOptions.SearchData);
		public bool IsRecursive => MaxDepth > 0;
		public bool IgnoreCase => 0 != (Options & RegistrySearchOptions.IgnoreCase);
		public bool MatchWholeName => 0 != (Options & RegistrySearchOptions.MatchWholeName);
		public bool MatchPattern => 0 != (Options & RegistrySearchOptions.MatchPattern);

		public bool Matches(string str)
		{
			if (!_patterns.IsDefaultOrEmpty && str != string.Empty)
				return _patterns.Any(r => r.Matches(str));
			else if (!SearchTexts.IsDefaultOrEmpty)
			{
				var comp = IgnoreCase ? StringComparison.InvariantCultureIgnoreCase
					: StringComparison.InvariantCulture;

				return SearchTexts.Any(
					MatchWholeName ? r => (str.Equals(r, comp) && str.TrimEnd('\0').Length == r.TrimEnd('\0').Length) //When matching whole names, reject matches terminating at a null injected in the middle of the string.
					: r => str.Contains(r, comp)
					);
			}
			else
				return true;
		}

		public bool Matches(ulong n)
		{
			return _integerValues != null && _integerValues.Contains(n) || Matches(n.ToString());
		}
		public bool Matches(uint n)
		{
			return _integerValues != null && _integerValues.Contains(n) || Matches(n.ToString());
		}

		public bool Matches(byte[] n)
		{
			try
			{
				var str = Encoding.Unicode.GetString(n);
				if (Matches(str))
					return true;
			}
			catch { }

			try
			{
				var str = Encoding.UTF8.GetString(n);
				if (Matches(str))
					return true;
			}
			catch { }

			return false;
		}

		public bool Matches(ImmutableArray<string> strings)
		{
			return !strings.IsDefaultOrEmpty && strings.Any(Matches);
		}

		public bool DataSearchMatches(RegistryData data)
		{
			return SearchData && data.Matches(this);
		}
	}
}
