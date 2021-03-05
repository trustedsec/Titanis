namespace Titanis.Asn1.Metadata
{
	public class Asn1ValueConstraint : Asn1Constraint
	{
		public object Value { get; }

		public Asn1ValueConstraint(object value)
		{
			this.Value = value;
		}
	}
}