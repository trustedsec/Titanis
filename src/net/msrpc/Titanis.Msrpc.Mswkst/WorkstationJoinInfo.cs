namespace Titanis.Msrpc.Mswkst
{
	public class WorkstationJoinInfo : IWantServerName
	{
		public string? ServerName { get; set; }
		public string? Domain { get; internal set; }
		public JoinStatus Status { get; internal set; }
	}
}
