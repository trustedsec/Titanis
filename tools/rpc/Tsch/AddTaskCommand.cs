using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Titanis.Msrpc.Mstsch;
using Titanis.Msrpc.Mstsch.Xml;

namespace Titanis.Cli.Tsch;

[Command]
[Description("Adds a scheduled task")]
public class AddTaskCommand : TschCommand
{
	[Parameter(After = nameof(ServerName))]
	[Description("Task path")]
	[Category(TaskParameterCategories.Task)]
	public string TaskPath { get; set; }

	[Parameter]
	[Description("Overwrite existing task")]
	[Category(TaskParameterCategories.Task)]
	public SwitchParam Overwrite { get; set; }

	[Parameter]
	[Description("Task author")]
	[Category(TaskParameterCategories.Task)]
	public string? Author { get; set; }

	[Parameter]
	[Description("Task description")]
	[Category(TaskParameterCategories.Task)]
	public string? Description { get; set; }

	[Parameter]
	[Mandatory]
	[Description("Execute command")]
	[Category(TaskParameterCategories.TaskActions)]
	public string? Exec { get; set; }

	[Parameter]
	[Description("Arguments (for -Exec)")]
	[Category(TaskParameterCategories.TaskActions)]
	public string? Args { get; set; }

	[Parameter]
	[Description("Working directory (for -Exec)")]
	[Category(TaskParameterCategories.TaskActions)]
	public string? WorkingDir { get; set; }

	[Parameter]
	[Description("Task logon type")]
	[DefaultValue(TaskLogonType.InteractiveTokenOrPassword)]
	[Category(TaskParameterCategories.TaskPrincipal)]
	public TaskLogonType LogonType { get; set; }

	[Parameter]
	[Mandatory]
	[Description("Run task as principal")]
	[Category(TaskParameterCategories.TaskPrincipal)]
	public string RunAs { get; set; }

	[Parameter]
	[Mandatory]
	[Description("Password for -RunAs principal")]
	[Category(TaskParameterCategories.TaskPrincipal)]
	public string RunAsPassword { get; set; }



	#region Settings
	[Parameter]
	[Description("Allow start on-demand")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(true)]
	public SwitchParam AllowStartOnDemand { get; set; }

	[Parameter]
	[Description("Restart on failure count")]
	[Category(TaskParameterCategories.TaskSettings)]
	public byte? RestartOnFailureCount { get; set; }

	[Parameter]
	[Description("Restart on failure interval")]
	[Category(TaskParameterCategories.TaskSettings)]
	public Duration? RestartOnFailureInterval { get; set; }

	[Parameter]
	[Description("Allow multiple instances")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(multipleInstancesPolicyType.IgnoreNew)]
	public multipleInstancesPolicyType MultipleInstancePolicy { get; set; }

	[Parameter]
	[Description("Disallow start if on battery")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(true)]
	public SwitchParam DisallowStartIfOnBattery { get; set; }

	[Parameter]
	[Description("Stop if going on batteries")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(true)]
	public SwitchParam StopIfGoingOnBatteries { get; set; }

	[Parameter]
	[Description("Allow hard terminate")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(true)]
	public SwitchParam AllowHardTerminate { get; set; }

