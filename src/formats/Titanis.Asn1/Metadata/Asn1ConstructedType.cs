namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a constructed type.
	/// </summary>
	public abstract class Asn1ConstructedType : Asn1Type
	{
		/// <inheritdoc/>
		public sealed override bool IsConstructed => true;
	}
}
