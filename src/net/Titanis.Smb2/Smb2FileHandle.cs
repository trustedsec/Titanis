using System;
using System.Runtime.InteropServices;

namespace Titanis.Smb2
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public record struct Smb2FileHandle
	{
		internal static unsafe int StructSize => sizeof(Smb2FileHandle);
		internal static Smb2FileHandle Invalid => new Smb2FileHandle(0xFFFFFFFF_FFFFFFFF, 0xFFFFFFFF_FFFFFFFF);

		public Smb2FileHandle(ulong low, ulong high)
		{
			this.low = low;
			this.high = high;
		}

		public ulong low;
		public ulong high;

		public unsafe Span<byte> AsSpan()
		{
			fixed (ulong* pStruc = &this.low)
			{
				return new Span<byte>((byte*)pStruc, StructSize);
			}
		}
	}
}