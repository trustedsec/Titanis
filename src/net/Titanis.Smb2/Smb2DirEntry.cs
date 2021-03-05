using System;
using System.ComponentModel;
using System.Security.AccessControl;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Smb2
{
	public class Smb2DirEntry
	{

		[Browsable(false)]
		public string FileName { get; set; }

		private string? _relativePath;
		[DisplayName("Name")]
		public string RelativePath { get => _relativePath ?? this.FileName; set => _relativePath = value; }

		public uint FileIndex { get; set; }

		[DisplayName("Create Time")]
		public DateTime CreationTime { get; set; }

		public DateTime LastAccessTime { get; set; }

		[DisplayName("Last Write Time")]
		public DateTime LastWriteTime { get; set; }

		[DisplayName("Change Time")]
		public DateTime LastChangeTime { get; set; }

		[DisplayName("Size")]
		[FileSize]
		public ulong Size { get; set; }

		[DisplayName("Size (on disk)")]
		[FileSize]
		public ulong SizeOnDisk { get; set; }

		[DisplayName("Attr.")]
		public FileAttributes FileAttributes { get; set; }

		[Browsable(false)]
		public bool IsDirectory => (0 != (this.FileAttributes & FileAttributes.Directory));

		public uint EaSize { get; set; }

		[DisplayName("Short Name")]
		public string? ShortName { get; set; }

		[DisplayName("File ID")]
		[DisplayFormatString("0x{0:X16}")]
		public ulong FileId { get; set; }

		[DisplayName("Reparse Tag")]
		public ReparseTag ReparseTag { get; internal set; }

		[DisplayName("Target")]
		public string LinkTarget { get; internal set; }

		[Browsable(false)]
		public SecurityDescriptor SecurityDescriptor { get; set; }

		[DisplayName("Sec. Desc.")]
		public string? SecurityDescriptorSddl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.All);
		public SecurityIdentifier? Owner => this.SecurityDescriptor?.Owner;
		public SecurityIdentifier? Group => this.SecurityDescriptor?.Group;
		[DisplayName("DACL")]
		public string? Dacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Access);
		[DisplayName("SACL")]
		public string? Sacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Audit);

		[DisplayName("Max. Access")]
		public Smb2FileAccessRights MaxAccess { get; set; }
	}
}