using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Winterop.Test;

[TestClass]
public class SecurityTests
{

	private static readonly SecurityIdentifier DomainSid = new SecurityIdentifier(SecurityIdentifierAuthority.NtAuthority, [21, 1, 2, 3]);

	[TestMethod]
	[DataRow("AA", WellKnownSid.AccessControlAssistanceOps)]
	[DataRow("AC", WellKnownSid.AllAppPackages)]
	[DataRow("AN", WellKnownSid.Anonymous)]
	[DataRow("AO", WellKnownSid.AccountOperators)]
	[DataRow("AP", WellKnownSid.ProtectedUsers)]
	[DataRow("AU", WellKnownSid.AuthenticatedUsers)]
	[DataRow("BA", WellKnownSid.BuiltinAdministrators)]
	[DataRow("BG", WellKnownSid.BuiltinGuests)]
	[DataRow("BO", WellKnownSid.BackupOperators)]
	[DataRow("BU", WellKnownSid.BuiltinUsers)]
	[DataRow("CA", WellKnownSid.CertPublishers)]
	[DataRow("CD", WellKnownSid.CertificateServiceDcomAccess)]
	[DataRow("CG", WellKnownSid.CreatorGroup)]
	[DataRow("CN", WellKnownSid.CloneableControllers)]
	[DataRow("CO", WellKnownSid.CreatorOwner)]
	[DataRow("CY", WellKnownSid.CryptographicOperators)]
	[DataRow("DA", WellKnownSid.DomainAdmins)]
	[DataRow("DC", WellKnownSid.DomainComputers)]
	[DataRow("DD", WellKnownSid.DomainDomainControllers)]
	[DataRow("DG", WellKnownSid.DomainGuests)]
	[DataRow("DU", WellKnownSid.DomainUsers)]
	[DataRow("EA", WellKnownSid.EnterpriseAdmins)]
	[DataRow("ED", WellKnownSid.EnterpriseDomainControllers)]
	[DataRow("EK", WellKnownSid.EnterpriseKeyAdmins)]
	[DataRow("ER", WellKnownSid.EventLogReaders)]
	[DataRow("ES", WellKnownSid.RdsEndpointServers)]
	[DataRow("HA", WellKnownSid.HyperVAdmins)]
	[DataRow("HI", WellKnownSid.MlHigh)]
	[DataRow("HO", WellKnownSid.UserModeHardwareOperators)]
	[DataRow("IS", WellKnownSid.IisIusrs)]
	[DataRow("IU", WellKnownSid.Interactive)]
	[DataRow("KA", WellKnownSid.KeyAdmins)]
	[DataRow("LA", WellKnownSid.Administrator)]
	[DataRow("LG", WellKnownSid.Guest)]
	[DataRow("LS", WellKnownSid.LocalService)]
	[DataRow("LU", WellKnownSid.PerflogUsers)]
	[DataRow("LW", WellKnownSid.MlLow)]
	[DataRow("ME", WellKnownSid.MlMedium)]
	[DataRow("MP", WellKnownSid.MlMediumPlus)]
	[DataRow("MU", WellKnownSid.PerfmonUsers)]
	[DataRow("NO", WellKnownSid.NetworkConfigurationOps)]
	[DataRow("NS", WellKnownSid.NetworkService)]
	[DataRow("NU", WellKnownSid.Network)]
	[DataRow("OW", WellKnownSid.OwnerRights)]
	[DataRow("PA", WellKnownSid.GroupPolicyCreatorOwners)]
	[DataRow("PO", WellKnownSid.PrinterOperators)]
	[DataRow("PS", WellKnownSid.PrincipalSelf)]
	[DataRow("PU", WellKnownSid.PowerUsers)]
	[DataRow("RA", WellKnownSid.RdsRemoteAccessServers)]
	[DataRow("RC", WellKnownSid.RestrictedCode)]
	[DataRow("RD", WellKnownSid.RemoteDesktop)]
	[DataRow("RE", WellKnownSid.Replicator)]
	//[DataRow("RM", WellKnownSid.RmsServiceOperators)]
	[DataRow("RO", WellKnownSid.EnterpriseReadonlyDomainControllers)]
	[DataRow("RS", WellKnownSid.RasServers)]
	[DataRow("RU", WellKnownSid.AliasPrew2Kcompacc)]
	[DataRow("SA", WellKnownSid.SchemaAdministrators)]
	[DataRow("SH", WellKnownSid.OpensshUsers)]
	[DataRow("SI", WellKnownSid.MlSystem)]
	[DataRow("SO", WellKnownSid.ServerOperators)]
	[DataRow("SS", WellKnownSid.ServiceAssertedIdentity)]
	[DataRow("SU", WellKnownSid.Service)]
	[DataRow("SY", WellKnownSid.LocalSystem)]
	[DataRow("UD", WellKnownSid.UserModeDrivers, IgnoreMessage = "FIXME: UMDF SID")]
	[DataRow("WD", WellKnownSid.Everyone)]
	[DataRow("WR", WellKnownSid.WriteRestrictedCode)]
	[DataRow("S-1-5-84-0-0-0-0-0", WellKnownSid.UserModeDrivers, IgnoreMessage = "FIXME: UMDF SID")]
	[DataRow("S-1-5-15", WellKnownSid.ThisOrganization)]
	[DataRow("S-1-5-65-1", WellKnownSid.ThisOrganizationCertificate)]
	[DataRow("S-1-5-90-0", WellKnownSid.WindowsManagerGroup)]
	[DataRow("S-1-5-32-560", WellKnownSid.WindowsAuthorizationAccessGroup)]
	public void ParseSidTest(string text, WellKnownSid expected)
	{
		SddlParseContext ctx = new SddlParseContext(text);
		var sid = SecurityIdentifier.Parse(ref ctx, DomainSid);
		var wks = sid.AsWellKnownSid();

		Assert.AreEqual(expected, wks);
	}

