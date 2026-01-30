using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Titanis.Certificates;
using Titanis.Cli;

namespace Cert;

[Subcommand("selfcert", typeof(SelfCertCommand))]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}