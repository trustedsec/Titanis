using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{

	public class Asn1ConstrainedType : Asn1DerivedType
	{
		//internal Asn1ConstrainedType()
		//{

		//}

		internal Asn1ConstrainedType(
			Asn1Type baseType
			)
			: base(baseType)
		{
		}

		public Asn1ConstrainedType(
			Asn1Type baseType,
			Asn1Constraint[] constraints
			)
			: base(baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));
			if (constraints.IsNullOrEmpty())
				throw new ArgumentNullException(nameof(constraints));

			this.Constraints = constraints;
		}

		public Asn1Constraint[] Constraints { get; internal set; }
		public override bool HasStaticTag => this.BaseType.HasStaticTag;
		public override Asn1Tag Tag => this.BaseType.Tag;

		public override Asn1TypeKind Kind => Asn1TypeKind.Constrained;

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitConstrained(this);
		}

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.BaseType.OnAttaching(this.Module, null);
			this.BaseType.OnAttached(this.EnclosingType, null);
		}
	}
}