	[TestMethod]
	public void TestParseSd_OwnerGroup()
	{
		var sd = SecurityDescriptor.ParseSddl("O:BAG:DA", DomainSid);
		var owks = sd.Owner.AsWellKnownSid();
		var gwks = sd.Group.AsWellKnownSid();

		Assert.AreEqual(WellKnownSid.BuiltinAdministrators, owks);
		Assert.AreEqual(WellKnownSid.DomainAdmins, gwks);
	}

	[TestMethod]
	public void TestParseSd_Dacl()
	{
		var sd = SecurityDescriptor.ParseSddl("D:PAIAR(A;CI;FA;;;BA)", DomainSid);
	}

	[TestMethod]
	[DataRow("AR", (int)AclFlags.ReqAutoInherit)]
	[DataRow("AI", (int)AclFlags.AutoInherited)]
	[DataRow("P", (int)AclFlags.Protected)]
	[DataRow("NO_ACCESS_CONTROL", (int)AclFlags.NoAcl)]
	public void TestParseSd_AclFlags(string text, int expected)
	{
		SddlParseContext ctx = new SddlParseContext(text);
		var actual = AccessControlList.ParseAclFlags(ref ctx);

		Assert.AreEqual((AclFlags)expected, actual);
	}

	[TestMethod]
	[DataRow("A", AccessControlEntryType.AccessAllowed)]
	[DataRow("D", AccessControlEntryType.AccessDenied)]
	[DataRow("OA", AccessControlEntryType.AccessAllowedObject)]
	[DataRow("OD", AccessControlEntryType.AccessDeniedObject)]
	public void TestParseSd_AceType(string text, AccessControlEntryType aceType)
	{
		text = text + ";";
		SddlParseContext ctx = new SddlParseContext(text);
		var actual = AccessControlEntry.ParseAceType(ref ctx);

		Assert.AreEqual(aceType, actual);
	}

