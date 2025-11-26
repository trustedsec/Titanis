using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;

namespace Ldap;

[Command]
[Description("Modifies a directory entry")]
[DetailedHelpText(@"Specify the changes as a series of name?=value pairs where ?= is:

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

	protected override void GetChanges(LdapModifyRequest modifyRequest)
	{
		base.GetChanges(modifyRequest);

		if (this.NewPassword != null)
		{
			if (this.OldPassword != null)
			{
				modifyRequest.DeleteValue("unicodePwd", EncodePassword(this.OldPassword));
				//modifyRequest.AddValue("unicodePwd", EncodePassword(this.NewPassword));
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
