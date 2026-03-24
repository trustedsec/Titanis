using System.ComponentModel;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;

public class GroupInfo
{
	internal GroupInfo(
		SamEntry entry,
		SecurityIdentifier sid,
		string? domain,
		SamGroupGeneralInfo? general
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
	public SamGroupGeneralInfo? General { get; }

	public string AccountName => this.Entry.Name;
	public string Domain { get; }
	public SamEntryType AccountType => this.Entry.EntryType;
	public uint Id => this.Entry.Id;
	public SecurityIdentifier Sid { get; }

	// General
	public SamGroupAttributes? Attributes => this.General?.Attributes;
	public int? MemberCount => this.General?.MemberCount;
	public string? AdminComment => this.General?.AdminComment;
}

/// <task category="SAM;Enumeration">Enumerate groups in the Security Accounts Manager database</task>
[Description("Enumerates groups")]
[OutputRecordType(typeof(GroupInfo))]
[DetailedHelpText(@"{0} attempts to query the general info for the groups returned by the server.")]
[Example("Enumerate all groups", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m!", Tag ="milchickNtlm_enum")]
public sealed class EnumGroupsCommand : SamDomainEnumCommand
{
	protected sealed override SamDomainAccessRights RequiredDomainAccess => SamDomainAccessRights.ListAccounts | SamDomainAccessRights.Read | SamDomainAccessRights.Lookup;
	protected override async Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken)
	{
		List<SamEntry> entries;

		try
		{
			this.WriteDiagnostic($"Enumerating groups in domain '{domainInfo.Name}' ({domainInfo.Id}).");
			entries = await domain.EnumGroups(cancellationToken);
		}
		catch (Exception ex)
		{
			if (this.ContinueOnError.IsSet)
			{
				this.WriteError($"Failed to enumerate groups in domain '{domainInfo.Name}' with error: {ex.Message}");
				return;
			}
			else
				throw;
		}

		foreach (var entry in entries)
		{
			SamGroup group;
			try
			{
				group = await domain.OpenGroupAsync(entry.Id, SamGroupAccessRights.MaxAllowed, cancellationToken);
			}
			catch (Exception ex)
			{
				this.WriteError($"Failed to open alias '{entry.Name}' with error: {ex.Message}");
				continue;
			}

			using (group)
			{
				SamGroupGeneralInfo? general = null;

				try
				{
					general = await group.QueryGeneralInfo(cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to query general info for group '{entry.Name}' with error: {ex.Message}");
				}

				var groupInfo = new GroupInfo(entry, group.Sid, domainInfo.Name, general);

				this.WriteRecord(groupInfo);
			}
		}
	}
}
