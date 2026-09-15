using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msscmr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.ScmTool;

[Command]
[Description("Changes the configuration of a service")]
[OutputRecordType(typeof(ServiceConfig), DefaultOutputStyle = OutputStyle.List)]
public class ConfigCommand : ServiceCommand
{
	protected override ServiceAccessRights RequiredServiceAccess => ServiceAccessRights.QueryConfig | ServiceAccessRights.ChangeConfig;

	protected override ScmAccessRights RequiredScmAccess => ScmAccessRights.Connect;


	[Parameter(20)]
	[Description("Service command line")]
	public string? BinPath { get; set; }

	[Parameter]
	[Description("Type of service")]
	[DefaultValue(ServiceTypes.OwnProcess)]
	public ServiceTypes? ServiceType { get; set; }

	[Parameter]
	[Description("Service start type")]
	[DefaultValue(ServiceStartType.Demand)]
	public ServiceStartType? StartType { get; set; }

	[Parameter]
	[Description("Error control")]
	[DefaultValue(ServiceErrorControl.Normal)]
	public ServiceErrorControl? ErrorControl { get; set; }

	[Parameter]
	[Description("Load order group")]
	public string? LoadOrderGroup { get; set; }

	[Parameter]
	[Description("Unique tag within the load order group")]
	[DefaultValue(0)]
	public int? Tag { get; set; }

	[Parameter]
	[Alias("deps")]
	[Description("List of services this service depends on")]
	public string[]? Dependencies { get; set; }

	[Parameter]
	[Description("Name of user account to run service as")]
	[DefaultValue("LocalSystem")]
	public string? StartName { get; set; }

	[Parameter]
	[Description("Password of service account")]
	public string? StartPassword { get; set; }

	[Parameter]
	[Description("Service display name")]
	public string? DisplayName { get; set; }

	protected override async Task<int> RunAsync(Scm scm, Service service, CancellationToken cancellationToken)
	{
		var config = await service.QueryConfigAsync(cancellationToken);
		if (this.BinPath != null) config.BinaryPathName = this.BinPath;
		if (this.ServiceType.HasValue) config.ServiceType = this.ServiceType.Value;
		if (this.StartType.HasValue) config.StartType = this.StartType.Value;
		if (this.ErrorControl.HasValue) config.ErrorControl = this.ErrorControl.Value;
		if (this.LoadOrderGroup != null) config.LoadOrderGroup = this.LoadOrderGroup;
		if (this.Tag.HasValue) config.TagId = this.Tag.Value;
		if (this.StartName != null) config.ServiceStartName = this.StartName;
		if (this.StartPassword != null) config.ServiceStartName = this.StartPassword;
		if (this.DisplayName != null) config.DisplayName = this.DisplayName;
		await service.ChangeConfigAsync(config, cancellationToken);

		this.WriteRecord(config);

		return 0;
	}
}
