using Microsoft.Win32;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis.Winterop.Registry;
using System.Collections.Immutable;

namespace Titanis.Cli
{
	public class RegistryQueryParameters : ParameterGroupBase
	{
		[Parameter(20)]
		[Description($"Limits results to listed value names")]
		public string[] ValueNameFilter { get; set; }

		[Parameter]
		[Description("Limits results to default value of registry key.")]
		[Alias("ve")]
		public SwitchParam QueryDefaultValue { get; set; }

		[Parameter]
		[Description("Queries all subkeys and values recursively.")]
		[Alias("s")]
		public SwitchParam Recursive { get; set; }

		[Parameter]
		[Description("Limit recursion to the depth specified")]
		[DefaultValue(0)]
		public int MaxDepth { get; set; }

		//UNDONE: Through WMI we cannot set the separator for REG_MULTI_SZ values, so this parameter is not implemented
		//[Parameter]
		//[Description("Separator character for REG_MULTI_SZ")]
		//[Alias("se")]
		//public char Separator { get; set; }


		//This can be an:
		// - Integer number (not hex)
		// - string using * (zero or more) or ?  (exactly one) wildcards
		[Parameter]
		[Description("Data or patterns to search for.")]
		[Alias("f")]
		public string[]? SearchPatterns { get; set; }

		[Parameter]
		[Description("Specifies to search in key names.")]
		[Alias("k")]
		public SwitchParam KeySearch { get; set; }

		[Parameter]
		[Description("Specifies to search in data.")]
		[Alias("d")]
		public SwitchParam DataSearch { get; set; }

		[Parameter]
		[Description("Specifies to search in value names.")]
		public SwitchParam ValueSearch { get; set; }

		[Parameter]
		[Description("Specifies that the search is case sensitive")]
		[Alias("c")]
		public SwitchParam CaseSensitive { get; set; }

		[Parameter]
		[Description("Specifies to return only exact matches.")]
		[Alias("e")]
		public SwitchParam Exact { get; set; }

		[Parameter]
		[Description("Specifies registry value data types.")]
		[Alias("t")]
		public RegistryValueType[]? Types { get; set; }



		public RegistrySearchFilter? ValidateAndBuildFilter(ParameterValidationContext context)
		{
			List<string> searchValueNames = new List<string>();
			RegistrySearchOptions searchOptions = RegistrySearchOptions.None;
			if (Exact.IsSet && SearchPatterns == null)
			{
				context.LogError($"-{nameof(Exact)} options require one or more -{nameof(SearchPatterns)} to be specified.");
			}

			if (QueryDefaultValue.IsSet)
			{
				searchValueNames.Add(String.Empty);
			}
			if (ValueNameFilter != null)
			{
				searchValueNames.AddRange(ValueNameFilter);
			}

			//We can't search without a search pattern
			if ((ValueSearch.IsSet || DataSearch.IsSet || KeySearch.IsSet) && SearchPatterns == null)
			{
				context.LogError($"The -{nameof(SearchPatterns)} option must be specified when using -{nameof(DataSearch)}, -{nameof(KeySearch)} or -{nameof(ValueSearch)}.");
			}

			if (!(DataSearch.IsSet || KeySearch.IsSet || ValueSearch.IsSet) && SearchPatterns == null)
			{
				//Everything is null and no value filters were specified, this is a normal single level listing
				if (searchValueNames.Count == 0)
				{
					searchOptions |= RegistrySearchOptions.SearchKeyNames | RegistrySearchOptions.SearchValueNames;
				}
				//Specific valuenames were specified to be retrieved, we're only interestied in those.
				else
				{
					searchOptions |= RegistrySearchOptions.SearchValueNames;
				}
			}
			if (!(DataSearch.IsSet || KeySearch.IsSet || ValueSearch.IsSet) && SearchPatterns != null)
			{
				searchOptions |= RegistrySearchOptions.SearchKeyNames;
				this.Log?.WriteWarning("Search type not specified, defaulting to key search.");
			}
			else
			{
				if (KeySearch.IsSet)
					searchOptions |= RegistrySearchOptions.SearchKeyNames;
				if (DataSearch.IsSet)
					searchOptions |= RegistrySearchOptions.SearchData;
				if (ValueSearch.IsSet)
					searchOptions |= RegistrySearchOptions.SearchValueNames;
			}
			if (!Exact.IsSet)
			{
				searchOptions |= RegistrySearchOptions.IgnoreCase;
				if (SearchPatterns != null)
				{
					searchOptions |= RegistrySearchOptions.MatchPattern;
				}
			}
			else
			{
				searchOptions |= RegistrySearchOptions.MatchWholeName;
			}


			int maxDepth = 0;
			if (Recursive.IsSet)
			{
				//searchOptions |= RegistrySearchOptions.IsRecursive;
				maxDepth = int.MaxValue;
			}
			if (MaxDepth != 0)
			{
				//searchOptions |= RegistrySearchOptions.IsRecursive;
				if (maxDepth > 0)
				{
					this.Log?.WriteWarning($"-{nameof(Recursive)} implies an infinite -{nameof(MaxDepth)}. Specify one or the other, not both.");
				}
				maxDepth = MaxDepth;
			}

			return new RegistrySearchFilter(searchValueNames.ToImmutableArray(),
				Types?.ToImmutableArray() ?? ImmutableArray<RegistryValueType>.Empty,
				SearchPatterns?.ToImmutableArray() ?? ImmutableArray<string>.Empty,
				searchOptions,
				maxDepth
				);
		}
	}
}
