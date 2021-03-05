using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1TaggedType : Asn1DerivedType
	{
		//internal Asn1TaggedType(Asn1Tag tag, Asn1TagMode tagMode)
		//{
		//	this.Tag = tag;
		//	this.TagMode = tagMode;
		//}

		public Asn1TaggedType(Asn1Type baseType, Asn1Tag tag, Asn1TagMode tagMode)
			: base(baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));

			this.Tag = tag;
			this.TagMode = tagMode;
		}

		public sealed override bool IsConstructed => (this.TagMode == Asn1TagMode.Explicit) || this.BaseType.IsConstructed;

		public override bool HasStaticTag => true;
		public override Asn1Tag Tag { get; }
		public Asn1TagMode TagMode { get; }

		public override Asn1TypeKind Kind => Asn1TypeKind.Tagged;

		public override object Visit(ITypeVisitor visitor)
		{
			return visitor.VisitTagged(this);
		}

		protected override void OnAttachedOverride()
		{
			base.OnAttachedOverride();
			this.BaseType.OnAttaching(this.Module, null);
			this.BaseType.OnAttached(this, null);
		}
	}
}
