namespace Titanis.Ldap
{
	// [MS-ADTS] § 3.1.1.3.4.1 LDAP Extended Controls
	static class AdExtensions
	{
		public const string PagingControlOid = "1.2.840.113556.1.4.319";
		public const string CrossDomainMoveTarget = "1.2.840.113556.1.4.521";
		public const string DirSyncOid = "1.2.840.113556.1.4.841";
		public const string DomainScope = "1.2.840.113556.1.4.1339";
		public const string ExtendedDN = "1.2.840.113556.1.4.529";
		public const string GetStats = "1.2.840.113556.1.4.970";
		public const string LazyCommit = "1.2.840.113556.1.4.619";
		public const string PermissiveModify = "1.2.840.113556.1.4.1413";
		public const string NotificationOid = "1.2.840.113556.1.4.528";
		public const string RespSort = "1.2.840.113556.1.4.474";
		public const string SdFlagsOid = "1.2.840.113556.1.4.801";
		public const string SearchOptions = "1.2.840.113556.1.4.1340";
		public const string Sort = "1.2.840.113556.1.4.473";
		public const string ShowDeletedOid = "1.2.840.113556.1.4.417";
		public const string TreeDelete = "1.2.840.113556.1.4.805";
		public const string VerifyName = "1.2.840.113556.1.4.1338";
		public const string VlvRequest = "2.16.840.1.113730.3.4.9";
		public const string VlvResponse = "2.16.840.1.113730.3.4.10";
		public const string Asq = "1.2.840.113556.1.4.1504";
		public const string QuotaControl = "1.2.840.113556.1.4.1852";
		public const string RangeOption = "1.2.840.113556.1.4.802";
		public const string ShutdownNotify = "1.2.840.113556.1.4.1907";
		public const string ForceUpdate = "1.2.840.113556.1.4.1974";
		public const string RangeRetrievalNoError = "1.2.840.113556.1.4.1948";
		public const string RodcDcPromo = "1.2.840.113556.1.4.1341";
		public const string DnInput = "1.2.840.113556.1.4.2026";
		public const string ShowDeactivatedLinkOid = "1.2.840.113556.1.4.2065";
		public const string ShowRecycledOid = "1.2.840.113556.1.4.2064";
		public const string PolicyHintsDeprecated = "1.2.840.113556.1.4.2066";
		public const string DirSyncEx = "1.2.840.113556.1.4.2090";
		public const string UpdateStats = "1.2.840.113556.1.4.2205";
		public const string TreeDeleteEx = "1.2.840.113556.1.4.2204";
		public const string SearchHints = "1.2.840.113556.1.4.2206";
		public const string ExpectedEntryCount = "1.2.840.113556.1.4.2211";
		public const string PolicyHints = "1.2.840.113556.1.4.2239";
		public const string SetOwner = "1.2.840.113556.1.4.2255";
		public const string BypassQuota = "1.2.840.113556.1.4.2256";
		public const string LinkTtl = "1.2.840.113556.1.4.2309";
		public const string SetCorrelationId = "1.2.840.113556.1.4.2330";
		public const string ThreadTraceOverride = "1.2.840.113556.1.4.2354";
	}
}
