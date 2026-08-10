using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;

namespace Titanis.Winterop.Security
{
	/// <summary>
	/// Specifies a well-known SID.
	/// </summary>
	public enum WellKnownSid
	{
		Unknown = -1,

		AccessControlAssistanceOps,
		AccountOperators,
		Administrator,
		AliasPrew2Kcompacc,
		AllAppPackages,
		AllowedRodcPasswordReplicationGroup,
		Anonymous,
		AuthenticatedUsers,
		AuthenticationAuthorityAssertedIdentity,
		BackupOperators,
		Batch,
		BuiltinAdministrators,
		BuiltinGuests,
		BuiltinUsers,
		CertPublishers,
		CertificateServiceDcomAccess,
		ClaimsValid,
		CloneableControllers,
		CompoundedAuthentication,
		ConsoleLogon,
		CreatorAuthority,
		CreatorGroup,
		CreatorOwner,
		CryptographicOperators,
		DeniedRodcPasswordReplicationGroup,
		Dialup,
		DigestAuthentication,
		DistributedComUsers,
		DomainAdmins,
		DomainComputers,
		DomainDomainControllers,
		DomainGuests,
		DomainUsers,
		EnterpriseAdmins,
		EnterpriseDomainControllers,
		EnterpriseKeyAdmins,
		EnterpriseReadonlyDomainControllers,
		EventLogReaders,
		Everyone,
		FreshPublicKeyIdentity,
		GroupPolicyCreatorOwners,
		GroupServer,
		Guest,
		HyperVAdmins,
		IisIusrs,
		IncomingForestTrustBuilders,
		Interactive,
		Iusr,
		KeyAdmins,
		KeyPropertyAttestation,
		KeyPropertyMfa,
		KeyTrustIdentity,
		Krbtgt,
		Local,
		LocalAccount,
		LocalAccountAndMemberOfAdministratorsGroup,
		LocalService,
		LocalSystem,
		LogonId,
		MlHigh,
		MlLow,
		MlMedium,
		MlMediumPlus,
		MlProtectedProcess,
		MlSecureProcess,
		MlSystem,
		MlUntrusted,
		Network,
		NetworkConfigurationOps,
		NetworkService,
		WdiServiceHost,
		NtVirtualMachines,
		NtVirtualMachine_Remote,
		NtAuthority,
		NtService,
		NtServicesAll,
		NtService_Dps,
		NtlmAuthentication,
		Null,
		OtherOrganization,
		OwnerRights,
		OwnerServer,
		PerflogUsers,
		PerfmonUsers,
		PowerUsers,
		PrincipalSelf,
		PrinterOperators,
		ProtectedUsers,
		Proxy,
		RasServers,
		RdsEndpointServers,
		RdsManagementServers,
		RdsRemoteAccessServers,
		ReadonlyDomainControllers,
		RemoteDesktop,
		RemoteInteractiveLogon,
		RemoteManagementUsers,
		Replicator,
		RestrictedCode,
		SchannelAuthentication,
		SchemaAdministrators,
		ServerOperators,
		Service,
		ServiceAssertedIdentity,
		StorageReplicaAdmins,
		TerminalServerLicenseServers,
		TerminalServerUser,
		ThisOrganization,
		ThisOrganizationCertificate,
		UserModeDrivers,
		WindowsManagerGroup,
		WindowsAuthorizationAccessGroup,
		WriteRestrictedCode,
		UserModeHardwareOperators,
		OpensshUsers,
	}

	enum WellKnownSidAuthorityMapping
	{
		/// <summary>
		/// No mapping
		/// </summary>
		Invalid = 0,
		/// <summary>
		/// S-1-0-x
		/// </summary>
		Null,
		/// <summary>
		/// S-1-1-x
		/// </summary>
		World,
		/// <summary>
		/// S-1-2-x
		/// </summary>
		Local,
		/// <summary>
		/// S-1-3-x
		/// </summary>
		Creator,
		/// <summary>
		/// S-1-5-x
		/// </summary>
		NtAuthority,
		/// <summary>
		/// S-1-5-5-x
		/// </summary>
		LogonSession,
		/// <summary>
		/// S-1-5-21-0-0-0-x
		/// </summary>
		Claims,
		/// <summary>
		/// S-1-5-21-domain-x
		/// </summary>
		DomainSpecific,
		/// <summary>
		/// S-1-5-32-x
		/// </summary>
		Builtin,
		/// <summary>
		/// S-1-5-64-x
		/// </summary>
		SecurityProviders,
		/// <summary>
		/// S-1-5-65-x
		/// </summary>
		ThisOrganization,
		/// <summary>
		/// S-1-5-80-x
		/// </summary>
		NtService,
		/// <summary>
		/// S-1-5-83-x
		/// </summary>
		NtVirtualMachine,
		/// <summary>
		/// S-1-5-84-0-0-0-0-x
		/// </summary>
		UserModeDriver,
		/// <summary>
		/// S-1-5-90-x
		/// </summary>
		WindowManager,
		/// <summary>
		/// S-1-15-2-x
		/// </summary>
		AppContainer,
		/// <summary>
		/// S-1-15-3-x
		/// </summary>
		Capabilities,
		/// <summary>
		/// S-1-16-x
		/// </summary>
		MandatoryLabel,
		/// <summary>
		/// S-1-18-x
		/// </summary>
		AuthenticationAuthority,
	}

