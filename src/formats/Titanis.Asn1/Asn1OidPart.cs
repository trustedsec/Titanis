using System;
using System.Collections.Generic;

namespace Titanis.Asn1
{
	public struct Asn1OidPart : IEquatable<Asn1OidPart>
	{
		public long? Value { get; }
		public string Name { get; }

		public Asn1OidPart(long? value)
		{
			this.Value = value;
			this.Name = null;
		}
		public Asn1OidPart(string text)
		{
			this.Value = null;
			this.Name = text;
		}
		public Asn1OidPart(long? value, string name)
		{
			this.Value = value;
			this.Name = name;
		}

		public override bool Equals(object obj)
		{
			return obj is Asn1OidPart part && Equals(part);
		}

		public bool Equals(Asn1OidPart other)
		{
			return Value == other.Value &&
				   Name == other.Name;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(Value, Name);
		}

		public static bool operator ==(Asn1OidPart left, Asn1OidPart right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1OidPart left, Asn1OidPart right)
		{
			return !(left == right);
		}

		public static implicit operator Asn1OidPart(long value)
		{
			return new Asn1OidPart(value);
		}
	}
}