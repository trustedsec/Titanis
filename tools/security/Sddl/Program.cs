using System.ComponentModel;
using Titanis.Cli;

[Subcommand("describe", typeof(DescribeCommand))]
[Description("Works with security descriptors represented in SDDL")]
internal class Program : MultiCommand
{
	private static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}