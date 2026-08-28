using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Msraa;
using Titanis.Winterop.Security;

namespace Titanis.Cli.Raza;

public class RemoteAuthorizationInfo
{
	public SecurityIdentifier? UserSid { get; set; }
	public SecurityIdentifier[] GroupSids { get; set; }
	public SecurityIdentifier[] RestrictedGroupSids { get; set; }
	public SecurityIdentifier[] DeviceGroupSids { get; set; }
	public ClaimsAttributeValue[] UserClaims { get; set; }
	public ClaimsAttributeValue[] DeviceClaims { get; set; }
}

public record struct ClaimsAttributeValue(string Name, object Value, ClaimAttributeOptions Options)
{
	public override string ToString() => $"[{this.Name}]={this.Value}";
}

[Command]
[Description("Gets remote authorization, such as groups and claims")]
[OutputRecordType(typeof(RemoteAuthorizationInfo), DefaultOutputStyle = OutputStyle.List)]
public class GetInfoCommand : RemoteAuthzCommand
{
	protected override async Task RunAsync(RemoteAuthorizationContext context, RemoteAuthorizationClient client, CancellationToken cancellationToken)
	{
		RemoteAuthorizationInfo info = new RemoteAuthorizationInfo();
		try { info.UserSid = await context.GetUserSid(cancellationToken); } catch { }
		try { info.GroupSids = await context.GetGroups(cancellationToken); } catch { }
		try { info.DeviceGroupSids = await context.GetDeviceSids(cancellationToken); } catch { }
		try { info.RestrictedGroupSids = await context.GetRestrictedGroups(cancellationToken); } catch { }
		try { info.UserClaims = Flatten(await context.GetUserClaims(cancellationToken)); } catch { }
		try { info.DeviceClaims = Flatten(await context.GetDeviceClaims(cancellationToken)); } catch { }

		this.WriteRecord(info);
	}

	private ClaimsAttributeValue[] Flatten(ClaimsAttribute[] attrs)
	{
		return attrs.SelectMany(r => r.Values, (a, v) => new ClaimsAttributeValue(a.Name, v, a.Options)).ToArray();
	}
}
