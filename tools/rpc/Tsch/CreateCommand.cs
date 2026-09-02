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
[Example("Create a task from XML file", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -XmlFile task.xml")]
[Example("Create a task with inline command", "{0} ecorp-dc -UserName veeam-admin@ecorp.local -Password B@ckupP@ssw0rd -TaskPath \\\\MyTask -Command cmd.exe -Arguments \"/c whoami > C:\\\\out.txt\"")]
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
			xml = BuildExecXml(Command, Arguments);
		}
		else
		{
			this.WriteError("Either -XmlFile or -Command must be specified");
			return 1;
		}

		TaskCreationFlags flags = Update.IsSet ? TaskCreationFlags.CreateOrUpdate : TaskCreationFlags.Create;

		var actualPath = await client.RegisterTask(
			TaskPath, xml, flags, null, TaskLogonType.S4U,
			cancellationToken);

		this.WriteMessage($"Task registered: {actualPath}");
		return 0;
	}

	private static string BuildExecXml(string command, string? arguments)
	{
		string argsElement = string.IsNullOrEmpty(arguments)
			? ""
			: $"\r\n        <Arguments>{EscapeXml(arguments)}</Arguments>";

		return
$@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.2"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <RegistrationInfo>
    <Description>Titanis scheduled task</Description>
  </RegistrationInfo>
  <Triggers>
    <TimeTrigger>
      <StartBoundary>2099-01-01T00:00:00</StartBoundary>
      <Enabled>true</Enabled>
    </TimeTrigger>
  </Triggers>
  <Principals>
    <Principal id=""Author"">
      <UserId>S-1-5-18</UserId>
      <RunLevel>HighestAvailable</RunLevel>
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

	private static string EscapeXml(string s)
	{
		return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
	}
}
