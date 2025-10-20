using System;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents an ASN.1 type derived from another.
	/// </summary>
	public abstract class Asn1DerivedType : Asn1Type
	{
		protected Asn1DerivedType(Asn1Type baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));

			this.BaseType = baseType;
		}

		/// <summary>
		/// Gets the base type.
		/// </summary>
		public Asn1Type BaseType { get; }
		/// <inheritdoc/>
		public sealed override Asn1Type CanonicalType => this.BaseType.CanonicalType;

		/// <inheritdoc/>
		public override bool HasStaticTag => this.BaseType.HasStaticTag;
		/// <inheritdoc/>
		public override Asn1Tag StaticTag => this.BaseType.StaticTag;
		/// <inheritdoc/>
		public override bool IsConstructed => this.BaseType.IsConstructed;
	}
}
