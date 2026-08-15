using Titanis.Ldap;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] 4.1.13.1.7 DS_REPL_NEIGHBORW
	public class DsrepNeighbor
	{
		internal DsrepNeighbor(
			LdapDistinguishedName namingContext,
			LdapDistinguishedName neighborDsaName,
			LdapDistinguishedName neighborDsaAddress,
			LdapDistinguishedName? asyncIntersiteTransport,
			DrsOptions replicaFlags,
			Guid namingContextGuid,
			Guid neighborDsaObjectGuid,
			Guid neighborDsaInvocationId,
			Guid asyncIntersiteTransportObjectGuid,
			long lastObjectChangeUsn,
			long attributeFilterUsn,
			DateTime lastSyncSuccessTime,
			DateTime lastSyntAttemptTime,
			Win32ErrorCode lastSyncResult
			)
		{
			NamingContext = namingContext;
			NeighborDsaName = neighborDsaName;
			NeighborDsaAddress = neighborDsaAddress;
			AsyncIntersiteTransport = asyncIntersiteTransport;
			ReplicaFlags = replicaFlags;
			NamingContextGuid = namingContextGuid;
			NeighborDsaObjectGuid = neighborDsaObjectGuid;
			NeighborDsaInvocationId = neighborDsaInvocationId;
			AsyncIntersiteTransportObjectGuid = asyncIntersiteTransportObjectGuid;
			LastObjectChangeUsn = lastObjectChangeUsn;
			AttributeFilterUsn = attributeFilterUsn;
			LastSyncSuccessTime = lastSyncSuccessTime;
			LastSyntAttemptTime = lastSyntAttemptTime;
			LastSyncResult = lastSyncResult;
		}

		public LdapDistinguishedName NamingContext { get; }
		public LdapDistinguishedName NeighborDsaName { get; }
		public LdapDistinguishedName NeighborDsaAddress { get; }
		public LdapDistinguishedName? AsyncIntersiteTransport { get; }
		public DrsOptions ReplicaFlags { get; }
		public Guid NamingContextGuid { get; }
		public Guid NeighborDsaObjectGuid { get; }
		public Guid NeighborDsaInvocationId { get; }
		public Guid AsyncIntersiteTransportObjectGuid { get; }
		public long LastObjectChangeUsn { get; }
		public long AttributeFilterUsn { get; }
		public DateTime LastSyncSuccessTime { get; }
		public DateTime LastSyntAttemptTime { get; }
		public Win32ErrorCode LastSyncResult { get; }
	}
}
