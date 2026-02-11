using System.ComponentModel;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

[Command]
[Description("Prints the bits with symbolic names")]
[OutputRecordType(typeof(NamedBit))]
internal class NamedBitsCommand : Command
{
	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var all = NamedBitGroups.AllGroups.SelectMany(r => r.NamedBits, (g, r) => new { g, r }).Select(r => new NamedBit(r.g.AttributeName, r.r.Key, r.r.Value, $"0x{r.r.Value:X8}"));
		this.WriteRecords(all);

		return Task.FromResult<int>(0);
	}

	record class NamedBit(string Attribute, string Name, ulong Value, string HexValue)
	{

	}
}
