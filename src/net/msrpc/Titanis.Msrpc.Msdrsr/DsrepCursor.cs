using Titanis.Ldap;

namespace Titanis.Msrpc.Msdrsr
{
	public enum DsrepCursorLevel : uint
	{
		Cursor = ReplicationInfoKind.CursorsForNC,
		Cursor2 = ReplicationInfoKind.Cursors2ForNC,
		Cursor3 = ReplicationInfoKind.Cursors3ForNC,
	}

	// [MS-DRSR] § 4.1.13.1.9 DS_REPL_CURSOR
	public class DsrepCursor
	{
		internal DsrepCursor(
			Guid sourceDsaInvocationId,
			long attributeFilterUsn
			)
		{
			SourceDsaInvocationId = sourceDsaInvocationId;
			AttributeFilterUsn = attributeFilterUsn;
		}
		internal DsrepCursor(
			Guid sourceDsaInvocationId,
			long attributeFilterUsn,
			DateTime lastSyncSuccessTime
			)
		{
			SourceDsaInvocationId = sourceDsaInvocationId;
			AttributeFilterUsn = attributeFilterUsn;
			LastSyncSuccessTime = lastSyncSuccessTime;
		}
		internal DsrepCursor(
			Guid sourceDsaInvocationId,
			long attributeFilterUsn,
			DateTime lastSyncSuccessTime,
			LdapDistinguishedName? sourceDsa
			)
		{
			SourceDsaInvocationId = sourceDsaInvocationId;
			AttributeFilterUsn = attributeFilterUsn;
			LastSyncSuccessTime = lastSyncSuccessTime;
			SourceDsa = sourceDsa;
		}

		public Guid SourceDsaInvocationId { get; }
		public long AttributeFilterUsn { get; }
		public DateTime? LastSyncSuccessTime { get; }
		public LdapDistinguishedName? SourceDsa { get; }
	}
}