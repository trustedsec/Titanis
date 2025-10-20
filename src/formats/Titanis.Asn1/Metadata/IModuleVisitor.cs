namespace Titanis.Asn1.Metadata
{
	public interface IModuleVisitor
	{
		void Visit(Asn1TypeDef typeDef);
		void Visit(Asn1ValueDef valueDef);
	}
}
