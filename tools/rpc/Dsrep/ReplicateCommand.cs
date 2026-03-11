using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Winterop.Security;

namespace Dsrep;

[Command]
[Description("Requests replica changes")]
[OutputRecordType(typeof(LdapEntry), DefaultOutputStyle = OutputStyle.List, DefaultFields = [
	nameof(LdapAttributeTypes.SAMAccountName),
	nameof(LdapAttributeTypes.GivenName),
	nameof(LdapAttributeTypes.UserPrincipalName),
	nameof(LdapAttributeTypes.InstanceType),
	nameof(LdapAttributeTypes.UserAccountControl),
	nameof(LdapAttributeTypes.DBCSPwd),
	nameof(LdapAttributeTypes.UnicodePwd),
	nameof(LdapAttributeTypes.LmPwdHistory),
	nameof(LdapAttributeTypes.NtPwdHistory),
	nameof(LdapAttributeTypes.SupplementalCredentials),
	])]
internal class ReplicateCommand : RpcCommand<DirectoryReplicationClient>
{
	[Parameter(After = nameof(RpcCommand.ServerName))]
	[Mandatory]
	public DsName[] ObjectName { get; set; }

	protected override Type InterfaceType => typeof(drsuapi);

	private static string[] GetLdapAttributes(string[] fieldNames)
	{
		List<string> attrs = new(fieldNames.Length);
		HashSet<string> fieldsAdded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		foreach (var name in fieldNames)
		{
			if (fieldsAdded.Add(name))
			{
				var attr = LdapAttributeTypes.TryGetByNameOrOid(name);
				attrs.Add(attr.Oid);
			}
		}

		return attrs.ToArray();
	}

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, CancellationToken cancellationToken)
	{
		var dcInfos = await client.GetDcInfo(this.RpcParameters.Authentication.UserDomain, cancellationToken);

		var outputAttrs = GetLdapAttributes(this.OutputFields);

		foreach (var objName in this.ObjectName)
		{
			var obj = await client.GetNcChanges(dcInfos[0], objName, outputAttrs, 1, cancellationToken);

			LdapAttribute[] returnedAttrs = new LdapAttribute[obj.Attributes.Length];
			for (int i = 0; i < obj.Attributes.Length; i++)
			{
				DsAttribute? attr = obj.Attributes[i];
				var attrType = LdapAttributeTypes.TryGetByNameOrOid(attr.Oid);

				var name = attrType?.Name;
				object[] values;
				if (attrType != null && attrType.Syntax != null)
				{
					values = Array.ConvertAll(attr.Values, r => attrType.Syntax.DecodeDsrep(r.Bytes));
				}
				else
				{
					values = Array.ConvertAll(attr.Values, r => r.Bytes);
				}

				returnedAttrs[i] = new LdapAttribute(attrType, values);
			}
			LdapEntry entry = new LdapEntry(new LdapDistinguishedName(obj.Name.Name), returnedAttrs);

			this.WriteRecord(entry);
		}

		return 0;
	}
}
