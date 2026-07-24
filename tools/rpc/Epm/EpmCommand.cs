using System.ComponentModel;
using Titanis.DceRpc.Epm;

namespace Titanis.Cli.EpmTool;

/// <summary>
/// Base class for commands using the endpoint mapper.
/// </summary>
public abstract class EpmCommand : RpcCommand<EpmClient>
{
	[Parameter]
	[Advanced]
	[Description("Number of results to fetch at a time")]
	[DefaultValue(DefaultPageSize)]
	public int PageSize { get; set; }

	const int DefaultPageSize = 32;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (PageSize <= 0)
			PageSize = DefaultPageSize;
	}
}
