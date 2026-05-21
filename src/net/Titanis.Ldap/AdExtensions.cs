namespace Titanis.Ldap
{
	// [MS-ADTS] § 3.1.1.3.4.1 LDAP Extended Controls
	static class AdExtensions
	{
		public const string PagingControlOid = "1.2.840.113556.1.4.319";
		public const string NotificationOid = "1.2.840.113556.1.4.528";
		public const string ShowDeletedOid = "1.2.840.113556.1.4.417";
		public const string ShowRecycledOid = "1.2.840.113556.1.4.2064";
		public const string ShowDeactivatedLinkOid = "1.2.840.113556.1.4.2065";
		public const string DirSyncOid = "1.2.840.113556.1.4.841";
		public const string SdFlagsOid = "1.2.840.113556.1.4.801";
	}
}
