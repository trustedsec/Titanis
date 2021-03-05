namespace Titanis.Security.Ntlm
{
	public enum NtlmAuthRecordOptions
	{
		None = 0,
		HasLmKey = 1,
		HasNtKey = 2,
	}

	public class NtlmAuthRecord
	{
		public Buffer128 LmKey { get; set; }
		public Buffer128 NtKey { get; set; }

		public NtlmAuthRecordOptions Options { get; set; }
		public bool HasLmKey => (0 != (this.Options & NtlmAuthRecordOptions.HasLmKey));
		public bool HasNtKey => (0 != (this.Options & NtlmAuthRecordOptions.HasNtKey));

		public NtlmAuthRecord(Buffer128 lmkey, Buffer128 ntkey)
		{
			this.LmKey = lmkey;
			this.NtKey = ntkey;
			this.Options =
				(lmkey.IsEmpty ? 0 : NtlmAuthRecordOptions.HasLmKey)
				| (ntkey.IsEmpty ? 0 : NtlmAuthRecordOptions.HasNtKey)
				;
		}
		public NtlmAuthRecord(Buffer128 lmkey, Buffer128 ntkey, NtlmAuthRecordOptions options)
		{
			this.LmKey = lmkey;
			this.NtKey = ntkey;
			this.Options = options;
		}
	}
}