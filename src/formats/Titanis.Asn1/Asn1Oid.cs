using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	public struct Asn1Oid : IEquatable<Asn1Oid>, IReadOnlyList<Asn1OidPart>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv,
		IAsn1DerDecodableTlv<Asn1Oid>, IAsn1DerDecodableValue<Asn1Oid>
	{
		public Asn1Oid(Asn1OidPart part0)
		{
			this._subparts = new Asn1OidPart[] { part0 };
		}
		public Asn1Oid(string str)
			: this(new Oid(str))
		{
		}

		public Asn1Oid(Oid oid)
		{
			if (oid is null)
				throw new ArgumentNullException(nameof(oid));

			string[] tokens = oid.Value.Split('.');
			this._subparts = Array.ConvertAll(tokens, r => new Asn1OidPart(ulong.Parse(r)));
		}

		public Asn1Oid(params Asn1OidPart[] parts)
		{
			ArgumentNullException.ThrowIfNull(parts);
			this._subparts = parts;
		}


		internal Asn1OidPart[] _subparts;
		public bool IsEmpty => this.Count == 0;

		public int Count => this._subparts?.Length ?? 0;

		public Asn1OidPart this[int index] => this._subparts[index];
		public Asn1Tag Tag => Asn1PredefTag.ObjectIdentifier;

		public override bool Equals(object obj)
		{
			return obj is Asn1Oid oid && Equals(oid);
		}

		public bool Equals(Asn1Oid other)
		{
			// TODO: Compare the array values
			return EqualityComparer<Asn1OidPart[]>.Default.Equals(_subparts, other._subparts);
		}

		public override int GetHashCode()
		{
			int hashCode = -1185072511;
			for (int i = 0; i < this._subparts.Length; i++)
			{
				hashCode = hashCode * -1521134295 + EqualityComparer<Asn1OidPart>.Default.GetHashCode(this._subparts[i]);
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

		public Oid ToOid()
		{
			return new Oid(this.ToString());
		}

		public override string ToString()
		{
			return (this._subparts == null) ? string.Empty : string.Join(".", this._subparts.Select(r => r.Value));
		}

		public IEnumerator<Asn1OidPart> GetEnumerator() => ((IEnumerable<Asn1OidPart>)this._subparts).GetEnumerator();
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
}