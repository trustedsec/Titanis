using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.Security;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli;

public class DcomAppInfo
{
	public Guid AppId { get; set; }
	public string? Title { get; set; }
	public SecurityDescriptor? LaunchPermissions { get; set; }
	public SecurityDescriptor? AccessPermissions { get; set; }
	public bool IsDllSurrogate { get; set; }
	public string? LocalService { get; set; }
	public string? ServiceParameters { get; set; }
	public string? RunAs { get; set; }
	public uint PreferredServerBitness { get; set; }
	public RpcAuthLevel AuthenticationLevel { get; set; }

}

	/// <task category="Registry;Enumeration">Get a list of registered DCOM applications</task>
[Command]
[Description("Gets information about a DCOM application")]
[OutputRecordType(typeof(DcomAppInfo))]
internal class GetDcomAppCommand : RegistryCommand
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("AppID(s) of app(s)")]
	public Guid[] AppId { get; set; }

	private async Task<T?> TryGetValue<T>(RegistryKey key, string? valueName, CancellationToken cancellationToken)
	{
		try
		{
			var value = await key.GetValue(valueName, cancellationToken);
			if (value is null)
				return default;

			if ((value.TypedValue ?? value.Bytes) is T typed)
				return typed;
			else
				return default;
		}
		catch (Exception ex)
		{
			return default;
		}
	}

	private SecurityDescriptor? SdFromBytes(byte[]? bytes)
	{
		if (bytes == null)
			return null;

		try
		{
			return new SecurityDescriptor(bytes);
		}
		catch (Exception ex)
		{
			this.WriteWarning($"Error parsing permissions: " + ex.Message);
			return null;
		}
	}

	protected override async Task<int> RunAsync(RemoteRegistryClient client, CancellationToken cancellationToken)
	{
		var options = this.KeyOptions;
		var hklm = await client.OpenLocalMachine(Winterop.Security.RegistryAccessRights.QueryValue, cancellationToken);
		await using (hklm)
		{
			var hkAppId = await hklm.OpenSubkey($@"SOFTWARE\Classes\AppID", RegistryAccessRights.QueryValue, options, cancellationToken);
			await using (hkAppId)
			{
				foreach (var appId in this.AppId)
				{
					var hkApp = await hkAppId.OpenSubkey(appId.ToString("B"), RegistryAccessRights.QueryValue, options, cancellationToken);

					var launchPerms = SdFromBytes(await this.TryGetValue<byte[]>(hkApp, "LaunchPermission", cancellationToken));
					var accessPerms = SdFromBytes(await this.TryGetValue<byte[]>(hkApp, "AccessPermission", cancellationToken));
					var info = new DcomAppInfo
					{
						AppId = appId,
						Title = await this.TryGetValue<string>(hkApp, null, cancellationToken),
						LaunchPermissions = launchPerms,
						AccessPermissions = accessPerms,
						IsDllSurrogate = (await this.TryGetValue<string>(hkApp, "DllSurrogate", cancellationToken) != null),
						LocalService = await this.TryGetValue<string>(hkApp, "LocalService", cancellationToken),
						ServiceParameters = await this.TryGetValue<string>(hkApp, "ServiceParameters", cancellationToken),
						RunAs = await this.TryGetValue<string>(hkApp, "RunAs", cancellationToken),
						PreferredServerBitness = await this.TryGetValue<uint>(hkApp, "PreferredBitness", cancellationToken),
						AuthenticationLevel = (RpcAuthLevel)await this.TryGetValue<uint>(hkApp, "AuthenticationLevel", cancellationToken),
					};
					this.WriteRecord(info);
				}
			}

			return 0;
		}
	}
}
