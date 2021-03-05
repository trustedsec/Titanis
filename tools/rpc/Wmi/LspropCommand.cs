using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI;Enumeration">List the properties of a WMI class or object</task>
[Command]
[Description("Lists the properties of a class or object.")]
[OutputRecordType(typeof(WmiProperty), DefaultFields = new string[] { nameof(WmiProperty.Name), nameof(WmiProperty.ClassOfOrigin), nameof(WmiProperty.PropertyType), nameof(WmiProperty.Subtype), nameof(WmiProperty.ShortDescription) })]
[DetailedHelpText(@"You may specify multiple object paths.  Each object path may be a class or an instance.

Use -WithQualifiers to filter by one or more qualifiers.  Each entry may either be a qualifier name or a name-value pair of the form <name>=<value>.  If only a name is specified, the filter matches if the qualifier is present with a value other than 'false'.  If the <name>=<value> syntax is used, the qualifier value must match using a case-insensitive string comparison.  If the qualifier has multiple values, only one value must match.")]
[Example("List the properties of the Win32_Process class", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 Win32_Process")]
[Example("List the properties of the Win32_Process class that require the SeDebugPrivilege", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers Privileges=SeDebugPrivilege Win32_Process")]
internal class LspropCommand : WmiNamespaceCommandBase
{
	[Parameter(10)]
	[Description("Path of class or object to inspect")]
	public string[] ObjectPath { get; set; }

	protected sealed override async Task<int> RunAsync(WmiScope ns, CancellationToken cancellationToken)
	{
		foreach (var objPath in this.ObjectPath)
		{
			try
			{
				var obj = await ns.GetObjectAsync(objPath, cancellationToken);

				if (obj is WmiClassObject cls)
				{
					var props = cls.Properties;
					foreach (var prop in props)
					{
						if (FilterQualifiers(prop.Qualifiers))
							this.WriteRecord(prop);
					}
				}
				else if (obj is WmiInstanceObject inst)
				{
					var props = inst.Properties;
					foreach (var prop in props)
					{
						if (FilterQualifiers(prop.Qualifiers))
							this.WriteRecord(prop);
					}
				}
				else
				{
					this.WriteError($"Object path `{objPath}' did not return a class object.");
				}
			}
			catch (Exception ex)
			{
				this.WriteMessage(LogMessage.Error(null, ex));
			}
		}

		return 0;
	}
}
