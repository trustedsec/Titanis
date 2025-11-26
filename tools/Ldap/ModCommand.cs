using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;
internal class ModCommand : LdapObjectCommandBase
{
	[Parameter(30)]
	[Description("Changes to make as name?=value")]
	public AttributeChangeSpec[]? Changes { get; set; }

	protected override async Task<LdapDistinguishedName> ResolveObjectName(string simpleName, LdapClient ldap, CancellationToken cancellationToken)
	{
		var result = await ldap.SimpleSearch(simpleName, cancellationToken);
		if (result.EntryCount == 1)
			return result.Entries[0].EntryName;
		else
		{
			this.WriteError($"The search for '{simpleName}' return multiple results:");
			foreach (var entry in result.Entries)
			{
				this.WriteMessage($"DN: {entry.EntryName}");
			}

			throw new InvalidOperationException($"The name '{simpleName}' resolved to multiple objects.  Either specify a more restrictive search string or specify the DN of the desired object.");
		}
	}

	protected virtual void GetChanges(LdapModifyRequest modifyRequest)
	{

	}

	struct ChangeContext
	{
		internal ChangeContext(LdapModifyRequest request)
		{
			this.request = request;
			this.values = new List<object>();
		}

		internal readonly LdapModifyRequest request;
		internal readonly List<object> values;
		internal string? lastName;
		internal LdapChangeType changeType;

		internal void ProcessChange(AttributeChangeSpec change)
		{
			if (lastName != null && (
				(lastName != change.Name)
				|| (changeType != change.ChangeType)
				))
			{
				CommitChange();
			}

			lastName = change.Name;
			changeType = change.ChangeType;
			values.Add(change.Value);
		}

		internal readonly void CommitChange()
		{
			request.AddChange(new LdapAttributeChange(lastName, values.ToArray(), changeType));
			values.Clear();
		}
	}

	protected sealed override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken)
	{
		LdapModifyRequest request = new LdapModifyRequest(objName);
		if (this.Changes != null)
		{
			ChangeContext ctx = new ChangeContext(request);
			foreach (var change in this.Changes)
			{
				ctx.ProcessChange(change);
			}
			ctx.CommitChange();
		}

		this.GetChanges(request);

		await ldap.Modify(request, cancellationToken);
	}
}
