using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Security.Kerberos;
using Titanis.Winterop;
using Titanis.Winterop.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wmi.Registry
{
	[Description("Add or modify a registry key or value")]
	[Example("Add a registry key without any values under HKLM\\Software\\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp")]
	[Example("Add a string value 'InstallPath' with data 'C:\\Program Files\\MyApp' under HKLM\\Software\\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -ValueName InstallPath -StringData ""C:\Program Files\MyApp""")]
	[Example("Add a multi string value 'Servers' with data 'server1', 'server2', 'server3' under HKLM\\Software\\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -ValueName Servers -MultiStringData server1, server2, ""My favorite server""")]
	[Example("Add a binary value 'BinaryData' with hex data 'DE AD BE EF 01 02 03' under HKLM\\Software\\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp -ValueName BinaryData -BinaryData DEADBEEF010203")]
	internal class RegistryAddCommand : WmiRegistryCommandBase
	{
		[Parameter]
		[Description("Name of value")]
		public string? ValueName { get; set; }

		[Parameter]
		[Description("Default key value")]
		public SwitchParam ValueEmpty { get; set; }

		[Parameter]
		[Description("REG_SZ data to set")]
		public string? StringData { get; set; }

		[Parameter]
		[Description("REG_EXPAND_SZ data to set")]
		public string? StringExpandData { get; set; }

		[Parameter]
		[Description("REG_BINARY data, as a hex string")]
		public string? BinaryData { get; set; }

		[Parameter]
		[Description("REG_DWORD data to set (hex or decimal)")]
		public uint? DwordData { get; set; }

		[Parameter]
		[Description("REG_MULTI_SZ data to set")]
		public string[]? MultiStringData { get; set; }

		[Parameter]
		[Description("REG_DWORD data to set (hex or decimal)")]
		public ulong? QwordData { get; set; }

		[Parameter]
		[Description("Overwrite the value if it already exists")]
		public SwitchParam Overwrite { get; set; }


		

		private RegistryData? data;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (ValueEmpty.IsSet && !string.IsNullOrEmpty(ValueName))
			{
				context.LogError($"The -{nameof(ValueEmpty)} is mutually exclusive with -{nameof(ValueName)}.");
			}
			if(ValueEmpty.IsSet)
			{
				ValueName = string.Empty;
			}
			int dataValuesSpecified = 0;
			if(StringData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateString(StringData);
			}
			if(StringExpandData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateExpandableString(StringExpandData);
			}
			if (BinaryData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateBinary(Convert.FromHexString(BinaryData));
			}
			if(DwordData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateDword(DwordData.Value);
			}
			if(MultiStringData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateRegMultiString(MultiStringData);
			}
			if(QwordData is not null)
			{
				dataValuesSpecified++;
				data = RegistryData.CreateDword(QwordData.Value);
			}
			if(dataValuesSpecified > 1)
			{
				context.LogError("Only one of the -*Data parameters may be specified.");
			}
			if (ValueName is not null && data is null)
			{
				context.LogError($"-{nameof(ValueName)} or -{nameof(ValueEmpty)} must be used with one of the -*Data parameters");
			}

		}

		protected override async Task<int> RunAsync(dynamic registry, CancellationToken cancellationToken)
		{
			var rootAsUint = (uint)KeyPath.Root;

			bool keyExists = (await registry.CheckAccess(rootAsUint, KeyPath.KeyPath, (uint)RegistryAccessRights.KeySetValue)).bGranted;
			if(!keyExists)
			{
				this.WriteDiagnostic($"Creating registry key '{KeyPath}'");
				((Win32ErrorCode)(await registry.CreateKey(rootAsUint, KeyPath.KeyPath)).ReturnValue).CheckAndThrow();
				this.WriteMessage($"Registry key '{KeyPath}' created.");
			}
			else
			{
				this.WriteMessage($"Registry key '{KeyPath}' already exists.");
			}
			if (ValueName is not null)
			{
				var regValues = (await registry.EnumValues(rootAsUint, KeyPath.KeyPath));
				((Win32ErrorCode)regValues.ReturnValue).CheckAndThrow();
				var values = (((Array?)regValues.sNames)?.OfType<string>()) ?? Array.Empty<string>();
				bool valueExists = values.Any(v => v.Equals(ValueName, StringComparison.OrdinalIgnoreCase));
				if (valueExists)
				{
					if (Overwrite.IsSet)
					{
						this.WriteVerbose($"Overwriting existing registry value '{ValueDisplayName(ValueName)}'");
					}
					else
					{
						this.WriteMessage($"Registry value '{KeyPath} : {ValueDisplayName(ValueName)}' already exists, use -{nameof(Overwrite)} to modify it.");
						return 1;
					}
				}
				
				this.WriteDiagnostic($"Creating registry value '{KeyPath} : {ValueDisplayName(ValueName)}'");
				Debug.Assert(data is not null);
				((Win32ErrorCode)(data switch
				{
					RegistryString t => (await registry.SetStringValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Value)).ReturnValue,
					RegistryExpandableString t => (await registry.SetExpandedStringValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Value)).ReturnValue,
					RegistryBinary t => (await registry.SetBinaryValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Bytes)).ReturnValue,
					RegistryDword t => (await registry.SetDWORDValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Value)).ReturnValue,
					RegistryMultiString t => (await registry.SetMultiStringValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Strings)).ReturnValue,
					RegistryQword t => (await registry.SetQWORDValue(rootAsUint, KeyPath.KeyPath, ValueName, t.Value)).ReturnValue,
					_ => throw new InvalidProgramException("Validation should prevent this error")
				}
				)).CheckAndThrow();
				this.WriteMessage($"Registry value '{KeyPath} : {ValueDisplayName(ValueName)}' set to {data}");
			}
			return 0;
		}
	}
}
