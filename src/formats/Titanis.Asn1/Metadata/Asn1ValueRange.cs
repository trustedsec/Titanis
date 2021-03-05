using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public class Asn1ValueRange
	{
		public Asn1ValueRange(object minValue, bool includesMinValue, object maxValue, bool includesMaxValue)
		{
			this.MinValue = minValue;
			this.IncludesMinValue = includesMinValue;
			this.MaxValue = maxValue;
			this.IncludesMaxValue = includesMaxValue;
		}

		public object MinValue { get; private set; }
		public bool IncludesMinValue { get; private set; }
		public object MaxValue { get; private set; }
		public bool IncludesMaxValue { get; set; }
	}
}
