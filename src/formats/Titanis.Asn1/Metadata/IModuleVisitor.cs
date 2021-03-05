namespace Titanis.Asn1.Metadata
{
	public interface IModuleVisitor
	{
		void VisitType(Asn1TypeDef typeDef);
		void VisitValue(Asn1ValueDef asn1ValueDef);
	}
}
