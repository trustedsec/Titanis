using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Titanis.Msrpc.Mstsch.Xml;

namespace Titanis.Msrpc.Mstsch
{
	public class TaskDefinition : IWantServerName
	{
		private readonly taskType? taskXml;

		internal TaskDefinition(
			string? name,
			string? folderPath,
			string xml,
			Xml.taskType? taskXml
			)
		{
			this.Name = name;
			this.FolderPath = folderPath;
			this._xml = xml;
			this.taskXml = taskXml;
		}

		public static TaskDefinition FromXml(string xml)
		{
			ArgumentNullException.ThrowIfNull(xml);
			return new TaskDefinition("name", "path", xml, null);
		}

		public static TaskDefinition Define(
			TaskTrigger[]? triggers,
			string principal,
			TaskLogonType logonType,
			Xml.runLevelType runLevel,
			TaskSettings settings,
			TaskAction[] actions,
			string? author = null,
			string? description = null
			)
		{
			ArgumentNullException.ThrowIfNull(actions);
			settings ??= new TaskSettings(new settingsType()
			{
			});

			triggers ??= [];
			//triggers ??= [new CalendarTrigger(new Xml.calendarTriggerType {
			//	Repetition = new repetitionType{
			//		Interval = TimeSpan.FromHours(1).ToXmlDuration(),
			//		Duration = TimeSpan.FromDays(1).ToXmlDuration(),
			//		StopAtDurationEnd = true
			//	},
			//	StartBoundary = DateTime.UtcNow,
			//	StartBoundarySpecified = true,
			//	EndBoundary=DateTime.UtcNow+TimeSpan.FromDays(1),
			//	EndBoundarySpecified=true,
			//	ExecutionTimeLimit=TimeSpan.FromDays(3).ToXmlDuration(),
			//	Enabled=true,
			//	RandomDelay=TimeSpan.FromHours(1).ToXmlDuration(),
			//	Item = new Xml.weeklyScheduleType {
			//		DaysOfWeek= new daysOfWeekType{
			//			Monday = new object(),
			//			Tuesday=new object(),
			//		},
			//		WeeksInterval=1,
			//		WeeksIntervalSpecified=true
			//	}
			//})];

			DateTime createdTime = DateTime.UtcNow;
			var taskXml = new Xml.taskType
			{
				RegistrationInfo = new registrationInfoType
				{
					Date = createdTime,
					DateSpecified = true,
					Author = author,
					Description = description
				},
				Triggers = (triggers.Length == 0) ? null : new triggersType
				{
					Items = Array.ConvertAll(triggers, r => r.XmlElement)
				},
				Principals = new principalsType()
				{
					Principal = new principalType
					{
						UserId = principal,
						LogonType = (Xml.logonType)logonType,
						LogonTypeSpecified = true,
						RunLevel = runLevel,
						RunLevelSpecified = true,
					}
				},
				Settings = settings.xml,
				Actions = BuildActionsFor(actions)
			};

			return new TaskDefinition(null, null, null, taskXml);
		}

		class ActionXmlCollector : ITaskActionVisitor
		{
			public void Visit(ShowMessageAction action)
			{
				throw new NotImplementedException();
			}

			public void Visit(SendEmailAction action)
			{
				throw new NotImplementedException();
			}

			public void Visit(ComHandlerAction action)
			{
				throw new NotImplementedException();
			}

			public void Visit(ExecuteAction action)
			{
				throw new NotImplementedException();
			}
		}

		private static actionsType BuildActionsFor(TaskAction[] actions)
		{
			return new actionsType
			{
				Items = Array.ConvertAll(actions, r => r.ActionXml)
			};
		}

		public string? ServerName { get; set; }
		public string? Name { get; }
		public string? FolderPath { get; }

		private string? _xml;
		public string GetXml() => (this._xml ??= this.BuildXml());

		private string BuildXml()
		{
			StringWriter writer = new StringWriter();
			TaskSchedulerClient.serTask.Serialize(writer, this.taskXml);
			return writer.ToString();
		}

		public TaskAction[] Actions => this.GetActions();
		private TaskAction[] GetActions()
		{
			var actionsXml = this.taskXml?.Actions;
			if (actionsXml != null)
			{
				List<TaskAction> actions = new List<TaskAction>();

				foreach (var item in actionsXml.Items)
				{
					if (item != null)
						actions.Add(item.CreateAction());
				}


				// UNDONE: This is what the actual schema would use, but it doesn't preserve order
				//var sources = new Xml.actionBaseType[][] {
				//	actionsXml.Exec,
				//	actionsXml.ComHandler,
				//	actionsXml.SendEmail,
				//	actionsXml.ShowMessage
				//};
				//foreach (var actionType in sources)
				//{
				//	if (actionType != null)
				//		actions.AddRange(actionType.Select(r => r.CreateAction()));
				//}
				return actions.ToArray();
			}
			else
				return [];
		}

		public TaskTrigger[] Triggers => this.GetTriggers();
		private TaskTrigger[] GetTriggers()
		{
			var triggersXml = this.taskXml?.Triggers?.Items;
			if (triggersXml != null)
			{
				var triggers = new List<TaskTrigger>();
				if (triggersXml != null)
				{
					foreach (var triggerXml in triggersXml)
					{
						if (triggerXml != null)
							triggers.Add(triggerXml.CreateTrigger());
					}
				}
				return triggers.ToArray();
			}
			else
			{
				return [];
			}
		}
	}
}
