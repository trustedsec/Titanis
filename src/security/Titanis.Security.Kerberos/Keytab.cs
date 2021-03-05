using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Security.Kerberos
{
	public class Keytab
	{
		public Keytab()
		{
		}

		public static Keytab LoadFrom(string fileName)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			byte[] bytes = File.ReadAllBytes(fileName);
			bool isValid = (bytes.Length > 2) && (bytes[0] == 5) && (bytes[1] == 2);
			if (!isValid)
				throw new InvalidDataException("The file is not a valid keytab file.");

			int pos = 2;
			List<KeytabEntry> entries = new List<KeytabEntry>();
			while ((pos + 4) < bytes.Length)
			{
				var length = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(pos, 4));
				pos += 4;
				if (length < 0)
				{
					length = -length;
					// Skip the hole
					pos += length;
				}
				else
				{
					var entryBytes = bytes.AsMemory(pos, length);
					ByteMemoryReader reader = new ByteMemoryReader(entryBytes);
					var entry = reader.ReadPduStruct<KeytabEntryRecord>(PduByteOrder.BigEndian);
					//var principal = entry.principal.nameType switch
					//{
					//	NameType.Principal
					//};

					pos += length;
				}
			}

			throw new NotImplementedException();
		}

		public List<KeytabEntry> Entries { get; } = new List<KeytabEntry>();

		public byte[] ToBytes()
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Describes an entry within a <see cref="Keytab"/>.
	/// </summary>
	public class KeytabEntry
	{
		public KeytabEntry(SecurityPrincipal principal, EType encType, byte[] keyBytes)
		{
			ArgumentNullException.ThrowIfNull(principal);
			ArgumentNullException.ThrowIfNull(keyBytes);
			Principal = principal;
			KeyBytes = keyBytes;
		}

		public SecurityPrincipal Principal { get; }
		public byte[] KeyBytes { get; }
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct KeytabEntryRecord
	{
		public KeytabPrincipal principal;
		public uint timestamp;

		[PduIgnore]
		private int _keyVersion;

		private byte KeyVersion8
		{
			get => (byte)this._keyVersion;
			set => this._keyVersion = value;
		}

		public EType encType;
		public ushort keyLength;
		[PduArraySize(nameof(keyLength))]
		public byte[] keyContents;

		partial void OnAfterReadPdu(Titanis.IO.IByteSource writer)
		{
			if (writer.RemainingLength() >= 4)
			{
				var ver = writer.ReadInt32BE();
				if (ver != 0)
					this._keyVersion = ver;
			}
		}

		[PduIgnore]
		public int keyVersion32
		{
			get => this._keyVersion;
			set => this._keyVersion = value;
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct KeytabPrincipal
	{
		public ushort count;
		public KeytabString realm;
		[PduArraySize(nameof(count))]
		public KeytabString[] components;

		public NameType nameType;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct KeytabData
	{
		public ushort length;
		[PduArraySize(nameof(length))]
		public byte[] bytes;
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct KeytabString
	{
		public ushort length;
		[PduString(CharSet.Ansi, nameof(length))]
		public string str;
	}
}
