using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using Titanis;
using Titanis.Cli;
using Titanis.Msrpc.Mswmi;
using Titanis.Security;
using Titanis.Smb2;
using Titanis.Winterop;

namespace Wmi;

/// <task category="WMI;Lateral Movement">Execute a command line on a remote system</task>
[Command]
[Description("Executes a command on a remote system via WMI")]
[DetailedHelpText(@"This command uses WMI Win32_Process.Create to execute a command line, optionally capturing the output and waiting for the executed program to exit.

Both -CaptureOutput and -CmdCall are enabled by default.  To disable them, specify -CaptureOutput:off or -CmdCall:off

Use -PollInterval to specify the polling interval for checking output as well as the Win32_ProcessTrace query.  Specify the value as a number followed by one of [ ms, s, m, h ] specifying the unit.

To specify environment variables for the started process, specify -EnvironmentVariables followed by a list of <name>=<value> pairs, separated by commas.  For example, to specify two variables named VAR1 and VAR2: `-EnvironmentVariables VAR1=value1, VAR2=value2`

-CaptureOutput redirects STDOUT and STDERR to a file using the redirection provided by CMD.EXE and therefore requires -CmdCall as well.  {0} generates a file name using a new GUID and creates this file in `C:\Windows\Temp` using SMB.  It periodically checks the file for updates using the interval specified by -PollInterval.  Any updates are fetched and printed to STDOUT.

While the command is running, {0} uses Win32_ProcessTrace to monitor the started process and its child processes.  Once the root process of the tree exits, {0} exits, returning the exit status returned by the remote process.

Use Ctrl+C to terminate the remote process.  When -CmdCall is enabled, the first child process is terminated (that isn't named `conhost.exe`).
")]
[Example("Running a simple command", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-DC1 -Verbose SystemInfo.exe")]
[Example("Specifying an environment variable", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-DC1 -Verbose ""ECHO %MYVAR%"" -EnvironmentVariables MYVAR=me")]
[Example("Specifying a polling interval", @"{0} -UserName milchick -Password Br3@kr00m! LUMON-DC1 -PollInterval 100ms -Verbose ""PING -t localhost""")]
internal class ExecCommand : WmiCommand
{
	[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
	public SmbParameters SmbParameters { get; set; }

	[Parameter(10)]
	[Mandatory]
	[Description("Command line to execute")]
	public string CommandLine { get; set; }

	[Parameter]
	[Description("Redirects STDOUR and STDERR to a file")]
	[DefaultValue(true)]
	public SwitchParam CaptureOutput { get; set; }

	[Parameter]
	[Description("Sets the working directory for the new process")]
	public string? WorkingDir { get; set; }

	[Parameter]
	[Description("Prepends 'cmd /q /c' to the command")]
	[DefaultValue(true)]
	public SwitchParam CmdCall { get; set; }

	[Parameter]
	[Description("Waits for the command to complete")]
	[DefaultValue(true)]
	public SwitchParam Wait { get; set; }

	[Parameter]
	[Description("Polling interval")]
	[DefaultValue("1s")]
	public Duration PollInterval { get; set; }

	[Parameter]
	[Description("Environment variables to pass to the command")]
	public string[]? EnvironmentVariables { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		this.SmbParameters?.Validate(context, this.Authentication);

		if (this.CaptureOutput.IsSet)
		{
			if (!this.CmdCall.IsSet)
				context.LogError("-CaptureOutput requires -CmdCall to be set");

			//if (this.SmbParameters != null)
			//	context.LogError("The command line is missing SMB parameters.  SMB parameters are required for -CaptureOutput.");
			//else
		}
	}

	private async Task ReadOutput(Smb2OpenFile outFile, TimeSpan pollInterval, CancellationToken cancellationToken)
	{
		try
		{
			var fileStream = outFile.GetStream(false);
			const int BufferSize = 1024;
			byte[] buffer = new byte[BufferSize];
			while (!cancellationToken.IsCancellationRequested)
			{
				var newSize = outFile.AllocationSize;
				if (newSize > fileStream.Position)
				{
					var cbRead = await fileStream.ReadAsync(buffer, 0, BufferSize);
					if (cbRead > 0)
					{
						string output = Encoding.UTF8.GetString(buffer.AsSpan().Slice(0, cbRead));
						this.Context.Terminal.WriteOutput(output);
					}
				}

				while (!cancellationToken.IsCancellationRequested && fileStream.Position >= outFile.Length)
				{
					await Task.Delay(pollInterval);
					await outFile.RefreshFileSizeAsync(cancellationToken);
				}
			}
		}
		catch (OperationCanceledException)
		{

		}
	}

	protected sealed override async Task<int> RunAsync(WmiClient wmi, CancellationToken cancellationToken)
	{
		var ns = await wmi.OpenNamespace(WmiClient.RootCimV2Namespace, "en-US", cancellationToken);

		var processClass = (WmiClassObject)await ns.GetObjectAsync("Win32_Process", cancellationToken);
		var processStartupClass = (WmiClassObject)await ns.GetObjectAsync("Win32_ProcessStartup", cancellationToken);

		var cmdLine = this.CommandLine;
		var workingDir = this.WorkingDir;

		if (this.CmdCall.IsSet)
		{
			cmdLine = "cmd.exe /q /c " + cmdLine;
		}

		var pollInterval = this.PollInterval.TimeSpan;

		uint exitStatus = 0;

		string? tempFileName = null;
		UncPath? tempFilePath = null;
		Smb2Client? smbClient = null;
		Smb2OpenFile? outFile = null;
		Task? readOutputTask = null;
		CancellationTokenSource readCancel = new CancellationTokenSource();
		if (this.CaptureOutput.IsSet)
		{
			if (!this.CmdCall.IsSet)
				throw new InvalidOperationException("Cannot redirect output without -CmdCall");

			tempFileName = Guid.NewGuid().ToString();
			this.WriteVerbose($"Writing output to temp file {tempFileName} on remote system");

			cmdLine += " 2>&1 >C:\\WINDOWS\\TEMP\\" + tempFileName;

			smbClient = this.SmbParameters.CreateClient();

			tempFilePath = new UncPath(this.ServerName, 445, "ADMIN$", $"Temp\\{tempFileName}");
			outFile = (Smb2OpenFile)await smbClient.CreateFileAsync(tempFilePath, new Smb2CreateInfo
			{
				CreateDisposition = Smb2CreateDisposition.Supersede,
				DesiredAccess = (uint)(Smb2FileAccessRights.GenericRead),
				//DesiredAccess = (uint)(Smb2FileAccessRights.DefaultCreateAccess | Smb2FileAccessRights.Delete),
				ShareAccess = Smb2ShareAccess.ReadWrite,
				FileAttributes = Titanis.Winterop.FileAttributes.Normal,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint
				//CreateOptions = Smb2FileCreateOptions.NonDirectory | Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint | Smb2FileCreateOptions.DeleteOnClose
			}, FileAccess.Read, cancellationToken);

			readOutputTask = ReadOutput(outFile, pollInterval, readCancel.Token);
		}

		try
		{
			var startupInfo = processStartupClass.Instantiate();
			const uint CREATE_NO_WINDOW = 0x08000000U;
			// UNDONE: Doesn't work; causes ERROR_NOT_READY
			//startupInfo["CreateFlags"] = CREATE_NO_WINDOW;

			if (this.EnvironmentVariables != null)
				startupInfo["EnvironmentVariables"] = this.EnvironmentVariables;

			dynamic dynProcessClass = processClass;
			uint pid = 0;
			uint childPid = 0;

			var procTrace = await ns.ExecuteWqlNotificationQueryAsync("SELECT * FROM Win32_ProcessTrace", 1, cancellationToken);
			CancellationTokenSource cancelTrace = new CancellationTokenSource();
			// Wait for CMD to exit
			Task traceTask = Task.Factory.StartNew(async () =>
			{
				while (await procTrace.ReadAsync(pollInterval, cancelTrace.Token))
				{
					var obj = (WmiInstanceObject)procTrace.Current;
					if (obj.WmiClass.Name == "Win32_ProcessStartTrace" && ((uint)obj["ParentProcessId"] == pid))
					{
						var startedPid = (uint)obj["ProcessId"];
						var procName = (string)obj["ProcessName"];
						this.WriteMessage($"Started subprocess [{procName}] with PID = {startedPid}");

						if (this.CmdCall.IsSet && !procName.Equals("conhost.exe", StringComparison.OrdinalIgnoreCase))
							childPid = startedPid;
					}
					else if (obj.WmiClass.Name == "Win32_ProcessStopTrace" && ((uint)obj["ProcessId"] == pid))
					{
						exitStatus = (uint)obj["ExitStatus"];
						this.WriteMessage($"Process exited with status = {exitStatus}");
						break;
					}
				}
			}).Unwrap();

			try
			{
				await this.ExecuteFrameAsync(async cx =>
				{
					var result = await (
						(workingDir == null) ? dynProcessClass.Create(
							CommandLine: cmdLine,
							ProcessStartupInformation: startupInfo
							)
						: dynProcessClass.Create(
							CommandLine: cmdLine,
							CurrentDirectory: workingDir,
							ProcessStartupInformation: startupInfo
							)
						);

					var returnValue = (Win32ErrorCode)(int)result.ReturnValue;
					if (returnValue == 0)
						pid = result.ProcessId;

					if (returnValue != Win32ErrorCode.ERROR_SUCCESS)
					{
						this.WriteError($"The remote process creation failed with error {returnValue}: {returnValue.GetErrorMessage()}");
						cancelTrace.Cancel();
					}
					else
					{
						this.WriteMessage($"Started process with PID = {pid}");

						// Wait until either CMD exits or the user presses Ctrl+C
						await traceTask.WaitAsync(cx);
					}
				});
			}
			catch (OperationCanceledException ex)
			{
				cancelTrace.Cancel();

				var workerPid = (childPid != 0) ? childPid : pid;
				if (workerPid != 0)
				{
					// Kill the process
					this.WriteMessage($"Terminating process {workerPid}...");
					try
					{
						var proc = await ns.ExecuteWqlQuerySingleAsync($"SELECT * FROM Win32_Process WHERE ProcessId = {workerPid}", cancellationToken);
						if (proc != null)
						{
							dynamic procObj = proc;
							var terminateRes = await procObj.Terminate(0U);
							var returnValue = (Win32ErrorCode)terminateRes.ReturnValue;
							returnValue.CheckAndThrow();
						}
					}
					catch (Exception ex2)
					{
						this.WriteError($"Failed to terminate process: {ex2.Message}");
					}
				}
			}

			// Wait for root process to exit
			await traceTask.WaitAsync(cancellationToken);
		}
		finally
		{
			// Finish reading output
			readCancel.Cancel();
			if (readOutputTask != null)
				try
				{
					await readOutputTask;
				}
				catch { }

			if (outFile != null)
				await outFile.CloseAsync(cancellationToken);
			if (tempFilePath != null)
			{
				this.WriteVerbose($"Deleting temp file {tempFileName}");
				try
				{
					await smbClient.DeleteFileAsync(tempFilePath, cancellationToken);
				}
				catch (Exception ex)
				{
					this.WriteError($"Failed to delete temp file: {ex.Message}");
				}
			}
		}

		return (int)exitStatus;
	}
}