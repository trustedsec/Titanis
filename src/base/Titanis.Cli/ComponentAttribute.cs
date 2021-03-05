using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Marks a type as a component.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ComponentAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="ComponentAttribute"/>.
		/// </summary>
		/// <param name="componentType">Type of component</param>
		/// <param name="tag">Tag associated with the component.</param>
		public ComponentAttribute(Type componentType, string tag)
		{
			if (componentType is null) throw new ArgumentNullException(nameof(componentType));
			if (tag is null) throw new ArgumentNullException(nameof(tag));

			ComponentType = componentType;
			Tag = tag;
		}

		/// <summary>
		/// Gets the type of component.
		/// </summary>
		public Type ComponentType { get; }
		/// <summary>
		/// Gets the tag associated with the component.
		/// </summary>
		public string Tag { get; }
	}
}
