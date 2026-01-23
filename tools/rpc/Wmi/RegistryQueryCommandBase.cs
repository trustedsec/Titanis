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
using Titanis.Cli.Registry;
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
		[ParameterGroup]
		public RegistryQueryParameterGroup QueryParameters { get; set; }


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

			this._filter = this.QueryParameters.ValidateAndBuildFilter(context);
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

			var searcher = new RegistrySearcher(this, this._filter!, this.Log);
			await searcher.DoSearch(new WmiRegistryKey(registry, this.KeyPath), cancellationToken);

			OnQueryComplete();
			return 0;


		}


		protected abstract void OnKeyMatch(RegistryPath keyPath);

		void IRegistrySearchCallback.OnKeyMatch(RegistryPath keyPath) => this.OnKeyMatch(keyPath);

		protected abstract void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData);
		void IRegistrySearchCallback.OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData) => this.OnValueMatch(keyPath, valueName, valueKind, valueData);
	}
}
