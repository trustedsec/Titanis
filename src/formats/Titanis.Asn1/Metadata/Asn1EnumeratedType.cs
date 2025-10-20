using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents an enumerated type.
	/// </summary>
	public sealed class Asn1EnumeratedType : Asn1PrimitiveTypeBase
	{
		public Asn1EnumeratedType(Asn1Enumeration[] enumerations)
		{
			if (enumerations == null)
				throw new ArgumentNullException(nameof(enumerations));

			this.Enumerations = enumerations;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "ENUMERATED {...}";

		/// <summary>
		/// Gets the list of enumerations declared by this type.
		/// </summary>
		public Asn1Enumeration[] Enumerations { get; }

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.CustomEnumeration;
		/// <inheritdoc/>
		protected sealed override Asn1Tag PrimitiveTag => Asn1PredefTag.Enumerated;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
