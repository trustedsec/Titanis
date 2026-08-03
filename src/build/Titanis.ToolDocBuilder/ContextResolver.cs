using Microsoft.Build.Framework;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml;
using Titanis.Cli;

namespace Titanis.ToolDocBuilder
{
	class ContextResolver : MetadataResolver
	{
		private readonly MetadataLoadContext context;
		private readonly IBuildEngine buildEngine;

		public ContextResolver(MetadataLoadContext context, IBuildEngine buildEngine)
		{
			this.context = context;
			this.buildEngine = buildEngine;
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

		class AssemblyLoadInfo
		{
			internal AssemblyLoadInfo(string fileName, XmlDocument? docFile)
			{
				FileName = fileName;
				DocFile = docFile;
			}

			public string FileName { get; }
			public XmlDocument? DocFile { get; }
		}
		private System.Runtime.CompilerServices.ConditionalWeakTable<Assembly, AssemblyLoadInfo> _asmLoadInfo = new System.Runtime.CompilerServices.ConditionalWeakTable<Assembly, AssemblyLoadInfo>();
		internal Assembly LoadAssemblyFile(string fileName)
		{
			var asm = this.context.LoadFromByteArray(File.ReadAllBytes(fileName));

			XmlDocument? doc = null;
			{
				var xmlDocName = fileName + ".xml";
				if (File.Exists(xmlDocName))
				{
					try
					{
						doc = new XmlDocument();
						doc.Load(xmlDocName);
					}
					catch
					{
						doc = null;
					}
				}
			}

			this._asmLoadInfo.Add(asm, new AssemblyLoadInfo(fileName, doc));

			return asm;
		}

		public override ICustomTypeDescriptor GetDescriptor(Type type)
		{
			return new SurrogateTypeDescriptor(type, this);
		}

		internal IEnumerable<Attribute> GetCustomAttributes(MemberInfo member, Type attributeType, bool inherit)
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
			try
			{
				return ReflectAttribute(attrDatum);
			}
			catch
			{
				return null;
			}
		}
		private Attribute? ReflectAttribute(CustomAttributeData attrDatum)
		{
			Type? type = attrDatum.AttributeType.FullName switch
			{
				"System.ComponentModel.BrowsableAttribute" => typeof(BrowsableAttribute),
				"System.ComponentModel.DefaultValueAttribute" => typeof(DefaultValueAttribute),
				"System.ComponentModel.DescriptionAttribute" => typeof(DescriptionAttribute),
				"System.ComponentModel.DisplayNameAttribute" => typeof(DisplayNameAttribute),
				"System.ComponentModel.CategoryAttribute" => typeof(CategoryAttribute),
				"System.Runtime.CompilerServices.NullableAttribute" => typeof(NullableAttribute),
				"Titanis.DisplayAlignmentAttribute" => typeof(DisplayAlignmentAttribute),
				"Titanis.DisplayFormatStringAttribute" => typeof(DisplayFormatStringAttribute),
				"Titanis.FileSizeAttribute" => typeof(FileSizeAttribute),
				_ => (attrDatum.AttributeType.FullName.StartsWith("Titanis.Cli.")) ? typeof(Command).Assembly.GetType(attrDatum.AttributeType.FullName) : this.GetRuntimeType(attrDatum.AttributeType)
			};
			if (type is null)
				return null;

			var args = attrDatum.ConstructorArguments.Select(r => ConvertCtorArg(r)).ToArray();
			Attribute? attr;
			try
			{
				attr = (Attribute)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.DefaultBinder, args, null);
			}
			catch (Exception ex)
			{
				buildEngine.LogMessageEvent(new BuildMessageEventArgs($"Error instantiating attribute {type.FullName} with arguments ({string.Join(", ", args.Select(r => (r is null) ? "<null>" : $"[{r.GetType().FullName}]{r}"))}): {ex}", null, BuildToolDocTask.SenderName, MessageImportance.High));
				throw;
			}
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
