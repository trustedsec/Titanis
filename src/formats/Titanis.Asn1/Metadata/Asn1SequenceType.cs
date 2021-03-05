using System;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SequenceType : Asn1ComplexType
	{
		public override Asn1TypeKind Kind => Asn1TypeKind.Sequence;
		public override bool HasStaticTag => true;
		public override Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);

		public Asn1SequenceType()
		{

		}

		public Asn1SequenceType(Asn1Field[] members) : base(members)
		{
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitSequence(this);
		}
	}

}
