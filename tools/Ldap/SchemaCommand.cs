using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Enumeration">Lists attributes defined within an Active Directory forest</task>
[Command]
[Description("Gets the schema")]
[OutputRecordType(typeof(LdapEntry), DefaultFields = [SpecialAttributes.LdapDisplayName, SpecialAttributes.AttributeId, SpecialAttributes.IsSingleValued, SpecialAttributes.AttributeSyntax, SpecialAttributes.OmSyntax, SpecialAttributes.OmObjectClass])]
internal class SchemaCommand : LdapCommandBase
{
	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		string[] attrNames = [SpecialAttributes.LdapDisplayName, SpecialAttributes.IsSingleValued, SpecialAttributes.AttributeId, SpecialAttributes.AttributeSyntax, SpecialAttributes.OmSyntax, SpecialAttributes.OmObjectClass];

		string[] defFields = [SpecialAttributes.LdapDisplayName, SpecialAttributes.AttributeId, SpecialAttributes.IsSingleValued, SpecialAttributes.AttributeSyntax, SpecialAttributes.OmSyntax, "oMObjectClass"/*SpecialAttributes.OmObjectClass*/];
		var attrs = Array.ConvertAll(this.OutputFields ?? defFields, r => new AttributeSpec(r));

		LdapQuery query = new(ldap.SchemaRoot, LdapSearchScope.SingleLevel, FilterFactory.Matches(LdapAttributeTypes.ObjectClass, "attributeSchema"), attrs)
		{
			PageSize = 100,
			Options = LdapQueryOptions.IncludeMissingAttributes | LdapQueryOptions.AllPages
		};

		var res = await ldap.Search(query, cancellationToken);
		this.WriteRecords(res.Entries);

		return 0;
	}
}
