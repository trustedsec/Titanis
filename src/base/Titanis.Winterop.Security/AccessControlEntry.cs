using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Winterop.Security
{
	/// <summary>
	/// Specifies a type af <see cref="AccessControlEntry"/>.
	/// </summary>
	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	public enum AccessControlEntryType
	{
		AccessAllowed = 0,
		AccessDenied = 1,
		Audit = 2,
		Alarm = 3,
		AccessAllowedCompound = 4,
		AccessAllowedObject = 5,
		AccessDeniedObject = 6,
		SystemAuditObject = 7,
		SystemAlarmObject = 8,
		AccessAllowedCallback = 9,
		AccessDeniedCallback = 0x0A,
		AcecssAllowedCallbackObject = 0x0B,
		AccessDeniedCallbackObject = 0x0C,
		SystemAuditCallback = 0x0D,
		SystemAlarmCallback = 0x0E,
		SystemAuditCallbackObject = 0x0F,
		SystemAlarmCallbackObject = 0x10,
		MandatoryLabel = 0x11,
		ResourceAttribute = 0x12,
		ScopedPolicyId = 0x13,
	}

	/// <summary>
	/// Specifies flags that affect the behavior of an <see cref="AccessControlEntry"/>.
	/// </summary>
	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	[Flags]
	public enum AccessControlEntryFlags
	{
		None = 0,

		ObjectInherit = 1,
		ContainerInherit = 2,
		NoPropagateInherit = 4,
		InheritOnly = 8,
		Inherited = 0x10,
		Critical = 0x20,
		SuccessfulAccessAudit = 0x40,
		FailedAccessAudit = 0x80,
	}

	/// <summary>
	/// Describes an entry within an <see cref="AccessControlList"/>.
	/// </summary>
	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	public abstract class AccessControlEntry
	{
		/// <summary>
		/// Initializes a new <see cref="AccessControlEntry"/>
		/// </summary>
		/// <param name="type"><see cref="AccessControlEntryType"/> specifying entry type</param>
		/// <param name="flags"><see cref="AccessControlEntryFlags"/> affecting behavior</param>
		protected AccessControlEntry(AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			this.AceType = type;
			this.AceFlags = flags;
		}

		public AccessControlEntryType AceType { get; }
		public AccessControlEntryFlags AceFlags { get; }

		#region SDDL
		/// <summary>
		/// Builds a string representing the ACE within SDDL.
		/// </summary>
		/// <returns></returns>
		public string ToSddlString()
		{
			StringBuilder sb = new StringBuilder();
			this.BuildSddl(sb);
			return sb.ToString();
		}

		/// <summary>
		/// Builds the SDDL describing this ACE.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to</param>
		public abstract void BuildSddl(StringBuilder sb);

		internal static void AppendFlagCodes(StringBuilder sb, uint flags, Dictionary<uint, string> flagCodes)
		{
			// If one code matches the flags exactly, use it
			if (flagCodes.TryGetValue(flags, out var masterCode))
				sb.Append(masterCode);
			else
			{
				uint shadowFlags = 0;
				// Check whether a combination of codes matches the flag
				foreach (var entry in flagCodes)
				{
					if ((flags & entry.Key) == entry.Key)
						shadowFlags |= entry.Key;
				}

				// If so, use the combination
				if (shadowFlags == flags)
				{
					foreach (var entry in flagCodes)
					{
						if ((flags & entry.Key) == entry.Key)
							sb.Append(entry.Value);
					}
				}
				else
				{
					// Write in hex
					sb.Append("0x").AppendFormat("{0:X}", flags);
				}
			}
		}
		private static readonly Dictionary<uint, string> aceFlagCodes = new Dictionary<uint, string>()
		{
			{ (uint)AccessControlEntryFlags.ContainerInherit, "CI" },
			{ (uint)AccessControlEntryFlags.ObjectInherit, "OI" },
			{ (uint)AccessControlEntryFlags.NoPropagateInherit, "NP" },
			{ (uint)AccessControlEntryFlags.InheritOnly, "IO" },
			{ (uint)AccessControlEntryFlags.Inherited, "ID" },
			{ (uint)AccessControlEntryFlags.SuccessfulAccessAudit, "SA" },
			{ (uint)AccessControlEntryFlags.FailedAccessAudit, "FA" },
			{ (uint)AccessControlEntryFlags.Critical, "CR" },
			// TODO: TP?
		};
		internal static readonly Dictionary<uint, string> fileAccessRightCodes = new Dictionary<uint, string>()
		{
			// Generic
			{ (uint)Smb2FileAccessRights.GenericAll, "GA" },
			{ (uint)Smb2FileAccessRights.GenericRead, "GR" },
			{ (uint)Smb2FileAccessRights.GenericWrite, "GW" },
			{ (uint)Smb2FileAccessRights.GenericExecute, "GX" },
			// Standard
			{ (uint)Smb2FileAccessRights.ReadControl, "RC" },
			{ (uint)Smb2FileAccessRights.Delete, "SD" },
			{ (uint)Smb2FileAccessRights.WriteDac, "WD" },
			{ (uint)Smb2FileAccessRights.WriteOwner, "WO" },
			// File
			{ (uint)Smb2FileAccessRights.FullAccess, "FA" },
			{ (uint)Smb2FileAccessRights.FileGenericRead, "FR" },
			{ (uint)Smb2FileAccessRights.FileGenericWrite, "FW" },
			{ (uint)Smb2FileAccessRights.FileGenericExecute, "FX" },
		};

		private protected void BuildSddlFromComponents(
			StringBuilder sb,
			AccessControlEntryType aceType,
			AccessControlEntryFlags aceFlags,
			uint accessRights,
			Dictionary<uint, string> accessRightCodes,
			Guid? objectGuid,
			Guid? inheritGuid,
			SecurityIdentifier? trustee)
		{
			sb.Append(GetAceTypeString(aceType))
				.Append(';')
				;
			AppendFlagCodes(sb, (uint)aceFlags, aceFlagCodes);
			sb.Append(';');
			AppendFlagCodes(sb, accessRights, accessRightCodes);
			sb.Append(';');
			if (objectGuid.HasValue)
				sb.Append(objectGuid.Value);
			sb.Append(';');
			if (inheritGuid.HasValue)
				sb.Append(inheritGuid.Value);
			sb.Append(';');
			if (trustee != null)
				sb.Append(trustee.AsSddlCode());
		}

		#endregion

		private static readonly Dictionary<AccessControlEntryType, string> aceTypeCodes = new Dictionary<AccessControlEntryType, string>()
		{
			{ AccessControlEntryType.AccessAllowed, "A" },
			{ AccessControlEntryType.AccessDenied, "D" },
			{ AccessControlEntryType.Audit, "AU" },
			{ AccessControlEntryType.Alarm, "AL" },
			{ AccessControlEntryType.AccessAllowedObject, "OA" },
			{ AccessControlEntryType.AccessDeniedObject, "OD" },
			{ AccessControlEntryType.SystemAuditObject, "OU" },
			{ AccessControlEntryType.SystemAlarmObject, "OL" },
			{ AccessControlEntryType.AccessAllowedCallback, "XA" },
			{ AccessControlEntryType.AccessDeniedCallback, "XD" },
			{ AccessControlEntryType.AcecssAllowedCallbackObject, "ZA" },
			{ AccessControlEntryType.SystemAuditCallback, "XU" },
			{ AccessControlEntryType.MandatoryLabel, "ML" },
			{ AccessControlEntryType.ResourceAttribute, "RA" },
			{ AccessControlEntryType.ScopedPolicyId, "SP" },

			// Unknown or unsupported
			//AccessControlEntryType.AccessAllowedCompound => null,
			//AccessControlEntryType.AccessDeniedCallbackObject => null,
			//AccessControlEntryType.SystemAlarmCallback => null,
			//AccessControlEntryType.SystemAuditCallbackObject => null,
			//AccessControlEntryType.SystemAlarmCallbackObject => null,
		};

		public static string? GetAceTypeString(AccessControlEntryType type)
		{
			aceTypeCodes.TryGetValue(type, out var s);
			return s;
		}

		private static uint ParseFlagCodes(ReadOnlySpan<char> text, Dictionary<uint, string> flagCodes, out int charactersConsumed)
		{
			uint flags = 0;
			int cchEaten = 0;
			foreach (var entry in flagCodes)
			{
				if (text.Length > cchEaten && text.Equals(entry.Value, StringComparison.Ordinal))
				{
					flags |= entry.Key;
					cchEaten += entry.Value.Length;
					text = text.Slice(entry.Value.Length);
				}
			}

			charactersConsumed = cchEaten;
			return flags;
		}
		public static AccessControlEntry ParseSddl(ReadOnlySpan<char> text)
		{
			AccessControlEntryType aceType = (AccessControlEntryType)(-1);
			int cchEaten = 0;
			foreach (var entry in aceTypeCodes)
			{
				if (text.Length >= entry.Value.Length && entry.Value.AsSpan().Equals(text, StringComparison.Ordinal))
				{
					aceType = entry.Key;
					cchEaten = entry.Value.Length;
					break;
				}
			}

			if (cchEaten == 0)
				throw new ArgumentException("Invalid ACE type", nameof(text));

			if (text.Length > cchEaten && text[cchEaten] == ';')
				text = text.Slice(cchEaten + 1);
			else
				throw new ArgumentException("Invalid ACE", nameof(text));

			throw new NotImplementedException();
		}


		// [MS-DTYP] § 2.4.4.3 ACCESS_ALLOWED_OBJECT_ACE
		[Flags]
		private enum ObjectAceFlags
		{
			None = 0,
			HasObjectType = 1,
			HasInheritedType = 2,
		}
		internal static AccessControlEntry FromBytes(ReadOnlySpan<byte> bytes, out int consumed)
		{
			if (bytes.Length < 4)
				throw new InvalidCastException("The data does not contain a complete ACE.");

			var type = (AccessControlEntryType)bytes[0];
			var flags = (AccessControlEntryFlags)bytes[1];
			int size = BinaryPrimitives.ReadUInt16LittleEndian(bytes.Slice(2, 2));
			consumed = size;
			bytes = bytes.Slice(4, size - 4);

			switch (type)
			{
				case AccessControlEntryType.AccessAllowed:
				case AccessControlEntryType.AccessDenied:
				case AccessControlEntryType.Audit:
				case AccessControlEntryType.ScopedPolicyId:
					{
						var accessMask = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
						var sid = new SecurityIdentifier(bytes.Slice(4));
						var ace = new SimpleAce(type, flags, accessMask, sid);
						return ace;
					}

				case AccessControlEntryType.AccessAllowedObject:
				case AccessControlEntryType.AccessDeniedObject:
				case AccessControlEntryType.SystemAuditObject:
					return ParseObjectAce(bytes, type, flags);

				case AccessControlEntryType.AccessAllowedCallback:
				case AccessControlEntryType.AccessDeniedCallback:
				case AccessControlEntryType.SystemAuditCallback:
					return ParseCallbackAce(bytes, type, flags);

				case AccessControlEntryType.AcecssAllowedCallbackObject:
				case AccessControlEntryType.AccessDeniedCallbackObject:
				case AccessControlEntryType.SystemAuditCallbackObject:
					return ParseCallbackObjectAce(bytes, type, flags);

				case AccessControlEntryType.MandatoryLabel:
					return ParseMandatoryLabelAce(bytes, type, flags);

				case AccessControlEntryType.ResourceAttribute:
					return ParseResourceAttributeAce(bytes, type, flags);

				case AccessControlEntryType.Alarm:
				case AccessControlEntryType.AccessAllowedCompound:
				case AccessControlEntryType.SystemAlarmObject:
				case AccessControlEntryType.SystemAlarmCallback:
				case AccessControlEntryType.SystemAlarmCallbackObject:
				default:
					throw new NotSupportedException();
			}
		}

		private static AccessControlEntry ParseObjectAce(ReadOnlySpan<byte> bytes, AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			var accessMask = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var objFlags = (ObjectAceFlags)BinaryPrimitives.ReadUInt32LittleEndian(bytes.Slice(4));

			Guid? objectType =
				0 != (objFlags & ObjectAceFlags.HasObjectType) ? MemoryMarshal.Read<Guid>(bytes.Slice(8, 16))
				: null;
			Guid? inheritedType =
				0 != (objFlags & ObjectAceFlags.HasInheritedType) ? MemoryMarshal.Read<Guid>(bytes.Slice(8 + 16, 16))
				: null;

			var sid = new SecurityIdentifier(bytes.Slice(8 + 16 + 16));
			var ace = new ObjectAce(type, flags, accessMask, objectType, inheritedType, sid);
			return ace;
		}
		private static AccessControlEntry ParseCallbackAce(ReadOnlySpan<byte> bytes, AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			var accessMask = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var sid = new SecurityIdentifier(bytes.Slice(4));
			var data = bytes.Slice(4 + sid.BinaryLength).ToArray();
			var ace = new CallbackAce(type, flags, accessMask, sid, data);
			return ace;
		}
		private static AccessControlEntry ParseCallbackObjectAce(ReadOnlySpan<byte> bytes, AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			var accessMask = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var objFlags = (ObjectAceFlags)BinaryPrimitives.ReadUInt32LittleEndian(bytes.Slice(4));

			Guid? objectType =
				0 != (objFlags & ObjectAceFlags.HasObjectType) ? MemoryMarshal.Read<Guid>(bytes.Slice(8, 16))
				: null;
			Guid? inheritedType =
				0 != (objFlags & ObjectAceFlags.HasInheritedType) ? MemoryMarshal.Read<Guid>(bytes.Slice(8 + 16, 16))
				: null;

			var sid = new SecurityIdentifier(bytes.Slice(8 + 16 + 16));
			var data = bytes.Slice(8 + 16 + 16 + sid.BinaryLength).ToArray();
			var ace = new CallbackObjectAce(type, flags, accessMask, objectType, inheritedType, sid, data);
			return ace;
		}
		private static AccessControlEntry ParseResourceAttributeAce(ReadOnlySpan<byte> bytes, AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			var accessMask = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var sid = new SecurityIdentifier(bytes.Slice(4));
			var data = bytes.Slice(4 + sid.BinaryLength).ToArray();
			var ace = new ResourceAttributeAce(type, flags, accessMask, sid, data);
			return ace;
		}
		private static AccessControlEntry ParseMandatoryLabelAce(ReadOnlySpan<byte> bytes, AccessControlEntryType type, AccessControlEntryFlags flags)
		{
			var policy = (MandatoryLabelPolicy)BinaryPrimitives.ReadUInt32LittleEndian(bytes);
			var sid = new SecurityIdentifier(bytes.Slice(4));
			var ace = new MandatoryLabelAce(type, flags, policy, sid);
			return ace;
		}

		internal abstract int BinaryLength { get; }
		internal abstract int GetBytes(Span<byte> span);
	}

	// [MS-DTYP] § 2.4.4.2 ACCESS_ALLOWED_ACE
	public sealed class SimpleAce : AccessControlEntry
	{
		public SimpleAce(
			AccessControlEntryType aceType,
			AccessControlEntryFlags flags,
			uint accessMask,
			SecurityIdentifier sid
			) : base(aceType, flags)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));

			this.AccessMask = accessMask;
			this.Trustee = sid;
		}

		public uint AccessMask { get; }

		public SecurityIdentifier Trustee { get; }

		public override void BuildSddl(StringBuilder sb)
		{
			BuildSddlFromComponents(
				sb,
				this.AceType,
				this.AceFlags,
				this.AccessMask,
				fileAccessRightCodes,
				null, null,
				this.Trustee);
		}

		internal override int BinaryLength => 8 + this.Trustee.BinaryLength;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), this.AccessMask);
			this.Trustee.GetBytes(bytes.Slice(8));

			return cb;
		}
	}

	[Flags]
	public enum MandatoryLabelPolicy
	{
		None = 0,

		NoWriteUp = 1,
		NoReadUp = 2,
		NoExecuteUp = 4,
	}

	// [MS-DTYP] § 2.4.4.2 ACCESS_ALLOWED_ACE
	public sealed class MandatoryLabelAce : AccessControlEntry
	{
		public MandatoryLabelAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			MandatoryLabelPolicy policy,
			SecurityIdentifier sid
			) : base(type, flags)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			this.Policy = policy;
			this.Trustee = sid;
		}

		public MandatoryLabelPolicy Policy { get; }
		public SecurityIdentifier Trustee { get; }

		private static readonly Dictionary<uint, string> accessRightCodes = new Dictionary<uint, string>()
		{
			{ (uint)MandatoryLabelPolicy.NoReadUp, "NR" },
			{ (uint)MandatoryLabelPolicy.NoWriteUp, "NW" },
			{ (uint)MandatoryLabelPolicy.NoExecuteUp, "NX" },
		};

		public override void BuildSddl(StringBuilder sb)
		{
			BuildSddlFromComponents(
				sb,
				this.AceType,
				this.AceFlags,
				(uint)this.Policy,
				accessRightCodes,
				null, null,
				this.Trustee);
		}

		internal override int BinaryLength => 8 + this.Trustee.BinaryLength;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), (uint)this.Policy);
			this.Trustee.GetBytes(bytes.Slice(8));

			return cb;
		}
	}

	// [MS-DTYP] § 2.4.4.6 ACCESS_ALLOWED_CALLBACK_ACE
	public sealed class CallbackAce : AccessControlEntry
	{
		public CallbackAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			SecurityIdentifier sid,
			byte[] applicationData
			) : base(type, flags)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			if (applicationData is null) throw new ArgumentNullException(nameof(applicationData));
			this.AccessMask = accessMask;
			this.Trustee = sid;
			this.ApplicationData = applicationData;
		}

		public uint AccessMask { get; }
		public SecurityIdentifier Trustee { get; }
		public byte[] ApplicationData { get; }

		public override void BuildSddl(StringBuilder sb)
		{
			BuildSddlFromComponents(
				sb,
				this.AceType,
				this.AceFlags,
				this.AccessMask,
				fileAccessRightCodes,
				null, null,
				this.Trustee);
		}

		internal override int BinaryLength => 8 + this.Trustee.BinaryLength + this.ApplicationData.Length;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), this.AccessMask);
			this.Trustee.GetBytes(bytes.Slice(8));
			int off = 8 + this.Trustee.BinaryLength;
			this.ApplicationData.CopyTo(bytes.Slice(off));

			return cb;
		}
	}

	// [MS-DTYP] § 2.4.4.3 ACCESS_ALLOWED_OBJECT_ACE
	public sealed class ObjectAce : AccessControlEntry
	{
		public ObjectAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			Guid? objectType,
			Guid? inheritedObjectType,
			SecurityIdentifier sid
			) : base(type, flags)
		{
			this.AccessMask = accessMask;
			this.ObjectType = objectType;
			this.InheritedObjectType = inheritedObjectType;
			this.Trustee = sid;
		}

		public Guid? ObjectType { get; }
		public Guid? InheritedObjectType { get; }
		public uint AccessMask { get; }
		public SecurityIdentifier Trustee { get; }

		public override void BuildSddl(StringBuilder sb)
		{
			BuildSddlFromComponents(
				sb,
				this.AceType,
				this.AceFlags,
				this.AccessMask,
				fileAccessRightCodes,
				this.ObjectType, this.InheritedObjectType,
				this.Trustee);
		}

		internal override int BinaryLength => 12 + 16 + 16 + this.Trustee.BinaryLength;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			var flags = 0U;
			if (this.ObjectType.HasValue)
				flags |= 1U;
			if (this.InheritedObjectType.HasValue)
				flags |= 2U;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), this.AccessMask);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(8, 4), flags);

			if (this.ObjectType.HasValue)
				this.ObjectType.Value.TryWriteBytes(bytes.Slice(12, 16));
			if (this.InheritedObjectType.HasValue)
				this.InheritedObjectType.Value.TryWriteBytes(bytes.Slice(12 + 16, 16));

			this.Trustee.GetBytes(bytes.Slice(12 + 16 + 16));

			return cb;
		}
	}

	// [MS-DTYP] § 2.4.4.8 ACCESS_ALLOWED_CALLBACK_OBJECT_ACE
	public sealed class CallbackObjectAce : AccessControlEntry
	{
		public CallbackObjectAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			Guid? objectType,
			Guid? inheritedObjectType,
			SecurityIdentifier sid,
			byte[] applicationData
			) : base(type, flags)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			if (applicationData is null) throw new ArgumentNullException(nameof(applicationData));
			this.AccessMask = accessMask;
			this.ObjectType = objectType;
			this.InheritedObjectType = inheritedObjectType;
			this.Trustee = sid;
			this.ApplicationData = applicationData;
		}

		public Guid? ObjectType { get; }
		public Guid? InheritedObjectType { get; }
		public uint AccessMask { get; }
		public SecurityIdentifier Trustee { get; }
		public byte[] ApplicationData { get; }

		public override void BuildSddl(StringBuilder sb)
		{
			BuildSddlFromComponents(
				sb,
				this.AceType,
				this.AceFlags,
				this.AccessMask,
				fileAccessRightCodes,
				this.ObjectType, this.InheritedObjectType,
				this.Trustee);
		}

		internal override int BinaryLength => 12 + 16 + 16 + this.Trustee.BinaryLength + this.ApplicationData.Length;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			var flags = 0U;
			if (this.ObjectType.HasValue)
				flags |= 1U;
			if (this.InheritedObjectType.HasValue)
				flags |= 2U;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), this.AccessMask);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(8, 4), flags);

			if (this.ObjectType.HasValue)
				this.ObjectType.Value.TryWriteBytes(bytes.Slice(12, 16));
			if (this.InheritedObjectType.HasValue)
				this.InheritedObjectType.Value.TryWriteBytes(bytes.Slice(12 + 16, 16));

			this.Trustee.GetBytes(bytes.Slice(12 + 16 + 16));

			int offData = 12 + 16 + 16 + this.Trustee.BinaryLength;
			this.ApplicationData.CopyTo(bytes.Slice(offData));

			return cb;
		}
	}

	// [MS-DTYP] § 2.4.4.15 SYSTEM_RESOURCE_ATTRIBUTE_ACE
	public sealed class ResourceAttributeAce : AccessControlEntry
	{
		public ResourceAttributeAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			SecurityIdentifier sid,
			byte[] attributeData
			) : base(type, flags)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			if (attributeData is null) throw new ArgumentNullException(nameof(attributeData));
			this.AccessMask = accessMask;
			this.Trustee = sid;
			this.AttributeData = attributeData;
		}

		public uint AccessMask { get; }
		public SecurityIdentifier Trustee { get; }
		public byte[] AttributeData { get; }

		public override void BuildSddl(StringBuilder sb)
		{
			throw new NotImplementedException();
		}

		internal override int BinaryLength => 8 + this.Trustee.BinaryLength + this.AttributeData.Length;
		internal override int GetBytes(Span<byte> bytes)
		{
			var cb = this.BinaryLength;

			bytes[0] = (byte)this.AceType;
			bytes[1] = (byte)this.AceFlags;
			BinaryPrimitives.WriteUInt16LittleEndian(bytes.Slice(2, 2), (ushort)cb);
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.Slice(4, 4), this.AccessMask);
			this.Trustee.GetBytes(bytes.Slice(8));
			int off = 8 + this.Trustee.BinaryLength;
			this.AttributeData.CopyTo(bytes.Slice(off));

			return cb;
		}
	}
}