using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Titanis.Winterop.Security
{

	[Flags]
	internal enum AclFlags
	{
		None = 0,

		NoAcl = 1,
		Protected = 2,
		ReqAutoInherit = 4,
		AutoInherited = 8,
	}

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
		public AccessControlList(IList<AccessControlEntry> entries, bool ownsList)
		{
			if (entries is null) throw new ArgumentNullException(nameof(entries));

			if (ownsList && entries is List<AccessControlEntry> list)
				this.Entries = list;
			else
				this.Entries = [.. entries];

			int rev = 2;
			foreach (var entry in entries)
			{
				if (entry.AceType is AccessControlEntryType.AccessAllowedObject or AccessControlEntryType.AccessDeniedObject or AccessControlEntryType.SystemAuditObject or AccessControlEntryType.SystemAlarmObject or AccessControlEntryType.MandatoryLabel)
				{
					rev = 4;
					break;
				}
			}
			this.Revision = rev;
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

				off = SecurityDescriptor.Align4(off);
			}

			Debug.Assert(off == aclSize);
		}

		[Flags]
		public enum DaclOptions
		{
			None = 0,
			AutoInheritRequired = 0x0100,
			AutoInherited = 0x0400,
			Defaulted = 0x8,
		};

		private const string NoAccessControlFlag = "NO_ACCESS_CONTROL";

		internal static AclFlags ParseAclFlags(ref SddlParseContext ctx)
		{
			AclFlags aclFlags = AclFlags.None;
			char lc = '\0';
			char c = '\0';

			while (ctx.LengthRemaining > 0)
			{
				c = ctx[0];

				// TODO: Stricter checking on duplicate options and such

				if (c is 'P')
					aclFlags |= AclFlags.Protected;
				else if (lc is 'A' && c is 'R' or 'I')
				{
					if (c is 'R')
						aclFlags |= AclFlags.ReqAutoInherit;
					else if (c is 'I')
						aclFlags |= AclFlags.AutoInherited;

					lc = '\0';
				}
				else if (c is 'A')
				{
					// Wait for next char
					lc = 'A';
				}
				else if (c is 'N' && ctx.LengthRemaining >= NoAccessControlFlag.Length && NoAccessControlFlag.AsSpan().Equals(ctx.Remaining(NoAccessControlFlag.Length), StringComparison.OrdinalIgnoreCase))
				{
					aclFlags = AclFlags.NoAcl;
					ctx.Advance(NoAccessControlFlag.Length);
					break;
				}
				else if (c is '(')
				{
					break;
				}
				else
					throw ctx.MakeUnexpectedCharException("Expected P, AR, AI, or NO_ACCESS_CONTROL.");

				ctx.Advance(1);
			}

			return aclFlags;
		}

		// [MS-DTYP] § 2.5.3.1.3 GetScopedPolicySid
		public SecurityIdentifier? GetScopedPolicySid()
		{
			foreach (var ace in this.Entries)
			{
				if (0 == (ace.AceFlags & AccessControlEntryFlags.InheritOnly) && ace.AceType is AccessControlEntryType.ScopedPolicyId && ace is SimpleAce simple)
				{
					return ace.Trustee;
				}
			}

			return null;
		}

		// [MS-DTYP] § 2.5.3.1.4 GetCentralizedAccessPolicy
		public SecurityIdentifier? GetCentralizedAccessPolicy()
		{
			var scoped = this.GetScopedPolicySid();
			// TODO: This needs some sort of hook to get the policy
			return null;
		}

		// [MS-DTYP] § 2.5.3.1.7 LookupAttributeInSacl
		public SecurityIdentifier? LookupAttribute(string name)
		{
			throw new NotImplementedException();
		}

		// [MS-DTYP] § 2.5.3.1.7 LookupAttributeInSacl
		public int LookupAttributeInSacl(
			string attributeName
			)
		{
			throw new NotImplementedException();
			foreach (var ace in this.Entries)
			{
				if (ace.AceType == AccessControlEntryType.ResourceAttribute)
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}