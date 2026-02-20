using System.ComponentModel;
using Titanis.Cli;
using Titanis.Winterop.Security;

[Description("Describes a security descriptor represented in SDDL")]
class DescribeCommand : Command
{
	[Parameter(0)]
	[Description("Security descriptor in SDDL notation")]
	public SecurityDescriptor Sddl { get; set; }

	[Parameter]
	[Description("Type of object")]
	public SecurityObjectType? ObjectType { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var sd = this.Sddl;
		if (sd.Owner != null)
			this.WriteMessage($"Owner SID: {sd.Owner} - WKS: {sd.Owner.AsWellKnownSid()}");

		var objType = this.ObjectType ?? SecurityObjectType.File;
		var model = ObjectSecurityModel.GetModelFor(objType);
		model ??= ObjectSecurityModel.File;

		if (sd.Dacl != null)
		{
			this.WriteMessage("Discretionary access control list");
			for (int i = 0; i < sd.Dacl.Entries.Count; i++)
			{
				AccessControlEntry? ace = sd.Dacl.Entries[i];
				this.WriteMessage($"  Entry #{i + 1}:");
				this.WriteMessage($"    Type: {ace.AceType}");
				this.WriteMessage($"    Options: {ace.AceFlags}");

				var wks = ace.Trustee.AsWellKnownSid();
				if (wks == WellKnownSid.Unknown)
					this.WriteMessage($"    Trustee: {ace.Trustee}");
				else
					this.WriteMessage($"    Trustee: {ace.Trustee} ({wks})");

				switch (ace.AceType)
				{
					case AccessControlEntryType.AccessAllowed:
					case AccessControlEntryType.AccessDenied:
					case AccessControlEntryType.Audit:
					case AccessControlEntryType.Alarm:
						{
							var simple = (SimpleAce)ace;
							this.WriteMessage($"    Access mask: 0x{ace.AccessMask:X8}");
							var rights = model.GetAccessRights(ace.AccessMask, true);
							var prmString = string.Join(", ", rights.Select(r => r.Description));
							this.WriteMessage($"    Permissions: {prmString}");
						}
						break;

					case AccessControlEntryType.InvalidType:
					case AccessControlEntryType.AccessAllowedCompound:
					case AccessControlEntryType.AccessAllowedObject:
					case AccessControlEntryType.AccessDeniedObject:
					case AccessControlEntryType.SystemAuditObject:
					case AccessControlEntryType.SystemAlarmObject:
					case AccessControlEntryType.AccessAllowedCallback:
					case AccessControlEntryType.AccessDeniedCallback:
					case AccessControlEntryType.AccessAllowedCallbackObject:
					case AccessControlEntryType.AccessDeniedCallbackObject:
					case AccessControlEntryType.SystemAuditCallback:
					case AccessControlEntryType.SystemAlarmCallback:
					case AccessControlEntryType.SystemAuditCallbackObject:
					case AccessControlEntryType.SystemAlarmCallbackObject:
					case AccessControlEntryType.MandatoryLabel:
					case AccessControlEntryType.ResourceAttribute:
					case AccessControlEntryType.ScopedPolicyId:
					default:
						break;
				}
				this.WriteMessage($"    Access mask: {ace.AccessMask}");
			}
		}


		throw new NotImplementedException();
	}
}
