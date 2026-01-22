using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal abstract class AddCommandBase : LdapObjectCommandBase
{
	protected abstract string RdnName { get; }
	protected virtual string? DefaultContainer => null;

	[Parameter]
	[Description("Attributes to set as name=value pars")]
	public AttributeChangeSpec[]? Attributes { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (this.Attributes != null)
		{
			foreach (var attr in this.Attributes)
			{
				if (attr.ChangeType != LdapChangeType.Replace)
					throw new SyntaxException($"Attribute {attr.Name} specifies a += or -= operation.  Only = is supported in this context.");
			}
		}
	}

	protected override Task<LdapDistinguishedName> ResolveObjectName(string simpleName, LdapClient ldap, CancellationToken cancellationToken)
	{
		var dn = this.RdnName + "=" + LdapRelativeDistinguishedName.Escape(simpleName);
		var container = this.DefaultContainer;
		if (container != null)
			dn += "," + container;

		dn += "," + ldap.DomainRoot;

		return Task.FromResult(new LdapDistinguishedName(dn));
	}
	protected abstract string NewObjectClass { get; }

	protected abstract Task GetAttributesFor(LdapDistinguishedName dn, Dictionary<string, object> attributes, LdapClient ldap, CancellationToken cancellationToken);

	class LdapAddRequest : ILdapModifyRequest
	{
		internal readonly Dictionary<string, object?> attrValues = new Dictionary<string, object?>();
		public void AddChange(string attributeName, object[] values, LdapChangeType changeType)
		{
			attrValues.Add(attributeName, values);
		}
	}

	protected override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken)
	{

		var addreq = new LdapAddRequest();
		if (this.Attributes != null)
		{
			ChangeContext ctx = new ChangeContext(this.Context);
			ctx.ProcessArgs(this.Attributes, addreq);
		}

		var attributes = addreq.attrValues;
		attributes.Add("objectClass", this.NewObjectClass);

		await this.GetAttributesFor(objName, attributes, ldap, cancellationToken);
		await ldap.Add(objName, attributes, cancellationToken);
	}
}
