namespace Titanis.Asn1.Metadata
{
	public interface ITypeVisitor<T>
	{
		T Visit(Asn1AnyType type);
		T Visit(Asn1BitStringType type);
		T Visit(Asn1ChoiceType type);
		T Visit(Asn1ConstrainedType type);
		T Visit(Asn1SetType type);
		T Visit(Asn1SequenceType type);
		T Visit(Asn1SetOfType type);
		T Visit(Asn1EnumeratedType type);
		T Visit(Asn1SequenceOfType type);
		T Visit(Asn1UnresolvedType type);
		T Visit(Asn1IntegerType type);
		T Visit(Asn1TaggedType type);
		T Visit(Asn1PrimitiveType type);
	}
}