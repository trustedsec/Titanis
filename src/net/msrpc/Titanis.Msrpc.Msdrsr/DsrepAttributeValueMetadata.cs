using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Msrpc.Msdrsr
{
	public class DsrepAttributeMetadataResult
	{
		internal DsrepAttributeMetadataResult(
			uint enumContext,
			DsrepAttributeValueMetadata[] values
			)
		{
			EnumContext = enumContext;
			Values = values;
		}

		public uint EnumContext { get; }
		public DsrepAttributeValueMetadata[] Values { get; }
	}

	// [MS-DRSR] § 4.1.13.1.23 DS_REPL_VALUE_META_DATA
	// [MS-DRSR] § 4.1.13.1.25 DS_REPL_VALUE_META_DATA_2
	public class DsrepAttributeValueMetadata
	{
		internal DsrepAttributeValueMetadata(
			string attributeName,
			LdapDistinguishedName objectDn,
			byte[]? data,
			DateTime? deletedTime,
			DateTime createdTime,
			int version,
			DateTime lastOriginatingChange,
			Guid lastOriginatingDsaInvocationId,
			long originatingChangeUsr,
			long localChangeUsn)
		{
			AttributeName = attributeName;
			ObjectDn = objectDn;
			Data = data;
			DeletedTime = deletedTime;
			CreatedTime = createdTime;
			Version = version;
			LastOriginatingChange = lastOriginatingChange;
			LastOriginatingDsaInvocationId = lastOriginatingDsaInvocationId;
			OriginatingChangeUsr = originatingChangeUsr;
			LocalChangeUsn = localChangeUsn;
		}
		internal DsrepAttributeValueMetadata(
			string attributeName,
			LdapDistinguishedName objectDn,
			byte[]? data,
			DateTime? deletedTime,
			DateTime createdTime,
			int version,
			DateTime lastOriginatingChange,
			Guid lastOriginatingDsaInvocationId,
			long originatingChangeUsr,
			long localChangeUsn,
			LdapDistinguishedName lastOriginatingDsaDn
			)
		{
			AttributeName = attributeName;
			ObjectDn = objectDn;
			Data = data;
			DeletedTime = deletedTime;
			CreatedTime = createdTime;
			Version = version;
			LastOriginatingChange = lastOriginatingChange;
			LastOriginatingDsaInvocationId = lastOriginatingDsaInvocationId;
			OriginatingChangeUsr = originatingChangeUsr;
			LocalChangeUsn = localChangeUsn;
			LastOriginatingDsaDn = lastOriginatingDsaDn;
		}

		public string AttributeName { get; }
		public LdapDistinguishedName ObjectDn { get; }
		public byte[]? Data { get; }
		public DateTime? DeletedTime { get; }
		public DateTime CreatedTime { get; }
		public int Version { get; }
		public DateTime LastOriginatingChange { get; }
		public Guid LastOriginatingDsaInvocationId { get; }
		public long OriginatingChangeUsr { get; }
		public long LocalChangeUsn { get; }
		public LdapDistinguishedName LastOriginatingDsaDn { get; }
	}
}
