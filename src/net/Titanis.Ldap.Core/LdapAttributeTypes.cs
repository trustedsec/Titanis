using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public static class LdapAttributeTypes
	{

		#region Exceptions
		// NOTE: This defines it with an OID syntax
		public static AttributeTypeDescription OmObjectClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.218", [SpecialAttributes.OmObjectClass], syntax: AdSyntaxes.ObjectReplicaLink_Oid);
		// NOTE: Windows 11 sends this attribute name in all lowercase
		public static AttributeTypeDescription ObjectClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.0", [SpecialAttributes.ObjectClass], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription MsDSAzObjectGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1949", ["msDS-AzObjectGuid"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription MSDSConsistencyGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1360", ["mS-DS-ConsistencyGuid"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription MsDSOptionalFeatureGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2062", ["msDS-OptionalFeatureGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription ObjectGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2", ["objectGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription AttributeSecurityGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.149", ["attributeSecurityGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription FRSReplicaSetGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.533", ["fRSReplicaSetGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription FRSVersionGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.43", ["fRSVersionGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription SchemaIDGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.148", ["schemaIDGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription DnsRecord = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.382", ["dnsRecord"], syntax: AdSyntaxes.DnsRecord);
		#endregion
		public readonly static AttributeTypeDescription ForceLogoff = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.39", ["forceLogoff"], syntax: AdSyntaxes.LargeInteger);
		#region Date/time
		public readonly static AttributeTypeDescription AccountExpires = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.159", ["accountExpires"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription BadPasswordTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.49", ["badPasswordTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription BuiltinCreationTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.13", ["builtinCreationTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription CreationTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.26", ["creationTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription DhcpUpdateTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.720", ["dhcpUpdateTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastBackupRestorationTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.519", ["lastBackupRestorationTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastContentIndexed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.50", ["lastContentIndexed"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastLogoff = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.51", ["lastLogoff"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastLogon = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.52", ["lastLogon"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastLogonTimestamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1696", ["lastLogonTimestamp"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LastSetTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.53", ["lastSetTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription LockoutTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.662", ["lockoutTime"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription PwdLastSet = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.96", ["pwdLastSet"], syntax: AdSyntaxes.LargeInteger_Timestamp);
		public readonly static AttributeTypeDescription MsDSUserPasswordExpiryTimeComputed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1996", ["msDS-UserPasswordExpiryTimeComputed"], syntax: AdSyntaxes.LargeInteger_Timestamp);

		#endregion

		#region rootDSE attributes
		public static AttributeTypeDescription dsServiceName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["dsServiceName"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription namingContexts = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["namingContexts"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription defaultNamingContext = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["defaultNamingContext"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription schemaNamingContext = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["schemaNamingContext"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription configurationNamingContext = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["configurationNamingContext"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription rootDomainNamingContext = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["rootDomainNamingContext"], syntax: AdSyntaxes.ObjectDsDn);
		public static AttributeTypeDescription supportedControl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["supportedControl"], syntax: AdSyntaxes.StringObjectIdentifier);
		public static AttributeTypeDescription supportedLDAPVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["supportedLDAPVersion"], syntax: AdSyntaxes.Integer);
		public static AttributeTypeDescription supportedLDAPPolicies = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["supportedLDAPPolicies"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription supportedSASLMechanisms = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["supportedSASLMechanisms"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription ldapServiceName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["ldapServiceName"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription supportedCapabilities = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["supportedCapabilities"], syntax: AdSyntaxes.StringObjectIdentifier);
		public static AttributeTypeDescription domainFunctionality = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["domainFunctionality"], syntax: AdSyntaxes.Integer);
		public static AttributeTypeDescription forestFunctionality = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["forestFunctionality"], syntax: AdSyntaxes.Integer);
		public static AttributeTypeDescription domainControllerFunctionality = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["domainControllerFunctionality"], syntax: AdSyntaxes.Integer);
		public static AttributeTypeDescription currentTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["currentTime"], syntax: AdSyntaxes.StringGeneralizedTime);
		public static AttributeTypeDescription isGlobalCatalogReady = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["isGlobalCatalogReady"], syntax: AdSyntaxes.Boolean);
		public static AttributeTypeDescription msDSPrefixTable= new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["msDS-PrefixTable"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription configurableSettingsEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["ConfigurableSettingsEffective"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription lDAPPoliciesEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["LDAPPoliciesEffective"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription msDSArenaInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["msDS-ArenaInfo"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription dumpLdapNotifications = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["dumpLdapNotifications"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription dsaVersionString = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["dsaVersionString"], syntax: AdSyntaxes.StringUnicode);
		public static AttributeTypeDescription validFSMOs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", ["validFSMOs"], syntax: AdSyntaxes.ObjectDsDn);
		#endregion

		//public static AttributeTypeDescription SubschemaSubentry= new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "", [""], syntax: LdapSyntaxes.StringObjectIdentifier);

		private static Dictionary<string, AttributeTypeDescription> BuildIndex()
		{
			Dictionary<string, AttributeTypeDescription> attrsByName = new Dictionary<string, AttributeTypeDescription>(StringComparer.OrdinalIgnoreCase);
			foreach (var attr in allAttrs)
			{
				if (attr.Name != null)
					attrsByName.Add(attr.Name, attr);
				if (!string.IsNullOrEmpty(attr.Oid))
					attrsByName.Add(attr.Oid, attr);
			}
			return attrsByName;
		}

		public static AttributeTypeDescription? TryGetByNameOrOid(string nameOrOid)
		{
			attrsByNameOrOid.TryGetValue(nameOrOid, out var attr);
			return attr;
		}

		#region Win2025 Schema
		public readonly static AttributeTypeDescription AccountNameHistory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1307", ["accountNameHistory"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSAggregateTokenRatePerUser = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.760", ["aCSAggregateTokenRatePerUser"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSAllocableRSVPBandwidth = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.766", ["aCSAllocableRSVPBandwidth"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSCacheTimeout = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.779", ["aCSCacheTimeout"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSDirection = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.757", ["aCSDirection"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSDSBMDeadTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.778", ["aCSDSBMDeadTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSDSBMPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.776", ["aCSDSBMPriority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSDSBMRefresh = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.777", ["aCSDSBMRefresh"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSEnableACSService = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.770", ["aCSEnableACSService"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ACSEnableRSVPAccounting = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.899", ["aCSEnableRSVPAccounting"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ACSEnableRSVPMessageLogging = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.768", ["aCSEnableRSVPMessageLogging"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ACSEventLogLevel = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.769", ["aCSEventLogLevel"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSIdentityName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.784", ["aCSIdentityName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSMaxAggregatePeakRatePerUser = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.897", ["aCSMaxAggregatePeakRatePerUser"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMaxDurationPerFlow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.761", ["aCSMaxDurationPerFlow"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSMaxNoOfAccountFiles = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.901", ["aCSMaxNoOfAccountFiles"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSMaxNoOfLogFiles = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.774", ["aCSMaxNoOfLogFiles"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSMaxPeakBandwidth = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.767", ["aCSMaxPeakBandwidth"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMaxPeakBandwidthPerFlow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.759", ["aCSMaxPeakBandwidthPerFlow"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMaxSizeOfRSVPAccountFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.902", ["aCSMaxSizeOfRSVPAccountFile"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSMaxSizeOfRSVPLogFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.775", ["aCSMaxSizeOfRSVPLogFile"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSMaxTokenBucketPerFlow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1313", ["aCSMaxTokenBucketPerFlow"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMaxTokenRatePerFlow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.758", ["aCSMaxTokenRatePerFlow"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMaximumSDUSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1314", ["aCSMaximumSDUSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMinimumDelayVariation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1317", ["aCSMinimumDelayVariation"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMinimumLatency = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1316", ["aCSMinimumLatency"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSMinimumPolicedSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1315", ["aCSMinimumPolicedSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedMaxSDUSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1320", ["aCSNonReservedMaxSDUSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedMinPolicedSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1321", ["aCSNonReservedMinPolicedSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedPeakRate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1318", ["aCSNonReservedPeakRate"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedTokenSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1319", ["aCSNonReservedTokenSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedTxLimit = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.780", ["aCSNonReservedTxLimit"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSNonReservedTxSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.898", ["aCSNonReservedTxSize"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSPermissionBits = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.765", ["aCSPermissionBits"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ACSPolicyName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.772", ["aCSPolicyName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.764", ["aCSPriority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSRSVPAccountFilesLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.900", ["aCSRSVPAccountFilesLocation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSRSVPLogFilesLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.773", ["aCSRSVPLogFilesLocation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSServerList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1312", ["aCSServerList"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSServiceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.762", ["aCSServiceType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ACSTimeOfDay = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.756", ["aCSTimeOfDay"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ACSTotalNoOfFlows = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.763", ["aCSTotalNoOfFlows"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Notes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.265", ["notes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AdditionalTrustedServiceNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.889", ["additionalTrustedServiceNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription StreetAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.256", ["streetAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AddressBookRoots = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1244", ["addressBookRoots"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription AddressBookRoots2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2046", ["addressBookRoots2"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription AddressEntryDisplayTable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.324", ["addressEntryDisplayTable"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AddressEntryDisplayTableMSDOS = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.400", ["addressEntryDisplayTableMSDOS"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription HomePostalAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.617", ["homePostalAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AddressSyntax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.255", ["addressSyntax"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AddressType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.350", ["addressType"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription AdminContextMenu = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.614", ["adminContextMenu"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AdminCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.150", ["adminCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription AdminDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.226", ["adminDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AdminDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.194", ["adminDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AdminMultiselectPropertyPages = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1690", ["adminMultiselectPropertyPages"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AdminPropertyPages = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.562", ["adminPropertyPages"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AllowedAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.913", ["allowedAttributes"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AllowedAttributesEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.914", ["allowedAttributesEffective"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AllowedChildClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.911", ["allowedChildClasses"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AllowedChildClassesEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.912", ["allowedChildClassesEffective"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AltSecurityIdentities = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.867", ["altSecurityIdentities"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ANR = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1208", ["aNR"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AppSchemaVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.848", ["appSchemaVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ApplicationName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.218", ["applicationName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AppliesTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.341", ["appliesTo"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AssetNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.283", ["assetNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Assistant = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.652", ["assistant"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription AssocNTAccount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1213", ["assocNTAccount"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AssociatedDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.37", ["associatedDomain"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription AssociatedName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.38", ["associatedName"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription AttributeDisplayNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.748", ["attributeDisplayNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AttributeID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.30", ["attributeID"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AttributeSyntax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.32", ["attributeSyntax"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription AttributeTypes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.21.5", ["attributeTypes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription AttributeCertificateAttribute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.58", ["attributeCertificateAttribute"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Audio = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.55", ["audio"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AuditingPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.202", ["auditingPolicy"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AuthenticationOptions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.11", ["authenticationOptions"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription AuthorityRevocationList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.38", ["authorityRevocationList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription AuxiliaryClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.351", ["auxiliaryClass"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription BadPwdCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.12", ["badPwdCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription BirthLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.332", ["birthLocation"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription BootFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.24", ["bootFile"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription BootParameter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.23", ["bootParameter"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription BridgeheadServerListBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.820", ["bridgeheadServerListBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription BridgeheadTransportList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.819", ["bridgeheadTransportList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription BuildingName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.48", ["buildingName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription BuiltinModifiedCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.14", ["builtinModifiedCount"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription BusinessCategory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.15", ["businessCategory"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription BytesPerMinute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.284", ["bytesPerMinute"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription CACertificate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.37", ["cACertificate"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CACertificateDN = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.697", ["cACertificateDN"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CAConnect = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.687", ["cAConnect"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CAUsages = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.690", ["cAUsages"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CAWEBURL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.688", ["cAWEBURL"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CanUpgradeScript = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.815", ["canUpgradeScript"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CanonicalName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.916", ["canonicalName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CarLicense = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.16.840.1.113730.3.1.1", ["carLicense"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Catalogs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.675", ["catalogs"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Categories = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.672", ["categories"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CategoryId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.322", ["categoryId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CertificateAuthorityObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.684", ["certificateAuthorityObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription CertificateRevocationList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.39", ["certificateRevocationList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CertificateTemplates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.823", ["certificateTemplates"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ClassDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.610", ["classDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CodePage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.16", ["codePage"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription COMClassID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.19", ["cOMClassID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMCLSID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.249", ["cOMCLSID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMInterfaceID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.20", ["cOMInterfaceID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMOtherProgId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.253", ["cOMOtherProgId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMProgID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.21", ["cOMProgID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMTreatAsClassId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.251", ["cOMTreatAsClassId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMTypelibId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.254", ["cOMTypelibId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription COMUniqueLIBID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.250", ["cOMUniqueLIBID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Info = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.81", ["info"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Cn = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.3", ["cn"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Company = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.146", ["company"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ContentIndexingAllowed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.24", ["contentIndexingAllowed"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ContextMenu = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.499", ["contextMenu"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ControlAccessRights = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.200", ["controlAccessRights"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Cost = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.135", ["cost"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription CountryCode = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.25", ["countryCode"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription C = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.6", ["c"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CreateDialog = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.810", ["createDialog"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CreateTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.18.1", ["createTimeStamp"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription CreateWizardExt = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.812", ["createWizardExt"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CreationWizard = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.498", ["creationWizard"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Creator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.679", ["creator"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription CRLObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.689", ["cRLObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription CRLPartitionedRevocationList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.683", ["cRLPartitionedRevocationList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CrossCertificatePair = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.40", ["crossCertificatePair"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CurrMachineId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.337", ["currMachineId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CurrentLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.335", ["currentLocation"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription CurrentParentCA = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.696", ["currentParentCA"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription CurrentValue = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.27", ["currentValue"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DBCSPwd = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.55", ["dBCSPwd"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DefaultClassStore = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.213", ["defaultClassStore"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DefaultGroup = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.480", ["defaultGroup"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DefaultHidingValue = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.518", ["defaultHidingValue"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription DefaultLocalPolicyObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.57", ["defaultLocalPolicyObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DefaultObjectCategory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.783", ["defaultObjectCategory"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DefaultPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.232", ["defaultPriority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DefaultSecurityDescriptor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.224", ["defaultSecurityDescriptor"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DeltaRevocationList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.53", ["deltaRevocationList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Department = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.141", ["department"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DepartmentNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.16.840.1.113730.3.1.2", ["departmentNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Description = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.13", ["description"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DesktopProfile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.346", ["desktopProfile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DestinationIndicator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.27", ["destinationIndicator"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.715", ["dhcpClasses"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DhcpFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.700", ["dhcpFlags"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription DhcpIdentification = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.701", ["dhcpIdentification"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DhcpMask = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.706", ["dhcpMask"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpMaxKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.719", ["dhcpMaxKey"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription DhcpObjDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.703", ["dhcpObjDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DhcpObjName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.702", ["dhcpObjName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DhcpOptions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.714", ["dhcpOptions"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DhcpProperties = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.718", ["dhcpProperties"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DhcpRanges = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.707", ["dhcpRanges"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpReservations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.709", ["dhcpReservations"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpServers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.704", ["dhcpServers"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpSites = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.708", ["dhcpSites"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpState = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.717", ["dhcpState"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpSubnets = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.705", ["dhcpSubnets"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DhcpType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.699", ["dhcpType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DhcpUniqueKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.698", ["dhcpUniqueKey"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription DisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.13", ["displayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DisplayNamePrintable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.353", ["displayNamePrintable"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription DITContentRules = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.21.2", ["dITContentRules"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Division = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.261", ["division"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DMDLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.36", ["dMDLocation"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DmdName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.598", ["dmdName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DNReferenceUpdate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1242", ["dNReferenceUpdate"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DnsAllowDynamic = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.378", ["dnsAllowDynamic"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription DnsAllowXFR = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.379", ["dnsAllowXFR"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription DNSHostName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.619", ["dNSHostName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DnsNotifySecondaries = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.381", ["dnsNotifySecondaries"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DNSProperty = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1306", ["dNSProperty"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DnsRoot = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.28", ["dnsRoot"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DnsSecureSecondaries = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.380", ["dnsSecureSecondaries"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DNSTombstoned = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1414", ["dNSTombstoned"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription DocumentAuthor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.14", ["documentAuthor"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DocumentIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.11", ["documentIdentifier"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DocumentLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.15", ["documentLocation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DocumentPublisher = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.56", ["documentPublisher"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DocumentTitle = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.12", ["documentTitle"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DocumentVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.13", ["documentVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DomainCAs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.668", ["domainCAs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription Dc = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.25", ["dc"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DomainCrossRef = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.472", ["domainCrossRef"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DomainID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.686", ["domainID"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DomainIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.755", ["domainIdentifier"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DomainPolicyObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.32", ["domainPolicyObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DomainPolicyReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.422", ["domainPolicyReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription DomainReplica = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.158", ["domainReplica"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DomainWidePolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.421", ["domainWidePolicy"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Drink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.5", ["drink"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DriverName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.229", ["driverName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DriverVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.276", ["driverVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DSCorePropagationData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1357", ["dSCorePropagationData"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription DSHeuristics = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.212", ["dSHeuristics"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DSUIAdminMaximum = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1344", ["dSUIAdminMaximum"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DSUIAdminNotification = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1343", ["dSUIAdminNotification"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DSUIShellMaximum = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1345", ["dSUIShellMaximum"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription DSASignature = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.74", ["dSASignature"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription DynamicLDAPServer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.537", ["dynamicLDAPServer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription Mail = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.3", ["mail"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription EFSPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.268", ["eFSPolicy"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription EmployeeID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.35", ["employeeID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription EmployeeNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.610", ["employeeNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription EmployeeType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.613", ["employeeType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Enabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.557", ["Enabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription EnabledConnection = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.36", ["enabledConnection"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription EnrollmentProviders = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.825", ["enrollmentProviders"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription EntryTTL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.4.1.1466.101.119.3", ["entryTTL"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ExtendedAttributeInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.909", ["extendedAttributeInfo"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ExtendedCharsAllowed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.380", ["extendedCharsAllowed"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ExtendedClassInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.908", ["extendedClassInfo"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ExtensionName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.227", ["extensionName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ExtraColumns = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1687", ["extraColumns"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FacsimileTelephoneNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.23", ["facsimileTelephoneNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FileExtPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.816", ["fileExtPriority"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Flags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.38", ["flags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FlatName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.511", ["flatName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ForeignIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.356", ["foreignIdentifier"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription FriendlyNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.682", ["friendlyNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FromEntry = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.910", ["fromEntry"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription FromServer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.40", ["fromServer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FrsComputerReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.869", ["frsComputerReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FrsComputerReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.870", ["frsComputerReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FRSControlDataCreation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.871", ["fRSControlDataCreation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSControlInboundBacklog = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.872", ["fRSControlInboundBacklog"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSControlOutboundBacklog = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.873", ["fRSControlOutboundBacklog"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSDirectoryFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.484", ["fRSDirectoryFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSDSPoll = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.490", ["fRSDSPoll"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSExtensions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.536", ["fRSExtensions"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription FRSFaultCondition = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.491", ["fRSFaultCondition"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSFileFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.483", ["fRSFileFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.874", ["fRSFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSLevelLimit = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.534", ["fRSLevelLimit"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSMemberReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.875", ["fRSMemberReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FRSMemberReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.876", ["fRSMemberReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FRSPartnerAuthLevel = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.877", ["fRSPartnerAuthLevel"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSPrimaryMember = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.878", ["fRSPrimaryMember"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription FRSReplicaSetType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.31", ["fRSReplicaSetType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSRootPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.487", ["fRSRootPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSRootSecurity = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.535", ["fRSRootSecurity"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription FRSServiceCommand = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.500", ["fRSServiceCommand"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSServiceCommandStatus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.879", ["fRSServiceCommandStatus"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSStagingPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.488", ["fRSStagingPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSTimeLastCommand = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.880", ["fRSTimeLastCommand"], syntax: AdSyntaxes.StringUtcTime);
		public readonly static AttributeTypeDescription FRSTimeLastConfigChange = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.881", ["fRSTimeLastConfigChange"], syntax: AdSyntaxes.StringUtcTime);
		public readonly static AttributeTypeDescription FRSUpdateTimeout = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.485", ["fRSUpdateTimeout"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription FRSVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.882", ["fRSVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FRSWorkingPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.486", ["fRSWorkingPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription FSMORoleOwner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.369", ["fSMORoleOwner"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription GarbageCollPeriod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.301", ["garbageCollPeriod"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Gecos = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.2", ["gecos"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription GeneratedConnection = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.41", ["generatedConnection"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription GenerationQualifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.44", ["generationQualifier"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GidNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.1", ["gidNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription GivenName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.42", ["givenName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GlobalAddressList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1245", ["globalAddressList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription GlobalAddressList2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2047", ["globalAddressList2"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription GovernsID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.22", ["governsID"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription GPLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.891", ["gPLink"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GPOptions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.892", ["gPOptions"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription GPCFileSysPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.894", ["gPCFileSysPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GPCFunctionalityVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.893", ["gPCFunctionalityVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription GPCMachineExtensionNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1348", ["gPCMachineExtensionNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GPCUserExtensionNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1349", ["gPCUserExtensionNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GPCWQLFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1694", ["gPCWQLFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GroupAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.152", ["groupAttributes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription GroupMembershipSAM = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.166", ["groupMembershipSAM"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription GroupPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.345", ["groupPriority"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription GroupType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.750", ["groupType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription GroupsToIgnore = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.344", ["groupsToIgnore"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription HasMasterNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.14", ["hasMasterNCs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription HasPartialReplicaNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.15", ["hasPartialReplicaNCs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription HelpData16 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.402", ["helpData16"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription HelpData32 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.9", ["helpData32"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription HelpFileName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.327", ["helpFileName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription HideFromAB = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1780", ["hideFromAB"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription HomeDirectory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.44", ["homeDirectory"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription HomeDrive = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.45", ["homeDrive"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Host = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.9", ["host"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription HouseIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.51", ["houseIdentifier"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IconPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.219", ["iconPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ImplementedCategories = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.320", ["implementedCategories"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription IndexedScopes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.681", ["indexedScopes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription InitialAuthIncoming = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.539", ["initialAuthIncoming"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription InitialAuthOutgoing = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.540", ["initialAuthOutgoing"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Initials = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.43", ["initials"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription InstallUiLevel = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.847", ["installUiLevel"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription InstanceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.1", ["instanceType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription InterSiteTopologyFailover = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1248", ["interSiteTopologyFailover"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription InterSiteTopologyGenerator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1246", ["interSiteTopologyGenerator"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription InterSiteTopologyRenew = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1247", ["interSiteTopologyRenew"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription InternationalISDNNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.25", ["internationalISDNNumber"], syntax: AdSyntaxes.StringNumeric);
		public readonly static AttributeTypeDescription InvocationId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.115", ["invocationId"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription IpHostNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.19", ["ipHostNumber"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription IpNetmaskNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.21", ["ipNetmaskNumber"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription IpNetworkNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.20", ["ipNetworkNumber"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription IpProtocolNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.17", ["ipProtocolNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription IpsecData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.623", ["ipsecData"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription IpsecDataType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.622", ["ipsecDataType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription IpsecFilterReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.629", ["ipsecFilterReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IpsecID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.621", ["ipsecID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IpsecISAKMPReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.626", ["ipsecISAKMPReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IpsecName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.620", ["ipsecName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IPSECNegotiationPolicyAction = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.888", ["iPSECNegotiationPolicyAction"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IpsecNegotiationPolicyReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.628", ["ipsecNegotiationPolicyReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IPSECNegotiationPolicyType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.887", ["iPSECNegotiationPolicyType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IpsecNFAReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.627", ["ipsecNFAReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IpsecOwnersReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.624", ["ipsecOwnersReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IpsecPolicyReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.517", ["ipsecPolicyReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IpServicePort = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.15", ["ipServicePort"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription IpServiceProtocol = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.16", ["ipServiceProtocol"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription IsCriticalSystemObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.868", ["isCriticalSystemObject"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription IsDefunct = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.661", ["isDefunct"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription IsDeleted = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.48", ["isDeleted"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription IsEphemeral = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1212", ["isEphemeral"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MemberOf = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.102", ["memberOf"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IsMemberOfPartialAttributeSet = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.639", ["isMemberOfPartialAttributeSet"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription IsPrivilegeHolder = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.638", ["isPrivilegeHolder"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription IsRecycled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2058", ["isRecycled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription IsSingleValued = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.33", ["isSingleValued"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription JpegPhoto = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.60", ["jpegPhoto"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Keywords = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.48", ["keywords"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription KnowledgeInformation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.2", ["knowledgeInformation"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription LabeledURI = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.4.1.250.1.57", ["labeledURI"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LastKnownParent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.781", ["lastKnownParent"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription LastUpdateSequence = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.330", ["lastUpdateSequence"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LDAPAdminLimits = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.843", ["lDAPAdminLimits"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LDAPDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.460", ["lDAPDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LDAPIPDenyList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.844", ["lDAPIPDenyList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LegacyExchangeDN = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.655", ["legacyExchangeDN"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription LinkID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.50", ["linkID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription LinkTrackSecret = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.269", ["linkTrackSecret"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LmPwdHistory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.160", ["lmPwdHistory"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LocalPolicyFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.56", ["localPolicyFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription LocalPolicyReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.457", ["localPolicyReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription LocaleID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.58", ["localeID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription L = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.7", ["l"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LocalizationDisplayId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1353", ["localizationDisplayId"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription LocalizedDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.817", ["localizedDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Location = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.222", ["location"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription LockOutObservationWindow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.61", ["lockOutObservationWindow"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription LockoutDuration = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.60", ["lockoutDuration"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription LockoutThreshold = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.73", ["lockoutThreshold"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription LoginShell = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.4", ["loginShell"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription ThumbnailLogo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.16.840.1.113730.3.1.36", ["thumbnailLogo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LogonCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.169", ["logonCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription LogonHours = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.64", ["logonHours"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LogonWorkstation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.65", ["logonWorkstation"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription LSACreationTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.66", ["lSACreationTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription LSAModifiedCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.67", ["lSAModifiedCount"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MacAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.22", ["macAddress"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MachineArchitecture = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.68", ["machineArchitecture"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription MachinePasswordChangeInterval = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.520", ["machinePasswordChangeInterval"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MachineRole = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.71", ["machineRole"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription MachineWidePolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.459", ["machineWidePolicy"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ManagedBy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.653", ["managedBy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ManagedObjects = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.654", ["managedObjects"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription Manager = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.10", ["manager"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MAPIID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.49", ["mAPIID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MarshalledInterface = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.72", ["marshalledInterface"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MasteredBy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1409", ["masteredBy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MaxPwdAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.74", ["maxPwdAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MaxRenewAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.75", ["maxRenewAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MaxStorage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.76", ["maxStorage"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MaxTicketAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.77", ["maxTicketAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MayContain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.25", ["mayContain"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription MeetingAdvertiseScope = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.582", ["meetingAdvertiseScope"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingApplication = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.573", ["meetingApplication"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingBandwidth = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.589", ["meetingBandwidth"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MeetingBlob = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.590", ["meetingBlob"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MeetingContactInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.578", ["meetingContactInfo"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.567", ["meetingDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingEndTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.588", ["meetingEndTime"], syntax: AdSyntaxes.StringUtcTime);
		public readonly static AttributeTypeDescription MeetingID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.565", ["meetingID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingIP = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.580", ["meetingIP"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingIsEncrypted = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.585", ["meetingIsEncrypted"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingKeyword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.568", ["meetingKeyword"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.574", ["meetingLanguage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.569", ["meetingLocation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingMaxParticipants = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.576", ["meetingMaxParticipants"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MeetingName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.566", ["meetingName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingOriginator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.577", ["meetingOriginator"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingOwner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.579", ["meetingOwner"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingProtocol = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.570", ["meetingProtocol"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingRating = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.584", ["meetingRating"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingRecurrence = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.586", ["meetingRecurrence"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingScope = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.581", ["meetingScope"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingStartTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.587", ["meetingStartTime"], syntax: AdSyntaxes.StringUtcTime);
		public readonly static AttributeTypeDescription MeetingType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.571", ["meetingType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MeetingURL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.583", ["meetingURL"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Member = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.31", ["member"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MemberNisNetgroup = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.13", ["memberNisNetgroup"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MemberUid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.12", ["memberUid"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MhsORAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.650", ["mhsORAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MinPwdAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.78", ["minPwdAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MinPwdLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.79", ["minPwdLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MinTicketAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.80", ["minTicketAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ModifiedCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.168", ["modifiedCount"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ModifiedCountAtLastProm = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.81", ["modifiedCountAtLastProm"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ModifyTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.18.2", ["modifyTimeStamp"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription Moniker = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.82", ["moniker"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MonikerDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.83", ["monikerDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MoveTreeState = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1305", ["moveTreeState"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsAuthzCentralAccessPolicyID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2154", ["msAuthz-CentralAccessPolicyID"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription MsAuthzEffectiveSecurityPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2150", ["msAuthz-EffectiveSecurityPolicy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsAuthzLastEffectiveSecurityPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2152", ["msAuthz-LastEffectiveSecurityPolicy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsAuthzMemberRulesInCentralAccessPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2155", ["msAuthz-MemberRulesInCentralAccessPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsAuthzMemberRulesInCentralAccessPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2156", ["msAuthz-MemberRulesInCentralAccessPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsAuthzProposedSecurityPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2151", ["msAuthz-ProposedSecurityPolicy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsAuthzResourceCondition = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2153", ["msAuthz-ResourceCondition"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsCOMDefaultPartitionLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1427", ["msCOM-DefaultPartitionLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsCOMObjectId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1428", ["msCOM-ObjectId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsCOMPartitionLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1423", ["msCOM-PartitionLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsCOMPartitionSetLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1424", ["msCOM-PartitionSetLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsCOMUserLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1425", ["msCOM-UserLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsCOMUserPartitionSetLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1426", ["msCOM-UserPartitionSetLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDFSCommentv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2036", ["msDFS-Commentv2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSGenerationGUIDv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2032", ["msDFS-GenerationGUIDv2"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSLastModifiedv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2034", ["msDFS-LastModifiedv2"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsDFSLinkIdentityGUIDv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2041", ["msDFS-LinkIdentityGUIDv2"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSLinkPathv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2039", ["msDFS-LinkPathv2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSLinkSecurityDescriptorv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2040", ["msDFS-LinkSecurityDescriptorv2"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription MsDFSNamespaceIdentityGUIDv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2033", ["msDFS-NamespaceIdentityGUIDv2"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSPropertiesv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2037", ["msDFS-Propertiesv2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSSchemaMajorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2030", ["msDFS-SchemaMajorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSSchemaMinorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2031", ["msDFS-SchemaMinorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSShortNameLinkPathv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2042", ["msDFS-ShortNameLinkPathv2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSTargetListv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2038", ["msDFS-TargetListv2"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSTtlv2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2035", ["msDFS-Ttlv2"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRCachePolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.29", ["msDFSR-CachePolicy"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRCommonStagingPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.38", ["msDFSR-CommonStagingPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRCommonStagingSizeInMb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.39", ["msDFSR-CommonStagingSizeInMb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRComputerReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.101", ["msDFSR-ComputerReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDFSRComputerReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.13.3.103", ["msDFSR-ComputerReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDFSRConflictPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.7", ["msDFSR-ConflictPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRConflictSizeInMb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.8", ["msDFSR-ConflictSizeInMb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRContentSetGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.18", ["msDFSR-ContentSetGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSRDefaultCompressionExclusionFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.34", ["msDFSR-DefaultCompressionExclusionFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRDeletedPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.26", ["msDFSR-DeletedPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRDeletedSizeInMb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.27", ["msDFSR-DeletedSizeInMb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRDfsLinkTarget = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.24", ["msDFSR-DfsLinkTarget"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRDfsPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.21", ["msDFSR-DfsPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRDirectoryFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.13", ["msDFSR-DirectoryFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRDisablePacketPrivacy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.32", ["msDFSR-DisablePacketPrivacy"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDFSREnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.9", ["msDFSR-Enabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDFSRExtension = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.2", ["msDFSR-Extension"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSRFileFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.12", ["msDFSR-FileFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.16", ["msDFSR-Flags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRKeywords = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.15", ["msDFSR-Keywords"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRMaxAgeInCacheInMin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.31", ["msDFSR-MaxAgeInCacheInMin"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRMemberReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.100", ["msDFSR-MemberReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDFSRMemberReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.13.3.102", ["msDFSR-MemberReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDFSRMinDurationCacheInMin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.30", ["msDFSR-MinDurationCacheInMin"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSROnDemandExclusionDirectoryFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.36", ["msDFSR-OnDemandExclusionDirectoryFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSROnDemandExclusionFileFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.35", ["msDFSR-OnDemandExclusionFileFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSROptions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.17", ["msDFSR-Options"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSROptions2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.37", ["msDFSR-Options2"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.25", ["msDFSR-Priority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRRdcEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.19", ["msDFSR-RdcEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDFSRRdcMinFileSizeInKb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.20", ["msDFSR-RdcMinFileSizeInKb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRReadOnly = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.28", ["msDFSR-ReadOnly"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDFSRReplicationGroupGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.23", ["msDFSR-ReplicationGroupGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSRReplicationGroupType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.10", ["msDFSR-ReplicationGroupType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRRootFence = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.22", ["msDFSR-RootFence"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRRootPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.3", ["msDFSR-RootPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRRootSizeInMb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.4", ["msDFSR-RootSizeInMb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRSchedule = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.14", ["msDFSR-Schedule"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDFSRStagingCleanupTriggerInPercent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.40", ["msDFSR-StagingCleanupTriggerInPercent"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRStagingPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.5", ["msDFSR-StagingPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDFSRStagingSizeInMb = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.6", ["msDFSR-StagingSizeInMb"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDFSRTombstoneExpiryInMin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.11", ["msDFSR-TombstoneExpiryInMin"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDFSRVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.13.3.1", ["msDFSR-Version"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDNSDNSKEYRecordSetTTL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2139", ["msDNS-DNSKEYRecordSetTTL"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSDNSKEYRecords = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2145", ["msDNS-DNSKEYRecords"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDNSDSRecordAlgorithms = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2134", ["msDNS-DSRecordAlgorithms"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSDSRecordSetTTL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2140", ["msDNS-DSRecordSetTTL"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSIsSigned = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2130", ["msDNS-IsSigned"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDNSKeymasterZones = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2128", ["msDNS-KeymasterZones"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDNSMaintainTrustAnchor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2133", ["msDNS-MaintainTrustAnchor"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSNSEC3CurrentSalt = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2149", ["msDNS-NSEC3CurrentSalt"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDNSNSEC3HashAlgorithm = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2136", ["msDNS-NSEC3HashAlgorithm"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSNSEC3Iterations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2138", ["msDNS-NSEC3Iterations"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSNSEC3OptOut = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2132", ["msDNS-NSEC3OptOut"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDNSNSEC3RandomSaltLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2137", ["msDNS-NSEC3RandomSaltLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSNSEC3UserSalt = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2148", ["msDNS-NSEC3UserSalt"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDNSParentHasSecureDelegation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2146", ["msDNS-ParentHasSecureDelegation"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDNSPropagationTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2147", ["msDNS-PropagationTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSRFC5011KeyRollovers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2135", ["msDNS-RFC5011KeyRollovers"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDNSSecureDelegationPollingPeriod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2142", ["msDNS-SecureDelegationPollingPeriod"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSSignWithNSEC3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2131", ["msDNS-SignWithNSEC3"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDNSSignatureInceptionOffset = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2141", ["msDNS-SignatureInceptionOffset"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDNSSigningKeyDescriptors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2143", ["msDNS-SigningKeyDescriptors"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDNSSigningKeys = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2144", ["msDNS-SigningKeys"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDRMIdentityCertificate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1843", ["msDRM-IdentityCertificate"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSAdditionalDnsHostName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1717", ["msDS-AdditionalDnsHostName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAdditionalSamAccountName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1718", ["msDS-AdditionalSamAccountName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAllUsersTrustQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1789", ["msDS-AllUsersTrustQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAllowedDNSSuffixes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1710", ["msDS-AllowedDNSSuffixes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAllowedToActOnBehalfOfOtherIdentity = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2182", ["msDS-AllowedToActOnBehalfOfOtherIdentity"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription MsDSAllowedToDelegateTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1787", ["msDS-AllowedToDelegateTo"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAppliesToResourceTypes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2195", ["msDS-AppliesToResourceTypes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSApproxImmedSubordinates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1669", ["msDS-Approx-Immed-Subordinates"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSApproximateLastLogonTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2262", ["msDS-ApproximateLastLogonTimeStamp"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSAssignedAuthNPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2295", ["msDS-AssignedAuthNPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAssignedAuthNPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2296", ["msDS-AssignedAuthNPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAssignedAuthNPolicySilo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2285", ["msDS-AssignedAuthNPolicySilo"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAssignedAuthNPolicySiloBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2286", ["msDS-AssignedAuthNPolicySiloBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAuthenticatedAtDC = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1958", ["msDS-AuthenticatedAtDC"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAuthenticatedToAccountlist = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1957", ["msDS-AuthenticatedToAccountlist"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAuthNPolicyEnforced = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2297", ["msDS-AuthNPolicyEnforced"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSAuthNPolicySiloEnforced = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2298", ["msDS-AuthNPolicySiloEnforced"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSAuthNPolicySiloMembers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2287", ["msDS-AuthNPolicySiloMembers"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAuthNPolicySiloMembersBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2288", ["msDS-AuthNPolicySiloMembersBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSAuxiliaryClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1458", ["msDS-Auxiliary-Classes"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription MsDSAzApplicationData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1819", ["msDS-AzApplicationData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzApplicationName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1798", ["msDS-AzApplicationName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzApplicationVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1817", ["msDS-AzApplicationVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzBizRule = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1801", ["msDS-AzBizRule"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzBizRuleLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1802", ["msDS-AzBizRuleLanguage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzClassId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1816", ["msDS-AzClassId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzDomainTimeout = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1795", ["msDS-AzDomainTimeout"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzGenerateAudits = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1805", ["msDS-AzGenerateAudits"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSAzGenericData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1950", ["msDS-AzGenericData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzLastImportedBizRulePath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1803", ["msDS-AzLastImportedBizRulePath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzLDAPQuery = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1792", ["msDS-AzLDAPQuery"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzMajorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1824", ["msDS-AzMajorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzMinorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1825", ["msDS-AzMinorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzOperationID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1800", ["msDS-AzOperationID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzScopeName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1799", ["msDS-AzScopeName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSAzScriptEngineCacheMax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1796", ["msDS-AzScriptEngineCacheMax"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzScriptTimeout = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1797", ["msDS-AzScriptTimeout"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSAzTaskIsRoleDefinition = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1818", ["msDS-AzTaskIsRoleDefinition"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSBehaviorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1459", ["msDS-Behavior-Version"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSBridgeHeadServersUsed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2049", ["msDS-BridgeHeadServersUsed"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsDSByteArray = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1831", ["msDS-ByteArray"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSCachedMembership = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1441", ["msDS-Cached-Membership"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSCachedMembershipTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1442", ["msDS-Cached-Membership-Time-Stamp"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSClaimAttributeSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2099", ["msDS-ClaimAttributeSource"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSClaimIsSingleValued = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2160", ["msDS-ClaimIsSingleValued"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSClaimIsValueSpaceRestricted = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2159", ["msDS-ClaimIsValueSpaceRestricted"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSClaimPossibleValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2097", ["msDS-ClaimPossibleValues"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSClaimSharesPossibleValuesWith = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2101", ["msDS-ClaimSharesPossibleValuesWith"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSClaimSharesPossibleValuesWithBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2102", ["msDS-ClaimSharesPossibleValuesWithBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSClaimSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2157", ["msDS-ClaimSource"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSClaimSourceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2158", ["msDS-ClaimSourceType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSClaimTypeAppliesToClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2100", ["msDS-ClaimTypeAppliesToClass"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSClaimValueType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2098", ["msDS-ClaimValueType"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSCloudAnchor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2273", ["msDS-CloudAnchor"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSCloudIsEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2275", ["msDS-CloudIsEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSCloudIsManaged = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2271", ["msDS-CloudIsManaged"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSCloudIssuerPublicCertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2274", ["msDS-CloudIssuerPublicCertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute1 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2214", ["msDS-cloudExtensionAttribute1"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute10 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2223", ["msDS-cloudExtensionAttribute10"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute11 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2224", ["msDS-cloudExtensionAttribute11"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute12 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2225", ["msDS-cloudExtensionAttribute12"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute13 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2226", ["msDS-cloudExtensionAttribute13"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute14 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2227", ["msDS-cloudExtensionAttribute14"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute15 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2228", ["msDS-cloudExtensionAttribute15"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute16 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2229", ["msDS-cloudExtensionAttribute16"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute17 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2230", ["msDS-cloudExtensionAttribute17"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute18 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2231", ["msDS-cloudExtensionAttribute18"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute19 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2232", ["msDS-cloudExtensionAttribute19"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2215", ["msDS-cloudExtensionAttribute2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute20 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2233", ["msDS-cloudExtensionAttribute20"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2216", ["msDS-cloudExtensionAttribute3"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2217", ["msDS-cloudExtensionAttribute4"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute5 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2218", ["msDS-cloudExtensionAttribute5"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute6 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2219", ["msDS-cloudExtensionAttribute6"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute7 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2220", ["msDS-cloudExtensionAttribute7"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute8 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2221", ["msDS-cloudExtensionAttribute8"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDScloudExtensionAttribute9 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2222", ["msDS-cloudExtensionAttribute9"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSComputerAllowedToAuthenticateTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2280", ["msDS-ComputerAllowedToAuthenticateTo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSComputerAuthNPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2291", ["msDS-ComputerAuthNPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSComputerAuthNPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2292", ["msDS-ComputerAuthNPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSComputerSID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2321", ["msDS-ComputerSID"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription MsDSComputerTGTLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2281", ["msDS-ComputerTGTLifetime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MSDSConsistencyChildCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1361", ["mS-DS-ConsistencyChildCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSDSCreatorSID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1410", ["mS-DS-CreatorSID"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription MsDSCustomKeyInformation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2322", ["msDS-CustomKeyInformation"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSDateTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1832", ["msDS-DateTime"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsDSDefaultQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1846", ["msDS-DefaultQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSDeletedObjectLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2068", ["msDS-DeletedObjectLifetime"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription MsDSDeviceDN = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2320", ["msDS-DeviceDN"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDeviceID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2252", ["msDS-DeviceID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSDeviceLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2261", ["msDS-DeviceLocation"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSDeviceMDMStatus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2308", ["msDS-DeviceMDMStatus"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDeviceObjectVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2257", ["msDS-DeviceObjectVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSDeviceOSType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2249", ["msDS-DeviceOSType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDeviceOSVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2250", ["msDS-DeviceOSVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDevicePhysicalIDs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2251", ["msDS-DevicePhysicalIDs"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDeviceTrustType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2325", ["msDS-DeviceTrustType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSDnsRootAlias = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1719", ["msDS-DnsRootAlias"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSDrsFarmID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2265", ["msDS-DrsFarmID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSEgressClaimsTransformationPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2192", ["msDS-EgressClaimsTransformationPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSEnabledFeature = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2061", ["msDS-EnabledFeature"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSEnabledFeatureBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2069", ["msDS-EnabledFeatureBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSEntryTimeToDie = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1622", ["msDS-Entry-Time-To-Die"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsDSExecuteScriptPassword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1783", ["msDS-ExecuteScriptPassword"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSExpirePasswordsOnSmartCardOnlyAccounts = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2344", ["msDS-ExpirePasswordsOnSmartCardOnlyAccounts"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSExternalDirectoryObjectId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2310", ["msDS-ExternalDirectoryObjectId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSExternalKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1833", ["msDS-ExternalKey"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSExternalStore = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1834", ["msDS-ExternalStore"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSFailedInteractiveLogonCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1972", ["msDS-FailedInteractiveLogonCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSFailedInteractiveLogonCountAtLastSuccessfulLogon = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1973", ["msDS-FailedInteractiveLogonCountAtLastSuccessfulLogon"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSFilterContainers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1703", ["msDS-FilterContainers"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSGenerationId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2166", ["msDS-GenerationId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSGeoCoordinatesAltitude = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2183", ["msDS-GeoCoordinatesAltitude"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSGeoCoordinatesLatitude = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2184", ["msDS-GeoCoordinatesLatitude"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSGeoCoordinatesLongitude = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2185", ["msDS-GeoCoordinatesLongitude"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSGroupMSAMembership = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2200", ["msDS-GroupMSAMembership"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription MsDSHABSeniorityIndex = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1997", ["msDS-HABSeniorityIndex"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSHasDomainNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1820", ["msDS-HasDomainNCs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDShasFullReplicaNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1925", ["msDS-hasFullReplicaNCs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSHasInstantiatedNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1709", ["msDS-HasInstantiatedNCs"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsDShasMasterNCs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1836", ["msDS-hasMasterNCs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSHostServiceAccount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2056", ["msDS-HostServiceAccount"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSHostServiceAccountBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2057", ["msDS-HostServiceAccountBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSIngressClaimsTransformationPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2191", ["msDS-IngressClaimsTransformationPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSInteger = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1835", ["msDS-Integer"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSIntId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1716", ["msDS-IntId"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSIsCompliant = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2314", ["msDS-IsCompliant"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIsDomainFor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1933", ["msDS-IsDomainFor"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSIsEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2248", ["msDS-IsEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIsFullReplicaFor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1932", ["msDS-IsFullReplicaFor"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsdsmemberOfTransitive = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2236", ["msds-memberOfTransitive"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSIsPartialReplicaFor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1934", ["msDS-IsPartialReplicaFor"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSIsPossibleValuesPresent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2186", ["msDS-IsPossibleValuesPresent"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIsPrimaryComputerFor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2168", ["msDS-IsPrimaryComputerFor"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSIsUsedAsResourceSecurityAttribute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2095", ["msDS-IsUsedAsResourceSecurityAttribute"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIsUserCachableAtRodc = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2025", ["msDS-IsUserCachableAtRodc"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSisGC = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1959", ["msDS-isGC"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIsManaged = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2270", ["msDS-IsManaged"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSisRODC = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1960", ["msDS-isRODC"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSIssuerCertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2240", ["msDS-IssuerCertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSIssuerPublicCertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2269", ["msDS-IssuerPublicCertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSKeyApproximateLastLogonTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2323", ["msDS-KeyApproximateLastLogonTimeStamp"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSKeyCredentialLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2328", ["msDS-KeyCredentialLink"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsDSKeyCredentialLinkBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2329", ["msDS-KeyCredentialLink-BL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSKeyId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2315", ["msDS-KeyId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSKeyMaterial = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2316", ["msDS-KeyMaterial"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSKeyPrincipal = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2318", ["msDS-KeyPrincipal"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSKeyPrincipalBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2319", ["msDS-KeyPrincipalBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSKeyUsage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2317", ["msDS-KeyUsage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSKeyVersionNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1782", ["msDS-KeyVersionNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSKrbTgtLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1923", ["msDS-KrbTgtLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSKrbTgtLinkBl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1931", ["msDS-KrbTgtLinkBl"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSLastFailedInteractiveLogonTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1971", ["msDS-LastFailedInteractiveLogonTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSLastKnownRDN = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2067", ["msDS-LastKnownRDN"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSLastSuccessfulInteractiveLogonTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1970", ["msDS-LastSuccessfulInteractiveLogonTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSLocalEffectiveDeletionTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2059", ["msDS-LocalEffectiveDeletionTime"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsDSLocalEffectiveRecycleTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2060", ["msDS-LocalEffectiveRecycleTime"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsDSLockoutDuration = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2018", ["msDS-LockoutDuration"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSLockoutObservationWindow = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2017", ["msDS-LockoutObservationWindow"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSLockoutThreshold = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2019", ["msDS-LockoutThreshold"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSLogonTimeSyncInterval = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1784", ["msDS-LogonTimeSyncInterval"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSMachineAccountQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1411", ["ms-DS-MachineAccountQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSManagedPassword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2196", ["msDS-ManagedPassword"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSManagedPasswordId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2197", ["msDS-ManagedPasswordId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSManagedPasswordInterval = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2199", ["msDS-ManagedPasswordInterval"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSManagedPasswordPreviousId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2198", ["msDS-ManagedPasswordPreviousId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDsmasteredBy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1837", ["msDs-masteredBy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDsMaxValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1842", ["msDs-MaxValues"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSMaximumPasswordAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2011", ["msDS-MaximumPasswordAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSMaximumRegistrationInactivityPeriod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2242", ["msDS-MaximumRegistrationInactivityPeriod"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsdsmemberTransitive = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2238", ["msds-memberTransitive"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSMembersForAzRole = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1806", ["msDS-MembersForAzRole"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSMembersForAzRoleBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1807", ["msDS-MembersForAzRoleBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSMembersOfResourcePropertyList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2103", ["msDS-MembersOfResourcePropertyList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSMembersOfResourcePropertyListBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2104", ["msDS-MembersOfResourcePropertyListBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSMinimumPasswordAge = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2012", ["msDS-MinimumPasswordAge"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSMinimumPasswordLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2013", ["msDS-MinimumPasswordLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSNCReplCursors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1704", ["msDS-NCReplCursors"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSNCReplInboundNeighbors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1705", ["msDS-NCReplInboundNeighbors"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSNCReplOutboundNeighbors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1706", ["msDS-NCReplOutboundNeighbors"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSNCReplicaLocations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1661", ["msDS-NC-Replica-Locations"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNCROReplicaLocations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1967", ["msDS-NC-RO-Replica-Locations"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNCROReplicaLocationsBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1968", ["msDS-NC-RO-Replica-Locations-BL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNcType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2024", ["msDS-NcType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSNeverRevealGroup = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1926", ["msDS-NeverRevealGroup"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNonMembers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1793", ["msDS-NonMembers"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNonMembersBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1794", ["msDS-NonMembersBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSNonSecurityGroupExtraClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1689", ["msDS-Non-Security-Group-Extra-Classes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSObjectReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1840", ["msDS-ObjectReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSObjectReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1841", ["msDS-ObjectReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSObjectSoa = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2353", ["msDS-ObjectSoa"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSOIDToGroupLink = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2051", ["msDS-OIDToGroupLink"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOIDToGroupLinkBl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2052", ["msDS-OIDToGroupLinkBl"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOperationsForAzRole = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1812", ["msDS-OperationsForAzRole"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOperationsForAzRoleBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1813", ["msDS-OperationsForAzRoleBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOperationsForAzTask = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1808", ["msDS-OperationsForAzTask"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOperationsForAzTaskBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1809", ["msDS-OperationsForAzTaskBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSOptionalFeatureFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2063", ["msDS-OptionalFeatureFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSOtherSettings = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1621", ["msDS-Other-Settings"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSparentdistname = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2203", ["msDS-parentdistname"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSPasswordComplexityEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2015", ["msDS-PasswordComplexityEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSPasswordHistoryLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2014", ["msDS-PasswordHistoryLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSPasswordReversibleEncryptionEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2016", ["msDS-PasswordReversibleEncryptionEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSPasswordSettingsPrecedence = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2023", ["msDS-PasswordSettingsPrecedence"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSPerUserTrustQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1788", ["msDS-PerUserTrustQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSPerUserTrustTombstonesQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1790", ["msDS-PerUserTrustTombstonesQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSPhoneticCompanyName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1945", ["msDS-PhoneticCompanyName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPhoneticDepartment = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1944", ["msDS-PhoneticDepartment"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPhoneticDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1946", ["msDS-PhoneticDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPhoneticFirstName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1942", ["msDS-PhoneticFirstName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPhoneticLastName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1943", ["msDS-PhoneticLastName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSpreferredDataLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2366", ["msDS-preferredDataLocation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPreferredGCSite = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1444", ["msDS-Preferred-GC-Site"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSPrimaryComputer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2167", ["msDS-PrimaryComputer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSPrincipalName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1865", ["msDS-PrincipalName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPromotionSettings = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1962", ["msDS-PromotionSettings"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSPSOApplied = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2021", ["msDS-PSOApplied"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSPSOAppliesTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2020", ["msDS-PSOAppliesTo"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSQuotaAmount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1845", ["msDS-QuotaAmount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSQuotaEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1848", ["msDS-QuotaEffective"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSQuotaTrustee = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1844", ["msDS-QuotaTrustee"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription MsDSQuotaUsed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1849", ["msDS-QuotaUsed"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSRegisteredOwner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2258", ["msDS-RegisteredOwner"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSRegisteredUsers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2263", ["msDS-RegisteredUsers"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSRegistrationQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2241", ["msDS-RegistrationQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSReplAttributeMetaData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1707", ["msDS-ReplAttributeMetaData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSReplValueMetaData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1708", ["msDS-ReplValueMetaData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSReplValueMetaDataExt = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2235", ["msDS-ReplValueMetaDataExt"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSDSReplicatesNCReason = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1408", ["mS-DS-ReplicatesNCReason"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsDSReplicationNotifyFirstDSADelay = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1663", ["msDS-Replication-Notify-First-DSA-Delay"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSReplicationNotifySubsequentDSADelay = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1664", ["msDS-Replication-Notify-Subsequent-DSA-Delay"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSReplicationEpoch = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1720", ["msDS-ReplicationEpoch"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSRequiredDomainBehaviorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2066", ["msDS-RequiredDomainBehaviorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSRequiredForestBehaviorVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2079", ["msDS-RequiredForestBehaviorVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSResultantPSO = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2022", ["msDS-ResultantPSO"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSRetiredReplNCSignatures = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1826", ["msDS-RetiredReplNCSignatures"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSRevealOnDemandGroup = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1928", ["msDS-RevealOnDemandGroup"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSRevealedDSAs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1930", ["msDS-RevealedDSAs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSRevealedList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1940", ["msDS-RevealedList"], syntax: AdSyntaxes.ObjectDnString);
		public readonly static AttributeTypeDescription MsDSRevealedListBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1975", ["msDS-RevealedListBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSRevealedUsers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1924", ["msDS-RevealedUsers"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsDSRIDPoolAllocationEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2213", ["msDS-RIDPoolAllocationEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDsSchemaExtensions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1440", ["msDs-Schema-Extensions"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSSDReferenceDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1711", ["msDS-SDReferenceDomain"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSSecondaryKrbTgtNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1929", ["msDS-SecondaryKrbTgtNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSSecurityGroupExtraClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1688", ["msDS-Security-Group-Extra-Classes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSServiceAllowedNTLMNetworkAuthentication = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2349", ["msDS-ServiceAllowedNTLMNetworkAuthentication"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSServiceAllowedToAuthenticateFrom = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2283", ["msDS-ServiceAllowedToAuthenticateFrom"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSServiceAllowedToAuthenticateTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2282", ["msDS-ServiceAllowedToAuthenticateTo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSServiceAuthNPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2293", ["msDS-ServiceAuthNPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSServiceAuthNPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2294", ["msDS-ServiceAuthNPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSServiceTGTLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2284", ["msDS-ServiceTGTLifetime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSSettings = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1697", ["msDS-Settings"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSShadowPrincipalSid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2324", ["msDS-ShadowPrincipalSid"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription MsDSSiteAffinity = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1443", ["msDS-Site-Affinity"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSSiteName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1961", ["msDS-SiteName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSSourceAnchor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2352", ["msDS-SourceAnchor"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSSourceObjectDN = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1879", ["msDS-SourceObjectDN"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSSPNSuffixes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1715", ["msDS-SPNSuffixes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSStrongNTLMPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2350", ["msDS-StrongNTLMPolicy"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSSupportedEncryptionTypes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1963", ["msDS-SupportedEncryptionTypes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSSyncServerUrl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2276", ["msDS-SyncServerUrl"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSTasksForAzRole = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1814", ["msDS-TasksForAzRole"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTasksForAzRoleBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1815", ["msDS-TasksForAzRoleBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTasksForAzTask = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1810", ["msDS-TasksForAzTask"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTasksForAzTaskBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1811", ["msDS-TasksForAzTaskBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTDOEgressBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2194", ["msDS-TDOEgressBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTDOIngressBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2193", ["msDS-TDOIngressBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsdstokenGroupNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2345", ["msds-tokenGroupNames"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsdstokenGroupNamesGlobalAndUniversal = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2346", ["msds-tokenGroupNamesGlobalAndUniversal"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsdstokenGroupNamesNoGCAcceptable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2347", ["msds-tokenGroupNamesNoGCAcceptable"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSTombstoneQuotaFactor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1847", ["msDS-TombstoneQuotaFactor"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSTopQuotaUsage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1850", ["msDS-TopQuotaUsage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSTransformationRules = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2189", ["msDS-TransformationRules"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSTransformationRulesCompiled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2190", ["msDS-TransformationRulesCompiled"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSTrustForestTrustInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1702", ["msDS-TrustForestTrustInfo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSUpdateScript = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1721", ["msDS-UpdateScript"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsDSUserAccountControlComputed = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1460", ["msDS-User-Account-Control-Computed"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsDSUserAllowedNTLMNetworkAuthentication = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2348", ["msDS-UserAllowedNTLMNetworkAuthentication"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsDSUserAllowedToAuthenticateFrom = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2278", ["msDS-UserAllowedToAuthenticateFrom"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSUserAllowedToAuthenticateTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2277", ["msDS-UserAllowedToAuthenticateTo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsDSUserAuthNPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2289", ["msDS-UserAuthNPolicy"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSUserAuthNPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2290", ["msDS-UserAuthNPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSUserTGTLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2279", ["msDS-UserTGTLifetime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSUSNLastSyncSuccess = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2055", ["msDS-USNLastSyncSuccess"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsDSValueTypeReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2187", ["msDS-ValueTypeReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsDSValueTypeReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2188", ["msDS-ValueTypeReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsExchAssistantName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.444", ["msExchAssistantName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsExchHouseIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.596", ["msExchHouseIdentifier"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsExchLabeledURI = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.593", ["msExchLabeledURI"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OwnerBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.104", ["ownerBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsFRSHubMember = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1693", ["msFRS-Hub-Member"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsFRSTopologyPref = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1692", ["msFRS-Topology-Pref"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsFVEKeyPackage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1999", ["msFVE-KeyPackage"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsFVERecoveryGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1965", ["msFVE-RecoveryGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsFVERecoveryPassword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1964", ["msFVE-RecoveryPassword"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsFVEVolumeGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1998", ["msFVE-VolumeGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Msieee80211Data = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1821", ["msieee80211-Data"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Msieee80211DataType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1822", ["msieee80211-DataType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Msieee80211ID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1823", ["msieee80211-ID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsIISFTPDir = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1786", ["msIIS-FTPDir"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsIISFTPRoot = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1785", ["msIIS-FTPRoot"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsImagingHashAlgorithm = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2181", ["msImaging-HashAlgorithm"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsImagingPSPIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2053", ["msImaging-PSPIdentifier"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsImagingPSPString = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2054", ["msImaging-PSPString"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsImagingThumbprintHash = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2180", ["msImaging-ThumbprintHash"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsKdsCreateTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2179", ["msKds-CreateTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsKdsDomainID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2177", ["msKds-DomainID"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsKdsKDFAlgorithmID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2169", ["msKds-KDFAlgorithmID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsKdsKDFParam = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2170", ["msKds-KDFParam"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsKdsPrivateKeyLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2174", ["msKds-PrivateKeyLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsKdsPublicKeyLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2173", ["msKds-PublicKeyLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsKdsRootKeyData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2175", ["msKds-RootKeyData"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsKdsSecretAgreementAlgorithmID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2171", ["msKds-SecretAgreementAlgorithmID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsKdsSecretAgreementParam = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2172", ["msKds-SecretAgreementParam"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsKdsUseStartTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2178", ["msKds-UseStartTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsKdsVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2176", ["msKds-Version"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Msnetieee80211GPPolicyData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1952", ["ms-net-ieee-80211-GP-PolicyData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Msnetieee80211GPPolicyGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1951", ["ms-net-ieee-80211-GP-PolicyGUID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Msnetieee80211GPPolicyReserved = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1953", ["ms-net-ieee-80211-GP-PolicyReserved"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Msnetieee8023GPPolicyData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1955", ["ms-net-ieee-8023-GP-PolicyData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Msnetieee8023GPPolicyGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1954", ["ms-net-ieee-8023-GP-PolicyGUID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Msnetieee8023GPPolicyReserved = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1956", ["ms-net-ieee-8023-GP-PolicyReserved"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsPKIAccountCredentials = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1894", ["msPKIAccountCredentials"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsPKICertTemplateOID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1436", ["msPKI-Cert-Template-OID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKICertificateApplicationPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1674", ["msPKI-Certificate-Application-Policy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKICertificateNameFlag = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1432", ["msPKI-Certificate-Name-Flag"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKICertificatePolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1439", ["msPKI-Certificate-Policy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKICredentialRoamingTokens = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2050", ["msPKI-CredentialRoamingTokens"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsPKIDPAPIMasterKeys = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1893", ["msPKIDPAPIMasterKeys"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription MsPKIEnrollmentFlag = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1430", ["msPKI-Enrollment-Flag"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKIEnrollmentServers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2076", ["msPKI-Enrollment-Servers"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIMinimalKeySize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1433", ["msPKI-Minimal-Key-Size"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKIOIDAttribute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1671", ["msPKI-OID-Attribute"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKIOIDCPS = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1672", ["msPKI-OID-CPS"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIOIDLocalizedName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1712", ["msPKI-OIDLocalizedName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIOIDUserNotice = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1673", ["msPKI-OID-User-Notice"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIPrivateKeyFlag = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1431", ["msPKI-Private-Key-Flag"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKIRAApplicationPolicies = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1675", ["msPKI-RA-Application-Policies"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIRAPolicies = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1438", ["msPKI-RA-Policies"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKIRASignature = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1429", ["msPKI-RA-Signature"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKIRoamingTimeStamp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1892", ["msPKIRoamingTimeStamp"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsPKISiteName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2077", ["msPKI-Site-Name"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKISupersedeTemplates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1437", ["msPKI-Supersede-Templates"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsPKITemplateMinorRevision = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1435", ["msPKI-Template-Minor-Revision"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsPKITemplateSchemaVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1434", ["msPKI-Template-Schema-Version"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsRADIUSFramedInterfaceId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1913", ["msRADIUS-FramedInterfaceId"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSFramedIpv6Prefix = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1915", ["msRADIUS-FramedIpv6Prefix"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSFramedIpv6Route = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1917", ["msRADIUS-FramedIpv6Route"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSSavedFramedInterfaceId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1914", ["msRADIUS-SavedFramedInterfaceId"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSSavedFramedIpv6Prefix = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1916", ["msRADIUS-SavedFramedIpv6Prefix"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSSavedFramedIpv6Route = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1918", ["msRADIUS-SavedFramedIpv6Route"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRRASAttribute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.884", ["msRRASAttribute"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsRRASVendorAttributeEntry = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.883", ["msRRASVendorAttributeEntry"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSPPConfigLicense = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2087", ["msSPP-ConfigLicense"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsSPPConfirmationId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2084", ["msSPP-ConfirmationId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSPPCSVLKPartialProductKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2106", ["msSPP-CSVLKPartialProductKey"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSPPCSVLKPid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2105", ["msSPP-CSVLKPid"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSPPCSVLKSkuId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2081", ["msSPP-CSVLKSkuId"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsSPPInstallationId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2083", ["msSPP-InstallationId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSPPIssuanceLicense = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2088", ["msSPP-IssuanceLicense"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsSPPKMSIds = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2082", ["msSPP-KMSIds"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsSPPOnlineLicense = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2085", ["msSPP-OnlineLicense"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsSPPPhoneLicense = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2086", ["msSPP-PhoneLicense"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSSQLAlias = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1395", ["mS-SQL-Alias"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLAllowAnonymousSubscription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1394", ["mS-SQL-AllowAnonymousSubscription"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLAllowImmediateUpdatingSubscription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1404", ["mS-SQL-AllowImmediateUpdatingSubscription"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLAllowKnownPullSubscription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1403", ["mS-SQL-AllowKnownPullSubscription"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLAllowQueuedUpdatingSubscription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1405", ["mS-SQL-AllowQueuedUpdatingSubscription"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLAllowSnapshotFilesFTPDownloading = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1406", ["mS-SQL-AllowSnapshotFilesFTPDownloading"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLAppleTalk = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1378", ["mS-SQL-AppleTalk"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLApplications = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1400", ["mS-SQL-Applications"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLBuild = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1368", ["mS-SQL-Build"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSSQLCharacterSet = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1370", ["mS-SQL-CharacterSet"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSSQLClustered = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1373", ["mS-SQL-Clustered"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLConnectionURL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1383", ["mS-SQL-ConnectionURL"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLContact = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1365", ["mS-SQL-Contact"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLCreationDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1397", ["mS-SQL-CreationDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLDatabase = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1393", ["mS-SQL-Database"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1390", ["mS-SQL-Description"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLGPSHeight = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1387", ["mS-SQL-GPSHeight"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLGPSLatitude = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1385", ["mS-SQL-GPSLatitude"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLGPSLongitude = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1386", ["mS-SQL-GPSLongitude"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLInformationDirectory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1392", ["mS-SQL-InformationDirectory"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLInformationURL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1382", ["mS-SQL-InformationURL"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLKeywords = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1401", ["mS-SQL-Keywords"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1389", ["mS-SQL-Language"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLLastBackupDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1398", ["mS-SQL-LastBackupDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLLastDiagnosticDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1399", ["mS-SQL-LastDiagnosticDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLLastUpdatedDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1381", ["mS-SQL-LastUpdatedDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1366", ["mS-SQL-Location"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLMemory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1367", ["mS-SQL-Memory"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MSSQLMultiProtocol = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1375", ["mS-SQL-MultiProtocol"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1363", ["mS-SQL-Name"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLNamedPipe = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1374", ["mS-SQL-NamedPipe"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLPublicationURL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1384", ["mS-SQL-PublicationURL"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLPublisher = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1402", ["mS-SQL-Publisher"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLRegisteredOwner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1364", ["mS-SQL-RegisteredOwner"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLServiceAccount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1369", ["mS-SQL-ServiceAccount"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1396", ["mS-SQL-Size"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MSSQLSortOrder = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1371", ["mS-SQL-SortOrder"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLSPX = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1376", ["mS-SQL-SPX"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLStatus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1380", ["mS-SQL-Status"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MSSQLTCPIP = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1377", ["mS-SQL-TCPIP"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLThirdParty = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1407", ["mS-SQL-ThirdParty"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSSQLType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1391", ["mS-SQL-Type"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLUnicodeSortOrder = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1372", ["mS-SQL-UnicodeSortOrder"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSSQLVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1388", ["mS-SQL-Version"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSSQLVines = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1379", ["mS-SQL-Vines"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTAPIConferenceBlob = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1700", ["msTAPI-ConferenceBlob"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsTAPIIpAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1701", ["msTAPI-IpAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTAPIProtocolId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1699", ["msTAPI-ProtocolId"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTAPIuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1698", ["msTAPI-uid"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTPMOwnerInformationTemp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2108", ["msTPM-OwnerInformationTemp"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTPMOwnerInformation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1966", ["msTPM-OwnerInformation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTPMSrkPubThumbprint = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2107", ["msTPM-SrkPubThumbprint"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsTPMTpmInformationForComputer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2109", ["msTPM-TpmInformationForComputer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTPMTpmInformationForComputerBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2110", ["msTPM-TpmInformationForComputerBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTSAllowLogon = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1979", ["msTSAllowLogon"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSBrokenConnectionAction = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1985", ["msTSBrokenConnectionAction"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSConnectClientDrives = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1986", ["msTSConnectClientDrives"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSConnectPrinterDrives = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1987", ["msTSConnectPrinterDrives"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSDefaultToMainPrinter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1988", ["msTSDefaultToMainPrinter"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSEndpointData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2070", ["msTSEndpointData"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSEndpointPlugin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2072", ["msTSEndpointPlugin"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSEndpointType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2071", ["msTSEndpointType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsTSExpireDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1993", ["msTSExpireDate"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsTSExpireDate2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2000", ["msTSExpireDate2"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsTSExpireDate3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2003", ["msTSExpireDate3"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsTSExpireDate4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2006", ["msTSExpireDate4"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription MsTSHomeDirectory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1977", ["msTSHomeDirectory"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSHomeDrive = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1978", ["msTSHomeDrive"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSInitialProgram = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1990", ["msTSInitialProgram"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLicenseVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1994", ["msTSLicenseVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLicenseVersion2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2001", ["msTSLicenseVersion2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLicenseVersion3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2004", ["msTSLicenseVersion3"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLicenseVersion4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2007", ["msTSLicenseVersion4"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSManagingLS = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1995", ["msTSManagingLS"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSManagingLS2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2002", ["msTSManagingLS2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSManagingLS3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2005", ["msTSManagingLS3"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSManagingLS4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2008", ["msTSManagingLS4"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSMaxConnectionTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1982", ["msTSMaxConnectionTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsTSMaxDisconnectionTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1981", ["msTSMaxDisconnectionTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsTSMaxIdleTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1983", ["msTSMaxIdleTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsTSPrimaryDesktop = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2073", ["msTSPrimaryDesktop"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTSPrimaryDesktopBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2074", ["msTSPrimaryDesktopBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTSProfilePath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1976", ["msTSProfilePath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSProperty01 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1991", ["msTSProperty01"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSProperty02 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1992", ["msTSProperty02"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSReconnectionAction = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1984", ["msTSReconnectionAction"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsTSRemoteControl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1980", ["msTSRemoteControl"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsTSSecondaryDesktopBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2078", ["msTSSecondaryDesktopBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTSSecondaryDesktops = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2075", ["msTSSecondaryDesktops"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsTSWorkDirectory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1989", ["msTSWorkDirectory"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLSProperty01 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2009", ["msTSLSProperty01"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsTSLSProperty02 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2010", ["msTSLSProperty02"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIAuthor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1623", ["msWMI-Author"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIChangeDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1624", ["msWMI-ChangeDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1676", ["msWMI-Class"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIClassDefinition = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1625", ["msWMI-ClassDefinition"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMICreationDate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1626", ["msWMI-CreationDate"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIGenus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1677", ["msWMI-Genus"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1627", ["msWMI-ID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIInt8Default = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1632", ["msWMI-Int8Default"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsWMIInt8Max = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1633", ["msWMI-Int8Max"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsWMIInt8Min = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1634", ["msWMI-Int8Min"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsWMIInt8ValidValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1635", ["msWMI-Int8ValidValues"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription MsWMIIntDefault = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1628", ["msWMI-IntDefault"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIintFlags1 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1678", ["msWMI-intFlags1"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIintFlags2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1679", ["msWMI-intFlags2"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIintFlags3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1680", ["msWMI-intFlags3"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIintFlags4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1681", ["msWMI-intFlags4"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIIntMax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1629", ["msWMI-IntMax"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIIntMin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1630", ["msWMI-IntMin"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIIntValidValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1631", ["msWMI-IntValidValues"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsWMIMof = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1638", ["msWMI-Mof"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1639", ["msWMI-Name"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMINormalizedClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1640", ["msWMI-NormalizedClass"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIParm1 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1682", ["msWMI-Parm1"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIParm2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1683", ["msWMI-Parm2"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIParm3 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1684", ["msWMI-Parm3"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIParm4 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1685", ["msWMI-Parm4"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIPropertyName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1641", ["msWMI-PropertyName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIQuery = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1642", ["msWMI-Query"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIQueryLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1643", ["msWMI-QueryLanguage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIScopeGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1686", ["msWMI-ScopeGuid"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMISourceOrganization = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1644", ["msWMI-SourceOrganization"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIStringDefault = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1636", ["msWMI-StringDefault"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMIStringValidValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1637", ["msWMI-StringValidValues"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMITargetClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1645", ["msWMI-TargetClass"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMITargetNameSpace = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1646", ["msWMI-TargetNameSpace"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMITargetObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1647", ["msWMI-TargetObject"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsWMITargetPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1648", ["msWMI-TargetPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsWMITargetType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1649", ["msWMI-TargetType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MscopeId = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.716", ["mscopeId"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription MsiFileList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.671", ["msiFileList"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsiScript = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.814", ["msiScript"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MsiScriptName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.845", ["msiScriptName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsiScriptPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.15", ["msiScriptPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsiScriptSize = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.846", ["msiScriptSize"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQAuthenticate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.923", ["mSMQAuthenticate"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQBasePriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.920", ["mSMQBasePriority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQComputerType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.933", ["mSMQComputerType"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription MSMQComputerTypeEx = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1417", ["mSMQComputerTypeEx"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQCost = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.946", ["mSMQCost"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQCSPName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.940", ["mSMQCSPName"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription MSMQDependentClientService = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1239", ["mSMQDependentClientService"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQDependentClientServices = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1226", ["mSMQDependentClientServices"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQDigests = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.948", ["mSMQDigests"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQDigestsMig = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.966", ["mSMQDigestsMig"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQDsService = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1238", ["mSMQDsService"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQDsServices = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1228", ["mSMQDsServices"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQEncryptKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.936", ["mSMQEncryptKey"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQForeign = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.934", ["mSMQForeign"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQInRoutingServers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.929", ["mSMQInRoutingServers"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQInterval1 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1308", ["mSMQInterval1"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQInterval2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1309", ["mSMQInterval2"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQJournal = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.918", ["mSMQJournal"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQJournalQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.921", ["mSMQJournalQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQLabel = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.922", ["mSMQLabel"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription MSMQLabelEx = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1415", ["mSMQLabelEx"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQLongLived = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.941", ["mSMQLongLived"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQMigrated = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.952", ["mSMQMigrated"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQMulticastAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1714", ["MSMQ-MulticastAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQNameStyle = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.939", ["mSMQNameStyle"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQNt4Flags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.964", ["mSMQNt4Flags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQNt4Stub = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.960", ["mSMQNt4Stub"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQOSType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.935", ["mSMQOSType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQOutRoutingServers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.928", ["mSMQOutRoutingServers"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQOwnerID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.925", ["mSMQOwnerID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQPrevSiteGates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1225", ["mSMQPrevSiteGates"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQPrivacyLevel = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.924", ["mSMQPrivacyLevel"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription MSMQQMID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.951", ["mSMQQMID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQQueueJournalQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.963", ["mSMQQueueJournalQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQQueueNameExt = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1243", ["mSMQQueueNameExt"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQQueueQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.962", ["mSMQQueueQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQQueueType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.917", ["mSMQQueueType"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQQuota = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.919", ["mSMQQuota"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsMQRecipientFormatName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1695", ["msMQ-Recipient-FormatName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQRoutingService = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1237", ["mSMQRoutingService"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQRoutingServices = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1227", ["mSMQRoutingServices"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQSecuredSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1713", ["MSMQ-SecuredSource"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQServiceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.930", ["mSMQServiceType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQServices = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.950", ["mSMQServices"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MSMQSignCertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.947", ["mSMQSignCertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQSignCertificatesMig = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.967", ["mSMQSignCertificatesMig"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQSignKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.937", ["mSMQSignKey"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQSite1 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.943", ["mSMQSite1"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQSite2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.944", ["mSMQSite2"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQSiteForeign = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.961", ["mSMQSiteForeign"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQSiteGates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.945", ["mSMQSiteGates"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQSiteGatesMig = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1310", ["mSMQSiteGatesMig"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MSMQSiteID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.953", ["mSMQSiteID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQSiteName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.965", ["mSMQSiteName"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription MSMQSiteNameEx = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1416", ["mSMQSiteNameEx"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MSMQSites = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.927", ["mSMQSites"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQTransactional = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.926", ["mSMQTransactional"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MSMQUserSid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1337", ["mSMQUserSid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription MSMQVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.942", ["mSMQVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsNPAllowDialin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1119", ["msNPAllowDialin"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription MsNPCalledStationID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1123", ["msNPCalledStationID"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsNPCallingStationID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1124", ["msNPCallingStationID"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsNPSavedCallingStationID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1130", ["msNPSavedCallingStationID"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSCallbackNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1145", ["msRADIUSCallbackNumber"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSFramedIPAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1153", ["msRADIUSFramedIPAddress"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsRADIUSFramedRoute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1158", ["msRADIUSFramedRoute"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRADIUSServiceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1171", ["msRADIUSServiceType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsRASSavedCallbackNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1189", ["msRASSavedCallbackNumber"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsRASSavedFramedIPAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1190", ["msRASSavedFramedIPAddress"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsRASSavedFramedRoute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1191", ["msRASSavedFramedRoute"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30Aliases = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.323", ["msSFU30Aliases"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30CryptMethod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.352", ["msSFU30CryptMethod"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30Domains = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.340", ["msSFU30Domains"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30FieldSeparator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.302", ["msSFU30FieldSeparator"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30IntraFieldSeparator = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.303", ["msSFU30IntraFieldSeparator"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30IsValidContainer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.350", ["msSFU30IsValidContainer"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsSFU30KeyAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.301", ["msSFU30KeyAttributes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30KeyValues = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.324", ["msSFU30KeyValues"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30MapFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.306", ["msSFU30MapFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30MasterServerName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.307", ["msSFU30MasterServerName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30MaxGidNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.342", ["msSFU30MaxGidNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsSFU30MaxUidNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.343", ["msSFU30MaxUidNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription MsSFU30Name = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.309", ["msSFU30Name"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30NetgroupHostAtDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.348", ["msSFU30NetgroupHostAtDomain"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30NetgroupUserAtDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.349", ["msSFU30NetgroupUserAtDomain"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30NisDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.339", ["msSFU30NisDomain"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30NSMAPFieldPosition = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.345", ["msSFU30NSMAPFieldPosition"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MsSFU30OrderNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.308", ["msSFU30OrderNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30PosixMember = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.346", ["msSFU30PosixMember"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsSFU30PosixMemberOf = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.347", ["msSFU30PosixMemberOf"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MsSFU30ResultAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.305", ["msSFU30ResultAttributes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30SearchAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.304", ["msSFU30SearchAttributes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30SearchContainer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.6.18.1.300", ["msSFU30SearchContainer"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MsSFU30YpServers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.6.18.1.341", ["msSFU30YpServers"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription MustContain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.24", ["mustContain"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription NameServiceFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.753", ["nameServiceFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription NCName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.16", ["nCName"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NETBIOSName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.87", ["nETBIOSName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootAllowNewClients = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.849", ["netbootAllowNewClients"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription NetbootAnswerOnlyValidClients = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.854", ["netbootAnswerOnlyValidClients"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription NetbootAnswerRequests = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.853", ["netbootAnswerRequests"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription NetbootCurrentClientCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.852", ["netbootCurrentClientCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription NetbootDUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.2234", ["netbootDUID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription NetbootGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.359", ["netbootGUID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription NetbootInitialization = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.358", ["netbootInitialization"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootIntelliMirrorOSes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.857", ["netbootIntelliMirrorOSes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootLimitClients = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.850", ["netbootLimitClients"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription NetbootLocallyInstalledOSes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.859", ["netbootLocallyInstalledOSes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootMachineFilePath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.361", ["netbootMachineFilePath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootMaxClients = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.851", ["netbootMaxClients"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription NetbootMirrorDataFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1241", ["netbootMirrorDataFile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootNewMachineNamingPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.855", ["netbootNewMachineNamingPolicy"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootNewMachineOU = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.856", ["netbootNewMachineOU"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NetbootSCPBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.864", ["netbootSCPBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NetbootServer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.860", ["netbootServer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NetbootSIFFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1240", ["netbootSIFFile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetbootTools = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.858", ["netbootTools"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription NetworkAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.459", ["networkAddress"], syntax: AdSyntaxes.StringTeletex);
		public readonly static AttributeTypeDescription NextLevelStore = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.214", ["nextLevelStore"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NextRid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.88", ["nextRid"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription NisMapEntry = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.27", ["nisMapEntry"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription NisMapName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.26", ["nisMapName"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription NisNetgroupTriple = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.3.6.1.1.1.1.14", ["nisNetgroupTriple"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription NonSecurityMember = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.530", ["nonSecurityMember"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NonSecurityMemberBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.531", ["nonSecurityMemberBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NotificationList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.303", ["notificationList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription NTGroupMembers = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.89", ["nTGroupMembers"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription NTMixedDomain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.357", ["nTMixedDomain"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription NtPwdHistory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.94", ["ntPwdHistory"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription NTSecurityDescriptor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.281", ["nTSecurityDescriptor"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription DistinguishedName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.49", ["distinguishedName"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ObjectCategory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.782", ["objectCategory"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ObjectClassCategory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.370", ["objectClassCategory"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription ObjectClasses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.21.6", ["objectClasses"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ObjectCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.506", ["objectCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ObjectSid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.146", ["objectSid"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription ObjectVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.76", ["objectVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription OEMInformation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.151", ["oEMInformation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OMObjectClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.218", ["oMObjectClass"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription OMSyntax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.231", ["oMSyntax"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription OMTGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.505", ["oMTGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription OMTIndxGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.333", ["oMTIndxGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription OncRpcNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.18", ["oncRpcNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription OperatingSystem = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.363", ["operatingSystem"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OperatingSystemHotfix = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.415", ["operatingSystemHotfix"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OperatingSystemServicePack = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.365", ["operatingSystemServicePack"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OperatingSystemVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.364", ["operatingSystemVersion"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OperatorCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.144", ["operatorCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription OptionDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.712", ["optionDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Options = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.307", ["options"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription OptionsLocation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.713", ["optionsLocation"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription O = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.10", ["o"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Ou = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.11", ["ou"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OrganizationalStatus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.45", ["organizationalStatus"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OriginalDisplayTable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.445", ["originalDisplayTable"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription OriginalDisplayTableMSDOS = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.214", ["originalDisplayTableMSDOS"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription OtherLoginWorkstations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.91", ["otherLoginWorkstations"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherMailbox = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.651", ["otherMailbox"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription MiddleName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.16.840.1.113730.3.1.34", ["middleName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherWellKnownObjects = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1359", ["otherWellKnownObjects"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription Owner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.32", ["owner"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription PackageFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.327", ["packageFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PackageName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.326", ["packageName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PackageType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.324", ["packageType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ParentCA = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.557", ["parentCA"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ParentCACertificateChain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.685", ["parentCACertificateChain"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ParentGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1224", ["parentGUID"], syntax: AdSyntaxes.StringOctetGuid);
		public readonly static AttributeTypeDescription PartialAttributeDeletionList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.663", ["partialAttributeDeletionList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PartialAttributeSet = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.640", ["partialAttributeSet"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PekKeyChangeInterval = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.866", ["pekKeyChangeInterval"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription PekList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.865", ["pekList"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PendingCACertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.693", ["pendingCACertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PendingParentCA = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.695", ["pendingParentCA"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription PerMsgDialogDisplayTable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.325", ["perMsgDialogDisplayTable"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PerRecipDialogDisplayTable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.326", ["perRecipDialogDisplayTable"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PersonalTitle = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.615", ["personalTitle"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherFacsimileTelephoneNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.646", ["otherFacsimileTelephoneNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherHomePhone = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.277", ["otherHomePhone"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription HomePhone = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.20", ["homePhone"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherIpPhone = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.722", ["otherIpPhone"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription IpPhone = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.721", ["ipPhone"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrimaryInternationalISDNNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.649", ["primaryInternationalISDNNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherMobile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.647", ["otherMobile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Mobile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.41", ["mobile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherTelephone = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.18", ["otherTelephone"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription OtherPager = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.118", ["otherPager"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Pager = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.42", ["pager"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Photo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.7", ["photo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PhysicalDeliveryOfficeName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.19", ["physicalDeliveryOfficeName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PhysicalLocationObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.514", ["physicalLocationObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ThumbnailPhoto = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.16.840.1.113730.3.1.35", ["thumbnailPhoto"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PKICriticalExtensions = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1330", ["pKICriticalExtensions"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PKIDefaultCSPs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1334", ["pKIDefaultCSPs"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PKIDefaultKeySpec = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1327", ["pKIDefaultKeySpec"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PKIEnrollmentAccess = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1335", ["pKIEnrollmentAccess"], syntax: AdSyntaxes.StringNtSecDesc);
		public readonly static AttributeTypeDescription PKIExpirationPeriod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1331", ["pKIExpirationPeriod"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PKIExtendedKeyUsage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1333", ["pKIExtendedKeyUsage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PKIKeyUsage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1328", ["pKIKeyUsage"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PKIMaxIssuingDepth = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1329", ["pKIMaxIssuingDepth"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PKIOverlapPeriod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1332", ["pKIOverlapPeriod"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PKT = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.206", ["pKT"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PKTGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.205", ["pKTGuid"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PolicyReplicationFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.633", ["policyReplicationFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PortName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.228", ["portName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PossSuperiors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.8", ["possSuperiors"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription PossibleInferiors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.915", ["possibleInferiors"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription PostOfficeBox = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.18", ["postOfficeBox"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PostalAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.16", ["postalAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PostalCode = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.17", ["postalCode"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PreferredDeliveryMethod = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.28", ["preferredDeliveryMethod"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription PreferredOU = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.97", ["preferredOU"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription PreferredLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.16.840.1.113730.3.1.39", ["preferredLanguage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrefixMap = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.538", ["prefixMap"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PresentationAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.29", ["presentationAddress"], syntax: AdSyntaxes.ObjectPresentationAddress);
		public readonly static AttributeTypeDescription PreviousCACertificates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.692", ["previousCACertificates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PreviousParentCA = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.694", ["previousParentCA"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription PrimaryGroupID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.98", ["primaryGroupID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrimaryGroupToken = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1412", ["primaryGroupToken"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.247", ["printAttributes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintBinNames = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.237", ["printBinNames"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintCollate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.242", ["printCollate"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription PrintColor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.243", ["printColor"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription PrintDuplexSupported = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1311", ["printDuplexSupported"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription PrintEndTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.234", ["printEndTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintFormName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.235", ["printFormName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintKeepPrintedJobs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.275", ["printKeepPrintedJobs"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription PrintLanguage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.246", ["printLanguage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintMACAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.288", ["printMACAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintMaxCopies = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.241", ["printMaxCopies"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMaxResolutionSupported = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.238", ["printMaxResolutionSupported"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMaxXExtent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.277", ["printMaxXExtent"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMaxYExtent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.278", ["printMaxYExtent"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMediaReady = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.289", ["printMediaReady"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintMediaSupported = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.299", ["printMediaSupported"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintMemory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.282", ["printMemory"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMinXExtent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.279", ["printMinXExtent"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintMinYExtent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.280", ["printMinYExtent"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintNetworkAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.287", ["printNetworkAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintNotify = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.272", ["printNotify"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintNumberUp = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.290", ["printNumberUp"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintOrientationsSupported = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.240", ["printOrientationsSupported"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintOwner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.271", ["printOwner"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintPagesPerMinute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.631", ["printPagesPerMinute"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintRate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.285", ["printRate"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintRateUnit = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.286", ["printRateUnit"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintSeparatorFile = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.230", ["printSeparatorFile"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintShareName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.270", ["printShareName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintSpooling = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.274", ["printSpooling"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrintStaplingSupported = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.281", ["printStaplingSupported"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription PrintStartTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.233", ["printStartTime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrintStatus = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.273", ["printStatus"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrinterName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.300", ["printerName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PriorSetTime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.99", ["priorSetTime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription PriorValue = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.100", ["priorValue"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Priority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.231", ["priority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrivateKey = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.101", ["privateKey"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PrivilegeAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.636", ["privilegeAttributes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PrivilegeDisplayName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.634", ["privilegeDisplayName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PrivilegeHolder = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.637", ["privilegeHolder"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription PrivilegeValue = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.635", ["privilegeValue"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ProductCode = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.818", ["productCode"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ProfilePath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.139", ["profilePath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ProxiedObjectName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1249", ["proxiedObjectName"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription ProxyAddresses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.210", ["proxyAddresses"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ProxyGenerationEnabled = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.523", ["proxyGenerationEnabled"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription ProxyLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.103", ["proxyLifetime"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription PublicKeyPolicy = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.420", ["publicKeyPolicy"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PurportedSearch = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.886", ["purportedSearch"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription PwdHistoryLength = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.95", ["pwdHistoryLength"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription PwdProperties = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.93", ["pwdProperties"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription QualityOfService = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.458", ["qualityOfService"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription QueryFilter = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1355", ["queryFilter"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription QueryPolicyBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.608", ["queryPolicyBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription QueryPolicyObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.607", ["queryPolicyObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription QueryPoint = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.680", ["queryPoint"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RangeLower = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.34", ["rangeLower"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RangeUpper = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.35", ["rangeUpper"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Name = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1", ["name"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RDNAttID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.26", ["rDNAttID"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription RegisteredAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.26", ["registeredAddress"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription RemoteServerName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.105", ["remoteServerName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RemoteSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.107", ["remoteSource"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RemoteSourceType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.108", ["remoteSourceType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RemoteStorageGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.809", ["remoteStorageGUID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ReplInterval = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1336", ["replInterval"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ReplPropertyMetaData = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.3", ["replPropertyMetaData"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ReplTopologyStayOfExecution = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.677", ["replTopologyStayOfExecution"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ReplUpToDateVector = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.4", ["replUpToDateVector"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ReplicaSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.109", ["replicaSource"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription DirectReports = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.436", ["directReports"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription RepsFrom = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.91", ["repsFrom"], syntax: AdSyntaxes.ObjectReplicaLink);
		public readonly static AttributeTypeDescription RepsTo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.83", ["repsTo"], syntax: AdSyntaxes.ObjectReplicaLink);
		public readonly static AttributeTypeDescription RequiredCategories = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.321", ["requiredCategories"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription RetiredReplDSASignatures = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.673", ["retiredReplDSASignatures"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Revision = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.145", ["revision"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Rid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.153", ["rid"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RIDAllocationPool = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.371", ["rIDAllocationPool"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription RIDAvailablePool = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.370", ["rIDAvailablePool"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription RIDManagerReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.368", ["rIDManagerReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription RIDNextRID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.374", ["rIDNextRID"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RIDPreviousAllocationPool = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.372", ["rIDPreviousAllocationPool"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription RIDSetReferences = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.669", ["rIDSetReferences"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription RIDUsedPool = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.373", ["rIDUsedPool"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription RightsGuid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.340", ["rightsGuid"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RoleOccupant = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.33", ["roleOccupant"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription RoomNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.6", ["roomNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RootTrust = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.674", ["rootTrust"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription RpcNsAnnotation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.366", ["rpcNsAnnotation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsBindings = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.113", ["rpcNsBindings"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsCodeset = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.367", ["rpcNsCodeset"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsEntryFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.754", ["rpcNsEntryFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RpcNsGroup = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.114", ["rpcNsGroup"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsInterfaceID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.115", ["rpcNsInterfaceID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsObjectID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.312", ["rpcNsObjectID"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsPriority = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.117", ["rpcNsPriority"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription RpcNsProfileEntry = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.118", ["rpcNsProfileEntry"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription RpcNsTransferSyntax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.314", ["rpcNsTransferSyntax"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SAMAccountName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.221", ["sAMAccountName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SAMAccountType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.302", ["sAMAccountType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SamDomainUpdates = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1969", ["samDomainUpdates"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Schedule = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.211", ["schedule"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription SchemaFlagsEx = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.120", ["schemaFlagsEx"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SchemaInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1358", ["schemaInfo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription SchemaUpdate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.481", ["schemaUpdate"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription SchemaVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.471", ["schemaVersion"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ScopeFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1354", ["scopeFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ScriptPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.62", ["scriptPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SDRightsEffective = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1304", ["sDRightsEffective"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SearchFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.334", ["searchFlags"], syntax: AdSyntaxes.Enumeration);
		public readonly static AttributeTypeDescription SearchGuide = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.14", ["searchGuide"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Secretary = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.21", ["secretary"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SecurityIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.121", ["securityIdentifier"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription SeeAlso = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.34", ["seeAlso"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SeqNotification = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.504", ["seqNotification"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SerialNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.5", ["serialNumber"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription ServerName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.223", ["serverName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ServerReference = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.515", ["serverReference"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ServerReferenceBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.516", ["serverReferenceBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ServerRole = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.157", ["serverRole"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ServerState = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.154", ["serverState"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ServiceBindingInformation = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.510", ["serviceBindingInformation"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ServiceClassID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.122", ["serviceClassID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ServiceClassInfo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.123", ["serviceClassInfo"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ServiceClassName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.509", ["serviceClassName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ServiceDNSName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.657", ["serviceDNSName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ServiceDNSNameType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.659", ["serviceDNSNameType"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ServiceInstanceVersion = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.199", ["serviceInstanceVersion"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription ServicePrincipalName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.771", ["servicePrincipalName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SetupCommand = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.325", ["setupCommand"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ShadowExpire = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.10", ["shadowExpire"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowFlag = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.11", ["shadowFlag"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowInactive = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.9", ["shadowInactive"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowLastChange = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.5", ["shadowLastChange"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowMax = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.7", ["shadowMax"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowMin = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.6", ["shadowMin"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShadowWarning = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.8", ["shadowWarning"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription ShellContextMenu = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.615", ["shellContextMenu"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ShellPropertyPages = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.563", ["shellPropertyPages"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ShortServerName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1209", ["shortServerName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription ShowInAddressBook = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.644", ["showInAddressBook"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription ShowInAdvancedViewOnly = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.169", ["showInAdvancedViewOnly"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription SIDHistory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.609", ["sIDHistory"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription SignatureAlgorithms = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.824", ["signatureAlgorithms"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SiteGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.362", ["siteGUID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription SiteLinkList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.822", ["siteLinkList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SiteList = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.821", ["siteList"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SiteObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.512", ["siteObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SiteObjectBL = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.513", ["siteObjectBL"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SiteServer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.494", ["siteServer"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription MailAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.786", ["mailAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SPNMappings = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1347", ["sPNMappings"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription St = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.8", ["st"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Street = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.9", ["street"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription StructuralObjectClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.21.9", ["structuralObjectClass"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription SubClassOf = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.21", ["subClassOf"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription SubRefs = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.2.7", ["subRefs"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SubSchemaSubEntry = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.18.10", ["subSchemaSubEntry"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SuperScopeDescription = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.711", ["superScopeDescription"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SuperScopes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.710", ["superScopes"], syntax: AdSyntaxes.StringPrintable);
		public readonly static AttributeTypeDescription SuperiorDNSRoot = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.532", ["superiorDNSRoot"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SupplementalCredentials = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.125", ["supplementalCredentials"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription SupportedApplicationContext = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.30", ["supportedApplicationContext"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Sn = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.4", ["sn"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription SyncAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.666", ["syncAttributes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SyncMembership = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.665", ["syncMembership"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SyncWithObject = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.664", ["syncWithObject"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription SyncWithSID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.667", ["syncWithSID"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription SystemAuxiliaryClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.198", ["systemAuxiliaryClass"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription SystemFlags = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.375", ["systemFlags"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription SystemMayContain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.196", ["systemMayContain"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription SystemMustContain = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.197", ["systemMustContain"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription SystemOnly = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.170", ["systemOnly"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription SystemPossSuperiors = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.195", ["systemPossSuperiors"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription TelephoneNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.20", ["telephoneNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TeletexTerminalIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.22", ["teletexTerminalIdentifier"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription TelexNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.21", ["telexNumber"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription PrimaryTelexNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.648", ["primaryTelexNumber"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TemplateRoots = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1346", ["templateRoots"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription TemplateRoots2 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.2048", ["templateRoots2"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription TerminalServer = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.885", ["terminalServer"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Co = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.131", ["co"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TextEncodedORAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "0.9.2342.19200300.100.1.2", ["textEncodedORAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TimeRefresh = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.503", ["timeRefresh"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription TimeVolChange = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.502", ["timeVolChange"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription Title = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "2.5.4.12", ["title"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TokenGroups = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1301", ["tokenGroups"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription TokenGroupsGlobalAndUniversal = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1418", ["tokenGroupsGlobalAndUniversal"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription TokenGroupsNoGCAcceptable = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1303", ["tokenGroupsNoGCAcceptable"], syntax: AdSyntaxes.StringSid);
		public readonly static AttributeTypeDescription TombstoneLifetime = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.54", ["tombstoneLifetime"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription TransportAddressAttribute = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.895", ["transportAddressAttribute"], syntax: AdSyntaxes.StringObjectIdentifier);
		public readonly static AttributeTypeDescription TransportDLLName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.789", ["transportDLLName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TransportType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.791", ["transportType"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription TreatAsLeaf = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.806", ["treatAsLeaf"], syntax: AdSyntaxes.Boolean);
		public readonly static AttributeTypeDescription TreeName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.660", ["treeName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TrustAttributes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.470", ["trustAttributes"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription TrustAuthIncoming = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.129", ["trustAuthIncoming"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription TrustAuthOutgoing = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.135", ["trustAuthOutgoing"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription TrustDirection = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.132", ["trustDirection"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription TrustParent = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.471", ["trustParent"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription TrustPartner = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.133", ["trustPartner"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription TrustPosixOffset = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.134", ["trustPosixOffset"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription TrustType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.136", ["trustType"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription UASCompat = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.155", ["uASCompat"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Uid = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.1", ["uid"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UidNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.0", ["uidNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription UNCName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.137", ["uNCName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UnicodePwd = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.90", ["unicodePwd"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UniqueIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.44", ["uniqueIdentifier"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UniqueMember = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.50", ["uniqueMember"], syntax: AdSyntaxes.ObjectDsDn);
		public readonly static AttributeTypeDescription UnixHomeDirectory = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.3.6.1.1.1.1.3", ["unixHomeDirectory"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription UnixUserPassword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.1910", ["unixUserPassword"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UnstructuredAddress = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113549.1.9.8", ["unstructuredAddress"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UnstructuredName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113549.1.9.2", ["unstructuredName"], syntax: AdSyntaxes.StringIa5);
		public readonly static AttributeTypeDescription UpgradeProductCode = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.813", ["upgradeProductCode"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UPNSuffixes = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.890", ["uPNSuffixes"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserAccountControl = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.8", ["userAccountControl"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription UserCert = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.645", ["userCert"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription Comment = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.156", ["comment"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserParameters = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.138", ["userParameters"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserPassword = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.35", ["userPassword"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UserPrincipalName = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.656", ["userPrincipalName"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserSharedFolder = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.751", ["userSharedFolder"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserSharedFolderOther = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.752", ["userSharedFolderOther"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserSMIMECertificate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.16.840.1.113730.3.140", ["userSMIMECertificate"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UserWorkstations = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.86", ["userWorkstations"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserClass = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "0.9.2342.19200300.100.1.8", ["userClass"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription UserPKCS12 = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.16.840.1.113730.3.1.216", ["userPKCS12"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription USNChanged = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.120", ["uSNChanged"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription USNCreated = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.19", ["uSNCreated"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription USNDSALastObjRemoved = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.267", ["uSNDSALastObjRemoved"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription USNIntersite = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.469", ["USNIntersite"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription USNLastObjRem = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.121", ["uSNLastObjRem"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription USNSource = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.896", ["uSNSource"], syntax: AdSyntaxes.LargeInteger);
		public readonly static AttributeTypeDescription ValidAccesses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.1356", ["validAccesses"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription Vendor = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.255", ["vendor"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription VersionNumber = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.141", ["versionNumber"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription VersionNumberHi = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.328", ["versionNumberHi"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription VersionNumberLo = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.329", ["versionNumberLo"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription VolTableGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.336", ["volTableGUID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription VolTableIdxGUID = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.334", ["volTableIdxGUID"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription VolumeCount = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.4.507", ["volumeCount"], syntax: AdSyntaxes.Integer);
		public readonly static AttributeTypeDescription WbemPath = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.301", ["wbemPath"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription WellKnownObjects = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.618", ["wellKnownObjects"], syntax: AdSyntaxes.ObjectDnBinary);
		public readonly static AttributeTypeDescription WhenChanged = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.3", ["whenChanged"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription WhenCreated = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.2", ["whenCreated"], syntax: AdSyntaxes.StringGeneralizedTime);
		public readonly static AttributeTypeDescription WinsockAddresses = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.142", ["winsockAddresses"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription WWWHomePage = new AttributeTypeDescription(AttributeTypeDescriptionFlags.SingleValue, "1.2.840.113556.1.2.464", ["wWWHomePage"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription Url = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "1.2.840.113556.1.4.749", ["url"], syntax: AdSyntaxes.StringUnicode);
		public readonly static AttributeTypeDescription X121Address = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.24", ["x121Address"], syntax: AdSyntaxes.StringNumeric);
		public readonly static AttributeTypeDescription X500uniqueIdentifier = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.45", ["x500uniqueIdentifier"], syntax: AdSyntaxes.StringOctet);
		public readonly static AttributeTypeDescription UserCertificate = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, "2.5.4.36", ["userCertificate"], syntax: AdSyntaxes.StringOctet);
		#endregion
		#region All attributes
		private static readonly ImmutableArray<AttributeTypeDescription> allAttrs = [
			#region rootDSE (not included in schema)
			dsServiceName,
			namingContexts,
			defaultNamingContext,
			schemaNamingContext,
			configurationNamingContext,
			rootDomainNamingContext,
			supportedControl,
			supportedLDAPVersion,
			supportedLDAPPolicies,
			supportedSASLMechanisms,
			ldapServiceName,
			supportedCapabilities,
			OmObjectClass,
			domainFunctionality,
			domainControllerFunctionality,
			forestFunctionality,
			currentTime,
			isGlobalCatalogReady,
			msDSPrefixTable,
			configurableSettingsEffective,
			lDAPPoliciesEffective,
			msDSArenaInfo,
			dumpLdapNotifications,
			dsaVersionString,
			validFSMOs,

			ObjectClass,
			#endregion

			#region Win2025 Schema
			AccountExpires,
			AccountNameHistory,
			ACSAggregateTokenRatePerUser,
			ACSAllocableRSVPBandwidth,
			ACSCacheTimeout,
			ACSDirection,
			ACSDSBMDeadTime,
			ACSDSBMPriority,
			ACSDSBMRefresh,
			ACSEnableACSService,
			ACSEnableRSVPAccounting,
			ACSEnableRSVPMessageLogging,
			ACSEventLogLevel,
			ACSIdentityName,
			ACSMaxAggregatePeakRatePerUser,
			ACSMaxDurationPerFlow,
			ACSMaxNoOfAccountFiles,
			ACSMaxNoOfLogFiles,
			ACSMaxPeakBandwidth,
			ACSMaxPeakBandwidthPerFlow,
			ACSMaxSizeOfRSVPAccountFile,
			ACSMaxSizeOfRSVPLogFile,
			ACSMaxTokenBucketPerFlow,
			ACSMaxTokenRatePerFlow,
			ACSMaximumSDUSize,
			ACSMinimumDelayVariation,
			ACSMinimumLatency,
			ACSMinimumPolicedSize,
			ACSNonReservedMaxSDUSize,
			ACSNonReservedMinPolicedSize,
			ACSNonReservedPeakRate,
			ACSNonReservedTokenSize,
			ACSNonReservedTxLimit,
			ACSNonReservedTxSize,
			ACSPermissionBits,
			ACSPolicyName,
			ACSPriority,
			ACSRSVPAccountFilesLocation,
			ACSRSVPLogFilesLocation,
			ACSServerList,
			ACSServiceType,
			ACSTimeOfDay,
			ACSTotalNoOfFlows,
			Notes,
			AdditionalTrustedServiceNames,
			StreetAddress,
			AddressBookRoots,
			AddressBookRoots2,
			AddressEntryDisplayTable,
			AddressEntryDisplayTableMSDOS,
			HomePostalAddress,
			AddressSyntax,
			AddressType,
			AdminContextMenu,
			AdminCount,
			AdminDescription,
			AdminDisplayName,
			AdminMultiselectPropertyPages,
			AdminPropertyPages,
			AllowedAttributes,
			AllowedAttributesEffective,
			AllowedChildClasses,
			AllowedChildClassesEffective,
			AltSecurityIdentities,
			ANR,
			AppSchemaVersion,
			ApplicationName,
			AppliesTo,
			AssetNumber,
			Assistant,
			AssocNTAccount,
			AssociatedDomain,
			AssociatedName,
			AttributeDisplayNames,
			AttributeID,
			AttributeSecurityGUID,
			AttributeSyntax,
			AttributeTypes,
			AttributeCertificateAttribute,
			Audio,
			AuditingPolicy,
			AuthenticationOptions,
			AuthorityRevocationList,
			AuxiliaryClass,
			BadPasswordTime,
			BadPwdCount,
			BirthLocation,
			BootFile,
			BootParameter,
			BridgeheadServerListBL,
			BridgeheadTransportList,
			BuildingName,
			BuiltinCreationTime,
			BuiltinModifiedCount,
			BusinessCategory,
			BytesPerMinute,
			CACertificate,
			CACertificateDN,
			CAConnect,
			CAUsages,
			CAWEBURL,
			CanUpgradeScript,
			CanonicalName,
			CarLicense,
			Catalogs,
			Categories,
			CategoryId,
			CertificateAuthorityObject,
			CertificateRevocationList,
			CertificateTemplates,
			ClassDisplayName,
			CodePage,
			COMClassID,
			COMCLSID,
			COMInterfaceID,
			COMOtherProgId,
			COMProgID,
			COMTreatAsClassId,
			COMTypelibId,
			COMUniqueLIBID,
			Info,
			Cn,
			Company,
			ContentIndexingAllowed,
			ContextMenu,
			ControlAccessRights,
			Cost,
			CountryCode,
			C,
			CreateDialog,
			CreateTimeStamp,
			CreateWizardExt,
			CreationTime,
			CreationWizard,
			Creator,
			CRLObject,
			CRLPartitionedRevocationList,
			CrossCertificatePair,
			CurrMachineId,
			CurrentLocation,
			CurrentParentCA,
			CurrentValue,
			DBCSPwd,
			DefaultClassStore,
			DefaultGroup,
			DefaultHidingValue,
			DefaultLocalPolicyObject,
			DefaultObjectCategory,
			DefaultPriority,
			DefaultSecurityDescriptor,
			DeltaRevocationList,
			Department,
			DepartmentNumber,
			Description,
			DesktopProfile,
			DestinationIndicator,
			DhcpClasses,
			DhcpFlags,
			DhcpIdentification,
			DhcpMask,
			DhcpMaxKey,
			DhcpObjDescription,
			DhcpObjName,
			DhcpOptions,
			DhcpProperties,
			DhcpRanges,
			DhcpReservations,
			DhcpServers,
			DhcpSites,
			DhcpState,
			DhcpSubnets,
			DhcpType,
			DhcpUniqueKey,
			DhcpUpdateTime,
			DisplayName,
			DisplayNamePrintable,
			DITContentRules,
			Division,
			DMDLocation,
			DmdName,
			DNReferenceUpdate,
			DnsAllowDynamic,
			DnsAllowXFR,
			DNSHostName,
			DnsNotifySecondaries,
			DNSProperty,
			DnsRecord,
			DnsRoot,
			DnsSecureSecondaries,
			DNSTombstoned,
			DocumentAuthor,
			DocumentIdentifier,
			DocumentLocation,
			DocumentPublisher,
			DocumentTitle,
			DocumentVersion,
			DomainCAs,
			Dc,
			DomainCrossRef,
			DomainID,
			DomainIdentifier,
			DomainPolicyObject,
			DomainPolicyReference,
			DomainReplica,
			DomainWidePolicy,
			Drink,
			DriverName,
			DriverVersion,
			DSCorePropagationData,
			DSHeuristics,
			DSUIAdminMaximum,
			DSUIAdminNotification,
			DSUIShellMaximum,
			DSASignature,
			DynamicLDAPServer,
			Mail,
			EFSPolicy,
			EmployeeID,
			EmployeeNumber,
			EmployeeType,
			Enabled,
			EnabledConnection,
			EnrollmentProviders,
			EntryTTL,
			ExtendedAttributeInfo,
			ExtendedCharsAllowed,
			ExtendedClassInfo,
			ExtensionName,
			ExtraColumns,
			FacsimileTelephoneNumber,
			FileExtPriority,
			Flags,
			FlatName,
			ForceLogoff,
			ForeignIdentifier,
			FriendlyNames,
			FromEntry,
			FromServer,
			FrsComputerReference,
			FrsComputerReferenceBL,
			FRSControlDataCreation,
			FRSControlInboundBacklog,
			FRSControlOutboundBacklog,
			FRSDirectoryFilter,
			FRSDSPoll,
			FRSExtensions,
			FRSFaultCondition,
			FRSFileFilter,
			FRSFlags,
			FRSLevelLimit,
			FRSMemberReference,
			FRSMemberReferenceBL,
			FRSPartnerAuthLevel,
			FRSPrimaryMember,
			FRSReplicaSetGUID,
			FRSReplicaSetType,
			FRSRootPath,
			FRSRootSecurity,
			FRSServiceCommand,
			FRSServiceCommandStatus,
			FRSStagingPath,
			FRSTimeLastCommand,
			FRSTimeLastConfigChange,
			FRSUpdateTimeout,
			FRSVersion,
			FRSVersionGUID,
			FRSWorkingPath,
			FSMORoleOwner,
			GarbageCollPeriod,
			Gecos,
			GeneratedConnection,
			GenerationQualifier,
			GidNumber,
			GivenName,
			GlobalAddressList,
			GlobalAddressList2,
			GovernsID,
			GPLink,
			GPOptions,
			GPCFileSysPath,
			GPCFunctionalityVersion,
			GPCMachineExtensionNames,
			GPCUserExtensionNames,
			GPCWQLFilter,
			GroupAttributes,
			GroupMembershipSAM,
			GroupPriority,
			GroupType,
			GroupsToIgnore,
			HasMasterNCs,
			HasPartialReplicaNCs,
			HelpData16,
			HelpData32,
			HelpFileName,
			HideFromAB,
			HomeDirectory,
			HomeDrive,
			Host,
			HouseIdentifier,
			IconPath,
			ImplementedCategories,
			IndexedScopes,
			InitialAuthIncoming,
			InitialAuthOutgoing,
			Initials,
			InstallUiLevel,
			InstanceType,
			InterSiteTopologyFailover,
			InterSiteTopologyGenerator,
			InterSiteTopologyRenew,
			InternationalISDNNumber,
			InvocationId,
			IpHostNumber,
			IpNetmaskNumber,
			IpNetworkNumber,
			IpProtocolNumber,
			IpsecData,
			IpsecDataType,
			IpsecFilterReference,
			IpsecID,
			IpsecISAKMPReference,
			IpsecName,
			IPSECNegotiationPolicyAction,
			IpsecNegotiationPolicyReference,
			IPSECNegotiationPolicyType,
			IpsecNFAReference,
			IpsecOwnersReference,
			IpsecPolicyReference,
			IpServicePort,
			IpServiceProtocol,
			IsCriticalSystemObject,
			IsDefunct,
			IsDeleted,
			IsEphemeral,
			MemberOf,
			IsMemberOfPartialAttributeSet,
			IsPrivilegeHolder,
			IsRecycled,
			IsSingleValued,
			JpegPhoto,
			Keywords,
			KnowledgeInformation,
			LabeledURI,
			LastBackupRestorationTime,
			LastContentIndexed,
			LastKnownParent,
			LastLogoff,
			LastLogon,
			LastLogonTimestamp,
			LastSetTime,
			LastUpdateSequence,
			LDAPAdminLimits,
			LDAPDisplayName,
			LDAPIPDenyList,
			LegacyExchangeDN,
			LinkID,
			LinkTrackSecret,
			LmPwdHistory,
			LocalPolicyFlags,
			LocalPolicyReference,
			LocaleID,
			L,
			LocalizationDisplayId,
			LocalizedDescription,
			Location,
			LockOutObservationWindow,
			LockoutDuration,
			LockoutThreshold,
			LockoutTime,
			LoginShell,
			ThumbnailLogo,
			LogonCount,
			LogonHours,
			LogonWorkstation,
			LSACreationTime,
			LSAModifiedCount,
			MacAddress,
			MachineArchitecture,
			MachinePasswordChangeInterval,
			MachineRole,
			MachineWidePolicy,
			ManagedBy,
			ManagedObjects,
			Manager,
			MAPIID,
			MarshalledInterface,
			MasteredBy,
			MaxPwdAge,
			MaxRenewAge,
			MaxStorage,
			MaxTicketAge,
			MayContain,
			MeetingAdvertiseScope,
			MeetingApplication,
			MeetingBandwidth,
			MeetingBlob,
			MeetingContactInfo,
			MeetingDescription,
			MeetingEndTime,
			MeetingID,
			MeetingIP,
			MeetingIsEncrypted,
			MeetingKeyword,
			MeetingLanguage,
			MeetingLocation,
			MeetingMaxParticipants,
			MeetingName,
			MeetingOriginator,
			MeetingOwner,
			MeetingProtocol,
			MeetingRating,
			MeetingRecurrence,
			MeetingScope,
			MeetingStartTime,
			MeetingType,
			MeetingURL,
			Member,
			MemberNisNetgroup,
			MemberUid,
			MhsORAddress,
			MinPwdAge,
			MinPwdLength,
			MinTicketAge,
			ModifiedCount,
			ModifiedCountAtLastProm,
			ModifyTimeStamp,
			Moniker,
			MonikerDisplayName,
			MoveTreeState,
			MsAuthzCentralAccessPolicyID,
			MsAuthzEffectiveSecurityPolicy,
			MsAuthzLastEffectiveSecurityPolicy,
			MsAuthzMemberRulesInCentralAccessPolicy,
			MsAuthzMemberRulesInCentralAccessPolicyBL,
			MsAuthzProposedSecurityPolicy,
			MsAuthzResourceCondition,
			MsCOMDefaultPartitionLink,
			MsCOMObjectId,
			MsCOMPartitionLink,
			MsCOMPartitionSetLink,
			MsCOMUserLink,
			MsCOMUserPartitionSetLink,
			MsDFSCommentv2,
			MsDFSGenerationGUIDv2,
			MsDFSLastModifiedv2,
			MsDFSLinkIdentityGUIDv2,
			MsDFSLinkPathv2,
			MsDFSLinkSecurityDescriptorv2,
			MsDFSNamespaceIdentityGUIDv2,
			MsDFSPropertiesv2,
			MsDFSSchemaMajorVersion,
			MsDFSSchemaMinorVersion,
			MsDFSShortNameLinkPathv2,
			MsDFSTargetListv2,
			MsDFSTtlv2,
			MsDFSRCachePolicy,
			MsDFSRCommonStagingPath,
			MsDFSRCommonStagingSizeInMb,
			MsDFSRComputerReference,
			MsDFSRComputerReferenceBL,
			MsDFSRConflictPath,
			MsDFSRConflictSizeInMb,
			MsDFSRContentSetGuid,
			MsDFSRDefaultCompressionExclusionFilter,
			MsDFSRDeletedPath,
			MsDFSRDeletedSizeInMb,
			MsDFSRDfsLinkTarget,
			MsDFSRDfsPath,
			MsDFSRDirectoryFilter,
			MsDFSRDisablePacketPrivacy,
			MsDFSREnabled,
			MsDFSRExtension,
			MsDFSRFileFilter,
			MsDFSRFlags,
			MsDFSRKeywords,
			MsDFSRMaxAgeInCacheInMin,
			MsDFSRMemberReference,
			MsDFSRMemberReferenceBL,
			MsDFSRMinDurationCacheInMin,
			MsDFSROnDemandExclusionDirectoryFilter,
			MsDFSROnDemandExclusionFileFilter,
			MsDFSROptions,
			MsDFSROptions2,
			MsDFSRPriority,
			MsDFSRRdcEnabled,
			MsDFSRRdcMinFileSizeInKb,
			MsDFSRReadOnly,
			MsDFSRReplicationGroupGuid,
			MsDFSRReplicationGroupType,
			MsDFSRRootFence,
			MsDFSRRootPath,
			MsDFSRRootSizeInMb,
			MsDFSRSchedule,
			MsDFSRStagingCleanupTriggerInPercent,
			MsDFSRStagingPath,
			MsDFSRStagingSizeInMb,
			MsDFSRTombstoneExpiryInMin,
			MsDFSRVersion,
			MsDNSDNSKEYRecordSetTTL,
			MsDNSDNSKEYRecords,
			MsDNSDSRecordAlgorithms,
			MsDNSDSRecordSetTTL,
			MsDNSIsSigned,
			MsDNSKeymasterZones,
			MsDNSMaintainTrustAnchor,
			MsDNSNSEC3CurrentSalt,
			MsDNSNSEC3HashAlgorithm,
			MsDNSNSEC3Iterations,
			MsDNSNSEC3OptOut,
			MsDNSNSEC3RandomSaltLength,
			MsDNSNSEC3UserSalt,
			MsDNSParentHasSecureDelegation,
			MsDNSPropagationTime,
			MsDNSRFC5011KeyRollovers,
			MsDNSSecureDelegationPollingPeriod,
			MsDNSSignWithNSEC3,
			MsDNSSignatureInceptionOffset,
			MsDNSSigningKeyDescriptors,
			MsDNSSigningKeys,
			MsDRMIdentityCertificate,
			MsDSAdditionalDnsHostName,
			MsDSAdditionalSamAccountName,
			MsDSAllUsersTrustQuota,
			MsDSAllowedDNSSuffixes,
			MsDSAllowedToActOnBehalfOfOtherIdentity,
			MsDSAllowedToDelegateTo,
			MsDSAppliesToResourceTypes,
			MsDSApproxImmedSubordinates,
			MsDSApproximateLastLogonTimeStamp,
			MsDSAssignedAuthNPolicy,
			MsDSAssignedAuthNPolicyBL,
			MsDSAssignedAuthNPolicySilo,
			MsDSAssignedAuthNPolicySiloBL,
			MsDSAuthenticatedAtDC,
			MsDSAuthenticatedToAccountlist,
			MsDSAuthNPolicyEnforced,
			MsDSAuthNPolicySiloEnforced,
			MsDSAuthNPolicySiloMembers,
			MsDSAuthNPolicySiloMembersBL,
			MsDSAuxiliaryClasses,
			MsDSAzApplicationData,
			MsDSAzApplicationName,
			MsDSAzApplicationVersion,
			MsDSAzBizRule,
			MsDSAzBizRuleLanguage,
			MsDSAzClassId,
			MsDSAzDomainTimeout,
			MsDSAzGenerateAudits,
			MsDSAzGenericData,
			MsDSAzLastImportedBizRulePath,
			MsDSAzLDAPQuery,
			MsDSAzMajorVersion,
			MsDSAzMinorVersion,
			MsDSAzObjectGuid,
			MsDSAzOperationID,
			MsDSAzScopeName,
			MsDSAzScriptEngineCacheMax,
			MsDSAzScriptTimeout,
			MsDSAzTaskIsRoleDefinition,
			MsDSBehaviorVersion,
			MsDSBridgeHeadServersUsed,
			MsDSByteArray,
			MsDSCachedMembership,
			MsDSCachedMembershipTimeStamp,
			MsDSClaimAttributeSource,
			MsDSClaimIsSingleValued,
			MsDSClaimIsValueSpaceRestricted,
			MsDSClaimPossibleValues,
			MsDSClaimSharesPossibleValuesWith,
			MsDSClaimSharesPossibleValuesWithBL,
			MsDSClaimSource,
			MsDSClaimSourceType,
			MsDSClaimTypeAppliesToClass,
			MsDSClaimValueType,
			MsDSCloudAnchor,
			MsDSCloudIsEnabled,
			MsDSCloudIsManaged,
			MsDSCloudIssuerPublicCertificates,
			MsDScloudExtensionAttribute1,
			MsDScloudExtensionAttribute10,
			MsDScloudExtensionAttribute11,
			MsDScloudExtensionAttribute12,
			MsDScloudExtensionAttribute13,
			MsDScloudExtensionAttribute14,
			MsDScloudExtensionAttribute15,
			MsDScloudExtensionAttribute16,
			MsDScloudExtensionAttribute17,
			MsDScloudExtensionAttribute18,
			MsDScloudExtensionAttribute19,
			MsDScloudExtensionAttribute2,
			MsDScloudExtensionAttribute20,
			MsDScloudExtensionAttribute3,
			MsDScloudExtensionAttribute4,
			MsDScloudExtensionAttribute5,
			MsDScloudExtensionAttribute6,
			MsDScloudExtensionAttribute7,
			MsDScloudExtensionAttribute8,
			MsDScloudExtensionAttribute9,
			MsDSComputerAllowedToAuthenticateTo,
			MsDSComputerAuthNPolicy,
			MsDSComputerAuthNPolicyBL,
			MsDSComputerSID,
			MsDSComputerTGTLifetime,
			MSDSConsistencyChildCount,
			MSDSConsistencyGuid,
			MSDSCreatorSID,
			MsDSCustomKeyInformation,
			MsDSDateTime,
			MsDSDefaultQuota,
			MsDSDeletedObjectLifetime,
			MsDSDeviceDN,
			MsDSDeviceID,
			MsDSDeviceLocation,
			MsDSDeviceMDMStatus,
			MsDSDeviceObjectVersion,
			MsDSDeviceOSType,
			MsDSDeviceOSVersion,
			MsDSDevicePhysicalIDs,
			MsDSDeviceTrustType,
			MsDSDnsRootAlias,
			MsDSDrsFarmID,
			MsDSEgressClaimsTransformationPolicy,
			MsDSEnabledFeature,
			MsDSEnabledFeatureBL,
			MsDSEntryTimeToDie,
			MsDSExecuteScriptPassword,
			MsDSExpirePasswordsOnSmartCardOnlyAccounts,
			MsDSExternalDirectoryObjectId,
			MsDSExternalKey,
			MsDSExternalStore,
			MsDSFailedInteractiveLogonCount,
			MsDSFailedInteractiveLogonCountAtLastSuccessfulLogon,
			MsDSFilterContainers,
			MsDSGenerationId,
			MsDSGeoCoordinatesAltitude,
			MsDSGeoCoordinatesLatitude,
			MsDSGeoCoordinatesLongitude,
			MsDSGroupMSAMembership,
			MsDSHABSeniorityIndex,
			MsDSHasDomainNCs,
			MsDShasFullReplicaNCs,
			MsDSHasInstantiatedNCs,
			MsDShasMasterNCs,
			MsDSHostServiceAccount,
			MsDSHostServiceAccountBL,
			MsDSIngressClaimsTransformationPolicy,
			MsDSInteger,
			MsDSIntId,
			MsDSIsCompliant,
			MsDSIsDomainFor,
			MsDSIsEnabled,
			MsDSIsFullReplicaFor,
			MsdsmemberOfTransitive,
			MsDSIsPartialReplicaFor,
			MsDSIsPossibleValuesPresent,
			MsDSIsPrimaryComputerFor,
			MsDSIsUsedAsResourceSecurityAttribute,
			MsDSIsUserCachableAtRodc,
			MsDSisGC,
			MsDSIsManaged,
			MsDSisRODC,
			MsDSIssuerCertificates,
			MsDSIssuerPublicCertificates,
			MsDSKeyApproximateLastLogonTimeStamp,
			MsDSKeyCredentialLink,
			MsDSKeyCredentialLinkBL,
			MsDSKeyId,
			MsDSKeyMaterial,
			MsDSKeyPrincipal,
			MsDSKeyPrincipalBL,
			MsDSKeyUsage,
			MsDSKeyVersionNumber,
			MsDSKrbTgtLink,
			MsDSKrbTgtLinkBl,
			MsDSLastFailedInteractiveLogonTime,
			MsDSLastKnownRDN,
			MsDSLastSuccessfulInteractiveLogonTime,
			MsDSLocalEffectiveDeletionTime,
			MsDSLocalEffectiveRecycleTime,
			MsDSLockoutDuration,
			MsDSLockoutObservationWindow,
			MsDSLockoutThreshold,
			MsDSLogonTimeSyncInterval,
			MsDSMachineAccountQuota,
			MsDSManagedPassword,
			MsDSManagedPasswordId,
			MsDSManagedPasswordInterval,
			MsDSManagedPasswordPreviousId,
			MsDsmasteredBy,
			MsDsMaxValues,
			MsDSMaximumPasswordAge,
			MsDSMaximumRegistrationInactivityPeriod,
			MsdsmemberTransitive,
			MsDSMembersForAzRole,
			MsDSMembersForAzRoleBL,
			MsDSMembersOfResourcePropertyList,
			MsDSMembersOfResourcePropertyListBL,
			MsDSMinimumPasswordAge,
			MsDSMinimumPasswordLength,
			MsDSNCReplCursors,
			MsDSNCReplInboundNeighbors,
			MsDSNCReplOutboundNeighbors,
			MsDSNCReplicaLocations,
			MsDSNCROReplicaLocations,
			MsDSNCROReplicaLocationsBL,
			MsDSNcType,
			MsDSNeverRevealGroup,
			MsDSNonMembers,
			MsDSNonMembersBL,
			MsDSNonSecurityGroupExtraClasses,
			MsDSObjectReference,
			MsDSObjectReferenceBL,
			MsDSObjectSoa,
			MsDSOIDToGroupLink,
			MsDSOIDToGroupLinkBl,
			MsDSOperationsForAzRole,
			MsDSOperationsForAzRoleBL,
			MsDSOperationsForAzTask,
			MsDSOperationsForAzTaskBL,
			MsDSOptionalFeatureFlags,
			MsDSOptionalFeatureGUID,
			MsDSOtherSettings,
			MsDSparentdistname,
			MsDSPasswordComplexityEnabled,
			MsDSPasswordHistoryLength,
			MsDSPasswordReversibleEncryptionEnabled,
			MsDSPasswordSettingsPrecedence,
			MsDSPerUserTrustQuota,
			MsDSPerUserTrustTombstonesQuota,
			MsDSPhoneticCompanyName,
			MsDSPhoneticDepartment,
			MsDSPhoneticDisplayName,
			MsDSPhoneticFirstName,
			MsDSPhoneticLastName,
			MsDSpreferredDataLocation,
			MsDSPreferredGCSite,
			MsDSPrimaryComputer,
			MsDSPrincipalName,
			MsDSPromotionSettings,
			MsDSPSOApplied,
			MsDSPSOAppliesTo,
			MsDSQuotaAmount,
			MsDSQuotaEffective,
			MsDSQuotaTrustee,
			MsDSQuotaUsed,
			MsDSRegisteredOwner,
			MsDSRegisteredUsers,
			MsDSRegistrationQuota,
			MsDSReplAttributeMetaData,
			MsDSReplValueMetaData,
			MsDSReplValueMetaDataExt,
			MSDSReplicatesNCReason,
			MsDSReplicationNotifyFirstDSADelay,
			MsDSReplicationNotifySubsequentDSADelay,
			MsDSReplicationEpoch,
			MsDSRequiredDomainBehaviorVersion,
			MsDSRequiredForestBehaviorVersion,
			MsDSResultantPSO,
			MsDSRetiredReplNCSignatures,
			MsDSRevealOnDemandGroup,
			MsDSRevealedDSAs,
			MsDSRevealedList,
			MsDSRevealedListBL,
			MsDSRevealedUsers,
			MsDSRIDPoolAllocationEnabled,
			MsDsSchemaExtensions,
			MsDSSDReferenceDomain,
			MsDSSecondaryKrbTgtNumber,
			MsDSSecurityGroupExtraClasses,
			MsDSServiceAllowedNTLMNetworkAuthentication,
			MsDSServiceAllowedToAuthenticateFrom,
			MsDSServiceAllowedToAuthenticateTo,
			MsDSServiceAuthNPolicy,
			MsDSServiceAuthNPolicyBL,
			MsDSServiceTGTLifetime,
			MsDSSettings,
			MsDSShadowPrincipalSid,
			MsDSSiteAffinity,
			MsDSSiteName,
			MsDSSourceAnchor,
			MsDSSourceObjectDN,
			MsDSSPNSuffixes,
			MsDSStrongNTLMPolicy,
			MsDSSupportedEncryptionTypes,
			MsDSSyncServerUrl,
			MsDSTasksForAzRole,
			MsDSTasksForAzRoleBL,
			MsDSTasksForAzTask,
			MsDSTasksForAzTaskBL,
			MsDSTDOEgressBL,
			MsDSTDOIngressBL,
			MsdstokenGroupNames,
			MsdstokenGroupNamesGlobalAndUniversal,
			MsdstokenGroupNamesNoGCAcceptable,
			MsDSTombstoneQuotaFactor,
			MsDSTopQuotaUsage,
			MsDSTransformationRules,
			MsDSTransformationRulesCompiled,
			MsDSTrustForestTrustInfo,
			MsDSUpdateScript,
			MsDSUserAccountControlComputed,
			MsDSUserAllowedNTLMNetworkAuthentication,
			MsDSUserAllowedToAuthenticateFrom,
			MsDSUserAllowedToAuthenticateTo,
			MsDSUserAuthNPolicy,
			MsDSUserAuthNPolicyBL,
			MsDSUserPasswordExpiryTimeComputed,
			MsDSUserTGTLifetime,
			MsDSUSNLastSyncSuccess,
			MsDSValueTypeReference,
			MsDSValueTypeReferenceBL,
			MsExchAssistantName,
			MsExchHouseIdentifier,
			MsExchLabeledURI,
			OwnerBL,
			MsFRSHubMember,
			MsFRSTopologyPref,
			MsFVEKeyPackage,
			MsFVERecoveryGuid,
			MsFVERecoveryPassword,
			MsFVEVolumeGuid,
			Msieee80211Data,
			Msieee80211DataType,
			Msieee80211ID,
			MsIISFTPDir,
			MsIISFTPRoot,
			MsImagingHashAlgorithm,
			MsImagingPSPIdentifier,
			MsImagingPSPString,
			MsImagingThumbprintHash,
			MsKdsCreateTime,
			MsKdsDomainID,
			MsKdsKDFAlgorithmID,
			MsKdsKDFParam,
			MsKdsPrivateKeyLength,
			MsKdsPublicKeyLength,
			MsKdsRootKeyData,
			MsKdsSecretAgreementAlgorithmID,
			MsKdsSecretAgreementParam,
			MsKdsUseStartTime,
			MsKdsVersion,
			Msnetieee80211GPPolicyData,
			Msnetieee80211GPPolicyGUID,
			Msnetieee80211GPPolicyReserved,
			Msnetieee8023GPPolicyData,
			Msnetieee8023GPPolicyGUID,
			Msnetieee8023GPPolicyReserved,
			MsPKIAccountCredentials,
			MsPKICertTemplateOID,
			MsPKICertificateApplicationPolicy,
			MsPKICertificateNameFlag,
			MsPKICertificatePolicy,
			MsPKICredentialRoamingTokens,
			MsPKIDPAPIMasterKeys,
			MsPKIEnrollmentFlag,
			MsPKIEnrollmentServers,
			MsPKIMinimalKeySize,
			MsPKIOIDAttribute,
			MsPKIOIDCPS,
			MsPKIOIDLocalizedName,
			MsPKIOIDUserNotice,
			MsPKIPrivateKeyFlag,
			MsPKIRAApplicationPolicies,
			MsPKIRAPolicies,
			MsPKIRASignature,
			MsPKIRoamingTimeStamp,
			MsPKISiteName,
			MsPKISupersedeTemplates,
			MsPKITemplateMinorRevision,
			MsPKITemplateSchemaVersion,
			MsRADIUSFramedInterfaceId,
			MsRADIUSFramedIpv6Prefix,
			MsRADIUSFramedIpv6Route,
			MsRADIUSSavedFramedInterfaceId,
			MsRADIUSSavedFramedIpv6Prefix,
			MsRADIUSSavedFramedIpv6Route,
			MsRRASAttribute,
			MsRRASVendorAttributeEntry,
			MsSPPConfigLicense,
			MsSPPConfirmationId,
			MsSPPCSVLKPartialProductKey,
			MsSPPCSVLKPid,
			MsSPPCSVLKSkuId,
			MsSPPInstallationId,
			MsSPPIssuanceLicense,
			MsSPPKMSIds,
			MsSPPOnlineLicense,
			MsSPPPhoneLicense,
			MSSQLAlias,
			MSSQLAllowAnonymousSubscription,
			MSSQLAllowImmediateUpdatingSubscription,
			MSSQLAllowKnownPullSubscription,
			MSSQLAllowQueuedUpdatingSubscription,
			MSSQLAllowSnapshotFilesFTPDownloading,
			MSSQLAppleTalk,
			MSSQLApplications,
			MSSQLBuild,
			MSSQLCharacterSet,
			MSSQLClustered,
			MSSQLConnectionURL,
			MSSQLContact,
			MSSQLCreationDate,
			MSSQLDatabase,
			MSSQLDescription,
			MSSQLGPSHeight,
			MSSQLGPSLatitude,
			MSSQLGPSLongitude,
			MSSQLInformationDirectory,
			MSSQLInformationURL,
			MSSQLKeywords,
			MSSQLLanguage,
			MSSQLLastBackupDate,
			MSSQLLastDiagnosticDate,
			MSSQLLastUpdatedDate,
			MSSQLLocation,
			MSSQLMemory,
			MSSQLMultiProtocol,
			MSSQLName,
			MSSQLNamedPipe,
			MSSQLPublicationURL,
			MSSQLPublisher,
			MSSQLRegisteredOwner,
			MSSQLServiceAccount,
			MSSQLSize,
			MSSQLSortOrder,
			MSSQLSPX,
			MSSQLStatus,
			MSSQLTCPIP,
			MSSQLThirdParty,
			MSSQLType,
			MSSQLUnicodeSortOrder,
			MSSQLVersion,
			MSSQLVines,
			MsTAPIConferenceBlob,
			MsTAPIIpAddress,
			MsTAPIProtocolId,
			MsTAPIuid,
			MsTPMOwnerInformationTemp,
			MsTPMOwnerInformation,
			MsTPMSrkPubThumbprint,
			MsTPMTpmInformationForComputer,
			MsTPMTpmInformationForComputerBL,
			MsTSAllowLogon,
			MsTSBrokenConnectionAction,
			MsTSConnectClientDrives,
			MsTSConnectPrinterDrives,
			MsTSDefaultToMainPrinter,
			MsTSEndpointData,
			MsTSEndpointPlugin,
			MsTSEndpointType,
			MsTSExpireDate,
			MsTSExpireDate2,
			MsTSExpireDate3,
			MsTSExpireDate4,
			MsTSHomeDirectory,
			MsTSHomeDrive,
			MsTSInitialProgram,
			MsTSLicenseVersion,
			MsTSLicenseVersion2,
			MsTSLicenseVersion3,
			MsTSLicenseVersion4,
			MsTSManagingLS,
			MsTSManagingLS2,
			MsTSManagingLS3,
			MsTSManagingLS4,
			MsTSMaxConnectionTime,
			MsTSMaxDisconnectionTime,
			MsTSMaxIdleTime,
			MsTSPrimaryDesktop,
			MsTSPrimaryDesktopBL,
			MsTSProfilePath,
			MsTSProperty01,
			MsTSProperty02,
			MsTSReconnectionAction,
			MsTSRemoteControl,
			MsTSSecondaryDesktopBL,
			MsTSSecondaryDesktops,
			MsTSWorkDirectory,
			MsTSLSProperty01,
			MsTSLSProperty02,
			MsWMIAuthor,
			MsWMIChangeDate,
			MsWMIClass,
			MsWMIClassDefinition,
			MsWMICreationDate,
			MsWMIGenus,
			MsWMIID,
			MsWMIInt8Default,
			MsWMIInt8Max,
			MsWMIInt8Min,
			MsWMIInt8ValidValues,
			MsWMIIntDefault,
			MsWMIintFlags1,
			MsWMIintFlags2,
			MsWMIintFlags3,
			MsWMIintFlags4,
			MsWMIIntMax,
			MsWMIIntMin,
			MsWMIIntValidValues,
			MsWMIMof,
			MsWMIName,
			MsWMINormalizedClass,
			MsWMIParm1,
			MsWMIParm2,
			MsWMIParm3,
			MsWMIParm4,
			MsWMIPropertyName,
			MsWMIQuery,
			MsWMIQueryLanguage,
			MsWMIScopeGuid,
			MsWMISourceOrganization,
			MsWMIStringDefault,
			MsWMIStringValidValues,
			MsWMITargetClass,
			MsWMITargetNameSpace,
			MsWMITargetObject,
			MsWMITargetPath,
			MsWMITargetType,
			MscopeId,
			MsiFileList,
			MsiScript,
			MsiScriptName,
			MsiScriptPath,
			MsiScriptSize,
			MSMQAuthenticate,
			MSMQBasePriority,
			MSMQComputerType,
			MSMQComputerTypeEx,
			MSMQCost,
			MSMQCSPName,
			MSMQDependentClientService,
			MSMQDependentClientServices,
			MSMQDigests,
			MSMQDigestsMig,
			MSMQDsService,
			MSMQDsServices,
			MSMQEncryptKey,
			MSMQForeign,
			MSMQInRoutingServers,
			MSMQInterval1,
			MSMQInterval2,
			MSMQJournal,
			MSMQJournalQuota,
			MSMQLabel,
			MSMQLabelEx,
			MSMQLongLived,
			MSMQMigrated,
			MSMQMulticastAddress,
			MSMQNameStyle,
			MSMQNt4Flags,
			MSMQNt4Stub,
			MSMQOSType,
			MSMQOutRoutingServers,
			MSMQOwnerID,
			MSMQPrevSiteGates,
			MSMQPrivacyLevel,
			MSMQQMID,
			MSMQQueueJournalQuota,
			MSMQQueueNameExt,
			MSMQQueueQuota,
			MSMQQueueType,
			MSMQQuota,
			MsMQRecipientFormatName,
			MSMQRoutingService,
			MSMQRoutingServices,
			MSMQSecuredSource,
			MSMQServiceType,
			MSMQServices,
			MSMQSignCertificates,
			MSMQSignCertificatesMig,
			MSMQSignKey,
			MSMQSite1,
			MSMQSite2,
			MSMQSiteForeign,
			MSMQSiteGates,
			MSMQSiteGatesMig,
			MSMQSiteID,
			MSMQSiteName,
			MSMQSiteNameEx,
			MSMQSites,
			MSMQTransactional,
			MSMQUserSid,
			MSMQVersion,
			MsNPAllowDialin,
			MsNPCalledStationID,
			MsNPCallingStationID,
			MsNPSavedCallingStationID,
			MsRADIUSCallbackNumber,
			MsRADIUSFramedIPAddress,
			MsRADIUSFramedRoute,
			MsRADIUSServiceType,
			MsRASSavedCallbackNumber,
			MsRASSavedFramedIPAddress,
			MsRASSavedFramedRoute,
			MsSFU30Aliases,
			MsSFU30CryptMethod,
			MsSFU30Domains,
			MsSFU30FieldSeparator,
			MsSFU30IntraFieldSeparator,
			MsSFU30IsValidContainer,
			MsSFU30KeyAttributes,
			MsSFU30KeyValues,
			MsSFU30MapFilter,
			MsSFU30MasterServerName,
			MsSFU30MaxGidNumber,
			MsSFU30MaxUidNumber,
			MsSFU30Name,
			MsSFU30NetgroupHostAtDomain,
			MsSFU30NetgroupUserAtDomain,
			MsSFU30NisDomain,
			MsSFU30NSMAPFieldPosition,
			MsSFU30OrderNumber,
			MsSFU30PosixMember,
			MsSFU30PosixMemberOf,
			MsSFU30ResultAttributes,
			MsSFU30SearchAttributes,
			MsSFU30SearchContainer,
			MsSFU30YpServers,
			MustContain,
			NameServiceFlags,
			NCName,
			NETBIOSName,
			NetbootAllowNewClients,
			NetbootAnswerOnlyValidClients,
			NetbootAnswerRequests,
			NetbootCurrentClientCount,
			NetbootDUID,
			NetbootGUID,
			NetbootInitialization,
			NetbootIntelliMirrorOSes,
			NetbootLimitClients,
			NetbootLocallyInstalledOSes,
			NetbootMachineFilePath,
			NetbootMaxClients,
			NetbootMirrorDataFile,
			NetbootNewMachineNamingPolicy,
			NetbootNewMachineOU,
			NetbootSCPBL,
			NetbootServer,
			NetbootSIFFile,
			NetbootTools,
			NetworkAddress,
			NextLevelStore,
			NextRid,
			NisMapEntry,
			NisMapName,
			NisNetgroupTriple,
			NonSecurityMember,
			NonSecurityMemberBL,
			NotificationList,
			NTGroupMembers,
			NTMixedDomain,
			NtPwdHistory,
			NTSecurityDescriptor,
			DistinguishedName,
			ObjectCategory,
			ObjectClassCategory,
			ObjectClasses,
			ObjectCount,
			ObjectGUID,
			ObjectSid,
			ObjectVersion,
			OEMInformation,
			OMSyntax,
			OMTGuid,
			OMTIndxGuid,
			OncRpcNumber,
			OperatingSystem,
			OperatingSystemHotfix,
			OperatingSystemServicePack,
			OperatingSystemVersion,
			OperatorCount,
			OptionDescription,
			Options,
			OptionsLocation,
			O,
			Ou,
			OrganizationalStatus,
			OriginalDisplayTable,
			OriginalDisplayTableMSDOS,
			OtherLoginWorkstations,
			OtherMailbox,
			MiddleName,
			OtherWellKnownObjects,
			Owner,
			PackageFlags,
			PackageName,
			PackageType,
			ParentCA,
			ParentCACertificateChain,
			ParentGUID,
			PartialAttributeDeletionList,
			PartialAttributeSet,
			PekKeyChangeInterval,
			PekList,
			PendingCACertificates,
			PendingParentCA,
			PerMsgDialogDisplayTable,
			PerRecipDialogDisplayTable,
			PersonalTitle,
			OtherFacsimileTelephoneNumber,
			OtherHomePhone,
			HomePhone,
			OtherIpPhone,
			IpPhone,
			PrimaryInternationalISDNNumber,
			OtherMobile,
			Mobile,
			OtherTelephone,
			OtherPager,
			Pager,
			Photo,
			PhysicalDeliveryOfficeName,
			PhysicalLocationObject,
			ThumbnailPhoto,
			PKICriticalExtensions,
			PKIDefaultCSPs,
			PKIDefaultKeySpec,
			PKIEnrollmentAccess,
			PKIExpirationPeriod,
			PKIExtendedKeyUsage,
			PKIKeyUsage,
			PKIMaxIssuingDepth,
			PKIOverlapPeriod,
			PKT,
			PKTGuid,
			PolicyReplicationFlags,
			PortName,
			PossSuperiors,
			PossibleInferiors,
			PostOfficeBox,
			PostalAddress,
			PostalCode,
			PreferredDeliveryMethod,
			PreferredOU,
			PreferredLanguage,
			PrefixMap,
			PresentationAddress,
			PreviousCACertificates,
			PreviousParentCA,
			PrimaryGroupID,
			PrimaryGroupToken,
			PrintAttributes,
			PrintBinNames,
			PrintCollate,
			PrintColor,
			PrintDuplexSupported,
			PrintEndTime,
			PrintFormName,
			PrintKeepPrintedJobs,
			PrintLanguage,
			PrintMACAddress,
			PrintMaxCopies,
			PrintMaxResolutionSupported,
			PrintMaxXExtent,
			PrintMaxYExtent,
			PrintMediaReady,
			PrintMediaSupported,
			PrintMemory,
			PrintMinXExtent,
			PrintMinYExtent,
			PrintNetworkAddress,
			PrintNotify,
			PrintNumberUp,
			PrintOrientationsSupported,
			PrintOwner,
			PrintPagesPerMinute,
			PrintRate,
			PrintRateUnit,
			PrintSeparatorFile,
			PrintShareName,
			PrintSpooling,
			PrintStaplingSupported,
			PrintStartTime,
			PrintStatus,
			PrinterName,
			PriorSetTime,
			PriorValue,
			Priority,
			PrivateKey,
			PrivilegeAttributes,
			PrivilegeDisplayName,
			PrivilegeHolder,
			PrivilegeValue,
			ProductCode,
			ProfilePath,
			ProxiedObjectName,
			ProxyAddresses,
			ProxyGenerationEnabled,
			ProxyLifetime,
			PublicKeyPolicy,
			PurportedSearch,
			PwdHistoryLength,
			PwdLastSet,
			PwdProperties,
			QualityOfService,
			QueryFilter,
			QueryPolicyBL,
			QueryPolicyObject,
			QueryPoint,
			RangeLower,
			RangeUpper,
			Name,
			RDNAttID,
			RegisteredAddress,
			RemoteServerName,
			RemoteSource,
			RemoteSourceType,
			RemoteStorageGUID,
			ReplInterval,
			ReplPropertyMetaData,
			ReplTopologyStayOfExecution,
			ReplUpToDateVector,
			ReplicaSource,
			DirectReports,
			RepsFrom,
			RepsTo,
			RequiredCategories,
			RetiredReplDSASignatures,
			Revision,
			Rid,
			RIDAllocationPool,
			RIDAvailablePool,
			RIDManagerReference,
			RIDNextRID,
			RIDPreviousAllocationPool,
			RIDSetReferences,
			RIDUsedPool,
			RightsGuid,
			RoleOccupant,
			RoomNumber,
			RootTrust,
			RpcNsAnnotation,
			RpcNsBindings,
			RpcNsCodeset,
			RpcNsEntryFlags,
			RpcNsGroup,
			RpcNsInterfaceID,
			RpcNsObjectID,
			RpcNsPriority,
			RpcNsProfileEntry,
			RpcNsTransferSyntax,
			SAMAccountName,
			SAMAccountType,
			SamDomainUpdates,
			Schedule,
			SchemaFlagsEx,
			SchemaIDGUID,
			SchemaInfo,
			SchemaUpdate,
			SchemaVersion,
			ScopeFlags,
			ScriptPath,
			SDRightsEffective,
			SearchFlags,
			SearchGuide,
			Secretary,
			SecurityIdentifier,
			SeeAlso,
			SeqNotification,
			SerialNumber,
			ServerName,
			ServerReference,
			ServerReferenceBL,
			ServerRole,
			ServerState,
			ServiceBindingInformation,
			ServiceClassID,
			ServiceClassInfo,
			ServiceClassName,
			ServiceDNSName,
			ServiceDNSNameType,
			ServiceInstanceVersion,
			ServicePrincipalName,
			SetupCommand,
			ShadowExpire,
			ShadowFlag,
			ShadowInactive,
			ShadowLastChange,
			ShadowMax,
			ShadowMin,
			ShadowWarning,
			ShellContextMenu,
			ShellPropertyPages,
			ShortServerName,
			ShowInAddressBook,
			ShowInAdvancedViewOnly,
			SIDHistory,
			SignatureAlgorithms,
			SiteGUID,
			SiteLinkList,
			SiteList,
			SiteObject,
			SiteObjectBL,
			SiteServer,
			MailAddress,
			SPNMappings,
			St,
			Street,
			StructuralObjectClass,
			SubClassOf,
			SubRefs,
			SubSchemaSubEntry,
			SuperScopeDescription,
			SuperScopes,
			SuperiorDNSRoot,
			SupplementalCredentials,
			SupportedApplicationContext,
			Sn,
			SyncAttributes,
			SyncMembership,
			SyncWithObject,
			SyncWithSID,
			SystemAuxiliaryClass,
			SystemFlags,
			SystemMayContain,
			SystemMustContain,
			SystemOnly,
			SystemPossSuperiors,
			TelephoneNumber,
			TeletexTerminalIdentifier,
			TelexNumber,
			PrimaryTelexNumber,
			TemplateRoots,
			TemplateRoots2,
			TerminalServer,
			Co,
			TextEncodedORAddress,
			TimeRefresh,
			TimeVolChange,
			Title,
			TokenGroups,
			TokenGroupsGlobalAndUniversal,
			TokenGroupsNoGCAcceptable,
			TombstoneLifetime,
			TransportAddressAttribute,
			TransportDLLName,
			TransportType,
			TreatAsLeaf,
			TreeName,
			TrustAttributes,
			TrustAuthIncoming,
			TrustAuthOutgoing,
			TrustDirection,
			TrustParent,
			TrustPartner,
			TrustPosixOffset,
			TrustType,
			UASCompat,
			Uid,
			UidNumber,
			UNCName,
			UnicodePwd,
			UniqueIdentifier,
			UniqueMember,
			UnixHomeDirectory,
			UnixUserPassword,
			UnstructuredAddress,
			UnstructuredName,
			UpgradeProductCode,
			UPNSuffixes,
			UserAccountControl,
			UserCert,
			Comment,
			UserParameters,
			UserPassword,
			UserPrincipalName,
			UserSharedFolder,
			UserSharedFolderOther,
			UserSMIMECertificate,
			UserWorkstations,
			UserClass,
			UserPKCS12,
			USNChanged,
			USNCreated,
			USNDSALastObjRemoved,
			USNIntersite,
			USNLastObjRem,
			USNSource,
			ValidAccesses,
			Vendor,
			VersionNumber,
			VersionNumberHi,
			VersionNumberLo,
			VolTableGUID,
			VolTableIdxGUID,
			VolumeCount,
			WbemPath,
			WellKnownObjects,
			WhenChanged,
			WhenCreated,
			WinsockAddresses,
			WWWHomePage,
			Url,
			X121Address,
			X500uniqueIdentifier,
			UserCertificate,
			#endregion
		];

		public static ImmutableArray<AttributeTypeDescription> GetAllAttributes() => allAttrs;
		#endregion

		private static readonly Dictionary<string, AttributeTypeDescription> attrsByNameOrOid = BuildIndex();
	}
}
