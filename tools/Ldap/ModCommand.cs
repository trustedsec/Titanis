using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

[Command]
[Description("Modifies an object in the directory")]
[Example("Add a certificate to an account", "{0} LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m! ALLENTOWN$  userCertificate:file+=allentown.cer", "This command authenticates as milchick, loads the certificate from the file allentown.cer, and associates it with the ALLENTOWN$ account.")]
[Example("Adding resource-based constrained delegate to a computer account", "{0} LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m!  Stealth$ msDS-AllowedToDelegateTo+=HOST/ALLENTOWN, msDS-AllowedToDelegateTo+=cifs/ALLENTOWN", "This command authenticates as milchick and allows the STEALTH$ account to delegate to ALLENTOWN for the `cifs` and `host` SPNs.")]
internal class ModCommand : LdapObjectCommandBase
{
	[Parameter(After = nameof(ObjectName))]
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

	protected virtual void GetAdditionalChanges(LdapModifyRequest modifyRequest)
	{

	}

	protected sealed override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, CancellationToken cancellationToken)
	{
		LdapModifyRequest request = new LdapModifyRequest(objName);
		if (this.Changes != null)
		{
			ChangeContext ctx = new ChangeContext(this.Context);
			ctx.ProcessArgs(this.Changes, request);
		}

		this.GetAdditionalChanges(request);

		await ldap.Modify(request, cancellationToken);
	}
}

struct ChangeContext
{
	internal ChangeContext(ICommandContext command)
	{
		this.command = command;
		this.values = new List<object>();
	}

	private readonly ICommandContext command;
	internal readonly List<object> values;
	internal string? lastAttrName;
	internal LdapChangeType changeType;

	internal void ProcessChange(AttributeChangeSpec change, ILdapModifyRequest request)
	{
		if (lastAttrName != null && (
			(lastAttrName != change.Name)
			|| (changeType != change.ChangeType)
			))
		{
			CommitChange(request);
		}

		lastAttrName = change.Name;
		changeType = change.ChangeType;

		object? value = change.Encoding switch
		{
			AttributeEncoding.Unspecified => LdapAttribute.ParseSpecialValue(change.Name, change.Value),
			AttributeEncoding.File => File.ReadAllBytes(this.command.ResolveFsPath(change.Value)),
			AttributeEncoding.Hex => BinaryHelper.ParseHexString(change.Value),
			AttributeEncoding.Base64 => Convert.FromBase64String(change.Value),
			_ => throw new FormatException($"Unsupported encoding {change.Encoding}.")
		};
		values.Add(value);
	}

	internal readonly void CommitChange(ILdapModifyRequest request)
	{
		request.AddChange(lastAttrName, values.ToArray(), changeType);
		values.Clear();
	}

	internal void ProcessArgs(AttributeChangeSpec[] attrs, ILdapModifyRequest request)
	{
		foreach (var change in attrs)
		{
			this.ProcessChange(change, request);
		}
		this.CommitChange(request);
	}
}
