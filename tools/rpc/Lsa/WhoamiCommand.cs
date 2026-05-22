using System.ComponentModel;
using Titanis.Msrpc.Mslsar;
using Titanis.Security;

namespace Titanis.Cli.LsaTool;

/// <task category="LSA">Get the name of the current user account</task>
[Command]
[OutputRecordType(typeof(UserPrincipalName), DefaultOutputStyle = OutputStyle.Freeform)]
[Description("Gets the name and domain of the connected user")]
[Example("Get connected user name (NTLM)", "{0} LUMON-FS1 -UserName milchick -Password Br3@kr00m!", Tag = "Ntlm_UserNamePassword")]
[Example("Get connected user name (Kerberos)", "{0} LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1", Tag = "Kerberos_UserNamePassword")]
[Example("Get connected user name (Kerberos S4U2proxy)", "{0} LUMON-FS1 -UserName allentown$@LUMON -Password password -S4UserName ColdHarbor -S4ProxyService host/allentown -Kdc LUMON-DC1", Tag = "Kerberos_S4U")]
[Example("Get connected user name (Kerberos S4U2proxy with NTLM hash)", "{0} LUMON-FS1 -UserName allentown$@LUMON -NtlmHash 8846F7EAEE8FB117AD06BDD830B7586C -S4UserName ColdHarbor -S4ProxyService host/allentown -Kdc LUMON-DC1", Tag = "Kerberos_S4U_NtlmHash")]
[Example("Get connected user name (Interrealm referral)", "{0} B5X-DC1.branch5x.lumon.ind -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 -PreferSmb -EncryptRpc", Tag = "Kerberos_Interrealm")]
public class WhoamiCommand : LsaCommand
{
	protected sealed override async Task<int> RunAsync(LsaClient client, CancellationToken cancellationToken)
	{
		var name = await client.WhoAmI(cancellationToken);
		this.WriteRecord(name);

		return 0;
	}
}