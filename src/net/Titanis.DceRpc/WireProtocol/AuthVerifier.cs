using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	internal partial class AuthVerifier
	{
		internal readonly AuthVerifierHeader hdr;
		internal readonly byte[] token;

		public AuthVerifier(in AuthVerifierHeader hdr, byte[] token)
		{
			this.hdr = hdr;
			this.token = token;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	[PduStruct]
	public partial struct AuthVerifierHeader
	{
		// [MS-RPCE] § 2.2.2.11 - sec_trailer Structure
		internal const int Alignment = 16;

		internal RpcAuthType auth_type; /* :01 which authent service */
		internal RpcAuthLevel auth_level; /* :01 which level within service */
		internal byte auth_pad_length; /* :01 */
		internal byte auth_reserved; /* :01 reserved, m.b.z. */
		internal uint auth_context_id; /* :04 */
	}
}
