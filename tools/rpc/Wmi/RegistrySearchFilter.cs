using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Titanis.Cli;
using Titanis.Winterop.Registry;

namespace Wmi.Registry
{
	[Flags]
	public enum RegistrySearchOptions
	{
		None = 0,

		SearchKeyNames = 1,
		SearchValueNames = 2,
		SearchData = 4,
		SearchTargetMask = SearchKeyNames | SearchValueNames | SearchData,

		IsRecursive = 8,
		IgnoreCase = 0x10,
		MatchWholeName = 0x20,
		MatchPattern = 0x40,
	}

	public class RegistrySearchFilter
	{
		public RegistrySearchFilter(
			ImmutableArray<string> valueNameFilters,
			ImmutableArray<RegistryValueKind> typeFilter,
			ImmutableArray<string> searchTexts,
			RegistrySearchOptions options
			)
		{
			this.ValueNames = valueNameFilters;
			this.Options = options;
			this.TypeFilters = typeFilter;

			if (!searchTexts.IsDefaultOrEmpty)
			{
				this.SearchTexts = searchTexts;

				if (0 != (options & RegistrySearchOptions.MatchPattern))
				{
					var patterns = ImmutableArray.CreateBuilder<WildcardPattern>(searchTexts.Length);
					List<ulong>? intFilters = null;

					for (int i = 0; i < searchTexts.Length; i++)
					{
						string? searchText = searchTexts[i];
						if (0 == (options & RegistrySearchOptions.MatchWholeName))
						{
							if (!searchText.StartsWith('*'))
								searchText = '*' + searchText;
							if (!searchText.EndsWith('*'))
								searchText += '*';
						}
						else
						{
							if (ulong.TryParse(searchText, out var ui64)
								|| (searchText.StartsWith("0x") && ulong.TryParse(searchText.Substring(2), out ui64)))
							{
								(intFilters ??= new List<ulong>()).Add(ui64);
							}
						}

						patterns.Add(new WildcardPattern(searchText));
					}

					this._patterns = patterns.ToImmutable();
					this._integerValues = intFilters?.ToArray();
				}
			}
		}

		#region Value name filter
		public ImmutableArray<string> ValueNames { get; }
		public bool HasValueNameFilter => !this.ValueNames.IsDefaultOrEmpty;
		public bool MatchesName(string name)
		{
			return !this.HasValueNameFilter || this.ValueNames.Any(r => r.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
		#endregion

		#region Type filter
		public ImmutableArray<RegistryValueKind> TypeFilters { get; set; }
		public bool HasTypeFilter => !this.TypeFilters.IsDefaultOrEmpty;
		public bool MatchesType(RegistryValueKind kind)
		{
			return !this.HasTypeFilter || this.TypeFilters.Contains(kind);
		}
		#endregion

		public ImmutableArray<string> SearchTexts { get; set; }
		public bool HasSearchFilter => !this.SearchTexts.IsDefaultOrEmpty;

		private ImmutableArray<WildcardPattern> _patterns;
		private ulong[]? _integerValues;


		public RegistrySearchOptions Options { get; set; }
		public bool SearchKeyNames => 0 != (this.Options & RegistrySearchOptions.SearchKeyNames);
		public bool SearchValueNames => 0 != (this.Options & RegistrySearchOptions.SearchValueNames);
		public bool SearchData => 0 != (this.Options & RegistrySearchOptions.SearchData);
		public bool IsRecursive => 0 != (this.Options & RegistrySearchOptions.IsRecursive);
		public bool IgnoreCase => 0 != (this.Options & RegistrySearchOptions.IgnoreCase);
		public bool MatchWholeName => 0 != (this.Options & RegistrySearchOptions.MatchWholeName);
		public bool MatchPattern => 0 != (this.Options & RegistrySearchOptions.MatchPattern);

		public bool Matches(string str)
		{
			if (!this._patterns.IsDefaultOrEmpty)
			{
				return this._patterns.Any(r => r.Matches(str, this.IgnoreCase));
			}
			else if (!this.SearchTexts.IsDefaultOrEmpty)
			{
				var comp = this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase
					: StringComparison.InvariantCulture;

				return this.SearchTexts.Any(
					this.MatchWholeName ? r => str.Equals(r, comp)
					: r => str.Contains(r, comp)
					);
			}
			else
				return true;
		}

		public bool Matches(ulong n)
		{
			return (this._integerValues != null && this._integerValues.Contains(n)) || this.Matches(n.ToString());
		}
		public bool Matches(uint n)
		{
			return (this._integerValues != null && this._integerValues.Contains(n)) || this.Matches(n.ToString());
		}

		public bool Matches(byte[] n)
		{
			try
			{
				var str = Encoding.Unicode.GetString(n);
				if (this.Matches(str))
					return true;
			}
			catch { }

			try
			{
				var str = Encoding.ASCII.GetString(n);
				if (this.Matches(str))
					return true;
			}
			catch { }

			return false;
		}

		public bool Matches(ImmutableArray<string> strings)
		{
			return !strings.IsDefaultOrEmpty && strings.Any(this.Matches);
		}

		public bool DataSearchMatches(RegistryData data)
		{
			return this.SearchData && data.Matches(this);
		}
	}
}
