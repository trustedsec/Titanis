using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Metadata
{
	public abstract class Asn1ValueSetConstraint : Asn1Constraint
	{
		public abstract Asn1Int64Range? TryGetInt64Range();
		public abstract Asn1UInt64Range? TryGetUInt64Range();
	}
}
