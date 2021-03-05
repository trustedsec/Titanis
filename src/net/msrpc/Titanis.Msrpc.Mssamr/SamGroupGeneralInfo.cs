using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamGroupGeneralInfo
	{
		private readonly SAMPR_GROUP_GENERAL_INFORMATION info;

		internal SamGroupGeneralInfo(SAMPR_GROUP_GENERAL_INFORMATION info)
		{
			this.info = info;
		}

		public string GroupName => this.info.Name.AsString();
		public SamGroupAttributes Attributes => (SamGroupAttributes)this.info.Attributes;
		public int MemberCount => (int)this.info.MemberCount;
		public string AdminComment => this.info.AdminComment.AsString();
	}
}