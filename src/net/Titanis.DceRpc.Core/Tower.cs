using dceidl;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.DceRpc
{
	public abstract class TowerFloor
	{

	}
	public sealed class GenericTowerFloor : TowerFloor
	{
		public GenericTowerFloor(
			byte[] protocolId,
			byte[] floorData
			)
		{
			if (protocolId is null) throw new ArgumentNullException(nameof(protocolId));
			if (floorData is null) throw new ArgumentNullException(nameof(floorData));
			this.FloorData = floorData;
			this.ProtocolId = protocolId;
		}

		public byte[] ProtocolId { get; }
		public byte[] FloorData { get; }
	}
	public abstract class GuidTowerFloor : TowerFloor
	{
		protected GuidTowerFloor(Guid protocolId, int majorVersion)
		{
			this.ProtocolId = protocolId;
			this.MajorVersion = majorVersion;
		}

		public Guid ProtocolId { get; }
		public int MajorVersion { get; }
	}
	public sealed class VersionedGuidTowerFloor : GuidTowerFloor
	{
		public VersionedGuidTowerFloor(
			Guid protocolId,
			int majorVersion,
			int minorVersion
			)
			: base(protocolId, majorVersion)
		{
			this.MinorVersion = minorVersion;
		}

		public int MinorVersion { get; }
	}
	public sealed class GenericGuidTowerFloor : GuidTowerFloor
	{
		public GenericGuidTowerFloor(
			Guid protocolId,
			int majorVersion,
			byte[] floorData
			)
			: base(protocolId, majorVersion)
		{
			if (floorData is null) throw new ArgumentNullException(nameof(floorData));
			this.FloorData = floorData;
		}

		public byte[] FloorData { get; }
	}
	public abstract class ProtocolTowerFloor : TowerFloor
	{
		protected ProtocolTowerFloor(ProtocolId protocolId)
		{
			this.ProtocolId = protocolId;
		}

		public ProtocolId ProtocolId { get; }
	}
	public sealed class VersionedProtocolTowerFloor : ProtocolTowerFloor
	{
		public VersionedProtocolTowerFloor(
			ProtocolId protocolId,
			int majorVersion,
			int minorVersion) : base(protocolId)
		{
			this.MajorVersion = majorVersion;
			this.MinorVersion = minorVersion;
		}

		public int MajorVersion { get; }
		public int MinorVersion { get; }
	}
	public sealed class PortTowerFloor : ProtocolTowerFloor
	{
		public PortTowerFloor(ProtocolId protocolId, int port) : base(protocolId)
		{
			this.Port = port;
		}

		public int Port { get; }
	}
	public sealed class NamedTowerFloor : ProtocolTowerFloor
	{
		public NamedTowerFloor(ProtocolId protocolId, string pipeName) : base(protocolId)
		{
			this.Name = pipeName;
		}

		public string Name { get; }
	}
	public sealed class IpTowerFloor : ProtocolTowerFloor
	{
		public IpTowerFloor(IPAddress address) : base(ProtocolId.Ipv4)
		{
			this.Address = address;
		}

		public IPAddress Address { get; }
	}
	public sealed class GenericProtocolTowerFloor : ProtocolTowerFloor
	{
		public GenericProtocolTowerFloor(
			ProtocolId protocolId,
			byte[] floorData
			) : base(protocolId)
		{
			if (floorData is null) throw new ArgumentNullException(nameof(floorData));
			this.FloorData = floorData;
		}

		public byte[] FloorData { get; }
	}

	public class Tower
	{
		internal Tower(twr_t twr)
		{
			this._twr = twr;

			// [C706] § Appendix L - Protocol Tower Encoding

			// [C706] § L.1 - Protocol Tower Contents
			// Floor 1 - RPC interface identifier
			// Floor 2 - RPC data representation identifier
			// Floor 3 - RPC protocol identifier
			// Floor 4 - Port address
			// Floor 5 - Host address

			ByteMemoryReader reader = new ByteMemoryReader(twr.tower_octet_string);
			var floorCount = reader.ReadUInt16LE();
			TowerFloor[] floors = new TowerFloor[floorCount];
			for (int i = 0; i < floors.Length; i++)
			{
				// [C706] § Appendix I - Protocol Identifiers

				var lhsSize = reader.ReadUInt16LE();

				TowerFloor? floor = null;
				if (lhsSize > 0)
				{
					if (reader.PeekByte() == 0x0D && lhsSize == 19)
					{
						reader.ReadByte();
						var uuid = reader.ReadGuid();
						var version = reader.ReadUInt16LE();

						int rhsSize = reader.ReadUInt16LE();
						if (rhsSize == 2)
						{
							ushort minorVersion = reader.ReadUInt16LE();
							floor = new VersionedGuidTowerFloor(uuid, version, minorVersion);
							if (i == 0)
							{
								this.InterfaceId = uuid;
								this.InterfaceVersion = new RpcVersion(version, minorVersion);
							}
							else if (i == 1)
							{
								this.SyntaxId = new SyntaxId(uuid, new RpcVersion(version, minorVersion));
							}
						}
						else
							floor = new GenericGuidTowerFloor(uuid, version, reader.ReadBytes(rhsSize));
					}
					else if (lhsSize == 1)
					{
						ProtocolId protocolId = (ProtocolId)reader.ReadByte();
						int rhsSize = reader.ReadUInt16LE();
						switch ((protocolId, rhsSize))
						{
							case (ProtocolId.Tcp4, 2):
							case (ProtocolId.Udp4, 2):
								//case (ProtocolId.Spx, 2):
								{
									ushort port = reader.ReadUInt16BE();
									this.Port = port;
									this.TransportProtocol = protocolId;
									floor = new PortTowerFloor(protocolId, port);
								}
								break;
							//case (ProtocolId.Ipx, 1):
							//	{
							//		ushort port = reader.ReadByte();
							//		this.Port = port;
							//		this.TransportProtocol = protoId;
							//		floor = new PortTowerFloor(protoId, port);
							//	}
							//	break;
							case (ProtocolId.Ipv4, 4):
								{
									IPAddress address = new(reader.ReadBytes(4));
									this.NetworkAddress = address;
									this.HostName = address.ToString();
									floor = new IpTowerFloor(address);
								}
								break;
							case (ProtocolId.C706NamedPipes, _):
							case (ProtocolId.MsrpcNamedPipes, _):
							case (ProtocolId.UnixDomain, _):
							case var k when (k.protocolId == ProtocolId.Null && rhsSize is 0x24 or 0x25):
								{
									var nameBytes = reader.ReadBytes(rhsSize);
									if (nameBytes.Length > 0 && nameBytes[^1] == 0)
										rhsSize--;

									this.TransportProtocol = protocolId;
									string pipeName = Encoding.UTF8.GetString(nameBytes.Slice(0, rhsSize));
									if (i == 3 && protocolId == ProtocolId.Null)
									{
										if (pipeName.Equals("F58797F6-C9F3-4D63-9BD4-E52AC020E586", StringComparison.OrdinalIgnoreCase))
											this.HostName = ".";
										else
											this.HostName = pipeName;
									}
									else
										this.PipeName = pipeName;
									floor = new NamedTowerFloor(protocolId, pipeName);
								}
								break;
							case (ProtocolId.MsrpcNetBios, _):
								{
									var nameBytes = reader.ReadBytes(rhsSize);
									if (nameBytes.Length > 0 && nameBytes[^1] == 0)
										rhsSize--;

									this.TransportProtocol = protocolId;
									string name = Encoding.UTF8.GetString(nameBytes.Slice(0, rhsSize));
									this.HostName = name;
									floor = new NamedTowerFloor(protocolId, name);
								}
								break;
							case (ProtocolId.RpcConnectionless, 2):
							case (ProtocolId.RpcConnectionOriented, 2):
							case (ProtocolId.RpcLocal, 2):
								{
									this.RpcProtocolId = protocolId;
									ushort majorVersion = protocolId switch
									{
										ProtocolId.RpcConnectionOriented => (ushort)5,
										ProtocolId.RpcConnectionless => (ushort)4,
										_ => 0,
									};

									ushort minorVersion = reader.ReadUInt16LE();
									this.RpcProtocolVersion = new RpcVersion(majorVersion, minorVersion);
									floor = new VersionedProtocolTowerFloor(protocolId, majorVersion, minorVersion);
								}
								break;
							default:
								floor = new GenericProtocolTowerFloor(protocolId, reader.ReadBytes(rhsSize));
								break;
						}
					}
				}
				else
				{
					// Uhhh this shouldn't hapen
				}

				if (floor == null)
				{
					var lhs = reader.ReadBytes(lhsSize);
					var rhsSize = reader.ReadUInt16LE();
					var rhs = reader.ReadBytes(rhsSize);
				}

				floors[i] = floor;
				//var rhsSize = reader.ReadUInt16LE();
				//byte[] addrData = reader.ReadBytes(rhsSize);
			}

			this.Floors = floors;
		}

		public Guid InterfaceId { get; }
		public RpcVersion InterfaceVersion { get; }
		public SyntaxId SyntaxId { get; }
		public ProtocolId RpcProtocolId { get; }
		public RpcVersion RpcProtocolVersion { get; }
		public ProtocolId TransportProtocol { get; }
		public ushort Port { get; }
		public string? HostName { get; }
		public IPAddress? NetworkAddress { get; }
		public TowerFloor[] Floors { get; }
		public string? PipeName { get; }

		internal twr_t _twr;

		private string? _str;
		public sealed override string ToString() => (this._str ??= this.BuildString());
		private string BuildClunkyProtseq()
		{
			if (this.Port != 0 && this.NetworkAddress != null)
				return $"Protocol: {this.TransportProtocol}; Address: {this.NetworkAddress}; InterfaceId: {this.InterfaceId}; Version: {this.InterfaceVersion}";
			else if (this.PipeName != null)
				return $"Pipe: {this.PipeName}; InterfaceId: {this.InterfaceId}; Version: {this.InterfaceVersion}";
			else
				return $"InterfaceId: {this.InterfaceId}; Version: {this.InterfaceVersion}";
		}
		private string BuildString()
		{
			string iidstr = $"{this.InterfaceId} v{this.InterfaceVersion} @ ";
			string? protseq = iidstr + (this.TransportProtocol switch
			{
				ProtocolId.Tcp4 => $"ncacn_ip_tcp:{this.NetworkAddress}[{this.Port}]",
				ProtocolId.Udp4 => $"ncadg_ip_udp:{this.NetworkAddress}[{this.Port}]",
				ProtocolId.MsrpcNamedPipes => $"ncacn_np:[{this.PipeName}]",
				ProtocolId.C706NamedPipes => $"ncalrpc:[{this.PipeName}]",
				ProtocolId.UnixDomain => $"ncacn_hvsocket:{this.HostName}[{this.PipeName}]",
				//ProtocolId.Spx => "ncacn_spx",
				//ProtocolId.Ipx => "ncacn_nb_ipx",
				_ => BuildClunkyProtseq()
			});
			return protseq;
		}

		public static unsafe Tower EncodeIpv4(
			RpcInterfaceId abstractSyntaxId,
			SyntaxId transferSyntaxId,
			ProtocolId protocol,
			IPEndPoint ep
			)
		{
			if (ep is null)
				throw new ArgumentNullException(nameof(ep));
			if (ep.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
				throw new ArgumentException(Messages.Tower_OnlyIpv4Supported);

			byte[] towerBuf = new byte[Tcp4Tower.StructSize];
			fixed (byte* pBuf = towerBuf)
			{
				ref Tcp4Tower tower = ref *(Tcp4Tower*)pBuf;
				tower.floorCount = 5;
				// Floor 1
				tower.f1_lhsSize = 19;
				tower.f1_proto = (ProtocolId)0x0D;
				tower.f1_abstractId = abstractSyntaxId.syntaxId.if_uuid;
				tower.f1_abstractVersionMajor = abstractSyntaxId.syntaxId.if_version.Major;
				tower.f1_rhsSize = 2;
				tower.f1_abstractVersionMinor = abstractSyntaxId.syntaxId.if_version.Minor;
				// Floor 2
				tower.f2_lhsSize = 19;
				tower.f2_proto = (ProtocolId)0x0D;
				tower.f2_transferId = transferSyntaxId.if_uuid;
				tower.f2_transferVersionMajor = transferSyntaxId.if_version.Major;
				tower.f2_rhsSize = 2;
				tower.f2_transferVersionMinor = transferSyntaxId.if_version.Minor;
				// Floor 3
				tower.f3_lhsSize = 1;
				tower.f3_proto = ProtocolId.RpcConnectionOriented;
				tower.f3_rhsSize = 2;
				tower.f3_dummy = 0;
				// Floor 4
				tower.f4_lhsSize = 1;
				tower.f4_proto = protocol;
				tower.f4_rhsSize = 2;
				tower.f4_port = (ushort)IPAddress.HostToNetworkOrder((short)ep.Port);
				// Floor 4
				tower.f5_lhsSize = 1;
				tower.f5_proto = ProtocolId.Ipv4;
				tower.f5_rhsSize = 4;
				tower.f5_ipAddress = (uint)ep.Address.AsUInt32();
			}

			return new Tower(new twr_t
			{
				tower_length = (ushort)Tcp4Tower.StructSize,
				tower_octet_string = towerBuf
			});
		}

		public static unsafe Tower EncodeNamedPipe(
			RpcInterfaceId abstractSyntaxId,
			SyntaxId transferSyntaxId,
			string? pipeName
			)
		{
			byte[] towerBuf = new byte[NamedPipeTower.StructSize];
			fixed (byte* pBuf = towerBuf)
			{
				ref NamedPipeTower tower = ref *(NamedPipeTower*)pBuf;
				tower.floorCount = 5;
				// Floor 1
				tower.f1_lhsSize = 19;
				tower.f1_proto = (ProtocolId)0x0D;
				tower.f1_abstractId = abstractSyntaxId.syntaxId.if_uuid;
				tower.f1_abstractVersionMajor = abstractSyntaxId.syntaxId.if_version.Major;
				tower.f1_rhsSize = 2;
				tower.f1_abstractVersionMinor = abstractSyntaxId.syntaxId.if_version.Minor;
				// Floor 2
				tower.f2_lhsSize = 19;
				tower.f2_proto = (ProtocolId)0x0D;
				tower.f2_transferId = transferSyntaxId.if_uuid;
				tower.f2_transferVersionMajor = transferSyntaxId.if_version.Major;
				tower.f2_rhsSize = 2;
				tower.f2_transferVersionMinor = transferSyntaxId.if_version.Minor;
				// Floor 3
				tower.f3_lhsSize = 1;
				tower.f3_proto = ProtocolId.RpcConnectionOriented;
				tower.f3_rhsSize = 2;
				tower.f3_dummy = 0;
				// Floor 4
				tower.f4_lhsSize = 1;
				tower.f4_proto = ProtocolId.MsrpcNamedPipes;
				tower.f4_rhsSize = 0;
				//tower.f4_pipeName = 0x00785C455049505C;
				// 5C 50 49 50 45 5C 78 00 => \PIPE\x
				// Floor 5
				tower.f5_lhsSize = 1;
				tower.f5_proto = ProtocolId.NetbiosAll;
				tower.f5_rhsSize = 0;
				//tower.f5_ipAddress = 0;
			}

			return new Tower(new twr_t
			{
				tower_length = (ushort)NamedPipeTower.StructSize,
				tower_octet_string = towerBuf
			});
		}

		public unsafe IPEndPoint? TryExtractIPEndpoint()
		{
			ByteMemoryReader reader = new ByteMemoryReader(this._twr.tower_octet_string);
			ushort floorCount = reader.ReadUInt16();
			if (floorCount == 5)
			{
				reader.SkipFloor();
				reader.SkipFloor();
				reader.SkipFloor();

				if (
					reader.TryReadTcpPortFloor(out ushort port)
					&& reader.TryReadIpv4AddressFloor(out IPAddress addr)
					)
				{
					return new IPEndPoint(addr, port);
				}
			}
			return null;

		}
	}

	static class TowerReaderExtensions
	{
		internal static void SkipFloor(this ByteMemoryReader reader)
		{
			ushort lhsSize = reader.ReadUInt16();
			reader.Advance(lhsSize);
			ushort rhsSize = reader.ReadUInt16();
			reader.Advance(rhsSize);
		}

		internal static bool TryReadTcpPortFloor(this ByteMemoryReader reader, out ushort port)
		{
			int startPos = reader.Position;

			ushort lhsSize = reader.ReadUInt16();
			if (lhsSize == 1)
			{
				ProtocolId proto = (ProtocolId)reader.ReadByte();
				if (proto == ProtocolId.Tcp4)
				{
					ushort rhsSize = reader.ReadUInt16();
					if (rhsSize == 2)
					{
						port = reader.ReadUInt16BE();
						return true;
					}
				}
			}

			reader.Position = startPos;
			port = 0;
			return false;
		}

		internal static bool TryReadIpv4AddressFloor(this ByteMemoryReader reader, out IPAddress addr)
		{
			int startPos = reader.Position;

			ushort lhsSize = reader.ReadUInt16();
			if (lhsSize == 1)
			{
				ProtocolId proto = (ProtocolId)reader.ReadByte();
				if (proto == ProtocolId.Ipv4)
				{
					ushort rhsSize = reader.ReadUInt16();
					if (rhsSize == 4)
					{
						uint addrValue = reader.ReadUInt32();
						addr = new IPAddress(addrValue);
						return true;
					}
				}
			}

			reader.Position = startPos;
			addr = null;
			return false;
		}
	}

	// [C706] § Appendix I
	public enum ProtocolId : byte
	{
		OsiTp4 = 5,
		OsiClns = 6,
		Tcp4 = 7,
		Udp4 = 8,
		Ipv4 = 9,
		RpcConnectionless = 0x0A,
		RpcConnectionOriented = 0x0B,
		// Observed with towers using ncalrpc
		RpcLocal = 0x0C,
		DnaSessionControl = 2,
		DnaSessionControlV3 = 3,
		DnaNspTransport = 4,
		DnaRouting = 6,
		MsrpcNamedPipes = 0x0F, // [MS-RPCE] § 2.1.1.2 - SMB (NCACN_NP)
		C706NamedPipes = 0x10,  // Actually appears to be LRPC
		C706NetBios = 0x11,
		MsrpcNetBios = 0x12, // [MS-RPCE] § 2.1.1.5 - NetBIOS over TCP (NCACN_NB_TCP)
		Spx = 0x13,
		Ipx = 0x14,
		AppletalkStream = 0x16,
		AppletalkDatagram = 0x17,
		Appletalk = 0x18,
		NetbiosAll = 0x19,
		VinesSpp = 0x1A,
		VinesIpc = 0x1B,
		StreetTalk = 0x1C,
		UnixDomain = 0x20,
		Null = 0x21,
		NetbiosName = 0x22
	}

	static class IPAddressExtensions
	{
		public static uint AsUInt32(this IPAddress addr)
		{
			// TODO: Figure this out without allocating an array
			return BitConverter.ToUInt32(addr.GetAddressBytes(), 0);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Tcp4Tower
	{
		internal unsafe static int StructSize => sizeof(Tcp4Tower);

		internal ushort floorCount;

		// Floor 1: Abstract syntax
		internal ushort f1_lhsSize;
		internal ProtocolId f1_proto;
		internal Guid f1_abstractId;
		internal ushort f1_abstractVersionMajor;
		internal ushort f1_rhsSize;
		internal ushort f1_abstractVersionMinor;
		// Floor 2: Transfer syntax
		internal ushort f2_lhsSize;
		internal ProtocolId f2_proto;
		internal Guid f2_transferId;
		internal ushort f2_transferVersionMajor;
		internal ushort f2_rhsSize;
		internal ushort f2_transferVersionMinor;
		// Floor 3: RPC protocol (CO/CL)
		internal ushort f3_lhsSize;
		internal ProtocolId f3_proto;
		internal ushort f3_rhsSize;
		internal ushort f3_dummy;
		// Floor 4: Transport protocol
		internal ushort f4_lhsSize;
		internal ProtocolId f4_proto;
		internal ushort f4_rhsSize;
		internal ushort f4_port;
		// Floor 5: Transport address
		internal ushort f5_lhsSize;
		internal ProtocolId f5_proto;
		internal ushort f5_rhsSize;
		internal uint f5_ipAddress;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct NamedPipeTower
	{
		internal unsafe static int StructSize => sizeof(NamedPipeTower);

		internal ushort floorCount;

		// Floor 1: Abstract syntax
		internal ushort f1_lhsSize;
		internal ProtocolId f1_proto;
		internal Guid f1_abstractId;
		internal ushort f1_abstractVersionMajor;
		internal ushort f1_rhsSize;
		internal ushort f1_abstractVersionMinor;
		// Floor 2: Transfer syntax
		internal ushort f2_lhsSize;
		internal ProtocolId f2_proto;
		internal Guid f2_transferId;
		internal ushort f2_transferVersionMajor;
		internal ushort f2_rhsSize;
		internal ushort f2_transferVersionMinor;
		// Floor 3: RPC protocol (CO/CL)
		internal ushort f3_lhsSize;
		internal ProtocolId f3_proto;
		internal ushort f3_rhsSize;
		internal ushort f3_dummy;
		// Floor 4: Transport protocol
		internal ushort f4_lhsSize;
		internal ProtocolId f4_proto;
		internal ushort f4_rhsSize;
		//internal ulong f4_pipeName;
		//internal byte f4_dummy;
		// Floor 5: Transport address
		internal ushort f5_lhsSize;
		internal ProtocolId f5_proto;
		internal ushort f5_rhsSize;
		//internal uint f5_ipAddress;
	}
}
