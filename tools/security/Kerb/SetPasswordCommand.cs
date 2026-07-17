using System.ComponentModel;
using System.Net;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <task category="Expanding Access;Kerberos">Set the password of another user account</task>
[Command]
[Description("Sets the password of (another) account")]
[DetailedHelpText(@"{0} uses the Windows 2000 Kerberos Change Password protocol (RFC 3244) and can be used to change the password of a user account that may or may not be the same as the authenticating user.  This service does not require an initial ticket and is more flexible than `changepw`.")]
[Example("milchick setting his own password", "{0} -UserName milchick@LUMON -Kdc 10.66.0.11 -Password Br3@kr00m! milchick@lumon.ind EradicateFolly!")]
[Example("milchick setting password for marks", "{0} -UserName milchick@LUMON -Kdc 10.66.0.11 -Password Br3@kr00m! marks@lumon.ind SafelySituated")]
public class SetPasswordCommand : Command, IHaveServerName
{
	[Parameter(0)]
	// NOTE: [RFC 3244] declares targname and targrealm as optional, although in practice this fails
	[Mandatory]
	[Description("Optional name of account to set password of")]
	public UserPrincipalName? TargetAccount { get; set; }

	[Parameter(1)]
	[Mandatory]
	[Description("New password to set")]
	public string NewPassword { get; set; }

	// Using AuthenticationParameters instead of InitialAuthParameterGroup allows the use of ticket files
	[ParameterGroup(ParameterGroupOptions.Required)]
	public AuthenticationParameters Authentication { get; set; }
	string? IHaveServerName.ServerName => (this.Authentication.Kdc as DnsEndPoint)?.Host;

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public NetworkParameters NetworkParameters { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);
		if (this.Authentication.Kdc == null)
			context.LogError(new ParameterValidationError(nameof(this.Authentication.Kdc), $"The -{nameof(AuthenticationParameters.Kdc)} must be specified."));
		this.Authentication.Validate(true, context, requiresKerberos: true);
	}

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var krb = this.CreateKerberosClient(new SimpleKdcLocator(this.Authentication.Kdc));

		(var krbAuthContext, _) = await this.Authentication.TryCreateKerberosContext(KerberosClient.ChangePwSpn, SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality, false);
		var cred = krbAuthContext.Credential;

		await krb.SetPassword(
			this.Authentication.Kdc.WithPort(464),
			krbAuthContext.Ticket,
			krbAuthContext.Credential,
			this.NewPassword,
			this.TargetAccount,
			this.TargetAccount.Realm ?? krbAuthContext.Ticket.TicketRealm,
			HostAddress.FromIPAddress(System.Net.IPAddress.Any),
			cancellationToken);

		this.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, $"Password changed for user '{this.TargetAccount ?? cred.UserName}@{TargetAccount?.Realm ?? cred.Realm}'."));

		return 0;
	}
}
