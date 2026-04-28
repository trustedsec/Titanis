using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	class FuseSessionHandle : SafeHandle
	{
		public FuseSessionHandle() : base(IntPtr.Zero, true)
		{ }

		public override bool IsInvalid => this.handle == IntPtr.Zero;

		protected override bool ReleaseHandle()
		{
			FuseNativeMethods.fuse_session_destroy(this.handle);
			return true;
		}
	}
}
