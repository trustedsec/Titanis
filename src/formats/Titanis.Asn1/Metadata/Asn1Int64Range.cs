namespace Titanis.Asn1.Metadata
{
	public struct Asn1Int64Range
	{
		public Asn1Int64Range(long? min, long? max)
		{
			this.Min = min;
			this.Max = max;
		}

		public long? Min { get; private set; }
		public long? Max { get; private set; }
	}
}