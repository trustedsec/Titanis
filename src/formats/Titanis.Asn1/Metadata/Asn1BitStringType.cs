using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a BIT STRING.
	/// </summary>
	public sealed class Asn1BitStringType : Asn1PrimitiveTypeBase
	{
		public Asn1BitStringType(Asn1NamedBit[] namedBits)
		{
			if (namedBits == null)
				throw new ArgumentNullException(nameof(namedBits));

			this.NamedBits = namedBits;
			this.MaxPosition = namedBits.Max(r => r.Position);
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "BIT STRING {...}";

		public int MaxPosition { get; set; }

		/// <summary>
		/// Gets the named bit positions within this bitstring.
		/// </summary>
		public Asn1NamedBit[] NamedBits { get; }
		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.CustomBitstring;
		/// <inheritdoc/>
		protected sealed override Asn1Tag PrimitiveTag => Asn1PredefTag.BitString;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
