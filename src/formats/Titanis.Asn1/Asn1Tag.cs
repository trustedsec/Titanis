using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{

	/// <summary>
	/// Describes a tag in an ASN.1 BER-derived encoding.
	/// </summary>
	// [X.690] § 8.1.2
	public struct Asn1Tag : IEquatable<Asn1Tag>
	{
		private const int TagNumberMask = 0x1FFFFFFF;
		internal const int ConstructedFlag = 0x2000_0000;

		public Asn1Tag(uint rawValue)
		{
			this._value = rawValue;
		}
		public Asn1Tag(Asn1PredefTag tag)
		{
			this._value = (uint)tag;
		}
		public Asn1Tag(byte b0)
		{
			// This is a simple tag
			// Unpack the flag bits
			this._value =
				(b0 & 0xE0U) << 24
				| (b0 & 0x1FU)
				;
		}
		public Asn1Tag(int tagNumber, Asn1TagFlags flags)
		{
			if (tagNumber >= (1 << 24))
				throw new ArgumentOutOfRangeException(nameof(tagNumber));

			this._value = (uint)tagNumber | (uint)flags << 24;
		}
		public Asn1Tag(Asn1TagClass tagClass, int tagNumber)
			: this(tagNumber, (Asn1TagFlags)tagClass)
		{
		}
		public Asn1Tag(Asn1PredefTag tag, Asn1TagFlags flags)
		{
			this._value = (uint)tag | (uint)flags << 24;
		}

		public override string ToString() => this.TagClass switch { Asn1TagClass.Universal => "UNIVERSAL ", Asn1TagClass.Application => "APPLICATION ", Asn1TagClass.Private => "PRIVATE ", _ => string.Empty } + this.TagNumber.ToString();

		public static implicit operator Asn1Tag(Asn1PredefTag predef)
			=> new Asn1Tag(predef);

		public static Asn1Tag Empty => new Asn1Tag();

		internal readonly uint _value;
		public uint RawValue => this._value;
		public uint TagNumber => (this._value & TagNumberMask);
		public bool IsEmpty => this._value == 0;
		public Asn1TagClass TagClass => (Asn1TagClass)((this._value >> 24) & (uint)Asn1TagClass.Mask);
		public uint Flags => ((this._value >> 24) & (uint)Asn1TagFlags.Mask);
		public bool IsConstructed => ((this._value & ConstructedFlag) != 0);

		internal bool IsEot => (this._value == 0);

		public Asn1Tag AsConstructed()
			=> new Asn1Tag(this._value | ConstructedFlag);

		public override bool Equals(object? obj)
		{
			return obj is Asn1Tag tag && this.Equals(tag);
		}

		public bool EqualsExactly(Asn1Tag other)
		{
			return this._value == other._value;
		}

		public bool Equals(Asn1Tag other)
		{
			return (this._value | ConstructedFlag) == (other._value | ConstructedFlag);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this._value | ConstructedFlag);
		}

		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !(left == right);
		}
	}

	public enum Asn1StringType : uint
	{
		PrintableString = Asn1PredefTag.PrintableString,
		T61String = Asn1PredefTag.T61String,
		Ia5String = Asn1PredefTag.IA5String,
	}
}
