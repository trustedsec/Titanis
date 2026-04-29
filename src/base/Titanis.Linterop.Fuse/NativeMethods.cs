using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	public class NativeMethods
	{
		const string LibcName = "libc";

		[DllImport(LibcName)]
		public static extern uint geteuid();
		[DllImport(LibcName)]
		public static extern uint getegid();
		[DllImport(LibcName)]
		internal static extern uint stat([MarshalAs(UnmanagedType.LPStr)] string path, ref stat stat);
	}
}
