using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Winterop.Security;

namespace Titanis.Ldap
{
	/// <summary>
	/// Provides each of the syntaxes used by Active Directory.
	/// </summary>
	/// <remarks>
	/// Use <see cref="TryGetSyntax(string, int, string?)"/> to get a syntax by attributeSyntax, omSyntax, and omObjectClass.
	/// </remarks>
	public static class AdSyntaxes
	{
		public static readonly BooleanSyntax Boolean = new BooleanSyntax();
		public static readonly LdapSyntax Enumeration = new EnumerationSyntax();
		public static readonly LdapSyntax Integer = new IntegerSyntax();
		public static readonly LdapSyntax LargeInteger = new LargeIntegerSyntax();
		public static readonly LdapSyntax LargeInteger_Timestamp = new LargeIntegerTimestampSyntax();
		public static readonly LdapSyntax ObjectAccessPoint = new ObjectAccessPointSyntax();
		public static readonly LdapSyntax ObjectDnString = new ObjectDnStringSyntax();
		public static readonly LdapSyntax ObjectOrName = new ObjectOrNameSyntax();
		public static readonly LdapSyntax ObjectDnBinary = new ObjectDnBinarySyntax();
		public static readonly LdapSyntax ObjectDsDn = new ObjectDsDnSyntax();
		public static readonly LdapSyntax ObjectPresentationAddress = new ObjectPresentationAddressSyntax();
		public static readonly LdapSyntax ObjectReplicaLink = new ObjectReplicaLinkSyntax();
		public static readonly LdapSyntax DnsRecord = new DnsRecordSyntax();
		public static readonly LdapSyntax StringCase = new StringCaseSyntax();
		public static readonly LdapSyntax StringIa5 = new StringIa5Syntax();
		public static readonly LdapSyntax StringNtSecDesc = new StringNtSecDescSyntax();
		public static readonly LdapSyntax StringNumeric = new StringNumericSyntax();
		public static readonly LdapSyntax StringObjectIdentifier = new StringObjectIdentifierSyntax();
		public static readonly LdapSyntax StringOctet = new StringOctetSyntax();
		public static readonly LdapSyntax StringPrintable = new StringPrintableSyntax();
		public static readonly LdapSyntax StringSid = new StringSidSyntax();
		public static readonly LdapSyntax StringTeletex = new StringTeletexSyntax();
		public static readonly LdapSyntax StringUnicode = new StringUnicodeSyntax();
		public static readonly LdapSyntax StringUtcTime = new StringUtcTimeSyntax();
		public static readonly LdapSyntax StringGeneralizedTime = new StringGeneralizedTimeSyntax();

		// HACK: Used for OM-Object-Class
		public static readonly LdapSyntax ObjectReplicaLink_Oid = new ObjectReplicaLinkOidSyntax();
		public static readonly LdapSyntax StringOctetGuid = new StringOctetGuidSyntax();

		private static readonly LdapSyntax[] _all = [
			Boolean,
			Enumeration,
			Integer,
			LargeInteger,
			ObjectAccessPoint,
			ObjectDnString,
			ObjectOrName,
			ObjectDnBinary,
			ObjectDsDn,
			ObjectPresentationAddress,
			ObjectReplicaLink,
			StringCase,
			StringIa5,
			StringNtSecDesc,
			StringNumeric,
			StringObjectIdentifier,
			StringOctet,
			StringPrintable,
			StringSid,
			StringTeletex,
			StringUnicode,
			StringUtcTime,
			StringGeneralizedTime,
			];

		record struct AdSyntaxKey(string oid, int omSyntax, string? omObjectClass);
		private static readonly Dictionary<AdSyntaxKey, LdapSyntax> _syntaxesByAdKey = BuildIndex();
		private static readonly Dictionary<string, LdapSyntax> _syntaxesByAdName = BuildNameIndex();

		private static Dictionary<AdSyntaxKey, LdapSyntax> BuildIndex()
		{
			return _all.ToDictionary(r => new AdSyntaxKey(r.ActiveDirectoryOid, r.OmSyntax, r.OmObjectClass));
		}

