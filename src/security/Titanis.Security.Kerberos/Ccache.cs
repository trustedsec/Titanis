using KerberosV5Spec2;
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
				realm = new CCacheStringData(ticket.ClientRealm),
				components = new CCacheStringData[]
				{
					new CCacheStringData(ticket.ClientName)
				},
			};
		}

		internal static CCachePrincipal FromSpn(SecurityPrincipalName spn, string realm)
		{
			ArgumentNullException.ThrowIfNull(spn);
			ArgumentException.ThrowIfNullOrEmpty(realm);

			var parts = spn.GetNameParts();
			return new CCachePrincipal()
			{
				version = 4,
				componentCount = parts.Length,
				nameType = spn.NameType,
				realm = new CCacheStringData(realm),
				components = Array.ConvertAll(parts, r => new CCacheStringData(r))
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

		internal CCacheAuthData(PadataType type, CCacheData authData)
		{
			this.authType = type;
			this.authData = authData;
		}
		internal CCacheAuthData(PadataType type, byte[] authData)
		{
			this.authType = type;
			this.authData = new CCacheData(authData);
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial class CCacheCredential
	{
		internal const string ConfigRealm = "X-CACHECONF:";
		internal const string ConfigClass = "krb5_ccache_conf_data";

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
		partial void OnBeforeWritePdu(Titanis.IO.ByteWriter writer)
		{
			this.authDataCount = this.authData?.Length ?? 0;
		}

		[field: PduIgnore]
		public bool IsConfigurationEntry { get; private set; }
		[field: PduIgnore]
		public string? ConfigurationKey { get; set; }
		[field: PduIgnore]
		public string? ConfigurationValue { get; set; }
		[field: PduIgnore]
		public string? ConfigurationClientName { get; set; }

		partial void OnAfterReadPdu<TSource>(TSource writer) where TSource : class, IByteSource
		{
			var server = this.server;
			bool isConfig = (server != null)
				&& (server.componentCount >= 2)
				&& (server.realm.str == ConfigRealm)
				&& (server.components[0].str == ConfigClass);
			if (isConfig)
			{
				this.ConfigurationKey = server.components[1].str;
				this.ConfigurationValue = Encoding.UTF8.GetString(this.ticket.bytes);
				if (server.componentCount > 2)
					this.ConfigurationClientName = server.components[2].str;

				this.IsConfigurationEntry = true;
			}
		}


		internal CCacheData ticket;
		internal CCacheData ticket2;
	}

	partial struct CCacheCredentialList : IPduStruct<byte>
	{
		[PduParameter]
		private byte version;

		internal CCacheCredential[] credentials;

		public void ReadFrom<TSource>(TSource reader, byte version) where TSource : class, IByteSource
		{
			List<CCacheCredential> creds = new List<CCacheCredential>();
			while (reader.RemainingLength() > 0)
			{
				var cred = reader.ReadPduStruct<CCacheCredential, byte>(version);
				creds.Add(cred);
			}

			this.credentials = creds.ToArray();
		}

		public void WriteTo(ByteWriter writer, byte version)
		{
			if (this.credentials != null)
			{
				foreach (var cred in this.credentials)
				{
					writer.WritePduStruct(cred, version);
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
}
