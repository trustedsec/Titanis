namespace Titanis.Msrpc.Msscmr
{
	public class ServiceConfig
	{
		public ServiceTypes ServiceType { get; set; }
		public ServiceStartType StartType { get; set; }
		public ServiceErrorControl ErrorControl { get; set; }
		public string BinaryPathName { get; set; }
		public string LoadOrderGroup { get; set; }
		public int TagId { get; set; }
		public string[] Dependencies { get; set; }
		public string ServiceStartName { get; set; }
		public string DisplayName { get; set; }

		public string StartPassword { get; set; }
	}
}