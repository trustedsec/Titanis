using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamUserGeneralInfo
	{
		private SAMPR_USER_GENERAL_INFORMATION info;

		internal SamUserGeneralInfo(in SAMPR_USER_GENERAL_INFORMATION info)
		{
			this.info = info;
		}

		public string UserName => this.info.UserName.AsString();
		public string FullName => this.info.FullName.AsString();
		public uint PrimaryGroupId => this.info.PrimaryGroupId;
		public string AdminComment => this.info.AdminComment.AsString();
		public string UserComment => this.info.UserComment.AsString();
	}
}