using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Smb2.Claims
{
	public struct ResourceClaim
	{
		public ResourceClaim(
			string name,
			string value,
			FsrmPropertyFlags flags
			)
		{
			Name = name;
			Value = value;
			Flags = flags;
		}

		public string Name { get; }
		public string Value { get; }
		public FsrmPropertyFlags Flags { get; }
	}
	public class ResourceClaimsInfo
	{
		private ResourceClaimsInfo()
		{

		}

		public ResourceClaimsInfo(
			DateTime timestamp,
			AdsCacheFlags cacheFlags,
			List<ResourceClaim> props)
		{
			Timestamp = timestamp;
			CacheFlags = cacheFlags;
			Properties = props;
		}

		// [MS-FCIADS] § 1.3 Overview
		public const string AdsName = ":FSRM{ef88c031-5950-4164-ab92-eec5f16005a5}:$DATA";

		public DateTime Timestamp { get; }
		public AdsCacheFlags CacheFlags { get; }
		public List<ResourceClaim> Properties { get; }

		private static void ReverseBits(Span<byte> bytes)
		{
			int i = 0;
			while (i < bytes.Length)
			{
				int rem = bytes.Length - i;
				if (rem >= 4)
				{
					uint value = MemoryMarshal.Read<uint>(bytes.Slice(i, 4));
					value = ((value & 0x55555555) << 1) | ((value & 0xAAAAAAAA) >> 1);
					value = ((value & 0x33333333) << 2) | ((value & 0xCCCCCCCC) >> 2);
					value = ((value & 0x0F0F0F0F) << 4) | ((value & 0xF0F0F0F0) >> 4);
					MemoryMarshal.Write(bytes.Slice(i, 4), value);
					i += 4;
				}
				else
				{
					uint value = bytes[i];
					value = ((value & 0x55) << 1) | ((value & 0xAA) >> 1);
					value = ((value & 0x33) << 2) | ((value & 0xCC) >> 2);
					value = ((value & 0x0F) << 4) | ((value & 0xF0) >> 4);
					bytes[i] = (byte)value;
					i++;
				}
			}
		}

		#region CRC64
		private const ulong Crc64Polynomial = 0x259C84CBA6426349UL;
		private static readonly ulong[] CrcTable = BuildCrc64Table(Crc64Polynomial);

		private static ulong[] BuildCrc64Table(ulong polynomial)
		{
			ulong[] crcTable = new ulong[256];
			for (uint i = 0; i < 256; i++)
			{
				ulong crc = i;
				for (int j = 0; j < 8; j++)
				{
					if ((crc & 1) == 1)
					{
						crc = (crc >> 1) ^ polynomial;
					}
					else
					{
						crc >>= 1;
					}
				}
				crcTable[i] = crc;
			}

			return crcTable;
		}

		public static ulong ComputeCrc64(ReadOnlySpan<byte> buffer)
		{
			ulong crc = 0;
			foreach (byte b in buffer)
			{
				byte index = (byte)((crc ^ b) & 0xFF);
				crc = (crc >> 8) ^ CrcTable[index];
			}
			return crc;
		}
		#endregion

		public static ResourceClaimsInfo DecodeResourceClaims(byte[] bytes)
		{
			ArgumentNullException.ThrowIfNull(bytes);
			if (bytes.Length < 16)
				throw new ArgumentException($"The array does not contain a valid FCIADS claims stream.", nameof(bytes));

			List<ResourceClaim> props = new List<ResourceClaim>();

			ByteMemoryReader reader = new(bytes);
			var hdr = reader.ReadPduStruct<AdsStreamHeader>();
			if (hdr.versionId != AdsStreamHeader.FciadsVersionId)
				throw new ArgumentException($"The array does not contain a valid FCIADS claims stream.", nameof(bytes));

			var computedHash = ComputeCrc64(bytes.Slice((int)hdr.crcStartPos));
			if (computedHash != hdr.crc)
				throw new ArgumentException($"The array does not contain a valid FCIADS claims stream (CRC64 mismatch).", nameof(bytes));

			if (hdr.firstFieldExtensionOffset != 0)
			{
				reader.Position = hdr.firstFieldExtensionOffset;
				while (reader.Position < reader.Length)
				{
					int offExtHeader = reader.Position;
					var exthdr = reader.ReadPduStruct<ADSFieldExtensionHeader>();
					if (exthdr.extensionId == ADSSecurePropertiesExtensionHeader.ExtensionId)
					{
						var propshdr = reader.ReadPduStruct<ADSSecurePropertiesExtensionHeader>();
						for (int i = 0; i < propshdr.propertyCount; i++)
						{
							int offProphdr = reader.Position;
							var prophdr = reader.ReadPduStruct<ADSSecurePropertyHeader>();
							string name = reader.ReadZStringUni();
							reader.Position = offProphdr + prophdr.valueOffset;
							var propdef = reader.ReadZStringUni();
							reader.Position = offProphdr + prophdr.length;

							props.Add(new ResourceClaim(name, propdef, prophdr.flags));
						}
					}

					reader.Position = offExtHeader + exthdr.blockLength;
				}
			}

			return new ResourceClaimsInfo(hdr.Timestamp, hdr.flags, props);
		}
	}

	// [MS-FSRM] § 2.2.1.2.18 AdsCacheFlags
	public enum AdsCacheFlags : uint
	{
		None = 0x00000000,
		Dirty = 0x00000001,
		PropertyFlagsValid = 0x00000002
	}

	// [MS-FCIADS] § 2.1 ADSStreamHeader
	[PduStruct]
	partial struct AdsStreamHeader
	{
		internal static readonly Guid FciadsVersionId = new Guid("43ee0c5f-e038-421c-8a3e-ab4eb1166124");
		internal Guid versionId;
		internal ulong crc;

		[PduPosition]
		internal long crcStartPos;

		internal long timeStampValue;
		internal DateTime Timestamp => DateTime.FromFileTimeUtc(this.timeStampValue);
		internal int streamLength;
		internal int firstFieldExtensionOffset;
		internal AdsCacheFlags flags;
		internal int nonSecurePropertyCount;
		internal long fileHash;
	}

	// [MS-FCIADS] § 2.2 ADSFieldExtensionHeader
	[PduStruct]
	partial struct ADSFieldExtensionHeader
	{
		internal Guid extensionId;
		internal int blockLength;
	}

	// [MS-FCIADS] § 2.3 ADSSecurePropertiesExtensionHeader
	[PduStruct]
	partial struct ADSSecurePropertiesExtensionHeader
	{
		internal static readonly Guid ExtensionId = new Guid("35c8acd4-a0db-426d-85fc-7911cb780e4e");

		internal int propertyCount;
	}

	// [MS-FSRM] § 2.2.1.2.20 FCI_ADS_SECURE_PROPERTY_TYPE
	enum SecurePropertyType : uint
	{
		Int64 = 1,
		String = 2,
	}

	// [MS-FSRM] § 2.2.2.6.1.1 FsrmPropertyFlags
	[Flags]
	public enum FsrmPropertyFlags : uint
	{
		Orphaned = 0x00000001,
		RetrievedFromCache = 0x00000002,
		RetrievedFromStorage = 0x00000004,
		SetByClassifier = 0x00000008,
		Deleted = 0x00000010,
		Reclassified = 0x00000020,
		AggregationFailed = 0x00000040,
		Existing = 0x00000080,
		FailedLoadingProperties = 0x00000100,
		FailedClassifyingProperties = 0x00000200,
		FailedSavingProperties = 0x00000400,
		Secure = 0x00000800,
		PolicyDerived = 0x00001000,
		Inherited = 0x00002000,
		Manual = 0x00004000,
		PropertySourceMask = 0x0000000E
	}
	// [MS-FSRM] § 2.2.1.2.19 AdsCachePropertyFlags
	[Flags]
	enum AdsCachePropertyFlags : uint
	{
		None = 0,
		Manual = 1,
		Deleted = 2,
		PolicyDerived = 4,
		Inherited = 8,
	}

	// [MS-FCIADS] § 2.4 ADSSecurePropertyHeader
	[PduStruct]
	partial struct ADSSecurePropertyHeader
	{
		internal SecurePropertyType secureType;
		internal FsrmPropertyFlags flags;
		internal int length;
		internal int valueOffset;
	}
}
