using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SetOfType : Asn1ArrayType
	{
		public override bool HasStaticTag => true;
		public override Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Set, Asn1TagFlags.Constructed);

		internal Asn1SetOfType()
		{

		}
		public Asn1SetOfType(Asn1Type elementType)
			: base(elementType)
		{

		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitSetOf(this);
		}

		public override Asn1TypeKind Kind => Asn1TypeKind.SetOf;
	}
}
