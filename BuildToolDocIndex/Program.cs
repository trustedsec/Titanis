using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Xml;
using Titanis.Cli;

namespace BuildToolDocIndex;

internal class Program : Command
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);

	[Parameter]
	[Mandatory]
	public string DocPath { get; set; }

	[Parameter]
	[Mandatory]
	public string ToolsDir { get; set; }

	[Parameter]
	[Mandatory]
	public string Configuration { get; set; }

	class Entry
	{
		public string CommandName { get; set; }
		public Type ImplementingType { get; set; }
	}

	class NameEntry
	{
		public string Command { get; set; }
		public string Description { get; set; }
	}

	class TaskEntry
	{
		public string? Category { get; set; }
		public string Task { get; set; }
		public string Command { get; set; }
	}

	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var toolDirs = Directory.GetDirectories(ToolsDir);
		var conf = this.Configuration.ToLower();

		List<NameEntry> nameEntries = new List<NameEntry>();
		List<TaskEntry> tasks = new List<TaskEntry>();
		Queue<Entry> commandEntries = new Queue<Entry>();


		foreach (var toolDir in toolDirs)
		{
			var toolName = Path.GetFileName(toolDir);
			var toolPath = Path.Combine(toolDir, conf, toolName + ".dll");
			if (File.Exists(toolPath))
			{
				Assembly asm = Assembly.LoadFrom(toolPath);
				if (asm.EntryPoint == null)
					continue;

				var progClass = asm.EntryPoint.DeclaringType;

				if (typeof(CommandBase).IsAssignableFrom(progClass))
				{
					var xmlFileName = Path.ChangeExtension(toolPath, ".xml");
					XmlDocument? doc = null;
					XmlNamespaceManager? nsm = null;
					if (File.Exists(xmlFileName))
					{
						doc = new XmlDocument();
						doc.Load(xmlFileName);

						nsm = new XmlNamespaceManager(doc.NameTable);
						//nsm.AddNamespace("doc", string.Empty);
					}

					commandEntries.Enqueue(new Entry { CommandName = toolName, ImplementingType = progClass });
					while (commandEntries.Count > 0)
					{
						var entry = commandEntries.Dequeue();

						var descr = entry.ImplementingType.GetCustomAttribute<DescriptionAttribute>();
						nameEntries.Add(new NameEntry { Command = entry.CommandName, Description = descr?.Description });

						{
							string xmlKey = "T:" + entry.ImplementingType.FullName;
							var taskNodes = doc.SelectNodes($"/doc/members/member[@name='{xmlKey}']/task");
							foreach (XmlElement taskNode in taskNodes)
							{
								var cat = taskNode.Attributes["category"]?.Value?.Split(';') ?? new string[] { null };
								var text = taskNode.InnerText;

								tasks.AddRange(cat.Select(r => new TaskEntry { Category = r, Task = text, Command = entry.CommandName }));
							}
						}

						if (typeof(MultiCommand).IsAssignableFrom(entry.ImplementingType))
						{
							var subs = entry.ImplementingType.GetCustomAttributes<SubcommandAttribute>();
							foreach (var sub in subs)
							{
								commandEntries.Enqueue(new Entry { CommandName = entry.CommandName + ' ' + sub.Name, ImplementingType = sub.CommandType });
							}
						}
					}
				}
			}
		}

		var templateText = File.ReadAllText("template.md");

		nameEntries.Sort((x, y) => x.Command.CompareTo(y.Command));
		{
			StringBuilder sb = new StringBuilder();
			foreach (var nameEntry in nameEntries)
			{
				string linkTarget = CommandNameToLink(nameEntry.Command);

				sb.AppendLine($"|[{nameEntry.Command}]({linkTarget})|{nameEntry.Description}|");
			}

			templateText = templateText.Replace("{ToolsByName}", sb.ToString());
		}

		{
			StringBuilder sb = new StringBuilder();
			var taskCategories = tasks.GroupBy(r => r.Category).OrderBy(r => r.Key);
			foreach (var cat in taskCategories)
			{
				sb.AppendLine($"|**{cat.Key}**|");

				var tasksInCat = cat.OrderBy(r => r.Task);
				foreach (var task in tasksInCat)
				{
					var linkTarget = CommandNameToLink(task.Command);
					sb.AppendLine($"|{task.Task}|[{task.Command}]({linkTarget})|");
				}
			}

			templateText = templateText.Replace("{ToolsByTask}", sb.ToString());
		}

		File.WriteAllText(Path.Combine(this.DocPath, "index.md"), templateText);

		return 0;
	}

	private static string CommandNameToLink(string command)
	{
		var tokens = command.Split(' ');
		var linkTarget = tokens[0] + ".md";
		if (tokens.Length > 0)
			linkTarget += "#" + string.Join("-", tokens.Select(r => r.ToLower()));
		return linkTarget;
	}
}
