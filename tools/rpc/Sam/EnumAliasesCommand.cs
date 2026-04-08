using System.ComponentModel;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;

public class AliasInfo
{
	internal AliasInfo(
		SamEntry entry,
		SecurityIdentifier sid,
		string? domain,
		SamAliasGeneralInfo? general
		)
	{
		Entry = entry;
		Sid = sid;
		Domain = domain;
		General = general;
	}

	[Browsable(false)]
	public SamEntry Entry { get; }
	[Browsable(false)]
	public SamAliasGeneralInfo? General { get; }

	public string AccountName => this.Entry.Name;
	public string Domain { get; }
	public SamEntryType AccountType => this.Entry.EntryType;
	public uint Id => this.Entry.Id;
	public SecurityIdentifier Sid { get; }

	// General
	public int? MemberCount => this.General?.MemberCount;
	public string? AdminComment => this.General?.AdminComment;
}

/// <task category="SAM;Enumeration">Enumerate aliases in the Security Accounts Manager database</task>
[Description("Enumerates aliases")]
[OutputRecordType(typeof(AliasInfo))]
[DetailedHelpText(@"{0} attempts to query the general info and attributes for the groups returned by the server.")]
[Example("Enumerate all aliases", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m!", Tag ="milchickNtlm_enum")]
public sealed class EnumAliasesCommand : SamDomainEnumCommand
{
	protected sealed override SamDomainAccessRights RequiredDomainAccess => SamDomainAccessRights.ListAccounts | SamDomainAccessRights.Read | SamDomainAccessRights.Lookup;
	protected override async Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken)
	{
		List<SamEntry> entries;

		try
		{
			this.WriteDiagnostic($"Enumerating aliases in domain '{domainInfo.Name}' ({domainInfo.Id}).");
			entries = await domain.EnumAliases(cancellationToken);
		}
		catch (Exception ex)
		{
			if (this.ContinueOnError.IsSet)
			{
				this.WriteError($"Failed to enumerate aliases in domain '{domainInfo.Name}' with error: {ex.Message}");
				return;
			}
			else
				throw;
		}

		foreach (var entry in entries)
		{
			SamAlias alias;
			try
			{
				alias = await domain.OpenAliasAsync(entry.Id, SamAliasAccessRights.MaxAllowed, cancellationToken);
			}
			catch (Exception ex)
			{
				this.WriteError($"Failed to open alias '{entry.Name}' with error: {ex.Message}");
				continue;
			}

			using (alias)
			{
				SamAliasGeneralInfo? general = null;

				try
				{
					general = await alias.QueryGeneralInfo(cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to query general info for group '{entry.Name}' with error: {ex.Message}");
				}

				var groupInfo = new AliasInfo(entry, alias.Sid, domainInfo.Name, general);

				this.WriteRecord(groupInfo);
			}
		}
	}
}
