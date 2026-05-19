using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Titanis.Logging.SourceGeneration
{
	[Generator]
	public class LogGenerator : IIncrementalGenerator
	{
		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
			var schemaFiles = context.AdditionalTextsProvider.Where(r => r.Path.EndsWith("logschema.xml", StringComparison.OrdinalIgnoreCase));

			context.RegisterSourceOutput(schemaFiles, GenerateLogSchema);
		}

		private static readonly XmlSerializer _ser = new XmlSerializer(typeof(Xml.LogSchemaXml));

		private void GenerateLogSchema(SourceProductionContext context, AdditionalText source)
		{
			try
			{
				this.GenerateLogSchema2(context, source);
			}
			catch (Exception ex)
			{
				context.AddSource("LogSchemaError.cs", ex.ToString());
			}

		}

		private void GenerateLogSchema2(SourceProductionContext context, AdditionalText source)
		{
			var text = source.GetText()?.ToString();
			var logSchema = (Xml.LogSchemaXml)_ser.Deserialize(new StringReader(text));

			foreach (var logSource in logSchema.Sources ?? [])
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendLine("using System;")
					.AppendLine("using Titanis;")
					;

				if (logSource.Imports != null)
				{
					foreach (var import in logSource.Imports)
					{
						sb.AppendLine($"using {import.Namespace};");
					}
				}

				sb
					.AppendLine()
					.AppendLine($"namespace {logSource.Namespace};")
					.AppendLine()
					;

				sb.AppendLine($"public enum {logSource.Name}MessageId")
					.AppendLine("{")
					;

				foreach (var message in logSource.Messages)
				{
					sb.AppendLine($"\t{message.Name},");

					//string[] paramNames = (message.Parameters != null) ? Array.ConvertAll(message.Parameters, r => r.Name) : [];
					//try
					//{
					//	LogMessageType msgtype = new LogMessageType(LogMessageSeverity.Info, "loggen", 0, message.Format, paramNames);
					//}
					//catch (Exception ex)
					//{
					//	throw new Exception($"Error while processing message named '{message.Name}': {ex.Message}");
					//}
				}

				sb
				.AppendLine("}")
				.AppendLine()
				;

				sb
					.AppendLine($"public static class {logSource.Name}LogSchema")
					.AppendLine("{")
					.AppendLine($"\tpublic const string {logSource.Name}Name = \"{logSource.Name}\";");
				;

				foreach (var message in logSource.Messages)
				{
					var format = message.Format ?? string.Empty;

					sb.Append($"\tinternal static readonly LogMessageType {message.Name} = new LogMessageType(LogMessageSeverity.{message.Severity}, {logSource.Name}Name, \"{message.Name}\", (int){logSource.Name}MessageId.{message.Name}, \"{format.Escape(false)}\"");

					if (message.Parameters != null)
					{
						foreach (var param in message.Parameters)
						{
							sb.Append($", \"{param.Name}\"");
						}
					}

					sb.AppendLine(");");

					sb.Append($"\tpublic static void Write{logSource.Name}{message.Name}Message(this ILog log");
					if (message.Parameters != null)
					{
						foreach (var param in message.Parameters)
						{
							sb.Append($", {param.Type} {param.Name}");
						}
					}
					sb.AppendLine(") {")
						.Append($"\t\t\tlog.WriteMessage({message.Name}.CreateWithText($\"{format.Escape(true)}\"");
					if (message.Parameters != null)
					{
						foreach (var param in message.Parameters)
						{
							sb.Append($", {param.Name}");
						}
					}
					sb.AppendLine("));");
					sb.AppendLine("\t}").AppendLine();
				}
				sb.AppendLine("}");

				string generated = sb.ToString();
				context.AddSource($"{logSource.Name}LogSchema.cs", sb.ToString());
			}
		}
	}

	static class StringExtensions
	{
		public static string Escape(this string str, bool interpolated)
		{
			int level = 0;
			StringBuilder sb = new StringBuilder();
			foreach (var c in str)
			{
				var escaped = c switch
				{
					'\n' => 'n',
					'\r' => 'r',
					'\\' => '\\',
					'\t' => 't',
					'"' => (level == 0) ? '"' : '\0',
					_ => '\0'
				};
				if (escaped != '\0')
				{
					sb.Append('\\').Append(escaped);
				}
				else
					sb.Append(c);

				if (interpolated)
				{
					if (c == '{')
						level++;
					else if (c == '}')
						level--;
				}
			}
			return sb.ToString();
		}
	}
}
