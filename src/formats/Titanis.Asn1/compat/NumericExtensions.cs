using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Titanis.Asn1.compat
{
	static class NumericExtensions
	{
		internal static int GetByteCount(this BigInteger n)
		{
			return n.ToByteArray().Length;
		}
	}
}
