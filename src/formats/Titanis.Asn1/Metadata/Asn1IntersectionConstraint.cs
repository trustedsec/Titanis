using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Metadata
{
	public class Asn1IntersectionConstraint : Asn1Constraint
	{
		public Asn1IntersectionConstraint(Asn1Constraint left, Asn1Constraint right)
		{
			ArgumentNullException.ThrowIfNull(left);
			ArgumentNullException.ThrowIfNull(right);
			this.Left = left;
			this.Right = right;
		}

		public Asn1Constraint Left { get; }
		public Asn1Constraint Right { get; }
	}
}
