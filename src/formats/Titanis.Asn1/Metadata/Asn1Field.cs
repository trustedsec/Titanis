using System;
using System.Runtime.CompilerServices;

namespace Titanis.Asn1.Metadata
{
	[Flags]
	public enum Asn1FieldOptions
	{
		None = 0,

		Optional = (1 << 0),
		HasDefaultValue = (1 << 1),
	}

	/// <summary>
	/// Represents a field within a <see cref="Asn1ComplexType"/>.
	/// </summary>
	public sealed class Asn1Field : ICloneable
	{
		public Asn1Field(
			string name,
			Asn1Type fieldType
			)
		{
			if (name is null)
				throw new ArgumentNullException(nameof(name));
			if (fieldType is null)
				throw new ArgumentNullException(nameof(fieldType));
			if (!IsValidName(name))
				throw new ArgumentException(string.Format(Messages.Asn1_ValueNameInvalid, name));

			this.Name = name;
			this.FieldType = fieldType;
		}

		public Asn1Field(
			string name,
			Asn1Type fieldType,
			bool optional
			)
			: this(name, fieldType)
		{
			if (optional)
				this.Options |= Asn1FieldOptions.Optional;
		}

		public Asn1Field(
			string name,
			Asn1Type fieldType,
			object? defaultValue
			)
			: this(name, fieldType)
		{
			this.Options |= Asn1FieldOptions.HasDefaultValue;
			this.DefaultValue = defaultValue;
		}

		private Asn1Field(
			string name,
			Asn1Type fieldType,
			Asn1FieldOptions options,
			object? defaultValue
			)
			: this(name, fieldType)
		{
			this.Name = name;
			this.FieldType = fieldType;
			this.Options = options;
			this.DefaultValue = defaultValue;
		}

		public Asn1FieldOptions Options { get; }

		public sealed override string ToString() =>
			this.IsOptional ? $"{this.Name} {this.FieldType} OPTIONAL"
			: (this.HasDefaultValue) ? $"{this.Name} {this.FieldType} DEFAULT {this.DefaultValue ?? "NULL"}"
			: $"{this.Name} {this.FieldType}";

		public string Name { get; }
		public Asn1Type FieldType { get; }
		public bool IsOptional => (0 != (this.Options & Asn1FieldOptions.Optional));
		public bool HasDefaultValue => (0 != (this.Options & Asn1FieldOptions.HasDefaultValue));
		public object? DefaultValue { get; }

		public static bool IsValidName(string name) => Asn1ValueDef.IsValidName(name);

		public Asn1Field Clone() => new Asn1Field(this.Name, this.FieldType, this.Options, this.DefaultValue);

		/// <inheritdoc/>
		object ICloneable.Clone() => this.Clone();
	}
}