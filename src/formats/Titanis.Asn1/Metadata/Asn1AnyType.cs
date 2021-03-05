using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public class Asn1AnyType : Asn1Type
	{
		internal static readonly Asn1AnyType Instance = new Asn1AnyType();

		public Asn1Field DefinedBy { get; internal set; }
		public override Asn1TypeKind Kind => Asn1TypeKind.Any;
		public override bool HasStaticTag => false;

		public override bool IsConstructed => false;

		internal Asn1AnyType()
		{

		}

		public Asn1AnyType(Asn1Field definedBy)
		{
			if (definedBy is null)
				throw new ArgumentNullException(nameof(definedBy));
			this.DefinedBy = definedBy;
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitAny(this);
		}
	}
}
