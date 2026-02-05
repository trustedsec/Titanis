using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb;

/// <task category="Kerberos">Generate protocol key from password</task>
[Command]
[Description("Generates a protocol key from a string, such as a password")]
[DetailedHelpText(@"When authenticating with a password, Kerberos internally generates a protocol key from the password and the accompanying salt using the String-to-key function defined for each encryption profile.  For Windows domains, the salt for a user account is usually the FQDN of the domain in uppercase followed by the account name.  Specifically, the salt is composed of the domain and SAM account name at the time of the last password is changed.  Therefore, if an account has been renamed, the salt retains the old account name until the user changes the password again.

NOTE: Be sure to read the above regarding salts.  Using the wrong salt has the same effect as using the wrong password and may result in account lockout.

You may use `Kerb getasinfo` to get the salt for an account.

For more details, see [MS-KILE] § 3.1.1.2

The domain name used for the salt must be the FQDN of the domain, not the shorter NetBIOS name.
")]
[Example("Generate keys for milchick in domain LUMON.IND", "{0} LUMON.INDmilchick Br3@kr00m!")]
[Example("Generate AES keys for milchick in domain LUMON.IND", "{0} LUMON.INDmilchick Br3@kr00m! -EncTypes Aes128CtsHmacSha1_96, Aes256CtsHmacSha1_96")]
[Example("Generate keys for computer ALLENTOWN$ in domain LUMON.IND", "{0} LUMON.INDhostallentown.lumon.ind password")]
[OutputRecordType(typeof(SessionKey), DefaultFields = new string[] { nameof(SessionKey.EType), nameof(SessionKey.KeyText) })]
internal class S2kCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("Salt as a string")]
	public string Salt { get; set; }

	[Parameter(10)]
	[Mandatory]
	[Description("String, such as the password")]
	public string Password { get; set; }

	[Parameter]
	[Description("Encryption types to generate for")]
	public EType[]? EncType { get; set; }

	[Parameter]
	[Description("Continue even if errors occur")]
	public SwitchParam ContinueOnError { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		KerberosClient krb = new KerberosClient(null);
		var etypes = this.EncType ?? krb.DefaultETypes;

		foreach (var etype in etypes)
		{
			this.WriteDiagnostic($"Generating key for {etype}");
			var encProfile = krb.TryGetEncProfile(etype);
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
