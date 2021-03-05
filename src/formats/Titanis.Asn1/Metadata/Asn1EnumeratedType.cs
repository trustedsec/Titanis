using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public class Asn1EnumeratedType : Asn1PrimitiveTypeBase
	{
		public Asn1Enumeration[] Enumerations { get; internal set; }

		public override Asn1TypeKind Kind => Asn1TypeKind.CustomEnumeration;
		public override Asn1Tag Tag => Asn1PredefTag.Enumerated;

		internal Asn1EnumeratedType()
		{

		}

		public Asn1EnumeratedType(Asn1Enumeration[] enumerations)
		{
			if (enumerations == null)
				throw new ArgumentNullException(nameof(enumerations));

			this.Enumerations = enumerations;
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitEnumerated(this);
		}
	}
}
