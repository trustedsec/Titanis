using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;

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

		//LdapQuery query = new(ldap.SchemaRoot, LdapSearchScope.SingleLevel, FilterFactory.Matches(LdapAttributeTypes.Name, "Address-Book-Roots"), attrs)
		LdapQuery query = new(ldap.SchemaRoot, LdapSearchScope.SingleLevel, FilterFactory.Matches(LdapAttributeTypes.ObjectClass, "attributeSchema"), attrs)
		{
			PageSize = 100,
			Options = LdapQueryOptions.IncludeMissingAttributes
		};

		do
		{
			var res = await ldap.Search(query, cancellationToken);
			query.PagingBookmark = res.Bookmark;

			this.WriteRecords(res.Entries);
		} while (!query.PagingBookmark.IsNullOrEmpty());

		return 0;
	}
}
