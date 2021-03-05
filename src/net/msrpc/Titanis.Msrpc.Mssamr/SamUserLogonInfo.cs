using ms_samr;
using System;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	public class SamUserLogonInfo
	{
		private SAMPR_USER_LOGON_INFORMATION info;

		internal SamUserLogonInfo(in SAMPR_USER_LOGON_INFORMATION info)
		{
			this.info = info;
		}

		public DateTime? LastLogon => this.info.LastLogon.AsNullableDateTime();
		public DateTime? LastLogoff => this.info.LastLogoff.AsNullableDateTime();
		public DateTime? PasswordLastSet => this.info.PasswordLastSet.AsNullableDateTime();
		public DateTime? PasswordCanChange => this.info.PasswordCanChange.AsNullableDateTime();
		public DateTime? PasswordMustChange => this.info.PasswordMustChange.AsNullableDateTime();
		public string UserName => this.info.UserName.AsString();
		public string FullName => this.info.FullName.AsString();
		public string HomeDirectory => this.info.HomeDirectory.AsString();
		public string HomeDirectoryDrive => this.info.HomeDirectoryDrive.AsString();
		public string ScriptPath => this.info.ScriptPath.AsString();
		public string ProfilePath => this.info.ProfilePath.AsString();
		public string WorkStations => this.info.WorkStations.AsString();
		public uint UserId => this.info.UserId;
		public uint PrimaryGroupId => this.info.PrimaryGroupId;
		public SamUserAccountFlags UserAccountControl => (SamUserAccountFlags)this.info.UserAccountControl;
		public int BadPasswordCount => this.info.BadPasswordCount;
		public int LogonCount => this.info.LogonCount;
		// TODO: Logon hours
	}
}