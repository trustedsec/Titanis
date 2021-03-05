using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{

	public enum Asn1PrimitiveSubtype
	{
		Unspecified = 0,

		Byte,
		SByte,
		Int16,
		UInt16,
		Int32,
		UInt32,
		Int64,
		UInt64,
	}

	public sealed class Asn1PrimitiveType : Asn1PrimitiveTypeBase
	{
		public override Asn1Tag Tag => this.PredefTag;
		public Asn1PredefTag PredefTag { get; }
		internal override bool IsPrimitiveInternal => true;

		public override Asn1TypeKind Kind => Asn1TypeKind.Primitive;
		public Asn1PrimitiveSubtype Subtype { get; }

		public Asn1PrimitiveType(Asn1PredefTag tag)
		{
			this.PredefTag = tag;
			this.Name = tag.ToString();
		}

		public Asn1PrimitiveType(Asn1PredefTag tag, Asn1PrimitiveSubtype subtype)
			: this(tag)
		{
			this.Subtype = subtype;
		}

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitPrimitive(this);
		}
	}
}
