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

	/// <summary>
	/// Represents a primitive (non-constructed) type.
	/// </summary>
	public sealed class Asn1PrimitiveType : Asn1PrimitiveTypeBase
	{
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

		// TODO: Put the actual ASN.1 name.
		/// <inheritdoc/>
		public sealed override string DefinitionString => this.Name;

		/// <inheritdoc/>
		protected sealed override Asn1Tag PrimitiveTag => this.PredefTag;
		/// <summary>
		/// Gets the <see cref="Asn1PredefTag"/> for this type.
		/// </summary>
		public Asn1PredefTag PredefTag { get; }
		/// <inheritdoc/>
		public override Asn1TypeKind Kind => Asn1TypeKind.Primitive;
		/// <summary>
		/// Gets a <see cref="Asn1PrimitiveSubtype"/> indicating the subtype.
		/// </summary>
		public Asn1PrimitiveSubtype Subtype { get; }

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
