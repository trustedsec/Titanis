using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.Net;

namespace Titanis.Socks
{
	struct SocketInfo
	{
		internal AddressFamily addressFamily;
		internal SocketType socketType;
		internal ProtocolType protocolType;

		public SocketInfo(
			AddressFamily addressFamily,
			SocketType socketType,
			ProtocolType protocolType)
		{
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
		}
	}

	/// <summary>
	/// Implements a <see cref="ISocketService"/> as a SOCKS client.
	/// </summary>
	/// <remarks>
	/// Call <see cref="CreateSocket(AddressFamily, SocketType, ProtocolType)"/> to
	/// create a socket that connects through the upsteram SOCKS server.
	/// </remarks>
	public class SocksClient : ISocketService
	{
		private readonly ISocketService _socketService;
		private readonly INameResolverService? _nameResolver;

		public SocksClient(
			IPEndPoint serverEP,
			ISocketService? socketService = null,
			INameResolverService? nameResolver = null,
			ILog? log = null
			)
		{
			ArgumentNullException.ThrowIfNull(serverEP);

			this.ServerEP = serverEP;
			this._socketService = socketService ?? Singleton.SingleInstance<PlatformSocketService>();
			this._nameResolver = nameResolver ?? new PlatformNameResolverService(log: log);
		}

		/// <summary>
		/// Gets the endpoint of the SOCKS server.
		/// </summary>
		public IPEndPoint ServerEP { get; }

		/// <inheritdoc/>
		public ISocket CreateSocket(
			AddressFamily addressFamily,
			SocketType socketType,
			ProtocolType protocolType
			)
		{
			return new SocksSocket(this, new SocketInfo(addressFamily, socketType, protocolType));
		}

		internal ISocket CreateSocket(SocketInfo socketInfo)
		{
			return this._socketService.CreateSocket(socketInfo.addressFamily, socketInfo.socketType, socketInfo.protocolType);
		}
	}
}
