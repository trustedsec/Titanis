using System.ComponentModel;
using Titanis.Msrpc.Msscmr;

namespace Titanis.Cli.ScmTool;

/// <task category="SCM;Lateral Movement">Create a service</task>
[Description("Creates and optionally starts a new service")]
[Example("Create and start a service", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m! -EncryptRpc myservice -DisplayName \"My Service\" C:\\windows\\system32\\cmd.exe -Start")]
internal class CreateServiceCommand : ScmCommand
{
	[Parameter(10)]
	[Mandatory]
	[Description("Name of service to create")]
	public string ServiceName { get; set; }

	[Parameter(20)]
	[Description("Service command line")]
	public string BinPath { get; set; }

	[Parameter]
	[Description("Type of service")]
	[DefaultValue(ServiceTypes.OwnProcess)]
	public ServiceTypes ServiceType { get; set; }

	[Parameter]
	[Description("Service start type")]
	[DefaultValue(ServiceStartType.Demand)]
	public ServiceStartType StartType { get; set; }

	[Parameter]
	[Description("Error control")]
	[DefaultValue(ServiceErrorControl.Normal)]
	public ServiceErrorControl ErrorControl { get; set; }

	[Parameter]
	[Description("Load order group")]
	public string? LoadOrderGroup { get; set; }

	[Parameter]
	[Description("Unique tag within the load order group")]
	[DefaultValue(0)]
	public int Tag { get; set; }

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

	[Parameter]
	[Description("Start the service once created")]
	public SwitchParam Start { get; set; }

	protected sealed override ScmAccess RequiredScmAccess => ScmAccess.CreateService;
	protected sealed override async Task<int> RunAsync(Scm scm, CancellationToken cancellationToken)
	{
		using var svc = await scm.CreateServiceAsync(this.ServiceName, new ServiceConfig
		{
			ServiceType = this.ServiceType,
			StartType = this.StartType,
			ErrorControl = this.ErrorControl,
			BinaryPathName = this.BinPath,
			LoadOrderGroup = this.LoadOrderGroup ?? string.Empty,
			TagId = this.Tag,
			Dependencies = this.Dependencies ?? Array.Empty<string>(),
			ServiceStartName = this.StartName ?? string.Empty,
			StartPassword = this.StartPassword ?? string.Empty,
			DisplayName = this.DisplayName ?? this.ServiceName,
		}, ServiceAccess.MaxAllowed, cancellationToken);

		this.WriteMessage($"Created service '{this.ServiceName}'");
		if (this.Start.IsSet)
		{
			this.WriteVerbose($"Starting service '{this.ServiceName}'...");
			await svc.StartAsync(cancellationToken);
			this.WriteMessage($"Started service '{this.ServiceName}'");
		}

		return 0;
	}
}