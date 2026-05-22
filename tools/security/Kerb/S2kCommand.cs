using System.ComponentModel;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

/// <task category="Kerberos">Generate protocol key from password (offline)</task>
[Command]
[Description("Generates a protocol key from a string, such as a password")]
[DetailedHelpText(@"When authenticating with a password, Kerberos internally generates a protocol key from the password and the accompanying salt using the String-to-key function defined for each encryption profile.  For Windows domains, the salt for a user account is usually the FQDN of the domain in uppercase followed by the account name.  Specifically, the salt is composed of the domain and SAM account name at the time of the last password is changed.  Therefore, if an account has been renamed, the salt retains the old account name until the user changes the password again.

NOTE: Be sure to read the above regarding salts.  Using the wrong salt has the same effect as using the wrong password and may result in account lockout.

You may use `Kerb getasinfo` to get the salt for an account.

For more details, see [MS-KILE] § 3.1.1.2

The domain name used for the salt must be the FQDN of the domain, not the shorter NetBIOS name.
")]
[Example("Generate keys for milchick in domain LUMON.IND", "{0} Br3@kr00m! LUMON.INDseth", Tag = "AllKeys")]
[Example("Generate AES keys for milchick in domain LUMON.IND", "{0} Br3@kr00m! LUMON.INDseth -EncType Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96", Tag = "AesKeys")]
[Example("Generate keys for computer ALLENTOWN$ in domain LUMON.IND", "{0} password LUMON.INDhostallentown.lumon.ind", Tag = "AllAllentown")]
[OutputRecordType(typeof(SessionKey), DefaultFields = new string[] { nameof(SessionKey.EType), nameof(SessionKey.KeyText) })]
public class S2kCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("String, such as the password")]
	public string Password { get; set; }

	[Parameter(After = nameof(Password))]
	[Description("Salt as a string")]
	public string Salt { get; set; }

	[Parameter(After = nameof(Salt))]
	[Description("Encryption types to generate for")]
	public EType[]? EncType { get; set; }

	[Parameter]
	[Description("Continue even if errors occur")]
	public SwitchParam ContinueOnError { get; set; }

	private KerberosClient _krb = new KerberosClient();
	private EType[] _etypes;
	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		this._etypes = this.EncType ?? this._krb.DefaultETypes;
		var hasNonRc4 = this._etypes.Any(r => r is not (EType.Rc4Hmac or EType.Rc4HmacExp));
		if (hasNonRc4)
		{
			if (this.Salt == null)
				context.LogError(nameof(Salt), $"-{nameof(Salt)} is required for encryption types other than Rc4Hmac");
		}
	}

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var etype in this._etypes)
		{
			this.WriteDiagnostic($"Generating key for {etype}");
			var encProfile = _krb.TryGetEncProfile(etype);
			if (encProfile is null)
				this.WriteWarning($"Encryption profile {etype} not available.");

			try
			{
				var key = encProfile.StringToKey(this.Password, this.Salt);
				this.WriteRecord(key);
			}
			catch (Exception ex)
			{
				if (this.ContinueOnError.IsSet)
					this.WriteError($"Error generating key for {etype}: {ex.Message}");
				else
					throw;
			}
		}

		return Task.FromResult(0);
	}
}
