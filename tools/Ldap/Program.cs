using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Net;

namespace Ldap
{
	[Description("Performs LDAP operations")]
	[Subcommand("search", typeof(SearchCommand))]
	[Subcommand("query", typeof(QueryCommand))]
	[Subcommand("watch", typeof(WatchCommand))]
	[Subcommand("schema", typeof(SchemaCommand))]
	[Subcommand("listsyntax", typeof(ListSyntaxCommand))]
	[Subcommand("namedbits", typeof(NamedBitsCommand))]
	[Subcommand("addou", typeof(AddOuCommand))]
	[Subcommand("adduser", typeof(AddUserCommand))]
	[Subcommand("addcomputer", typeof(AddComputerCommand))]
	[Subcommand("moduser", typeof(ModUserCommand))]
	internal partial class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}
}
