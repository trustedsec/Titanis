using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{

	// [MS-WMIO] § 2.2.78 - Encoded-String
	enum EncodedStringFlag : byte
	{
		Compressed = 0,
		Utf16 = 1,
	}

	// [MS-WMIO] § 2.2.65 - InstancePropQualifierSet
	enum InstancePropQualsetFlag : byte
	{
		NoProps = 1,
		HasProps = 2,
	}

	static class WmiReader
	{
		// [MS-WMIO] § 2.2.1 - EncodingUnit
		internal static WmiObject ReadEncodingUnit(this ByteMemoryReader reader)
		{
			var encUnit = reader.ReadPduStruct<EncodingUnit>();
			return encUnit.objectBlock.obj;
		}

		// [MS-WMIO] § 2.2.71 - EncodedValue
		internal static WmiObject ReadEncodedObject(this ByteMemoryReader reader)
		{
			return reader.ReadPduStruct<EncodedObject>().objBlock.obj;
		}

		// [MS-WMIO] § 2.2.73 - EncodingLength
		// TODO: Range check
		internal static int ReadEncodingLength(this IByteSource reader) => reader.ReadInt32LE();
		// [MS-WMIO] § 2.2.28 - NdTableValueTableLength
		// TOOD: Range check
		internal static int ReadNdTableValueTableLength(this ByteMemoryReader reader) => reader.ReadInt32LE();

		// [MS-WMIO] § 2.2.66 - Heap
		internal static Heap ReadHeap(this ByteMemoryReader reader)
			=> reader.ReadPduStruct<HeapStruc>().ToHeap();
		internal static Qualifier[] ReadQualifierSet(this ByteMemoryReader reader)
		{
			return reader.ReadPduStruct<QualifierSet>().qualifiers;
		}

		// [MS-WMIO] § 2.2.71 - EncodedValue
		internal static EncodedValue ReadEncodedValue(this IByteSource reader, CimType cimType)
		{
			if (0 != (cimType & CimType.Array)
				|| (cimType is CimType.String or CimType.DateTime or CimType.Reference or CimType.Object))
			{
				// Read as a HeapRef
				return new EncodedValue(cimType, reader.ReadUInt32());
			}
			else
			{
				long value = cimType switch
				{
					CimType.None => 0,
					CimType.SInt8 => reader.ReadSByte(),
					CimType.UInt8 => reader.ReadByte(),
					CimType.SInt16 => reader.ReadInt16LE(),
					CimType.UInt16 => reader.ReadUInt16LE(),
					CimType.SInt32 => reader.ReadInt32LE(),
					CimType.UInt32 => reader.ReadUInt32LE(),
					CimType.SInt64 => reader.ReadInt64(),
					CimType.UInt64 => (long)reader.ReadUInt64(),
					CimType.Real32 => reader.ReadUInt32LE(),
					CimType.Real64 => (long)reader.ReadInt64LE(),
					CimType.Boolean => reader.ReadUInt16LE(),
					CimType.Char16 => (long)reader.ReadUInt16LE(),
				};
				return new EncodedValue(cimType, value);
			}
		}
		// [MS-WMIO] § 2.2.82 - CimType
		internal static CimType ReadCimType(this ByteMemoryReader reader)
		{
			var value = reader.ReadUInt32LE();
			Debug.Assert(value <= ushort.MaxValue);
			return (CimType)value;
		}
		// [MS-WMIO] § 2.2.18 - ClassNameEncoding
		internal static string ReadClassNameEncoding(this ByteMemoryReader reader)
		{
			return reader.ReadPduStruct<ClassNameEncoding>().name.value;
		}
		// [MS-WMIO] § 2.2.15 - ClassPart
		internal static WmiClassObject ReadClassPart(
			this ByteMemoryReader reader,
			WmiClassObject? baseClass)
		{
			var classPart = reader.ReadPduStruct<ClassPart>();
			return classPart.CreateClass(baseClass);
		}
		// [MS-WMIO] § 2.2.14 - ClassAndMethodsPart
		internal static WmiClassObject ReadClassAndMethodsPart(
			this ByteMemoryReader reader,
			WmiClassObject? baseClass)
		{
			var classPart = reader.ReadPduStruct<ClassAndMethodsPart>();
			return classPart.CreateClass(baseClass);
		}

		// [MS-WMIO] § 2.2.26 - NdTable
		internal static NdTable ReadNdTable(this IByteSource reader, int propertyCount)
		{
			var cbNdTable = NdTable.ComputeNdTableLength(propertyCount);
			return new NdTable(reader.ReadBytes(cbNdTable), propertyCount);
		}

		// [MS-WMIO] § 2.2.70 - MethodSignatureBlock
		internal static WmiClassObject? ReadMethodSignatureBlock(this ByteMemoryReader reader)
		{
			MethodSignatureBlock sigblock = reader.ReadPduStruct<MethodSignatureBlock>();
			return (WmiClassObject)sigblock.obj?.obj;
		}

		// [MS-WMIO] § 2.2.11 - ClassType
		internal static WmiClassObject ReadClassType(this ByteMemoryReader reader, WmiDecoration? deco)
		{
			return reader.ReadPduStruct<ClassType>().CreateClass();
		}
	}
}
