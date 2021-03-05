using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop
{
	// [MS-FSCC] § 2.7.1. FILE_NOTIFY_INFORMATION
	public enum FileNotifyAction : uint
	{
		Added = 1,
		Removed = 2,
		Modified = 3,
		RenamedOldName = 4,
		RenamedNewName = 5,
		AddedStream = 6,
		RemovedStream = 7,
		ModifiedStream = 8,
		RemovedByDelete = 9,
		IdNotTunnelled = 0x0A,
		TunnelledIdCollision = 0x08,
	}

	// [MS-FSCC] § 2.7.1. FILE_NOTIFY_INFORMATION
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FileNotifyInfoHeader
	{
		public unsafe static int StructSize => sizeof(FileNotifyInfoHeader);

		public int nextEntryOffset;
		public FileNotifyAction action;
		public int fileNameLength;
	}

	/// <summary>
	/// Describes a change that occurred within a directory.
	/// </summary>
	public class FileChangeNotification
	{
		public FileChangeNotification(FileNotifyAction action, string fileName)
		{
			this.Action = action;
			this.FileName = fileName;
		}
		public FileChangeNotification(FileNotifyAction action, string fileName, string? oldFileName)
		{
			this.Action = action;
			this.FileName = fileName;
		}

		/// <summary>
		/// Gets a <see cref="FileNotifyAction"/> value that specifies what type of change occurred.
		/// </summary>
		public FileNotifyAction Action { get; }
		/// <summary>
		/// Gets the name of the changed file.
		/// </summary>
		/// <remarks>
		/// For 
		/// </remarks>
		public string FileName { get; }
		/// <summary>
		/// Gets the old name of a renamed file.
		/// </summary>
		public string? OldFileName { get; }

		/// <inheritdoc/>
		public override string ToString()
			=> this.OldFileName != null ? $"{this.Action}: {this.OldFileName} => {this.FileName}"
			: $"{this.Action}: {this.FileName}";
	}
}
