using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public abstract class Asn1ConstructedType : Asn1Type
	{
		public sealed override bool IsConstructed => true;
	}

	public abstract class Asn1ArrayType : Asn1ConstructedType
	{
		public Asn1Type ElementType { get; internal set; }

		internal Asn1ArrayType()
		{

		}

		private protected Asn1ArrayType(Asn1Type elementType)
		{
			if (elementType is null)
				throw new ArgumentNullException(nameof(elementType));

			this.ElementType = elementType;
		}

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.OnAttaching(this.Module, null);
			this.ElementType.OnAttaching(this.Module, null);
			this.ElementType.OnAttached(this.EnclosingType, null);
		}
	}
}
