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

	/// <summary>
	/// Represents an ASN.1 type.
	/// </summary>
	public abstract class Asn1Type
	{
		protected Asn1Type()
		{
		}

		/// <summary>
		/// Gets the canonical type for this type.
		/// </summary>
		/// <remarks>
		/// For derived types, this returns the type it is based on.
		/// </remarks>
		public virtual Asn1Type CanonicalType => this;
		/// <summary>
		/// Gets the name of this type, if it is named.
		/// </summary>
		public string? Name { get; protected set; }
		internal string? ComponentName { get; private set; }
		/// <summary>
		/// Gets the module that declared this type.
		/// </summary>
		/// <remarks>
		/// Universal types return <see langword="null"/>.
		/// </remarks>
		public Asn1Module? DeclaringModule { get; private set; }
		/// <summary>
		/// Gets the type that encloses this type.
		/// </summary>
		public Asn1Type? EnclosingType { get; private set; }
		/// <summary>
		/// Gets a value indicating whether this is a constructed type.
		/// </summary>
		/// <remarks>
		/// Constructed types are composed of elements.
		/// </remarks>
		public abstract bool IsConstructed { get; }
		/// <summary>
		/// Gets a <see cref="Asn1TypeKind"/> value for this type.
		/// </summary>
		public abstract Asn1TypeKind Kind { get; }
		public Asn1TypeKind CanonicalKind => this.CanonicalType.Kind;

		private const string UnnamedTypeName = "<unnamed>";

		/// <inheritdoc/>
		public sealed override string ToString() => (this.Name is null) ? this.DefinitionString : $"{this.Name} ::= {this.DefinitionString}";
		/// <summary>
		/// Gets the definition as a string.
		/// </summary>
		public abstract string DefinitionString { get; }

		/// <summary>
		/// Gets a value indicating whether this type has a tag that doesn't change.
		/// </summary>
		public abstract bool HasStaticTag { get; }
		/// <summary>
		/// Gets the static tag for this type.
		/// </summary>
		/// <remarks>
		/// If <see cref="HasStaticTag"/> is <see langword="false"/>, this property
		/// throws <see cref="NotImplementedException"/>.
		/// </remarks>
		public virtual Asn1Tag StaticTag => throw new NotSupportedException();
		//public virtual Asn1Tag EffectiveTag => this.HasStaticTag ? this.Tag : Asn1PredefTag.Unspecified;

		private Asn1TypeFlags _flags;
		private bool IsAttached => (0 != (this._flags & Asn1TypeFlags.Attached));
		private bool IsTopLevel => (0 != (this._flags & Asn1TypeFlags.TopLevel));

		/// <summary>
		/// Attaches the type as a top-level type defined within a module.
		/// </summary>
		/// <param name="declaringModule">Module declaring the type</param>
		/// <param name="name">Assigned name</param>
		internal void AttachTopLevel(Asn1Module declaringModule, string name)
		{
			if (!this.IsAttached && this.Kind is not Asn1TypeKind.Primitive)
			{
				this._flags |= Asn1TypeFlags.TopLevel;

				this.DeclaringModule = declaringModule;
				if (string.IsNullOrEmpty(this.Name))
					this.Name = name;
			}
		}

		internal void OnAttached(
			Asn1Module? declaringModule,
			Asn1Type? enclosingType,
			string? componentName)
		{
			if (!this.IsAttached && this.Kind is not Asn1TypeKind.Primitive)
			{
				this._flags |= Asn1TypeFlags.Attached;

				if (this.DeclaringModule == null)
					this.DeclaringModule = declaringModule;

				if (this.EnclosingType == null)
					this.EnclosingType = enclosingType;

				if (string.IsNullOrEmpty(this.ComponentName))
					this.ComponentName = componentName;

				this.DeclaringModule?.AddType(this);

				this.OnAttachedOverride();
			}
		}

		protected virtual void OnAttachedOverride()
		{
		}

		public abstract T Accept<T>(ITypeVisitor<T> visitor);

		public Asn1Type Tagged(Asn1Tag tag, Asn1TagMode tagMode)
		{
			var type = this;
			if (tagMode is Asn1TagMode.Implicit)
			{
				if (type is Asn1AnyType or Asn1ChoiceType)
					throw new InvalidOperationException("Cannot wrap ANY or CHOICE with an implicit tag");
				if (type is Asn1TaggedType tagged && tagged.TagMode is Asn1TagMode.Implicit)
					type = tagged.BaseType;
			}

			if (tagMode is Asn1TagMode.Explicit || type.IsConstructed)
				tag = tag.AsConstructed();

				return new Asn1TaggedType(type, tag, tagMode);
		}
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