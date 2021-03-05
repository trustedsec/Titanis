using System;
using System.Diagnostics;

namespace Titanis.Asn1.Metadata
{
	[Flags]
	enum Asn1TypeFlags
	{
		None = 0,
		Attached = (1 << 0),
		TopLevel = (1 << 1),
	}

	public abstract class Asn1Type
	{
		private protected Asn1Type()
		{
		}

		public virtual Asn1Type CanonicalType => this;

		public string Name { get; protected set; }
		public Asn1Module Module { get; private set; }
		public Asn1Type EnclosingType { get; private set; }
		public bool IsPrimitiveType => this.IsPrimitiveInternal;
		internal virtual bool IsPrimitiveInternal => false;

		internal string SuggestedName { get; private set; }
		internal void SuggestName(string suggestedName)
		{
			if (this.SuggestedName == null)
				this.SuggestedName = suggestedName;
			var canonical = this.CanonicalType;
			if (canonical != this)
				canonical.SuggestName(suggestedName);
		}

		public abstract bool IsConstructed { get; }

		public override string ToString() => this.Name;

		public virtual Asn1Tag Tag => throw new NotSupportedException();
		public virtual Asn1Tag EffectiveTag => this.HasStaticTag ? this.Tag : Asn1PredefTag.Unspecified;
		public abstract bool HasStaticTag { get; }

		private Asn1TypeFlags _flags;
		private bool IsAttached => (0 != (this._flags & Asn1TypeFlags.Attached));
		private bool IsTopLevel => (0 != (this._flags & Asn1TypeFlags.TopLevel));

		internal void AttachTopLevel(Asn1Module module, string name)
		{
			if (!this.IsAttached)
			{
				this._flags |= Asn1TypeFlags.TopLevel;
				this.OnAttaching(module, name);
			}
		}

		internal void OnAttaching(Asn1Module module, string name)
		{
			if (!this.IsAttached)
			{
				this.Module = module;
				if (string.IsNullOrEmpty(this.Name))
					this.Name = name;
			}
			// UNDONE: Silently don't attach.  This may be a reference to a type from another module.
			//throw new InvalidOperationException(Messages.Asn1_TypeAlreadyAttachedToModule);
		}
		internal void OnAttached(Asn1Type enclosingType, string name)
		{
			Debug.Assert(this.Module != null);

			if (!this.IsAttached)
			{
				this._flags |= Asn1TypeFlags.Attached;

				if (this.EnclosingType == null)
					this.EnclosingType = enclosingType;

				if (string.IsNullOrEmpty(this.Name))
					this.Name = name;

				this.Module.AddType(this);

				this.OnAttachedOverride();
			}
		}

		protected virtual void OnAttachedOverride()
		{
		}

		public virtual Asn1Field TryGetMember(string name)
		{
			throw new NotSupportedException();
		}

		public abstract Asn1TypeKind Kind { get; }

		public abstract object Visit(ITypeVisitor visitor);
	}

	public static class Asn1Types
	{
		public static Asn1AnyType Any => Asn1AnyType.Instance;

		#region Primitive Types
		public static Asn1PrimitiveType BitString { get; } = new Asn1PrimitiveType(Asn1PredefTag.BitString);
		public static Asn1PrimitiveType Boolean { get; } = new Asn1PrimitiveType(Asn1PredefTag.Boolean);
		public static Asn1PrimitiveType Integer { get; } = new Asn1PrimitiveType(Asn1PredefTag.Integer);
		public static Asn1PrimitiveType Real { get; } = new Asn1PrimitiveType(Asn1PredefTag.Real);
		public static Asn1PrimitiveType OctetString { get; } = new Asn1PrimitiveType(Asn1PredefTag.OctetString);
		public static Asn1PrimitiveType Null { get; } = new Asn1PrimitiveType(Asn1PredefTag.Null);
		public static Asn1PrimitiveType Sequence { get; } = new Asn1PrimitiveType(Asn1PredefTag.Sequence);
		public static Asn1PrimitiveType ObjectIdentifier { get; } = new Asn1PrimitiveType(Asn1PredefTag.ObjectIdentifier);
		public static Asn1PrimitiveType RelativeOid { get; } = new Asn1PrimitiveType(Asn1PredefTag.RelativeOid);
		public static Asn1PrimitiveType Iri { get; } = new Asn1PrimitiveType(Asn1PredefTag.Iri);
		public static Asn1PrimitiveType RelativeIri { get; } = new Asn1PrimitiveType(Asn1PredefTag.RelativeIri);
		public static Asn1PrimitiveType Time { get; } = new Asn1PrimitiveType(Asn1PredefTag.Time);
		public static Asn1PrimitiveType Date { get; } = new Asn1PrimitiveType(Asn1PredefTag.Date);
		public static Asn1PrimitiveType TimeOfDay { get; } = new Asn1PrimitiveType(Asn1PredefTag.TimeOfDay);
		public static Asn1PrimitiveType DateTime { get; } = new Asn1PrimitiveType(Asn1PredefTag.DateTime);
		public static Asn1PrimitiveType Duration { get; } = new Asn1PrimitiveType(Asn1PredefTag.Duration);
		public static Asn1PrimitiveType BMPString { get; } = new Asn1PrimitiveType(Asn1PredefTag.BMPString);
		public static Asn1PrimitiveType GeneralString { get; } = new Asn1PrimitiveType(Asn1PredefTag.GeneralString);
		public static Asn1PrimitiveType GraphicString { get; } = new Asn1PrimitiveType(Asn1PredefTag.GraphicString);
		public static Asn1PrimitiveType IA5String { get; } = new Asn1PrimitiveType(Asn1PredefTag.IA5String);
		public static Asn1PrimitiveType Iso646String { get; } = new Asn1PrimitiveType(Asn1PredefTag.Iso646String);
		public static Asn1PrimitiveType NumericString { get; } = new Asn1PrimitiveType(Asn1PredefTag.NumericString);
		public static Asn1PrimitiveType PrintableString { get; } = new Asn1PrimitiveType(Asn1PredefTag.PrintableString);
		public static Asn1PrimitiveType TeletexString { get; } = new Asn1PrimitiveType(Asn1PredefTag.TeletexString);
		public static Asn1PrimitiveType T61String { get; } = new Asn1PrimitiveType(Asn1PredefTag.T61String);
		public static Asn1PrimitiveType UniversalString { get; } = new Asn1PrimitiveType(Asn1PredefTag.UniversalString);
		public static Asn1PrimitiveType UTF8String { get; } = new Asn1PrimitiveType(Asn1PredefTag.UTF8String);
		public static Asn1PrimitiveType VideotexString { get; } = new Asn1PrimitiveType(Asn1PredefTag.VideotexString);
		public static Asn1PrimitiveType CharacterString { get; } = new Asn1PrimitiveType(Asn1PredefTag.CharacterString);
		public static Asn1PrimitiveType VisibleString { get; } = new Asn1PrimitiveType(Asn1PredefTag.VisibleString);
		public static Asn1PrimitiveType GeneralizedTime { get; } = new Asn1PrimitiveType(Asn1PredefTag.GeneralizedTime);
		public static Asn1PrimitiveType UtcTime { get; } = new Asn1PrimitiveType(Asn1PredefTag.UtcTime);
		#endregion
	}
}