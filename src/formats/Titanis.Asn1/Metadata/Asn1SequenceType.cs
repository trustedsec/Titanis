using System;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SequenceType : Asn1ComplexType
	{
		public Asn1SequenceType(Asn1Field[] members, bool isExtensible) : base(members)
		{
			this.IsExtensible = isExtensible;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "SEQUENCE";
		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Sequence;
		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);

		/// <inheritdoc/>
		public sealed override bool IsExtensible { get; }

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}

}
