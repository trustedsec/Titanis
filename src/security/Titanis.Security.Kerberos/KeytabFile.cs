using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Security.Kerberos
{
	public class KeytabFile
	{
		public KeytabFile()
		{
		}

		public static KeytabFile LoadFrom(string fileName)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			byte[] bytes = File.ReadAllBytes(fileName);
			bool isValid = (bytes.Length > 2) && (bytes[0] == 5) && (bytes[1] == 2);
			if (!isValid)
				throw new InvalidDataException("The file is not a valid keytab file.");

			int pos = 2;
			KeytabFile kt = new KeytabFile();
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
					var rec = reader.ReadPduStruct<KeytabEntryRecord>(PduByteOrder.BigEndian);

					SecurityPrincipalName spn;
					if (rec.principal.components.Length == 1)
					{
						if (ServiceClassNames.Krbtgt.Equals(rec.principal.components[0].str, StringComparison.OrdinalIgnoreCase))
						{
							spn = new ServicePrincipalName(rec.principal.components[0].str, rec.principal.realm.str);
						}
						else
						{
							spn = new UserPrincipalName(rec.principal.components[0].str, rec.principal.realm.str, null, rec.principal.nameType);
						}
					}
					else if (rec.principal.components.Length is 2)
					{
						spn = new ServicePrincipalName(
							rec.principal.components[0].str,
							rec.principal.components[1].str
							);
					}
					else if (rec.principal.components.Length is 3)
					{
						spn = new ServicePrincipalName(
							rec.principal.components[0].str,
							[
								rec.principal.components[1].str,
								rec.principal.components[2].str
							]);
					}
					else
						continue;

					KeytabEntry entry = new KeytabEntry(spn, rec.keyVersion32, rec.encType, rec.keyContents);

					kt.Entries.Add(entry);

					pos += length;
				}
			}

			return kt;
		}

		public List<KeytabEntry> Entries { get; } = new List<KeytabEntry>();

		public byte[] ToBytes()
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// Describes an entry within a <see cref="KeytabFile"/>.
	/// </summary>
	public class KeytabEntry
	{
		internal KeytabEntry(
			SecurityPrincipalName principal,
			int kvno,
			EType encType,
			byte[] keyBytes)
		{
			ArgumentNullException.ThrowIfNull(principal);
			ArgumentNullException.ThrowIfNull(keyBytes);
			Principal = principal;
			KeyBytes = keyBytes;
			this.EType = encType;
			this.KeyBytes = keyBytes;
		}

		public SecurityPrincipalName Principal { get; }

		[Browsable(false)]
		public byte[] KeyBytes { get; }

		public int Kvno { get; }

		public EType EType { get; }

		[DisplayName("Key")]
		public string KeyText => this.KeyBytes.ToHexString();
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

		public PrincipalNameType nameType;

		public override string ToString() => string.Join("/", this.components);
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

		public override string ToString() => str;
	}
}
