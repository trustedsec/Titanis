using System.Runtime.InteropServices;

namespace Titanis.Smb2
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Smb2OpenFileAttributes
	{
		internal long creationTime;
		internal long lastAccessTime;
		internal long lastWriteTime;
		internal long changeTime;
		internal long allocationSize;
		internal long endOfFile;

		internal Winterop.FileAttributes fileAttributes;
	}
}
