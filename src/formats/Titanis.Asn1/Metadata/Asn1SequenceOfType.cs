using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SequenceOfType : Asn1ArrayType
	{
		public Asn1SequenceOfType(Asn1Type elementType)
			: base(elementType)
		{

		}

		// TODO: Insert constraint
		/// <inheritdoc/>
		public sealed override string DefinitionString => $"SEQUENCE OF {this.ElementType.DefinitionString}";

		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.SequenceOf;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
