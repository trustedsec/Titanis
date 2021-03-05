using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Titanis.Cli
{
	public class ComponentSelector
	{
		internal ComponentSelector(string tagSelector)
		{
			TagSelector = tagSelector;
		}

		public string TagSelector { get; }
	}

	[ValueListProvider(typeof(ComponentListProvider))]
	[TypeConverter(typeof(ComponentSelectorConverter))]
	public sealed class ComponentSelector<T> : ComponentSelector
	{
		public ComponentSelector(string tagSelector) : base(tagSelector)
		{
		}

		public void SelectComponents()
		{

		}
	}

	public class ComponentSelectorConverter : TypeConverter
	{
		private Type? TryGetComponentType(Type componentSelectorType)
		{
			if (componentSelectorType.IsGenericType && (componentSelectorType.GetGenericTypeDefinition() == typeof(ComponentSelector<>)))
			{
				var elemType = componentSelectorType.GetGenericArguments()[0];
				return elemType;
			}
			else
				return null;
		}
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (context is ParameterConverterContext paramContext)
			{
				var compType = TryGetComponentType(paramContext.Parameter.ElementType);
				return (compType is not null);
			}
			else
				return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (context is ParameterConverterContext paramContext && value is string str)
			{
				Type elementType = paramContext.Parameter.ElementType;
				var compType = TryGetComponentType(elementType);
				if (compType is not null)
				{
					var selector = (ComponentSelector)Activator.CreateInstance(elementType, new object[] { str });
					return selector;
				}
			}

			return base.ConvertFrom(context, culture, value);
		}
	}

	/// <summary>
	/// Describes a component.
	/// </summary>
	/// <seealso cref="ComponentListProvider"/>
	public class ComponentInfo
	{
		internal ComponentInfo(string tag, Type implementingType)
		{
			Tag = tag;
			ImplementingType = implementingType;
		}

		/// <summary>
		/// Gets the tag identifying the component.
		/// </summary>
		public string Tag { get; }
		/// <summary>
		/// Gets the type implementing the component.
		/// </summary>
		public Type ImplementingType { get; }
	}

	/// <summary>
	/// Provides a list of components for a parameter.
	/// </summary>
	public class ComponentListProvider : ValueListProvider
	{
		/// <inheritdoc/>
		public sealed override Array GetValueListFor(ParameterMetadata parameter, object? command, CommandMetadataContext context)
		{
			var components = new List<ComponentInfo>();
			ComponentCatalog.DiscoverComponents(parameter.DeclaringCommand.ImplementingType.Assembly, context.Resolver, components);
			var tags = (new string[] { "*" }).Concat(components.Select(r => r.Tag)).ToArray();
			return tags;
		}
	}
}
