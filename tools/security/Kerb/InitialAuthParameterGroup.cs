using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Kerb;

/// <summary>
/// Defines parameters for initial authentication with a KDC.
/// </summary>
internal class InitialAuthParameterGroup : ParameterGroupBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(KdcCommand.KdcPosition - 1)]
	[Mandatory]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of user (no domain)")]
	public UserPrincipalName UserName { get; set; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of realm (domain)")]
	public string? Realm { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


	internal string? EffectiveRealm => this.Realm ?? this.UserName.Realm;

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Password")]
	public string? Password { get; set; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("NTLM hash (hex-encoded, no colons)")]
	public HexString? NtlmHash { get; set; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("AES 128 key")]
	public HexString? Aes128Key { get; set; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("AES 256 key")]
	public HexString? Aes256Key { get; set; }


	internal void Validate(ParameterValidationContext context)
	{
		var realm = this.Realm ?? this.UserName.Realm;
		if (string.IsNullOrEmpty(realm))
			context.LogError(new ParameterValidationError(nameof(Realm), $"Realm must be specified either with -{nameof(Realm)} or as part of -{nameof(UserName)}"));

		int credCount = 0;
		if (this.Password != null) credCount++;
		if (this.NtlmHash != null) credCount++;
		if (this.Aes128Key != null) credCount++;
		if (this.Aes256Key != null) credCount++;

		if (credCount != 1)
			context.LogError(new ParameterValidationError(null, "The command line must specify exactly one (1) credential."));

	}

	internal async Task<TicketInfo> RequestInitialTicket(
		KerberosClient krb,
		SecurityPrincipalName? spn,
		EType[]? etypes,
		TicketParameters? ticketParams,
		CancellationToken cancellationToken,
		ILog log)
	{
		KerberosCredential cred = GetCredential();

		var ticket = await krb.RequestInitialTicket(this.EffectiveRealm, cred, spn, ticketParams, etypes, cancellationToken).ConfigureAwait(false);
		return ticket;
	}

	public KerberosCredential GetCredential()
	{
		var realm = this.EffectiveRealm;
		var userName = this.UserName.UserName;

		return (this.Password != null) ? new KerberosPasswordCredential(userName, realm, this.Password)
			: (this.NtlmHash != null) ? new KerberosKeyCredential(userName, realm, EType.Rc4Hmac, this.NtlmHash.Bytes)
			: (this.Aes128Key != null) ? new KerberosKeyCredential(userName, realm, EType.Aes128CtsHmacSha1_96, this.Aes128Key.Bytes)
			: (this.Aes256Key != null) ? new KerberosKeyCredential(userName, realm, EType.Aes256CtsHmacSha1_96, this.Aes256Key.Bytes)
			: throw new SyntaxException("No credential provided");
	}
}
