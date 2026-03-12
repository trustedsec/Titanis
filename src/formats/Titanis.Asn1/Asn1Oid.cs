using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	/// <summary>
	/// Represents an object identifier.
	/// </summary>
	/// <remarks>
	/// An object identifier (OID) is a string of integer values (arcs) separated by dots that identifies something.  Each arc must not exceed the capacity of a 32-bit unsigned integer.  An OID must not exceed 128 arcs.
	/// </remarks>
	public struct Asn1Oid : IEquatable<Asn1Oid>, IReadOnlyList<uint>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv,
		IAsn1DerDecodableTlv<Asn1Oid>, IAsn1DerDecodableValue<Asn1Oid>
	{
		public Asn1Oid(string? numericArcs)
		{
			if (string.IsNullOrEmpty(numericArcs))
			{

			}
			else
			{
				List<uint> arcs = new List<uint>();
				int startIndex = 0;
				int isep;
				while ((isep = numericArcs.IndexOf('.', startIndex)) >= 0)
				{
					if (isep == startIndex)
						throw CreateFormatException();

					if (!uint.TryParse(numericArcs[startIndex..isep], out uint arc))
						throw CreateFormatException();

					arcs.Add(arc);
					startIndex = isep + 1;
				}

				// Final arc
				{
					isep = numericArcs.Length;
					if (!uint.TryParse(numericArcs[startIndex..isep], out uint arc))
						throw CreateFormatException();
					arcs.Add(arc);
				}

				this._arcs = arcs.ToArray();
			}
		}

		private Exception CreateFormatException(Exception? inner = null)
		{
			return new FormatException("The string must be a series of at least 2 non-negative integers separated by dots.", inner);
		}

		public Asn1Oid(params uint[] arcs)
		{
			ArgumentNullException.ThrowIfNull(arcs);
			this._arcs = arcs;
		}

		private string? _text;
		public string Text => (this._text ??= this.BuildText());

		private string BuildText() => string.Join(".", this._arcs ?? []);

		internal readonly uint[]? _arcs;
		[MemberNotNullWhen(false, nameof(_arcs))]
		public bool IsEmpty => this.Count == 0;
		public static Asn1Oid Empty => new Asn1Oid();

		public int Count => this._arcs?.Length ?? 0;

		public uint this[int index] => this._arcs[index];
		public Asn1Tag Tag => Asn1PredefTag.ObjectIdentifier;

		public override bool Equals(object? obj)
		{
			return
				(obj is Asn1Oid oid && Equals(oid))
				|| (obj is string str && Equals(str));
		}
		public bool Equals(string? oid)
		{
			oid ??= string.Empty;
			return (oid == this.Text);
		}
		public bool Equals(Oid? oid)
		{
			string value = oid?.Value ?? string.Empty;
			return (value == this.Text);
		}

		public bool Equals(Asn1Oid other)
		{
			if (this.IsEmpty)
				return other.IsEmpty;
			else if (other.IsEmpty)
				return false;

			return this._arcs.SequenceEqual(other._arcs);
		}

		public override int GetHashCode()
		{
			if (this.IsEmpty)
				return 0;

			int hashCode = -1185072511;
			for (int i = 0; i < this._arcs.Length; i++)
			{
				hashCode = HashCode.Combine(hashCode, this._arcs[i].GetHashCode());
			}
			return hashCode;
		}



		public static bool operator ==(Asn1Oid left, Asn1Oid right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Oid left, Asn1Oid right)
		{
			return !(left == right);
		}



		public static bool operator ==(Asn1Oid left, string right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Oid left, string right)
		{
			return !(left == right);
		}



		public static bool operator ==(string left, Asn1Oid right)
		{
			return right.Equals(left);
		}

		public static bool operator !=(string left, Asn1Oid right)
		{
			return !(left == right);
		}



		public static bool operator ==(Asn1Oid left, Oid? right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1Oid left, Oid? right)
		{
			return !(left == right);
		}



		public static bool operator ==(Oid? left, Asn1Oid right)
		{
			return right.Equals(left);
		}

		public static bool operator !=(Oid? left, Asn1Oid right)
		{
			return !(left == right);
		}



		[Obsolete("Use Text to get the textual representation.", false)]
		public Oid ToOid()
		{
			return new Oid(this.ToString());
		}

		public override string ToString() => this.Text;

		public IEnumerator<uint> GetEnumerator() => ((IEnumerable<uint>)(this._arcs ?? [])).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOidValue(this);
		}

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeOidTlv(this, this.Tag);
		}

		static Asn1Oid IAsn1DerDecodableTlv<Asn1Oid>.DecodeTlvFrom(Asn1DerDecoder decoder) => decoder.DecodeOidTlv();
		static bool IAsn1DerDecodableTlv<Asn1Oid>.TryDecodeTlvFrom(Asn1DerDecoder decoder, out Asn1Oid value) => decoder.TryDecodeTaggedValue<Asn1Oid>(Asn1PredefTag.RelativeOid, out value);
		static Asn1Oid IAsn1DerDecodableValue<Asn1Oid>.DecodeValueFrom(Asn1DerDecoder decoder) => decoder.DecodeOidValue();
	}

	// TODO: It would be great if the built-in Oid class supported value equality, but it doesn't.  Parts of the code (like AuthContext) represent OIDs as string to avoid including Titanis.Asn1 as a dependency.  Asn1Oid should be moved to a lightweight Titanis.Asn1.Core.  Until then, though, the following should cover most use cases.
	public static class Asn1OidExtensions
	{
		public static bool Equals(this string str, Asn1Oid oid) => oid.Equals(str);
		public static bool Equals(this object obj, Asn1Oid oid) => oid.Equals(obj);
	}
}