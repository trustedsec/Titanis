namespace Titanis.Msrpc.Mseven6.BinXml
{
	class TemplateInstance : Node
	{
		public TemplateInstance(Guid id, in FrugalList<FragmentHeader> headers, Element elem, ValueSpecEntry[] data)
		{
			this.Id = id;
			this.Headers = headers;
			this.Element = elem;
			this.Data = data;
		}

		public Guid Id { get; }
		public FrugalList<FragmentHeader> Headers { get; }
		public Element Element { get; }
		// TODO: Should this be a frugal list?
		public ValueSpecEntry[] Data { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class Substitution : AttrCharDataNode
	{
		internal Substitution(Token token, ushort id, BinXmlValueType valueType)
		{
			this.Token = token;
			this.Id = id;
			this.ValueType = valueType;
		}

		public Token Token { get; }
		public ushort Id { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}
}
