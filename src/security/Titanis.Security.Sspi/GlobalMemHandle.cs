using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Sspi
{
	internal class GlobalMemHandle : SafeHandle
	{
		public GlobalMemHandle() : base(-1, true)
		{
		}

		public GlobalMemHandle(IntPtr hgbl, int size)
			: base(-1, true)
		{
			this.handle = hgbl;
			Size = size;
		}

		public static GlobalMemHandle Alloc(ReadOnlySpan<byte> bytes)
		{
			var hgbl = Marshal.AllocHGlobal(bytes.Length);
			unsafe
			{
				fixed (byte* pBytes = bytes)
				{
					Unsafe.CopyBlock(hgbl.ToPointer(), pBytes, (uint)bytes.Length);
				}
			}
			return new GlobalMemHandle(hgbl, bytes.Length);
		}

		public override bool IsInvalid => this.handle == -1;

		public int Size { get; }

		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}
	}
}
