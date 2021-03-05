namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SetType : Asn1ComplexType
	{
		public override Asn1TypeKind Kind => Asn1TypeKind.Set;
		public override bool HasStaticTag => true;
		public override Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Set, Asn1TagFlags.Constructed);

		public Asn1SetType(Asn1Field[] members) : base(members)
		{
		}
		public Asn1SetType() : base()
		{
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitSet(this);
		}
	}

}
