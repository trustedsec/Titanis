using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.WinFileInfo;

namespace Titanis.Smb2.PowerShell
{
	public class SmbItem
	{
		private UncPath itemPath;
		private Smb2DirEntry entry;

		public SmbItem(UncPath itemPath, Smb2DirEntry entry)
		{
			this.itemPath = itemPath;
			this.entry = entry;
		}

		public string? Name => this.entry.FileName;
		public DateTime CreationTime => this.entry.CreationTime;
		public DateTime LastAccessTime => this.entry.LastAccessTime;
		public DateTime LastWriteTime => this.entry.LastWriteTime;
		public DateTime LastChangeTime => this.entry.LastChangeTime;
		public ulong Size => this.entry.Size;
		public string SizeText => Titanis.Cli.FileSizeFormatter.FormatValue(this.entry.Size, "H2");
		public ulong SizeOnDisk => this.entry.SizeOnDisk;
		public WinFileInfo.FileAttributes FileAttributes => this.entry.FileAttributes;
		public bool IsDirectory => (0 != (this.FileAttributes & WinFileInfo.FileAttributes.Directory));
		public bool IsReparsePoint => (0 != (this.FileAttributes & WinFileInfo.FileAttributes.ReparsePoint));

		public string FileAttributesText => FileAttributeFormatter.FormatValue(this.FileAttributes);
		public string? ShortName => this.entry.ShortName;
		public ulong FileId => this.entry.FileId;
		public ReparseTag ReparseTag => this.entry.ReparseTag;
		public string LinkTarget => this.entry.LinkTarget;

		public string ItemClass
		{
			get
			{
				if (this.IsDirectory)
				{
					if (this.IsReparsePoint)
					{
						if (this.ReparseTag == ReparseTag.SymbolicLink)
						{
							return SmbItemClasses.SymlinkDir;
						}
						else if (this.ReparseTag == ReparseTag.MountPoint)
						{
							if (this.LinkTarget?.StartsWith(@"\??\Volume") ?? false)
								return SmbItemClasses.MountPoint;
							else
								return SmbItemClasses.Junction;
						}
					}

					return SmbItemClasses.Directory;
				}
				else
				{
					if (this.IsReparsePoint)
					{
						if (this.ReparseTag == ReparseTag.SymbolicLink)
						{
							return SmbItemClasses.Symlink;
						}
					}
				}

				return SmbItemClasses.File;
			}
		}
	}
}
