using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Titanis.Info.Schema;

namespace Titanis.Info
{
	public partial class Item
	{
		private readonly InfoBase owner;
		private readonly ItemInfo info;
		private readonly ItemClassInfo itemClass;

		internal Item(
			InfoBase owner,
			ItemInfo info,
			ItemClassInfo itemClass
			)
		{
			this.owner = owner;
			this.info = info;
			this.itemClass = itemClass;
		}

		internal object? GetProp(string propertyName)
		{
			this.info.ExtraFields.TryGetValue(propertyName, out var value);
			return value;
		}
	}

	partial class Item : ICustomTypeDescriptor
	{
		public AttributeCollection GetAttributes() => AttributeCollection.Empty;
		public string GetClassName() => this.itemClass.Name;
		public string GetComponentName()
		{
			throw new NotImplementedException();
		}

		public TypeConverter GetConverter() => null;
		public EventDescriptor GetDefaultEvent() => null;
		public PropertyDescriptor GetDefaultProperty() => null;
		public object GetEditor(Type editorBaseType) => null;
		public EventDescriptorCollection GetEvents() => EventDescriptorCollection.Empty;
		public EventDescriptorCollection GetEvents(Attribute[] attributes) => EventDescriptorCollection.Empty;

		private PropertyDescriptorCollection? _props;
		public PropertyDescriptorCollection GetProperties() => (this._props ??= new PropertyDescriptorCollection(this.itemClass.GetProperties()));

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();
		public object GetPropertyOwner(PropertyDescriptor pd) => this;
	}

	internal class ItemInfo : IWantExtraFields
	{
		internal IList<ItemMultiValue> multiValues;

		public long ItemId { get; set; }
		public ItemClassInfo ItemClass { get; set; }
		public int Version { get; set; }
		public Dictionary<string, object?> ExtraFields { get; set; }
		void IWantExtraFields.SetExtraFields(Dictionary<string, object?> values)
		{
			this.ExtraFields = values;
		}

		public ItemFlags ItemFlags { get; internal set; }
	}

	record struct ExtraFieldInfo(string Name, TypeCode Typecode);

	public interface IWantExtraFields
	{
		void SetExtraFields(Dictionary<string, object?> values);
	}
}
