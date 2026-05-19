using System.ComponentModel;

namespace Titanis.Cli.LdapTool
{
	[Description("Performs LDAP operations")]
	[Subcommand("search", typeof(SearchCommand))]
	[Subcommand("query", typeof(QueryCommand))]
	[Subcommand("watch", typeof(WatchCommand))]
	[Subcommand("schema", typeof(SchemaCommand))]
	[Subcommand("listsyntax", typeof(ListSyntaxCommand))]
	[Subcommand("namedbits", typeof(NamedBitsCommand))]
	[Subcommand("add", typeof(AddCommand))]
	[Subcommand("addou", typeof(AddOuCommand))]
	[Subcommand("adduser", typeof(AddUserCommand))]
	[Subcommand("addcomputer", typeof(AddComputerCommand))]
	[Subcommand("mod", typeof(ModCommand))]
	[Subcommand("moduser", typeof(ModUserCommand))]
	[Subcommand("whoami", typeof(WhoamiCommand))]
	[Subcommand("lspart", typeof(ListPartitionsCommand))]
	[Subcommand("timestamp", typeof(TimestampCommand))]
	[Subcommand("mountfs", typeof(FuseCommand))]
	[Subcommand("rm", typeof(DeleteCommand))]
	internal partial class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}
}
