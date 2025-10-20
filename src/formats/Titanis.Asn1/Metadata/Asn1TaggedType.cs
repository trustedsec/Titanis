using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	/// <summary>
	/// Represents a type that is assigned a tag.
	/// </summary>
	public sealed class Asn1TaggedType : Asn1DerivedType
	{
		internal Asn1TaggedType(Asn1Type baseType, Asn1Tag tag, Asn1TagMode tagMode)
			: base(baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));

			this.StaticTag = tag;
			this.TagMode = tagMode;

			// UNDONE: Causes recursion with deferred types
			//Debug.Assert((baseType.IsConstructed || tagMode is Asn1TagMode.Explicit) == tag.IsConstructed);
		}

		/// <inheritdoc/>
		public sealed override bool IsConstructed => (this.TagMode == Asn1TagMode.Explicit) || this.BaseType.IsConstructed;

		/// <inheritdoc/>
		public sealed override string DefinitionString => $"[{this.StaticTag}] {this.TagMode switch { Asn1TagMode.Implicit => "IMPLICIT", Asn1TagMode.Explicit => "EXPLICIT", _ => string.Empty }} {this.BaseType}";

		/// <inheritdoc/>
		public sealed override bool HasStaticTag => true;
		/// <inheritdoc/>
		public sealed override Asn1Tag StaticTag { get; }
		/// <inheritdoc/>
		public Asn1TagMode TagMode { get; }

		/// <inheritdoc/>
		public sealed override Asn1TypeKind Kind => Asn1TypeKind.Tagged;

		/// <inheritdoc/>
		public sealed override T Accept<T>(ITypeVisitor<T> visitor) => visitor.Visit(this);

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.BaseType.OnAttached(this.DeclaringModule, this, null);
		}
	}
}
