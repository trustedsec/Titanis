using System.ComponentModel;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;

[Command]
[Description("Lists the entries in a keytab")]
[OutputRecordType(typeof(KeytabEntry))]
internal class ListKeytabCommand : Command
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	[Parameter(0, EnvironmentVariable = "DEFCKTNAME")]
	[Alias("kt")]
	[Mandatory]
	[Description("Name of keytab file")]
	public string Keytab { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		this.WriteDiagnostic($"Loading keytab file '{this.Keytab}'");
		KeytabFile keytab = KeytabFile.LoadFrom(this.Keytab);

		foreach (var entry in keytab.Entries)
		{
			this.WriteRecord(entry);
		}

		return 0;
	}
}