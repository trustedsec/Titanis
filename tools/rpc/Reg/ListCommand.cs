using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public enum RegistryItemType
	{
		Key,
		Value,
	}
	public class RegistryItem
	{
		public RegistryItem(string keyName, string? className)
		{
			this.Name = keyName;
			this.ClassName = className;
		}
		public RegistryItem(RegistrySubkeyInfo subkey)
			: this(subkey.KeyName, subkey.ClassName)
		{
			this.ItemType = RegistryItemType.Key;
		}
		public RegistryItem(RegistryValueInfo value)
			: this(value.Name, null)
		{
			this.ItemType = RegistryItemType.Value;
			this.ValueType = value.ValueType;
			this.DataLength = value.DataLength;
			this.Bytes = value.Bytes;

			this.Value = (value.TypedValue is string[] multi) ? string.Join('|', multi) : value.TypedValue;
		}

		public string Name { get; }
		public RegistryItemType ItemType { get; }
		public RegistryValueType? ValueType { get; }
		public string? ClassName { get; }
		[Browsable(false)]
		public int? DataLength { get; }
		public object? Value { get; }
		public byte[]? Bytes { get; }
		public string? BytesAsHexString => (this.Bytes != null) ? this.Bytes.ToHexString() : null;
	}

	[Command]
	[Description("Lists the contents of a key")]
	[OutputRecordType(typeof(RegistryItem))]
	[Example("Lists loaded user hives backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics HKU")]
	internal class ListCommand : RegistryKeyCommand
	{
		[Parameter]
		[Description("Include subkeys")]
		[DefaultValue(true)]
		public SwitchParam IncludeSubkeys { get; set; }

		[Parameter]
		[Description("Include values")]
		[DefaultValue(true)]
		public SwitchParam IncludeValues { get; set; }

		[Parameter]
		[Description("Include value data")]
		[DefaultValue(false)]
		public SwitchParam IncludeData { get; set; }

		protected override RegistryAccessRights RequiredKeyAccess => RegistryAccessRights.None
			| (this.IncludeSubkeys.IsSet ? RegistryAccessRights.EnumerateSubkeys : RegistryAccessRights.None)
			| (this.IncludeValues.IsSet ? RegistryAccessRights.QueryValue : RegistryAccessRights.None)
			| (this.IncludeData.IsSet ? RegistryAccessRights.QueryValue : RegistryAccessRights.None)
			;

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			try
			{
				var subkeys = key.GetSubkeyNames(cancellationToken);
				await foreach (var subkeyInfo in subkeys)
				{
					this.WriteRecord(new RegistryItem(subkeyInfo));
				}
			}
			catch (Exception ex)
			{
				this.WriteError($"An error occurred enumerating subkeys: {ex.Message}");
			}

			try
			{
				var values = key.GetValues(true, cancellationToken);
				await foreach (var valueInfo in values)
				{
					this.WriteRecord(new RegistryItem(valueInfo));
				}
			}
			catch (Exception ex)
			{
				this.WriteError($"An error occurred enumerating values: {ex.Message}");
			}

			return 0;
		}
	}
}
