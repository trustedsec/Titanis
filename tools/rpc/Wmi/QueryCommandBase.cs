using System.ComponentModel;
using Titanis.Msrpc.Mswmi;

namespace Titanis.Cli.WmiTool;

/// <summary>
/// Base class for commands that execute a WMI query.
/// </summary>
[OutputRecordType(typeof(WmiObject))]
internal abstract class QueryCommandBase : WmiNamespaceCommandBase
{

	[Parameter]
	[Advanced]
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

		while (await results.ReadAsync(cancellationToken))
		{
			var record = results.Current;
			if (record is null)
				continue;

			this.WriteRecord(record);
		}

		return 0;
	}
}
