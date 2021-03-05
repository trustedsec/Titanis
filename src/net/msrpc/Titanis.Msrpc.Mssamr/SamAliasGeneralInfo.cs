using ms_samr;
using System;

namespace Titanis.Msrpc.Mssamr
{
	public class SamAliasGeneralInfo
	{
		private readonly SAMPR_ALIAS_GENERAL_INFORMATION info;

		internal SamAliasGeneralInfo(SAMPR_ALIAS_GENERAL_INFORMATION info)
		{
			this.info = info;
		}

		public string AliasName => this.info.Name.AsString();
		public int MemberCount => (int)this.info.MemberCount;
		public string AdminComment => this.info.AdminComment.AsString();
	}
}