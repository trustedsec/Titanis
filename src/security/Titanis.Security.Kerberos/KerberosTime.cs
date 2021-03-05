using System;
using Titanis.Asn1;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Represents a point it time.
	/// </summary>
	internal struct KerberosTime
	{
		public KerberosTime(DateTime dt)
		{
			this.dt = new DateTime(
				dt.Year, dt.Month, dt.Day,
				dt.Hour, dt.Minute, dt.Second);
			this.usec = (uint)((dt.Ticks / 10) % 1_000_000);
		}
		public KerberosTime(GeneralizedTime gt, uint usec)
		{
			this.dt = gt.value;
			this.usec = usec;
		}

		internal readonly DateTime dt;
		internal readonly uint usec;

		internal DateTime AsDateTime() => this.dt + TimeSpan.FromTicks(this.usec * 10);

		internal static KerberosTime Now() => Now(default);
		internal static KerberosTime Now(TimeSpan skew) => new KerberosTime(DateTime.UtcNow + skew);
	}
}
