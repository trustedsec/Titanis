using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public class Asn1BitStringType : Asn1PrimitiveTypeBase
	{
		public Asn1NamedNumber[] NamedNumbers { get; internal set; }
		public override Asn1TypeKind Kind => Asn1TypeKind.CustomBitstring;
		public override Asn1Tag Tag => Asn1PredefTag.BitString;

		internal Asn1BitStringType()
		{

		}
		public Asn1BitStringType(Asn1NamedNumber[] namedNumbers)
		{
			if (namedNumbers == null)
				throw new ArgumentNullException(nameof(namedNumbers));

			this.NamedNumbers = namedNumbers;
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitBitString(this);
		}
	}
}
