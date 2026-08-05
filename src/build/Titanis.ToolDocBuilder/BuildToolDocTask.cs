using Microsoft.Build.Framework;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Titanis.Cli;

namespace Titanis.ToolDocBuilder
{
	public class BuildToolDocTask : ITask
	{
		public const string SenderName = "Titanis.ToolDocBuilder";

		public IBuildEngine? BuildEngine { get; set; }
		public ITaskHost? HostObject { get; set; }

		[Required]
		public string? AssemblyFile { get; set; }

		[Required]
		public string? DocFile { get; set; }

		[Required]
		public string? BashAutocompFile { get; set; }
		[Required]
		public string? ZshAutocompPath { get; set; }
		[Required]
		public string? ManPagePath { get; set; }

		public bool Execute()
		{
			if (this.BuildEngine is null)
				throw new InvalidOperationException($"This {nameof(BuildToolDocTask)} has not been initialized with a {nameof(BuildEngine)} and cannot execute.");
			if (this.AssemblyFile is null || this.DocFile is null)
				throw new InvalidOperationException($"This {nameof(BuildToolDocTask)} has not been initialized with required parameters {nameof(AssemblyFile)} and {nameof(DocFile)}.");

			Debug.Assert(this.AssemblyFile is not null);
			Debug.Assert(this.DocFile is not null);

			try
			{
				return GenerateDoc(
					this.BuildEngine,
					this.AssemblyFile!,
					this.DocFile!,
					this.BashAutocompFile,
					this.ZshAutocompPath!,
					this.ManPagePath
					);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		public static bool GenerateDoc(
			IBuildEngine buildEngine,
			string assemblyFile,
			string docFile,
			string? bashAutocompFile,
			string? zshAutocompPath,
			string? manPagePath)
		{
			List<string> searchDirs = new List<string>();
			searchDirs.Add(Path.GetDirectoryName(assemblyFile));

			buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Building documentation for '{assemblyFile}' with documentation file '{docFile}'.", null, SenderName, MessageImportance.Normal));

			string? netInstallBase;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				netInstallBase = @"C:\Program Files\dotnet";
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				netInstallBase = File.ReadAllLines(@"/etc/dotnet/install_location").FirstOrDefault();
			}
			else
				netInstallBase = null;

			buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Using .NET install base {netInstallBase}", null, SenderName, MessageImportance.Low));

			if (!string.IsNullOrEmpty(netInstallBase))
			{
				netInstallBase = Path.Combine(netInstallBase, @"shared/Microsoft.NETCore.App");
				buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Search dotnet install base: {netInstallBase}", null, SenderName, MessageImportance.Low));
				try
				{
					var runtimes = Directory.GetDirectories(netInstallBase, "8.*");
					if (runtimes.Length == 0)
					{
						buildEngine.LogErrorEvent(new BuildErrorEventArgs("doc", "sdk", null, 0, 0, 0, 0, $"No .NET 8 files found.  Install the .NET 8 SDK", null, SenderName));
					}

					Array.Sort(runtimes, (x, y) =>
					{
						if (Version.TryParse(Path.GetFileName(x), out var xver) && Version.TryParse(Path.GetFileName(y), out var yver))
						{
							return -xver.CompareTo(yver);
						}
						else
							return 0;
					});
					foreach (var item in runtimes)
					{
						buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Found runtime: {item}", null, SenderName, MessageImportance.Low));
						Console.WriteLine($"Found runtime: {item}");
					}
					searchDirs.AddRange(runtimes);
				}
				catch (Exception)
				{
					throw;
				}
			}

			var asmResolver = new DirAssemblyResolver(searchDirs.ToArray());
			var loadContext = new MetadataLoadContext(asmResolver, "System.Private.CoreLib");

			var mdResolver = new ContextResolver(loadContext, buildEngine);
			mdResolver.RuntimeSearchDirectories.Add(Path.GetDirectoryName(assemblyFile));
			var asmNetStandard = loadContext.LoadFromAssemblyName("netstandard");
			//var asmNetStandard = Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\netstandard\v4.0_2.0.0.0__cc7b13ffcd2ddd51\netstandard.dll");
			CommandMetadataContext mdContext = new CommandMetadataContext(mdResolver);

			var cliAssembly = loadContext.LoadFromAssemblyName("Titanis.Cli");
			var commandBaseType = cliAssembly.GetType("Titanis.Cli.CommandBase");
			var commandType = cliAssembly.GetType("Titanis.Cli.Command");
			var multiCommandType = cliAssembly.GetType("Titanis.Cli.MultiCommand");

			var asm = mdResolver.LoadAssemblyFile(assemblyFile);
			var fileDate = File.GetLastWriteTimeUtc(assemblyFile);
			AssemblyName asmName = asm.GetName();
			var version = asmName.Version;

			var programType = asm.EntryPoint.DeclaringType;

			bool isCommand = (commandBaseType.IsAssignableFrom(programType));
			if (isCommand)
			{
				using var fileWriter = File.CreateText(docFile);
				var docWriter = new MarkdownDocWriter(fileWriter, 80);

				using TextWriter bashComp = (string.IsNullOrEmpty(bashAutocompFile) ? new StringWriter() : File.CreateText(bashAutocompFile));
				bashComp.NewLine = "\n";
				bashComp.WriteLine("# bash completion");
				bashComp.WriteLine("source \"${BASH_SOURCE[0]%/*}/Titanis-comp\"");

				Queue<SubcommandAttribute> commandQueue = new Queue<SubcommandAttribute>();
				commandQueue.Enqueue(new SubcommandAttribute(asm.GetName().Name, programType));
				while (commandQueue.Count > 0)
				{
					var commandInfo = commandQueue.Dequeue();
					var commandName = commandInfo.Name;
					var bashName = commandInfo.Name.Replace(' ', '_');
					var aliasName = commandInfo.Name.Replace(' ', '-');
					var type = commandInfo.CommandType;

					var desc = mdResolver.GetCustomAttribute<DescriptionAttribute>(type, true)?.Description;

					fileWriter.WriteLine($"# {commandName}");

					bashComp.WriteLine();
					bashComp.WriteLine($"# {commandName}");
					bashComp.WriteLine($"_{bashName} () {{");

					using TextWriter zshComp = string.IsNullOrEmpty(zshAutocompPath) ? new StringWriter() : File.CreateText(Path.Combine(zshAutocompPath, $"_{bashName}"));
					zshComp.NewLine = "\n";
					zshComp.WriteLine($"#compdef {bashName}");
					zshComp.WriteLine();
					zshComp.WriteLine($"_{bashName}() {{");

					using TextWriter manPage = new StringWriter();
					manPage.NewLine = "\n";
					var manWriter = new ManWriter(manPage);
					manWriter.WriteComment("t");
					manWriter.WriteComment($"Man page for {aliasName}");
					manPage.WriteLine(".pc");
					manPage.WriteLine($".TH MAN 1 \"{fileDate:yyyy-MM-dd}\" \"{version}\" \"{asmName.Name}\"");
					manWriter.SectionHeader("NAME");
					manPage.WriteLine($"{aliasName} \\- {desc}");

					if (commandType.IsAssignableFrom(type))
					{
						buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Processing command type {type.FullName}", null, SenderName, MessageImportance.Low));

						var md = Command.GetCommandMetadata(type, mdContext);
						if (string.IsNullOrEmpty(md.Description))
							buildEngine.LogErrorEvent(MakeMissingDescError(type.FullName));

						bashComp.WriteLine($"\tdeclare -A params=(");
						zshComp.WriteLine($"\tdeclare -A params=(");

						//StringBuilder sbBashParams = new StringBuilder();
						List<string> posNames = new List<string>();
						foreach (var param in md.Parameters)
						{
							if (string.IsNullOrEmpty(param.Description))
								buildEngine.LogErrorEvent(MakeMissingDescError(commandName, param.Name));

							string valuesToken = string.Empty;
							string formatToken = string.Empty;// (param.ElementType.Name == nameof(FileSpec)) ? "file" : (param.ElementType.IsEnum) ? "enum" : "";
							if (param.HasValueList)
							{
								var valueList = param.GetValueList(null, mdContext);
								if (valueList != null)
								{
									formatToken = "list";
									valuesToken = string.Join(";", valueList.OfType<object>());
						}
							}
							else if (param.IsFileSpec)
							{
								formatToken = "file";
								valuesToken = string.Join(";", param.FileTypes.SelectMany(r => r.Patterns));
							}
							else
							{
								valuesToken = string.Empty;
							}
							//sbBashParams.Append($"-{param.Name}:{formatToken}");

							bashComp.WriteLine($"\t\t['-{param.Name.ToLower()}']=$'{param.Name}:{formatToken}:{valuesToken}'");
							zshComp.WriteLine($"\t\t['-{param.Name.ToLower()}']=$'{param.Name}|{(param.IsMandatory ? "X" : param.IsAdvanced ? "@" : null)}|{(param.IsSwitch ? null : EscapeShellString(param.Placeholder))}|{formatToken}|{valuesToken}|{EscapeShellString(param.Category)}|{EscapeShellString(param.Description)}'");
							}
						bashComp.WriteLine($"\t)");
						zshComp.WriteLine($"\t)");

						bashComp.WriteLine($"\tdeclare -a paramsByPos=(");
						zshComp.WriteLine($"\tdeclare -a paramsByPos=(");
						foreach (var param in md.PositionalParameters)
						{
							bashComp.WriteLine($"\t\t'{param.Name}'");
							zshComp.WriteLine($"\t\t'{param.Name}'");
						}
						bashComp.WriteLine($"\t)");
						zshComp.WriteLine($"\t)");

						zshComp.WriteLine($"\t_comp_Titanis");
						bashComp.WriteLine($"\t_comp_Titanis");

						Command.BuildCommandHelpText(type, docWriter, commandName, null, mdContext);
						Command.BuildCommandHelpText(type, manWriter, commandName, null, mdContext, CommandHelpOptions.Default & ~CommandHelpOptions.Description);
					}
					else if (multiCommandType.IsAssignableFrom(type))
					{
						buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Processing multi-command type {commandType.FullName}", null, SenderName, MessageImportance.Low));

						if (string.IsNullOrEmpty(desc))
							buildEngine.LogErrorEvent(MakeMissingDescError(type.FullName));

						MultiCommand.BuildCommandHelpText(type.GetTypeInfo(), docWriter, commandName, mdContext, CommandHelpOptions.Default);
						MultiCommand.BuildCommandHelpText(type.GetTypeInfo(), manWriter, commandName, mdContext, CommandHelpOptions.Default & ~CommandHelpOptions.Description);

						var subcmds = mdResolver.GetCustomAttributes<SubcommandAttribute>(type, true);
						subcmds = subcmds.OrderBy(r => r.Name);

						zshComp.WriteLine("\tlocal -a commands=(");

						foreach (var subcmd in subcmds)
						{
							commandQueue.Enqueue(new SubcommandAttribute(commandName + " " + subcmd.Name, subcmd.CommandType));

							var md = Command.GetCommandMetadata(subcmd.CommandType, mdContext);

							zshComp.WriteLine($"\t\t'{subcmd.Name}:{EscapeShellString(md.Description)}'");
						}

						bashComp.WriteLine($"\t_comp_T_subcommands \"$1\" \"$2\" {string.Join(" ", subcmds.Select(r => r.Name))}");
						bashComp.WriteLine("\treturn $?");

						zshComp.WriteLine($"\t)");
						zshComp.WriteLine($"\t_comp_T_subcommands");
					}

					zshComp.WriteLine("}");

					bashComp.WriteLine("}");
					bashComp.WriteLine($"complete -F _{bashName} {aliasName}");

					if (!string.IsNullOrEmpty(manPagePath))
					{
						if (!Directory.Exists(manPagePath))
							Directory.CreateDirectory(manPagePath);
						using var manStream = File.Create(Path.Combine(manPagePath, $"{aliasName}.1.gz"));
						using var mangz = new GZipStream(manStream, CompressionLevel.Optimal);
						using var mantext = new StreamWriter(mangz, Encoding.UTF8);
						mantext.NewLine = "\n";
						mantext.Write(manPage.ToString());
						mantext.Flush();
					}
				}
			}

			return true;
		}

		private static string? EscapeShellString(string? str) => str?.Replace("'", "\\'");

		private static BuildErrorEventArgs MakeMissingDescError(string commandName, string paramName)
		{
			return new BuildErrorEventArgs("Documentation", "DOC0002", commandName, 0, 0, 0, 0, $"Parameter {paramName} of command class {commandName} does not have a description.", null, "DocBuilder");
		}

		private static BuildErrorEventArgs MakeMissingDescError(string commandName)
		{
			return new BuildErrorEventArgs("Documentation", "DOC0001", commandName, 0, 0, 0, 0, $"The command class {commandName} does not have a description.", null, "DocBuilder");
		}

		class DirAssemblyResolver : MetadataAssemblyResolver
		{
			internal DirAssemblyResolver(string[] searchDirs)
			{
				SearchDirs = searchDirs;
			}

			public string[] SearchDirs { get; }

			public override Assembly? Resolve(MetadataLoadContext context, AssemblyName assemblyName)
			{
				var baseName = assemblyName.Name + ".dll";

				foreach (var searchDir in this.SearchDirs)
				{
					if (searchDir is null)
						continue;

					string path = Path.Combine(searchDir, baseName);
					if (File.Exists(path))
					{
						var bytes = File.ReadAllBytes(path);
						var refasm = context.LoadFromByteArray(bytes);
						return refasm;
					}
				}

				return null;
			}
		}
	}

	internal class NullableAttribute : Attribute
	{
		public NullableAttribute(byte flag)
		{
			Flags = [flag];
		}
		public NullableAttribute(byte[] flags)
		{
			Flags = flags;
		}

		public byte[] Flags { get; }
	}
}
