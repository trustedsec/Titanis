using System.Collections.Immutable;
using System.ComponentModel;
using Titanis.Winterop.Registry;

namespace Titanis.Cli.Registry
{
	public class RegistryQueryParameterGroup
	{
		[Parameter]
		[Description($"Value name to query")]
		[Alias("vn")]
		public string[]? ValueName { get; set; }

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
		public string[]? SearchPattern { get; set; }

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
		public RegistryValueKind[]? Type { get; set; }

		public RegistrySearchFilter ValidateAndBuildFilter(ParameterValidationContext context)
		{
			// Value name filter
			string[]? valueNameFilter;
			if (this.ValueName != null)
			{
				if (ValueEmpty.IsSet)
					context.LogError($"The -{nameof(ValueEmpty)} option is mutually exclusive with -{nameof(ValueName)}.");

				valueNameFilter = ValueName;
			}
			else if (ValueEmpty.IsSet)
				valueNameFilter = [string.Empty];
			else
				valueNameFilter = null;


			RegistrySearchOptions searchTargets = RegistrySearchOptions.None;
			var searchOptions = RegistrySearchOptions.None;

			if (this.Recursive.IsSet)
				searchOptions |= RegistrySearchOptions.IsRecursive;

			// Process search filter and options
			if (!SearchPattern.IsNullOrEmpty())
			{
				if (Exact.IsSet)
					searchOptions |= RegistrySearchOptions.MatchWholeName;
				else
					searchOptions |= RegistrySearchOptions.MatchPattern;

				if (!CaseSensitive.IsSet) searchOptions |= RegistrySearchOptions.IgnoreCase;

				if (this.DataSearch.IsSet)
					searchTargets |= RegistrySearchOptions.SearchData;
				if (this.KeySearch.IsSet)
					searchTargets |= RegistrySearchOptions.SearchKeyNames;
				if (valueNameFilter?.FirstOrDefault() == "*")
				{
					searchTargets |= RegistrySearchOptions.SearchValueNames;
					valueNameFilter = null;
				}

				if (searchTargets == RegistrySearchOptions.None)
					searchTargets = RegistrySearchOptions.SearchTargetMask;
			}
			else
			{
				if (Exact.IsSet || CaseSensitive.IsSet || DataSearch.IsSet || KeySearch.IsSet)
					context.LogError($"-{nameof(Exact)}, -{nameof(CaseSensitive)}, -{nameof(DataSearch)}, and -{nameof(KeySearch)} options require -{nameof(SearchPattern)} to be specified.");
			}



			var filter = new RegistrySearchFilter(
				ImmutableArray.Create(valueNameFilter),
				ImmutableArray.Create(this.Type),
				ImmutableArray.Create(this.SearchPattern),
				searchOptions | searchTargets);
			return filter;
		}
	}
}
