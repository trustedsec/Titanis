using System.ComponentModel;
using Titanis.DceRpc.Epm;

namespace Titanis.Cli.EpmTool;

/// <summary>
/// Base class for commands using the endpoint mapper.
/// </summary>
public abstract class EpmCommand : RpcCommand<EpmClient>
{
	[Parameter]
	[Description("Number of results to fetch at a time")]
	public int PageSize { get; set; }

	const int DefaultPageSize = 32;

	protected sealed override Type InterfaceType => typeof(epm.ept);

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (PageSize <= 0)
			PageSize = DefaultPageSize;
	}
}
