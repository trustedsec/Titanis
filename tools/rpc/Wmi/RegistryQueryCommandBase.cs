using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis;
using Titanis.Asn1.Metadata;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Wmi.Registry
{
	/// <summary>
	/// Implements registry query functionality based off the semantics of reg.exe
	/// </summary>
	[DetailedHelpResource(typeof(Messages), nameof(Messages.wmi_base_query_Detailed))]
	[OutputFieldFormat(nameof(RegistryEntry.ValueName), null, typeof(ValueNameFormatter))]
	internal abstract partial class RegistryQueryCommandBase : WmiRegistryCommandBase, IRegistrySearchCallback
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

		//TODO: WMI StdRegProv GetSecurityDescriptor does not currently work as expected.
		//[Parameter]
		//[Description("Queries key security descriptors")]
		//[Alias("sec")]
		//public SwitchParam GetSecurity { get; set; }

		/// <summary>
		/// Called before the query begins.
		/// </summary>
		protected virtual void OnBeforeQuery()
		{
		}
		/// <summary>
		/// Called after the query has completed.
		/// </summary>
		protected virtual void OnQueryComplete()
		{
			//No-op
		}

		private (RegistryPath, SecurityDescriptor)? cachedSecurityDescriptor;

		private RegistrySearchFilter? _filter;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

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
			this._filter = filter;
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

			this.OnBeforeQuery();

			await DoSearch(new WmiRegistryKey(registry, this.KeyPath), cancellationToken).ConfigureAwait(false);
			OnQueryComplete();
			return 0;


		}

		private Task DoSearch(WmiRegistryKey registryKey, CancellationToken cancellationToken)
		{
			var searcher = new RegistrySearcher(this, this._filter, this.Log);
			return searcher.DoSearch(registryKey, cancellationToken);
		}


		protected abstract void OnKeyMatch(RegistryPath keyPath);

		void IRegistrySearchCallback.OnKeyMatch(RegistryPath keyPath) => this.OnKeyMatch(keyPath);

		protected abstract void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData);
		void IRegistrySearchCallback.OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData) => this.OnValueMatch(keyPath, valueName, valueKind, valueData);
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
