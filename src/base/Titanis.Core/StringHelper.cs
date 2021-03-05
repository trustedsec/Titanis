using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Titanis
{
	public static class StringHelper
	{
		public static int GetHashCode(this string str, StringComparison comparison)
		{
			bool ignoreCase = 0 != ((uint)comparison & 1);
			comparison = (comparison & (StringComparison)~1);
			var culture = comparison switch
			{
				StringComparison.CurrentCulture => CultureInfo.CurrentCulture,
				StringComparison.InvariantCulture or StringComparison.Ordinal => CultureInfo.InvariantCulture,
				_ => CultureInfo.CurrentCulture
			};
			return culture.CompareInfo.GetHashCode(str, CompareOptions.IgnoreCase);
		}
	}
}
