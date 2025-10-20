using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Metadata
{
	public class Asn1UnionConstraint : Asn1ValueSetConstraint
	{
		public Asn1UnionConstraint(Asn1Constraint left, Asn1Constraint right)
		{
			ArgumentNullException.ThrowIfNull(left);
			ArgumentNullException.ThrowIfNull(right);
			this.Left = left;
			this.Right = right;
		}

		public Asn1Constraint Left { get; }
		public Asn1Constraint Right { get; }

		public override Asn1Int64Range? TryGetInt64Range()
		{
			var l = (this.Left as Asn1ValueSetConstraint)?.TryGetInt64Range();
			var r = (this.Right as Asn1ValueSetConstraint)?.TryGetInt64Range();

			if (l.HasValue && r.HasValue)
			{
				return new Asn1Int64Range(
					(l.Value.Min is null || l.Value.Min < r.Value.Min) ? l.Value.Min : r.Value.Min,
					true,
					(l.Value.Max is null || l.Value.Max > r.Value.Max) ? l.Value.Max : r.Value.Max,
					true
					);
			}
			else
				return null;
		}

		public override Asn1UInt64Range? TryGetUInt64Range()
		{
			var l = (this.Left as Asn1ValueSetConstraint)?.TryGetUInt64Range();
			var r = (this.Right as Asn1ValueSetConstraint)?.TryGetUInt64Range();

			if (l.HasValue && r.HasValue)
			{
				return new Asn1UInt64Range(
					(l.Value.Min is null || l.Value.Min < r.Value.Min) ? l.Value.Min : r.Value.Min,
					true,
					(l.Value.Max is null || l.Value.Max > r.Value.Max) ? l.Value.Max : r.Value.Max,
					true
					);
			}
			else
				return null;
		}
	}
}
