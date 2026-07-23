using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

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
