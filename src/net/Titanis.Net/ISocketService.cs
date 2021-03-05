using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Titanis.Net;

namespace Titanis.Net
{
	/// <summary>
	/// Provides functionality to create sockets.
	/// </summary>
	public interface ISocketService
	{
		/// <summary>
		/// Creates a socket.
		/// </summary>
		/// <param name="addressFamily"></param>
		/// <param name="socketType"></param>
		/// <param name="protocolType"></param>
		/// <returns>An object implementing <see cref="ISocket"/> with the requested values.</returns>
		ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType);
	}
}

public static class SocketServiceExtensions
{
	/// <summary>
	/// Creates a TCP socket.
	/// </summary>
	/// <param name="sockets"><see cref="ISocketService"/> instance</param>
	/// <param name="addressFamily">Address family (either <see cref="AddressFamily.InterNetwork"/> or <see cref="AddressFamily.InterNetworkV6"/>)</param>
	/// <returns>An object implementing <see cref="ISocket"/> with the requested values.</returns>
	/// <remarks>
	/// <paramref name="addressFamily"/> is not validated, so the caller is free to choose
	/// other <see cref="AddressFamily"/> values.
	/// </remarks>
	public static ISocket CreateTcpSocket(this ISocketService sockets, AddressFamily addressFamily)
	{
		return sockets.CreateSocket(addressFamily, SocketType.Stream, ProtocolType.Tcp);
	}
	/// <summary>
	/// Creates a TCP socket.
	/// </summary>
	/// <param name="sockets"><see cref="ISocketService"/> instance</param>
	/// <returns>An object implementing <see cref="ISocket"/> over TCP over IPv4.</returns>
	public static ISocket CreateTcp4Socket(this ISocketService sockets)
	{
		return sockets.CreateSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}
	/// <summary>
	/// Creates a TCP socket.
	/// </summary>
	/// <param name="sockets"><see cref="ISocketService"/> instance</param>
	/// <returns>An object implementing <see cref="ISocket"/> over TCP over IPv6.</returns>
	public static ISocket CreateTcp6Socket(this ISocketService sockets)
	{
		return sockets.CreateSocket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
	}
}