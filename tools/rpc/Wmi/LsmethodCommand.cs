using ms_wmi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;

namespace Wmi;

/// <task category="WMI;Enumeration">List the methods of a WMI class or object</task>
[Command]
[Description("Lists the methods of a class or object.")]
[OutputRecordType(typeof(WmiMethod), DefaultFields = new string[] { nameof(WmiMethod.Name), nameof(WmiMethod.Signature), nameof(WmiMethod.ShortDescription) })]
[DetailedHelpText(@"You may specify multiple object paths.  Each object path may be a class or an instance.

Use -WithQualifiers to filter by one or more qualifiers.  Each entry may either be a qualifier name or a name-value pair of the form <name>=<value>.  If only a name is specified, the filter matches if the qualifier is present with a value other than 'false'.  If the <name>=<value> syntax is used, the qualifier value must match using a case-insensitive string comparison.  If the qualifier has multiple values, only one value must match.")]
[Example("List the methods of the Win32_Process class", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 Win32_Process")]
[Example("List only the static methods of the Win32_Process class", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers static Win32_Process")]
[Example("List the methods of the Win32_Process class that require the SeDebugPrivilege", "{0} -namespace root\\cimv2 -UserName milchick -Password Br3@kr00m! LUMON-FS1 -WithQualifiers Privileges=SeDebugPrivilege Win32_Process")]
internal class LsmethodCommand : WmiNamespaceCommandBase
{
	[Parameter(10)]
	[Mandatory]
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
					var methods = cls.Methods;
					if (methods != null)
					{
						foreach (var method in methods)
						{
							if (FilterQualifiers(method.Qualifiers))
								this.WriteRecord(method);
						}
					}
				}
				else if (obj is WmiInstanceObject inst)
				{
					var methods = inst.WmiClass.Methods;

					if (methods != null)
					{
						foreach (var method in methods)
						{
							if (FilterQualifiers(method.Qualifiers))
								this.WriteRecord(method);
						}
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
