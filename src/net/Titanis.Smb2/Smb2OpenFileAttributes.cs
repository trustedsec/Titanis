using System.Runtime.InteropServices;

namespace Titanis.Smb2
{
	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public partial struct Smb2OpenFileAttributes
	{
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public long allocationSize;
		public long endOfFile;

		public Winterop.FileAttributes fileAttributes;
	}
}
