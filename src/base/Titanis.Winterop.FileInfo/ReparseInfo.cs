using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop
{
	public abstract class ReparseInfo
	{
		protected ReparseInfo(ReparseTag tag)
		{
			this.Tag = tag;
		}

		public ReparseTag Tag { get; }

		public static ReparseInfo Parse(ReadOnlySpan<byte> bytes)
		{
			// [MS-FSCC] § 2.1.2.2 - REPARSE_DATA_BUFFER
			var tag = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var reparseLength = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(4, 2));

			// TODO: Enforce reparseLength

			if (0 == (tag & 0x8000_0000))
			{
				// [MS-FSCC] § 2.1.2.3 - REPARSE_GUID_DATA_BUFFER
				Guid guid = MemoryMarshal.Read<Guid>(bytes.Slice(8, 16));
				var info = new GenericGuidReparseInfo((ReparseTag)tag, guid, bytes.ToArray());
				return info;
			}
			else
			{
				var reparseData = bytes.Slice(8, reparseLength);
				switch ((ReparseTag)tag)
				{
					case ReparseTag.SymbolicLink:
						return SymbolicLinkInfo.ParseSymbolicLink(bytes);
					case ReparseTag.MountPoint:
						return MountPointInfo.ParseMountPoint(bytes);
					default:
						return new GenericReparseInfo((ReparseTag)tag, bytes.Slice(4).ToArray());
				}
			}
		}

		/// <summary>
		/// Converts the reparse info into a byte array.
		/// </summary>
		public abstract byte[] ToByteArray();
	}

	// [MS-FSCC] Š 2.1.2.4 Symbolic Link Reparse Data Buffer
	[Flags]
	public enum SymbolicLinkFlags : uint
	{
		FullPathName = 0,
		RelativePath = 1,
	}

	// [MS-FSCC] Š 2.1.2.4 Symbolic Link Reparse Data Buffer
	public sealed class SymbolicLinkInfo : ReparseInfo
	{
		public SymbolicLinkInfo(SymbolicLinkFlags flags, string substituteName, string printName, ushort unusedLength = 0)
			: base(ReparseTag.SymbolicLink)
		{
			if (substituteName is null) throw new ArgumentNullException(nameof(substituteName));
			if (printName is null) throw new ArgumentNullException(nameof(printName));

			if (substituteName.Length > ushort.MaxValue)
				throw new ArgumentException("The length of the name must fit within a 16-bit integer", nameof(substituteName));
			if (printName.Length > ushort.MaxValue)
				throw new ArgumentException("The length of the name must fit within a 16-bit integer", nameof(printName));
			if (substituteName.Length + printName.Length > ushort.MaxValue)
				throw new ArgumentException("The combined length of the substitute and print name must fit within a 16-bit integer", nameof(printName));

			this.Flags = flags;
			this.SubstituteName = substituteName;
			this.PrintName = printName;
			this.UnusedPathLength = unusedLength;
		}

		public SymbolicLinkFlags Flags { get; }
		public string SubstituteName { get; }
		public string PrintName { get; }
		public ushort UnusedPathLength { get; }

		internal static SymbolicLinkInfo ParseSymbolicLink(ReadOnlySpan<byte> bytes)
		{
			// [MS-SMB2] § 2.2.2.2.1
			ushort unusedLength = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(6, 2));

			// [MS-FSCC] § 2.1.2.4 Symbolic Link Reparse Data Buffer
			int offSubstName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(8, 2));
			int cbSubstName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(10, 2));
			int offPrintName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(12, 2));
			int cbPrintName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(14, 2));
			SymbolicLinkFlags flags = (SymbolicLinkFlags)BinaryPrimitives.ReadUInt32LittleEndian(bytes.Slice(16, 4));

			string substName = Encoding.Unicode.GetString(bytes.Slice(20 + offSubstName, cbSubstName));
			string printName = Encoding.Unicode.GetString(bytes.Slice(20 + offPrintName, cbPrintName));

			return new SymbolicLinkInfo(flags, substName, printName, unusedLength);
		}

		public sealed override byte[] ToByteArray()
		{
			int cbSubstName = Encoding.Unicode.GetByteCount(this.SubstituteName);
			int cbPrintName = Encoding.Unicode.GetByteCount(this.PrintName);

			byte[] bytes = new byte[20 + cbSubstName + cbPrintName + 2 + 2];
			Span<byte> byteSpan = bytes;
			BinaryPrimitives.WriteUInt32LittleEndian(byteSpan.Slice(0, 4), (uint)ReparseTag.SymbolicLink);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(4, 2), (ushort)(cbSubstName + cbPrintName + 12 + 2 + 2));
			// Reserved (2)
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(8, 2), 0);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(10, 2), (ushort)cbSubstName);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(12, 2), (ushort)(cbSubstName + 2));
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(14, 2), (ushort)cbPrintName);
			BinaryPrimitives.WriteUInt32LittleEndian(byteSpan.Slice(16, 4), (uint)this.Flags);

			Encoding.Unicode.GetBytes(this.SubstituteName, byteSpan.Slice(20, cbSubstName));
			Encoding.Unicode.GetBytes(this.PrintName, byteSpan.Slice(20 + cbSubstName + 2, cbPrintName));

			return bytes;
		}
	}

	// [MS-FSCC] Š 2.1.2.4 Mount Point Reparse Data Buffer
	public class MountPointInfo : ReparseInfo
	{
		public MountPointInfo(string substituteName, string printName)
			: base(ReparseTag.MountPoint)
		{
			if (substituteName is null) throw new ArgumentNullException(nameof(substituteName));
			if (printName is null) throw new ArgumentNullException(nameof(printName));

			if (substituteName.Length > ushort.MaxValue)
				throw new ArgumentException("The length of the name must fit within a 16-bit integer", nameof(substituteName));
			if (printName.Length > ushort.MaxValue)
				throw new ArgumentException("The length of the name must fit within a 16-bit integer", nameof(printName));
			if (substituteName.Length + printName.Length > ushort.MaxValue)
				throw new ArgumentException("The combined length of the substitute and print name must fit within a 16-bit integer", nameof(printName));

			this.SubstituteName = substituteName;
			this.PrintName = printName;
		}

		public string SubstituteName { get; }
		public string PrintName { get; }

		public static ReparseInfo ParseMountPoint(ReadOnlySpan<byte> bytes)
		{
			// [MS-FSCC] Š 2.1.2.4 Mount Point Reparse Data Buffer
			int offSubstName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(8, 2));
			int cbSubstName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(10, 2));
			int offPrintName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(12, 2));
			int cbPrintName = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(14, 2));

			string substName = Encoding.Unicode.GetString(bytes.Slice(16 + offSubstName, cbSubstName));
			string printName = Encoding.Unicode.GetString(bytes.Slice(16 + offPrintName, cbPrintName));

			return new MountPointInfo(substName, printName);
		}

		public sealed override byte[] ToByteArray()
		{
			int cbSubstName = Encoding.Unicode.GetByteCount(this.SubstituteName);
			int cbPrintName = Encoding.Unicode.GetByteCount(this.PrintName);

			byte[] bytes = new byte[16 + cbSubstName + 2 + cbPrintName + 2];
			Span<byte> byteSpan = bytes;
			BinaryPrimitives.WriteUInt32LittleEndian(byteSpan.Slice(0, 4), (uint)ReparseTag.MountPoint);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(4, 2), (ushort)(8 + cbSubstName + 2 + cbPrintName + 2));
			// Reserved (2)
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(8, 2), 0);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(10, 2), (ushort)cbSubstName);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(12, 2), (ushort)(cbSubstName + 2));
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(14, 2), (ushort)cbPrintName);

			Encoding.Unicode.GetBytes(this.SubstituteName, byteSpan.Slice(16, cbSubstName));
			Encoding.Unicode.GetBytes(this.PrintName, byteSpan.Slice(16 + cbSubstName + 2, cbPrintName));

			return bytes;
		}
	}

	// [MS-FSCC] § 2.1.2.3 - REPARSE_GUID_DATA_BUFFER
	public class GenericGuidReparseInfo : ReparseInfo
	{
		public GenericGuidReparseInfo(ReparseTag tag, Guid guid, byte[] data) : base(tag)
		{
			if (data is null) throw new ArgumentNullException(nameof(data));
			if ((int)tag < 0)
				throw new ArgumentException("The tag for a reparse point with a GUID must not have the high bit set.", nameof(tag));
			if (data.Length > ushort.MaxValue)
				throw new ArgumentException("The array is too large.  The length cannot exceed the maximum value of a 16-bit integer.", nameof(tag));

			this.Guid = guid;
			this.Data = data;
		}

		public Guid Guid { get; }
		public byte[] Data { get; }

		public override byte[] ToByteArray()
		{
			int cbData = this.Data.Length;

			byte[] bytes = new byte[8 + 16 + cbData];
			Span<byte> byteSpan = bytes;
			BinaryPrimitives.WriteUInt32LittleEndian(byteSpan.Slice(0, 4), (uint)this.Tag);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(2, 2), (ushort)cbData);
			// Reserved (2)
			this.Guid.TryWriteBytes(byteSpan.Slice(8, 16));
			this.Data.CopyTo(byteSpan.Slice(8 + 16));

			return bytes;
		}
	}

	// [MS-FSCC] § 2.1.2.2 - REPARSE_DATA_BUFFER
	public class GenericReparseInfo : ReparseInfo
	{
		public GenericReparseInfo(ReparseTag tag, byte[] data) : base(tag)
		{
			if (data is null) throw new ArgumentNullException(nameof(data));
			if ((int)tag >= 0)
				throw new ArgumentException("The tag for a reparse point with a GUID must have the high bit set.", nameof(tag));
			if (data.Length > ushort.MaxValue)
				throw new ArgumentException("The array is too large.  The length cannot exceed the maximum value of a 16-bit integer.", nameof(tag));

			this.Data = data;
		}

		public byte[] Data { get; }

		public override byte[] ToByteArray()
		{
			int cbData = this.Data.Length;

			byte[] bytes = new byte[8 + cbData];
			Span<byte> byteSpan = bytes;
			BinaryPrimitives.WriteUInt32LittleEndian(byteSpan.Slice(0, 4), (uint)this.Tag);
			BinaryPrimitives.WriteUInt16LittleEndian(byteSpan.Slice(2, 2), (ushort)cbData);
			// Reserved (2)
			this.Data.CopyTo(byteSpan.Slice(8));

			return bytes;
		}
	}
}
