using Titanis.Msrpc.Mstsch.Xml;

namespace Titanis.Msrpc.Mstsch
{
	public class TaskSettings
	{
		internal Xml.settingsType xml;

		internal TaskSettings(Xml.settingsType xml)
		{
			this.xml = xml;
			this.xml.IdleSettings ??= new idleSettingsType();
			this.xml.RestartOnFailure ??= new restartType()
			{
				Interval = "PT1M",
				Count = 1
			};
		}

		public TaskSettings()
			: this(new settingsType())
		{

		}

		private static T? GetNullable<T>(T xmlValue, bool xmlSpecified)
			where T : struct
		{
			return (xmlSpecified) ? xmlValue : default(T?);
		}

		private static void SetNullable<T>(ref T xmlValue, ref bool xmlSpecified, T? value)
			where T : struct
		{
			xmlSpecified = value.HasValue;
			xmlValue = value ?? default;
		}

		public multipleInstancesPolicyType MultipleInstancesPolicy { get => this.xml.MultipleInstancesPolicy; set => this.xml.MultipleInstancesPolicy = value; }
		public bool DisallowStartIfOnBatteries { get => this.xml.DisallowStartIfOnBatteries; set => this.xml.DisallowStartIfOnBatteries = value; }
		public bool StopIfGoingOnBatteries { get => this.xml.StopIfGoingOnBatteries; set => this.xml.StopIfGoingOnBatteries = value; }
		public bool AllowHardTerminatenBatteries { get => this.xml.AllowHardTerminate; set => this.xml.AllowHardTerminate = value; }
		public bool StartWhenAvailable { get => this.xml.StartWhenAvailable; set => this.xml.StartWhenAvailable = value; }
		public string? NetworkProfileName { get => this.xml.NetworkProfileName; set => this.xml.NetworkProfileName = value; }
		public bool RunOnlyIfNetworkAvailable { get => this.xml.RunOnlyIfNetworkAvailable; set => this.xml.RunOnlyIfNetworkAvailable = value; }

		public TimeSpan? IdleDuration { get => TaskXmlExtensions.ParseXmlDurationNullable(this.xml.IdleSettings.Duration); set => this.xml.IdleSettings.Duration = value.ToXmlDuration(); }
		public TimeSpan? IdleWaitTimeout { get => TaskXmlExtensions.ParseXmlDurationNullable(this.xml.IdleSettings.WaitTimeout); set => this.xml.IdleSettings.WaitTimeout = value.ToXmlDuration(); }
		public bool RestartOnIdle { get => this.xml.IdleSettings.RestartOnIdle; set => this.xml.IdleSettings.RestartOnIdle = value; }


		public bool AllowStartOnDemand { get => this.xml.AllowStartOnDemand; set => this.xml.AllowStartOnDemand = value; }
		public bool Enabled { get => this.xml.Enabled; set => this.xml.Enabled = value; }
		public bool Hidden { get => this.xml.Hidden; set => this.xml.Hidden = value; }
		public bool RunOnlyIfIdle { get => this.xml.RunOnlyIfIdle; set => this.xml.RunOnlyIfIdle = value; }
		public bool DisallowStartOnRemoteAppSession { get => this.xml.DisallowStartOnRemoteAppSession; set => this.xml.DisallowStartOnRemoteAppSession = value; }
		public bool UseUnifiedSchedulingEngine { get => this.xml.UseUnifiedSchedulingEngine; set => this.xml.UseUnifiedSchedulingEngine = value; }
		public bool WakeToRun { get => this.xml.WakeToRun; set => this.xml.WakeToRun = value; }
		public TimeSpan? ExecutionTimeLimit { get => TaskXmlExtensions.ParseXmlDurationNullable(this.xml.ExecutionTimeLimit); set => this.xml.ExecutionTimeLimit = value.ToXmlDuration(); }
		public TimeSpan? DeleteExpiredTaskAfter { get => TaskXmlExtensions.ParseXmlDurationNullable(this.xml.DeleteExpiredTaskAfter); set => this.xml.DeleteExpiredTaskAfter = value.ToXmlDuration(); }
		public sbyte Priority { get => this.xml.Priority; set => this.xml.Priority = value; }

		public TimeSpan? RestartOnFailureInterval { get => TaskXmlExtensions.ParseXmlDurationNullable(this.xml.RestartOnFailure.Interval); set => this.xml.RestartOnFailure.Interval = value.ToXmlDuration(); }
		public byte RestartOnFailureCount { get => this.xml.RestartOnFailure.Count; set => this.xml.RestartOnFailure.Count = value; }
	}
}
