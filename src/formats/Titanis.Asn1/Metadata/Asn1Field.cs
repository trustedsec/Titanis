using System;

namespace Titanis.Asn1.Metadata
{
	[Flags]
	public enum Asn1FieldOptions
	{
		None = 0,

		Optional = (1 << 0),
		HasDefaultValue = (1 << 1),
	}

	public class Asn1Field
	{
		public Asn1FieldOptions Options { get; }

		public string Name { get; }
		public Asn1Type FieldType { get; }
		public bool IsOptional => (0 != (this.Options & Asn1FieldOptions.Optional));
		public bool HasDefaultValue => (0 != (this.Options & Asn1FieldOptions.HasDefaultValue));
		public object DefaultValue { get; }

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
			bool optional,
			object defaultValue
			)
			: this(name, fieldType)
		{
			if (optional)
			{
				this.Options |= Asn1FieldOptions.Optional;
			}
			else
			{
				this.Options |= Asn1FieldOptions.HasDefaultValue | Asn1FieldOptions.Optional;
				this.DefaultValue = defaultValue;
			}
		}

		public static bool IsValidName(string name) => Asn1ValueDef.IsValidName(name);
	}
}