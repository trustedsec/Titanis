using MS_EVEN6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mseven6.BinXml
{
	/// <summary>
	/// Base class for all BinXml nodes.
	/// </summary>
	abstract class Node
	{
		public abstract void Accept(IBinXmlVisitor visitor);
	}

	interface IValueNode
	{
		MS_EVEN6.EvtRpcVariant AsVariant();
	}

	/// <summary>
	/// Represents a BinXml document.
	/// </summary>
	class BinXmlDocument : Node
	{
		internal BinXmlDocument(
			ProcessingInstruction? prolog,
			Fragment fragment,
			ProcessingInstruction? misc)
		{
			ArgumentNullException.ThrowIfNull(fragment);
			Prolog = prolog;
			Fragment = fragment;
			Misc = misc;
		}

		public ProcessingInstruction? Prolog { get; }
		public Fragment Fragment { get; }
		public ProcessingInstruction? Misc { get; }

		internal static BinXmlDocument Read(byte[] eventData)
		{
			BinXmlReadContext ctx = new BinXmlReadContext(eventData);
			return ctx.ReadDocument();
		}

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class ProcessingInstruction : Node
	{
		internal ProcessingInstruction(string name, string data)
		{
			this.Name = name;
			this.Data = data;
		}

		public string Name { get; }
		public string Data { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class Fragment : Node
	{
		internal Fragment(in FrugalList<FragmentHeader> headers, Node node)
		{
			this.Headers = headers;
			this.Node = node;
		}

		public FrugalList<FragmentHeader> Headers { get; }
		public Node Node { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	[Flags]
	enum FragmentHeaderFlags
	{

	}
	class FragmentHeader : Node
	{
		internal FragmentHeader(byte majorVersion, byte minorVersion, FragmentHeaderFlags flags)
		{
			this.MajorVersion = majorVersion;
			this.MinorVersion = minorVersion;
			this.Flags = flags;
		}

		public byte MajorVersion { get; }
		public byte MinorVersion { get; }
		public FragmentHeaderFlags Flags { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class Element : Node
	{
		internal Element(
			string name,
			in FrugalList<Attr> attrList,
			in FrugalList<Node> content)
		{
			this.Name = name;
			this.AttrList = attrList;
			this.Content = content;
		}

		public string Name { get; }
		public FrugalList<Attr> AttrList { get; }
		public FrugalList<Node> Content { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class Attr : Node
	{
		internal Attr(string name, in FrugalList<Node> data)
		{
			this.Name = name;
			this.Data = data;
		}

		public string Name { get; }
		public FrugalList<Node> Data { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	/// <summary>
	/// Base class for value nodes.
	/// </summary>
	abstract class AttrCharDataNode : Node
	{

	}

	class NullValueNode : AttrCharDataNode, IValueNode
	{
		internal NullValueNode()
		{
		}

		public static readonly NullValueNode instance = new NullValueNode();

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);

		public EvtRpcVariant AsVariant() => new EvtRpcVariant() { type = EvtRpcVariantType.EvtRpcVarTypeNull };
	}

	class BoolValue : AttrCharDataNode, IValueNode
	{
		internal BoolValue(bool value)
		{
			this.Value = value;
		}

		public bool Value { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => new EvtRpcVariant(this.Value);
	}

	class IntegerValue : AttrCharDataNode, IValueNode
	{
		internal IntegerValue(long value, BinXmlValueType valueType)
		{
			this.Value = value;
			this.ValueType = valueType;
		}

		public long Value { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => this.ValueType switch
		{
			BinXmlValueType.Int64 => new EvtRpcVariant((ulong)this.Value),
			_ => new EvtRpcVariant((uint)this.Value),
		};
	}

	class UnsignedIntegerValue : AttrCharDataNode, IValueNode
	{
		internal UnsignedIntegerValue(ulong value, BinXmlValueType valueType)
		{
			this.Value = value;
			this.ValueType = valueType;
		}

		public ulong Value { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => this.ValueType switch
		{
			BinXmlValueType.UInt64 => new EvtRpcVariant(this.Value),
			_ => new EvtRpcVariant((uint)this.Value),
		};
	}

	class SizeValue : AttrCharDataNode, IValueNode
	{
		internal SizeValue(ulong value, int length)
		{
			this.Value = value;
			this.Length = length;
		}

		public ulong Value { get; }
		public int Length { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => this.Length switch
		{
			8 => new EvtRpcVariant(this.Value),
			_ => new EvtRpcVariant((uint)this.Value),
		};
	}

	class HexIntegerValue : AttrCharDataNode, IValueNode
	{
		internal HexIntegerValue(ulong value, BinXmlValueType valueType)
		{
			this.Value = value;
			this.ValueType = valueType;
		}

		public ulong Value { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => this.ValueType switch
		{
			BinXmlValueType.HexInt64 => new EvtRpcVariant(this.Value),
			_ => new EvtRpcVariant((uint)this.Value),
		};
	}

	class FloatValue : AttrCharDataNode
	{
		internal FloatValue(double value, BinXmlValueType valueType)
		{
			this.Value = value;
			this.ValueType = valueType;
		}

		public double Value { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class TextValueNode : AttrCharDataNode, IValueNode
	{
		internal TextValueNode(string text)
		{
			ArgumentNullException.ThrowIfNull(text);
			this.Value = text;
		}

		public string Value { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => new EvtRpcVariant(this.Value);
	}

	class SidValue : AttrCharDataNode, IValueNode
	{
		internal SidValue(SecurityIdentifier sid)
		{
			ArgumentNullException.ThrowIfNull(sid);
			this.Sid = sid;
		}

		public SecurityIdentifier Sid { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => new EvtRpcVariant(this.Sid.ToString());
	}

	class GuidValue : AttrCharDataNode, IValueNode
	{
		internal GuidValue(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => new EvtRpcVariant(this.Value);
	}

	class TimeValue : AttrCharDataNode, IValueNode
	{
		internal TimeValue(DateTime value, BinXmlValueType valueType)
		{
			Value = value;
			ValueType = valueType;
		}

		public DateTime Value { get; }
		public BinXmlValueType ValueType { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
		public EvtRpcVariant AsVariant() => new EvtRpcVariant((ulong)this.Value.ToFileTimeUtc());
	}

	class BinaryValue : AttrCharDataNode
	{
		internal BinaryValue(byte[] value)
		{
			ArgumentNullException.ThrowIfNull(value);
			this.Value = value;
		}

		public byte[] Value { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class ArrayValue : AttrCharDataNode
	{
		internal ArrayValue(in FrugalList<Node> nodes)
		{
			ArgumentNullException.ThrowIfNull(nodes);
			this.Nodes = nodes;
		}

		public FrugalList<Node> Nodes { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class CharRefNode : AttrCharDataNode
	{
		internal CharRefNode(ushort code)
		{
			Code = code;
		}

		public ushort Code { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}

	class EntityRefNode : AttrCharDataNode
	{
		internal EntityRefNode(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public override void Accept(IBinXmlVisitor visitor) => visitor.Visit(this);
	}
}
