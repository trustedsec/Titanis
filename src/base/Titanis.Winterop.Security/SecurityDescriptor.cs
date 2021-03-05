using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Winterop.Security
{
	[Flags]
	public enum SecurityDescriptorSections
	{
		None = 0,

		Audit = 1,
		Access = 2,
		Owner = 4,
		Group = 8,
		All = 0x0F
	}

	// [MS-DTYP] § 2.4.6 SECURITY_DESCRIPTOR
	[Flags]
	public enum SecurityDescriptorControl
	{
		None = 0,

		// OD
		OwnerDefaulted = 1,
		// GD
		GroupDefaulted = 2,
		// DP
		DaclPresent = 4,
		// DD
		DaclDefaulted = 8,
		// SP
		SaclPresent = 0x0010,
		// SD
		SaclDefaulted = 0x0020,
		// DT
		DaclTrusted = 0x0040,
		// SS
		ServerSecurity = 0x0080,
		// DC
		DaclRequiredAutoInherit = 0x0100,
		// SC
		SaclRequiredAutoInherit = 0x0200,
		// DI
		DaclAutoInherited = 0x0400,
		// SI
		SaclAutoInherited = 0x0800,
		// PD
		DaclProtected = 0x1000,
		// PS
		SaclProtected = 0x2000,
		// RM
		ResourceManagerControlValid = 0x4000,
		// SR
		SelfRelative = 0x8000,
	}

	/// <summary>
	/// Represents a security descriptor
	/// </summary>
	// [MS-DTYP] § 2.4.6 SECURITY_DESCRIPTOR
	public class SecurityDescriptor
	{
		private SecurityDescriptorControl _control;

		public SecurityDescriptor(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length < 20)
				throw new ArgumentException("The provided buffer is not large enough to contain a valid security descriptor.", nameof(bytes));
			var rev = bytes[0];
			if (rev != 1)
				throw new InvalidDataException("The buffer does not appear to contain a valid security descriptor.");
			var control = (SecurityDescriptorControl)BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(2, 2));
			this._control = control;

			if (0 == (control & SecurityDescriptorControl.SelfRelative))
				throw new NotSupportedException("The security descriptor is not self-relative.  Absolute security descriptors are not supported.");

			var offOwner = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(4, 4));
			var offGroup = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(8, 4));
			var offSacl = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(12, 4));
			var offDacl = BinaryPrimitives.ReadInt32LittleEndian(bytes.Slice(16, 4));

			if (offOwner != 0 && 0 == (control & SecurityDescriptorControl.OwnerDefaulted))
				this.Owner = new SecurityIdentifier(bytes.Slice(offOwner));
			if (offGroup != 0 && 0 == (control & SecurityDescriptorControl.GroupDefaulted))
				this.Group = new SecurityIdentifier(bytes.Slice(offGroup));
			if (offDacl != 0 && 0 != (control & SecurityDescriptorControl.DaclPresent))
				this.Dacl = new AccessControlList(bytes.Slice(offDacl));
			if (offSacl != 0 && 0 != (control & SecurityDescriptorControl.SaclPresent))
				this.Sacl = new AccessControlList(bytes.Slice(offDacl));
		}
		public SecurityDescriptor(
			SecurityDescriptorControl control,
			SecurityIdentifier? owner,
			SecurityIdentifier? group,
			AccessControlList? dacl,
			AccessControlList? sacl)
		{
			this.Owner = owner;
			this.Group = group;
			this.Sacl = sacl;
			this.Dacl = dacl;

			control &= ~(
				SecurityDescriptorControl.OwnerDefaulted
				| SecurityDescriptorControl.GroupDefaulted
				| SecurityDescriptorControl.DaclPresent
				| SecurityDescriptorControl.SaclPresent);

			if (owner == null)
				control |= SecurityDescriptorControl.OwnerDefaulted;
			if (group == null)
				control |= SecurityDescriptorControl.GroupDefaulted;
			if (dacl != null)
				control |= SecurityDescriptorControl.DaclPresent;
			if (sacl != null)
				control |= SecurityDescriptorControl.SaclPresent;

			this._control = control;
		}

		public SecurityIdentifier? Owner { get; }
		public SecurityIdentifier? Group { get; }
		public AccessControlList? Sacl { get; }
		public AccessControlList? Dacl { get; }

		public sealed override string ToString()
			=> this.ToSddlString(SecurityDescriptorSections.All);
		public string ToSddlString(SecurityDescriptorSections sections)
		{
			StringBuilder sb = new StringBuilder();
			if (this.Owner != null && 0 != (sections & SecurityDescriptorSections.Owner))
				sb.Append("O:").Append(this.Owner.ToSddlString());

			if (this.Group != null && 0 != (sections & SecurityDescriptorSections.Group))
				sb.Append("G:").Append(this.Group.ToSddlString());

			if (this.Dacl != null && 0 != (sections & SecurityDescriptorSections.Access))
			{
				sb.Append("D:");
				AclToSddl(sb, this.Dacl);
			}
			if (this.Sacl != null && 0 != (sections & SecurityDescriptorSections.Audit))
			{
				sb.Append("S:");
				AclToSddl(sb, this.Sacl);
			}

			return sb.ToString();
		}

		private void AclToSddl(StringBuilder sb, AccessControlList acl)
		{
			if (0 != (this._control & SecurityDescriptorControl.DaclProtected)) sb.Append('P');
			if (0 != (this._control & SecurityDescriptorControl.DaclRequiredAutoInherit)) sb.Append("AR");
			if (0 != (this._control & SecurityDescriptorControl.DaclAutoInherited)) sb.Append("AI");
			// TODO: NO_ACCESS_CONTROL / SDDL_NULL_ACL

			foreach (var ace in acl.Entries)
			{
				sb.Append('(')
					.Append(ace.ToSddlString())
					.Append(')');
			}
		}

		public byte[] ToByteArray()
		{
			int off = 20;

			int offOwner = 0;
			int offGroup = 0;
			int offDacl = 0;
			int offSacl = 0;

			if (this.Owner != null)
			{
				offOwner = off;
				off += this.Owner.BinaryLength;
			}
			if (this.Group != null)
			{
				off = Align8(off);
				offGroup = off;
				off += this.Group.BinaryLength;
			}
			if (this.Sacl != null)
			{
				off = Align8(off);
				offSacl = off;
				off += this.Sacl.BinaryLength;
			}
			if (this.Dacl != null)
			{
				off = Align8(off);
				offDacl = off;
				off += this.Dacl.BinaryLength;
			}

			byte[] buf = new byte[off];
			buf[0] = 1;
			BinaryPrimitives.WriteUInt16LittleEndian(buf.AsSpan().Slice(2, 2), (ushort)this._control);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(4, 4), offOwner);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(8, 4), offGroup);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(12, 4), offSacl);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(16, 4), offDacl);

			this.Owner?.GetBytes(buf.AsSpan().Slice(offOwner));
			this.Group?.GetBytes(buf.AsSpan().Slice(offGroup));
			this.Sacl?.GetBytes(buf.AsSpan().Slice(offSacl));
			this.Dacl?.GetBytes(buf.AsSpan().Slice(offDacl));

			return buf;
		}

		private static int Align8(int off)
		{
			if ((off & 7) != 0)
				off = off + 7 & 8 - 1;
			return off;
		}
	}
}