	/// <summary>
	/// Maps a <see cref="WellKnownSid"/> to a <see cref="SecurityIdentifier"/> and its SDDL code.
	/// </summary>
	readonly struct WellKnownSidMapping
	{
		internal WellKnownSidMapping(WellKnownSidAuthorityMapping authority, uint rid, WksSddlKey sddlCode = default)
		{
			this.authority = authority;
			this.rid = rid;
			this.sddlCode = sddlCode;
		}

		public bool IsValid => this.authority > 0;

		internal readonly WellKnownSid wks;
		internal readonly WellKnownSidAuthorityMapping authority;
		internal readonly uint rid;
		internal readonly WksSddlKey sddlCode;

		internal readonly SecurityIdentifier BuildSid(SecurityIdentifier? domainSid)
		{
			var authoritySid = MapAuthority(this.authority, domainSid);
			return authoritySid.Concat(this.rid);
		}

		private static SecurityIdentifier MapAuthority(WellKnownSidAuthorityMapping authority, SecurityIdentifier? domainSid)
		{
			if (authority == WellKnownSidAuthorityMapping.DomainSpecific)
			{
				if (domainSid is null)
					throw new ArgumentException($"The well-known-SID mapping refers to a domain-specific SID, but no domain SID was provided.", nameof(domainSid));
				return domainSid;
			}
			else
			{
				var authoritySid = ((uint)authority >= authoritySids.Length) ? null : authoritySids[(int)authority];
				if (authoritySid is null)
					throw new NotSupportedException($"The well-known SID authority {authority} cannot be mapped.");

				return authoritySid;
			}
		}

		public static WellKnownSid FindSimpleMapping(WellKnownSidAuthorityMapping authority, uint rid)
		{
			for (int i = 0; i < wksMappings.Length; i++)
			{
				WellKnownSidMapping mapping = wksMappings[i];
				if (mapping.authority == authority && mapping.rid == rid)
					return (WellKnownSid)i;
			}

			return WellKnownSid.Unknown;
		}

		#region Authority SIDs
		private static SecurityIdentifier[] authoritySids = new SecurityIdentifier[]
		{
			null,
			new SecurityIdentifier( SecurityIdentifierAuthority.Null),
			new SecurityIdentifier( SecurityIdentifierAuthority.World),
			new SecurityIdentifier( SecurityIdentifierAuthority.Local),
			new SecurityIdentifier( SecurityIdentifierAuthority.Creator),
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority),
			// LogonSession
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 5),
			// Claims
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, [21, 0, 0, 0]),
			// DomainSpecific
			null,
			// Builtin
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 32),
			// SecurityProviders
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 64),
			// ThisOrganization
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 65),
			// NtService
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 80),
			// UMDF
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, [84, 0, 0, 0, 0]),
			// WindowManager
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 90),
			// NtVirtualMachine
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 83),
			// AppContainer
			new SecurityIdentifier( SecurityIdentifierAuthority.AppPackageAuthority, 2),
			// Capabilities
			new SecurityIdentifier( SecurityIdentifierAuthority.NtAuthority, 3),
			new SecurityIdentifier( SecurityIdentifierAuthority.MandatoryLabel),
			new SecurityIdentifier( SecurityIdentifierAuthority.Authentication),
		};
		#endregion

		#region WKS mappings
		/// <summary>
		/// List of <see cref="WellKnownSidMapping"/>
		/// </summary>
		/// <remarks>
		/// This array is ordered to align with the values in <see cref="WellKnownSid"/>
		/// </remarks>
		private static readonly WellKnownSidMapping[] wksMappings = new WellKnownSidMapping[]
		{
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 579, new WksSddlKey('A', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 548, new WksSddlKey('A', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 500, new WksSddlKey('L', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 554, new WksSddlKey('R', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AppContainer, 1, new WksSddlKey('A', 'C')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 571),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 7, new WksSddlKey('A', 'N')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 11, new WksSddlKey('A', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 1),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 551, new WksSddlKey('B', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 3),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 544, new WksSddlKey('B', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 546, new WksSddlKey('B', 'G')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 545, new WksSddlKey('B', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 517, new WksSddlKey('C', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 574, new WksSddlKey('C', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Claims, 497),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 522, new WksSddlKey('C', 'N')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Claims, 496),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Local, 1),
			default,
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Creator, 1, new WksSddlKey('C', 'G')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Creator, 0, new WksSddlKey('C', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 569, new WksSddlKey('C', 'Y')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 572),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 1),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.SecurityProviders, 21),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 562),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 512, new WksSddlKey('D', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 515, new WksSddlKey('D', 'C')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 516, new WksSddlKey('D', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 514, new WksSddlKey('D', 'G')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 513, new WksSddlKey('D', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 519, new WksSddlKey('E', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 9, new WksSddlKey('E', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 527, new WksSddlKey('E', 'K')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 498, new WksSddlKey('R', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 573, new WksSddlKey('E', 'R')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.World, 0, new WksSddlKey('W', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 3),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 520, new WksSddlKey('P','A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Creator, 3),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 501, new WksSddlKey('L', 'G')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 578, new WksSddlKey('H', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 568, new WksSddlKey('I', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 557),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 4, new WksSddlKey('I', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 17),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 526, new WksSddlKey('K', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 6),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 5),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 4),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 502),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Local, 0),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 113),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 114),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 19, new WksSddlKey('L', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 18, new WksSddlKey('S', 'Y')),
			default, //new WellKnownSidMapping(WellKnownSidAuthorityMapping.LogonSession, x_y),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 12288, new WksSddlKey('H', 'I')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 4096, new WksSddlKey('L', 'W')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 8192, new WksSddlKey('M', 'E')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 8448, new WksSddlKey('M', 'P')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 20480),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 28672),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 16384, new WksSddlKey('S', 'I')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.MandatoryLabel, 0),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 2, new WksSddlKey('N', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 556, new WksSddlKey('N', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 20, new WksSddlKey('N', 'S')),
			default, //new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtService, <WdiServiceHost>),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtVirtualMachine, 0),
			default,
			default,
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 80),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtService, 0),
			default, //new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtService, <DPS>),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.SecurityProviders, 10),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Null, 0),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 1000),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Creator, 4, new WksSddlKey('O', 'W')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Creator, 2),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 559, new WksSddlKey('L', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 558, new WksSddlKey('M', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 547, new WksSddlKey('P', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 10, new WksSddlKey('P', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 550, new WksSddlKey('P', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 525, new WksSddlKey('A', 'P')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 8),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 553, new WksSddlKey('R', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 576, new WksSddlKey('E', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 577),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 575, new WksSddlKey('R', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 521),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 555, new WksSddlKey('R', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 14),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 580),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 552, new WksSddlKey('R', 'E')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 12, new WksSddlKey('R', 'C')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.SecurityProviders, 14),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.DomainSpecific, 518, new WksSddlKey('S', 'A')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 549, new WksSddlKey('S', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 6, new WksSddlKey('S', 'U')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.AuthenticationAuthority, 2, new WksSddlKey('S', 'S')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 582),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 561),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 13),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 15),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.ThisOrganization, 1),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.UserModeDriver, 0, new WksSddlKey('U', 'D')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.WindowManager, 0),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 560),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.NtAuthority, 33, new WksSddlKey('W', 'R')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 584, new WksSddlKey('H', 'O')),
			new WellKnownSidMapping(WellKnownSidAuthorityMapping.Builtin, 585, new WksSddlKey('S', 'H')),
		};

		internal struct WksSddlKey : IEquatable<WksSddlKey>
		{
			public WksSddlKey(char c1, char c2)
			{
				ushort value = char.ToUpper(c1);
				value <<= 8;
				value |= char.ToUpper(c2);
				this.value = value;
			}

			private ushort value;
			public bool IsValid => this.value != 0;

			public override string ToString()
			{
				return Compat.CreateString(2, this.value, (b, s) =>
				{
					b[0] = (char)(s >> 8);
					b[1] = (char)(byte)s;
				});
			}

			public override bool Equals(object? obj)
			{
				return obj is WksSddlKey key && Equals(key);
			}

			public bool Equals(WksSddlKey other)
			{
				return value == other.value;
			}

			public override int GetHashCode()
			{
				return System.HashCode.Combine(value);
			}

			public static bool operator ==(WksSddlKey left, WksSddlKey right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(WksSddlKey left, WksSddlKey right)
			{
				return !(left == right);
			}
		}

		private static Dictionary<WksSddlKey, WellKnownSid> wksXref = BuildWksMappingXref();

		private static Dictionary<WksSddlKey, WellKnownSid> BuildWksMappingXref()
		{
			Dictionary<WksSddlKey, WellKnownSid> xref = new Dictionary<WksSddlKey, WellKnownSid>(wksMappings.Length);

			for (int i = 0; i < wksMappings.Length; i++)
			{
				WellKnownSidMapping mapping = wksMappings[i];

				if (mapping.sddlCode.IsValid)
				{
					var wks = (WellKnownSid)i;
					xref.Add(mapping.sddlCode, wks);
				}
			}

			return xref;
		}
		#endregion

		internal static WellKnownSidMapping TryFindWksMappingFromSddlCode(char c1, char c2)
		{
			var code = new WksSddlKey(c1, c2);
			if (!wksXref.TryGetValue(code, out var wks))
				return default;

			if ((int)wks < 0)
				return default;

			return wksMappings[(int)wks];
		}
		internal static WellKnownSidMapping FromWks(WellKnownSid wks)
			=> wksMappings[(int)wks];
		internal static string? TryMapWksToCode(WellKnownSid wks)
		{
			if (wks == WellKnownSid.Unknown)
				return null;

			var map = FromWks(wks);
			if (map.sddlCode.IsValid)
			{
				return map.sddlCode.ToString();
			}
			else
				return null;
		}
	}



	enum CreatorRid
	{
		CreatorOwner = 0,
		CreatorGroup = 1,
		OwnerRights = 3,
	}

	enum NtAuthorityRid
	{
		Dialup = 1,
		Network = 2,
		Batch = 3,
		Interactive = 4,
		LogonIds = 5,
		Service = 6,
		AnonymousLogon = 7,
		Proxy = 8,
		EnterpriseControllers = 9,
		PrincipalSelf = 10,
		AuthenticatedUser = 11,
		RestrictedCode = 12,
		TerminalServer = 13,
		LocalSystem = 18,
		LocalService = 19,
		NetworkService = 20,
		NonUnique = 21,
		BuiltinDomain = 32,
		WriteRestrictedCode = 33,
		RestrictedServicesBase = 99,
	}

	enum DomainRid
	{
		CertsvcDcomAccessGroup = 0x23E,
		AdminUser = 0x1F4,
		GuestUser = 0x1F5,
		AdminsGroup = 0x200,
		UsersGroup = 0x201,
		GuestsGroup = 0x202,
		ComputersGroup = 0x203,
		DomainControllers = 0x204,
		CertificateAdmins = 0x205,
		EnterpriseReadOnlyDomainControllers = 0x1F2,
		SchemaAdmins = 0x206,
		EnterpriseAdmins = 0x207,
		PolicyAdmins = 0x208,
		ReadOnlyDomainControllers = 0x209,
		CloneableControllers = 0x20A,
		CdcGroup = 0x20C,
		ProtectedUsers = 0x20D,
		KeyAdmins = 0x20E,
		EnterpriseKeyAdmins = 0x20F,
	}

	enum MandatoryLaberRid
	{
		Untrusted = 0,
		Low = 0x1000,
		Medium = 0x2000,
		MediumPlus = 0x2100,
		High = 0x3000,
		System = 0x4000,
		ProtectedProcess = 0x5000,
	}

	enum LocalGroupAliasRid
	{
		Admins = 544,
		Users = 545,
		Guests = 546,
		PowerUsers = 547,
		AccountOperators = 548,
		ServerOperators = 549, PrintOperators = 550,
		BackupOperators = 551,
		Replicator = 552,
		RasServers = 553,
		PreWindows2000CompatibleAccess = 554,
		RemoteDesktopUsers = 555,
		NetworkConfigurationOperators = 556,
		IncomingForestTrustBuilders = 557,
		PerformanceMonitoringUsers = 558,
		PerformanceLogUsers = 559,
		AuthorizationAccess = 560,
		TerminalServicesLicenseServers = 561,
		DcomUsers = 562,
		InternetUsers = 568,
		CryptoOperators = 569,
		CacheablePrincipalsGroup = 571,
		NonCacheablePrincipalsGroup = 572,
		EventLogReadersGroup = 573,
		CertsvcDcomAccessGroup = 574,
		RdsRemoteAccessServers = 575,
		RdsEndpointServers = 576,
		RdsManagementServers = 577,
		HyperVAdmins = 578,
		AccessControlAssistanceOperators = 579,
		RemoteManagementUsers = 580,
		DefaultAccount = 581,
		StorageReplicaAdmins = 582,
		DeviceOwners = 583,
		UserModeHardwareOperators = 584,
	}

	enum AuthenticationTypeRid
	{
		ServiceAsserted = 2,
	}

}
