using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Titanis.Security;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <summary>
/// Defines parameters for initial authentication with a KDC.
/// </summary>
public class InitialAuthParameterGroup : ParameterGroupBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	private UserPrincipalName _userName;
	[Parameter(KdcCommand.KdcPosition - 1)]
	[Mandatory]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of user (no domain)")]
	public UserPrincipalName UserName { get => _userName; set => _userName = value; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of realm (domain)")]
	public string? Realm { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	[ParameterGroup]
	public UserCertificateParameterGroup? CertificateParameters { get; set; }

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
	[Advanced]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("AES 128 key")]
	public HexString? AesKey { get; set; }

	[Parameter]
	[Advanced]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("DES key")]
	public HexString? DesKey { get; set; }

	[Parameter(EnvironmentVariable = "KRB5_CLIENT_KTNAME")]
	[Advanced]
	[Description("Name of keytab file")]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[KeytabFileSpec(true)]
	public FileSpec? Keytab { get; set; }

	private X509Certificate2? _userCert;

	internal void Validate(ParameterValidationContext context)
	{
		var realm = this.Realm ?? this.UserName.Realm;
		if (string.IsNullOrEmpty(realm))
			context.LogError(new ParameterValidationError(nameof(Realm), $"Realm must be specified either with -{nameof(Realm)} or as part of -{nameof(UserName)}"));

		this._userCert = this.CertificateParameters?.Validate(context, ref this._userName);

		int credCount = 0;
		if (this.Password != null) credCount++;
		if (this.NtlmHash != null) credCount++;
		if (this.AesKey != null) credCount++;
		if (this.DesKey != null) credCount++;
		if (this._userCert != null) credCount++;
		if (this.Keytab != null) credCount++;

		if (credCount != 1)
			// TODO: Now that KerberosKeyCredential supports multiple keys, remove this constraint
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
		KerberosCredential cred = GetCredential(log);

		var ticket = await krb.RequestInitialTicket(this.EffectiveRealm, cred, spn, ticketParams, etypes, cancellationToken).ConfigureAwait(false);
		return ticket;
	}

	public KerberosCredential GetCredential(ILog? log)
	{
		var realm = this.EffectiveRealm;
		var userName = this.UserName.WithRealm(realm);

		if (this._userCert != null)
		{
			return new KerberosPkinitCredential(userName, this._userCert);
		}

		return (this.Password != null) ? new KerberosPasswordCredential(userName, this.Password)
			: (this.NtlmHash != null) ? new KerberosKeyCredential(userName, EType.Rc4Hmac, this.NtlmHash.Bytes)
			: (this.AesKey != null) ? new KerberosKeyCredential(userName, (this.AesKey.Bytes.Length switch
			{
				(128 / 8) => EType.Aes128CtsHmacSha1_96,
				(256 / 8) => EType.Aes256CtsHmacSha1_96,
				_ => throw new ArgumentException("The AES key is not the correct size for AES 128 or AES 256.")
			}), this.AesKey.Bytes)
			: (this.DesKey != null) ? new KerberosKeyCredential(userName, EType.DesCbcMd5, this.DesKey.Bytes)
			: (this.Keytab != null) ? (this.LoadKeytab() ?? throw new InvalidOperationException("Unable to load credentials from keytab"))
			: throw new SyntaxException("No credential provided");
	}

	private KerberosKeyCredential? LoadKeytab()
	{
		var keys = AuthenticationParameters.LoadKeytab(this.Keytab, this.UserName, this.Realm, this.RequireFileAccess(), this.Log);
		if (keys.IsNullOrEmpty())
			return null;
		return new KerberosKeyCredential(this.UserName, keys.Select(r => r.ToEncryptionKey()));
	}
}
