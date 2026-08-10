using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

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

		OwnerMask = OwnerDefaulted,
		GroupMask = GroupDefaulted,
		DaclMask = DaclPresent | DaclDefaulted | DaclTrusted | DaclRequiredAutoInherit | DaclProtected,
		SaclMask = SaclPresent | SaclDefaulted | SaclRequiredAutoInherit | SaclProtected,
	}

	/// <summary>
	/// Represents a security descriptor
	/// </summary>
	// [MS-DTYP] § 2.4.6 SECURITY_DESCRIPTOR
	[TypeConverter(typeof(SecurityDescriptorConverter))]
	public class SecurityDescriptor
	{
		public SecurityDescriptor(
			SecurityDescriptorControl control,
			SecurityIdentifier? owner
			)
		{
			this.Control = control;

			this.Owner = owner;
		}

		public SecurityDescriptor(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length < 20)
				throw new ArgumentException("The provided buffer is not large enough to contain a valid security descriptor.", nameof(bytes));
			var rev = bytes[0];
			if (rev != 1)
				throw new InvalidDataException("The buffer does not appear to contain a valid security descriptor.");
			var control = (SecurityDescriptorControl)BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(2, 2));
			this.Control = control;

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
				this.Sacl = new AccessControlList(bytes.Slice(offSacl));
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

			this.Control = control;
		}

		public SecurityDescriptorControl Control { get; set; }
		public SecurityIdentifier? Owner { get; set; }
		public SecurityIdentifier? Group { get; set; }
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
				AclToSddl(sb, this.Dacl, false);
			}
			if (this.Sacl != null && 0 != (sections & SecurityDescriptorSections.Audit))
			{
				sb.Append("S:");
				AclToSddl(sb, this.Sacl, true);
			}

			return sb.ToString();
		}

		private void AclToSddl(StringBuilder sb, AccessControlList acl, bool isSacl)
		{
			if (isSacl)
			{
				if (0 != (this.Control & SecurityDescriptorControl.SaclProtected)) sb.Append('P');
				if (0 != (this.Control & SecurityDescriptorControl.SaclRequiredAutoInherit)) sb.Append("AR");
				if (0 != (this.Control & SecurityDescriptorControl.SaclAutoInherited)) sb.Append("AI");
			}
			else
			{
				if (0 != (this.Control & SecurityDescriptorControl.DaclProtected)) sb.Append('P');
				if (0 != (this.Control & SecurityDescriptorControl.DaclRequiredAutoInherit)) sb.Append("AR");
				if (0 != (this.Control & SecurityDescriptorControl.DaclAutoInherited)) sb.Append("AI");
			}
			// TODO: NO_ACCESS_CONTROL / SDDL_NULL_ACL

			foreach (var ace in acl.Entries)
			{
				sb.Append('(')
					.Append(ace.ToSddlString())
					.Append(')');
			}
		}

		public byte[] ToByteArray() => this.ToByteArray(SecurityInfo.Owner | SecurityInfo.Group | SecurityInfo.Dacl | SecurityInfo.Sacl);
		public byte[] ToByteArray(SecurityInfo sections)
		{
			int off = 20;

			int offOwner = 0;
			int offGroup = 0;
			int offDacl = 0;
			int offSacl = 0;

			if (0 != (sections & SecurityInfo.Owner) && this.Owner != null)
			{
				offOwner = off;
				off += this.Owner.BinaryLength;
			}
			if (0 != (sections & SecurityInfo.Group) && this.Group != null)
			{
				off = Align4(off);
				offGroup = off;
				off += this.Group.BinaryLength;
			}
			if (0 != (sections & SecurityInfo.Sacl) && this.Sacl != null)
			{
				off = Align4(off);
				offSacl = off;
				off += this.Sacl.BinaryLength;
			}
			if (0 != (sections & SecurityInfo.Dacl) && this.Dacl != null)
			{
				off = Align4(off);
				offDacl = off;
				off += this.Dacl.BinaryLength;
			}

			byte[] buf = new byte[off];
			buf[0] = 1;
			BinaryPrimitives.WriteUInt16LittleEndian(buf.AsSpan().Slice(2, 2), (ushort)(this.Control | SecurityDescriptorControl.SelfRelative));
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(4, 4), offOwner);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(8, 4), offGroup);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(12, 4), offSacl);
			BinaryPrimitives.WriteInt32LittleEndian(buf.AsSpan().Slice(16, 4), offDacl);

			if (0 != (sections & SecurityInfo.Owner))
				this.Owner?.GetBytes(buf.AsSpan().Slice(offOwner));
			if (0 != (sections & SecurityInfo.Group))
				this.Group?.GetBytes(buf.AsSpan().Slice(offGroup));
			if (0 != (sections & SecurityInfo.Sacl))
				this.Sacl?.GetBytes(buf.AsSpan().Slice(offSacl));
			if (0 != (sections & SecurityInfo.Dacl))
				this.Dacl?.GetBytes(buf.AsSpan().Slice(offDacl));

			return buf;
		}

		internal static int Align8(int off)
		{
			if ((off & 7) != 0)
				off = (off + 7) & ~(8 - 1);
			return off;
		}
		internal static int Align4(int off)
		{
			if ((off & 3) != 0)
				off = (off + 3) & ~(4 - 1);
			return off;
		}

		public static SecurityDescriptor ParseSddl(ReadOnlySpan<char> chars, SecurityIdentifier? domainSid)
		{
			if (chars.Length == 0)
				return new SecurityDescriptor(SecurityDescriptorControl.None, null);

			var ctx = new SddlParseContext(chars);
			var sd = ParseSddl(ref ctx, domainSid);

			if (ctx.LengthRemaining > 0)
				throw ctx.MakeException("The SDDL contains extra characters after the security descriptor.");

			return sd;
		}

		internal static SecurityDescriptor ParseSddl(ref SddlParseContext ctx, SecurityIdentifier? domainSid)
		{
			SecurityDescriptorControl control = SecurityDescriptorControl.None;

			SecurityIdentifier? owner = null;
			SecurityIdentifier? group = null;
			AccessControlList? dacl = null;
			AccessControlList? sacl = null;

			while (ctx.LengthRemaining > 0)
			{
				if (ctx.LengthRemaining < 2)
					throw ctx.MakeException("The string is too short to be a valid SDDL string.");

				var c = ctx[0];
				var c2 = ctx[1];
				if (c2 == ':')
				{
					ctx.Advance(2);

					if (c == 'O')
					{
						if (owner != null)
							throw ctx.MakeException("The SDDL specifies multiple owners, which is not allowed.");

						owner = SecurityIdentifier.Parse(ref ctx, domainSid);
						continue;
					}
					else if (c == 'G')
					{
						if (group != null)
							throw ctx.MakeException("The SDDL specifies multiple groups, which is not allowed.");

						group = SecurityIdentifier.Parse(ref ctx, domainSid);
						continue;
					}
					else if (c is 'D' or 'S')
					{
						var isSacl = c is 'S';
						if (isSacl)
						{
							if (sacl != null)
								throw ctx.MakeException("The SDDL specifies multiple SACLs, which is not allowed.");
						}
						else
						{
							if (dacl != null)
								throw ctx.MakeException("The SDDL specifies multiple DACLs, which is not allowed.");
						}

						var acl = ParseAclSddl(ref ctx, isSacl, domainSid, out var aclFlags);

						((isSacl ? ref sacl : ref dacl)) = acl;

						control |= aclFlags;
						continue;
					}
				}

				throw ctx.MakeUnexpectedCharException(c.ToString(), "Expected O:, G:, D:, or S:");
			}

			var sd = new SecurityDescriptor(control, owner, group, dacl, sacl);
			return sd;
		}
		private static AccessControlList? ParseAclSddl(ref SddlParseContext ctx, bool isSacl, SecurityIdentifier? domainSid, out SecurityDescriptorControl controlFlags)
		{
			var aclFlags = AccessControlList.ParseAclFlags(ref ctx);
			if (aclFlags == AclFlags.NoAcl)
			{
				controlFlags = SecurityDescriptorControl.None;
				return null;
			}

			SecurityDescriptorControl flags = SecurityDescriptorControl.None;
			if (isSacl)
			{
				if (0 != (aclFlags & AclFlags.ReqAutoInherit))
					flags |= SecurityDescriptorControl.SaclRequiredAutoInherit;
				if (0 != (aclFlags & AclFlags.AutoInherited))
					flags |= SecurityDescriptorControl.SaclAutoInherited;
				if (0 != (aclFlags & AclFlags.Protected))
					flags |= SecurityDescriptorControl.SaclProtected;
			}
			else
			{
				if (0 != (aclFlags & AclFlags.ReqAutoInherit))
					flags |= SecurityDescriptorControl.DaclRequiredAutoInherit;
				if (0 != (aclFlags & AclFlags.AutoInherited))
					flags |= SecurityDescriptorControl.DaclAutoInherited;
				if (0 != (aclFlags & AclFlags.Protected))
					flags |= SecurityDescriptorControl.DaclProtected;
			}

			controlFlags = flags;

			List<AccessControlEntry> aces = new List<AccessControlEntry>();
			while (ctx.AdvanceIf('('))
			{
				var ace = AccessControlEntry.ParseSddl(ref ctx, domainSid);
				aces.Add(ace);
				ctx.Expect(')');
			}

			var acl = new AccessControlList(aces, true);
			return acl;
		}

		public static StandardAccessRights GetRightsToRead(SecurityInfo securityInfo)
		{
			StandardAccessRights access = 0;
			if (0 != (securityInfo & (SecurityInfo.Dacl | SecurityInfo.Owner | SecurityInfo.Group))) access |= StandardAccessRights.ReadControl;
			if (0 != (securityInfo & (SecurityInfo.Dacl | SecurityInfo.Owner | SecurityInfo.Group))) access |= (StandardAccessRights)SpecialAccessRights.AccessSystemSecurity;

			return access;
		}

		public StandardAccessRights RightsToSet
		{
			get
			{
				StandardAccessRights access = 0;
				if (this.Dacl != null)
					access |= StandardAccessRights.WriteDac;
				if (this.Owner != null || this.Group != null)
					access |= StandardAccessRights.WriteOwner;
				if (this.Sacl != null)
					access |= (StandardAccessRights)SpecialAccessRights.AccessSystemSecurity;
				return access;
			}
		}
	}

	public class SecurityDescriptorConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
			sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

		// The subauthorities are arbitraty; they can't be zero since that would indicate claims
		public static readonly SecurityIdentifier PlaceholderDomainSid = new SecurityIdentifier(SecurityIdentifierAuthority.NtAuthority, [21, 1, 1, 1]) { IsDomainPlaceholder = true };

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str && str.Length > 1)
			{
				if (str[1] == ':')
				{
					var sd = SecurityDescriptor.ParseSddl(str.AsSpan(), PlaceholderDomainSid);
					return sd;
				}
				else
				{
					var bytes = BinaryHelper.ParseHexString(str.AsSpan());
					return new SecurityDescriptor(bytes);
				}
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}

	ref struct SddlParseContext
	{
		internal SddlParseContext(ReadOnlySpan<char> chars)
		{
			this.chars = chars;
		}

		internal bool domainSpecific;

		private ReadOnlySpan<char> chars;
		private int offset;

		internal int LengthRemaining => this.chars.Length;

		internal char this[int index] => this.chars[index];

		internal void Advance(int count)
		{
			this.chars = this.chars.Slice(count);
		}
		internal bool AdvanceIf(char c)
		{
			if (this.LengthRemaining > 0 && this[0] == c)
			{
				this.Advance(1);
				return true;
			}
			else
				return false;
		}
		internal void Expect(char c)
		{
			if (this.LengthRemaining > 0 && this[0] == c)
			{
				this.Advance(1);
			}
			else
				throw MakeUnexpectedCharException($"Expected '{c}'.");
		}

		internal ReadOnlySpan<char> Remaining(int length)
		{
			return this.chars.Slice(0, length);
		}

		internal Exception MakeUnexpectedCharException(string reason)
		{
			return this.MakeUnexpectedCharException(this.UnexpectedToken(), reason);
		}
		internal Exception MakeUnexpectedCharException(string token, string reason)
		{
			string message = $"Unexpected character	'{token}' @ {this.offset}";
			return new FormatException(message);
		}

		private string UnexpectedToken()
		{
			return (this.LengthRemaining > 0 ? this[0].ToString() : "<end>");
		}

		internal Exception MakeException(string reason)
		{
			string message = $"Error at character	'{UnexpectedToken()}' @ {this.offset}: {reason}";
			return new FormatException(message);
		}
	}
}
