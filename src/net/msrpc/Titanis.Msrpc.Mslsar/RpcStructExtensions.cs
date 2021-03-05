using ms_dtyp;
using ms_lsar;
using System;
using System.Text;

namespace Titanis.Msrpc.Mslsar
{
	static class RpcStructExtensions
	{
		public static string AsString(this in RPC_UNICODE_STRING rpcString)
		{
			var arr = rpcString.Buffer?.value.Array;
			if (arr == null)
				return null;
			else
			{
				var arrseg = rpcString.Buffer.value;
				return new string(arr, arrseg.Offset, arrseg.Count);
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
	}
}