using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public interface IAsn1DateTime : IAsn1Tag
	{
		DateTime Value { get; set; }

		static abstract Asn1Tag StaticTag { get; }
	}
	public interface IAsn1DateTime<TSelf> : IAsn1DateTime
	{
		static abstract TSelf CreateFromValue(DateTime dt);
	}

	public struct Asn1Date : IAsn1DateTime<Asn1Date>
	{
		public Asn1Date(DateTime value)
		{
			this.Value = value;
		}

		public DateTime Value { get; set; }
		DateTime IAsn1DateTime.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator DateTime(Asn1Date dt) => dt.Value;
		public static implicit operator Asn1Date(DateTime value) => new Asn1Date(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Date;

		static Asn1Tag IAsn1DateTime.StaticTag => Asn1PredefTag.Date;
		static Asn1Date IAsn1DateTime<Asn1Date>.CreateFromValue(DateTime dt) => new Asn1Date(dt);
	}

	public struct Asn1Time : IAsn1DateTime<Asn1Time>
	{
		public Asn1Time(DateTime value)
		{
			this.Value = value;
		}

		public DateTime Value { get; set; }
		DateTime IAsn1DateTime.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator DateTime(Asn1Time dt) => dt.Value;
		public static implicit operator Asn1Time(DateTime value) => new Asn1Time(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Time;

		static Asn1Tag IAsn1DateTime.StaticTag => Asn1PredefTag.Time;
		static Asn1Time IAsn1DateTime<Asn1Time>.CreateFromValue(DateTime dt) => new Asn1Time(dt);
	}

	public struct TimeOfDay : IAsn1DateTime<TimeOfDay>
	{
		public TimeOfDay(DateTime value)
		{
			this.Value = value;
		}

		public DateTime Value { get; set; }
		DateTime IAsn1DateTime.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator DateTime(TimeOfDay dt) => dt.Value;
		public static implicit operator TimeOfDay(DateTime value) => new TimeOfDay(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.TimeOfDay;

		static Asn1Tag IAsn1DateTime.StaticTag => Asn1PredefTag.TimeOfDay;
		static TimeOfDay IAsn1DateTime<TimeOfDay>.CreateFromValue(DateTime dt) => new TimeOfDay(dt);
	}

	public struct GeneralizedTime : IAsn1DateTime<GeneralizedTime>
	{
		public GeneralizedTime(DateTime value)
		{
			this.Value = value;
		}

		public DateTime Value { get; set; }
		DateTime IAsn1DateTime.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator DateTime(GeneralizedTime dt) => dt.Value;
		public static implicit operator GeneralizedTime(DateTime value) => new GeneralizedTime(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GeneralizedTime;

		static Asn1Tag IAsn1DateTime.StaticTag => Asn1PredefTag.GeneralizedTime;
		static GeneralizedTime IAsn1DateTime<GeneralizedTime>.CreateFromValue(DateTime dt) => new GeneralizedTime(dt);
	}

	public struct UtcTime : IAsn1DateTime<UtcTime>
	{
		public UtcTime(DateTime value)
		{
			this.Value = value;
		}

		public DateTime Value { get; set; }
		DateTime IAsn1DateTime.Value { get => this.Value; set => this.Value = value; }

		public static implicit operator DateTime(UtcTime dt) => dt.Value;
		public static implicit operator UtcTime(DateTime value) => new UtcTime(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UtcTime;

		static Asn1Tag IAsn1DateTime.StaticTag => Asn1PredefTag.UtcTime;
		static UtcTime IAsn1DateTime<UtcTime>.CreateFromValue(DateTime dt) => new UtcTime(dt);
	}

}
