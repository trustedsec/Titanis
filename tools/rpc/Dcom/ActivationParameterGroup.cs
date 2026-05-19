using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Cli.DcomTool;

public class ActivationParameterGroup : ParameterGroupBase
{

	[Parameter(After = nameof(IHaveServerName.ServerName))]
	[Mandatory]
	[Description("CLSID of object to activate")]
	public Guid Clsid { get; set; }

	[Parameter]
	[Description("Name of file to activate")]
	public string? FileName { get; set; }
}
