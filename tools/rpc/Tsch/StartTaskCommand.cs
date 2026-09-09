using System;
using System.ComponentModel;
using System.Linq;
using Titanis;
using Titanis.Cli;
using Titanis.Cli.Tsch;
using Titanis.Msrpc.Mslsar;
using Titanis.Msrpc.Mstsch;
using Titanis.Winterop.Security;

namespace Tsch;

public class StartedTask : IWantServerName
{
	public string? ServerName { get; set; }
	public string TaskPath { get; set; }
	public Guid InstanceGuid { get; set; }
}

[Description("Starts a task")]
[OutputRecordType(typeof(StartedTask), DefaultOutputStyle = OutputStyle.List)]
public class StartTaskCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Path of task")]
	public string[] TaskPath { get; set; }

	[Parameter(After = nameof(TaskPath))]
	[Description("Task start arguments")]
	public string[]? TaskArgs { get; set; }

	[Parameter(After = nameof(TaskPath))]
	[Description("Run task as authenticated user")]
	public SwitchParam RunAsSelf { get; set; }

	[Parameter(After = nameof(TaskPath))]
	[Description("Terminal Server session to run task in")]
	public int? SessionId { get; set; }

	[Parameter(After = nameof(TaskPath))]
	[Description("Runs the task as a specific user (name or SID)")]
	public string? AsUser { get; set; }

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		var options = TaskStartOptions.None;
		if (this.RunAsSelf.IsSet)
			options |= TaskStartOptions.RunAsSelf;
		if (this.SessionId.HasValue)
			options |= TaskStartOptions.UseSessionId;

		SecurityIdentifier? asUser;
		if (this.AsUser != null)
		{
			options |= TaskStartOptions.UserSid;

			if (this.AsUser.StartsWith("S-1-5-"))
				asUser = SecurityIdentifier.Parse(this.AsUser);
			else
			{
				LsaClient lsaClient = new();
				await this.RpcParameters.BindServiceClient(lsaClient, this.CurrentServerName, true, cancellationToken);
				using (var policy = await lsaClient.OpenPolicy(LsaPolicyAccess.LookupNames, cancellationToken))
				{
					var mapping = await policy.ResolveAccountName(this.AsUser, cancellationToken);
					if (mapping.AccountSid != null)
						asUser = mapping.AccountSid;
					else
					{
						throw new Exception($"Failed to resolve {this.AsUser} to a SID.");
					}
				}
			}
		}
		else
		{
			asUser = null;
		}

		foreach (var taskPath_ in this.TaskPath)
		{
			var taskPath = taskPath_;
			taskPath = taskPath_.Replace('/', '\\');
			var instanceGuid = await client.StartTask(
				taskPath,
				this.TaskArgs,
				options,
				this.SessionId ?? 0,
				asUser,
				cancellationToken);
			this.WriteRecord(new StartedTask
			{
				TaskPath = taskPath,
				InstanceGuid = instanceGuid
			});
		}
		return 0;
	}
}
