using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Mocks
{
	public static class Arg
	{
		public static T Any<T>() => throw new NotImplementedException();
		public static T Matches<T>(Func<T, bool> matches) => throw new NotImplementedException();
	}
}
