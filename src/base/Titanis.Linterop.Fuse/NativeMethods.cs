using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Linterop.Fuse;

namespace Titanis.Smb2.Fusion
{
	internal class NativeMethods
	{
		const string LibcName = "libc";

		[DllImport(LibcName)]
		internal static extern uint geteuid();
		[DllImport(LibcName)]
		internal static extern uint getegid();
		[DllImport(LibcName)]
		internal static extern uint stat([MarshalAs(UnmanagedType.LPStr)] string path, ref stat stat);
	}
}
