namespace Titanis.Msrpc.Mswkst
{
	public class LoggedOnUserInfo : IWantServerName
	{
		public string? ServerName { get; set; }
		public string? UserName { get; internal set; }
		public string? LogonDomain { get; internal set; }
		public string? OtherDomain { get; internal set; }
		public string? LogonServer { get; internal set; }
	}
}
