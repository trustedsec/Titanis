using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;

namespace Wmi.Registry
{
	[OutputRecordType(typeof(RegistryEntry), DefaultFields = new string[]
{
		nameof(RegistryEntry.SubKey),
		nameof(RegistryEntry.ValueName),
		nameof(RegistryEntry.Kind),
		nameof(RegistryEntry.Data),
})]
	[OutputFieldFormat(nameof(RegistryEntry.ValueName), ValueNameFormatter.DefaultIfEmptyFormat, typeof(ValueNameFormatter))]
	[Description("Query registry values")]
	[Example(@"Query all values and direct subkeys of HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! \\LUMON-FS1\HKLM\Software\MyApp")]
	[Example(@"Query the value name 'InstallPath' under HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! -ValueName InstallPath \\LUMON-FS1\HKLM\Software\MyApp")]
	[Example(@"Finds all non-empty default value under HKLM\Software\Microsoft", @"{0} -UserName milchick -Password Br3@kr00m! -ValueEmpty -Recursive \\LUMON-FS1\HKLM\Software\Microsoft")]
	[Example(@"Search for any value name or data item containing the string 'password' under HKLM\Software", @"{0} -UserName milchick -Password Br3@kr00m! -ValueSearch -DataSearch -SearchPattern password -Recursive \\LUMON-FS1\HKLM\Software")]
	internal class RegistryQueryCommand : RegistryQueryCommandBase
	{
		private void WriteRegistryRecord(RegistryEntry entry) => this.WriteRecord(entry);

		protected override void OnKeyMatch(RegistryPath keyPath)
		{
			this.WriteRegistryRecord(new RegistryEntry(keyPath));
		}

		protected override void OnValueMatch(RegistryPath keyPath, string valueName, RegistryValueKind valueKind, RegistryData? valueData)
		{
			var regEntry = new RegistryEntry(keyPath, valueName, valueData);
			this.WriteRegistryRecord(regEntry);
		}
	}
}
