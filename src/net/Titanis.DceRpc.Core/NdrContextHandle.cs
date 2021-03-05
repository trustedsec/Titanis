using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.DceRpc
{
	// [C706] ndr_context_handle
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NdrContextHandle
	{
		public unsafe static int StructSize => sizeof(NdrContextHandle);

		internal int context_handle_attributes;
		internal Guid context_handle_uuid;

		public bool IsEmpty => this.context_handle_uuid == Guid.Empty;
	}
}
