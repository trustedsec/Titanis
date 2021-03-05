using System;
using System.Linq;
using System.Reflection;


namespace Titanis
{
	public static class AttributeExtensions
	{
		public static TValue? GetAttributeValue<TAttribute, TValue>(
			this Type type,
			Func<TAttribute, TValue> valueSelector)
			where TAttribute : Attribute
		{
			if (type.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute attr)
			{
				return valueSelector(attr);
			}
			return default(TValue);
		}
	}
}