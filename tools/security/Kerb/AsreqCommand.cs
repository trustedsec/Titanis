using System.ComponentModel;
using System.Net;
using Titanis.Cli;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	/// <task category="Kerberos;Expanding Access">Request a ticket-granting-ticket</task>
	/// <task category="Kerberos;Enumeration">Check whether a user account requires pre-authentication</task>
	/// <task category="Kerberos;Enumeration">Check the encryption types supported for a user account</task>
	/// <task category="Enumeration">Check whether a user name is valid</task>
	[Command]
	[Description("Requests a TGT from the KDC.")]
	[DetailedHelpText(@"This command sends an AS-REQ to the KDC to request a ticket-granting ticket.

The command line must include either a password or a hex-encoded key that is used both for preauthentication as well as to decrypt the response.  When specifying the NTLM hash, specify just the NTLM portion with no colon.

The provided credential determines the encryption type of the response.  If you provide a password then all encryption types supported by {0} are presented.  To override this, use -EncTypes to specify a list of encryption types to accept.  Note that this only effects the encryption used in the response and not the preauthorization data.

Dates/times are interpreted as local time unless otherwise specified.  If only a time is supplied, the assumed date is today.

Note that the ticket flags and time parameters affect the request sent to the KDC.  However, the KDC is free to ignore them; specifying an option doesn't guarantee that the ticket will have the requested option.

If you don't specify any options for the ticket, {0} uses default values, requesting a ticket that expires 10 hours from now with the options Canonicalize, RenewableOk, Renewable, and Forwardable.  If any options are specified, then no default values are applied and only the options specified are used.
")]
	[Example("Requesting a TGT with a user name / password", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite", Tag = "milchick_password")]
	[Example("Requesting a TGT with a UPN / password", "{0} -UserName milchick@LUMON.IND -Password Br3@kr00m! -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite", Tag = "milchickUpn_password")]
	[Example("Requesting a TGT with PKINIT", "{0} -UserName milchick@LUMON.IND -UserCert milchick.pfx -UserKeyPassword password -Kdc LUMON-DC1 -v -OutputFileName milchick-tgt.kirbi -Overwrite", Tag = "milchick_pkinit")]

	[Example("Requesting a TGT with a password request Rc4Hmac", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Rc4Hmac -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with a password request AES 128 or AES 256", "{0} -UserName milchick -Realm LUMON -Password Br3@kr00m! -EncTypes Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96 -Kdc 10.66.0.11 -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an NTLM Hash", "{0} -UserName milchick -NtlmHash B406A01772D0AD225D7B1C67DD81496F -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an AES 128 key", "{0} -UserName milchick -AesKey c5673764957bc2839e367ba7b82f32e1 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	[Example("Requesting a TGT with an AES 256 key", "{0} -UserName milchick -AesKey 76332deee4296dcb20200888630755268e605c8576e50ff38db2d8b92351f4e4 -Kdc 10.66.0.11 -Realm LUMON -v -OutputFileName milchick-tgt.kirbi -Overwrite")]
	public class AsreqCommand : KdcRequestCommand
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

		[ParameterGroup(ParameterGroupOptions.Required)]
		public InitialAuthParameterGroup InitialAuth { get; set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Encryption types to request in response")]
		public EType[]? EncTypes { get; set; }


		[ParameterGroup]
		public TicketParameterGroup? TicketParamGroup { get; set; }

		[Parameter]
		[Description("SPNs to request ticket(s) for")]
		public SecurityPrincipalName[]? Target { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this.InitialAuth.Validate(context);

			if (this.EncTypes != null && this.InitialAuth.Password == null)
				context.LogError(nameof(EncTypes), "EncTypes may only be specified along with -Password");

			if (this.Target == null)
				this.Target = [null];
		}

		protected sealed override async Task<IList<TicketInfo>> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
		{
			List<TicketInfo> tickets = new List<TicketInfo>();
			TicketParameters? ticketParams = this.TicketParamGroup?.GetTicketParameters(this.Log, KerberosClient.DefaultTgtOptions);
			TicketInfo? armorTicket = this.ArmorTicket != null ? this.LoadTgtFromStore(krb, this.ArmorTicket) : null;
			ticketParams.ArmorTicket = armorTicket;
			foreach (var target in this.Target)
			{
				this.WriteDiagnostic($"Requesting ticket for target={((target is null) ? "<null>" : target)}");
				var ticket = await this.InitialAuth.RequestInitialTicket(
					krb,
					target,
					this.EncTypes,
					ticketParams,
					cancellationToken,
					this.Log);

				var realm = this.InitialAuth.EffectiveRealm;
				if (!string.Equals(ticket.TicketRealm, realm, StringComparison.OrdinalIgnoreCase))
					this.WriteWarning($"The ticket realm '{ticket.TicketRealm}' does not match the requested realm '{realm}'.  This may be the result of canonicalization.");

				tickets.Add(ticket);
			}

			return tickets;
		}
	}
}