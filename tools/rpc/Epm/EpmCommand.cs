using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.DceRpc.Epm;

namespace Titanis.DceRpc.Cli;

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
	protected sealed override int WellKnownPort => 135;

	protected sealed override string? WellKnownPipeName => "epmapper";

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (PageSize <= 0)
			PageSize = DefaultPageSize;
	}
}
