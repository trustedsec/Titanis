using System.ComponentModel;
using Titanis.Ldap;
using Titanis.Winterop.Security;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Expanding Access">Modify an object in Active Directory</task>
[Command]
[Description("Modifies an object in the directory")]
[Example("Add a certificate to an account", "{0} LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m! ALLENTOWN$  userCertificate:file+=allentown.cer", "This command authenticates as milchick, loads the certificate from the file allentown.cer, and associates it with the ALLENTOWN$ account.")]
[Example("Adding resource-based constrained delegate to a computer account", "{0} LUMON-DC1 -UserName milchick@LUMON -Password Br3@kr00m!  Stealth$ msDS-AllowedToDelegateTo+=HOST/ALLENTOWN, msDS-AllowedToDelegateTo+=cifs/ALLENTOWN", "This command authenticates as milchick and allows the STEALTH$ account to delegate to ALLENTOWN for the `cifs` and `host` SPNs.")]
internal class ModCommand : LdapObjectCommandBase
{
	private List<SecurityIdentifier>? _allowAltIdentities;

	[Parameter(After = nameof(ObjectName))]
	[Description("Changes to make as name?=value")]
	public AttributeChangeSpec[]? Changes { get; set; }

	[Parameter]
	[Description("Account name to add to msDS-AllowedToActOnBehalfOfOtherIdentity")]
	public string[]? AllowOnBehalfOf { get; set; }

	protected override AttributeSpec[]? GetRequiredObjAttributes()
	{
		if (this.AllowOnBehalfOf != null)
			return [LdapAttributeTypes.MsDSAllowedToActOnBehalfOfOtherIdentity];
		else
			return base.GetRequiredObjAttributes();
	}

	protected override async Task OnBeforeProcessObjects(LdapClient ldap, CancellationToken cancellationToken)
	{
		base.OnBeforeProcessObjects(ldap, cancellationToken);

		if (this.AllowOnBehalfOf != null)
		{
			var users = new List<SecurityIdentifier>();
			// TODO: This should support SIDs, DNs, and samAccountNames
			foreach (var name in this.AllowOnBehalfOf)
			{
				var results = await ldap.SimpleSearch(name, [LdapAttributeTypes.ObjectSid], cancellationToken);
				var entry = results.Entries.FirstOrDefault();
				var sid = entry?[LdapAttributeTypes.ObjectSid.Name]?.Value as SecurityIdentifier;
				if (sid != null)
				{
					users.Add(sid);
				}
			}

			if (users.Count > 0)
				this._allowAltIdentities = users;
		}
	}

	/// <summary>
	/// Gets additional changes to apply.
	/// </summary>
	/// <param name="modifyRequest"></param>
	protected virtual void GetAdditionalChanges(LdapModifyRequest modifyRequest, LdapEntry? existingEntry)
	{
		if (this._allowAltIdentities != null)
		{
			SecurityDescriptor? sd = existingEntry?[LdapAttributeTypes.MsDSAllowedToActOnBehalfOfOtherIdentity]?.Value as SecurityDescriptor;
			var dacl = sd?.Dacl ?? new AccessControlList([], true);
			foreach (var sid in this._allowAltIdentities)
			{
				dacl.Entries.Add(new SimpleAce(AccessControlEntryType.AccessAllowed, AccessControlEntryFlags.None, 0xF01FF, sid));
			}

			SecurityDescriptor newSD = new(SecurityDescriptorControl.None, null, null, dacl, null)
			{
				Owner = sd?.Owner ?? new SecurityIdentifier(SecurityIdentifierAuthority.NtAuthority, [32, 544]),
				Control = SecurityDescriptorControl.DaclPresent | SecurityDescriptorControl.SelfRelative
			};
			modifyRequest.ReplaceValue(LdapAttributeTypes.MsDSAllowedToActOnBehalfOfOtherIdentity.Name, newSD);
		}
	}

	protected sealed override async Task RunAsync(LdapClient ldap, LdapDistinguishedName objName, LdapEntry? existingEntry, CancellationToken cancellationToken)
	{
		LdapModifyRequest request = new LdapModifyRequest(objName);
		if (this.Changes != null)
		{
			ChangeContext ctx = new ChangeContext(this.Services);
			ctx.ProcessArgs(this.Changes, request);
		}

		this.GetAdditionalChanges(request, existingEntry);
		await ldap.Modify(request, cancellationToken);
	}
}

struct ChangeContext
{
	internal ChangeContext(IServiceProvider services)
	{
		this._values = new List<object>();
		this._fileAccess = services.GetService<IFileAccess>();
	}

	internal readonly List<object> _values;
	private readonly IFileAccess? _fileAccess;
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
			AttributeEncoding.File => this._fileAccess.ReadAllBytesFrom(new FileSpec(change.Value, false)),
			AttributeEncoding.Hex => BinaryHelper.ParseHexString(change.Value),
			AttributeEncoding.Base64 => Convert.FromBase64String(change.Value),
			_ => throw new FormatException($"Unsupported encoding {change.Encoding}.")
		};
		_values.Add(value);
	}

	internal readonly void CommitChange(ILdapModifyRequest request)
	{
		request.AddChange(lastAttrName, _values.ToArray(), changeType);
		_values.Clear();
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
