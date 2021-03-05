using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;
internal abstract class WmiNamespaceCommandBase : WmiCommand
{
	[Parameter]
	[Description("Namespace to query")]
	[DefaultValue(@"root\cimv2")]
	public string Namespace { get; set; }

	[Parameter]
	[Description("Locale")]
	[DefaultValue("en-US")]
	public string Locale { get; set; }

	[Parameter]
	[Description("Filter qualifiers")]
	public string[]? WithQualifiers { get; set; }

	public bool FilterQualifiers(WmiQualifier[]? qualifiers)
	{
		var filters = this.WithQualifiers;
		if (filters.IsNullOrEmpty())
			return true;

		foreach (var filter in filters)
		{
			string name;
			string? filterValue = null;
			{
				var isep = filter.IndexOf('=');
				if (isep >= 0)
				{
					filterValue = filter.Substring(isep + 1).Trim();
					name = filter.Substring(0, isep).Trim();
				}
				else
				{
					name = filter.Trim();
				}
			}

			bool foundMatch = false;
			foreach (var qual in qualifiers)
			{
				bool nameMatches = qual.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
				if (nameMatches)
				{
					if (filterValue is null)
					{
						foundMatch = ((qual.Value as bool?) ?? true);
					}
					else
					{
						if (qual.Value is object?[] arr)
						{
							foreach (var value in arr)
							{
								var valueText = value?.ToString() ?? string.Empty;
								foundMatch = valueText.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
							}
						}
						else
						{
							var valueText = qual.Value?.ToString() ?? string.Empty;
							foundMatch = valueText.Equals(filterValue, StringComparison.OrdinalIgnoreCase);
						}
					}

					if (foundMatch)
						break;
				}
			}

			if (!foundMatch)
				return false;
		}

		return true;
	}

	/// <inheritdoc/>
	protected sealed override async Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken)
	{
		var ns = await wmi.OpenNamespace(this.Namespace, this.Locale, cancellationToken);
		return await RunAsync(ns, cancellationToken);
	}

	protected abstract Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken);
}
