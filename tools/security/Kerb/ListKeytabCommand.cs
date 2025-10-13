using System.ComponentModel;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb;

[Command]
[Description("Lists the entries in a keytab")]
internal class ListKeytabCommand : Command
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(0)]
	[Mandatory]
	[Description("Name of keytab file")]
	public string FileName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		Keytab keytab = Keytab.LoadFrom(this.FileName);

		throw new NotImplementedException();
	}
}