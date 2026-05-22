using System.ComponentModel;
using System.Reflection;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SddlTool;

/// <task category="Security;SDDL">Describe a security descriptor (SDDL or hex) (offline)</task>
[Command]
[Description("Describes a security descriptor")]
[DetailedHelpText(@"This command accepts one or more security descriptors.  Each security descriptor may be specified either in the SDDL form, or in the binary form as a series of hex digits.  The -ObjectType specifies how the bits are translated to specific permissions.  If no object type is specified, it is assumed to be for a file.

Specifying -PrintHex or -PrintSddl effectively allows you to convert between the SDDL and binary form of a security descriptor.
")]
[Example("Describe a security descriptor of a registry key", "{0} O:BAG:SYD:PAI(A;CI;KA;;;BA)(A;CI;KR;;;AU)(A;CI;KA;;;LS)(A;CI;KA;;;NS)(A;CI;KR;;;IU)(A;CI;KA;;;SY) -ObjectType RegistryKey")]
[Example("Describe a binary security descriptor on a file", "{0} 010004805800000068000000000000001400000002004400030000000000140003000000010100000000000504000000000014000700000001010000000000050a00000000001400030000000101000000000005120000000102000000000005200000002002000001020000000000052000000020020000")]
class DescribeCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("Security descriptor in hex or SDDL notation")]
	public SecurityDescriptor[] SddlOrHex { get; set; }

	[Parameter]
	[Description("Type of object")]
	public SecurityObjectType? ObjectType { get; set; }

	[Parameter]
	[Description("Prints the binary form as a string of hex digits")]
	public SwitchParam PrintHex { get; set; }

	[Parameter]
	[Description("Prints the SDDL form")]
	public SwitchParam PrintSddl { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var sddl in this.SddlOrHex)
		{
			var sd = sddl;

			if (this.PrintSddl.IsSet)
				this.WriteRecord($"SDDL: {sd.ToSddlString(SecurityDescriptorSections.All)}");
			if (this.PrintHex.IsSet)
				this.WriteRecord($"Hex: {sd.ToByteArray().ToHexString()}");

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
