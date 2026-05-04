using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	internal static class DateTimeExtensions
	{
		public static timespec ToTimespec(this DateTime dt)
		{
			var ts = dt - DateTime.UnixEpoch;
			var ticks = ts.Ticks;

			return new timespec(ticks / 10_000_000, ticks % 10_000_000);
		}
		public static timespec ToTimespec(this DateTime? dt)
		{
			if (!dt.HasValue)
				return default;
			else
				return dt.Value.ToTimespec();
		}
	}
}
