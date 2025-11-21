using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public static class LdapMatchRules
	{

	}

	public abstract class LdapMatchRule : IComparer
	{
		public abstract int Compare(object? x, object? y);
	}
	public abstract class LdapMatchRule<T> : LdapMatchRule, IComparer<T>
	{
		public sealed override int Compare(object? x, object? y)
		{
			if (x is T xTyped && y is T yTyped)
				return this.Compare(xTyped, yTyped);
			else
				return 0;
		}

		public abstract int Compare(T? x, T? y);
	}

	// [MS-ADTS] § 3.1.1.2.2.4.1
	public sealed class BoolComparisonRule : LdapMatchRule<bool>
	{
		public sealed override int Compare(bool x, bool y)
		{
			return (x == y) ? 0 : (x ? 1 : -1);
		}
	}

	// [MS-ADTS] § 3.1.1.2.2.4.2
	public sealed class IntegerComparisonRule : LdapMatchRule<int>
	{
		public sealed override int Compare(int x, int y)
		{
			return (x < y) ? -1 : (x == y) ? 0 : 1;
		}
	}
}
