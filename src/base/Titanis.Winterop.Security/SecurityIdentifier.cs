using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Winterop.Security
{
	public enum SecurityIdentifierAuthority : long
	{
		Null = 0,
		World = 1,
		Local = 2,
		Creator = 3,
		NonUnique = 4,
		NtAuthority = 5,
		AppPackageAuthority = 0x0F,
		MandatoryLabel = 0x10,
		ScopedPolicyId = 0x11,
		Authentication = 0x12,
	}

	public enum WellKnownSid
	{
		Unknown = -1,
		AccessControlAssistance,
		AllAppPackages,
		Anonymous,
		AccountOperators,
		ProtectedUsers,
		AuthenticatedUsers,
		BuiltinAdministrators,
		BuiltinGuests,
		BackupOperators,
		BuiltinUsers,
		CertificateAdministrators,
		CertsvcDcomAccess,
		CreatorGroup,
		CloneableControllers,
		CreatorOwner,
		CryptoOperators,
		DomainAdministrators,
		DomainComputers,
		DomainControllers,
		DomainGuests,
		DomainUsers,
		EnterpriseAdmins,
		EnterpriseDomainControllers,
		EnterpriseKeyAdmins,
		EventLogReaders,
		RdsEndpointServers,
		HyperVAdmins,
		MandatoryLabelHigh,
		HardwareOperators,
		IisUsers,
		InteractiveUsers,
		DomainKeyAdmins,
		LocalAdministrator,
		LocalGuest,
		LocalService,
		PerformanceLogUsers,
		MandatoryLabelLow,
		MandatoryLabelMedium,
		MandatoryLabelMediumPlus,
		PerfoermanceMonitorUsers,
		NetworkConfigurationOperators,
		NetworkService,
		NetworkUsers,
		OwnerRights,
		GroupPolicyAdmins,
		PrinterOperators,
		PrincipalSelf,
		PowerUsers,
		RdsRemoteAccessServers,
		RestrictedCode,
		RemoteDesktop,
		Replicator,
		RmsServiceOperators,
		EnterpriseReadOnlyDomainControllers,
		RemoteAccessServers,
		PreWindows2000CompatibleAccounts,
		SchemaAdministrators,
		MandatoryLabelSystem,
		ServerOperators,
		ServiceAsserted,
		ServiceUser,
		LocalSystem,
		UserModeDrivers,
		Everyone,
		WriteRestrictedCode,

		// Unmapped
		NullSid,
		LocalUsers,
	}

	// [MS-DTYP] § 2.4.6 SECURITY_DESCRIPTOR
	[TypeConverter(typeof(SecurityIdentifierConverter))]
	public class SecurityIdentifier
	{
		public const int RevisionValue = 1;

		public SecurityIdentifier(ReadOnlySpan<byte> bytes)
			: this(Trim(bytes).ToArray(), 0)
		{ }
		public SecurityIdentifier(SecurityIdentifierAuthority authority, uint[] subauthorities)
			: this(BuildSidFromComponents(authority, subauthorities), 0)
		{ }

		private static byte[] BuildSidFromComponents(SecurityIdentifierAuthority authority, uint[] subauthorities)
		{
			if (subauthorities is null) throw new ArgumentNullException(nameof(subauthorities));
			if (subauthorities.Length > byte.MaxValue)
				throw new ArgumentException("The number of subauthorities specified exceeds the maximum of 255.", nameof(subauthorities));

			int cb = 8 + subauthorities.Length * 4;
			byte[] buf = new byte[cb];
			BinaryPrimitives.WriteInt64BigEndian(buf, (long)authority);
			buf[0] = RevisionValue;
			buf[1] = (byte)subauthorities.Length;

			for (int i = 0; i < subauthorities.Length; i++)
			{
				var subauth = subauthorities[i];
				BinaryPrimitives.WriteUInt32LittleEndian(buf.AsSpan().Slice(8 + 4 * i), subauth);
			}

			return buf;
		}

		internal SecurityIdentifier(byte[] bytes, int dummy)
		{
			// Read this awkward 6-byte integer
			var idAuthValue = 0
				| (long)bytes[2] << 40
				| (long)bytes[3] << 32
				| (long)bytes[4] << 24
				| (long)bytes[5] << 16
				| (long)bytes[6] << 8
				| bytes[7];
			IdentifierAuthority = (SecurityIdentifierAuthority)idAuthValue;

			// TODO: Trim excess bytes

			_bytes = bytes;
		}

		public static int GetSize(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length < 8)
				throw new ArgumentException("The provided buffer is not large enough to contain a valid security identifier.", nameof(bytes));

			var rev = bytes[0];
			if (rev != RevisionValue)
				throw new InvalidDataException("The buffer does not appear to contain a valid security identifier.");

			var authCount = bytes[1];
			int size = 8 + authCount * 4;
			return size;
		}
		private static ReadOnlySpan<byte> Trim(ReadOnlySpan<byte> bytes)
		{
			var size = GetSize(bytes);
			var authCount = bytes[1];
			if (bytes.Length < 8 + authCount * 4)
				throw new InvalidDataException("The buffer appears to be incomplete.");

			return bytes.Slice(0, GetSize(bytes));
		}

		private byte[] _bytes;
		public int BinaryLength => this._bytes.Length;
		public SecurityIdentifierAuthority IdentifierAuthority { get; }

		public int Revision => _bytes[0];
		public int SubauthorityCount => _bytes[1];

		public uint Rid => this.GetSubauthority(this.SubauthorityCount - 1);

		public uint GetSubauthority(int index)
		{
			if ((uint)index >= (uint)SubauthorityCount)
				throw new ArgumentOutOfRangeException(nameof(index), "The index must be non-negative and less than the number of subauthorities.");

			return BinaryPrimitives.ReadUInt32LittleEndian(_bytes.AsSpan().Slice(8 + 4 * index, 4));
		}

		private static readonly Regex SidPattern = new Regex(@"^S-1-((0x(?<ih>[0-9a-fA-F]+))|(?<id>\d+))(-(?<sa>\d+))*");
		public static SecurityIdentifier Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			var m = SidPattern.Match(text);
			if (!m.Success)
				throw new FormatException("The string does not appear to be a valid SID.");

			long idAuth =
				m.Groups["ih"].Success ? long.Parse(m.Groups["ih"].Value, NumberStyles.HexNumber)
				: long.Parse(m.Groups["id"].Value);

			var subauthCaptures = m.Groups["sa"].Captures;

			// TODO: Validate number of subauths

			int cb = 8 + subauthCaptures.Count * 4;
			byte[] buf = new byte[cb];
			BinaryPrimitives.WriteInt64BigEndian(buf, idAuth);
			buf[0] = RevisionValue;
			buf[1] = (byte)subauthCaptures.Count;

			for (int i = 0; i < subauthCaptures.Count; i++)
			{
				var cap = subauthCaptures[i];
				uint subauth = uint.Parse(cap.Value);
				BinaryPrimitives.WriteUInt32LittleEndian(buf.AsSpan().Slice(8 + 4 * i), subauth);
			}

			return new SecurityIdentifier(buf, 0);
		}

		private string? _str;
		public override string ToString()
			=> _str ??= BuildString();

		// [MS-DTYP] § 2.4.2.1 SID String Format Syntax
		private string BuildString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("S-1-");
			if ((long)IdentifierAuthority <= uint.MaxValue)
				sb.Append((long)IdentifierAuthority);
			else
				sb.Append("0x").AppendFormat("{0:X}", (long)IdentifierAuthority);

			int cSubauth = SubauthorityCount;
			for (int i = 0; i < cSubauth; i++)
			{
				var subauth = GetSubauthority(i);

				sb.Append('-').Append(subauth);
			}

			return sb.ToString();
		}

		private static readonly Dictionary<WellKnownSid, string> wellKnownAliases = new Dictionary<WellKnownSid, string>()
		{
			{ WellKnownSid.AccessControlAssistance, "AA" },
			{ WellKnownSid.AllAppPackages, "AC" },
			{ WellKnownSid.Anonymous, "AN" },
			{ WellKnownSid.AccountOperators, "AO" },
			{ WellKnownSid.ProtectedUsers, "AP" },
			{ WellKnownSid.AuthenticatedUsers, "AU" },
			{ WellKnownSid.BuiltinAdministrators, "BA" },
			{ WellKnownSid.BuiltinGuests, "BG" },
			{ WellKnownSid.BackupOperators, "BO" },
			{ WellKnownSid.BuiltinUsers, "BU" },
			{ WellKnownSid.CertificateAdministrators, "CA" },
			{ WellKnownSid.CertsvcDcomAccess, "CD" },
			{ WellKnownSid.CreatorGroup, "CG" },
			{ WellKnownSid.CloneableControllers, "CN" },
			{ WellKnownSid.CreatorOwner, "CO" },
			{ WellKnownSid.CryptoOperators, "CY" },
			{ WellKnownSid.DomainAdministrators, "DA" },
			{ WellKnownSid.DomainComputers, "DC" },
			{ WellKnownSid.DomainControllers, "DD" },
			{ WellKnownSid.DomainGuests, "DG" },
			{ WellKnownSid.DomainUsers, "DU" },
			{ WellKnownSid.EnterpriseAdmins, "EA" },
			{ WellKnownSid.EnterpriseDomainControllers, "ED" },
			{ WellKnownSid.EnterpriseKeyAdmins, "EK" },
			{ WellKnownSid.EventLogReaders, "ER" },
			{ WellKnownSid.RdsEndpointServers, "ES" },
			{ WellKnownSid.HyperVAdmins, "HA" },
			{ WellKnownSid.MandatoryLabelHigh, "HI" },
			{ WellKnownSid.HardwareOperators, "HO" },
			{ WellKnownSid.IisUsers, "IS" },
			{ WellKnownSid.InteractiveUsers, "IU" },
			{ WellKnownSid.DomainKeyAdmins, "KA" },
			{ WellKnownSid.LocalAdministrator, "LA" },
			{ WellKnownSid.LocalGuest, "LG" },
			{ WellKnownSid.LocalService, "LS" },
			{ WellKnownSid.PerformanceLogUsers, "LU" },
			{ WellKnownSid.MandatoryLabelLow, "LW" },
			{ WellKnownSid.MandatoryLabelMedium, "ME" },
			{ WellKnownSid.MandatoryLabelMediumPlus, "MP" },
			{ WellKnownSid.PerfoermanceMonitorUsers, "MU" },
			{ WellKnownSid.NetworkConfigurationOperators, "NO" },
			{ WellKnownSid.NetworkService, "NS" },
			{ WellKnownSid.NetworkUsers, "NU" },
			{ WellKnownSid.OwnerRights, "OW" },
			{ WellKnownSid.GroupPolicyAdmins, "PA" },
			{ WellKnownSid.PrinterOperators, "PO" },
			{ WellKnownSid.PrincipalSelf, "PS" },
			{ WellKnownSid.PowerUsers, "PU" },
			{ WellKnownSid.RdsRemoteAccessServers, "RA" },
			{ WellKnownSid.RestrictedCode, "RC" },
			{ WellKnownSid.RemoteDesktop, "RD" },
			{ WellKnownSid.Replicator, "RE" },
			{ WellKnownSid.RmsServiceOperators, "RM" },
			{ WellKnownSid.EnterpriseReadOnlyDomainControllers, "RO" },
			{ WellKnownSid.RemoteAccessServers, "RS" },
			{ WellKnownSid.PreWindows2000CompatibleAccounts, "RU" },
			{ WellKnownSid.SchemaAdministrators, "SA" },
			{ WellKnownSid.MandatoryLabelSystem, "SI" },
			{ WellKnownSid.ServerOperators, "SO" },
			{ WellKnownSid.ServiceAsserted, "SS" },
			{ WellKnownSid.ServiceUser, "SU" },
			{ WellKnownSid.LocalSystem, "SY" },
			{ WellKnownSid.UserModeDrivers, "UD" },
			{ WellKnownSid.Everyone, "WD" },
			{ WellKnownSid.WriteRestrictedCode, "WR" },
		};

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
			ertsvcDcomAccessGroup = 0x23E,
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

		struct WellKnownSidMapping
		{
			internal WellKnownSidMapping(WellKnownSid wks, string? sddlCode, SecurityIdentifierAuthority authority, int rid)
			{
				this.wks = wks;
				this.sddlCode = sddlCode;
				this.authority = authority;
				this.rid = rid;
			}
			internal WellKnownSid wks;
			internal string? sddlCode;
			internal readonly SecurityIdentifierAuthority authority;
			internal readonly int rid;
		}

		private const SecurityIdentifierAuthority LocalAuthorityPseudo = (SecurityIdentifierAuthority)(-1);
		private const SecurityIdentifierAuthority DomainAuthorityPseudo = (SecurityIdentifierAuthority)(-2);

		private static readonly WellKnownSidMapping[] wksMappings = new WellKnownSidMapping[]
		{
			new WellKnownSidMapping(WellKnownSid.AccessControlAssistance, "AA", LocalAuthorityPseudo, (int)LocalGroupAliasRid.AccessControlAssistanceOperators),
			//new WellKnownSidMapping(WellKnownSid.AllAppPackages, "AC", SecurityIdentifierAuthority.AppPackageAuthority, (int)NtAuthorityRid),
			new WellKnownSidMapping(WellKnownSid.Anonymous, "AN", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.AnonymousLogon),
			new WellKnownSidMapping(WellKnownSid.AccountOperators, "AO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.AccountOperators),
			new WellKnownSidMapping(WellKnownSid.ProtectedUsers, "AP", DomainAuthorityPseudo, (int)DomainRid.ProtectedUsers),
			new WellKnownSidMapping(WellKnownSid.AuthenticatedUsers, "AU", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.AuthenticatedUser),
			new WellKnownSidMapping(WellKnownSid.BuiltinAdministrators, "BA", LocalAuthorityPseudo, (int)LocalGroupAliasRid.Admins),
			new WellKnownSidMapping(WellKnownSid.BuiltinGuests, "BG", LocalAuthorityPseudo, (int)LocalGroupAliasRid.Guests),
			new WellKnownSidMapping(WellKnownSid.BackupOperators, "BO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.BackupOperators),
			new WellKnownSidMapping(WellKnownSid.BuiltinUsers, "BU", LocalAuthorityPseudo, (int)LocalGroupAliasRid.Users),
			new WellKnownSidMapping(WellKnownSid.CertificateAdministrators, "CA", DomainAuthorityPseudo, (int)DomainRid.CertificateAdmins),
			new WellKnownSidMapping(WellKnownSid.CertsvcDcomAccess, "CD", LocalAuthorityPseudo, (int)LocalGroupAliasRid.CertsvcDcomAccessGroup),
			new WellKnownSidMapping(WellKnownSid.CreatorGroup, "CG", SecurityIdentifierAuthority.Creator, (int)CreatorRid.CreatorGroup),
			new WellKnownSidMapping(WellKnownSid.CloneableControllers, "CN", DomainAuthorityPseudo, (int)DomainRid.CloneableControllers),
			new WellKnownSidMapping(WellKnownSid.CreatorOwner, "CO", SecurityIdentifierAuthority.Creator, (int)CreatorRid.CreatorOwner),
			new WellKnownSidMapping(WellKnownSid.CryptoOperators, "CY", LocalAuthorityPseudo, (int)LocalGroupAliasRid.CryptoOperators),
			new WellKnownSidMapping(WellKnownSid.DomainAdministrators, "DA", DomainAuthorityPseudo, (int)DomainRid.AdminsGroup),
			new WellKnownSidMapping(WellKnownSid.DomainComputers, "DC", DomainAuthorityPseudo, (int)DomainRid.ComputersGroup),
			new WellKnownSidMapping(WellKnownSid.DomainControllers, "DD", DomainAuthorityPseudo, (int)DomainRid.DomainControllers),
			new WellKnownSidMapping(WellKnownSid.DomainGuests, "DG", DomainAuthorityPseudo, (int)DomainRid.GuestsGroup),
			new WellKnownSidMapping(WellKnownSid.DomainUsers, "DU", DomainAuthorityPseudo, (int)DomainRid.UsersGroup),
			new WellKnownSidMapping(WellKnownSid.EnterpriseAdmins, "EA", DomainAuthorityPseudo, (int)DomainRid.EnterpriseAdmins),
			new WellKnownSidMapping(WellKnownSid.EnterpriseDomainControllers, "ED", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.EnterpriseControllers),
			new WellKnownSidMapping(WellKnownSid.EnterpriseKeyAdmins, "EK", DomainAuthorityPseudo, (int)DomainRid.EnterpriseKeyAdmins),
			new WellKnownSidMapping(WellKnownSid.EventLogReaders, "ER", LocalAuthorityPseudo, (int)LocalGroupAliasRid.EventLogReadersGroup),
			new WellKnownSidMapping(WellKnownSid.RdsEndpointServers, "ES", LocalAuthorityPseudo, (int)LocalGroupAliasRid.RdsEndpointServers),
			new WellKnownSidMapping(WellKnownSid.HyperVAdmins, "HA", LocalAuthorityPseudo, (int)LocalGroupAliasRid.HyperVAdmins),
			new WellKnownSidMapping(WellKnownSid.MandatoryLabelHigh, "HI", SecurityIdentifierAuthority.MandatoryLabel, (int)MandatoryLaberRid.High),
			new WellKnownSidMapping(WellKnownSid.HardwareOperators, "HO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.UserModeHardwareOperators),
			new WellKnownSidMapping(WellKnownSid.IisUsers, "IS", LocalAuthorityPseudo, (int)LocalGroupAliasRid.InternetUsers),
			new WellKnownSidMapping(WellKnownSid.InteractiveUsers, "IU", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.Interactive),
			new WellKnownSidMapping(WellKnownSid.DomainKeyAdmins, "KA", DomainAuthorityPseudo, (int)DomainRid.KeyAdmins),
			new WellKnownSidMapping(WellKnownSid.LocalAdministrator, "LA", DomainAuthorityPseudo, (int)DomainRid.AdminUser),
			new WellKnownSidMapping(WellKnownSid.LocalGuest, "LG", DomainAuthorityPseudo, (int)DomainRid.GuestUser),
			new WellKnownSidMapping(WellKnownSid.LocalService, "LS", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.LocalService),
			new WellKnownSidMapping(WellKnownSid.PerformanceLogUsers, "LU", LocalAuthorityPseudo, (int)LocalGroupAliasRid.PerformanceLogUsers),
			new WellKnownSidMapping(WellKnownSid.MandatoryLabelLow, "LW", SecurityIdentifierAuthority.MandatoryLabel, (int)MandatoryLaberRid.Low),
			new WellKnownSidMapping(WellKnownSid.MandatoryLabelMedium, "ME", SecurityIdentifierAuthority.MandatoryLabel, (int)MandatoryLaberRid.Medium),
			new WellKnownSidMapping(WellKnownSid.MandatoryLabelMediumPlus, "MP", SecurityIdentifierAuthority.MandatoryLabel, (int)MandatoryLaberRid.MediumPlus),
			new WellKnownSidMapping(WellKnownSid.PerfoermanceMonitorUsers, "MU", LocalAuthorityPseudo, (int)LocalGroupAliasRid.PerformanceMonitoringUsers),
			new WellKnownSidMapping(WellKnownSid.NetworkConfigurationOperators, "NO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.NetworkConfigurationOperators),
			new WellKnownSidMapping(WellKnownSid.NetworkService, "NS", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.NetworkService),
			new WellKnownSidMapping(WellKnownSid.NetworkUsers, "NU", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.Network),
			new WellKnownSidMapping(WellKnownSid.OwnerRights, "OW", SecurityIdentifierAuthority.Creator, (int)CreatorRid.OwnerRights),
			new WellKnownSidMapping(WellKnownSid.GroupPolicyAdmins, "PA", DomainAuthorityPseudo, (int)DomainRid.PolicyAdmins),
			new WellKnownSidMapping(WellKnownSid.PrinterOperators, "PO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.PrintOperators),
			new WellKnownSidMapping(WellKnownSid.PrincipalSelf, "PS", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.PrincipalSelf),
			new WellKnownSidMapping(WellKnownSid.PowerUsers, "PU", LocalAuthorityPseudo, (int)LocalGroupAliasRid.PowerUsers),
			new WellKnownSidMapping(WellKnownSid.RdsRemoteAccessServers, "RA", LocalAuthorityPseudo, (int)LocalGroupAliasRid.RdsRemoteAccessServers),
			new WellKnownSidMapping(WellKnownSid.RestrictedCode, "RC", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.RestrictedCode),
			new WellKnownSidMapping(WellKnownSid.RemoteDesktop, "RD", LocalAuthorityPseudo, (int)LocalGroupAliasRid.RemoteDesktopUsers),
			new WellKnownSidMapping(WellKnownSid.Replicator, "RE", LocalAuthorityPseudo, (int)LocalGroupAliasRid.Replicator),
			new WellKnownSidMapping(WellKnownSid.RmsServiceOperators, "RM", LocalAuthorityPseudo, (int)LocalGroupAliasRid.RemoteManagementUsers),
			new WellKnownSidMapping(WellKnownSid.EnterpriseReadOnlyDomainControllers, "RO", DomainAuthorityPseudo, (int)DomainRid.PolicyAdmins),
			new WellKnownSidMapping(WellKnownSid.RemoteAccessServers, "RS", LocalAuthorityPseudo, (int)LocalGroupAliasRid.RasServers),
			new WellKnownSidMapping(WellKnownSid.PreWindows2000CompatibleAccounts, "RU", LocalAuthorityPseudo, (int)LocalGroupAliasRid.PreWindows2000CompatibleAccess),
			new WellKnownSidMapping(WellKnownSid.SchemaAdministrators, "SA", DomainAuthorityPseudo, (int)DomainRid.SchemaAdmins),
			new WellKnownSidMapping(WellKnownSid.MandatoryLabelSystem, "SI", SecurityIdentifierAuthority.MandatoryLabel, (int)MandatoryLaberRid.System),
			new WellKnownSidMapping(WellKnownSid.ServerOperators, "SO", LocalAuthorityPseudo, (int)LocalGroupAliasRid.ServerOperators),
			new WellKnownSidMapping(WellKnownSid.ServiceAsserted, "SS", SecurityIdentifierAuthority.Authentication, (int)AuthenticationTypeRid.ServiceAsserted),
			new WellKnownSidMapping(WellKnownSid.ServiceUser, "SU", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.Service),
			new WellKnownSidMapping(WellKnownSid.LocalSystem, "SY", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.LocalSystem),
			//new WellKnownSidMapping(WellKnownSid.UserModeDrivers, "UD", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.AuthenticatedUser),
			new WellKnownSidMapping(WellKnownSid.Everyone, "WD", SecurityIdentifierAuthority.World, 0),
			new WellKnownSidMapping(WellKnownSid.WriteRestrictedCode, "WR", SecurityIdentifierAuthority.NtAuthority, (int)NtAuthorityRid.WriteRestrictedCode),
		};

		private int FindWksMapping()
		{
			var auth = this.IdentifierAuthority;

			int iMapping;
			if (this.SubauthorityCount == 1)
			{
				var rid = this.GetSubauthority(0);
				iMapping = Array.FindIndex(wksMappings, r => r.authority == auth && r.rid == rid);
			}
			else if (this.SubauthorityCount == 2 && auth == SecurityIdentifierAuthority.NtAuthority && this.GetSubauthority(0) == 32)
			{
				var rid = this.GetSubauthority(1);
				iMapping = Array.FindIndex(wksMappings, r => r.authority == LocalAuthorityPseudo && r.rid == rid);
			}
			else
			{
				var rid = this.GetSubauthority(0);
				iMapping = Array.FindIndex(wksMappings, r => r.authority == DomainAuthorityPseudo && r.rid == rid);
			}

			return iMapping;
		}

		/// <summary>
		/// Gets the <see cref="WellKnownSid"/> for the this <see cref="SecurityIdentifier"/>.
		/// </summary>
		/// <returns>A <see cref="WellKnownSid"/> value.  If this SID doesn't map to a <see cref="WellKnownSid"/> value, this method returns <see cref="WellKnownSid.Unknown"/>.</returns>
		public WellKnownSid AsWellKnownSid()
		{
			int iMapping = FindWksMapping();

			return iMapping >= 0 ? wksMappings[iMapping].wks : WellKnownSid.Unknown;
		}

		/// <summary>
		/// Gets the SDDL code for the this <see cref="SecurityIdentifier"/>.
		/// </summary>
		/// <returns>A string representing this SID in SDDL, if available; otherwise, <see langword="null"/></returns>
		public string? AsSddlCode()
		{
			int iMapping = FindWksMapping();

			return iMapping >= 0 ? wksMappings[iMapping].sddlCode : null;
		}

		/// <summary>
		/// Gets the string representing this SID within SDDL.
		/// </summary>
		/// <returns>An SDDL code if available; otherwise, the string representation of the SID.</returns>
		public string ToSddlString()
		{
			var str = this.AsSddlCode() ?? this.ToString();
			return str;
		}

		public byte[] GetBytes()
			=> (byte[])this._bytes.Clone();
		public void GetBytes(Span<byte> bytes)
		{
			if (bytes.Length < this._bytes.Length)
				throw new ArgumentException("The buffer is too small to contain the SID.  The buffer must be at least the length indicated by BinaryLength.");

			this._bytes.CopyTo(bytes);
		}

		/// <summary>
		/// Creates a new RID by concatenating a RID to this SID.
		/// </summary>
		/// <param name="relativeId">RID to concacenate</param>
		/// <returns>A <see cref="SecurityIdentifier"/> with <paramref name="relativeId"/> appended.</returns>
		public SecurityIdentifier Concat(uint relativeId)
		{
			var bytes = new byte[this._bytes.Length + 4];
			this.GetBytes(bytes);
			bytes[1]++;
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(bytes.Length - 4, 4), relativeId);
			return new SecurityIdentifier(bytes);
		}
	}

	public class SecurityIdentifierConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				return SecurityIdentifier.Parse(str);
			}
			else
				return base.ConvertFrom(context, culture, value);
		}

		public sealed override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return
				(destinationType == typeof(string))
				|| base.CanConvertTo(context, destinationType);
		}

		public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				var sid = (SecurityIdentifier)value;
				return sid.ToString();

			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
