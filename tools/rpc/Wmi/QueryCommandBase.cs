using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;
[OutputRecordType(typeof(WmiObject))]
internal abstract class QueryCommandBase : WmiNamespaceCommandBase
{

	[Parameter]
	[Description("Number of results to fetch at a time")]
	[DefaultValue(10)]
	public int PageSize { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (this.PageSize < 1)
			context.LogError(nameof(PageSize), "PageSize must be greater than 0");
	}

	/// <summary>
	/// Gets the WQL query text to execute.
	/// </summary>
	/// <returns>WQL query text</returns>
	protected abstract string GetQueryText();

	/// <inheritdoc/>
	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		var results = await ns.ExecuteWqlQueryAsync(this.GetQueryText(), this.PageSize, cancellationToken);

		bool first = true;
		while (await results.ReadAsync(cancellationToken))
		{
			var record = results.Current;

			if (first)
			{
				first = false;
				this.SetOutputFormat(this.ConsoleOutputStyle ?? OutputStyle.Table, OutputField.GetFieldsFor(record, this.OutputFields));
			}


			this.WriteRecord(record);
		}

		return 0;
	}
}
