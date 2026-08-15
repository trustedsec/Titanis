using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Msrpc.Msdrsr
{
	public enum DsrepObjectMetadataLevel : uint
	{
		Metadata = ReplicationInfoKind.MetadataForObject,
		Metadata2 = ReplicationInfoKind.Metadata2ForObject,
	}

	// [MS-DRSR] § 4.1.13.1.14 DS_REPL_OBJ_META_DATA
	// [MS-DRSR] § 4.1.13.1.16 DS_REPL_OBJ_META_DATA_2
	public class DsrepObjectMetadata
	{
		internal DsrepObjectMetadata(
			string? attributeName,
			int version,
			DateTime dateTime,
			Guid lastOriginatingDsaInvocationId,
			long originatingChangeUsn,
			long localChangeUsn)
		{
			AttributeName = attributeName;
			Version = version;
			DateTime = dateTime;
			LastOriginatingDsaInvocationId = lastOriginatingDsaInvocationId;
			OriginatingChangeUsn = originatingChangeUsn;
			LocalChangeUsn = localChangeUsn;
		}

		internal DsrepObjectMetadata(
			string? attributeName,
			int version,
			DateTime dateTime,
			Guid lastOriginatingDsaInvocationId,
			long originatingChangeUsn,
			long localChangeUsn,
			LdapDistinguishedName? lastOriginatingDsa
			)
			: this(attributeName, version, dateTime, lastOriginatingDsaInvocationId, originatingChangeUsn, localChangeUsn)
		{
			LastOriginatingDsa = lastOriginatingDsa;
		}

		public string? AttributeName { get; }
		public int Version { get; }
		public DateTime DateTime { get; }
		public Guid LastOriginatingDsaInvocationId { get; }
		public long OriginatingChangeUsn { get; }
		public long LocalChangeUsn { get; }
		public LdapDistinguishedName? LastOriginatingDsa { get; }
	}
}
