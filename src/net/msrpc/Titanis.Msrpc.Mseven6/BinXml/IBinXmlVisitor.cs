using System.Diagnostics;
using System.Xml;

namespace Titanis.Msrpc.Mseven6.BinXml
{
	interface IBinXmlVisitor
	{
		void Visit(BinXmlDocument node);
		void Visit(Fragment node);
		void Visit(ProcessingInstruction node);
		void Visit(FragmentHeader node);
		void Visit(Element node);
		void Visit(Attr node);
		void Visit(TextValueNode node);
		void Visit(CharRefNode node);
		void Visit(EntityRefNode node);
		void Visit(TemplateInstance node);
		void Visit(Substitution node);

		void Visit(NullValueNode node);
		void Visit(IntegerValue node);
		void Visit(UnsignedIntegerValue node);
		void Visit(FloatValue node);
		void Visit(BoolValue node);
		void Visit(GuidValue node);
		void Visit(BinaryValue node);
		void Visit(TimeValue node);
		void Visit(ArrayValue node);
		void Visit(HexIntegerValue node);
		void Visit(SidValue node);
		void Visit(SizeValue sizeValue);
	}

	abstract class BinXmlVisitor : IBinXmlVisitor
	{
		protected virtual void DefaultVisit(Node node) { }
		public virtual void Visit(BinXmlDocument node) => this.DefaultVisit(node);
		public virtual void Visit(Fragment node) => this.DefaultVisit(node);
		public virtual void Visit(ProcessingInstruction node) => this.DefaultVisit(node);
		public virtual void Visit(FragmentHeader node) => this.DefaultVisit(node);
		public virtual void Visit(Element node) => this.DefaultVisit(node);
		public virtual void Visit(Attr node) => this.DefaultVisit(node);
		public virtual void Visit(NullValueNode node) => this.DefaultVisit(node);
		public virtual void Visit(TextValueNode node) => this.DefaultVisit(node);
		public virtual void Visit(IntegerValue node) => this.DefaultVisit(node);
		public virtual void Visit(UnsignedIntegerValue node) => this.DefaultVisit(node);
		public virtual void Visit(FloatValue node) => this.DefaultVisit(node);
		public virtual void Visit(BoolValue node) => this.DefaultVisit(node);
		public virtual void Visit(GuidValue node) => this.DefaultVisit(node);
		public virtual void Visit(BinaryValue node) => this.DefaultVisit(node);
		public virtual void Visit(TimeValue node) => this.DefaultVisit(node);
		public virtual void Visit(CharRefNode node) => this.DefaultVisit(node);
		public virtual void Visit(EntityRefNode node) => this.DefaultVisit(node);
		public virtual void Visit(TemplateInstance node) => this.DefaultVisit(node);
		public virtual void Visit(Substitution node) => this.DefaultVisit(node);
		public virtual void Visit(ArrayValue node) => this.DefaultVisit(node);
		public virtual void Visit(HexIntegerValue node) => this.DefaultVisit(node);
		public virtual void Visit(SidValue node) => this.DefaultVisit(node);
		public virtual void Visit(SizeValue node) => this.DefaultVisit(node);
	}

	class BinXmlWriterVisitor : BinXmlTreeVisitor
	{
		public BinXmlWriterVisitor(XmlWriter writer)
		{
			this._writer = writer;
		}

		private readonly XmlWriter _writer;

		public override void Visit(ProcessingInstruction node) => this._writer.WriteProcessingInstruction(node.Name, node.Data);

		public override void Visit(Element node)
		{
			if ((node.Content.Count == 1) && node.Content[0] is ArrayValue ary)
			{
				foreach (var item in ary.Nodes)
				{
					_writer.WriteStartElement(node.Name);

					foreach (var attr in node.AttrList)
					{
						attr.Accept(this);
					}
					foreach (var content in node.Content)
					{
						content.Accept(this);
					}
					_writer.WriteEndElement();
				}
			}
			else
			{
				_writer.WriteStartElement(node.Name);
				foreach (var attr in node.AttrList)
				{
					attr.Accept(this);
				}
				foreach (var content in node.Content)
				{
					content.Accept(this);
				}

				_writer.WriteEndElement();
			}
		}

		public override void Visit(Attr node)
		{
			if (node.Name is "xmlns")
			{
				// XmlWrite doesn't like this.  The NS declaration should instead percolate up to the containing element.
			}
			else if (node.Data.Count == 0 || node.Data[0] is NullValueNode || (node.Data[0] is Substitution subst && !this.HasValue(subst)))
			{
				// Don't emit
			}
			else
					{
						_writer.WriteStartAttribute(node.Name);
						foreach (var data in node.Data)
						{
							data.Accept(this);
						}
						_writer.WriteEndAttribute();
					}
		}

		public override void Visit(NullValueNode node) => this._writer.WriteValue(string.Empty);
		public override void Visit(TextValueNode node) => this._writer.WriteValue(node.Value);
		public override void Visit(IntegerValue node) => this._writer.WriteValue(node.Value.ToString());
		public override void Visit(UnsignedIntegerValue node) => this._writer.WriteValue(node.Value.ToString());
		public override void Visit(FloatValue node) => this._writer.WriteValue(node.Value.ToString());
		public override void Visit(SizeValue node) => this._writer.WriteValue(node.Value.ToString());
		public override void Visit(BoolValue node) => this._writer.WriteValue(node.Value ? "true" : "false");
		public override void Visit(GuidValue node) => this._writer.WriteValue(node.Value.ToString("b"));
		public override void Visit(BinaryValue node) => this._writer.WriteValue(node.Value.ToHexString());
		public override void Visit(TimeValue node) => this._writer.WriteValue(node.Value.ToString("o"));
		public override void Visit(HexIntegerValue node) => this._writer.WriteValue($"0x{node.Value:X}");
		public override void Visit(SidValue node) => this._writer.WriteValue(node.Sid.ToString());
		public override void Visit(CharRefNode node) => this._writer.WriteCharEntity((char)node.Code);
		public override void Visit(EntityRefNode node) => this._writer.WriteEntityRef(node.Name);
	}
}
