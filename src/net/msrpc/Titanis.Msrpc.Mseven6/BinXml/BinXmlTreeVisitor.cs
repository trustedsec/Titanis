namespace Titanis.Msrpc.Mseven6.BinXml
{
	abstract class BinXmlTreeVisitor : BinXmlVisitor
	{

		public override void Visit(BinXmlDocument node)
		{
			node.Prolog?.Accept(this);
			node.Fragment.Accept(this);
			node.Misc?.Accept(this);
		}

		public override void Visit(Fragment node) => node.Node.Accept(this);

		public override void Visit(FragmentHeader node)
		{
			// Do Nothing
		}

		protected virtual void VisitValueNode(Node data)
		{
			data.Accept(this);
		}

		public override void Visit(Element node)
		{
			foreach (var attr in node.AttrList)
			{
				attr.Accept(this);
			}
			foreach (var content in node.Content)
			{
				this.VisitValueNode(content);
			}
		}

		public override void Visit(Attr node)
		{
			foreach (var data in node.Data)
			{
				this.VisitValueNode(data);
			}
		}

		protected TemplateInstance? CurrentTemplateInstance { get; private set; }
		public override void Visit(TemplateInstance node)
		{
			var prev = this.CurrentTemplateInstance;
			try
			{
				this.CurrentTemplateInstance = node;
				node.Element.Accept(this);
			}
			finally
			{
				this.CurrentTemplateInstance = prev;
			}
		}

		protected bool HasValue(Substitution subst)
		{
			if (this.CurrentTemplateInstance != null)
			{
				var data = this.CurrentTemplateInstance.Data[subst.Id];
				return (data.Value is not NullValueNode);
			}
			return false;
		}

		public override void Visit(Substitution node)
		{
			this.CurrentTemplateInstance.Data[node.Id].Value.Accept(this);
		}
	}
}
