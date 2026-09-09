using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mstsch.Xml
{

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/windows/2004/02/mit/task")]
	public enum logonType
	{
		// Redefined from schema to match [MS-TSCH]
		None = 0,
		Password = 1,
		// Case sensitive
		S4U = 2,
		Interactive = 3,
		Group = 4,
		ServiceAccount = 5,
		InteractiveTokenOrPassword = 6,
	}

	abstract partial class triggerBaseType
	{
		internal abstract TaskTrigger CreateTrigger();
	}

	sealed partial class calendarTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new CalendarTrigger(this);
	}

	public abstract class calendarScheduleType
	{
		internal abstract CalendarSchedule CreateSchedule();
	}
	sealed partial class dailyScheduleType : calendarScheduleType
	{
		internal sealed override CalendarSchedule CreateSchedule() => new DailySchedule(this);
	}
	sealed partial class monthlyScheduleType : calendarScheduleType
	{
		internal sealed override CalendarSchedule CreateSchedule() => new MonthlySchedule(this);
	}

	sealed partial class monthlyDayOfWeekScheduleType : calendarScheduleType
	{
		internal sealed override CalendarSchedule CreateSchedule() => new MonthlyDayOfWeekSchedule(this);
	}

	sealed partial class weeklyScheduleType : calendarScheduleType
	{
		internal sealed override CalendarSchedule CreateSchedule() => new WeeklySchedule(this);
	}

	sealed partial class sessionStateChangeTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new SessionStateChangeTrigger(this);
	}

	sealed partial class logonTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new LogonTrigger(this);
	}

	sealed partial class eventTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new EventTrigger(this);
	}

	sealed partial class timeTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new TimeTrigger(this);
	}

	sealed partial class idleTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new IdleTrigger(this);
	}

	sealed partial class registrationTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new RegistrationTrigger(this);
	}

	sealed partial class bootTriggerType
	{
		internal sealed override TaskTrigger CreateTrigger() => new BootTrigger(this);
	}



	abstract partial class actionBaseType
	{
		public sealed override string ToString() => this.ToTaskString();
		protected abstract string ToTaskString();
		internal abstract TaskAction CreateAction();
	}



	partial class showMessageType
	{
		protected override string ToTaskString()
		{
			return $"Show message: {this.Title} - {this.Body}";
		}

		internal override TaskAction CreateAction() => new ShowMessageAction(this);
	}

	partial class sendEmailType
	{
		protected override string ToTaskString()
		{
			return $"Send email: To: {this.toField}, Subject: {this.subjectField}";
		}

		internal override TaskAction CreateAction() => new SendEmailAction(this);
	}

	partial class comHandlerType
	{
		protected override string ToTaskString()
		{
			return $"COM Handler: {this.classIdField}";
		}

		internal override TaskAction CreateAction() => new ComHandlerAction(this);
	}

	partial class execType
	{
		protected override string ToTaskString()
		{
			return $"Exec: {this.commandField} {this.argumentsField}";
		}

		internal override TaskAction CreateAction() => new ExecuteAction(this);
	}
}
