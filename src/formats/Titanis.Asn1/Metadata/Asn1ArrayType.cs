using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a type composed of zero or more elements of the same type.
	/// </summary>
	public abstract class Asn1ArrayType : Asn1ConstructedType
	{
		protected Asn1ArrayType(Asn1Type elementType)
		{
			if (elementType is null)
				throw new ArgumentNullException(nameof(elementType));

			this.ElementType = elementType;
		}

		/// <summary>
		/// Gets the type of element contained in the array.
		/// </summary>
		public Asn1Type ElementType { get; }

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.ElementType.OnAttached(this.DeclaringModule, this.EnclosingType, "element");
		}
	}
}
