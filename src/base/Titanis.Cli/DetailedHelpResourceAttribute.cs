using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the detailed help text as a property of a type.
	/// </summary>
	/// <remarks>
	/// This class allows you to specify the detailed help text as text within a resource class
	/// and reference the text as a <see cref="DescriptionAttribute"/>.
	/// </remarks>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class DetailedHelpResourceAttribute : DetailedHelpTextAttribute
	{
		public DetailedHelpResourceAttribute(Type resourceClass, string propertyName)
			: base(GetText(resourceClass, propertyName))
		{
			this.ResourceClass = resourceClass;
			this.PropertyName = propertyName;
		}

		public sealed override string GetText(CommandMetadataContext context)
		{
			return GetText(ResourceClass, this.PropertyName);
		}
		private static string GetText(Type resourceClass, string propertyName)
		{
			PropertyInfo prop = resourceClass.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
			try
			{
				string desc = prop?.GetValue(null, null) as string;
				return desc ?? $"{resourceClass.FullName}.{propertyName}";
			}
			catch (InvalidOperationException ex)
			{
				var resStream = resourceClass.Assembly.GetManifestResourceStream(resourceClass.FullName + ".resources");
				if (resStream is null)
					throw;

				ResourceSet resSet = new ResourceSet(resStream);
				var value = resSet.GetObject(propertyName);

				return value as string;
			}
		}

		public Type ResourceClass { get; }
		public string PropertyName { get; }
	}
}
