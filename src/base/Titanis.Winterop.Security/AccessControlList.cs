using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;

namespace Titanis.Winterop.Security
{
	// [MS-DTYP] § 2.4.5 ACL
	public class AccessControlList
	{
		const int AclRevision2 = 2;
		const int AclRevision4 = 4;

		public AccessControlList(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length < 8)
				throw new InvalidDataException("The data does not constitute a valid ACL.");

			int aclSize = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(2, 2));

			int aceCount = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(4, 2));
			if (bytes.Length < aclSize)
				throw new InvalidDataException("The provided data is incomplete.");

			int rev = bytes[0];
			this.Revision = rev;

			if (rev is not (AclRevision2 or AclRevision4))
				throw new InvalidDataException("The ACL version is not supported.");

			List<AccessControlEntry> aces = new List<AccessControlEntry>(aceCount);
			int offset = 8;
			for (int i = 0; i < aceCount; i++)
			{
				var ace = AccessControlEntry.FromBytes(bytes.Slice(offset), out int length);
				aces.Add(ace);

				offset += length;
			}

			this.Entries = new List<AccessControlEntry>(aces);
		}

		public List<AccessControlEntry> Entries { get; }
		public int Revision { get; }

		public int BinaryLength
		{
			get
			{
				int size = 8;
				foreach (var ace in this.Entries)
				{
					size += ace.BinaryLength;
					if (0 != (size & 0x3))
						size = size + 3 & ~3;
				}

				return size;
			}
		}

		public void GetBytes(Span<byte> bytes)
		{
			var aclSize = this.BinaryLength;
			if (bytes.Length < aclSize)
				throw new ArgumentException("The buffer isn't large enough to hold the ACL.  It must be at least BinaryLength bytes in size.", nameof(bytes));
			if (aclSize > ushort.MaxValue)
				throw new InvalidOperationException("The ACL is too large to be represented in binary form.");
			if (this.Entries.Count > ushort.MaxValue)
				throw new InvalidOperationException("The ACL has too many entries to be represented in binary form.");

			bytes[0] = (byte)this.Revision;
			bytes[1] = 0;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)aclSize);
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(4, 2), (ushort)this.Entries.Count);
			bytes[6] = 0;
			bytes[7] = 0;

			int off = 8;
			foreach (var entry in this.Entries)
			{
				int size = entry.GetBytes(bytes.Slice(off));
				off += size;

				if (0 != (off & 0x3))
					off = off + 3 & ~3;
			}
		}
	}
}