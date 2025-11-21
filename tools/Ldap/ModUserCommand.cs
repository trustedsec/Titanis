using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal class ModUserCommand : LdapObjectCommandBase
{
	protected override string RdnName => "CN";

	protected override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken)
	{
		Dictionary<string, object> attributes = new Dictionary<string, object>();
		attributes.Add("unicodePwd", Encoding.Unicode.GetBytes("\"password\""));

		await ldap.Modify(objName, attributes, cancellationToken);
	}
}
