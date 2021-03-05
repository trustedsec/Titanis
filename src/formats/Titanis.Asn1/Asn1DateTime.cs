using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public interface IAsn1DateTime : IAsn1Tag
	{
		DateTime Value { get; set; }
	}


	public struct Asn1Date : IAsn1DateTime
	{
		public Asn1Date(DateTime value)
		{
			this.value = value;
		}

		public DateTime value;
		DateTime IAsn1DateTime.Value { get => this.value; set => this.value = value; }

		public static implicit operator DateTime(Asn1Date dt) => dt.value;
		public static implicit operator Asn1Date(DateTime value) => new Asn1Date(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Date;
	}

	public struct Asn1Time : IAsn1DateTime
	{
		public Asn1Time(DateTime value)
		{
			this.value = value;
		}

		public DateTime value;
		DateTime IAsn1DateTime.Value { get => this.value; set => this.value = value; }

		public static implicit operator DateTime(Asn1Time dt) => dt.value;
		public static implicit operator Asn1Time(DateTime value) => new Asn1Time(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.Time;
	}

	public struct TimeOfDay : IAsn1DateTime
	{
		public TimeOfDay(DateTime value)
		{
			this.value = value;
		}

		public DateTime value;
		DateTime IAsn1DateTime.Value { get => this.value; set => this.value = value; }

		public static implicit operator DateTime(TimeOfDay dt) => dt.value;
		public static implicit operator TimeOfDay(DateTime value) => new TimeOfDay(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.TimeOfDay;
	}

	public struct GeneralizedTime : IAsn1DateTime
	{
		public GeneralizedTime(DateTime value)
		{
			this.value = value;
		}

		public DateTime value;
		DateTime IAsn1DateTime.Value { get => this.value; set => this.value = value; }

		public static implicit operator DateTime(GeneralizedTime dt) => dt.value;
		public static implicit operator GeneralizedTime(DateTime value) => new GeneralizedTime(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.GeneralizedTime;
	}

	public struct UtcTime : IAsn1DateTime
	{
		public UtcTime(DateTime value)
		{
			this.value = value;
		}

		public DateTime value;
		DateTime IAsn1DateTime.Value { get => this.value; set => this.value = value; }

		public static implicit operator DateTime(UtcTime dt) => dt.value;
		public static implicit operator UtcTime(DateTime value) => new UtcTime(value);

		Asn1Tag IAsn1Tag.Tag => Asn1PredefTag.UtcTime;
	}

}
