using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Info;

namespace Infobase;

public abstract class InfobaseCommand : Command
{
	[Mandatory]
	public override FileSpec? LogBase { get => base.LogBase; set => base.LogBase = value; }

	protected abstract Task<int> RunAsync(InfoBase infobase, CancellationToken cancellationToken);
	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		string idbFileName = this.ResolveFsPath(this.LogBase);
		this.WriteDiagnostic($"Opening infobase {idbFileName}");
		InfoBase idb = await Titanis.Info.InfoBase.OpenAsync(idbFileName, cancellationToken);
		return await this.RunAsync(idb, cancellationToken);
	}
}
