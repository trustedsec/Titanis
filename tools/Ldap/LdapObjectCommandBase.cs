using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

public abstract class LdapObjectCommandBase : LdapCommandBase
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Names or DNs of objects to create")]
	public string[] ObjectName { get; set; }

	protected virtual async Task<LdapEntry?> ResolveObjectName(string simpleName, LdapClient ldap, AttributeSpec[] attributes, CancellationToken cancellationToken)
	{
		var result = await ldap.SimpleSearch(simpleName, attributes, cancellationToken);
		if (result.EntryCount == 0)
			return null;
		else if (result.EntryCount == 1)
			return result.Entries[0];
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

	/// <summary>
	/// Gets a list of attributes that must be fetched before the command runs on an object.
	/// </summary>
	/// <returns>An array of <see cref="AttributeSpec"/> specifying the attributes</returns>
	/// <remarks>
	/// If the returned list is not empty, the attributes are fetched on each object before calling <see cref="RunAsync(LdapClient, LdapDistinguishedName, LdapEntry?, CancellationToken)"/>.
	/// No validation is performed; if the attributes are not present, no error is reported.
	/// </remarks>
	protected virtual AttributeSpec[]? GetRequiredObjAttributes() => null;

	protected sealed override async Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken)
	{
		bool hasMatch = false;

		bool hasReqAttrs = false;
		var reqAttrs = this.GetRequiredObjAttributes();
		if (reqAttrs is null)
			reqAttrs = [LdapAttributeTypes.DistinguishedName];
		else
			hasReqAttrs = true;

		await this.OnBeforeProcessObjects(ldap, cancellationToken);
		foreach (var name in this.ObjectName)
		{
			LdapDistinguishedName dn;
			LdapEntry? entry = null;
			if (!name.Contains('='))
			{
				// This is a simple namee
				entry = await this.ResolveObjectName(name, ldap, reqAttrs, cancellationToken);
				if (entry is null)
				{
					this.WriteWarning($"No object found matching '{name}'");
					continue;
				}
				dn = entry.EntryName;
			}
			else
			{
				var fullName = name;

				// This is relative to the domain root
				if (!name.Contains(",DC="))
					fullName += "," + ldap.DomainRoot;
				dn = new LdapDistinguishedName(fullName);

				if (hasReqAttrs)
					entry = await ldap.Get(dn, reqAttrs, cancellationToken);
			}

			hasMatch = true;
			await RunAsync(ldap, dn, entry, cancellationToken);
		}
		await this.OnAfterProcessObjects(ldap, cancellationToken);

		return 0;
	}

	protected virtual Task OnBeforeProcessObjects(LdapClient ldap, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	protected virtual Task OnAfterProcessObjects(LdapClient ldap, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	protected abstract Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, LdapEntry? existingEntry, CancellationToken cancellationToken);
}