	[TestMethod]
	[DataRow("CI", AccessControlEntryFlags.ContainerInherit)]
	[DataRow("OI", AccessControlEntryFlags.ObjectInherit)]
	[DataRow("NP", AccessControlEntryFlags.NoPropagateInherit)]
	[DataRow("IO", AccessControlEntryFlags.InheritOnly)]
	[DataRow("ID", AccessControlEntryFlags.Inherited)]
	[DataRow("SA", AccessControlEntryFlags.SuccessfulAccessAudit)]
	[DataRow("FA", AccessControlEntryFlags.FailedAccessAudit)]
	[DataRow("OICIIO", AccessControlEntryFlags.ContainerInherit | AccessControlEntryFlags.ObjectInherit | AccessControlEntryFlags.InheritOnly)]
	public void TestParseSd_AceFlags(string text, AccessControlEntryFlags expected)
	{
		SddlParseContext ctx = new SddlParseContext(text);
		var actual = AccessControlEntry.ParseAceFlags(ref ctx);

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow("FA", (uint)FileAccessRights.FileAll)]
	[DataRow("0xAA55", 0xAA55u)]
	public void TestParseSd_AceRights(string text, uint expected)
	{
		SddlParseContext ctx = new SddlParseContext(text);

		var actual = AccessControlEntry.ParseRights(ref ctx);

		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[DataRow("A;CIOI;FA;;;BA", nameof(SimpleAce), AccessControlEntryType.AccessAllowed, AccessControlEntryFlags.ContainerInherit | AccessControlEntryFlags.ObjectInherit, (uint)FileAccessRights.FileAll, DisplayName = "A;CIOI;FA;;;BA")]
	[DataRow("D;NP;KR;;;BA", nameof(SimpleAce), AccessControlEntryType.AccessDenied, AccessControlEntryFlags.NoPropagateInherit, (uint)RegistryAccessRights.KeyRead, DisplayName = "D;NP;KR;;;BA")]
	[DataRow("ML;NP;NR;;;LW", nameof(MandatoryLabelAce), AccessControlEntryType.MandatoryLabel, AccessControlEntryFlags.NoPropagateInherit, (uint)MandatoryLabelPolicy.NoReadUp, DisplayName = "ML;NP;NR;;;LW")]
	public void TestParseSd_Ace(string text, string aceType, AccessControlEntryType aceKind, AccessControlEntryFlags aceFlags, uint rights)
	{
		SddlParseContext ctx = new SddlParseContext(text);
		var ace = AccessControlEntry.ParseSddl(ref ctx, DomainSid);

		Assert.AreEqual(aceType, ace.GetType().Name);
		Assert.AreEqual(aceFlags, ace.AceFlags);
		Assert.AreEqual(aceKind, ace.AceType);
		Assert.AreEqual(rights, ace.AccessMask);
	}

	[TestMethod]
	public void ParseRealSD()
	{
		SecurityDescriptor sd = new SecurityDescriptor(bytes);
		var sddl = sd.ToSddlString(SecurityDescriptorSections.All);
	}

	private static readonly byte[] bytes = BinaryHelper.ParseHexString("0100148ca80a0000b80a000014000000240100000400100107000000074238002000000003000000be3b0ef3f09fd111b6030000f80367c1a57a96bfe60dd011a28500aa003049e2010100000000000100000000074238002000000003000000bf3b0ef3f09fd111b6030000f80367c1a57a96bfe60dd011a28500aa003049e2010100000000000100000000024024000001000001050000000000051500000076836f68b6b9731755f5a9b80102000002c22400ff010f0001050000000000051500000076836f68b6b9731755f5a9b82f0c000002c22400ff010f0001050000000000051500000076836f68b6b9731755f5a9b8530400000240180000010000010200000000000520000000200200000240140020000c0001010000000000010000000004008409360000000100140002000000010100000000000100000000050a3c0010000000030000000042164cc020d011a76800aa006e052914cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a3c0010000000030000000042164cc020d011a76800aa006e0529ba7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050a3c0010000000030000001020205fa579d011902000c04fc2d4cf14cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a3c0010000000030000001020205fa579d011902000c04fc2d4cfba7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050a3c00100000000300000040c20abca979d011902000c04fc2d4cf14cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a3c00100000000300000040c20abca979d011902000c04fc2d4cfba7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050a3c001000000003000000422fba59a279d011902000c04fc2d3cf14cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a3c001000000003000000422fba59a279d011902000c04fc2d3cfba7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050a3c001000000003000000f8887003e10ad211b42200a0c968f93914cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a3c001000000003000000f8887003e10ad211b42200a0c968f939ba7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050038000001000001000000187e0f3e7a2c104cba824d926db99a3e01050000000000051500000076836f68b6b9731755f5a9b80a020000050038000001000001000000aaf63111079cd111f79f00c04fc2dcd201050000000000051500000076836f68b6b9731755f5a9b8f2010000050038000001000001000000adf63111079cd111f79f00c04fc2dcd201050000000000051500000076836f68b6b9731755f5a9b8040200000502380030000000010000000fd6475b9060b2409f372a4de88f306301050000000000051500000076836f68b6b9731755f5a9b80e0200000502380030000000010000000fd6475b9060b2409f372a4de88f306301050000000000051500000076836f68b6b9731755f5a9b80f020000050a38000800000003000000a66d029b3c0d5c468bee5199d7165cba867a96bfe60dd011a28500aa003049e2010100000000000300000000050a38000800000003000000a66d029b3c0d5c468bee5199d7165cba867a96bfe60dd011a28500aa003049e201010000000000050a000000050a380010000000030000006d9ec6b7c72cd211854e00a0c983f608867a96bfe60dd011a28500aa003049e2010100000000000509000000050a380010000000030000006d9ec6b7c72cd211854e00a0c983f6089c7a96bfe60dd011a28500aa003049e2010100000000000509000000050a380010000000030000006d9ec6b7c72cd211854e00a0c983f608ba7a96bfe60dd011a28500aa003049e2010100000000000509000000050a38002000000003000000937b1bea485ed546bc6c4df4fda78a35867a96bfe60dd011a28500aa003049e201010000000000050a00000005002c000001000001000000765be9894d44624c991a0facbeda640c0102000000000005200000002002000005002c000001000001000000aaf63111079cd111f79f00c04fc2dcd20102000000000005200000002002000005002c000001000001000000abf63111079cd111f79f00c04fc2dcd20102000000000005200000002002000005002c000001000001000000acf63111079cd111f79f00c04fc2dcd20102000000000005200000002002000005002c000001000001000000adf63111079cd111f79f00c04fc2dcd20102000000000005200000002002000005002c000001000001000000aef63111079cd111f79f00c04fc2dcd20102000000000005200000002002000005002c000001000001000000c96da3e217aec347b58bbe34c55ba6330102000000000005200000002d02000005002c001000000001000000607340c7bf20d011a76800aa006e05290102000000000005200000002a02000005002c001000000001000000d09f11b8f6046247ab7a4986c76b3f9a0102000000000005200000002a020000050a2c00940002000200000014cc28483714bc459b07ad6f015e5f280102000000000005200000002a020000050a2c0094000200020000009c7a96bfe60dd011a28500aa003049e20102000000000005200000002a020000050a2c009400020002000000ba7a96bfe60dd011a28500aa003049e20102000000000005200000002a0200000500280000010000010000005e4cc705eb4db443bd9f86664c2a7fd501010000000000050b000000050028000001000001000000765be9894d44624c991a0facbeda640c0101000000000005090000000500280000010000010000007ddcc2ccada67a4a8846c04e3cc5350101010000000000050b0000000500280000010000010000009c360f28c7678e43ae981d46f3c6f54101010000000000050b000000050028000001000001000000aaf63111079cd111f79f00c04fc2dcd2010100000000000509000000050028000001000001000000abf63111079cd111f79f00c04fc2dcd2010100000000000509000000050028000001000001000000acf63111079cd111f79f00c04fc2dcd2010100000000000509000000050028000001000001000000aef63111079cd111f79f00c04fc2dcd2010100000000000509000000050028001000000001000000d09f11b8f6046247ab7a4986c76b3f9a01010000000000050b000000050328003000000001000000e5c3783f9af7bd46a0b89d18116ddc7901010000000000050a000000050a28003001000001000000de47e6916fd9704b9557d63ff4f3ccd801010000000000050a00000000002400bd010e0001050000000000051500000076836f68b6b9731755f5a9b80002000000022400ff010f0001050000000000051500000076836f68b6b9731755f5a9b80702000000001800100002000102000000000005200000002a02000000021800040000000102000000000005200000002a02000000021800bd010f000102000000000005200000002002000000001400100000000101000000000001000000000000140094000200010100000000000509000000000014009400020001010000000000050b00000000001400ff010f000101000000000005120000000102000000000005200000002002000001020000000000052000000020020000");
}