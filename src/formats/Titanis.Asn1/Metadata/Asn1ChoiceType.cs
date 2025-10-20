using System;
using System.Linq;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a CHOICE defined in ASN.1.
	/// </summary>
	public sealed class Asn1ChoiceType : Asn1ComplexType
	{
		public Asn1ChoiceType(Asn1Field[] members, bool isExtensible) : base(members)
		{
			foreach (var member in members)
			{
				if (!member.FieldType.HasStaticTag)
					throw new ArgumentException($"Member '{member.Name}' does not have a static tag and cannot be included within a CHOICE.");
			}

			this.IsExtensible = isExtensible;
		}

		/// <inheritdoc/>
		public sealed override string DefinitionString => "CHOICE {...}";

		/// <inheritdoc/>
		public sealed override bool HasStaticTag => false;
		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Choice;

		/// <inheritdoc/>
		public sealed override bool IsExtensible { get; }

		//public bool HasDynamicMember => this.Members.Any(m => !m.FieldType.HasStaticTag);

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);
	}
}
