namespace Titanis.Asn1.Metadata
{
	public abstract class Asn1PrimitiveTypeBase : Asn1Type
	{
		public sealed override bool IsConstructed => false;
		public sealed override bool HasStaticTag => true;
	}
}
