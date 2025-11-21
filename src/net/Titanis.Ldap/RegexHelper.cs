using System.Text.RegularExpressions;

namespace Titanis.Ldap
{
	static class RegexHelper
	{
		public static string? CapturedValueOrNull(this Group group)
			=> group.Success ? group.Value : null;
		public static int? CapturedInt32OrNull(this Group group)
			=> group.Success ? int.Parse(group.Value) : null;
	}
}
