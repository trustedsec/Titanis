using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	public static class DateTimeExtensions
	{
		public static DateTime RoundSeconds(this DateTime dt)
		{
			var m = dt.Ticks % TimeSpan.TicksPerSecond;
			if (m != 0)
				dt = new DateTime(dt.Ticks - m);
			return dt;
		}
	}
}
