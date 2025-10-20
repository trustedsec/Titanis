namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a primitive (non-constructed) type.
	/// </summary>
	public abstract class Asn1PrimitiveTypeBase : Asn1Type
	{
		/// <inheritdoc/>
		public sealed override bool IsConstructed => false;
		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag => this.PrimitiveTag;
		/// <summary>
		/// Gets the tag for this primitive type.
		/// </summary>
		/// <remarks>
		/// This member is declared abstract apart from <see cref="StaticTag"/> to
		/// force derived types to implement it.
		/// </remarks>
		protected abstract Asn1Tag PrimitiveTag { get; }
	}
}
