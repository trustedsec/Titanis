using System.ComponentModel;
using System.Reflection;
using Titanis.Cli;

namespace Titanis.ToolDocBuilder
{
	internal class SurrogateTypeDescriptor : ICustomTypeDescriptor
	{
		internal SurrogateTypeDescriptor(Type type, ContextResolver resolver)
		{
			this._type = type;
			this._resolver = resolver;
		}


		private readonly Type _type;
		private readonly ContextResolver _resolver;

		public AttributeCollection GetAttributes()
		{
			return new AttributeCollection(this._resolver.GetCustomAttributes(this._type, typeof(Attribute), true).ToArray());
		}

		public string? GetClassName() => this._type.FullName;
		public string? GetComponentName() => this._type.Name;
		public TypeConverter? GetConverter() => null;
		public EventDescriptor? GetDefaultEvent() => null;
		public PropertyDescriptor? GetDefaultProperty() => null;
		public object? GetEditor(Type editorBaseType) => null;
		public EventDescriptorCollection GetEvents() => EventDescriptorCollection.Empty;
		public EventDescriptorCollection GetEvents(Attribute[]? attributes) => EventDescriptorCollection.Empty;

		private PropertyDescriptorCollection? _props;
		public PropertyDescriptorCollection GetProperties() => (this._props ??= this.LoadProperties());
		private PropertyDescriptorCollection LoadProperties()
		{
			var props = this._type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			var propDescrs = Array.ConvertAll(props, r => new SurrogatePropertyDescriptor(r, this._resolver.GetCustomAttributes(r, typeof(Attribute), true).ToArray()));
			return new PropertyDescriptorCollection(propDescrs, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[]? attributes) => this._props;
		public object? GetPropertyOwner(PropertyDescriptor? pd) => this;
	}
}

class SurrogatePropertyDescriptor : PropertyDescriptor
{
	private readonly PropertyInfo _prop;

	internal SurrogatePropertyDescriptor(PropertyInfo prop, Attribute[] attributes)
		: base(prop.Name, attributes)
	{
		this._prop = prop;
	}

	public override Type ComponentType => null;

	public override bool IsReadOnly => false;

	public override Type PropertyType => this._prop.PropertyType;

	public override bool CanResetValue(object component) => false;
	public override object GetValue(object component)
	{
		throw new NotImplementedException();
	}

	public override void ResetValue(object component)
	{
		throw new NotImplementedException();
	}

	public override void SetValue(object component, object value)
	{
		throw new NotImplementedException();
	}

	public override bool ShouldSerializeValue(object component) => false;
}