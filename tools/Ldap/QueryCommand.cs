using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Ldap.FilterExpressions;

namespace Ldap;

[Command]
[Description("Queries the directory")]
[DetailedHelpText(@"{0} issues a query to an LDAP server.  Use -OutputFields to specify the names of the attributes to retrieve; by default, only the DN of the entries is printed.

If no search base is provided, {0} uses the root of the domain.

-Filter accepts an LDAP query.  An LDAP query consists of one or more assertions of the form

	(<attr> <op> <value>)

where <op> is one of:
  =   (exact match, has attribute, or matches substring)
  ~=  (approximate match)
  <=  (less or equal)
  >=  (greater or equal)
  &=  (has all bits) (LDAP_MATCHING_RULE_BIT_AND)
  |=  (has one or more bits) (LDAP_MATCHING_RULE_BIT_OR)
  *=  (transitive match) (LDAP_MATCHING_RULE_TRANSITIVE_EVAL)

NOTE: Active Directory treats `=` and `~=` the same, although the queries are represented differently on the wire.
NOTE: `&=`, `|=`, and `*=` are extensions implemented by Active Directory.

To invert a filter and return objects that do not meet the criteria, prepend a `!`.  For example, to return disabled accounts:

To query objects with an attribute, use `=*`.  For example, to query objects with a servicePrincipalName, use:

  (servicePrincipalName=*)

To combine multiple assertions, specify a `&` (all must match) or `|` (at least one must match) followed by multiple filter clauses, surrounding the entire expression with `(` and `)`.  For example:

  (&(attr1=value)(attr2=value)(attr3=value))

A few of the fields support named bits.


NOTE: Although not strictly required, it is a good idea to surround the filter with quotes to avoid having to escape special characters.")]
[Example("Find User with Logon Name 'milchick'", "{0} LUMON-DC1 '(samAccountName=milchick)' -OutputFields distinguishedName, objectSid")]
[Example("Find Objects with SPNs", "{0} LUMON-DC1 '(servicePrincipalName=*)' -OutputFields distinguishedName, objectSid, servicePrincipalName")]
internal class QueryCommand : QueryCommandBase
{
	[Parameter(20)]
	[Description("LDAP query")]
	public string? Filter { get; set; }

	private LdapFilter? _filter;
	protected override void ValidateParameters(ParameterValidationContext context)
	{
		if (!string.IsNullOrEmpty(this.Filter))
		{
			try
			{
				this._filter = FilterExpression.Parse(this.Filter).ToFilter();
			}
			catch (Exception ex)
			{
				context.LogError(nameof(Filter), ($"Error while parsing filter expression '{this.Filter}': {ex.Message}"));
			}
		}

		base.ValidateParameters(context);
	}

	protected override LdapQuery CreateQuery(LdapDistinguishedName searchBase)
	{
		var query = new LdapQuery(searchBase, LdapSearchScope.WholeSubtree, this._filter, []);
		return query;
	}
}
