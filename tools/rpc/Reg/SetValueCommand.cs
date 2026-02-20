using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	public enum RegistryValueEncoding
	{
		Auto = 0,
		Utf16,
		Dword,
		Qword,
		Binary,
		File,
	}

	[TypeConverter(typeof(RegistryValueSpecConverter))]
	class RegistryValueSpec
	{
		public RegistryValueSpec(string? valueName, RegistryValueType valueType, byte[] valueData)
		{
			this.ValueName = valueName;
			this.ValueType = valueType;
			this.ValueData = valueData;
		}

		public string? ValueName { get; }
		public RegistryValueType ValueType { get; }
		public byte[] ValueData { get; }
	}

	partial class RegistryValueSpecConverter : TypeConverter
	{
		private static readonly Regex rgxValueSpec = SpecRegex();

		[GeneratedRegex(@"^(?<n>((\\[\\])|[^\\])+)(:(?<t>\w+))(:(?<e>\w+))?=(?<v>.*)$")]
		private static partial Regex SpecRegex();

		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
			=> (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				var m = rgxValueSpec.Match(str);
				if (!m.Success)
					throw new FormatException($"The text '{str}' is not a valid value specification.");

				var typeName = m.Groups["t"].Value;
				RegistryValueType valueType;
				RegistryValueEncoding enc;
				if (Enum.TryParse(typeName, true, out valueType))
				{
					enc = valueType switch
					{
						RegistryValueType.String => throw new NotImplementedException(),
						RegistryValueType.ExpandString => throw new NotImplementedException(),
						RegistryValueType.MultiString => throw new NotImplementedException(),
						RegistryValueType.DwordLE or
						RegistryValueType.DwordBE => RegistryValueEncoding.Dword,
						RegistryValueType.Qword => RegistryValueEncoding.Qword,
						RegistryValueType.None or
						RegistryValueType.Binary or
						_ => RegistryValueEncoding.Binary
					};
				}
				else if (
					(typeName.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && uint.TryParse(typeName.Substring(2), NumberStyles.HexNumber, null, out uint intType))
					|| uint.TryParse(typeName, out intType))
				{
					valueType = (RegistryValueType)intType;
					enc = RegistryValueEncoding.Binary;
				}
				else
					throw new FormatException($"The type '{typeName}' is not a supported registry value type.");

				var valueName = m.Groups["n"].Value;
				valueName = StringHelper.UnescapeCStyle(valueName);
				return valueName;
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
	}

	[Command]
	[Description("Sets one or more values in a registry key")]
	internal class SetValueCommand : RegistryKeyCommand
	{

		[Parameter(20)]
		[Description("Values to set")]
		//[Mandatory]
		public RegistryValueSpec[] Values { get; set; }

		[Parameter]
		[Description("Use backup semantics")]
		public SwitchParam Backup { get; set; }

		protected override RegistryAccessRights RequiredKeyAccess => RegistryAccessRights.SetValue;

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();

			return 0;
		}
	}
}
