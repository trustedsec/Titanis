using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct PortAnyHeader
	{
		public static unsafe int StructSize => sizeof(PortAnyHeader);

		internal ushort length;
	}
}