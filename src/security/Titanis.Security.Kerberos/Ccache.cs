using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.IO;
using Titanis.PduStruct;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct CCacheHeader
	{
		internal ushort headerSize;

		[PduArraySize(nameof(headerSize))]
		internal byte[] headerData;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCacheData
	{
		internal int length;
		[PduArraySize(nameof(length))]
		internal byte[] bytes;

		public CCacheData() { }
		public CCacheData(byte[] bytes)
		{
			this.length = bytes.Length;
			this.bytes = bytes;
		}

		public sealed override string ToString()
			=> this.bytes?.ToHexString();
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCacheStringData
	{
		public CCacheStringData() { }
		public CCacheStringData(string str)
		{
			this.str = str;
			this.length = str.Length;
		}

		internal int length;
		[PduString(CharSet.Ansi, nameof(length))]
		internal string str;

		public sealed override string ToString()
			=> this.str;
	}


	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCachePrincipal
	{
		[PduParameter]
		internal byte version;

		private bool OverVersion1 => this.version > 1;

		[PduConditional(nameof(OverVersion1))]
		internal PrincipalNameType nameType;

		internal int componentCount;
		internal CCacheStringData realm;

		private int ActualComponentCount => this.componentCount;
		[PduArraySize(nameof(ActualComponentCount))]
		internal CCacheStringData[] components;

		public sealed override string ToString()
			=> $"{this.nameType}: {string.Join("/", this.components.Select(r => r.str))}";

		internal static CCachePrincipal FromTicketClient(TicketInfo ticket)
		{
			return new CCachePrincipal()
			{
				version = 4,
				componentCount = 1,
				nameType = PrincipalNameType.Principal,
				realm = new CCacheStringData(ticket.UserRealm),
				components = new CCacheStringData[]
				{
					new CCacheStringData(ticket.UserName)
				},
			};
		}

		internal static CCachePrincipal FromTicketService(TicketInfo ticket)
		{
			return new CCachePrincipal()
			{
				version = 4,
				componentCount = 2,
				nameType = ticket.TargetSpn.NameType,
				realm = new CCacheStringData(ticket.ServiceRealm),
				components = Array.ConvertAll(ticket.TargetSpn.GetNameParts(), r => new CCacheStringData(r))
			};
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct CCacheKeyBlock
	{
		internal EType encType;
		internal CCacheData keyData;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct CCacheAddress
	{
		internal ushort addrType;
		internal CCacheData addrData;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct CCacheAuthData
	{
		internal PadataType authType;
		internal CCacheData authData;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCacheCredential
	{
		[PduParameter]
		internal byte version;

		[PduArguments(nameof(version))]
		internal CCachePrincipal client;
		[PduArguments(nameof(version))]
		internal CCachePrincipal server;
		internal CCacheKeyBlock key;
		internal int authTime;
		internal int startTime;
		internal int endTime;
		internal int renewTill;
		internal byte isSKey;
		internal KdcOptions ticketFlags;

		internal int addressCount;
		[PduArraySize(nameof(addressCount))]
		internal CCacheAddress[] addresses;

		internal int authDataCount;
		[PduArraySize(nameof(authDataCount))]
		internal CCacheAuthData[] authData;

		internal CCacheData ticket;
		internal CCacheData ticket2;
	}

	partial struct CCacheCredentialList : IPduStruct<byte>
	{
		[PduParameter]
		private byte version;

		internal CCacheCredential[] credentials;

		public void ReadFrom(IByteSource reader, byte version)
			=> this.ReadFrom(reader, PduByteOrder.BigEndian, version);

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder, byte version)
		{
			List<CCacheCredential> creds = new List<CCacheCredential>();
			while (reader.RemainingLength() > 0)
			{
				var cred = reader.ReadPduStruct<CCacheCredential, byte>(byteOrder, version);
				creds.Add(cred);
			}

			this.credentials = creds.ToArray();
		}

		public void WriteTo(ByteWriter writer, byte version)
			=> this.WriteTo(writer, PduByteOrder.BigEndian, version);

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder, byte version)
		{
			if (this.credentials != null)
			{
				foreach (var cred in this.credentials)
				{
					writer.WritePduStruct(cred, byteOrder, version);
				}
			}
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCache
	{
		internal byte format;
		internal byte version;

		private bool HasHeader => (this.version == 4);
		[PduConditional(nameof(HasHeader))]
		internal CCacheHeader? header;

		[PduArguments(nameof(version))]
		internal CCachePrincipal defaultPrincipal;

		[PduArguments(nameof(version))]
		internal CCacheCredentialList credList;
	}

	internal static class CCacheExtensions
	{
		public static int ToCCacheDateTime(this DateTime dt)
		{
			return (int)(dt - TicketParameters.DefaultEndTime).TotalSeconds;
		}
	}

	class CCacheTicketWrapper : Asn1Explicit<Ticket_Ticket>
	{
		public CCacheTicketWrapper()
		{
			this.Tag = new Asn1Tag(0x61);
		}
		public CCacheTicketWrapper(Ticket_Ticket ticket)
			: this()
		{
			this.Value = ticket;
		}
	}
}
