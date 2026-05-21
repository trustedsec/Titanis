using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Smb2
{
	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct SockaddrStorage
	{
		internal ushort family;
		[PduArraySize(126)]
		internal byte[] data;

		public AddressFamily Family => (AddressFamily)family;

		internal unsafe SockaddrIpv4 AsIpv4()
		{
			fixed (byte* pData = this.data)
			{
				return *(SockaddrIpv4*)pData;
			}
		}

		internal unsafe SockaddrIpv6 AsIpv6()
		{
			fixed (byte* pData = this.data)
			{
				return *(SockaddrIpv6*)pData;
			}
		}

		public IPEndPoint ToIPEndPoint()
		{
			switch (this.Family)
			{
				case AddressFamily.InterNetwork:
					return this.AsIpv4().ToIPEndPoint();
				case AddressFamily.InterNetworkV6:
					return this.AsIpv6().ToIPEndPoint();
				default:
					throw new FormatException(Messages.Sockaddr_InvalidFamily);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct SockaddrIpv4
	{
		internal static unsafe int StructSize => sizeof(SockaddrIpv4);

		internal ushort port;
		internal uint ipv4Address;

		internal IPEndPoint ToIPEndPoint()
		{
			return new IPEndPoint(new IPAddress(this.ipv4Address), this.port);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct SockaddrIpv6
	{
		internal static unsafe int StructSize => sizeof(SockaddrIpv6);

		internal ushort port;
		internal uint flowInfo;
		internal unsafe fixed byte ipv4Address[16];
		internal uint scopeId;

		internal unsafe byte[] GetAddressData()
		{
			byte[] data = new byte[16];
			fixed (byte* pData = this.ipv4Address)
			{
				Marshal.Copy(new IntPtr(pData), data, 0, 16);
			}
			return data;
		}

		internal IPEndPoint ToIPEndPoint()
		{
			return new IPEndPoint(new IPAddress(this.GetAddressData(), this.scopeId), this.port);
		}
	}

	[Flags]
	public enum Smb2NicCapabilities : uint
	{
		RssCapable = 1,
		RdmaCapable = 2,
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2NicInfoStruct
	{
		internal static unsafe int StructSize => sizeof(Smb2NicInfoStruct);

		internal int next;
		internal int ifIndex;
		internal Smb2NicCapabilities caps;
		internal uint reserved;
		internal long linkSpeed;

		internal SockaddrStorage sockaddr;
	}
}
