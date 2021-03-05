using System;
using System.Collections.Generic;

namespace Titanis.Security.Ntlm
{

	public class NtlmAuthStore : INtlmAuthStore
	{
		private Dictionary<string, NtlmAuthRecord> _records = new Dictionary<string, NtlmAuthRecord>(StringComparer.OrdinalIgnoreCase);

		public NtlmAuthStore()
		{
			this._records = new Dictionary<string, NtlmAuthRecord>();
		}
		public NtlmAuthStore(Dictionary<string, NtlmAuthRecord> records)
		{
			if (records is null)
				throw new ArgumentNullException(nameof(records));

			this._records = records;
		}

		public void AddUserAuthRecord(string userName, NtlmAuthRecord record)
		{
			this._records.Add(userName, record);
		}

		public NtlmAuthRecord GetUserAuthRecord(string userName)
		{
			return this._records[userName];
		}
	}
}