using System.ComponentModel;
using Titanis.Net;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <task category="Expanding Access;Kerberos">Change a password</task>
[Command]
[Description("Changes an account password")]
[DetailedHelpText(@"{0} uses the Kerberos Change Password protocol and can only be used to change the password of the authenticating user.  To set the password of another user, use the `setpw` command.

This protocol requires an initial ticket.  That is, it requires a ticket from an ASREQ/ASREP exchange and not from a TGSREQ/TGSREP exchange.  Therefore, this command requires credentials and does not accept a ticket as a parameter.  The `setpw` command does not have this restriction and accepts a ticket as a parameter.")]
[Example("milchick changing his own password", "{0} milchick@LUMON 10.66.0.11 -Password EradicateFolly! Br3@kr00m!")]
public class ChangePasswordCommand : KdcCommand
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public InitialAuthParameterGroup InitialAuth { get; set; }

	[Parameter(KdcPosition + 1)]
	[Mandatory]
	[Description("New password to set")]
	public string NewPassword { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var krb = this.CreateKerberosClient();

		var ticket = await this.InitialAuth.RequestInitialTicket(
			krb,
			KerberosClient.ChangePwSpn,
			null,
			null,
			cancellationToken,
			this.Log);

		KerberosCredential cred = this.InitialAuth.GetCredential(this.Log);

		await krb.ChangePassword(
			this.Kdc.WithPort(464),
			ticket,
			cred,
			this.NewPassword,
			HostAddress.FromIPAddress(System.Net.IPAddress.Any),
			cancellationToken);

		this.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, $"Password changed for user '{cred.UserName}@{cred.Realm}'."));

		return 0;
	}
}
