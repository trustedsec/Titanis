using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace Titanis.Smb2
{
	public class Smb2NicInfo
	{
		internal Smb2NicInfoStruct info;

		internal Smb2NicInfo() { }

		[DisplayName("Interface Index")]
		public int InterfaceIndex => this.info.ifIndex;
		public Smb2NicCapabilities Capabilities => this.info.caps;
		[DisplayName("Link Speed")]
		public long LinkSpeed => this.info.linkSpeed;

		[DisplayName("Address Family")]
		public AddressFamily AddressFamily => this.info.sockaddr.Family;

		private IPEndPoint _ep;
		public IPEndPoint EndPoint => (this._ep ??= this.info.sockaddr.ToIPEndPoint());
	}
}
