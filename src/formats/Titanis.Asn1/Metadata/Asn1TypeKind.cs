namespace Titanis.Asn1.Metadata
{
	public enum Asn1TypeKind
	{
		Unknown = 0,

		Any,
		Primitive,
		Sequence,
		Set,
		Choice,
		CustomBitstring,
		Constrained,
		CustomEnumeration,
		CustomInteger,
		SequenceOf,
		SetOf,
		Tagged,
	}
}