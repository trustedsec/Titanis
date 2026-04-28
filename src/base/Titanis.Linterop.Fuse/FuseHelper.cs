using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	internal static class FuseHelper
	{
		public static void ThrowLastError()
		{
			var res = Marshal.GetLastWin32Error();
			var msg = Marshal.GetLastPInvokeErrorMessage();
			throw new Exception(msg);
		}
	}
}
