using Lightweight_Directory_Access_Protocol_V3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public sealed partial class LdapEntry
	{
		internal LdapEntry(
			LdapDistinguishedName? dn,
			LdapAttribute[] attributes
			)
		{
			this.EntryName = dn;
			this.Attributes = attributes;

			this._attrsByType = attributes.ToDictionary(r => r.AttributeType);
		}

		[DisplayName("dn")]
		public LdapDistinguishedName? EntryName { get; }
		public LdapAttribute[] Attributes { get; }
		public string? ObjectClass { get; }

		private Dictionary<AttributeTypeDescription, LdapAttribute> _attrsByType;

		public sealed override string? ToString() => this.EntryName?.Text;

		public LdapAttribute? this[AttributeTypeDescription descr]
		{
			get
			{
				this._attrsByType.TryGetValue(descr, out var attr);
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

			if (this._enumType != null)
			{
				var value = Enum.ToObject(this._enumType, attr.Value);
				return value;
			}

			return (attr is null) ? null : this.attr.IsSingleValued ? attr.Value : attr.Values;
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
