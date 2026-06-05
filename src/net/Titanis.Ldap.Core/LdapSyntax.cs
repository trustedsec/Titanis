using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Titanis.Ldap
{
	// [RFC 4512] § 3.3
	public static class LdapSyntaxes
	{
		public static readonly LdapSyntax LargeInteger = new LargeIntegerSyntax();

		private static readonly LdapSyntax[] _all = [
			LargeInteger,
			];
	}


	// [RFC 4517]
	/// <summary>
	/// Represents a syntax used within LDAP.
	/// </summary>
	/// <remarks>
	/// A syntax defines the encoding of an attribute value.
	/// </remarks>
	/// <seealso cref="LdapSyntaxes"/>
	/// <seealso cref="AttributeTypeDescription.Syntax"/>
	public abstract class LdapSyntax
	{
		protected LdapSyntax()
		{
		}

		/// <summary>
		/// Gets the name of the syntax defined in the RFC.
		/// </summary>
		public abstract string? RfcName { get; }
		/// <summary>
		/// Gets the OID of the syntax defined by LDAP.
		/// </summary>
		public abstract string RfcOid { get; }

		/// <summary>
		/// Gets the name of the syntax used within Active Directory
		/// </summary>
		public virtual string? ActiveDirectoryName => null;
		/// <summary>
		/// Gets the OID of the syntax used within Active Directory.
		/// </summary>
		public virtual string? ActiveDirectoryOid => null;
		/// <summary>
		/// Gets the <c>omSyntax</c> value used within Active Directory.
		/// </summary>
		public virtual int OmSyntax => 0;
		/// <summary>
		/// Gets the <c>omSyntax</c> value used within Active Directory.
		/// </summary>
		public virtual string? OmObjectClass => null;
		/// <summary>
		/// Gets the <see cref="Type"/> of values handled by this syntax.
		/// </summary>
		/// <remarks>
		/// <see cref="Decode(byte[])"/> returns values of this type,
		/// and <see cref="Encode(object)"/> only accepts values of this type.
		/// </remarks>
		public abstract Type RuntimeType { get; }

		/// <summary>
		/// Decodes an LDAP value using this syntax.
		/// </summary>
		/// <param name="bytes">Bytes</param>
		/// <returns>Decode value</returns>
		public abstract object Decode(byte[] bytes);
		/// <summary>
		/// Decodes a value from [MS-DRSR] using this syntax.
		/// </summary>
		/// <param name="bytes">Bytes</param>
		/// <returns>Decode value</returns>
		public virtual object DecodeDsrep(byte[] bytes) => this.Decode(bytes);
		/// <summary>
		/// Encodes a value to [MS-DRSR] using this syntax.
		/// </summary>
		/// <param name="obj">Value</param>
		/// <returns>Encoded value</returns>
		public virtual byte[] EncodeDsrep(object obj) => this.Encode(obj);
		/// <summary>
		/// Encodes a value using this syntax.
		/// </summary>
		/// <param name="value">Value to encode</param>
		/// <returns>Encoded bytes representing <paramref name="value"/>.</returns>
		public abstract byte[] Encode(object value);

		public abstract bool IsCorrectType(object value);

		public abstract object Parse(string text);

		public virtual byte[] ParseAndEncode(string text)
		{
			var value = this.Parse(text);
			var encoded = this.Encode(value);
			return encoded;
		}

		/// <summary>
		/// Encodes an attribute value, either in its native type or textual representation.
		/// </summary>
		/// <param name="valueOrText">Value or text</param>
		/// <returns>A <see cref="byte"/> array of the encoded value</returns>
		/// <exception cref="ArgumentException"><paramref name="valueOrText"/> is not a valid value or textual representation.</exception>
		public virtual byte[] ParseOrEncode(object valueOrText)
		{
			if (IsCorrectType(valueOrText))
			{
				return this.Encode(valueOrText);
			}
			else if (valueOrText is string str)
			{
				return this.ParseAndEncode(str);
			}
			else
			{
				throw new ArgumentException($"The value '{valueOrText}' is not the correct type and cannot be parsed as text with syntax {this.GetType().Name}.");
			}
		}

	}
	/// <summary>
	/// Implements <see cref="LdapSyntax"/> to handle typed values.
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public abstract class LdapSyntax<T> : LdapSyntax
	{
		protected LdapSyntax()
			: base()
		{

		}

		/// <inheritdoc/>
		public sealed override Type RuntimeType => typeof(T);

		/// <inheritdoc/>
		public sealed override object? Decode(byte[] bytes)
		{
			ArgumentNullException.ThrowIfNull(bytes);
			return this.DecodeImpl(bytes);
		}
		/// <summary>
		/// Decodes a value using this syntax.
		/// </summary>
		/// <param name="bytes">Bytes</param>
		/// <returns></returns>
		protected abstract T DecodeImpl(byte[] bytes);

		/// <inheritdoc/>
		public sealed override byte[] Encode(object value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if (value is T typed)
				return this.EncodeImpl(typed);
			else
				throw new ArgumentException($"The value '{value}' ({value.GetType().FullName}) is not the expected type {typeof(T).FullName}.");
		}

		/// <summary>
		/// Encodes a value using this syntax.
		/// </summary>
		/// <param name="value">Value to encode</param>
		/// <returns>Encoded bytes representing <paramref name="value"/>.</returns>
		protected abstract byte[] EncodeImpl(T value);

		/// <inheritdoc/>
		public override bool IsCorrectType(object value) => value is T;
	}

	// [RFC 4517] § 3.3.1
	// [RFC 4512] § 4.1.2
	// [MS-ADTS] § 3.1.1.3.1.1.1 subSchema
	// TODO: Test the deviations of this syntax in [MS-ADTS]
	/// <summary>
	/// Describes an <see cref="AttributeTypeDescription"/>.
	/// </summary>
	public sealed class AttributeTypeDescriptionSyntax : LdapSyntax<AttributeTypeDescription>
	{
		internal AttributeTypeDescriptionSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Attribute Type Description";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.3";

		/// <inheritdoc/>
		protected sealed override AttributeTypeDescription DecodeImpl(byte[] bytes)
		{
			var str = Encoding.UTF8.GetString(bytes);
			return AttributeTypeDescription.Parse(str);
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(AttributeTypeDescription value)
		{
			return ParseAndEncode(value.ToString());
		}

		public override object Parse(string text)
		{
			return AttributeTypeDescription.Parse(text);
		}
		public override byte[] ParseAndEncode(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}
	}

	// [RFC 4517] § 3.3.2
	/// <summary>
	/// Encodes a bit string.
	/// </summary>
	public sealed class BitStringSyntax : LdapSyntax<BitArray>
	{
		internal BitStringSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Bit String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.6";

		/// <inheritdoc/>
		protected sealed override BitArray DecodeImpl(byte[] bytes)
		{
			if (bytes.Length < 3)
				throw new ArgumentException("Invalid bitstring", nameof(bytes));
			if (bytes[0] is not (byte)'\'' && bytes[^2] is not (byte)'\'' && bytes[^1] is not (byte)'B')
				throw new ArgumentException("Invalid bitstring", nameof(bytes));

			BitArray bits = new BitArray(bytes.Length - 3);
			for (int i = 1; i < bytes.Length - 2; i++)
			{
				byte b = bytes[i];
				bool bit = b switch { (byte)'0' => false, (byte)'1' => true, _ => throw new ArgumentException($"Value at index {i} is not valid in a bit string.", nameof(bytes)) };
				bits[i - 1] = bit;
			}

			return bits;
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(BitArray value)
		{
			byte[] encoded = new byte[value.Length + 3];
			encoded[0] = (byte)'\'';
			for (int i = 0; i < value.Length; i++)
			{
				var bit = value[i];
				encoded[i + 1] = (bit ? (byte)'1' : (byte)'0');
			}
			encoded[^2] = (byte)'\'';
			encoded[^1] = (byte)'B';
			return encoded;
		}

		public override object Parse(string text)
		{
			return DecodeImpl(Encoding.UTF8.GetBytes(text));
		}
	}

	// [RFC 4517] § 3.3.4
	/// <summary>
	/// Encodes a 2-character country string.
	/// </summary>
	public sealed class CountryStringSyntax : LdapSyntax<string>
	{
		internal CountryStringSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Country String";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.11";

		/// <inheritdoc/>
		protected sealed override string DecodeImpl(byte[] bytes)
		{
			if (bytes.Length != 2)
				throw new ArgumentException("Invalid country string", nameof(bytes));

			var str = $"{(char)bytes[0]}{(char)bytes[1]}";
			return str;
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(string value)
		{
			if (value.Length != 2) throw new ArgumentException($"Not a valid country string: {value}");
			return new byte[] { (byte)value[0], (byte)value[1] };
		}

		public override object Parse(string text) => text;
	}

	// [RFC 4517] § 3.3.5
	public enum DeliveryMethod
	{
		Any,
		Mhs = 1,
		Physical = 2,
		Telex = 3,
		Teletex = 4,
		G3Fax = 5,
		G4Fax = 6,
		Ia5Terminal = 7,
		Videotex = 8,
		Telephone = 9
	}

	// [RFC 4517] § 3.3.5
	/// <summary>
	/// Encodes a list of delivery methods.
	/// </summary>
	public sealed class DeliveryMethodSyntax : LdapSyntax<DeliveryMethod[]>
	{
		internal DeliveryMethodSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "Delivery Method";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.14";

		private static readonly string[] methodNames = [
			"any",
			"mhs",
			"physical",
			"telex",
			"teletex",
			"g3fax",
			"g4fax",
			"ia5",
			"videotex",
			"telephone"
			];
		public static string GetMethodName(DeliveryMethod method)
		{
			if ((int)method >= methodNames.Length)
				throw new ArgumentException($"Unknown delivery method: {method}");

			return methodNames[(int)method];
		}

		/// <inheritdoc/>
		protected sealed override DeliveryMethod[] DecodeImpl(byte[] bytes)
		{
			ArgumentNullException.ThrowIfNull(bytes);

			string str = Encoding.UTF8.GetString(bytes);
			return Parse(str);
		}

		public override DeliveryMethod[] Parse(string text)
		{
			var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			List<DeliveryMethod> methods = new List<DeliveryMethod>((tokens.Length + 1) / 2);
			methods.Add(Enum.Parse<DeliveryMethod>(tokens[0], true));
			for (int i = 2; i < tokens.Length; i += 2)
			{
				var token1 = tokens[i - 1];
				var token2 = tokens[i - 1];
				if (token1 != "$")
					throw new ArgumentException($"Token {i} was '{token1}' when '$' was expected.", nameof(text));

				methods.Add(Enum.Parse<DeliveryMethod>(token2, true));
			}

			return methods.ToArray();
		}

		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(DeliveryMethod[] value)
		{
			return Encoding.UTF8.GetBytes(string.Join("$", value.Select(r => GetMethodName(r))));
		}
	}

	// [RFC 4512] § 4.1.6
	// TODO: Parse the syntax
	public class ContentRule
	{
		public ContentRule(string description)
		{
			ArgumentException.ThrowIfNullOrEmpty(description);
			Description = description;
		}

		public string Description { get; }

		/// <inheritdoc/>
		public sealed override string ToString() => this.Description;
	}

	// [RFC 4517] § 3.3.7
	// [RFC 4512] § 4.1.6
	/// <summary>
	/// Encodes a DIT content rule.
	/// </summary>
	public sealed class DitContentRuleSyntax : LdapSyntax<ContentRule>
	{
		internal DitContentRuleSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "DIT Content Rule Description";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.16";

		/// <inheritdoc/>
		protected sealed override ContentRule DecodeImpl(byte[] bytes)
		{
			return new ContentRule(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(ContentRule value)
		{
			return ParseAndEncode(value.ToString());
		}

		public override object Parse(string text)
		{
			return new ContentRule(text);
		}

		public override byte[] ParseAndEncode(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}
	}

	// [RFC 4512] § 4.1.7.1
	// TODO: Parse the syntax
	public class StructureRule
	{
		public StructureRule(string description)
		{
			ArgumentException.ThrowIfNullOrEmpty(description);
			Description = description;
		}

		public string Description { get; }

		public sealed override string ToString() => this.Description;
	}

	// [RFC 4517] § 3.3.8
	// [RFC 4512] § 4.1.7.1
	public sealed class DitStructureRuleSyntax : LdapSyntax<StructureRule>
	{
		internal DitStructureRuleSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "DIT Structure Rule Description";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.17";

		/// <inheritdoc/>
		protected sealed override StructureRule DecodeImpl(byte[] bytes)
		{
			return new StructureRule(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(StructureRule value)
		{
			return Encoding.UTF8.GetBytes(value.Description);
		}

		public override object Parse(string text)
		{
			return new StructureRule(text);
		}
	}

	// [RFC 4517] § 3.3.9
	public sealed class DistinguishedNameSyntax : LdapSyntax<LdapDistinguishedName>
	{
		internal DistinguishedNameSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string RfcName => "DN";
		/// <inheritdoc/>
		public sealed override string RfcOid => "1.3.6.1.4.1.1466.115.121.1.12";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryName => "Object(DS-DN)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.1";

		/// <inheritdoc/>
		protected sealed override LdapDistinguishedName DecodeImpl(byte[] bytes)
		{
			// TODO: [MS-ADTS] § 3.1.1.3.4.1.5 - LDAP_SERVER_EXTENDED_DN_OID
			return new LdapDistinguishedName(Encoding.UTF8.GetString(bytes));
		}
		/// <inheritdoc/>
		protected sealed override byte[] EncodeImpl(LdapDistinguishedName value)
		{
			return Encoding.UTF8.GetBytes(value.Text);
		}

		public override object Parse(string text)
		{
			return new LdapDistinguishedName(text);
		}
	}


	////////////////////////////////////////
	// GAP
	//////////////////////////////////////
	//
}
