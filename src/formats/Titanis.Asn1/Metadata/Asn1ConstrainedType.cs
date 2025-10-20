using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a type with one or more constraints.
	/// </summary>
	public sealed class Asn1ConstrainedType : Asn1DerivedType
	{
		public Asn1ConstrainedType(
			Asn1Type baseType,
			Asn1Constraint constraint
			)
			: base(baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));
			if (constraint is null)
				throw new ArgumentNullException(nameof(constraint));

			this.Constraint = constraint;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => $"{this.BaseType} ({this.Constraint})";

		/// <summary>
		/// Gets the constraints applied to this type.
		/// </summary>
		public Asn1Constraint Constraint { get; }
		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Constrained;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.BaseType.OnAttached(this.DeclaringModule, this.EnclosingType, null);
		}
	}
}
