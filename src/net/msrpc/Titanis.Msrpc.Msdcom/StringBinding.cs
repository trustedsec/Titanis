using System.Text.RegularExpressions;

namespace Titanis.Msrpc.Msdcom
{
	// [MS-DCOM] § 2.2.19.3 - STRINGBINDING
	public sealed class StringBinding
	{
		public StringBinding(ushort towerId, string networkAddress)
		{
			this.TowerId = towerId;
			this.NetworkAddress = networkAddress;

			if (!string.IsNullOrEmpty(networkAddress))
			{
				var m = rgxTcpBinding.Match(networkAddress);
				this.HostName = m.Groups["h"].Value;

				var portGroup = m.Groups["p"];
				if (portGroup.Success)
					this.Port = int.Parse(portGroup.Value);
			}
		}

		private static readonly Regex rgxTcpBinding = new Regex(@"^(?<h>[^[]*)\[(?<p>\d+)\]$");

		public sealed override string ToString() => this.NetworkAddress;

		public ushort TowerId { get; }
		public string NetworkAddress { get; }

		public string? HostName { get; }
		public int Port { get; }

		public bool IsValid => !string.IsNullOrEmpty(this.HostName) && this.Port != 0;
	}
}
