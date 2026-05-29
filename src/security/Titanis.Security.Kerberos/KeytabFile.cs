using KerberosV5Spec2;
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
			return LoadFrom(bytes);
		}
		public static KeytabFile LoadFrom(byte[] bytes)
		{
			ArgumentNullException.ThrowIfNull(bytes);

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
					var rec = reader.ReadPduStruct<KeytabEntryRecord>();

					SecurityPrincipalName spn;
					if (rec.principal.components.Length == 1)
					{
						if (ServiceClassNames.Krbtgt.Equals(rec.principal.components[0].str, StringComparison.OrdinalIgnoreCase))
						{
							spn = new ServicePrincipalName(rec.principal.nameType, rec.principal.components[0].str, rec.principal.realm.str);
						}
						else
						{
							spn = new UserPrincipalName(rec.principal.components[0].str, rec.principal.realm.str, null, rec.principal.nameType);
						}
					}
					else if (rec.principal.components.Length is 2)
					{
						spn = new ServicePrincipalName(
							rec.principal.nameType,
							rec.principal.components[0].str,
							rec.principal.components[1].str
							);
					}
					else if (rec.principal.components.Length is 3)
					{
						spn = new ServicePrincipalName(
							rec.principal.nameType,
							rec.principal.components[0].str,
							[
								rec.principal.components[1].str,
								rec.principal.components[2].str
							]);
					}
					else
						continue;

					KeytabEntry entry = new KeytabEntry(spn, rec.principal.realm.str, rec.timestamp, rec.keyVersion32, rec.encType, rec.keyContents);

					kt.Entries.Add(entry);

					pos += length;
				}
			}

			return kt;
		}

		public List<KeytabEntry> Entries { get; } = new List<KeytabEntry>();

		public byte[] ToBytes()
		{
			const int version = 2;

			var writer = new ByteWriter();
			writer.WriteByte(5);
			writer.WriteByte(2);

			foreach (var entry in this.Entries)
			{
				int offStart = writer.Position;
				writer.WriteInt32BE(0);

				writer.WritePduStruct(entry.ToRecord(version));

				int offEnd = writer.Position;
				writer.SetPosition(offStart);
				writer.WriteInt32BE(offEnd - offStart - 4);
				writer.SetPosition(offEnd);
			}

			return writer.GetData().ToArray();
		}
	}

	/// <summary>
	/// Describes an entry within a <see cref="KeytabFile"/>.
	/// </summary>
	public class KeytabEntry
	{
		public KeytabEntry(
			SecurityPrincipalName principal,
			string realm,
			uint timestamp,
			int kvno,
			EType encType,
			byte[] keyBytes)
		{
			ArgumentNullException.ThrowIfNull(principal);
			ArgumentException.ThrowIfNullOrEmpty(realm);
			ArgumentNullException.ThrowIfNull(keyBytes);

			this.Principal = principal;
			this.Realm = realm;
			this.Timestamp = timestamp;
			this.Kvno = kvno;
			this.KeyBytes = keyBytes;
			this.EType = encType;
			this.KeyBytes = keyBytes;
		}

		public SecurityPrincipalName Principal { get; }
		public string Realm { get; }
		public uint Timestamp { get; }

		public int Kvno { get; }

		[Browsable(false)]
		public byte[] KeyBytes { get; }

		public EType EType { get; }

		[DisplayName("Key")]
		public string KeyText => this.KeyBytes.ToHexString();

		internal KeytabEntryRecord ToRecord(int version)
		{
			return new KeytabEntryRecord
			{
				principal = this.Principal.ToKeytabStruct(this.Realm, version),
				timestamp = this.Timestamp,
				keyVersion32 = this.Kvno,
				encType = this.EType,
				keyLength = (ushort)this.KeyBytes.Length,
				keyContents = this.KeyBytes
			};
		}

		public EncryptionKey ToEncryptionKey() => new EncryptionKey((int)this.EType, this.KeyBytes);
	}

	static class Extensions
	{
		internal static KeytabString ToKeytabStruct(this string? str)
		{
			return string.IsNullOrEmpty(str) ? new KeytabString() : new KeytabString { length = (ushort)str.Length, str = str };
		}
		internal static KeytabPrincipal ToKeytabStruct(this SecurityPrincipalName spn, string realm, int version)
		{
			return new KeytabPrincipal
			{
				count = (ushort)((version < 2) ? (1 + spn.NamePartCount) : spn.NamePartCount),
				realm = realm.ToKeytabStruct(),
				components = Array.ConvertAll(spn.GetNameParts(), r => r.ToKeytabStruct()),
				nameType = spn.NameType
			};
		}
	}
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct KeytabEntryRecord
	{
		public KeytabPrincipal principal;
		public uint timestamp;

		[PduIgnore]
		private int _keyVersion;

		[PduField]
		private byte KeyVersion8
		{
			get => (byte)Math.Min(byte.MaxValue, this._keyVersion);
			set => this._keyVersion = value;
		}

		public EType encType;
		public ushort keyLength;
		[PduArraySize(nameof(keyLength))]
		public byte[] keyContents;

		partial void OnAfterReadPdu<TSource>(TSource writer) where TSource : class, IByteSource
		{
			if (writer.RemainingLength() >= 4)
			{
				var ver = writer.ReadInt32BE();
				if (ver != 0)
					this._keyVersion = ver;
			}
		}

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
