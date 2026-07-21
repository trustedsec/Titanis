using System.ComponentModel;
using System.Linq;
using System.Text;
using Titanis.Certificates;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Expanding Access">Create a computer account</task>
[Command]
[Description("Adds a computer account to the directory")]
internal class AddComputerCommand : AddCommandBase
{
	protected override string RdnName => "CN";
	protected override string NewObjectClass => "computer";
	protected override string? DefaultContainer => "CN=Computers";

	[Parameter]
	[Description("Password of new account")]
	public string? NewPassword { get; set; }

	[Parameter]
	[Description("User name for auth requests")]
	public string? LogonName { get; set; }

	[Parameter]
	[Description("Display name for user")]
	public string? DisplayName { get; set; }

	[Parameter]
	[Description("Names of files containing certificates to associate with the user")]
	public FileSpec[]? UserCerts { get; set; }

	[Parameter]
	[Description("Name of installed operating system")]
	public string? Os { get; set; }

	[Parameter]
	[Description("Version of installed operating system")]
	public string? OsVersion { get; set; }

	[Parameter]
	[Description("Groups to make the user a member of")]
	public string[]? MemberOf { get; set; }

	protected override async Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken)
	{
		if (this.NewPassword != null)
		{
			attributes.Add("unicodePwd", new BinaryString(Encoding.Unicode.GetBytes($"\"{this.NewPassword}\"")));
		}

		var logonName = this.LogonName ?? dn.Rdns[0].Values[0];
		attributes.Add("sAMAccountName", logonName);

		if (this.DisplayName != null)
			attributes.Add("displayName", this.DisplayName);
		if (this.Os != null)
			attributes.Add("operatingSystem", this.Os);
		if (this.OsVersion != null)
			attributes.Add("operatingSystemVersion", this.OsVersion);

		attributes.Add("userAccountControl", (int)UserAccountControlFlags.WorkstationTrustAccount);

		if (this.UserCerts != null)
		{
			List<BinaryString> certValues = new List<BinaryString>();
			foreach (var userCertFile in this.UserCerts)
			{
				this.WriteDiagnostic($"Loading certificate from '{userCertFile}'");
				var certs = CertificateHelper.LoadFrom(this.FileAccessService.ReadAllBytesFrom( userCertFile));
				foreach (var cert in certs)
				{
					if (cert.HasEku(ExtendedKeyUsages.ClientAuthentication))
					{
						certValues.Add(new BinaryString(cert.RawData));
					}
				}

				if (certValues.Count > 0)
				{
					attributes.Add("userCertificate", certValues.ToArray());
				}
			}
		}

		if (this.MemberOf != null)
		{
			List<LdapDistinguishedName> groups = new List<LdapDistinguishedName>(this.MemberOf.Length);
			foreach (var groupName in this.MemberOf)
			{
				var groupResults = await ldap.SimpleSearch(groupName, cancellationToken);
				if (groupResults.EntryCount == 1)
				{
					groups.Add(groupResults.Entries[0].EntryName);
				}
				else
				{
					;
				}
			}

			attributes.Add("memberOf", groups.ToArray());
		}
	}
}
