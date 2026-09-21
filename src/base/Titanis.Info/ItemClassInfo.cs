using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Titanis.Info.Schema;

namespace Titanis.Info
{

	public class ItemClassInfo
	{
		private readonly ItemClass classRec;

		internal ItemClassInfo(
			InfoBase owner,
			ItemClass classRec)
		{
			Owner = owner;
			this.classRec = classRec;
		}

		internal int ClassId => this.classRec.Id;
		public InfoBase Owner { get; }
		public string Name => this.classRec.Name;

		private readonly List<ItemPropertyInfo> _props = new List<ItemPropertyInfo>();
		private readonly Dictionary<string, ItemPropertyInfo> _propsByName = new Dictionary<string, ItemPropertyInfo>(StringComparer.OrdinalIgnoreCase);
		internal void AddProperty(ItemPropertyInfo property)
		{
			this._props.Add(property);
			// TODO: Sync
			this._propsByName.Add(property.Name, property);
		}
		internal Dictionary<string, ItemPropertyInfo> GetPropsByName() => this._propsByName;
		public ItemPropertyInfo[] GetProperties() => this._props.ToArray();
	}

	public class ItemPropertyInfo : PropertyDescriptor
	{
		internal ItemPropertyInfo(ItemProperty propdata)
			: base(propdata.Name, [])
		{
			this.propdata = propdata;
			this.PropertyType = DataHelpers.GetTypeFromCode(propdata.ClrTypeCode);
			if (0 != (propdata.Flags & ItemPropertyFlags.Multi))
				this.PropertyType = this.PropertyType.MakeArrayType();
		}

		private readonly ItemProperty propdata;

		internal string FieldName => this.propdata.FieldName;
		internal int Id => this.propdata.Id;
		public override Type PropertyType { get; }
		public bool IsMultiValued => 0 != (this.propdata.Flags & ItemPropertyFlags.Multi);

		internal ExtraFieldInfo GetExtraFieldInfo() => new ExtraFieldInfo { Name = this.FieldName, Typecode = this.propdata.ClrTypeCode };


		public override Type ComponentType => typeof(Item);

		public override bool IsReadOnly => true;

		public override bool CanResetValue(object component) => false;

		private Item AsItem(object component) => (Item)component;
		public override object? GetValue(object component)
		{
			Item item = this.AsItem(component);
			return this.IsMultiValued ? item.GetMultiProp(this.Id) : item.GetProp(this.Name);
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
}
