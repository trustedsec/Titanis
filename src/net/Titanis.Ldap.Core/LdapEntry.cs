using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Titanis.Ldap
{
	public sealed partial class LdapEntry
	{
		public LdapEntry(
			LdapDistinguishedName? dn,
			LdapAttribute[] attributes
			)
		{
			this.EntryName = dn;
			this.Attributes = attributes;

			this._attrsByName = attributes.ToDictionary(r => r.AttributeType.Name, StringComparer.OrdinalIgnoreCase);
			this.ObjectClass = this[LdapAttributeTypes.ObjectClass]?.Values?.LastOrDefault() as string;
		}

		public LdapEntry(
			LdapDistinguishedName? dn,
			Dictionary<string, object> attributes)
			: this(dn, AcceptAttributes(attributes))
		{
		}

		private static LdapAttribute[] AcceptAttributes(Dictionary<string, object> attributes)
		{
			ArgumentNullException.ThrowIfNull(attributes);

			List<LdapAttribute> attrs = new List<LdapAttribute>(attributes.Count);
			foreach (var attrEntry in attributes)
			{
				if (attrEntry.Value is null)
					throw new ArgumentException($"The attribute list contains an attribute '{attrEntry.Key}' with no value.");

				var attrType = LdapAttributeTypes.TryGetByNameOrOid(attrEntry.Key);
				if (attrType is null)
				{
					attrType = new AttributeTypeDescription(default, attrEntry.Key);
				}

				if (attrEntry.Value is object[] multiValues)
					;
				else
					multiValues = [attrEntry.Value];

				var encodedValues = new object[multiValues.Length];
				for (int iValue = 0; iValue < encodedValues.Length; iValue++)
				{
					var multiValue = multiValues[iValue];

					var syntax = attrType?.Syntax;
					if (syntax != null && !syntax.RuntimeType.IsAssignableFrom(multiValue.GetType()))
					{
						if (multiValue is string text)
						{
							multiValue = attrType.Syntax.Parse(text);
						}
						else if (multiValue is BinaryString binary)
						{
							multiValue = attrType.Syntax.Decode(binary.Bytes);
						}
						else
						{
							throw new NotImplementedException();
						}
					}

					encodedValues[iValue] = multiValue;
				}

				attrs.Add(new LdapAttribute(new LdapAttributeDescription(attrType.Name), attrType, encodedValues));
			}
			return attrs.ToArray();
		}

		[DisplayName("dn")]
		public LdapDistinguishedName? EntryName { get; }
		public LdapAttribute[] Attributes { get; }
		public string? ObjectClass { get; }

		private readonly Dictionary<string?, LdapAttribute> _attrsByName;

		public sealed override string? ToString() => this.EntryName?.Text;

		public LdapAttribute? this[AttributeTypeDescription descr]
		{
			get
			{
				this._attrsByName.TryGetValue(descr.Name, out var attr);
				return attr;
			}
		}

		public LdapAttribute? this[string name]
		{
			get
			{
				this._attrsByName.TryGetValue(name, out var attr);
				return attr;
			}
		}
	}

	public sealed class LdapAttributeInfo
	{
		internal LdapAttributeInfo(string name, string oid, LdapSyntax syntax, int? maxLength)
		{ }
	}

	class LdapNamePropertyDescriptor : PropertyDescriptor
	{
		internal LdapNamePropertyDescriptor()
			: base(nameof(LdapEntry.EntryName), null)
		{
		}

		public override Type ComponentType => typeof(LdapEntry);
		public override bool IsReadOnly => true;
		public override Type PropertyType => typeof(string);
		public override bool CanResetValue(object component) => false;
		public override object? GetValue(object? component) => ((LdapEntry)component).EntryName?.Text;
		public override void ResetValue(object component) => throw new NotSupportedException();
		public override void SetValue(object? component, object? value) => throw new NotSupportedException();
		public override bool ShouldSerializeValue(object component) => true;
	}

	class LdapPropertyDescriptor : PropertyDescriptor
	{
		private readonly AttributeTypeDescription attr;

		internal LdapPropertyDescriptor(AttributeTypeDescription attr)
			: base(attr.Name, null)
		{
			this.attr = attr;
			if (NamedBitGroups.GroupsByName.TryGetValue(attr.Name, out var group))
			{
				_enumType = group.PrimaryEnumType;
			}

		}

		private Type? _enumType;

		public override Type ComponentType => typeof(LdapEntry);
		public override bool IsReadOnly => true;
		public override Type PropertyType
		{
			get
			{
				if (this._enumType is not null)
					return this._enumType;

				var syntax = attr.Syntax;
				if (this.attr.IsSingleValued)
					return syntax?.RuntimeType ?? typeof(object);
				else
					return (syntax?.RuntimeType ?? typeof(object)).MakeArrayType();
			}
		}
		public override bool CanResetValue(object component) => false;
		public override object? GetValue(object? component)
		{
			var entry = (LdapEntry)component;
			var attr = entry[this.attr];

			if (attr != null)
			{
				if (this._enumType != null)
				{
					var value = Enum.ToObject(this._enumType, attr.Value);
					return value;
				}

				return this.attr.IsSingleValued ? attr.Value : attr.Values;
			}

			return null;
		}
		public override void ResetValue(object component) => throw new NotSupportedException();
		public override void SetValue(object? component, object? value) => throw new NotSupportedException();
		public override bool ShouldSerializeValue(object component) => true;
	}

	partial class LdapEntry : ICustomTypeDescriptor
	{
		AttributeCollection ICustomTypeDescriptor.GetAttributes() => AttributeCollection.Empty;
		string? ICustomTypeDescriptor.GetClassName() => this.ObjectClass;
		string? ICustomTypeDescriptor.GetComponentName() => this.EntryName?.Text;
		TypeConverter? ICustomTypeDescriptor.GetConverter() => null;
		EventDescriptor? ICustomTypeDescriptor.GetDefaultEvent() => null;
		PropertyDescriptor? ICustomTypeDescriptor.GetDefaultProperty() => null;
		object? ICustomTypeDescriptor.GetEditor(Type editorBaseType) => null;
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => EventDescriptorCollection.Empty;
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[]? attributes) => EventDescriptorCollection.Empty;

		private PropertyDescriptorCollection _props;
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => (this._props ??= this.BuildProps());

		private PropertyDescriptorCollection BuildProps()
		{
			List<PropertyDescriptor> props = new List<PropertyDescriptor>(this.Attributes.Length + 1);
			props.Add(new LdapNamePropertyDescriptor());
			foreach (var attr in this.Attributes)
			{
				props.Add(new LdapPropertyDescriptor(attr.AttributeType));
			}
			return new PropertyDescriptorCollection(props.ToArray(), true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[]? attributes)
		{
			// TODO: Apply property filters
			return (this._props ??= this.BuildProps());
		}

		object? ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor? pd)
		{
			return this;
		}
	}
}
