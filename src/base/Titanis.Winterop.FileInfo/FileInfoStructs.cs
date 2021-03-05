using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileNamesInfo
	{
		public static unsafe int StructSize => sizeof(FileNamesInfo);

		public int nextEntryOffset;
		public uint fileIndex;
		public int fileNameLength;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileDirectoryInfo
	{
		public static unsafe int StructSize => sizeof(FileDirectoryInfo);

		public int nextEntryOffset;
		public uint fileIndex;
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong endOfFile;
		public ulong allocationSize;
		public FileAttributes fileAttributes;
		public int fileNameLength;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileNetworkOpenInfo
	{
		public static unsafe int StructSize => sizeof(FileNetworkOpenInfo);

		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong allocationSize;
		public ulong endOfFile;
		public FileAttributes fileAttributes;
		public int reserved;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileFullDirectoryInfo
	{
		public static unsafe int StructSize => sizeof(FileFullDirectoryInfo);

		public int nextEntryOffset;
		public uint fileIndex;
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong endOfFile;
		public ulong allocationSize;
		public FileAttributes fileAttributes;
		public int fileNameLength;
		public uint eaSize;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileIdFullDirectoryInfo
	{
		public static unsafe int StructSize => sizeof(FileIdFullDirectoryInfo);

		public int nextEntryOffset;
		public uint fileIndex;
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong endOfFile;
		public ulong allocationSize;
		public FileAttributes fileAttributes;
		public int fileNameLength;
		public uint eaSize;
		public uint reserved;
		public ulong fileId;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileBothDirectoryInfo : IHaveShortName
	{
		public static unsafe int StructSize => sizeof(FileBothDirectoryInfo);

		public int nextEntryOffset;
		public uint fileIndex;
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong endOfFile;
		public ulong allocationSize;
		public FileAttributes fileAttributes;
		public int fileNameLength;
		public uint eaSize;
		public byte shortNameLength;
		public byte reserved;
		public unsafe fixed byte shortName[24];

		public int ShortNameLength => this.shortNameLength;

		public Encoding ShortNameEncoding => Encoding.Unicode;

		public unsafe ref byte GetShortNameRef()
		{
			fixed (byte* pName = this.shortName)
			{
				return ref *pName;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileIdBothDirectoryInfo : IHaveShortName
	{
		public static unsafe int StructSize => sizeof(FileIdBothDirectoryInfo);


		public int nextEntryOffset;
		public uint fileIndex;
		public long creationTime;
		public long lastAccessTime;
		public long lastWriteTime;
		public long changeTime;
		public ulong endOfFile;
		public ulong allocationSize;
		public FileAttributes fileAttributes;
		public int fileNameLength;
		public uint eaSize;
		public byte shortNameLength;
		public byte reserved;
		public unsafe fixed byte shortName[24];
		public ushort reserved2;
		public ulong fileId;

		public int ShortNameLength => this.shortNameLength;

		public Encoding ShortNameEncoding => Encoding.Unicode;

		public unsafe ref byte GetShortNameRef()
		{
			fixed (byte* pName = this.shortName)
			{
				return ref *pName;
			}
		}
	}
}
