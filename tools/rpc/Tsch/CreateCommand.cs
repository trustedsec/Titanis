using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool;

/// <task category="TSCH;Lateral Movement">Create or update a scheduled task</task>
[Command]
[Description("Creates or updates a scheduled task from XML")]
[Example("Create a task running as SYSTEM", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -Command cmd.exe -Arguments \"/c whoami > C:\\\\out.txt\"")]
[Example("Create a task as a user (runs in their session)", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -Command cmd.exe -RunAs ECORP\\\\someuser")]
[Example("Create a task that fires on user logon", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -Command cmd.exe -RunAs ECORP\\\\someuser -OnLogon")]
public class CreateCommand : TschCommand
{
	[Parameter]
	[Mandatory]
	[Description("Full path for the task (e.g. \\MyTask)")]
	public string TaskPath { get; set; }

	[Parameter]
	[Description("Path to an XML file containing the task definition")]
	public string? XmlFile { get; set; }

	[Parameter]
	[Description("Command to execute (generates a simple XML definition)")]
	public string? Command { get; set; }

	[Parameter]
	[Description("Arguments for the command")]
	public string? Arguments { get; set; }

	[Parameter]
	[Description("Run task as this user (default: SYSTEM). Uses InteractiveToken logon.")]
	public string? RunAs { get; set; }

	[Parameter]
	[Description("Logon type: InteractiveToken (default with -RunAs), Password (batch, no desktop), S4U")]
	public string? RunAsLogon { get; set; }

	[Parameter]
	[Description("Trigger on user logon instead of on-demand only")]
	public SwitchParam OnLogon { get; set; }

	[Parameter]
	[Description("Update existing task if it already exists")]
	public SwitchParam Update { get; set; }

	protected sealed override async Task<int> RunAsync(TschClient client, CancellationToken cancellationToken)
	{
		string xml;
		if (!string.IsNullOrEmpty(XmlFile))
		{
			xml = File.ReadAllText(XmlFile);
		}
		else if (!string.IsNullOrEmpty(Command))
		{
			xml = BuildExecXml(Command, Arguments, RunAs, RunAsLogon, OnLogon.IsSet);
		}
		else
		{
			this.WriteError("Either -XmlFile or -Command must be specified");
			return 1;
		}

		TaskLogonType logonType = ResolveLogonType(RunAsLogon, RunAs);
		TaskCreationFlags flags = Update.IsSet ? TaskCreationFlags.CreateOrUpdate : TaskCreationFlags.Create;

		var actualPath = await client.RegisterTask(
			TaskPath, xml, flags, null, logonType,
			cancellationToken);

		this.WriteMessage($"Task registered: {actualPath}");
		return 0;
	}

	private static TaskLogonType ResolveLogonType(string? logonStr, string? runAs)
	{
		if (!string.IsNullOrEmpty(logonStr))
		{
			return logonStr.ToLowerInvariant() switch
			{
				"password" => TaskLogonType.Password,
				"s4u" => TaskLogonType.S4U,
				"interactivetoken" or "interactive" => TaskLogonType.InteractiveToken,
				_ => TaskLogonType.None
			};
		}
		if (string.IsNullOrEmpty(runAs) || runAs == "S-1-5-18" || runAs!.Equals("SYSTEM", StringComparison.OrdinalIgnoreCase))
			return TaskLogonType.S4U;
		return TaskLogonType.None;
	}

	private static string BuildExecXml(string command, string? arguments, string? runAs, string? logonType, bool onLogon)
	{
		string argsElement = string.IsNullOrEmpty(arguments)
			? ""
			: $"\r\n        <Arguments>{EscapeXml(arguments)}</Arguments>";

		string userId = string.IsNullOrEmpty(runAs) ? "S-1-5-18" : EscapeXml(runAs);
		bool isSystem = string.IsNullOrEmpty(runAs) || runAs == "S-1-5-18"
			|| runAs!.Equals("SYSTEM", StringComparison.OrdinalIgnoreCase);

		string xmlLogonType = ResolveXmlLogonType(logonType, runAs);
		string logonElement = string.IsNullOrEmpty(xmlLogonType)
			? ""
			: $"\r\n      <LogonType>{xmlLogonType}</LogonType>";
		string runLevel = "\r\n      <RunLevel>HighestAvailable</RunLevel>";

		string triggers;
		if (onLogon && !isSystem)
		{
			triggers =
$@"  <Triggers>
    <LogonTrigger>
      <UserId>{userId}</UserId>
      <Enabled>true</Enabled>
    </LogonTrigger>
  </Triggers>";
		}
		else if (!isSystem && !onLogon)
		{
			triggers =
@"  <Triggers>
    <RegistrationTrigger>
      <Enabled>true</Enabled>
    </RegistrationTrigger>
  </Triggers>";
		}
		else
		{
			triggers =
@"  <Triggers>
    <TimeTrigger>
      <StartBoundary>2099-01-01T00:00:00</StartBoundary>
      <Enabled>true</Enabled>
    </TimeTrigger>
  </Triggers>";
		}

		return
$@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.2"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <RegistrationInfo>
    <Description>Titanis scheduled task</Description>
  </RegistrationInfo>
{triggers}
  <Principals>
    <Principal id=""Author"">
      <UserId>{userId}</UserId>{logonElement}{runLevel}
    </Principal>
  </Principals>
  <Settings>
    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
    <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
    <AllowHardTerminate>true</AllowHardTerminate>
    <AllowStartOnDemand>true</AllowStartOnDemand>
    <Enabled>true</Enabled>
    <Hidden>false</Hidden>
  </Settings>
  <Actions Context=""Author"">
    <Exec>
      <Command>{EscapeXml(command)}</Command>{argsElement}
    </Exec>
  </Actions>
</Task>";
	}

	private static string ResolveXmlLogonType(string? logonStr, string? runAs)
	{
		if (!string.IsNullOrEmpty(logonStr))
		{
			return logonStr.ToLowerInvariant() switch
			{
				"password" => "Password",
				"s4u" => "S4U",
				"interactivetoken" or "interactive" => "InteractiveToken",
				_ => ""
			};
		}
		return "";
	}

	private static string EscapeXml(string s)
	{
		return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
	}
}
