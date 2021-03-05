using System.Linq;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1ChoiceType : Asn1ComplexType
	{
		public override bool HasStaticTag => false;

		public override Asn1TypeKind Kind => Asn1TypeKind.Choice;
		//public bool HasDynamicMember => this.Members.Any(m => !m.FieldType.HasStaticTag);

		public Asn1ChoiceType(Asn1Field[] members) : base(members)
		{
		}
		public Asn1ChoiceType() : base()
		{
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitChoice(this);
		}
	}
}
