using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop.Security
{
	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	/// <summary>
	/// Specifies a type af <see cref="AccessControlEntry"/>.
	/// </summary>
	public enum AccessControlEntryType
	{
		InvalidType = -1,

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
		AccessAllowedCallbackObject = 0x0B,
		AccessDeniedCallbackObject = 0x0C,
		SystemAuditCallback = 0x0D,
		SystemAlarmCallback = 0x0E,
		SystemAuditCallbackObject = 0x0F,
		SystemAlarmCallbackObject = 0x10,
		MandatoryLabel = 0x11,
		ResourceAttribute = 0x12,
		ScopedPolicyId = 0x13,
	}

	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	/// <summary>
	/// Specifies flags that affect the behavior of an <see cref="AccessControlEntry"/>.
	/// </summary>
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

		// Only on trust filter ACE
		TrustProtectedFilter = 0x40,
		// Only on access allowed ACE
		CriticalAce = 0x20,
	}

	// [MS-DTYP] § 2.4.4.1 ACE_HEADER
	/// <summary>
	/// Describes an entry within an <see cref="AccessControlList"/>.
	/// </summary>
	public abstract class AccessControlEntry
	{
		/// <summary>
		/// Initializes a new <see cref="AccessControlEntry"/>
		/// </summary>
		/// <param name="type"><see cref="AccessControlEntryType"/> specifying entry type</param>
		/// <param name="flags"><see cref="AccessControlEntryFlags"/> affecting behavior</param>
		protected AccessControlEntry(AccessControlEntryType type, AccessControlEntryFlags flags, SecurityIdentifier trustee)
		{
			this.AceType = type;
			this.AceFlags = flags;
			this.Trustee = trustee;
		}

		public AccessControlEntryType AceType { get; }
		public AccessControlEntryFlags AceFlags { get; }
		public SecurityIdentifier Trustee { get; }

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
			{ (uint)FileAccessRights.GenericAll, "GA" },
			{ (uint)FileAccessRights.GenericRead, "GR" },
			{ (uint)FileAccessRights.GenericWrite, "GW" },
			{ (uint)FileAccessRights.GenericExecute, "GX" },
			// Standard
			{ (uint)FileAccessRights.ReadControl, "RC" },
			{ (uint)FileAccessRights.Delete, "SD" },
			{ (uint)FileAccessRights.WriteDac, "WD" },
			{ (uint)FileAccessRights.WriteOwner, "WO" },
			// File
			{ (uint)FileAccessRights.FileAll, "FA" },
			{ (uint)FileAccessRights.FileRead, "FR" },
			{ (uint)FileAccessRights.FileWrite, "FW" },
			{ (uint)FileAccessRights.FileExecute, "FX" },
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
				sb.Append(trustee.ToSddlString());
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
			{ AccessControlEntryType.AccessAllowedCallbackObject, "ZA" },
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

		internal static AccessControlEntry? ParseSddl(ref SddlParseContext ctx, SecurityIdentifier? domainSid)
		{
			AccessControlEntryType aceType = ParseAceType(ref ctx);
			ctx.Expect(';');
			AccessControlEntryFlags flags = ParseAceFlags(ref ctx);
			ctx.Expect(';');
			uint rights = AccessControlEntry.ParseRights(ref ctx);

			// Object type
			Guid? objType = null;
			ctx.Expect(';');
			objType = ParseGuid(ref ctx);

			Guid? inheritType = null;
			ctx.Expect(';');
			inheritType = ParseGuid(ref ctx);

			ctx.Expect(';');

			var trustee = SecurityIdentifier.Parse(ref ctx, domainSid);

			byte[] callbackData = Array.Empty<byte>();

			var ace = aceType switch
			{
				AccessControlEntryType.AccessAllowed or
				AccessControlEntryType.AccessDenied or
				AccessControlEntryType.Audit or
				AccessControlEntryType.ScopedPolicyId => (AccessControlEntry)new SimpleAce(aceType, flags, rights, trustee),

				AccessControlEntryType.AccessAllowedObject or
				AccessControlEntryType.AccessDeniedObject or
				AccessControlEntryType.SystemAuditObject => new ObjectAce(aceType, flags, rights, objType, inheritType, trustee),

				AccessControlEntryType.AccessAllowedCallback or
				AccessControlEntryType.AccessDeniedCallback or
				AccessControlEntryType.SystemAuditCallback => new CallbackAce(aceType, flags, rights, trustee, callbackData),

				AccessControlEntryType.AccessAllowedCallbackObject or
				AccessControlEntryType.AccessDeniedCallbackObject or
				AccessControlEntryType.SystemAuditCallbackObject => new CallbackObjectAce(aceType, flags, rights, objType, inheritType, trustee, callbackData),

				AccessControlEntryType.MandatoryLabel => new MandatoryLabelAce(aceType, flags, (MandatoryLabelPolicy)rights, trustee),

				AccessControlEntryType.ResourceAttribute => new ResourceAttributeAce(aceType, flags, rights, trustee, callbackData),



				AccessControlEntryType.Alarm or _ => throw new NotSupportedException("Future use"),
				//AccessControlEntryType.AccessAllowedCompound => throw new NotImplementedException(),
				//AccessControlEntryType.SystemAlarmObject => throw new NotImplementedException(),
				//AccessControlEntryType.SystemAlarmCallback => throw new NotImplementedException(),
				//AccessControlEntryType.SystemAlarmCallbackObject => throw new NotImplementedException(),
			};

			return ace;
		}

		internal static AccessControlEntryFlags ParseAceFlags(ref SddlParseContext ctx)
		{
			AccessControlEntryFlags flags = AccessControlEntryFlags.None;
			char c = '\0';
			while (ctx.LengthRemaining >= 2 && (c = ctx[0]) != ';')
			{
				var c1 = ctx[0];
				var c2 = ctx[1];

				AccessControlEntryFlags flag = (c1, c2) switch
				{
					('C', 'I') => AccessControlEntryFlags.ContainerInherit,
					('O', 'I') => AccessControlEntryFlags.ObjectInherit,
					('N', 'P') => AccessControlEntryFlags.NoPropagateInherit,
					('I', 'O') => AccessControlEntryFlags.InheritOnly,
					('I', 'D') => AccessControlEntryFlags.Inherited,
					('S', 'A') => AccessControlEntryFlags.SuccessfulAccessAudit,
					('F', 'A') => AccessControlEntryFlags.FailedAccessAudit,
					// Extra
					('T', 'P') => AccessControlEntryFlags.TrustProtectedFilter,
					('C', 'R') => AccessControlEntryFlags.CriticalAce,
					_ => throw ctx.MakeUnexpectedCharException($"Unknown ACE flag '{c1}{c2}'.")
				};

				// TODO: Check for duplicate flag
				flags |= flag;

				ctx.Advance(2);
			}

			return flags;
		}

		internal static AccessControlEntryType ParseAceType(ref SddlParseContext ctx)
		{
			char c = '\0';
			int offset = 0;
			for (; (offset < ctx.LengthRemaining) && ((c = ctx[offset]) != ';'); offset++)
				;

			if (c != ';')
				throw ctx.MakeUnexpectedCharException("Expected ACE type.");

			AccessControlEntryType aceType;
			if (offset == 1)
			{
				c = ctx[0];
				aceType = c switch
				{
					'A' => AccessControlEntryType.AccessAllowed,
					'D' => AccessControlEntryType.AccessDenied,
					_ => throw ctx.MakeUnexpectedCharException($"Unknown ACE type '{c}'.")
				};
				ctx.Advance(1);
			}
			else if (offset == 2)
			{
				var c1 = ctx[0];
				var c2 = ctx[1];
				aceType = (c1, c2) switch
				{
					('O', 'A') => AccessControlEntryType.AccessAllowedObject,
					('O', 'D') => AccessControlEntryType.AccessDeniedObject,
					('A', 'U') => AccessControlEntryType.Audit,
					('A', 'L') => AccessControlEntryType.Alarm,
					('O', 'U') => AccessControlEntryType.SystemAuditObject,
					('O', 'L') => AccessControlEntryType.SystemAlarmObject,
					('M', 'L') => AccessControlEntryType.MandatoryLabel,
					('X', 'A') => AccessControlEntryType.AccessAllowedCallback,
					('X', 'D') => AccessControlEntryType.AccessDeniedCallback,
					('R', 'A') => AccessControlEntryType.ResourceAttribute,
					('S', 'P') => AccessControlEntryType.ScopedPolicyId,
					('X', 'U') => AccessControlEntryType.SystemAuditCallback,
					('Z', 'A') => AccessControlEntryType.AccessAllowedCallbackObject,
					//('T', 'L') => AccessControlEntryType.ProcessTrustLevel,
					//('F', 'L') => AccessControlEntryType.AccessFilter,
					_ => throw ctx.MakeUnexpectedCharException($"Unknown ACE type '{c1}{c2}'.")
				};
				ctx.Advance(2);
			}
			else
			{
				throw ctx.MakeUnexpectedCharException($"Unknown ACE type.");
			}

			return aceType;
		}

		const int GuidTextLength = 32 + 4;
		private static Guid? ParseGuid(ref SddlParseContext ctx)
		{
			if (ctx.LengthRemaining >= GuidTextLength && ctx[0] != ';')
			{
				if (Compat.TryParseGuid(ctx.Remaining(GuidTextLength), out var guid))
				{
					ctx.Advance(GuidTextLength);
					return guid;
				}
				else
					throw ctx.MakeUnexpectedCharException("Expected GUID or ;");
			}

			return null;
		}

		internal static uint ParseRights(ref SddlParseContext ctx)
		{
			uint rights = 0;
			char c = '\0';
			while (ctx.LengthRemaining >= 2 && (c = ctx[0]) is not (';' or ')'))
			{
				var c1 = ctx[0];
				var c2 = ctx[1];

				ctx.Advance(2);

				if (
					(rights == 0)
					&& (c1 == '0')
					&& (c2 == 'x')
					)
				{
					while (ctx.LengthRemaining > 0 && BinaryHelper.IsHexChar(c = ctx[0]))
					{
						rights *= 16;
						rights += (uint)BinaryHelper.ParseHexChar(c);

						ctx.Advance(1);
					}

					break;
				}
				else
				{
					uint right = (c1, c2) switch
					{
						// Generic
						('G', 'R') => (uint)GenericAccessRights.GenericRead,
						('G', 'W') => (uint)GenericAccessRights.GenericWrite,
						('G', 'X') => (uint)GenericAccessRights.GenericExecute,
						('G', 'A') => (uint)GenericAccessRights.GenericAll,
						// Standard
						('W', 'O') => (uint)StandardAccessRights.WriteOwner,
						('W', 'D') => (uint)StandardAccessRights.WriteDac,
						('R', 'C') => (uint)StandardAccessRights.ReadControl,
						('S', 'D') => (uint)StandardAccessRights.Delete,
						// File
						('F', 'A') => (uint)FileAccessRights.FileAll,
						('F', 'X') => (uint)FileAccessRights.FileExecute,
						('F', 'W') => (uint)FileAccessRights.FileWrite,
						('F', 'R') => (uint)FileAccessRights.FileRead,
						// Registry
						('K', 'A') => (uint)RegistryAccessRights.KeyAll,
						('K', 'X') => (uint)RegistryAccessRights.KeyExecute,
						('K', 'W') => (uint)RegistryAccessRights.KeyWrite,
						('K', 'R') => (uint)RegistryAccessRights.KeyRead,
						// Directory
						('C', 'R') => (uint)DirectoryObjectAccessRights.ControlAccess,
						('L', 'O') => (uint)DirectoryObjectAccessRights.ListObject,
						('D', 'T') => (uint)DirectoryObjectAccessRights.DeleteTree,
						('W', 'P') => (uint)DirectoryObjectAccessRights.WriteProperty,
						('R', 'P') => (uint)DirectoryObjectAccessRights.ReadProperty,
						('S', 'W') => (uint)DirectoryObjectAccessRights.SelfWrite,
						('L', 'C') => (uint)DirectoryObjectAccessRights.ListChildren,
						('D', 'C') => (uint)DirectoryObjectAccessRights.DeleteChild,
						('C', 'C') => (uint)DirectoryObjectAccessRights.CreateChild,
						// Mandatory label
						('N', 'R') => (uint)MandatoryLabelPolicy.NoReadUp,
						('N', 'W') => (uint)MandatoryLabelPolicy.NoWriteUp,
						('N', 'X') => (uint)MandatoryLabelPolicy.NoExecuteUp,


						_ => throw new FormatException($"Unknown ACE right '{c1}{c2}'.")
					};

					rights |= right;
				}
			}
			if (ctx.LengthRemaining > 0 && ctx[0] is not (';' or ')'))
				throw new FormatException("The ACE string is not valid.");

			return rights;
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

				case AccessControlEntryType.AccessAllowedCallbackObject:
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

			int off = 8;
			Guid? objectType = ReadGuidIf(0 != (objFlags & ObjectAceFlags.HasObjectType), bytes, ref off);
			Guid? inheritedType = ReadGuidIf(0 != (objFlags & ObjectAceFlags.HasInheritedType), bytes, ref off);

			var sid = new SecurityIdentifier(bytes.Slice(off));
			var ace = new ObjectAce(type, flags, accessMask, objectType, inheritedType, sid);
			return ace;
		}

		private static Guid? ReadGuidIf(bool hasGuid, ReadOnlySpan<byte> bytes, ref int off)
		{
			Guid? inheritedType;
			if (hasGuid)
			{
				inheritedType = MemoryMarshal.Read<Guid>(bytes.Slice(off, 16));
				off += 16;
			}
			else
				inheritedType = null;
			return inheritedType;
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

			int off = 8;
			Guid? objectType = ReadGuidIf(0 != (objFlags & ObjectAceFlags.HasObjectType), bytes, ref off);
			Guid? inheritedType = ReadGuidIf(0 != (objFlags & ObjectAceFlags.HasInheritedType), bytes, ref off);

			var sid = new SecurityIdentifier(bytes.Slice(off));
			var data = bytes.Slice(off + sid.BinaryLength).ToArray();
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
		public abstract uint AccessMask { get; }

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
			) : base(aceType, flags, sid)
		{
			this.AccessMask = accessMask;
		}

		public sealed override uint AccessMask { get; }

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
			) : base(type, flags, sid)
		{
			this.Policy = policy;
		}

		public MandatoryLabelPolicy Policy { get; }
		public override uint AccessMask => (uint)this.Policy;

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

	public interface ICallbackAce
	{
		byte[] ApplicationData { get; }
	}

	// [MS-DTYP] § 2.4.4.6 ACCESS_ALLOWED_CALLBACK_ACE
	public sealed class CallbackAce : AccessControlEntry, ICallbackAce
	{
		public CallbackAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			SecurityIdentifier sid,
			byte[] applicationData
			) : base(type, flags, sid)
		{
			if (applicationData is null) throw new ArgumentNullException(nameof(applicationData));
			this.AccessMask = accessMask;
			this.ApplicationData = applicationData;
		}

		public sealed override uint AccessMask { get; }
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

		internal override int BinaryLength => 8 + this.Trustee.BinaryLength + SecurityDescriptor.Align4(this.ApplicationData.Length);
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

	public interface IObjectAce
	{
		Guid? ObjectType { get; }
		Guid? InheritedObjectType { get; }
	}
	// [MS-DTYP] § 2.4.4.3 ACCESS_ALLOWED_OBJECT_ACE
	public sealed class ObjectAce : AccessControlEntry, IObjectAce
	{
		public ObjectAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			Guid? objectType,
			Guid? inheritedObjectType,
			SecurityIdentifier sid
			) : base(type, flags, sid)
		{
			this.AccessMask = accessMask;
			this.ObjectType = objectType;
			this.InheritedObjectType = inheritedObjectType;
		}

		public Guid? ObjectType { get; }
		public Guid? InheritedObjectType { get; }
		public sealed override uint AccessMask { get; }

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
	public sealed class CallbackObjectAce : AccessControlEntry, IObjectAce, ICallbackAce
	{
		public CallbackObjectAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			Guid? objectType,
			Guid? inheritedObjectType,
			SecurityIdentifier sid,
			byte[] applicationData
			) : base(type, flags, sid)
		{
			if (applicationData is null) throw new ArgumentNullException(nameof(applicationData));
			this.AccessMask = accessMask;
			this.ObjectType = objectType;
			this.InheritedObjectType = inheritedObjectType;
			this.ApplicationData = applicationData;
		}

		public Guid? ObjectType { get; }
		public Guid? InheritedObjectType { get; }
		public sealed override uint AccessMask { get; }
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
	public sealed class ResourceAttributeAce : AccessControlEntry, ICallbackAce
	{
		public ResourceAttributeAce(
			AccessControlEntryType type,
			AccessControlEntryFlags flags,
			uint accessMask,
			SecurityIdentifier sid,
			byte[] attributeData
			) : base(type, flags, sid)
		{
			if (attributeData is null) throw new ArgumentNullException(nameof(attributeData));
			this.AccessMask = accessMask;
			this.AttributeData = attributeData;
		}

		public sealed override uint AccessMask { get; }
		public byte[] AttributeData { get; }
		byte[] ICallbackAce.ApplicationData => this.AttributeData;

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