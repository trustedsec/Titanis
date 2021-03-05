using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct PortAny
	{
		internal byte[] spec;

		public PortAny(byte[] spec)
		{
			this.spec = spec;
		}
	}
}