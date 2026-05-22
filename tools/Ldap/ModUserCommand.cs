using System.ComponentModel;
using System.Text;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP;Expanding Access">Modify a user account in Active Directory</task>
[Command]
[Description("Modifies a directory entry")]
[DetailedHelpText(@"Specify attribute changes as a series of name?=value pairs where ?= is:

  +=   Add a value
  -=   Remove a value
  =    Replace all values

For example:

	servicePrincipleName+=HOST/ALLENTOWN   # Adds the SPN
	servicePrincipleName-=HOST/ALLENTOWN   # Removes the SPN
	servicePrincipleName=HOST/ALLENTOWN   # Replaces all SPNs

To add or remove multiple values, specify each value as a separate name?=value pair:

	# Adds 3 SPNs
	servicePrincipleName+=HOST/ALLENTOWN servicePrincipleName+=cifs/ALLENTOWN servicePrincipleName+=RestrictedKrbHost/ALLENTOWN

By default, the attribute values are parsed according to their syntax.  For numeric attributes with bitflags, you may use the named bits, separating multiple bit names with a comma.  For example, to set the encryption types for an account:

	msDS-SupportedEncryptionTypes=Aes128CtsHmacSha1_96,Aes256CtsHmacSha1_96

Use the `namedbits` command to view a list of supported attributes with bitflags.



You may specify multiple operations for the same attribute within a single command line.  Each operation is sent to the LDAP server as part of the modification request, in the order specified on the command line.  Note that consecutive changes to the same attribute with the same operation are combined.  IN the above example, all 3 SPNs are added in a single operation.


")]
internal class ModUserCommand : ModCommand
{
	[Parameter]
	[Description("Old password (for password change)")]
	public string? OldPassword { get; set; }

	[Parameter]
	[Description("New password (for password change or reset)")]
	public string? NewPassword { get; set; }

	protected override void GetAdditionalChanges(LdapModifyRequest modifyRequest, LdapEntry? existingEntry)
	{
		base.GetAdditionalChanges(modifyRequest, existingEntry);

		if (this.NewPassword != null)
		{
			if (this.OldPassword != null)
			{
				modifyRequest.DeleteValue("unicodePwd", EncodePassword(this.OldPassword));
				modifyRequest.AddValue("unicodePwd", EncodePassword(this.NewPassword));
			}
			else
			{
				modifyRequest.ReplaceValue("unicodePwd", EncodePassword((this.NewPassword)));
			}
		}
	}

	private static BinaryString EncodePassword(string password)
	{
		return new BinaryString(Encoding.Unicode.GetBytes($"\"{password}\""));
	}
}
