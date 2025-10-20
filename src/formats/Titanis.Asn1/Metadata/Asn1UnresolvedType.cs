using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1UnresolvedType : Asn1Type
	{
		public Asn1UnresolvedType(string name)
		{
			this.Name = name;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "<unresolved>";

		/// <inheritdoc/>
		public sealed override bool IsConstructed => false;

		/// <inheritdoc/>
		public sealed override bool HasStaticTag => false;

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Unknown;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
