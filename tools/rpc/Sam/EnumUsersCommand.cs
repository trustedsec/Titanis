using System.ComponentModel;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;

public class UserInfo
{
	internal UserInfo(
		SamEntry entry,
		SecurityIdentifier sid,
		string? domain,
		SamUserGeneralInfo? general,
		SamUserAccountInfo? accountInfo
		)
	{
		Entry = entry;
		Sid = sid;
		Domain = domain;
		General = general;
		AccountInfo = accountInfo;
	}

	[Browsable(false)]
	public SamEntry Entry { get; }
	[Browsable(false)]
	public SamUserGeneralInfo? General { get; }
	[Browsable(false)]
	public SamUserAccountInfo? AccountInfo { get; }

	public string AccountName => this.Entry.Name;
	public string Domain { get; }
	public SamEntryType AccountType => this.Entry.EntryType;
	public uint Id => this.Entry.Id;
	public SecurityIdentifier Sid { get; }

	// General
	public string? FullName => this.General?.FullName;
	public string? AdminComment => this.General?.AdminComment;

	// Account
	public DateTime? PasswordLastSet => this.AccountInfo?.PasswordLastSet;
	public DateTime? LastLogon => this.AccountInfo?.LastLogon;
	public int? BadPasswordCount => this.AccountInfo?.BadPasswordCount;
}

/// <task category="SAM;Enumeration">Enumerate user accounts in the Security Accounts Manager database</task>
[Description("Enumerates user accounts")]
[OutputRecordType(typeof(UserInfo))]
[DetailedHelpText(@"{0} attempts to query the general and account info for the users returned by the server.")]
[Example("Enumerate all accounts", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m!")]
internal sealed class EnumUsersCommand : SamDomainEnumCommand
{
	protected sealed override SamDomainAccessRights RequiredDomainAccess => SamDomainAccessRights.ListAccounts | SamDomainAccessRights.Read | SamDomainAccessRights.Lookup;
	protected override async Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken)
	{
		List<SamEntry> entries;

		// Users
		try
		{
			this.WriteDiagnostic($"Enumerating users in domain '{domainInfo.Name}' ({domainInfo.Id}).");
			entries = await domain.EnumUsers(cancellationToken);
		}
		catch (Exception ex)
		{
			if (this.ContinueOnError.IsSet)
			{
				this.WriteError($"Failed to enumerate users in domain '{domainInfo.Name}' with error: {ex.Message}");
				return;
			}
			else
				throw;
		}

		foreach (var entry in entries)
		{
			SamUser user;
			try
			{
				user = await domain.OpenUserAsync(entry.Id, SamUserAccessRights.MaxAllowed, cancellationToken);
			}
			catch (Exception ex)
			{
				this.WriteError($"Failed to open user '{entry.Name}' with error: {ex.Message}");
				continue;
			}

			using (user)
			{
				SamUserGeneralInfo? general = null;
				SamUserAccountInfo? accountInfo = null;

				try
				{
					general = await user.QueryGeneralInfo(cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to query general info for user '{entry.Name}' with error: {ex.Message}");
				}

				try
				{
					accountInfo = await user.QueryAccountInfo(cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to query account info for user '{entry.Name}' with error: {ex.Message}");
				}

				var userInfo = new UserInfo(entry, user.Sid, domainInfo.Name, general, accountInfo);

				this.WriteRecord(userInfo);
			}
		}
	}
}
