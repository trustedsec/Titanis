using Microsoft.Build.Framework;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titanis.Cli;

namespace Titanis.ToolDocBuilder
{
	public class BuildToolDocTask : ITask
	{
		public IBuildEngine? BuildEngine { get; set; }
		public ITaskHost? HostObject { get; set; }

		[Required]
		public string? AssemblyFile { get; set; }

		[Required]
		public string? DocFile { get; set; }

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
				return GenerateDoc(this.BuildEngine, this.AssemblyFile!, this.DocFile!);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}

		public static bool GenerateDoc(IBuildEngine buildEngine, string assemblyFile, string docFile)
		{
			List<string> searchDirs = new List<string>();
			searchDirs.Add(Path.GetDirectoryName(assemblyFile));

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

			if (!string.IsNullOrEmpty(netInstallBase))
			{
				netInstallBase = Path.Combine(netInstallBase, @"shared/Microsoft.NETCore.App");
				buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Search dotnet install base: {netInstallBase}", null, "Titanis.ToolDocBuilder", MessageImportance.Low));
				try
				{
					var runtimes = Directory.GetDirectories(netInstallBase, "8.*");
					if (runtimes.Length == 0)
					{
						buildEngine.LogErrorEvent(new BuildErrorEventArgs("doc", "sdk", null, 0, 0, 0, 0, $"No .NET 8 files found.  Install the .NET 8 SDK", null, "Titanis.ToolDocBuilder"));
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
						buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Found runtime: {item}", null, "Titanis.ToolDocBuilder", MessageImportance.Low));
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

			var mdResolver = new ContextResolver(loadContext);
			mdResolver.RuntimeSearchDirectories.Add(Path.GetDirectoryName(assemblyFile));
			var asmNetStandard = loadContext.LoadFromAssemblyName("netstandard");
			//var asmNetStandard = Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\netstandard\v4.0_2.0.0.0__cc7b13ffcd2ddd51\netstandard.dll");
			CommandMetadataContext mdContext = new CommandMetadataContext(mdResolver);

			var cliAssembly = loadContext.LoadFromAssemblyName("Titanis.Cli");
			var commandBaseType = cliAssembly.GetType("Titanis.Cli.CommandBase");
			var commandType = cliAssembly.GetType("Titanis.Cli.Command");
			var multiCommandType = cliAssembly.GetType("Titanis.Cli.MultiCommand");

			var asm = loadContext.LoadFromAssemblyPath(assemblyFile);
			var programType = asm.EntryPoint.DeclaringType;

			bool isCommand = (commandBaseType.IsAssignableFrom(programType));
			if (isCommand)
			{
				using var fileWriter = File.CreateText(docFile);
				var docWriter = new MarkdownDocWriter(fileWriter, 80);


				Queue<SubcommandAttribute> commandQueue = new Queue<SubcommandAttribute>();
				commandQueue.Enqueue(new SubcommandAttribute(asm.GetName().Name, programType));
				while (commandQueue.Count > 0)
				{
					var commandInfo = commandQueue.Dequeue();
					var commandName = commandInfo.Name;
					var type = commandInfo.CommandType;

					fileWriter.WriteLine($"# {commandName}");

					if (commandType.IsAssignableFrom(type))
					{
						var md = Command.GetCommandMetadata(commandInfo.CommandType, mdContext);
						if (string.IsNullOrEmpty(md.Description))
							buildEngine.LogErrorEvent(MakeMissingDescError(commandName));
						foreach (var param in md.Parameters)
						{
							if (string.IsNullOrEmpty(param.Description))
								buildEngine.LogErrorEvent(MakeMissingDescError(commandName, param.Name));
						}

						Command.BuildCommandHelpText(type, docWriter, commandName, null, mdContext);
					}
					else if (multiCommandType.IsAssignableFrom(type))
					{
						var desc = mdResolver.GetCustomAttribute<DescriptionAttribute>(type, true)?.Description;
						if (string.IsNullOrEmpty(desc))
							buildEngine.LogErrorEvent(MakeMissingDescError(commandName));

						MultiCommand.BuildCommandHelpText(type.GetTypeInfo(), docWriter, commandName, mdContext);

						var subcmds = mdResolver.GetCustomAttributes<SubcommandAttribute>(type, true);
						subcmds = subcmds.OrderBy(r => r.Name);
						foreach (var subcmd in subcmds)
						{
							commandQueue.Enqueue(new SubcommandAttribute(commandName + " " + subcmd.Name, subcmd.CommandType));
						}
					}
				}
			}

			return true;
		}

		private static BuildErrorEventArgs MakeMissingDescError(string commandName, string paramName)
		{
			return new BuildErrorEventArgs("Documentation", "DOC0002", commandName, 0, 0, 0, 0, $"Parameter {paramName} of command {commandName} does not have a description.", null, "DocBuilder");
		}

		private static BuildErrorEventArgs MakeMissingDescError(string commandName)
		{
			return new BuildErrorEventArgs("Documentation", "DOC0001", commandName, 0, 0, 0, 0, $"The command {commandName} does not have a description.", null, "DocBuilder");
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
						var refasm = context.LoadFromAssemblyPath(path);
						return refasm;
					}
				}

				return null;
			}
		}
	}

	class ContextResolver : MetadataResolver
	{
		private readonly MetadataLoadContext context;

		public ContextResolver(MetadataLoadContext context)
		{
			this.context = context;
		}

		class EnumValue
		{
			public EnumValue(string name, object value)
			{
				Name = name;
				Value = value;
			}

			public string Name { get; }
			public object Value { get; }

			public sealed override string ToString()
			{
				return this.Name;

			}
		}
		public sealed override Array GetEnumValues(Type enumType)
		{
			FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
			EnumValue[] values = Array.ConvertAll(fields, f => new EnumValue(f.Name, f.GetRawConstantValue()));
			return values;
		}

		public sealed override Type ReflectType(Type sourceType)
		{
			var declaringType = sourceType.DeclaringType;
			if (declaringType is null)
			{
				var fullName = sourceType.FullName;
				Assembly asm;
				try
				{
					asm = context.LoadFromAssemblyName(sourceType.Assembly.GetName());
				}
				catch (Exception)
				{
					asm = context.LoadFromAssemblyName("netstandard");
				}
				var resolved = asm.GetType(fullName);
				return resolved;
			}
			else
			{
				var declaring = this.ReflectType(declaringType);
				return (Type)declaring.GetMember(sourceType.Name)[0];
			}
		}

		public sealed override bool IsDefined(MemberInfo member, Type attributeType)
		{
			return this.GetCustomAttributes(member, attributeType, true).Any();
		}
		public sealed override IEnumerable<T> GetCustomAttributes<T>(MemberInfo member, bool inherit)
		{
			return GetCustomAttributes(member, typeof(T), inherit).OfType<T>();
		}

		private IEnumerable<Attribute> GetCustomAttributes(MemberInfo member, Type attributeType, bool inherit)
		{
			attributeType = this.ReflectType(attributeType);

			var attrData = member.GetCustomAttributesData();
			foreach (var attrDatum in attrData)
			{
				bool matches =
					(attrDatum.AttributeType.Equals(attributeType))
					|| (inherit && (attributeType.IsAssignableFrom(attrDatum.AttributeType)));
				if (matches)
				{
					var attr = TryInstantiateAttr(attrDatum);
					if (attr is not null)
						yield return (Attribute)attr;
				}
			}
		}

		private Attribute? TryInstantiateAttr(CustomAttributeData attrDatum)
		{
			Type type = this.GetRuntimeType(attrDatum.AttributeType);
			if (type is null)
				return null;

			var args = attrDatum.ConstructorArguments.Select(r => ConvertCtorArg(r)).ToArray();
			var attr = (Attribute)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, args, null);
			foreach (var namedArg in attrDatum.NamedArguments)
			{
				var member = type.GetMember(namedArg.MemberName)[0];
				var value = ConvertCtorArg(namedArg.TypedValue);
				if (member is FieldInfo field)
					field.SetValue(attr, value);
				else if (member is PropertyInfo prop)
					prop.SetValue(attr, value);
			}

			return attr;
		}


		private object ConvertCtorArg(CustomAttributeTypedArgument typedValue)
		{
			if (typedValue.Value is IList<CustomAttributeTypedArgument> list)
			{
				var args = list.Select(r => ConvertCtorArg(r)).ToArray();
				Array typedArgs = Array.CreateInstance(this.GetRuntimeType(typedValue.ArgumentType.GetElementType()), args.Length);
				for (int i = 0; i < args.Length; i++)
				{
					var arg = args[i];
					typedArgs.SetValue(arg, i);
				}
				return typedArgs;
			}
			else if (typedValue.ArgumentType.IsEnum)
			{
				return Enum.ToObject(this.GetRuntimeType(typedValue.ArgumentType), typedValue.Value);
			}
			else
				return typedValue.Value;
		}
	}
}
