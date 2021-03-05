namespace Titanis.Asn1.Metadata
{
	public interface ITypeVisitor
	{
		object VisitAny(Asn1AnyType type);
		object VisitBitString(Asn1BitStringType type);
		object VisitChoice(Asn1ChoiceType type);
		object VisitConstrained(Asn1ConstrainedType type);
		object VisitSet(Asn1SetType type);
		object VisitSequence(Asn1SequenceType type);
		object VisitSetOf(Asn1SetOfType type);
		object VisitEnumerated(Asn1EnumeratedType type);
		object VisitSequenceOf(Asn1SequenceOfType type);
		object VisitUnresolved(Asn1UnresolvedType type);
		object VisitInteger(Asn1IntegerType type);
		object VisitTagged(Asn1TaggedType type);
		object VisitPrimitive(Asn1PrimitiveType type);
	}
}