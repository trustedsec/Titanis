using ms_drsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.13.1.21 DS_REPL_OPW
	public class DsrepPendingOp
	{
		internal DsrepPendingOp(
			DateTime enqueuedTime,
			uint serialNumber,
			uint priority,
			DS_REPL_OP_TYPE opType,
			uint options,
			LdapDistinguishedName namingContextDn,
			LdapDistinguishedName dsaDn,
			string? dsaAddress,
			Guid namingContextId,
			Guid dsaId
			)
		{
			EnqueuedTime = enqueuedTime;
			SerialNumber = serialNumber;
			Priority = priority;
			OpType = opType;
			Options = options;
			NamingContextDn = namingContextDn;
			DsaDn = dsaDn;
			DsaAddress = dsaAddress;
			NamingContextId = namingContextId;
			DsaId = dsaId;
		}

		public DateTime EnqueuedTime { get; }
		public uint SerialNumber { get; }
		public uint Priority { get; }
		public DS_REPL_OP_TYPE OpType { get; }
		public uint Options { get; }
		public LdapDistinguishedName NamingContextDn { get; }
		public LdapDistinguishedName DsaDn { get; }
		public string? DsaAddress { get; }
		public Guid NamingContextId { get; }
		public Guid DsaId { get; }
	}
}
