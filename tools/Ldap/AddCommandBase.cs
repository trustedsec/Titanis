using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

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

	protected override Task<LdapEntry> ResolveObjectName(string simpleName, LdapClient ldap, AttributeSpec[] attributes, CancellationToken cancellationToken)
	{
		var dn = this.RdnName + "=" + LdapRelativeDistinguishedName.Escape(simpleName);
		var container = this.DefaultContainer;
		if (container != null)
			dn += "," + container;

		dn += "," + ldap.DomainRoot;

		return Task.FromResult(new LdapEntry(new LdapDistinguishedName(dn), Array.Empty<LdapAttribute>()));
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

	protected override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, LdapEntry? existingEntry, CancellationToken cancellationToken)
	{

		var addreq = new LdapAddRequest();
		if (this.Attributes != null)
		{
			ChangeContext ctx = new ChangeContext(this.Services);
			ctx.ProcessArgs(this.Attributes, addreq);
		}

		var newAttrs = addreq.attrValues;
		newAttrs.Add("objectClass", this.NewObjectClass);

		await this.GetAttributesFor(objName, newAttrs, ldap, cancellationToken);
		await ldap.Add(objName, newAttrs, cancellationToken);
	}
}
