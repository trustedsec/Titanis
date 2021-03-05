using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	// [DMTF-DSP0004] § 2.2.1 - Datetime Type
	public struct WmiInterval : IEquatable<WmiInterval>
	{
		public WmiInterval(
			int days,
			int hours,
			int minutes,
			int seconds,
			int microseconds
			)
		{
			Precision prec;

			if (days < 0)
				throw new ArgumentOutOfRangeException(nameof(days));

			if (hours < 0)
			{
				prec = Precision.Days;
				hours = 0; minutes = 0; seconds = 0; microseconds = 0;
			}
			else if (minutes < 0)
			{
				prec = Precision.Hours;
				minutes = 0; seconds = 0; microseconds = 0;
			}
			else if (seconds < 0)
			{
				prec = Precision.Minutes;
				seconds = 0; microseconds = 0;
			}
			else if (microseconds < 0)
			{
				prec = Precision.Seconds;
				microseconds = 0;
			}
			else
			{
				prec = Precision.Microseconds;
			}
			this._precision = prec;

			// 1 tick = 100-nanoseconds
			var ticks = (((((long)days * 24 + hours) * 60 + minutes) * 60 + seconds) * 1_000_000 + microseconds) * 10;
			this.Duration = TimeSpan.FromTicks(ticks);
		}

		enum Precision
		{
			Days,
			Hours,
			Minutes,
			Seconds,
			Microseconds
		}
		private Precision _precision;

		public override string ToString()
			=> this.ToWmiString();
		public string ToWmiString()
			=> this._precision switch
			{
				Precision.Days => $"{this.Duration.Days:00000000}******.******:000",
				Precision.Hours => $"{this.Duration.Days:00000000}{this.Duration.Hours:00}****.******:000",
				Precision.Minutes => $"{this.Duration.Days:00000000}{this.Duration.Hours:00}{this.Duration.Minutes:00}**.******:000",
				Precision.Seconds => $"{this.Duration.Days:00000000}{this.Duration.Hours:00}{this.Duration.Minutes:00}{this.Duration.Seconds:00}.******:000",
				_ => $"{this.Duration.Days:00000000}{this.Duration.Hours:00}{this.Duration.Minutes:00}{this.Duration.Seconds:00}.{(this.Duration.Ticks / 10 % 1_000_000):000000}:000"
			};

		public TimeSpan Duration { get; }

		public override bool Equals(object? obj)
		{
			return obj is WmiInterval time && this.Equals(time);
		}

		public bool Equals(WmiInterval other)
		{
			return this.Duration == other.Duration &&
				this._precision == other._precision;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.Duration, this._precision);
		}

		public static bool operator ==(WmiInterval left, WmiInterval right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(WmiInterval left, WmiInterval right)
		{
			return !(left == right);
		}
	}
}
