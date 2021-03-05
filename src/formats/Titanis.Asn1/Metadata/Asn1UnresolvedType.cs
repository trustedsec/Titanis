using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1UnresolvedType : Asn1Type
	{
		public Asn1UnresolvedType(string name)
		{
			this.Name = name;
		}

		public override bool IsConstructed => true;

		public override bool HasStaticTag => false;

		public override Asn1TypeKind Kind => Asn1TypeKind.Unknown;

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitUnresolved(this);
		}
	}
}
