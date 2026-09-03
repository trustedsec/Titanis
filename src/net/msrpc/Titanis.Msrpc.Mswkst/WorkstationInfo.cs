namespace Titanis.Msrpc.Mswkst
{
	public class WorkstationInfo : IWantServerName
	{
		public string? ServerName { get; set; }
		public Platform? Platform { get; set; }
		public int? OutgoingSmbConnectionIdleTimeout { get; set; }
		public int? MaxCommands { get; set; }
		public int? IncomingSmbConnectionIdleTimeout { get; set; }
		public int? DormantFileLimit { get; set; }
		public string? ComputerName { get; internal set; }
		public string? Domain { get; internal set; }
		public Version OsVersion { get; internal set; }
		public uint LoggedOnUserCount { get; internal set; }
	}
}
