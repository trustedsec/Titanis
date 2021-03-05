using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the description as a property of a type.
	/// </summary>
	/// <remarks>
	/// This class allows you to specify the description as text within a resource class
	/// and reference the text as a <see cref="DescriptionAttribute"/>.
	/// </remarks>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class DescriptionResourceAttribute : DescriptionAttribute
	{
		public DescriptionResourceAttribute(Type resourceClass, string propertyName)
			: base(GetText(resourceClass, propertyName))
		{
			this.ResourceClass = resourceClass;
			this.PropertyName = propertyName;
		}

		private static string GetText(Type resourceClass, string propertyName)
		{
			PropertyInfo prop = resourceClass.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
			string desc = prop?.GetValue(null, null) as string;
			return desc ?? $"{resourceClass.FullName}.{propertyName}";
		}

		public Type ResourceClass { get; }
		public string PropertyName { get; }
	}
}