		private static Dictionary<string, LdapSyntax> BuildNameIndex()
		{
			return _all.ToDictionary(r => r.ActiveDirectoryName!, StringComparer.OrdinalIgnoreCase);
		}

		public static LdapSyntax? TryGetSyntax(string oid, int omSyntax, string? omObjectClass)
		{
			_syntaxesByAdKey.TryGetValue(new AdSyntaxKey(oid, omSyntax, omObjectClass), out var syntax);
			return syntax;
		}
	}

	// [RFC 2522] § 6.4
	// [RFC 4517] § 3.3.3
	/// <summary>
	/// Encodes a boolean true/false value.
	/// </summary>
	public sealed class BooleanSyntax : LdapSyntax<bool>
	{
		internal BooleanSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Boolean";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.7";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Boolean";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.8";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 1;

		private static readonly byte[] TrueBytes = new byte[] {
			(byte)'T', (byte)'R', (byte)'U', (byte)'E'
		};
		private static readonly byte[] FalseBytes = new byte[] {
			(byte)'F', (byte)'A', (byte)'L', (byte)'S', (byte)'E'
		};

		/// <inheritdoc/>
		protected sealed override bool DecodeImpl(byte[] bytes)
		{
			bool value = bytes switch
			{
				[(byte)'T', (byte)'R', (byte)'U', (byte)'E'] => true,
				[(byte)'F', (byte)'A', (byte)'L', (byte)'S', (byte)'E'] => false,
				_ => throw new ArgumentException($"Invalid boolean", nameof(bytes))
			};
			return value;
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(bool value)
		{
			return value ? TrueBytes : FalseBytes;
		}

		public override object Parse(string text) => bool.Parse(text);
	}

	// [RFC 2252] § 6.16
	// [RFC 4517] § 3.3.16
	// TODO: AD doesn't mention a size limit; let's try 32 and see what happens
	public class EnumerationSyntax : LdapSyntax<int>
	{
		internal EnumerationSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "INTEGER";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.27";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Enumeration";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.9";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 10;

