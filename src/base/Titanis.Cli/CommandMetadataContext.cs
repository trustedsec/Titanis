using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Titanis.Cli
{
	public class CommandMetadataContext
	{
		public CommandMetadataContext(
			MetadataResolver resolver
			)
		{
			this.Resolver = resolver;

			this.ComponentCatalog = new ComponentCatalog(resolver);
		}

		public MetadataResolver Resolver { get; }
		public ComponentCatalog ComponentCatalog { get; }

		internal ICustomTypeDescriptor SurrogateDescriptor(Type recordType)
		{
			return new SurrogateTypeDescriptor(this, recordType);
		}
	}

	class SurrogateTypeDescriptor : CustomTypeDescriptor
	{
		private readonly CommandMetadataContext context;
		private readonly Type type;

		public SurrogateTypeDescriptor(CommandMetadataContext context, Type type)
			: base()
		{
			this.context = context;
			this.type = type;
		}
		class TypeConv : TypeConverter
		{
			protected class SurrogatePropDescriptor : SimplePropertyDescriptor
			{
				public SurrogatePropDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes)
					: base(componentType, name, propertyType, attributes)
				{ }

				public override object GetValue(object component)
				{
					throw new NotImplementedException();
				}

				public override void SetValue(object component, object value)
				{
					throw new NotImplementedException();
				}
			}

			public static PropertyDescriptor CreatePropertyDescriptor(Type componentType, PropertyInfo property, MetadataResolver resolver)
			{
				var desc = new SurrogatePropDescriptor(componentType, property.Name, property.PropertyType, resolver.GetCustomAttributes<Attribute>(property, true).ToArray());
				return desc;
			}
		}

		public sealed override AttributeCollection GetAttributes()
		{
			var attrs = this.context.Resolver.GetCustomAttributes<Attribute>(this.type, true).ToArray();
			return new AttributeCollection(attrs);
		}
		public override PropertyDescriptorCollection GetProperties()
		{
			var props = this.type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

			var descriptors = Array.ConvertAll(props, r => TypeConv.CreatePropertyDescriptor(this.type, r, this.context.Resolver));
			return new PropertyDescriptorCollection(descriptors, true);
		}
	}
}
