using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Cracks a name")]
[OutputRecordType(typeof(DsrepCrackedName))]
public class CrackNameCommand : DsbindCommand
{
	protected override DsbindScenario Scenario => DsbindScenario.Unspecified;

	[Parameter]
	[Description("Schema GUID(s) to resolve")]
	public Guid[]? BySchemaGuid { get; set; }

	[Parameter]
	[Description("Object GUID(s) to resolve")]
	public Guid[]? ByObjectGuid { get; set; }

	[Parameter]
	[Description("Object DN(s) to resolve")]
	public LdapDistinguishedName[]? ByObjectDn { get; set; }

	[Parameter]
	[Description("SAM account name(s) to resolve")]
	public string[]? BySamAccountName { get; set; }

	[Parameter]
	[Description("User principal name(s) to resolve")]
	public string[]? ByUpn { get; set; }

	[Parameter]
	[Description("Service principal name(s) to resolve")]
	public string[]? BySpn { get; set; }

	[Parameter]
	[Description("Security identifier(s) to resolve")]
	public string[]? BySid { get; set; }

	[Parameter]
	[Description("Canonical name(s) to resolve")]
	public string[]? ByCn { get; set; }

	[Parameter]
	[Description("Display name(s) to resolve")]
	public string[]? ByDisplayName { get; set; }

	[Parameter]
	[Description("Format of name to print")]
	[DefaultValue(DsCrackNameResultFormat.Fqdn1779)]
	public DsCrackNameResultFormat DesiredFormat { get; set; }

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		if (!this.BySchemaGuid.IsNullOrEmpty())
			await this.CrackNames(dsbind, Array.ConvertAll(this.BySchemaGuid, r => r.ToString("B")), DsCrackNameFormat.MapSchemaGuid, cancellationToken);
		if (!this.ByObjectGuid.IsNullOrEmpty())
			await this.CrackNames(dsbind, Array.ConvertAll(this.ByObjectGuid, r => r.ToString("B")), DsCrackNameFormat.UniqueIdName, cancellationToken);
		if (!this.ByObjectDn.IsNullOrEmpty())
			await this.CrackNames(dsbind, Array.ConvertAll(this.ByObjectDn, r => r.ToString()), DsCrackNameFormat.Fqdn1779, cancellationToken);
		if (!this.BySamAccountName.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.BySamAccountName, DsCrackNameFormat.SamAccountNameSansDomain, cancellationToken);
		if (!this.ByUpn.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.ByUpn, DsCrackNameFormat.UserPrincipalName, cancellationToken);
		if (!this.BySpn.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.BySpn, DsCrackNameFormat.ServicePrincipalName, cancellationToken);
		if (!this.BySid.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.BySid, DsCrackNameFormat.StringSidName, cancellationToken);
		if (!this.ByCn.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.ByCn, DsCrackNameFormat.CanonicalName, cancellationToken);
		if (!this.ByDisplayName.IsNullOrEmpty())
			await this.CrackNames(dsbind, this.ByDisplayName, DsCrackNameFormat.DisplayName, cancellationToken);

		return 0;
	}

	private async Task CrackNames(
		DsBinding dsbind,
		string[] names,
		DsCrackNameFormat sourceFormat,
		CancellationToken cancellationToken)
	{
		var cracked = await dsbind.CrackNames(
			names,
			sourceFormat,
			this.DesiredFormat,
			cancellationToken);
		this.WriteRecords(cracked);
	}
}
