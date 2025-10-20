namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SetType : Asn1ComplexType
	{
		public Asn1SetType(Asn1Field[] members, bool isExtensible) : base(members)
		{
			this.IsExtensible = isExtensible;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "SET {...}";

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Set;
		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag => new Asn1Tag(Asn1PredefTag.Set, Asn1TagFlags.Constructed);
		/// <inheritdoc/>
		public sealed override bool IsExtensible { get; }

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}

}
