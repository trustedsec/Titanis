using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public static class Asn1BuiltinValues
	{
		public static object True = true;
		public static object False = false;

		public static object RealZero = 0.0;
		public static object NaN = double.NaN;
		public static object PlusInfinity = double.PositiveInfinity;
		public static object MinusInfinity = double.NegativeInfinity;
	}
}
