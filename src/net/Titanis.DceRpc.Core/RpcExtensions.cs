using ms_dtyp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.DceRpc
{
	public static partial class RpcExtensions
	{
		public static string AsUtf8String(this ArraySegment<byte> array)
		{
			return Encoding.UTF8.GetString(array.AsSpan());
		}
		public static RpcPointer<string>? ToRpcPointerOrNull(this string? str)
		{
			return str != null ? new RpcPointer<string>(str) : null;
		}

		public static RPC_UNICODE_STRING ToRpcUnicodeString(this string? str)
		{
			if (str is null)
				return new RPC_UNICODE_STRING();

			var chars = (str + '\0').ToCharArray();
			RPC_UNICODE_STRING rpcstr = new RPC_UNICODE_STRING
			{
				Length = (ushort)(str.Length * 2),
				MaximumLength = (ushort)((str.Length + 1) * 2),
				Buffer = new RpcPointer<ArraySegment<char>>(new ArraySegment<char>(chars, 0, str.Length))
			};
			return rpcstr;
		}

		public static unsafe DateTime AsDateTime(this RpcPointer<LARGE_INTEGER> pInt64)
		{
			LARGE_INTEGER li = pInt64.value;
			return DateTime.FromFileTime(*(long*)&li);
		}

		public static string? AsString(this in RPC_UNICODE_STRING rpcString, bool trimNull = false)
		{
			var arr = rpcString.Buffer?.value.Array;
			if (arr == null)
				return null;
			else
			{
				var arrseg = rpcString.Buffer!.value;
				var count = arrseg.Count;
				if (trimNull && count > 0 && arr[count - 1] == '\0')
					count--;
				return new string(arr, arrseg.Offset, count);
			}
		}

		public static RPC_UNICODE_STRING AsRpcString(this string str)
		{
			if (str == null)
			{
				return new RPC_UNICODE_STRING();
			}
			else
			{
				int cb = Encoding.Unicode.GetByteCount(str);
				return new RPC_UNICODE_STRING
				{
					Length = (ushort)cb,
					MaximumLength = (ushort)(cb + 2),
					Buffer = new DceRpc.RpcPointer<System.ArraySegment<char>>(new System.ArraySegment<char>(
						(str + '\0').ToCharArray(), 0, str.Length
						))
				};
			}
		}

		public static DateTime? ToDateTimeOrNull(this FILETIME ft)
			=> (
				((ft.dwLowDateTime == uint.MaxValue) && (ft.dwHighDateTime == int.MaxValue))
				|| ((ft.dwLowDateTime == 0) && (ft.dwHighDateTime == 0))
			) ? null
			: DateTime.FromFileTimeUtc((((long)ft.dwHighDateTime) << 32) | ft.dwLowDateTime);

		public static DateTime ToDateTime(this FILETIME ft)
			=> DateTime.FromFileTimeUtc((((long)ft.dwHighDateTime) << 32) | ft.dwLowDateTime);
	}
}
