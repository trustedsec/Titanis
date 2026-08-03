using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Titanis
{
	/// <summary>
	/// Resolves metadata.
	/// </summary>
	public abstract class MetadataResolver
	{
		public static MetadataResolver Default => Singleton.SingleInstance<ReflectionMetadataResolver>();

		public ICustomTypeDescriptor GetDescriptor(object instance)
		{
			if (instance is ICustomTypeDescriptor custom)
				return custom;
			return TypeDescriptor.GetProvider(instance).GetTypeDescriptor(instance);
		}
		public abstract ICustomTypeDescriptor GetDescriptor(Type type);
		protected virtual XmlDocument? LoadDocForType(Assembly assembly)
		{
			string fileName = assembly.Location;
			fileName = Path.ChangeExtension(fileName, ".xml");
			if (File.Exists(fileName))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fileName);
				return doc;
			}

			return null;
		}

		class XmlDocInfo
		{
			internal readonly XmlDocument? doc;
			internal readonly XmlNamespaceManager? nsmgr;

			internal XmlDocInfo(XmlDocument? doc, XmlNamespaceManager? nsmgr)
			{
				this.doc = doc;
				this.nsmgr = nsmgr;
			}
		}
		private System.Runtime.CompilerServices.ConditionalWeakTable<Assembly, XmlDocInfo> _asmDocInfo = new System.Runtime.CompilerServices.ConditionalWeakTable<Assembly, XmlDocInfo>();
		public XmlNode? GetDocumentation(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));
			if (!this._asmDocInfo.TryGetValue(type.Assembly, out var docInfo))
			{
				var doc = this.LoadDocForType(type.Assembly);
				XmlNamespaceManager? nsmgr = null;
				if (doc != null)
				{
					nsmgr = new XmlNamespaceManager(doc.NameTable);
					nsmgr.AddNamespace("doc", "");
				}
				docInfo = new XmlDocInfo(doc, nsmgr);
				this._asmDocInfo.Add(type.Assembly, docInfo);
			}

			if (docInfo.doc != null)
			{
				const string prefix = "doc";
				return docInfo.doc.SelectSingleNode($"/{prefix}:doc/{prefix}:members/{prefix}:member[@name='T:{type.FullName}']", docInfo.nsmgr);
			}

			return null;
		}

		public abstract Type ReflectType(Type type);
		public T? GetCustomAttribute<T>(Type member, bool inherit) where T : Attribute
		{
			return this.GetCustomAttributes<T>(member, inherit).FirstOrDefault();
		}
		public IEnumerable<T> GetCustomAttributes<T>(Type type, bool inherit) where T : Attribute
		{
			var attrs = this.GetDescriptor(type).GetAttributes().OfType<T>();
			return attrs;
		}

		public abstract Array GetEnumValues(Type enumType);

		public List<string> RuntimeSearchDirectories { get; set; } = new List<string>();


		private static ConcurrentDictionary<string, Assembly> _assemblyCache = new ConcurrentDictionary<string, Assembly>();
		public Type GetRuntimeType(Type reflectedType)
		{
			var asmName = reflectedType.Assembly.GetName();

			if (!_assemblyCache.TryGetValue(asmName.Name, out var asm))
			{
				try
				{
					foreach (var searchDir in this.RuntimeSearchDirectories)
					{
						var candidatePath = Path.Combine(searchDir, asmName.Name + ".dll");
						if (File.Exists(candidatePath))
						{
							asm = Assembly.LoadFrom(candidatePath);
						}
					}

					if (asm is null)
						asm = Assembly.Load(asmName.Name);
				}
				catch
				{
					//asm = Assembly.Load("netstandard, Version=2.0.0.0");

					if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					{
						asm = Assembly.LoadFrom(@"C:\Windows\Microsoft.NET\assembly\GAC_MSIL\netstandard\v4.0_2.0.0.0__cc7b13ffcd2ddd51\netstandard.dll");
					}
					else
					{
						asm = Assembly.LoadFrom(@"/usr/lib/dotnet/sdk/8.0.127/ref/netstandard.dll");
					}
				}
				_assemblyCache.TryAdd(asmName.Name, asm);
			}

			var runtimeType = asm.GetType(reflectedType.FullName);
			return runtimeType;
		}
	}

	public static class MetadataHelper
	{
		public static bool IsDefined<TAttribute>(this PropertyDescriptor property)
			where TAttribute : Attribute
		{
			foreach (var attr in property.Attributes)
			{
				if (attr is TAttribute)
					return true;
			}
			return false;
		}
		public static TAttribute? GetCustomAttribute<TAttribute>(this ICustomTypeDescriptor typeDescr, bool inherited = true)
			where TAttribute : Attribute
		{
			foreach (var attr in typeDescr.GetAttributes())
			{
				if (attr is TAttribute typed)
					return typed;
			}
			return null;
		}
		public static TAttribute? GetCustomAttribute<TAttribute>(this PropertyDescriptor property, bool inherited = true)
			where TAttribute : Attribute
		{
			foreach (var attr in property.Attributes)
			{
				if (attr is TAttribute typed)
					return typed;
			}
			return null;
		}
	}

	public sealed class ReflectionMetadataResolver : MetadataResolver
	{
		public sealed override Type ReflectType(Type type) => type;
		public override ICustomTypeDescriptor GetDescriptor(Type type) => TypeDescriptor.GetProvider(type).GetTypeDescriptor(type);

		public sealed override Array GetEnumValues(Type enumType)
		{
			return Enum.GetValues(enumType);
		}
	}
}
