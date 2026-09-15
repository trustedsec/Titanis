using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mseven6
{
	// [MS-EVEN6] § 2.2.17 Result Set
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct ResultSet
	{
		internal int totalSize;
		internal int headerSize;
		internal int eventOffset;
		internal int bookmarkOffset;
		internal int binXmlSize;
		[PduArraySize(nameof(binXmlSize))]
		internal byte[] eventData;
		internal int numberOfSubqueryIds;
		[PduArraySize(nameof(numberOfSubqueryIds))]
		internal uint[] subqueryIds;
		internal BookmarkData bookmark;

	}

	// [MS-EVEN6] § 2.2.17 Result Set
	enum BookmarkReadDirection : int
	{
		Chronological = 0,
		Reverse = 1,
	}

	// [MS-EVEN6] § 2.2.17 Result Set
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct BookmarkData
	{
		internal int bookmarkSize;
		internal int headerSize;
		internal int channelSize;
		internal int currentChannel;
		internal BookmarkReadDirection readDirection;
		internal int recordIdsOffset;
		private int LogRecordCount => (this.bookmarkSize - this.headerSize) / 8;
		[PduArraySize(nameof(LogRecordCount))]
		internal ulong[] logRecordNumbers;
	}

}
