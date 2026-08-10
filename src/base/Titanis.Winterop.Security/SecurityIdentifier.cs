using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Winterop.Security
{
	/// <summary>
	/// Specifies an authority in a <see cref="SecurityIdentifier"/>
	/// </summary>
	/// <seealso cref="SecurityIdentifier.IdentifierAuthority"/>
	public enum SecurityIdentifierAuthority : long
	{
		Null = 0,
		World = 1,
		Local = 2,
		Creator = 3,
		NonUnique = 4,
		NtAuthority = 5,
		AppPackageAuthority = 0x0F,
		MandatoryLabel = 0x10,
		ScopedPolicyId = 0x11,
		Authentication = 0x12,
	}

	// [MS-DTYP] § 2.4.6 SECURITY_DESCRIPTOR
	/// <summary>
	/// Represents a security identifier.
	/// </summary>
	[TypeConverter(typeof(SecurityIdentifierConverter))]
	public class SecurityIdentifier : IEquatable<SecurityIdentifier>
	{
		public const int RevisionValue = 1;
		private const string DomainPlaceholderPrefix = "S-1-5-21-<domain>-";

		internal SecurityIdentifier(byte[] bytes, int dummy)
		{
			// Read this awkward 6-byte integer
			var idAuthValue = 0
				| (long)bytes[2] << 40
				| (long)bytes[3] << 32
				| (long)bytes[4] << 24
				| (long)bytes[5] << 16
				| (long)bytes[6] << 8
				| bytes[7];
			IdentifierAuthority = (SecurityIdentifierAuthority)idAuthValue;

			// TODO: Trim excess bytes

			_bytes = bytes;
		}

		/// <summary>
		/// Initializes a new <see cref="SecurityIdentifier"/> from its binary representation.
		/// </summary>
		/// <param name="bytes">Buffer containing binary SID</param>
		/// <remarks>
		/// If <paramref name="bytes"/> contains bytes following the SID, they are trimmed without error.
		/// </remarks>
		public SecurityIdentifier(ReadOnlySpan<byte> bytes)
			: this(Trim(bytes).ToArray(), 0)
		{ }
		/// <summary>
		/// Initializes a new <see cref="SecurityIdentifier"/> from its components.
		/// </summary>
		/// <param name="authority">Authority</param>
		public SecurityIdentifier(SecurityIdentifierAuthority authority)
			: this(BuildSidFromComponents(authority, []), 0)
		{ }
		/// <summary>
		/// Initializes a new <see cref="SecurityIdentifier"/> from its components.
		/// </summary>
		/// <param name="authority">Authority</param>
		/// <param name="subauthority">Subauthority</param>
		public SecurityIdentifier(SecurityIdentifierAuthority authority, uint subauthority)
			: this(BuildSidFromComponents(authority, [subauthority]), 0)
		{ }
		/// <summary>
		/// Initializes a new <see cref="SecurityIdentifier"/> from its components.
		/// </summary>
		/// <param name="authority">Authority</param>
		/// <param name="subauthorities">Subauthorities</param>
		public SecurityIdentifier(SecurityIdentifierAuthority authority, ReadOnlySpan<uint> subauthorities)
			: this(BuildSidFromComponents(authority, subauthorities), 0)
		{ }

		private static byte[] BuildSidFromComponents(SecurityIdentifierAuthority authority, ReadOnlySpan<uint> subauthorities)
		{
			if (subauthorities.Length > byte.MaxValue)
				throw new ArgumentException("The number of subauthorities specified exceeds the maximum of 255.", nameof(subauthorities));

			int cb = 8 + subauthorities.Length * 4;
			byte[] buf = new byte[cb];
			BinaryPrimitives.WriteInt64BigEndian(buf, (long)authority);
			buf[0] = RevisionValue;
			buf[1] = (byte)subauthorities.Length;

			for (int i = 0; i < subauthorities.Length; i++)
			{
				var subauth = subauthorities[i];
				BinaryPrimitives.WriteUInt32LittleEndian(buf.AsSpan().Slice(8 + 4 * i), subauth);
			}

			return buf;
		}

		/// <summary>
		/// Gets the size of the binary representation of a SID.
		/// </summary>
		/// <param name="bytes">Buffer containing binary representation</param>
		/// <returns>Number of bytes in the SID</returns>
		/// <exception cref="ArgumentException"><paramref name="bytes"/> is empty</exception>
		/// <exception cref="InvalidDataException"><paramref name="bytes"/> is not a valid SID</exception>
		public static int GetSize(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length < 8)
				throw new ArgumentException("The provided buffer is not large enough to contain a valid security identifier.", nameof(bytes));

			var rev = bytes[0];
			if (rev != RevisionValue)
				throw new InvalidDataException("The buffer does not appear to contain a valid security identifier.");

			var authCount = bytes[1];
			int size = 8 + authCount * 4;
			return size;
		}
		/// <summary>
		/// Removes bytes following the binary representation
		/// </summary>
		/// <param name="bytes">Buffer containing binary representation</param>
		/// <returns>Trimmed buffer</returns>
		/// <exception cref="InvalidDataException"><paramref name="bytes"/> does not contain a complete SID</exception>
		private static ReadOnlySpan<byte> Trim(ReadOnlySpan<byte> bytes)
		{
			var size = GetSize(bytes);
			var authCount = bytes[1];
			if (bytes.Length < 8 + authCount * 4)
				throw new InvalidDataException("The buffer appears to be incomplete.");

			return bytes.Slice(0, GetSize(bytes));
		}

		private byte[] _bytes;
		/// <summary>
		/// Gets the length of the binary representation.
		/// </summary>
		public int BinaryLength => this._bytes.Length;
		/// <summary>
		/// Gets the authority.
		/// </summary>
		public SecurityIdentifierAuthority IdentifierAuthority { get; }
		/// <summary>
		/// Gets the revision.
		/// </summary>
		/// <seealso cref="RevisionValue"/>
		public int Revision => _bytes[0];
		/// <summary>
		/// Gets the number of subauthorities.
		/// </summary>
		public int SubauthorityCount => _bytes[1];
		/// <summary>
		/// Gets the value of the last subauthority.
		/// </summary>
		public uint Rid => this.GetSubauthority(this.SubauthorityCount - 1);

		/// <summary>
		/// Gets a value indicating whether this SID is a placeholder for a domain-specific sid
		/// </summary>
		public bool IsDomainPlaceholder { get; internal set; }

		/// <summary>
		/// Gets the subauthority at the specified index.
		/// </summary>
		/// <param name="index">Zero-based index of subauthority</param>
		/// <returns>Value of subauthority at <paramref name="index"/></returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of range</exception>
		public uint GetSubauthority(int index)
		{
			if ((uint)index >= (uint)SubauthorityCount)
				throw new ArgumentOutOfRangeException(nameof(index), "The index must be non-negative and less than the number of subauthorities.");

			return BinaryPrimitives.ReadUInt32LittleEndian(_bytes.AsSpan().Slice(8 + 4 * index, 4));
		}

		/// <summary>
		/// Gets the list of subauthorities.
		/// </summary>
		/// <returns>An array of the subauthorities</returns>
		public uint[] GetSubauthorities()
		{
			uint[] subauths = new uint[this.SubauthorityCount];
			for (int i = 0; i < subauths.Length; i++)
			{
				subauths[i] = this.GetSubauthority(i);
			}
			return subauths;
		}

		/// <summary>
		/// Parses an integer component of a SID
		/// </summary>
		/// <param name="ctx">SDDL parse context</param>
		/// <param name="n">Parsed value</param>
		/// <returns><see langword="true"/> if an integer was parsed; otherwise, <see langword="false"/></returns>
		/// <remarks>
		/// This method supports both the decimal representation and hex representation when preceded by <c>0x</c>.
		/// </remarks>
		private static bool TryParseSidUInt(ref SddlParseContext ctx, out uint n)
		{
			if (ctx.LengthRemaining == 0 || ctx[0] != '-')
			{
				n = 0;
				return false;
			}

			ctx.Advance(1);

			if (
				(ctx.LengthRemaining > 2)
				&& (ctx[0] == '0')
				&& (ctx[1] == 'x')
				)
			{
				ctx.Advance(2);

				bool valid = false;
				n = 0;

				for (; ctx.LengthRemaining > 0 && BinaryHelper.IsHexChar(ctx[0]); ctx.Advance(1))
				{
					valid = true;
					n *= 16;
					n += (uint)BinaryHelper.ParseHexChar(ctx[0]);
				}

				return valid;
			}
			else
			{
				bool valid = false;
				n = 0;
				for (; ctx.LengthRemaining > 0 && char.IsDigit(ctx[0]); ctx.Advance(1))
				{
					valid = true;
					n *= 10;
					n += (uint)(ctx[0] - '0');
				}

				return valid;
			}
		}

		// [MS-DTYP] 2.4.2.1 - SID String Format Syntax
		public static SecurityIdentifier Parse(ReadOnlySpan<char> text)
		{
			return Parse(text, null);
		}
		public static SecurityIdentifier Parse(ReadOnlySpan<char> text, SecurityIdentifier? domainSid)
		{
			bool isPlaceholder = false;
			bool hasDomainPlaceholder = false;
			if (text.StartsWith(DomainPlaceholderPrefix))
			{
				string sub = "S-1-5-21-1-1-1-";
				Span<char> revised = stackalloc char[text.Length - DomainPlaceholderPrefix.Length + sub.Length];
				sub.AsSpan().CopyTo(revised);
				text.Slice(DomainPlaceholderPrefix.Length).CopyTo(revised.Slice(sub.Length));
				text = sub.AsSpan();
				hasDomainPlaceholder = true;
			}

			var ctx = new SddlParseContext(text);

			var sid = Parse(ref ctx, domainSid);
			sid.IsDomainPlaceholder = ctx.domainSpecific && hasDomainPlaceholder;
			if (ctx.LengthRemaining > 0)
				throw new FormatException("The provided text contained trailing characters that could not be parsed as a valid SID.");

			return sid;
		}

		internal static SecurityIdentifier Parse(ref SddlParseContext ctx, SecurityIdentifier? domainSid)
		{
			if (ctx.LengthRemaining < 2) throw new FormatException("The buffer is too short to contain a valid SID.");

			var c1 = ctx[0];
			var c2 = ctx[1];
			if (char.ToUpper(c1) == 'S' && c2 == '-')
			{
				ctx.Advance(1);

				int offset = 1;
				// Revision
				if (!TryParseSidUInt(ref ctx, out uint rev) || rev != RevisionValue)
					throw new FormatException($"The SID uses the unsupported revision {rev}.");
				// Authority
				if (!TryParseSidUInt(ref ctx, out uint authority))
					throw new FormatException($"The SID does not have an authority.");

				List<uint> subauths = new List<uint>();
				while (
					ctx.LengthRemaining > 0
					&& TryParseSidUInt(ref ctx, out uint n))
				{
					subauths.Add(n);
				}

				int cb = 8 + subauths.Count * 4;
				byte[] buf = new byte[cb];
				BinaryPrimitives.WriteInt64BigEndian(buf, authority);
				buf[0] = RevisionValue;
				buf[1] = (byte)subauths.Count;

				for (int i = 0; i < subauths.Count; i++)
				{
					uint subauth = subauths[i];
					BinaryPrimitives.WriteUInt32LittleEndian(buf.AsSpan().Slice(8 + 4 * i), subauth);
				}

				ctx.domainSpecific = false;
				return new SecurityIdentifier(buf, 0);
			}
			else
			{
				var mapping = WellKnownSidMapping.TryFindWksMappingFromSddlCode(c1, c2);
				if (!mapping.IsValid)
					throw ctx.MakeException($"Unknown WKS '{c1}{c2}");

				ctx.Advance(2);

				ctx.domainSpecific = true;
				var sid = mapping.BuildSid(domainSid);

				return sid;
			}
		}

		public static SecurityIdentifier FromWks(WellKnownSid wks, SecurityIdentifier? domainSid)
		{
			return WellKnownSidMapping.FromWks(wks).BuildSid(domainSid);
		}

		private string? _str;
		/// <inheritdoc/>
		public sealed override string ToString()
			=> _str ??= BuildString();

		// [MS-DTYP] § 2.4.2.1 SID String Format Syntax
		private string BuildString()
		{
			if (this.IsDomainPlaceholder)
				return $"{DomainPlaceholderPrefix}{this.Rid}";

			StringBuilder sb = new StringBuilder();
			sb.Append("S-1-");
			if ((long)IdentifierAuthority <= uint.MaxValue)
				sb.Append((long)IdentifierAuthority);
			else
				sb.Append("0x").AppendFormat("{0:X}", (long)IdentifierAuthority);

			int cSubauth = SubauthorityCount;
			for (int i = 0; i < cSubauth; i++)
			{
				var subauth = GetSubauthority(i);

				sb.Append('-').Append(subauth);
			}

			return sb.ToString();
		}

		#region Equality
		/// <inheritdoc/>
		public sealed override int GetHashCode()
		{
			int hash = this.IdentifierAuthority.GetHashCode();
			for (int i = 0; i < this.SubauthorityCount; i++)
			{
				var subauth = this.GetSubauthority(i);
				hash = HashCode.Combine(hash, subauth);
			}
			return hash;
		}

		/// <inheritdoc/>
		public sealed override bool Equals(object? obj) => (obj is SecurityIdentifier other) && this.Equals(other);
		/// <inheritdoc/>
		public bool Equals(SecurityIdentifier? other)
		{
			if (other is null)
				return false;

			if (
				(this.IdentifierAuthority == other.IdentifierAuthority)
				&& (this.SubauthorityCount == this.SubauthorityCount)
				)
			{
				for (int i = 0; i < this.SubauthorityCount; i++)
				{
					var subauthX = this.GetSubauthority(i);
					var subauthY = other.GetSubauthority(i);
					if (subauthX != subauthY)
						return false;
				}

				return true;
			}

			return false;
		}

		public static bool operator ==(SecurityIdentifier? x, SecurityIdentifier? y) => object.ReferenceEquals(x, y) || (x?.Equals(y) ?? false);
		public static bool operator !=(SecurityIdentifier? x, SecurityIdentifier? y) => !(x == y);

		#endregion



		private WellKnownSid FindWksMapping()
		{
			WellKnownSidAuthorityMapping authmap;
			if (this.SubauthorityCount == 1)
			{
				authmap = this.IdentifierAuthority switch
				{
					SecurityIdentifierAuthority.Null => WellKnownSidAuthorityMapping.Null,
					SecurityIdentifierAuthority.World => WellKnownSidAuthorityMapping.World,
					SecurityIdentifierAuthority.Local => WellKnownSidAuthorityMapping.Local,
					SecurityIdentifierAuthority.Creator => WellKnownSidAuthorityMapping.Creator,
					SecurityIdentifierAuthority.NtAuthority => WellKnownSidAuthorityMapping.NtAuthority,
					SecurityIdentifierAuthority.MandatoryLabel => WellKnownSidAuthorityMapping.MandatoryLabel,
					SecurityIdentifierAuthority.Authentication => WellKnownSidAuthorityMapping.AuthenticationAuthority,
					_ => WellKnownSidAuthorityMapping.Invalid
				};
			}
			else if (this.IdentifierAuthority == SecurityIdentifierAuthority.NtAuthority && this.SubauthorityCount == 5 && this.GetSubauthority(0) == 21)
			{
				var s1 = this.GetSubauthority(1);
				var s2 = this.GetSubauthority(2);
				var s3 = this.GetSubauthority(3);
				if (s1 == 0 && s2 == 0 && s3 == 0)
					authmap = WellKnownSidAuthorityMapping.Claims;
				else
					authmap = WellKnownSidAuthorityMapping.DomainSpecific;
			}
			else if (this.IdentifierAuthority == SecurityIdentifierAuthority.NtAuthority && this.SubauthorityCount == 2)
			{
				var subauth0 = this.GetSubauthority(0);
				authmap = subauth0 switch
				{
					5 => WellKnownSidAuthorityMapping.LogonSession,
					32 => WellKnownSidAuthorityMapping.Builtin,
					64 => WellKnownSidAuthorityMapping.SecurityProviders,
					65 => WellKnownSidAuthorityMapping.ThisOrganization,
					80 => WellKnownSidAuthorityMapping.NtService,
					//83=> WellKnownSidAuthorityMapping.NtVirtualMachine,
					90 => WellKnownSidAuthorityMapping.WindowManager,
					_ => WellKnownSidAuthorityMapping.Invalid
				};
			}
			else if (this.IdentifierAuthority == SecurityIdentifierAuthority.AppPackageAuthority && this.SubauthorityCount == 2)
			{
				var subauth0 = this.GetSubauthority(0);
				authmap = subauth0 switch
				{
					2 => WellKnownSidAuthorityMapping.AppContainer,
					3 => WellKnownSidAuthorityMapping.Capabilities,
					_ => WellKnownSidAuthorityMapping.Invalid
				};
			}
			else
			{
				authmap = WellKnownSidAuthorityMapping.Invalid;
			}

			if (authmap == WellKnownSidAuthorityMapping.Invalid)
				return WellKnownSid.Unknown;

			return WellKnownSidMapping.FindSimpleMapping(authmap, this.Rid);
		}

		/// <summary>
		/// Gets the <see cref="WellKnownSid"/> for the this <see cref="SecurityIdentifier"/>.
		/// </summary>
		/// <returns>A <see cref="WellKnownSid"/> value.  If this SID doesn't map to a <see cref="WellKnownSid"/> value, this method returns <see cref="WellKnownSid.Unknown"/>.</returns>
		public WellKnownSid AsWellKnownSid()
		{
			return FindWksMapping();
		}

		/// <summary>
		/// Gets the SDDL code for the this <see cref="SecurityIdentifier"/>.
		/// </summary>
		/// <returns>A string representing this SID in SDDL, if available; otherwise, <see langword="null"/></returns>
		public string? AsSddlCode()
		{
			var wks = FindWksMapping();
			var code = WellKnownSidMapping.TryMapWksToCode(wks);
			return code;
		}

		/// <summary>
		/// Gets the string representing this SID within SDDL.
		/// </summary>
		/// <returns>An SDDL code if available; otherwise, the string representation of the SID.</returns>
		public string ToSddlString()
		{
			var str = this.AsSddlCode() ?? this.ToString();
			return str;
		}

		public byte[] GetBytes()
			=> (byte[])this._bytes.Clone();
		public int GetBytes(Span<byte> bytes)
		{
			if (bytes.Length < this._bytes.Length)
				throw new ArgumentException("The buffer is too small to contain the SID.  The buffer must be at least the length indicated by BinaryLength.");

			this._bytes.CopyTo(bytes);

			return this._bytes.Length;
		}

		/// <summary>
		/// Creates a new RID by concatenating a RID to this SID.
		/// </summary>
		/// <param name="relativeId">RID to concacenate</param>
		/// <returns>A <see cref="SecurityIdentifier"/> with <paramref name="relativeId"/> appended.</returns>
		public SecurityIdentifier Concat(uint relativeId)
		{
			var bytes = new byte[this._bytes.Length + 4];
			this.GetBytes(bytes);
			bytes[1]++;
			BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(bytes.Length - 4, 4), relativeId);
			return new SecurityIdentifier(bytes);
		}

		// [MS-DTYP] § 2.5.3.1.2 SidDominates
		public bool SidDominates(SecurityIdentifier other)
		{
			var sid1 = this;

			if (other is null) throw new ArgumentNullException(nameof(other));

			if (sid1 == other)
				return true;

			if (other.SubauthorityCount > sid1.SubauthorityCount)
				return false;

			uint[] subauths = sid1.GetSubauthorities();
			uint[] subauths2 = other.GetSubauthorities();
			for (int i = 0; i < subauths.Length; i++)
			{
				uint subauth1 = subauths[i];
				uint subauth2 = subauths2[i];
				if (subauth1 >= subauth2)
					return true;
			}

			return false;
		}
	}

	public class SecurityIdentifierConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}
		public sealed override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				return SecurityIdentifier.Parse(str.AsSpan(), SecurityDescriptorConverter.PlaceholderDomainSid);
			}
			else
				return base.ConvertFrom(context, culture, value);
		}

		public sealed override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return
				(destinationType == typeof(string))
				|| base.CanConvertTo(context, destinationType);
		}

		public sealed override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				var sid = (SecurityIdentifier)value;
				return sid.ToString();

			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
