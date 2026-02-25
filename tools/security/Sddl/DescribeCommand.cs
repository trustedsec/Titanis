using System.ComponentModel;
using System.Reflection;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Winterop.Security;

[Description("Describes a security descriptor represented in SDDL")]
class DescribeCommand : Command
{
	[Parameter(0)]
	[Description("Security descriptor in SDDL notation")]
	public SecurityDescriptor[] Sddl { get; set; }

	[Parameter]
	[Description("Type of object")]
	public SecurityObjectType? ObjectType { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var sddl in this.Sddl)
		{
			var sd = sddl;

			var objType = this.ObjectType ?? SecurityObjectType.File;
			var model = ObjectSecurityModel.GetModelFor(objType);
			model ??= ObjectSecurityModel.File;

			if (sd.Owner != null)
			{
				this.WriteRecord($"Owner: {sd.Owner} ({sd.Owner.AsWellKnownSid()})");
			}
			if (sd.Group != null)
			{
				this.WriteRecord($"Group: {sd.Group} ({sd.Group.AsWellKnownSid()})");
			}
			if (sd.Dacl != null)
			{
				this.WriteRecord("Discretionary access control list");
				this.PrintAcl(sd.Dacl, model);
			}
			if (sd.Sacl != null)
			{
				this.WriteRecord("System access control list");
				this.PrintAcl(sd.Sacl, model);
			}
		}

		return Task.FromResult(0);
	}

	private void PrintAcl(AccessControlList acl, ObjectSecurityModel model)
	{
		for (int i = 0; i < acl.Entries.Count; i++)
		{
			AccessControlEntry? ace = acl.Entries[i];
			this.WriteRecord($"  Entry #{i + 1}:");
			this.WriteRecord($"    Type: {ace.AceType} (0x{(uint)ace.AceType:X8})");
			this.WriteRecord($"    Options: {ace.AceFlags} (0x{(uint)ace.AceFlags:X8})");

			var wks = ace.Trustee.AsWellKnownSid();
			if (wks == WellKnownSid.Unknown)
				this.WriteRecord($"    Trustee: {ace.Trustee}");
			else
				this.WriteRecord($"    Trustee: {ace.Trustee} ({wks})");

			this.WriteRecord($"    Access mask: 0x{ace.AccessMask:X8}");
			switch (ace)
			{
				case SimpleAce simple:
					{
						var rights = model.GetAccessRights(ace.AccessMask, true);
						this.WriteRecord($"    Permissions:");
						foreach (var right in rights)
						{
							this.WriteRecord($"      {right.Description}");
						}
					}
					break;

				case ObjectAce objace:
					{
						this.WriteRecord($"    Permissions:");
						if (objace.ObjectType.HasValue && (DirectoryObjectAccessRights)objace.AccessMask is DirectoryObjectAccessRights.ControlAccess)
						{
							string descr;
							if (AdExtendedRights.TryGetExtendedRight(objace.ObjectType.Value, out var ext))
								descr = ext.Description;
							else
								descr = objace.ObjectType.ToString();

							this.WriteRecord($"      {descr}");
						}
						else
						{
							var mask = (DirectoryObjectAccessRights)objace.AccessMask;
							if (objace.ObjectType.HasValue && 0 != (mask & (DirectoryObjectAccessRights.ReadProperty | DirectoryObjectAccessRights.WriteProperty)))
							{
								string descr;
								if (AdPropertySets.TryGetPropertySet(objace.ObjectType.Value, out var propset))
									descr = $"{propset.Description} properties ({objace.ObjectType})";
								else if (AdProperties.TryGetProperty(objace.ObjectType.Value, out var prop))
									descr = $"{prop.Name} ({objace.ObjectType.Value})";
								else
									descr = objace.ObjectType.ToString();

								if (0 != (mask & DirectoryObjectAccessRights.ReadProperty))
									this.WriteRecord($"      Read {descr}");
								if (0 != (mask & DirectoryObjectAccessRights.WriteProperty))
									this.WriteRecord($"      Write {descr}");

								mask &= ~(DirectoryObjectAccessRights.ReadProperty | DirectoryObjectAccessRights.WriteProperty);
							}

							if (mask != 0)
							{
								var rights = model.GetAccessRights(ace.AccessMask, true);
								foreach (var right in rights)
								{
									this.WriteRecord($"      {right.Description}");
								}
							}
						}
					}
					break;

				default:
					break;
			}
		}
	}
}
