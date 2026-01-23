using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi.Registry
{
	internal abstract class WmiRegistryCommandBase : WmiCommandBase
	{
		private const string RegistryProviderName = "StdRegProv";

		[Parameter(10)]
		[Mandatory]
		[Placeholder(@"\\Server\[HKLM|HKCU|HKCR|HKU|HKCC][\path]")]
		[Description("Key path")]
		public RegistryPath KeyPath { get; set; }

		[Parameter]
		[Description("Locale")]
		[DefaultValue("en-US")]
		public string Locale { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			return await this.ConnectAndRun(KeyPath.ServerName, cancellationToken).ConfigureAwait(false);
		}

		protected override async Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken)
		{
			var ns = await wmi.OpenNamespace(WmiClient.RootCimV2Namespace, this.Locale, cancellationToken);
			var regProv = await ns.GetObjectAsync(RegistryProviderName, cancellationToken);
			if (regProv is null)
			{
				this.WriteError("Failed to get StdRegProv class");
				return 1;
			}
			return await RunAsync(regProv, cancellationToken);
		}

		protected abstract Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken);

		public const string DefaultValueName = "(default)";
		protected static string ValueDisplayName(string name) => string.IsNullOrEmpty(name) ? DefaultValueName : name;
	}
}
