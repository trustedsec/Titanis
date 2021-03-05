using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents a catalog of available components.
	/// </summary>
	public class ComponentCatalog
	{
		public ComponentCatalog(MetadataResolver resolver)
		{
			if (resolver is null) throw new ArgumentNullException(nameof(resolver));
			this._resolver = resolver;
		}

		private readonly MetadataResolver _resolver;

		private HashSet<Assembly> _assemblies = new HashSet<Assembly>();
		public void AddAssembly(Assembly assembly)
		{
			if (assembly is null) throw new ArgumentNullException(nameof(assembly));

			this._assemblies.Add(assembly);
		}

		public static void DiscoverComponents(Assembly assembly, MetadataResolver resolver, IList<ComponentInfo> components)
		{
			if (assembly is null) throw new ArgumentNullException(nameof(assembly));
			if (resolver is null) throw new ArgumentNullException(nameof(resolver));
			if (components is null) throw new ArgumentNullException(nameof(components));

			Type?[] types;
			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				types = ex.Types;
			}

			foreach (var type in types)
			{
				if (type is null || type.IsAbstract || !type.IsClass)
					continue;

				var attrs = resolver.GetCustomAttributes<ComponentAttribute>(type, false);
				foreach (var attr in attrs)
				{
					var tech = new ComponentInfo(attr.Tag, type);
					components.Add(tech);
				}
			}
		}
	}
}
