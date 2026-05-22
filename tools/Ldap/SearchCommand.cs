using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Enumeration">Search for objects by name in Active Directory</task>
[Command]
[Description("Searches the directory by name")]
[DetailedHelpText(@"{0} uses the ANR feature of Active Directory to find objects where any designated name-like field begins with a search string.

To request items that match exactly (rather than those beginning with) a search term, prepend `=` to the search term.

Other substring searches (contains or begins with) are not supported; wildcards will be interpreted literally.

Note that these rules are observed and enforced by Active Directory; {0} merely sends what you give it.")]
[Example(@"Search for accounts beginning with `admin`", "{0} admin")]
[Example(@"Search for accounts matching `milchick` exactly", "{0} =milchick")]
[Example("Search using SSL (Kerberos)", "{0} LUMON-DC1 -UserName marks@LUMON -Password She's@live!! -Kdc LUMON-DC1 -Ssl milchick", Tag = "milchickKerb_ldaps_ChannelBinding")]
[Example("Search using SSL (NTLM)", "{0} LUMON-DC1 -UserName marks@LUMON -Password She's@live!! -Ssl milchick", Tag = "milchickNtlm_ldaps_ChannelBinding")]
public class SearchCommand : QueryCommandBase
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Name to search for")]
	public string[] SearchName { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		foreach (var name in this.SearchName)
		{
			if (name.Contains('*'))
				this.WriteWarning($"Search filter {name} contains a wildcard and will likely fail (unless the record itself contains the wildcard).");
		}

		base.ValidateParameters(context);
	}

	protected override LdapQuery CreateQuery(LdapDistinguishedName searchBase)
	{
		LdapFilter? filter = null;
		foreach (var name in this.SearchName)
		{
			var clause = FilterFactory.Matches(LdapAttributeTypes.ANR, name);

			filter =
				(filter is null) ? clause
				: filter.Or(clause);
		}

		var query = new LdapQuery(searchBase, LdapSearchScope.WholeSubtree, filter, []);
		return query;
	}
}
