using ms_dtyp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc
{
	public static class RpcExtensions
	{
		public static string AsUtf8String(this ArraySegment<byte> array)
		{
			return Encoding.UTF8.GetString(array.AsSpan());
		}

		public static RPC_UNICODE_STRING ToRpcUnicodeString(this string str)
		{
			var chars = (str + '\0').ToCharArray();
			RPC_UNICODE_STRING rpcstr = new RPC_UNICODE_STRING
			{
				Buffer = new RpcPointer<ArraySegment<char>>(new ArraySegment<char>(chars, 0, str.Length)),
				Length = (ushort)(str.Length * 2),
				MaximumLength = (ushort)((str.Length + 1) * 2)
			};
			return rpcstr;
		}

		public static unsafe DateTime AsDateTime(this RpcPointer<LARGE_INTEGER> pInt64)
		{
			LARGE_INTEGER li = pInt64.value;
			return DateTime.FromFileTime(*(long*)&li);
		}
	}
}
