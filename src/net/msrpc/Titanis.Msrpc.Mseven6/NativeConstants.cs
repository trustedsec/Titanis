using ms_dtyp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mseven6
{
	// [MS-EVEN6] § 6 Appendix A: Full IDL
	internal class NativeConstants
	{
		internal const int MAX_PAYLOAD = 2 * 1024 * 1024;
		internal const int MAX_RPC_QUERY_LENGTH = MAX_PAYLOAD / 2;
		internal const int MAX_RPC_CHANNEL_NAME_LENGTH = 512;
		internal const int MAX_RPC_QUERY_CHANNEL_SIZE = 512;
		internal const int MAX_RPC_EVENT_ID_SIZE = 256;
		internal const int MAX_RPC_FILE_PATH_LENGTH = 32768;
		internal const int MAX_RPC_CHANNEL_PATH_LENGTH = 32768;
		internal const int MAX_RPC_BOOKMARK_LENGTH = MAX_PAYLOAD / 2;
		internal const int MAX_RPC_PUBLISHER_ID_LENGTH = 2048;
		internal const int MAX_RPC_PROPERTY_BUFFER_SIZE = MAX_PAYLOAD;
		internal const int MAX_RPC_FILTER_LENGTH = MAX_RPC_QUERY_LENGTH;
		internal const int MAX_RPC_RECORD_COUNT = 1024;
		internal const int MAX_RPC_EVENT_SIZE = MAX_PAYLOAD;
		internal const int MAX_RPC_BATCH_SIZE = MAX_PAYLOAD;
		internal const int MAX_RPC_RENDERED_STRING_SIZE = MAX_PAYLOAD;
		internal const int MAX_RPC_CHANNEL_COUNT = 8192;
		internal const int MAX_RPC_PUBLISHER_COUNT = 8192;
		internal const int MAX_RPC_EVENT_METADATA_COUNT = 256;
		internal const int MAX_RPC_VARIANT_LIST_COUNT = 256;
		internal const int MAX_RPC_BOOL_ARRAY_COUNT = MAX_PAYLOAD / 1;
		internal const int MAX_RPC_UINT32_ARRAY_COUNT = MAX_PAYLOAD / 4;
		internal const int MAX_RPC_UINT64_ARRAY_COUNT = MAX_PAYLOAD / 8;
		internal const int MAX_RPC_STRING_ARRAY_COUNT = MAX_PAYLOAD / 512;
		internal const int MAX_RPC_GUID_ARRAY_COUNT = MAX_PAYLOAD / 16;
		internal const int MAX_RPC_STRING_LENGTH = MAX_PAYLOAD / 2;

		//const int MAX_RPC_BOOL_ARRAY_COUNT = MAX_PAYLOAD / sizeof(BOOL);
	}
}
