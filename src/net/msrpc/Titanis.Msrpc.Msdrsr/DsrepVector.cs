namespace Titanis.Msrpc.Msdrsr
{
	public class DsrepVector
	{
		internal DsrepVector(
			Guid dsaGuid,
			long highPropertyUpdateUsn)
		{
			DsaGuid = dsaGuid;
			HighPropertyUpdateUsn = highPropertyUpdateUsn;
		}

		public Guid DsaGuid { get; }
		public long HighPropertyUpdateUsn { get; }
	}
}