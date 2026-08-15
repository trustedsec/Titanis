using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.13.1.4 DS_REPL_INFO Codes
	internal enum ReplicationInfoKind : uint
	{
		Neighbors = 0,
		CursorsForNC = 1,
		MetadataForObject = 2,
		KccDsaConnectFailures = 3,
		KccDsaLinkFailures = 4,
		PendingOps = 5,
		MetadataForAttributeValue = 6,
		Cursors2ForNC = 7,
		Cursors3ForNC = 8,
		Metadata2ForObject = 9,
		Metadata2ForAttributeValue = 10,

		ServerOutgoingCalls = 0xFFFFFFFA,
		UpToDateVectorV1 = 0xFFFFFFFB,
		ClientContexts = 0xFFFFFFFC,
		RepsTo = 0xFFFFFFFE,
	}
}
