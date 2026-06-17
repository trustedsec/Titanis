using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	internal abstract class QueryCommandBase : RegistryKeyCommand, IRegistrySearchCallback
	{
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public RegistryQueryParameters QueryParameters { get; set; }

		/// Even if we're just enumerating keys we need QueryValue rights for <see cref="RegistryKey.QueryInfo(CancellationToken)"/>
		protected override RegistryAccessRights RequiredKeyAccess => RegistryAccessRights.QueryValue | RegistryAccessRights.EnumerateSubkeys;

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		private RegistrySearchFilter _filter;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this._filter = this.QueryParameters.ValidateAndBuildFilter(context);

		}

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			this.OnBeforeQuery();

			var searcher = new RegistrySearcher(this, this._filter!, this.Log);
			await searcher.DoSearch(key, cancellationToken);

			OnQueryComplete();
			return 0;


		}


		protected abstract void OnKeyMatch(RegistryPath keyPath);

		void IRegistrySearchCallback.OnKeyMatch(RegistryPath keyPath) => this.OnKeyMatch(keyPath);

		protected abstract void OnValueMatch(RegistryPath keyPath, RegistryValueInfo value);
		void IRegistrySearchCallback.OnValueMatch(RegistryPath keyPath, RegistryValueInfo value) => this.OnValueMatch(keyPath, value);

	}
}