	[Parameter]
	[Description("Start when available")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam StartWhenAvailable { get; set; }

	[Parameter]
	[Description("Network profile")]
	[Category(TaskParameterCategories.TaskSettings)]
	public string? NetworkProfile { get; set; }

	[Parameter]
	[Description("Run only if network available")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam RunOnlyIfNetworkAvailable { get; set; }

	[Parameter]
	[Description("Wake to run")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam WakeToRun { get; set; }

	[Parameter]
	[Description("Wake to run")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(true)]
	public SwitchParam Enabled { get; set; }

	[Parameter]
	[Description("Hidden")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam Hidden { get; set; }

	[Parameter]
	[Description("Delete expired task after duration")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue("0s")]
	public Duration DeleteExpiredAfter { get; set; }

	[Parameter]
	[Description("Execution time limit")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue("72h")]
	public Duration ExecutionTimeLimit { get; set; }

	[Parameter]
	[Description("Task priority")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue((sbyte)7)]
	public sbyte Priority { get; set; }

	[Parameter]
	[Description("Run only if idle")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam RunOnlyIfIdle { get; set; }

	[Parameter]
	[Description("Use unified scheduling engine")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam UseUnifiedSchedulingEngine { get; set; }

	[Parameter]
	[Description("Disallow start on remote app session")]
	[Category(TaskParameterCategories.TaskSettings)]
	[DefaultValue(false)]
	public SwitchParam DisallowStartOnRemoteAppSession { get; set; }
	#endregion

	#region Triggers

	#region Common

	[Parameter]
	[Description("Trigger start boundary")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public DateTime? TriggerStartBoundary { get; set; }

	[Parameter]
	[Description("Trigger end boundary")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public DateTime? TriggerEndBoundary { get; set; }

	[Parameter]
	[Description("Trigger execution time limit")]
	[Category(TaskParameterCategories.TaskTriggers)]
	[DefaultValue("72h")]
	public Duration TriggerExecutionTimeLimit { get; set; }

	#endregion

	#region Session
	[Parameter]
	[Description("Trigger on session state change")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public sessionStateChangeType? OnSessionStateChange { get; set; }

	[Parameter]
	[Description("User ID of session state trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public string? SessionUser { get; set; }

	[Parameter]
	[Description("Delay after session state trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public Duration? SessionStateDelay { get; set; }
	#endregion

	#region Boot
	[Parameter]
	[Description("Trigger on boot")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public SwitchParam OnBoot { get; set; }

	[Parameter]
	[Description("Delay after boot trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public Duration? BootDelay { get; set; }
	#endregion

	#region Idle
	[Parameter]
	[Description("Trigger on idle")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public SwitchParam OnIdle { get; set; }
	#endregion

	#region Logon
	[Parameter]
	[Description("Trigger on logon")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public SwitchParam OnLogon { get; set; }

	[Parameter]
	[Description("User ID of logon trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public string? LogonUser { get; set; }

	[Parameter]
	[Description("Delay after logon trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public Duration? LogonDelay { get; set; }
	#endregion

	#region Calendar
	[Parameter]
	[Description("Trigger at -TriggerStartBoundary")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public SwitchParam Once { get; set; }

	[Parameter]
	[Description("Random delay for -Once after boot trigger")]
	[Category(TaskParameterCategories.TaskTriggers)]
	public Duration? OnceDelay { get; set; }
	#endregion

	#endregion

	protected override async Task<int> RunAsync(TaskSchedulerClient client, CancellationToken cancellationToken)
	{
		TaskSettings settings = new()
		{
			AllowStartOnDemand = this.AllowStartOnDemand.IsSet,
			MultipleInstancesPolicy = this.MultipleInstancePolicy,
			DisallowStartIfOnBatteries = this.DisallowStartIfOnBattery.IsSet,
			StopIfGoingOnBatteries = this.StopIfGoingOnBatteries.IsSet,
			AllowHardTerminatenBatteries = this.AllowHardTerminate.IsSet,
			StartWhenAvailable = this.StartWhenAvailable.IsSet,
			NetworkProfileName = this.NetworkProfile,
			RunOnlyIfNetworkAvailable = this.RunOnlyIfNetworkAvailable.IsSet,
			WakeToRun = this.WakeToRun.IsSet,
			Enabled = this.Enabled.IsSet,
			Hidden = this.Hidden.IsSet,
			DeleteExpiredTaskAfter = this.DeleteExpiredAfter.TimeSpan,
			ExecutionTimeLimit = this.ExecutionTimeLimit.TimeSpan,
			Priority = this.Priority,
			RunOnlyIfIdle = this.RunOnlyIfIdle.IsSet,
			UseUnifiedSchedulingEngine = this.UseUnifiedSchedulingEngine.IsSet,
			DisallowStartOnRemoteAppSession = this.DisallowStartOnRemoteAppSession.IsSet,
		};

		List<TaskTrigger> triggers = new List<TaskTrigger>();
		if (this.OnSessionStateChange.HasValue)
		{
			var trigger = new SessionStateChangeTrigger(
				this.OnSessionStateChange.Value,
				this.SessionUser,
				this.SessionStateDelay?.TimeSpan ?? default
				);
			triggers.Add(trigger);
		}
		if (this.OnBoot.IsSet)
		{
			var trigger = new BootTrigger(this.BootDelay?.TimeSpan ?? default);
			triggers.Add(trigger);
		}
		if (this.Once.IsSet)
		{
			var trigger = new TimeTrigger(this.OnceDelay?.TimeSpan ?? default);
			triggers.Add(trigger);
		}
		if (this.OnIdle.IsSet)
		{
			var trigger = new IdleTrigger();
			triggers.Add(trigger);
		}
		if (this.OnLogon.IsSet)
		{
			var trigger = new LogonTrigger(
				this.LogonUser,
				this.LogonDelay?.TimeSpan ?? default
				);
			triggers.Add(trigger);
		}

		foreach (var trigger in triggers)
		{
			this.ApplyTriggerSettings(trigger);
		}



		var taskDef = TaskDefinition.Define(
			triggers.ToArray(),
			this.RunAs,
			this.LogonType,
			Msrpc.Mstsch.Xml.runLevelType.LeastPrivilege,
			settings,
			[TaskAction.Execute(this.Exec, this.WorkingDir, this.Args)],
			this.Author,
			this.Description
			);
		await client.CreateTask(
			this.TaskPath,
			taskDef,
			RegisterTaskOptions.Create | RegisterTaskOptions.Overwrite,
			null,
			this.LogonType,
			this.RunAs,
			this.RunAsPassword,
			cancellationToken);
		return 0;
	}

	private void ApplyTriggerSettings(TaskTrigger trigger)
	{
		trigger.StartBoundary = this.TriggerStartBoundary;
		trigger.EndBoundary = this.TriggerEndBoundary;
		trigger.ExecutionTimeLimit = this.ExecutionTimeLimit.TimeSpan;
		trigger.IsEnabled = true;
	}
}
