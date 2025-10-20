using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SetOfType : Asn1ArrayType
	{
		public Asn1SetOfType(Asn1Type elementType)
			: base(elementType)
		{

		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "SEQUENCE {...}";

		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Set, Asn1TagFlags.Constructed);

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.SetOf;
	}
}
