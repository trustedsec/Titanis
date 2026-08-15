using ms_drsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdrsr
{
	public enum DsrepKccFailureKind : uint
	{
		Connect = ReplicationInfoKind.KccDsaConnectFailures,
		Link = ReplicationInfoKind.KccDsaLinkFailures,
	}

	// [MS-DRSR] § 4.1.13.1.19 DS_REPL_KCC_DSA_FAILUREW
	public class DsrepKccFailure
	{
		internal DsrepKccFailure(
			LdapDistinguishedName dsaDn,
			Guid dsaObjectGuid,
			DateTime firstFailureTime,
			uint failureCount,
			Win32ErrorCode lastResult
			)
		{
			DsaDn = dsaDn;
			DsaObjectGuid = dsaObjectGuid;
			FirstFailureTime = firstFailureTime;
			FailureCount = failureCount;
			LastResult = lastResult;
		}

		public LdapDistinguishedName DsaDn { get; }
		public Guid DsaObjectGuid { get; }
		public DateTime FirstFailureTime { get; }
		public uint FailureCount { get; }
		public Win32ErrorCode LastResult { get; }
	}
}
