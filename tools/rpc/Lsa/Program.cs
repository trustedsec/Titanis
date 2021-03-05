using System.ComponentModel;
using Titanis.Cli;

namespace Lsa;

[Description("Commands for interacting with the LSA")]
[Subcommand("lookupsid", typeof(LookupSidCommand))]
[Subcommand("lookupname", typeof(LookupNameCommand))]
[Subcommand("whoami", typeof(WhoamiCommand))]
[Subcommand("enumaccounts", typeof(EnumAccountsCommand))]
[Subcommand("enumprivaccounts", typeof(EnumPrivAccountsCommand))]
[Subcommand("createaccount", typeof(CreateAccountCommand))]
[Subcommand("getprivs", typeof(GetAccountPrivilegesCommand))]
[Subcommand("getrights", typeof(GetAccountRightsCommand))]
[Subcommand("getsysaccess", typeof(GetSystemAccessRightsCommand))]
[Subcommand("setsysaccess", typeof(SetSystemAccessRightsCommand))]
[Subcommand("addpriv", typeof(AddPrivCommand))]
[Subcommand("rmpriv", typeof(RemovePrivCommand))]
internal class Program : MultiCommand
{
	static int Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
