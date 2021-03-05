using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamUserPreferencesInfo
	{
		private SAMPR_USER_PREFERENCES_INFORMATION info;

		internal SamUserPreferencesInfo(in SAMPR_USER_PREFERENCES_INFORMATION info)
		{
			this.info = info;
		}

		public string UserComment => this.info.UserComment.AsString();
		public int CountryCode => this.info.CountryCode;
		public int CodePage => this.info.CodePage;
	}
}