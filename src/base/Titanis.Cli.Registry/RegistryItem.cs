using System.ComponentModel;
using Titanis.Winterop.Registry;

namespace Titanis.Cli.Registry
{
	public enum RegistryItemType
	{
		Key,
		Value,
	}
	public class RegistryItem
	{
		public RegistryItem(string? parentKey, string keyName, string? className)
		{
			this.ParentKeyName = parentKey;
			this.Name = keyName;
			this.ClassName = className;
		}
		public RegistryItem(string? parentKey, RegistrySubkeyInfo subkey)
			: this(parentKey, subkey.KeyName, subkey.ClassName)
		{
			this.ItemType = RegistryItemType.Key;
		}
		public RegistryItem(RegistryPath subkey)
			: this(RegistryPath.GetParentKeyNameFromPath(subkey.KeyPath), subkey.KeyName, null)
		{
			this.ItemType = RegistryItemType.Key;
		}
		public RegistryItem(string? parentKey, RegistryValueInfo value)
			: this(parentKey, value.Name, null)
		{
			this.ItemType = RegistryItemType.Value;
			this.ValueType = value.ValueType;
			this.DataLength = value.DataLength;
			this.Bytes = value.Bytes;
			this.Value = value.TypedValue is byte[] bytes ? new Blob(bytes) : value.TypedValue;
		}
		public string? ParentKeyName { get; }
		public string Name { get; }
		public RegistryItemType ItemType { get; }
		public RegistryValueType? ValueType { get; }
		public string? ClassName { get; }
		[Browsable(false)]
		public int? DataLength { get; }
		public object? Value { get; }
		[Browsable(false)]
		public byte[]? Bytes { get; }
		public string? BytesAsHexString => (this.Bytes != null) ? this.Bytes.ToHexString() : null;
	}
}
