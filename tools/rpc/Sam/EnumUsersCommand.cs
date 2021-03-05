using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mssamr;
using Titanis.Smb2.Cli;

namespace Titanis.Cli.SamTool;

public class UserInfo
{
	internal UserInfo(
		SamEntry entry,
		SamUserGeneralInfo? general,
		SamUserAccountInfo? accountInfo
		)
	{
		Entry = entry;
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
	public uint Id => this.Entry.Id;

	// General
	public string? FullName => this.General?.FullName;
	public string? AdminComment => this.General?.AdminComment;

	// Account
	public DateTime? PasswordLastSet => this.AccountInfo?.PasswordLastSet;
	public DateTime? LastLogon => this.AccountInfo?.LastLogon;
	public int? BadPasswordCount => this.AccountInfo?.BadPasswordCount;
}

/// <task category="SAM;Enumeration">Enumerate the accounts in the Security Accounts Manager database</task>
[Description("Enumerates the users")]
[OutputRecordType(typeof(UserInfo))]
[DetailedHelpText(@"{0} attempts to query the general and account info for the users returned by the server.")]
[Example("Enumerate all accounts", "{0} LUMON-DC1 -UserName milchick -Password Br3@kr00m!")]
internal sealed class EnumUsersCommand : SamCommand
{
	protected sealed override SamServerAccess RequiredAccess => SamServerAccess.EnumerateDomains | SamServerAccess.LookupDomain;

	protected override async Task<int> RunAsync(Sam sam, CancellationToken cancellationToken)
	{
		var domains = await sam.GetDomains(cancellationToken);
		foreach (var domainInfo in domains)
		{
			try
			{
				var domain = await sam.OpenDomainAsync(domainInfo.Name, SamDomainAccess.ListAccounts | SamDomainAccess.Read | SamDomainAccess.Lookup, cancellationToken);
				var users = await domain.EnumUsers(cancellationToken);
				foreach (var entry in users)
				{
					try
					{
						using (var user = await domain.OpenUserAsync(entry.Id, SamUserAccess.MaxAllowed, cancellationToken))
						{
							SamUserGeneralInfo? general = null;
							SamUserAccountInfo? accountInfo = null;

							try
							{
								general = await user.QueryGeneralInfo(cancellationToken);
							}
							catch { }
							try
							{
								accountInfo = await user.QueryAccountInfo(cancellationToken);
							}
							catch { }

							var userInfo = new UserInfo(entry, general, accountInfo);

							this.WriteRecord(userInfo);
						}
					}
					catch (Exception ex)
					{
						this.WriteError($"Failed to open user '{entry.Name}' with error: {ex.Message}");
					}
				}
			}
			catch (Exception ex)
			{
				this.WriteError($"Failed to open domain '{domainInfo.Name}' with error: {ex.Message}");
			}
		}

		return 0;
	}
}
