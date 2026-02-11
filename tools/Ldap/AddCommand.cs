using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

[Command]
[Description("Adds an object to the directory")]
internal class AddCommand : AddCommandBase
{
	[Parameter(After = nameof(ObjectName))]
	[Mandatory]
	[Description("Object class of object to add")]
	public string ObjectClass { get; set; }

	protected override string RdnName => "CN";

	protected override string NewObjectClass => this.ObjectClass;

	protected override Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
