using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents ANY type.
	/// </summary>
	/// <remarks>
	/// Although a value of type ANY may be of a constructed type,
	/// this implementation treats the value as an octet string.
	/// </remarks>
	public sealed class Asn1AnyType : Asn1Type
	{
		public Asn1AnyType(string? definedBy)
		{
			this.DefinedBy = definedBy;
		}

		internal static readonly Asn1AnyType Instance = new Asn1AnyType(null);

		/// <inheritdoc/>
		public sealed override string DefinitionString => (this.DefinedBy is null) ? $"ANY" : $"ANY DEFINED BY {this.DefinedBy}";

		/// <summary>
		/// Gets the field that defines the type of values of this type.
		/// </summary>
		public string? DefinedBy { get; }
		///<inheritdoc/>
		public override Asn1TypeKind Kind => Asn1TypeKind.Any;
		/// <inheritdoc/>
		public sealed override bool HasStaticTag => false;
		/// <inheritdoc/>
		public sealed override bool IsConstructed => false;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
