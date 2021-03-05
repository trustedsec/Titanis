using System;

namespace Titanis.Asn1.Metadata
{
	public abstract class Asn1DerivedType : Asn1Type
	{
		//protected Asn1DerivedType()
		//{

		//}

		protected Asn1DerivedType(Asn1Type baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));

			this.BaseType = baseType;
		}

		public Asn1Type BaseType { get; internal set; }
		public override Asn1Type CanonicalType => this.BaseType.CanonicalType;

		public override bool IsConstructed => this.BaseType.IsConstructed;
	}
}
