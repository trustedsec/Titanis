using System.ComponentModel;
using Titanis.Cli;
using Titanis.Cli.SddlTool;

[Subcommand("describe", typeof(DescribeCommand))]
[Subcommand("lookupguid", typeof(LookupGuidCommand))]
[Subcommand("lookupwks", typeof(LookupWksCommand))]
[Description("Works with security descriptors represented in SDDL")]
internal class Program : MultiCommand
{
	private static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}