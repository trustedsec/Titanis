using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1IntegerType : Asn1PrimitiveTypeBase
	{
		public Asn1NamedNumber[] NamedNumbers { get; internal set; }

		public override Asn1TypeKind Kind => Asn1TypeKind.CustomInteger;
		public override Asn1Tag Tag => Asn1PredefTag.Integer;

		internal Asn1IntegerType()
		{

		}
		public Asn1IntegerType(Asn1NamedNumber[] namedNumbers)
		{
			if (namedNumbers == null)
				throw new ArgumentNullException(nameof(namedNumbers));

			this.NamedNumbers = namedNumbers;
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitInteger(this);
		}
	}
}
