using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Cli.WmiTool;
using Titanis.Msrpc.Mswmi;
using Titanis.Winterop.Registry;

namespace Wmi.Registry
{
	internal abstract class WmiRegistryCommandBase : WmiCommand
	{
		private const string RegistryProviderName = "StdRegProv";

		[Parameter(10)]
		[Mandatory]
		[Placeholder(@"[HKLM|HKCU|HKCR|HKU|HKCC][\path]")]
		[Description("Path of target registry key")]
		public string KeyPath { get; set; }

		[Parameter]
		[Description("Locale")]
		[DefaultValue("en-US")]
		public string Locale { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		internal RegistryPath keyPath;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
			try
			{
				keyPath = RegistryPath.Parse(KeyPath);
			}
			catch (ArgumentException ex)
			{
				context.LogError($"The Key path is invalid: ${ex.Message}");
			}
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

	}
}
