using System;
using System.Runtime.InteropServices;

namespace Titanis.Smb2
{
	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public partial struct Smb2FileHandle : IEquatable<Smb2FileHandle>
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

		public override bool Equals(object? obj)
		{
			return obj is Smb2FileHandle handle && Equals(handle);
		}

		public bool Equals(Smb2FileHandle other)
		{
			return low == other.low &&
				   high == other.high;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(low, high);
		}

		public static bool operator ==(Smb2FileHandle left, Smb2FileHandle right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Smb2FileHandle left, Smb2FileHandle right)
		{
			return !(left == right);
		}
	}
}