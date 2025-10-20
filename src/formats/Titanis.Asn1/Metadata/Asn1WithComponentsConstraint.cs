using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Metadata
{
	public class Asn1WithComponentsConstraint : Asn1Constraint
	{
		public Asn1WithComponentsConstraint(Dictionary<string, Asn1Constraint> constraints, bool isPartial)
		{
			ArgumentNullException.ThrowIfNull(constraints);
			this.IsPartial = isPartial;
			this.Constraints = constraints;
		}

		public bool IsPartial { get; }
		public Dictionary<string, Asn1Constraint> Constraints { get; }
	}
}
