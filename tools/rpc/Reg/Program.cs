using System.ComponentModel;
using ms_rrp;
using Titanis.Cli;
using Titanis.Winterop.Registry;

namespace Titanis.Msrpc.Msrrp.Cli
{
	[Subcommand("set", typeof(SetValueCommand))]
	[Subcommand("list", typeof(ListCommand))]
	[Subcommand("save", typeof(SaveKeyCommand))]
	[Subcommand("keyinfo", typeof(KeyInfoCommand))]
	[Subcommand("syskey", typeof(SyskeyCommand))]
	[Subcommand("dumpsam", typeof(DumpSamCommand))]
	[Subcommand("getsd", typeof(GetsdCommand))]
	[Subcommand("setsd", typeof(SetsdCommand))]
	[Subcommand("dumplsasecrets", typeof(DumpLsaSecretsCommand))]
	[Description("Interacts with the registry")]
	internal class Program : MultiCommand
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}

	abstract class RegistryCommand : RpcCommand<RemoteRegistryClient>
	{
		protected override Type InterfaceType => typeof(winreg);

		[Parameter]
		[Description("Open with backup semantics")]
		public SwitchParam BackupSemantics { get; set; }

		protected RegistryKeyOptions KeyOptions => (this.BackupSemantics.IsSet ? RegistryKeyOptions.BackupRestore : RegistryKeyOptions.None);
	}

}
