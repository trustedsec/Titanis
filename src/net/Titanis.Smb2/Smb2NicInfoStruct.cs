using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Smb2
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct SockaddrStorage
	{
		internal static unsafe int StructSize => sizeof(SockaddrStorage);

		internal ushort family;
		internal unsafe fixed byte data[126];

		public AddressFamily Family => (AddressFamily)family;

		internal unsafe ref SockaddrIpv4 AsIpv4()
		{
			fixed (byte* pData = this.data)
			{
				return ref *(SockaddrIpv4*)pData;
			}
		}

		internal unsafe ref SockaddrIpv6 AsIpv6()
		{
			fixed (byte* pData = this.data)
			{
				return ref *(SockaddrIpv6*)pData;
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

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2NicInfoStruct
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