		/// <inheritdoc/>
		protected sealed override int DecodeImpl(byte[] bytes)
		{
			return int.Parse(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(int value)
		{
			return Encoding.UTF8.GetBytes(value.ToString());
		}

		public override object Parse(string text) => int.Parse(text);
	}

	// [RFC 2252] § 6.16
	// [RFC 4517] § 3.3.16
	// TODO: The RFC mentions an integer of unlimited length, but in AD it's an Int32.
	public sealed class IntegerSyntax : LdapSyntax<int>
	{
		internal IntegerSyntax()
		{

		}
		/// <inheritdoc/>
		public sealed override string RfcName => "INTEGER";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.27";

		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryName => "Integer";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.9";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 2;

		/// <inheritdoc/>
		protected sealed override int DecodeImpl(byte[] bytes)
		{
			return int.Parse(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(int value)
		{
			return Encoding.UTF8.GetBytes(value.ToString());
		}

		public override object Parse(string text)
		{
			return int.Parse(text);
		}

		// [MS-DRSR] § 5.16.1.1
		/// <inheritdoc/>
		public override object DecodeDsrep(byte[] bytes)
		{
			if (bytes.Length != 4)
				throw new FormatException($"The encoded value must be exactly 4 bytes.");

			return BinaryPrimitives.ReadInt32LittleEndian(bytes);
		}
		// [MS-DRSR] § 5.16.1.1
		/// <inheritdoc/>
		public override byte[] EncodeDsrep(object obj)
		{
			byte[] bytes = new byte[4];
			BinaryPrimitives.WriteInt32LittleEndian(bytes, (int)obj);
			return bytes;
		}
	}

	// [RFC 4517] § 3.3.16
	// [RFC 2252] § 6.16
	// TODO: The RFC mentions an integer of unlimited length, but in AD it's an Int64.
	public sealed class LargeIntegerSyntax : LdapSyntax<long>
	{
		internal LargeIntegerSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "INTEGER";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.906";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryName => "LargeInteger";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.16";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 65;

		/// <inheritdoc/>
		protected sealed override long DecodeImpl(byte[] bytes)
		{
			return long.Parse(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(long value)
		{
			return Encoding.UTF8.GetBytes(value.ToString());
		}

		public override object Parse(string text) => long.Parse(text);

		// [MS-DRSR] § 5.16.1.2
		/// <inheritdoc/>
		public override object DecodeDsrep(byte[] bytes)
		{
			if (bytes.Length != 8)
				throw new FormatException($"The encoded value must be exactly 8 bytes.");

			return BinaryPrimitives.ReadInt64LittleEndian(bytes);
		}
		// [MS-DRSR] § 5.16.1.1
		/// <inheritdoc/>
		public override byte[] EncodeDsrep(object obj)
		{
			byte[] bytes = new byte[8];
			BinaryPrimitives.WriteInt64LittleEndian(bytes, (long)obj);
			return bytes;
		}
	}

	public struct AdTimestamp
	{
		public AdTimestamp(long value)
		{
			this.Value = value;
		}
		public AdTimestamp(DateTime value)
		{
			this.Value = value.ToFileTimeUtc();
		}

		public readonly long Value { get; }

		public readonly DateTime AsDateTimeUtc() => DateTime.FromFileTimeUtc(this.Value);

		public override string ToString() => $"{this.Value:N0} ({this.AsDateTimeUtc():O})";

		private static Regex rgxRelative = new Regex(@"^((?<t>today)|(?<n>now))\s*((?<s>\+|-)\s*(?<d>.*))?$", RegexOptions.IgnoreCase);
		public static AdTimestamp Parse(string text)
		{
			if (long.TryParse(text, NumberStyles.AllowThousands, null, out var n))
			{
				return new AdTimestamp(n);
			}
			else if (DateTime.TryParse(text, out var dt))
			{
				return new AdTimestamp(dt);
			}
			else
			{
				var m = rgxRelative.Match(text);
				if (m.Success)
				{
					dt = m.Groups["t"].Success ? DateTime.UtcNow.Date : DateTime.UtcNow;
					Group signGroup = m.Groups["s"];
					if (signGroup.Success)
					{
						var sign = signGroup.Value[0];
						var duration = Duration.Parse(m.Groups["d"].Value).TimeSpan;
						dt = sign switch
						{
							'-' => dt - duration,
							'+' => dt + duration
						};
					}
					return new AdTimestamp(dt);
				}

				throw new ArgumentException($"Could not parse timestamp as either a numeric value or date/time.");
			}
		}
	}
	// cf.:
	// [RFC 4517] § 3.3.16
	// [RFC 2252] § 6.16
	// TODO: The RFC mentions an integer of unlimited length, but in AD it's an Int64.
	public sealed class LargeIntegerTimestampSyntax : LdapSyntax<AdTimestamp>
	{
		internal LargeIntegerTimestampSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "INTEGER";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.906";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryName => "LargeInteger";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.16";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 65;

		/// <inheritdoc/>
		protected sealed override AdTimestamp DecodeImpl(byte[] bytes)
		{
			return AdTimestamp.Parse(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(AdTimestamp value)
		{
			return Encoding.UTF8.GetBytes(value.Value.ToString());
		}

		public override object Parse(string text) => AdTimestamp.Parse(text);

		// [MS-DRSR] § 5.16.1.2
		/// <inheritdoc/>
		public override object DecodeDsrep(byte[] bytes)
		{
			if (bytes.Length != 8)
				throw new FormatException($"The encoded value must be exactly 8 bytes.");

			return new AdTimestamp(BinaryPrimitives.ReadInt64LittleEndian(bytes));
		}
		// [MS-DRSR] § 5.16.1.1
		/// <inheritdoc/>
		public override byte[] EncodeDsrep(object obj)
		{
			byte[] bytes = new byte[8];
			BinaryPrimitives.WriteInt64LittleEndian(bytes, ((AdTimestamp)obj).Value);
			return bytes;
		}
	}

	public class AccessPointRef
	{
		public AccessPointRef(PresentationAddress presentationAddress, LdapDistinguishedName dn)
		{
			ArgumentNullException.ThrowIfNull(presentationAddress);
			ArgumentNullException.ThrowIfNull(dn);
			PresentationAddress = presentationAddress;
			Dn = dn;
		}

		public PresentationAddress PresentationAddress { get; }
		public LdapDistinguishedName Dn { get; }

		public string Text => $"{this.PresentationAddress.Text}#X500:{this.Dn.Text}";
		public sealed override string ToString() => this.Text;
	}

	// [MS-ADTS] § 3.1.1.2.2.2
	// [MS-ADTS] § 3.1.1.3.1.1.1 subSchema
	public sealed partial class ObjectAccessPointSyntax : LdapSyntax<AccessPointRef>
	{
		internal ObjectAccessPointSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Access Point";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.2";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Access-Point)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.14";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.3.12.2.1011.28.0.702";

		protected sealed override AccessPointRef DecodeImpl(byte[] bytes)
		{
			string str = Encoding.UTF8.GetString(bytes);
			return Parse(str);
		}

		public override AccessPointRef Parse(string text)
		{
			Match m = rgxAP.Match(text);
			if (!m.Success)
				throw new ArgumentException($"The value '{text}' is not a valid access point reference.");

			return new AccessPointRef(new PresentationAddress(m.Groups["pa"].Value), new LdapDistinguishedName(m.Groups["dn"].Value));
		}

		protected sealed override byte[] EncodeImpl(AccessPointRef value)
		{
			string str = value.PresentationAddress.Text + "#X500:" + value.Dn.Text;
			return Encoding.UTF8.GetBytes(str);
		}

		private static readonly Regex rgxAP = AccessPointRegex();

		[GeneratedRegex(@"^(?<pa>.*)#X500:(?<dn>.*)$")]
		private static partial Regex AccessPointRegex();
	}


	// [MS-ADTS] § 3.1.1.2.2.2.1
	public class DnString
	{
		public DnString(string value, LdapDistinguishedName dn)
		{
			Value = value;
			Dn = dn;
		}

		public string Value { get; }
		public LdapDistinguishedName Dn { get; }
		public string Text => $"{this.Value}:{this.Dn.Text}";
	}

	// [MS-ADTS] § 3.1.1.2.2.2.1
	public sealed class ObjectDnStringSyntax : LdapSyntax<DnString>
	{
		internal ObjectDnStringSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => null;
		/// <inheritdoc/>
		public sealed override string RfcOid => null;
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(DN-String)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.14";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.12";

		protected sealed override DnString DecodeImpl(byte[] bytes)
		{
			(var str, var dn) = DecodeDN(bytes, 'S');
			return new DnString(str, dn);
		}
		protected sealed override byte[] EncodeImpl(DnString value)
		{
			string valuePart = value.Value;
			var byteCount_value = Encoding.UTF8.GetByteCount(valuePart);
			string str = $"S:{byteCount_value}:{valuePart}:{value.Dn.Text}";
			return Encoding.UTF8.GetBytes(str);
		}

		public override object Parse(string text)
		{
			// TODO: How should this be handled?  Should there be a friendlier syntax for the user?
			throw new NotImplementedException();
		}

		internal static (string, LdapDistinguishedName) DecodeDN(byte[] bytes, char expectedPrefix)
		{
			if (bytes.Length < 4 || bytes[0] != (byte)expectedPrefix || bytes[1] != (byte)':')
				throw new ArgumentException("Not a valid Object(DN-x)", nameof(bytes));
			int byteCount = 0;
			bool complete = false;
			int i;
			for (i = 2; i < bytes.Length; i++)
			{
				var c = (char)bytes[i];
				if (c == (byte)':')
				{
					complete = true;
					break;
				}
				if (!char.IsDigit(c))
					throw new ArgumentException($"Not a valid Object(DN-String); expected decimal digit at position {i}. ", nameof(bytes));

				byteCount *= 10;
				byteCount += (c - '0');
			}

			if (!complete)
				throw new ArgumentException($"Not a valid Object(DN-String); expected ':' at position {i}. ", nameof(bytes));

			i++;

			if ((i + byteCount) > bytes.Length)
				throw new ArgumentException($"The byte count {byteCount} exceeds the number of bytes remaining in the string.");
			// TODO: What if there are extra bytes remaining?

			var str = Encoding.UTF8.GetString(bytes.AsSpan(i, byteCount));

			var dnPart = Encoding.UTF8.GetString(bytes.AsSpan(i + byteCount + 1));
			var dn = new LdapDistinguishedName(dnPart);

			return (str, dn);
		}
	}


	// [MS-ADTS] § 3.1.1.2.2.2.4
	public sealed class ObjectOrNameSyntax : LdapSyntax<LdapDistinguishedName>
	{
		internal ObjectOrNameSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.1221";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(OR-Name)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.7";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "2.6.6.1.2.5.11.29";

		protected sealed override LdapDistinguishedName DecodeImpl(byte[] bytes)
		{
			return Parse(Encoding.UTF8.GetString(bytes));
		}

		public override LdapDistinguishedName Parse(string text)
		{
			return new LdapDistinguishedName(text);
		}

		protected sealed override byte[] EncodeImpl(LdapDistinguishedName value)
		{
			return Encoding.UTF8.GetBytes(value.Text);
		}
	}

	// [MS-ADTS] § 3.1.1.2.2.2.3
	public class DnBinary
	{
		public DnBinary(byte[] binary, LdapDistinguishedName dn)
		{
			Binary = binary;
			Dn = dn;
		}

		public byte[] Binary { get; }
		public LdapDistinguishedName Dn { get; }
		private string? _text;
		public string Text => (this._text ??= this.BuildText());
		private string BuildText()
		{
			return $"{this.Binary.ToHexString()}:{this.Dn.Text}";
		}

		public override string ToString() => this.Text;
	}
	// [MS-ADTS] § 3.1.1.2.2.2.3
	public sealed class ObjectDnBinarySyntax : LdapSyntax<DnBinary>
	{
		internal ObjectDnBinarySyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.903";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(DN-Binary)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.7";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.11";

		protected sealed override DnBinary DecodeImpl(byte[] bytes)
		{
			(var str, var dn) = ObjectDnStringSyntax.DecodeDN(bytes, 'B');
			return new DnBinary(BinaryHelper.ParseHexString(str), dn);
		}

		protected sealed override byte[] EncodeImpl(DnBinary value)
		{
			string namePart = BinaryHelper.ToHexString(value.Binary);
			string str = $"B:{namePart.Length}:{namePart}:{value.Dn.Text}";
			return Encoding.UTF8.GetBytes(str);
		}

		public override object Parse(string text)
		{
			return DecodeImpl(Encoding.UTF8.GetBytes(text));
		}
	}

	// [RFC 2252] § 6.9
	// [MS-ADTS] § 3.1.1.1.6
	public sealed class ObjectDsDnSyntax : LdapSyntax<LdapDistinguishedName>
	{
		internal ObjectDsDnSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => "DN";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.12";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(DS-DN)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.1";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.3.12.2.1011.28.0.714";

		/// <inheritdoc/>
		protected sealed override LdapDistinguishedName DecodeImpl(byte[] bytes)
		{
			string text = Encoding.UTF8.GetString(bytes);
			return Parse(text);
		}

		public override LdapDistinguishedName Parse(string text)
		{
			return new LdapDistinguishedName(text);
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(LdapDistinguishedName value)
		{
			return Encoding.UTF8.GetBytes(value.Text);
		}
	}

	// [RFC 1278]
	public sealed class PresentationAddress
	{
		public PresentationAddress(string text)
		{
			Text = text ?? string.Empty;
		}

		public string Text { get; }

		public sealed override string ToString() => this.Text;
	}

	// [MS-ADTS] § 1.3.12.2.1011.28.0.732
	// [RFC 2252] § 6.28
	// [RFC 1278]
	public sealed class ObjectPresentationAddressSyntax : LdapSyntax<PresentationAddress>
	{
		internal ObjectPresentationAddressSyntax()
		{

		}


		/// <inheritdoc/>
		public sealed override string? RfcName => "PresentationAddress";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.43";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Presentation-Address)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.13";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.3.12.2.1011.28.0.732";


		protected sealed override PresentationAddress DecodeImpl(byte[] bytes)
		{
			return Parse(Encoding.UTF8.GetString(bytes));
		}

		public override PresentationAddress Parse(string text)
		{
			return new PresentationAddress(text);
		}

		protected sealed override byte[] EncodeImpl(PresentationAddress value)
		{
			return Encoding.UTF8.GetBytes(value.Text);
		}
	}

	// [RFC 2252] § 6.2
	// [RFC 2252] § 4.3.1
	public abstract class Asn1EncodedSyntax<T> : LdapSyntax<T>
	{
		protected abstract T DecodeOctets(byte[] bytes);
		protected sealed override T DecodeImpl(byte[] bytes)
		{
			if (bytes is null || bytes.Length == 0)
				throw new ArgumentException($"The value does not contain any data.", nameof(bytes));

			//var decoder = Asn1DerEncoding.CreateDerDecoder(bytes, Asn1DerDecoderOptions.AllowBer);
			//bytes = decoder.DecodeOctetStringTlv();

			return this.DecodeOctets(bytes);
		}

		protected abstract byte[] EncodeOctets(T value);
		protected sealed override byte[] EncodeImpl(T value)
		{
			ArgumentNullException.ThrowIfNull(value);

			return this.EncodeOctets(value);
			//var octets = this.EncodeOctets(value);

			//var encoder = Asn1DerEncoding.CreateDerEncoder();
			//encoder.EncodeOctetStringTlv(octets);

			//return encoder.GetBytes().ToArray();
		}

	}

	public sealed class ObjectReplicaLinkSyntax : Asn1EncodedSyntax<BinaryString>
	{
		internal ObjectReplicaLinkSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => "Binary";
		/// <inheritdoc/>
		public sealed override string RfcOid => "OctetString";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Replica-Link)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.6";

		protected sealed override BinaryString DecodeOctets(byte[] bytes)
		{
			return new BinaryString(bytes);
		}

		protected sealed override byte[] EncodeOctets(BinaryString value)
		{
			return value.Bytes;
		}

		public override object Parse(string text)
		{
			return new BinaryString(BinaryHelper.ParseHexString(text));
		}
	}


	public sealed class ObjectReplicaLinkOidSyntax : LdapSyntax<Asn1Oid>
	{
		internal ObjectReplicaLinkOidSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => "Object(Replica-Link)";
		/// <inheritdoc/>
		public sealed override string RfcOid => "OctetString";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Replica-Link)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 4;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.6";

		protected sealed override Asn1Oid DecodeImpl(byte[] bytes)
		{
			return Asn1DerDecoder.DecodeValue<Asn1Oid>(bytes);
		}

		protected sealed override byte[] EncodeImpl(Asn1Oid value)
		{
			return Asn1DerEncoder.EncodeValue(value).ToArray();
		}

		public override object Parse(string text)
		{
			return new Asn1Oid(text);
		}
	}





	public abstract class StringSyntax : LdapSyntax<string>
	{
		protected StringSyntax() { }

		public virtual Encoding Encoding => Encoding.UTF8;
		/// <inheritdoc/>
		protected sealed override string DecodeImpl(byte[] bytes)
		{
			return this.Encoding.GetString(bytes);
		}
		protected sealed override byte[] EncodeImpl(string value)
		{
			return this.Encoding.GetBytes(value);
		}

		public override object Parse(string text) => text;

		public override object DecodeDsrep(byte[] bytes)
		{
			return Encoding.Unicode.GetString(bytes);
		}
		public override byte[] EncodeDsrep(object obj)
		{
			return Encoding.Unicode.GetBytes((string)obj);
		}
	}

	// [MS-ADTS] § 3.1.1.2.2.2.5
	public sealed class StringCaseSyntax : StringSyntax
	{
		internal StringCaseSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.1362";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Case)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.3";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 27;
	}

	// [RFC 2252] § 6.15
	// [RFC 4517] § 3.3.15
	public sealed class StringIa5Syntax : StringSyntax
	{
		internal StringIa5Syntax() { }

		/// <inheritdoc/>
		public sealed override string RfcName => "IA5 String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.26";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(IA5)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.5";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 22;
	}

	// [MS-ADTS] § 3.1.1.2.2.2.6
	public sealed class StringNtSecDescSyntax : Asn1EncodedSyntax<SecurityDescriptor>
	{
		internal StringNtSecDescSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.907";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(NT-Sec-Desc)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.15";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 66;

		protected sealed override SecurityDescriptor DecodeOctets(byte[] bytes)
		{
			return new SecurityDescriptor(bytes);
		}
		protected sealed override byte[] EncodeOctets(SecurityDescriptor value)
		{
			return value.ToByteArray();
		}

		public override object Parse(string text)
		{
			return SecurityDescriptor.ParseSddl(text, null);
		}
	}

	// [RFC 4517] § 3.3.23
	public sealed class StringNumericSyntax : StringSyntax
	{
		internal StringNumericSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "Numeric String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.36";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.6";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Numeric)";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 18;
	}

	// [RFC 2252] § 6.25
	// [RFC 4517] § 3.3.26
	public sealed class StringObjectIdentifierSyntax : StringSyntax
	{
		internal StringObjectIdentifierSyntax() { }

		/// <inheritdoc/>
		public sealed override string RfcName => "OID";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.38";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Object-Identifier)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.2";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 6;
	}

	// [RFC 2252] § 6.2
	// [RFC 4517] § 3.3.25
	public sealed class StringOctetSyntax : Asn1EncodedSyntax<BinaryString>
	{
		internal StringOctetSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "Binary";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.40";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Octet)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 4;

		/// <inheritdoc/>
		protected sealed override BinaryString DecodeOctets(byte[] bytes)
		{
			return new BinaryString(bytes);
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeOctets(BinaryString value)
		{
			return value.Bytes;
		}

		public override object Parse(string text)
		{
			return new BinaryString(BinaryHelper.ParseHexString(text));
		}
	}

	public sealed class StringOctetGuidSyntax : Asn1EncodedSyntax<Guid>
	{
		internal StringOctetGuidSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "Binary";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.40";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Octet)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 4;

		/// <inheritdoc/>
		protected sealed override Guid DecodeOctets(byte[] bytes)
		{
			return new Guid(bytes);
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeOctets(Guid value)
		{
			return value.ToByteArray();
		}

		public override object Parse(string text)
		{
			return Guid.Parse(text);
		}
	}

	// [RFC 2252] § 6.29
	// [RFC 4517] § 3.3.29
	public sealed class StringPrintableSyntax : StringSyntax
	{
		internal StringPrintableSyntax() { }

		/// <inheritdoc/>
		public sealed override string RfcName => "Printable String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.44";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Printable)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.5";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 19;
	}

	// [MS-ADTS] § 3.1.1.2.2.2.7
	public sealed class StringSidSyntax : Asn1EncodedSyntax<SecurityIdentifier>
	{
		internal StringSidSyntax() { }

		/// <inheritdoc/>
		public sealed override string RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.40";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Sid)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.17";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 4;

		/// <inheritdoc/>
		protected sealed override SecurityIdentifier DecodeOctets(byte[] bytes)
		{
			return new SecurityIdentifier(bytes);
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeOctets(SecurityIdentifier value)
		{
			return value.GetBytes();
		}

		public override object Parse(string text)
		{
			// TODO: Parse domain-specific SIDs
			return SecurityIdentifier.Parse(text);
		}
	}

	// [MS-ADTS] § 3.1.1.2.2.2.8
	public sealed class StringTeletexSyntax : StringSyntax
	{
		internal StringTeletexSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => this.ActiveDirectoryName;
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.2.840.113556.1.4.905";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Teletex)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.4";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 20;
	}

	// [RFC 2252] § 6.10
	public sealed class StringUnicodeSyntax : StringSyntax
	{
		internal StringUnicodeSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "Directory String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.15";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Unicode)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.12";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 64;
	}


	// [RFC 2252] § 6.31
	public sealed class StringUtcTimeSyntax : StringSyntax
	{
		internal StringUtcTimeSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "UTC Time";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.53";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(UTC-Time)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.11";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 23;
	}

	// [RFC 2252] § 6.31
	public sealed class StringGeneralizedTimeSyntax : StringSyntax
	{
		internal StringGeneralizedTimeSyntax() { }

		/// <inheritdoc/>
		public sealed override string? RfcName => "Generalized Time";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.24";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "String(Generalized-Time)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.11";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 24;
	}

}
