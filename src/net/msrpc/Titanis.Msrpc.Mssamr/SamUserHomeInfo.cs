using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamUserHomeInfo
	{
		private SAMPR_USER_HOME_INFORMATION info;

		internal SamUserHomeInfo(in SAMPR_USER_HOME_INFORMATION info)
		{
			this.info = info;
		}

		public string HomeDirectory => this.info.HomeDirectory.AsString();
		public string HomeDirectoryDrive => this.info.HomeDirectoryDrive.AsString();
	}
}