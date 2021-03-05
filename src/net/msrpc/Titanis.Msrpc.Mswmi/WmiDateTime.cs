using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	public struct WmiDateTime : IEquatable<WmiDateTime>
	{
		public WmiDateTime(DateTime datetime, int utcOffsetMinutes)
		{
			this.DateTime = datetime;
			this.UtcOffsetMinutes = utcOffsetMinutes;
		}

		public override string ToString()
			=> this.ToWmiString();
			//=> $"{this.DateTime} UTC{(this.UtcOffsetMinutes >= 0 ? "+" : null)}{this.UtcOffsetMinutes}";
		public string ToWmiString()
		{
			string str = $"{this.DateTime.Year:0000}{this.DateTime.Month:00}{this.DateTime.Day:00}{this.DateTime.Hour:00}{this.DateTime.Minute:00}{this.DateTime.Second:00}.{(this.DateTime.Ticks / 10 % 1_000_000):000000}{(this.UtcOffsetMinutes >= 0 ? '+' : '-')}{Math.Abs(this.UtcOffsetMinutes):000}";
			return str;
		}

		public DateTime DateTime { get; }
		public int UtcOffsetMinutes { get; }

		public override bool Equals(object? obj)
		{
			return obj is WmiDateTime time && this.Equals(time);
		}

		public bool Equals(WmiDateTime other)
		{
			return this.DateTime == other.DateTime &&
				   this.UtcOffsetMinutes == other.UtcOffsetMinutes;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.DateTime, this.UtcOffsetMinutes);
		}

		public static bool operator ==(WmiDateTime left, WmiDateTime right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(WmiDateTime left, WmiDateTime right)
		{
			return !(left == right);
		}
	}
}
