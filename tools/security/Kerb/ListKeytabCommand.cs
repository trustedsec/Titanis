using System.ComponentModel;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb;

[Command]
[Description("Lists the entries in a keytab")]
internal class ListKeytabCommand : Command
{
	[Parameter(0)]
	[Description("Name of keytab file")]
	public string FileName { get; set; }

	protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		Keytab keytab = Keytab.LoadFrom(this.FileName);

		throw new NotImplementedException();
	}
}