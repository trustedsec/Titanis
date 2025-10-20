using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents the INTEGER type.
	/// </summary>
	public sealed class Asn1IntegerType : Asn1PrimitiveTypeBase
	{
		public Asn1IntegerType(Asn1NamedNumber[] namedNumbers)
		{
			if (namedNumbers == null)
				throw new ArgumentNullException(nameof(namedNumbers));

			this.NamedNumbers = namedNumbers;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "INTEGER {...}";

		/// <summary>
		/// Gets the named values defined by this type.
		/// </summary>
		public Asn1NamedNumber[] NamedNumbers { get; internal set; }

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.CustomInteger;
		/// <inheritdoc/>
		protected sealed override Asn1Tag PrimitiveTag => Asn1PredefTag.Integer;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
