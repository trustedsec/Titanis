using System.ComponentModel;
using System.Text.RegularExpressions;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

/// <task category="LDAP">List named bit flags used by LDAP attributes (offline)</task>
[Command]
[Description("Prints the bits with symbolic names")]
[OutputRecordType(typeof(NamedBit))]
internal class NamedBitsCommand : Command
{
	[Parameter(0)]
	[Description("Attribute(s) to print (default is all)")]
	public string[]? Attribute { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		IEnumerable<NamedBitGroup> groups = NamedBitGroups.AllGroups;
		if (!this.Attribute.IsNullOrEmpty())
		{
			var attrGroups = (
				from a in this.Attribute
				join g in groups on a.ToUpper() equals g.AttributeName.ToUpper() into ag
				select ag
				);
			foreach (var ga in attrGroups)
			{
				if (ga.Count() == 0)
					this.WriteWarning($"Attribute {ga} didn't match any named bit groups.");
			}

			groups = attrGroups.SelectMany(g => g);
		}

		var all = groups.SelectMany(r => r.NamedBits, (g, r) => new { g, r }).Select(r => new NamedBit(r.g.AttributeName, r.r.Key, r.r.Value, $"0x{r.r.Value:X8}"));
		this.WriteRecords(all);

		return Task.FromResult<int>(0);
	}

	record class NamedBit(string Attribute, string Name, ulong Value, string HexValue)
	{

	}
}
