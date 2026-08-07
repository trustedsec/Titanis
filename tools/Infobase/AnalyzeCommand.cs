using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Info;

namespace Infobase;

[Command]
[Description("Analyzes attack paths")]
[OutputRecordType(typeof(Item))]
public class AnalyzeCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("Infobase file name")]
	public FileSpec InfoBase { get; set; }

	protected override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		string idbFileName = this.ResolveFsPath(this.InfoBase);
		this.WriteDiagnostic($"Opening infobase {idbFileName}");
		InfoBase idb = await Titanis.Info.InfoBase.OpenAsync(idbFileName, cancellationToken);

		var items = await idb.GetItems("Titanis.Ldap.LdapEntry", 0, cancellationToken);
		this.WriteRecords(items);

		return 0;
	}
}
