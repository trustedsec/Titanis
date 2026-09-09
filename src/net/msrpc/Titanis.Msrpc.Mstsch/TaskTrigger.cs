using Titanis.Msrpc.Mstsch.Xml;

namespace Titanis.Msrpc.Mstsch
{
	public abstract class TaskTrigger
	{
		protected abstract string ToTaskString();
		public sealed override string ToString() => this.ToTaskString();

		internal abstract Xml.triggerBaseType XmlElement { get; }

		public DateTime? StartBoundary { get => this.GetNullable(this.XmlElement.startBoundaryField, this.XmlElement.startBoundaryFieldSpecified); set => this.SetNullable(ref this.XmlElement.startBoundaryField, ref this.XmlElement.startBoundaryFieldSpecified, value); }
		public DateTime? EndBoundary { get => this.GetNullable(this.XmlElement.endBoundaryField, this.XmlElement.endBoundaryFieldSpecified); set => this.SetNullable(ref this.XmlElement.endBoundaryField, ref this.XmlElement.endBoundaryFieldSpecified, value); }
		public TimeSpan ExecutionTimeLimit { get => TaskXmlExtensions.ParseXmlDuration(this.XmlElement.ExecutionTimeLimit); set => this.XmlElement.ExecutionTimeLimit = value.ToXmlDuration(); }
		public bool IsEnabled { get => this.XmlElement.Enabled; set => this.XmlElement.Enabled = value; }
	}

	public sealed class CalendarTrigger : TaskTrigger
	{
		private Xml.calendarTriggerType xml;

		internal CalendarTrigger(Xml.calendarTriggerType xml)
		{
			this.xml = xml;
			this.Schedule = (xml.Item as Xml.calendarScheduleType)?.CreateSchedule();
		}

		public CalendarSchedule? Schedule { get; }
		internal sealed override triggerBaseType XmlElement => this.xml;

		protected sealed override string ToTaskString() => this.Schedule?.ToString() ?? "(calendar)";
	}

	public abstract class CalendarSchedule
	{

	}

	public sealed class DailySchedule : CalendarSchedule
	{
		private Xml.dailyScheduleType xml;

		internal DailySchedule(Xml.dailyScheduleType xml)
		{
			this.xml = xml;
		}
	}

	public sealed class MonthlySchedule : CalendarSchedule
	{
		private Xml.monthlyScheduleType xml;

		internal MonthlySchedule(Xml.monthlyScheduleType xml)
		{
			this.xml = xml;
		}
	}

	public sealed class MonthlyDayOfWeekSchedule : CalendarSchedule
	{
		private Xml.monthlyDayOfWeekScheduleType xml;

		internal MonthlyDayOfWeekSchedule(Xml.monthlyDayOfWeekScheduleType xml)
		{
			this.xml = xml;
		}
	}

	public sealed class WeeklySchedule : CalendarSchedule
	{
		private Xml.weeklyScheduleType xml;

		internal WeeklySchedule(Xml.weeklyScheduleType xml)
		{
			this.xml = xml;
		}
	}



	public sealed class SessionStateChangeTrigger : TaskTrigger
	{
		private Xml.sessionStateChangeTriggerType xml;

		public SessionStateChangeTrigger(sessionStateChangeType stateChange, string? userId, TimeSpan delay)
			: this(new Xml.sessionStateChangeTriggerType
			{
				StateChange = stateChange,
				UserId = userId,
				Delay = delay.ToXmlDuration(),
			})
		{
		}

		internal SessionStateChangeTrigger(Xml.sessionStateChangeTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => $"Session state change: {this.xml.StateChange} for user {this.xml.UserId ?? "<any>"}";
	}

	public sealed class LogonTrigger : TaskTrigger
	{
		private Xml.logonTriggerType xml;

		public LogonTrigger(string? logonUser, TimeSpan delay)
			: this(new Xml.logonTriggerType
			{
				UserId = logonUser,
				Delay = delay.ToXmlDuration()
			})
		{
		}

		internal LogonTrigger(Xml.logonTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => $"User logon: User {this.xml.UserId}";
	}

	public sealed class EventTrigger : TaskTrigger
	{
		private Xml.eventTriggerType xml;

		internal EventTrigger(Xml.eventTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => "Event trigger";
	}

	public sealed class TimeTrigger : TaskTrigger
	{
		private Xml.timeTriggerType xml;

		public TimeTrigger(TimeSpan randomDelay)
			: this(new timeTriggerType()
			{
				RandomDelay = randomDelay.ToXmlDuration()
			})
		{
		}

		internal TimeTrigger(Xml.timeTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => "Time trigger";
	}

	public sealed class IdleTrigger : TaskTrigger
	{
		private Xml.idleTriggerType xml;

		public IdleTrigger()
			: this(new idleTriggerType())
		{
		}
		internal IdleTrigger(Xml.idleTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => "Idle";
	}

	public sealed class RegistrationTrigger : TaskTrigger
	{
		private Xml.registrationTriggerType xml;

		internal RegistrationTrigger(Xml.registrationTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => "Registration";
	}

	public sealed class BootTrigger : TaskTrigger
	{
		private Xml.bootTriggerType xml;

		public BootTrigger(TimeSpan delay)
			: this(new bootTriggerType()
			{
				Delay = delay.ToXmlDuration()
			})
		{
		}

		internal BootTrigger(Xml.bootTriggerType xml)
		{
			this.xml = xml;
		}
		internal sealed override triggerBaseType XmlElement => this.xml;
		protected override string ToTaskString() => "Boot";
	}
}