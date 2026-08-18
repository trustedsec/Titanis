using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msdrsr;
using Titanis.Security;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Adds SID history from one principal to another")]
public class AddSidHistoryCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Source user (must include domain)")]
	public UserPrincipalName SourceUser { get; set; }

	[Parameter(After = nameof(SourceUser))]
	[Mandatory]
	[Description("Destination user (must include domain)")]
	public UserPrincipalName DestinationUser { get; set; }

	[Parameter]
	[Description("Source domain controller")]
	public string? SourceDc { get; set; }

	[Parameter]
	[Description("User name to authenticate to source DC")]
	public UserPrincipalName? SourceAuthUser { get; set; }

	[Parameter]
	[Description("User name to authenticate to source DC")]
	public string? SourcePassword { get; set; }

	[Parameter]
	[Description("Checks whether the channel is secure")]
	public SwitchParam CheckSecure { get; set; }

	[Parameter]
	[Description("Deletes the source object")]
	public SwitchParam DeleteSource { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		if (string.IsNullOrEmpty(this.SourceUser.Realm))
			context.LogError(nameof(SourceUser), "The source user must include a domain name.");

		if (string.IsNullOrEmpty(this.DestinationUser.Realm))
			context.LogError(nameof(DestinationUser), "The destination user must include a domain name.");

		base.ValidateParameters(context);
	}

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var options = DsrepAddSidHistoryOptions.None;
		if (this.CheckSecure.IsSet) options |= DsrepAddSidHistoryOptions.CheckSecure;
		if (this.DeleteSource.IsSet) options |= DsrepAddSidHistoryOptions.DeleteSourceObject;

		await dsbind.AddSidHistory(
			options,
			this.SourceUser.Realm,
			this.SourceUser.UserName,
			this.SourceDc,
			this.SourceUser?.UserName,
			this.SourceUser?.Realm,
			this.SourcePassword,
			this.DestinationUser.Realm,
			this.DestinationUser.UserName,
			cancellationToken
			);
		return 0;
	}
}
