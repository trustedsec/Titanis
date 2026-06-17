using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Cli.Registry;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <task category="Registry;Enumeration">List the contents of a registry key</task>
	/// <task category="Registry;Enumeration">Search for specific registry keys, values, or data</task>
	[Command]
	[Description("Lists the contents of a key")]
	[OutputRecordType(typeof(RegistryItem))]
	[OutputFieldFormat(nameof(RegistryItem.Name), RegistryItemNameFormatter.DefaultIfEmptyFormat, typeof(RegistryItemNameFormatter))]
	[Example("Lists loaded user hives backup operator", "{0} -UserName marks@LUMON -Kdc 10.66.0.11 -Password She'sAlive!! LUMON-FS1 -BackupSemantics HKU")]
	[Example(@"Query all values and direct subkeys of HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\MyApp")]
	[Example(@"Query the value names 'InstallPath' and 'Version' under HKLM\Software\MyApp", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\MyApp -ValueNameFilter InstallPath, Version")]
	[Example(@"Finds all non-empty default value under HKLM\Software\Microsoft", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software\Microsoft -QueryDefaultValue -Recursive ")]
	[Example(@"Search for any value name or data item containing the string 'password' or 'credential' under HKLM\Software", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-FS1 HKLM\Software -ValueSearch -DataSearch -SearchPatterns password, credential -Recursive")]
	internal class QueryCommand : QueryCommandBase
	{
		protected override void OnKeyMatch(RegistryPath keyPath) => this.WriteRecord(new RegistryItem(keyPath));

		protected override void OnValueMatch(RegistryPath keyPath, RegistryValueInfo value) => this.WriteRecord(new RegistryItem(keyPath.KeyPath, value));
	}
}
