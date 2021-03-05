using System.ComponentModel;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;
using Titanis.Security;

namespace Lsa;

[Command]
[OutputRecordType(typeof(UserPrincipalName), DefaultOutputStyle = OutputStyle.Freeform)]
[Description("Gets the name and domain of the connected user")]
[Example("Get connected user name", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m!")]
internal class WhoamiCommand : LsaCommand
{
	protected sealed override async Task<int> RunAsync(LsaClient client, CancellationToken cancellationToken)
	{
		var name = await client.WhoAmI(cancellationToken);
		this.WriteRecord(name);

		return 0;
	}
}