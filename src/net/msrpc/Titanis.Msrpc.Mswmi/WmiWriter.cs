using ms_oaut;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Mswmi.Wmio;

namespace Titanis.Msrpc.Mswmi
{
	static class WmiWriter
	{
		// [MS-WMIO] § 2.2.1 - EncodingUnit
		public static void WriteEncodingUnit(this ByteWriter writer, WmiObject obj)
		{
			if (obj is null) throw new ArgumentNullException(nameof(obj));

#if DEBUG
			var writerArg = writer;
			writer = new ByteWriter();
#endif

			// [MS-WMIO] § 2.2.77 - Signature
			writer.WriteUInt32LE(WmiObjref.EncodingUnitSignature);
			// [MS-WMIO] § 2.2.4 - ObjectEncodingLength
			var offLength = writer.ReserveEncodingLength();
			// [MS-WMIO] § 2.2.5 - ObjectBlock
			var offObjStart = writer.Position;
			writer.WriteObjectBlock(obj);

			// HACK: mofcomp seems to pad with 0x4F trailing bytes
			//writer.Advance(0x4F);

			int offEnd = writer.Position;
			writer.SetPosition(offLength);
			var cbObj = offEnd - offObjStart;
			writer.WriteInt32LE(cbObj);

#if DEBUG
			var reader = new ByteMemoryReader(writer.GetData());
			var encoded = reader.ReadEncodingUnit();
			writerArg.WriteBytes(writer.GetData().ToArray());
#endif
		}

		// [MS-WMIO] § 2.2.5 - ObjectBlock
		public static void WriteObjectBlock(this ByteWriter writer, WmiObject obj)
		{
			writer.WritePduStruct(new ObjectBlock(obj));
		}

		// [MS-WMIO] § 2.2.5 - ObjectFlags
		public static void WriteObjectFlags(this ByteWriter writer, WmiObjectFlags flags)
			=> writer.WriteByte((byte)flags);

		// [MS-WMIO] § 2.2.78 - Encoded-String
		public static bool IsSingleByteString(this string str)
		{
			foreach (var c in str)
			{
				if (c > byte.MaxValue)
					return false;
			}
			return true;
		}

		// [MS-WMIO] § 2.2.68 - HeapStringRef
		public static void WriteStringRef(this ByteWriter writer, string? str, ByteWriter heapWriter)
		{
			if (string.IsNullOrEmpty(str))
			{
				writer.WriteHeapRef(HeapRef.NullHeapRefValue);
			}
			else
			{
				writer.WriteHeapRef(heapWriter.WriteHeapString(str));
			}
		}

		private static int WriteHeapObject(this ByteWriter heapWriter, WmiObject obj)
		{
			if (obj is null) throw new ArgumentNullException(nameof(obj));

			// [MS-WMIO] § 2.2.71 - EncodedValue
			int offLength = heapWriter.ReserveEncodingLength();
			int offObject = heapWriter.Position;
			heapWriter.WriteObjectBlock(obj);
			int offEnd = heapWriter.Position;
			heapWriter.SetPosition(offLength);
			heapWriter.WriteInt32LE(offEnd - offObject);
			heapWriter.SetPosition(offEnd);
			return offLength;
		}
		public static HeapStringRef WriteHeapString(this ByteWriter heapWriter, string? str)
		{
			if (str == null)
				return HeapStringRef.Null;

			int presetIndex = Array.IndexOf(Heap.implicitStrings, str);
			if (presetIndex >= 0)
			{
				return new HeapStringRef((uint)(presetIndex | 0x8000_0000));
			}
			else
			{
				var offHeap = heapWriter.Position;
				heapWriter.WriteEncodedString(str);
				return new HeapStringRef((uint)offHeap);
			}
		}

		// [MS-WMIO] § 2.2.28 - NdTableValueTableLength
		// TOOD: Range check
		internal static void WriteNdTableValueTableLength(this ByteWriter writer, int size) => writer.WriteInt32LE(size);

		// [MS-WMIO] § 2.2.17 - DerivationList
		internal static void WriteDerivationList(this ByteWriter writer, string? baseClass, string[] superclassList)
		{
			superclassList ??= Array.Empty<string>();
			string? baseClassName = baseClass;
			if (!string.IsNullOrEmpty(baseClassName))
				superclassList.Prepend(baseClassName);
			writer.WritePduStruct<DerivationList>(new DerivationList { classNames = superclassList });
		}

		// [MS-WMIO] § 2.2.18 - ClassNameEncoding
		internal static void WriteClassName(this ByteWriter writer, string name)
		{
			writer.WritePduStruct(new ClassNameEncoding(name));
		}

		internal static int ReserveEncodingLength(this ByteWriter writer)
		{
			var pos = writer.Position;
			writer.Advance(4);
			return pos;
		}

		// [MS-WMIO] § 2.2.78 - Encoded-String
		internal static void WriteEncodedString(this ByteWriter writer, string str)
		{
			new Wmio.EncodedString(str).WriteTo(writer);
		}

		// [MS-WMIO] § 2.2.7 - Decoration
		internal static void WriteDecoration(this ByteWriter writer, WmiDecoration decoration)
		{
			ArgumentNullException.ThrowIfNull(decoration);
			writer.WriteEncodedString(decoration.ServerName);
			writer.WriteEncodedString(decoration.NamespaceName);
		}

		// [MS-WMIO] § 2.2.66 - Heap
		public static void WriteHeap(this ByteWriter writer, ReadOnlySpan<byte> heapBytes)
		{
			Debug.Assert(heapBytes.Length <= uint.MaxValue);
			writer.WriteUInt32LE(0x8000_0000 | (uint)heapBytes.Length);
			writer.WriteBytes(heapBytes);
		}

		// [MS-WMIO] § 2.2.25 - PropertyInfoRef
		// [MS-WMIO] § 2.2.30 - PropertyInfo
		public static HeapRef WritePropertyInfo(
			this ByteWriter heapWriter,
			WmiProperty property,
			string[] derivationList)
		{
			int derIndex = Array.IndexOf(derivationList, property.ClassOfOrigin);
			if (derIndex < 0)
				throw new ArgumentException($"The property indicates a class of origin '{property.ClassOfOrigin}', but this class does not appear in the derivation list.");

			ByteWriter propWriter = new ByteWriter();
			PropertyInfo propInfo = new PropertyInfo
			{
				propertyType = property.PropertyTypeInheritedFlag,
				declarationOrder = (ushort)property.DeclarationOrder,
				valueTableOffset = (uint)property.ValueTableOffset,
				classOfOrigin = derIndex,
				qualifierSet = EncodeQualifierSet(property.Qualifiers, heapWriter)
			};
			propWriter.WritePduStruct(propInfo);

			var pos = heapWriter.Position;
			heapWriter.WriteBytes(propWriter.GetData().Span);
			return new HeapRef((uint)pos);
		}

		public static void WritePropertyType(this ByteWriter writer, CimType propertyType)
		{
			writer.WriteByte((byte)propertyType);
		}

		// [MS-WMIO] § 2.2.59 - QualifierSet
		public static void WriteQualifierSet(this ByteWriter writer, IReadOnlyList<WmiQualifier> qualifiers, ByteWriter heapWriter)
		{
			if (qualifiers.IsNullOrEmpty())
			{
				writer.WriteInt32LE(4);
			}
			else
			{
				QualifierSet qualset = EncodeQualifierSet(qualifiers, heapWriter);
				writer.WritePduStruct(qualset);
			}
		}

		private static QualifierSet EncodeQualifierSet(IReadOnlyList<WmiQualifier> qualifiers, ByteWriter heapWriter)
		{
			return new QualifierSet
			{
				qualifiers = qualifiers.Select(r => r.Encode(heapWriter)).ToArray()
			};
		}

		public static void WriteEncodingLengthAt(
			this ByteWriter writer,
			int startOffset)
		{
			int endOffset = writer.Position;
			writer.SetPosition(startOffset);
			int length = endOffset - startOffset;
			writer.WriteInt32LE(length);
			writer.SetPosition(endOffset);
		}

		// [MS-WMIO] § 2.2.71 - EncodedValue
		public static void WriteEncodedValue(
			this ByteWriter writer,
			CimType cimType,
			CimSubtype subtype,
			object value,
			ByteWriter heapWriter
			)
		{
			writer.WriteEncodedValue(EncodedValue.EncodeValue(cimType, subtype, value, heapWriter));
		}

		// [MS-WMIO] § 2.2.71 - EncodedValue
		public static void WriteEncodedValue(this ByteWriter writer, EncodedValue encodedValue)
		{
			var value = encodedValue.rawValue;
			switch (encodedValue.cimType)
			{
				case CimType.None: /* Do nothing */ break;
				case CimType.SInt8: writer.WriteSByte((sbyte)value); break;
				case CimType.UInt8: writer.WriteByte((byte)value); break;
				case CimType.SInt16: writer.WriteInt16LE((short)value); break;
				case CimType.UInt16: writer.WriteUInt16LE((ushort)value); break;
				case CimType.SInt32: writer.WriteInt32LE((int)value); break;
				case CimType.UInt32: writer.WriteUInt32LE((uint)value); break;
				case CimType.SInt64: writer.WriteInt64LE((long)value); break;
				case CimType.UInt64: writer.WriteUInt64LE((ulong)value); break;
				case CimType.Real32: writer.WriteSingle((float)value); break;
				case CimType.Real64: writer.WriteDouble((double)value); break;
				// [MS-WMIO] § 2.2.75 - BOOL
				case CimType.Boolean: writer.WriteUInt16LE((ushort)value); break;
				case CimType.Char16: writer.WriteUInt16LE((char)value); break;
				case CimType.String:
				case CimType.DateTime:
				case CimType.Reference:
				case CimType.Object:
				case var k when (0 != (k & CimType.Array)):
					writer.WriteHeapRef(new HeapRef((uint)value)); break;
				default:
					throw new NotImplementedException();
			}

		}

		// [MS-WMIO] § 2.2.71 - EncodedValue
		public static void WriteEncodedValue(
			this byte[] valueTable,
			int startIndex,
			CimType cimType,
			CimSubtype subtype,
			object? value,
			ByteWriter heapWriter
			)
		{
			// [MS-WMIO] § 2.2.74 - NoValue
			if (value is null) throw new ArgumentNullException(nameof(value));

			Debug.Assert(WmiProperty.CheckValue(cimType, subtype, value));



			if (value is Array arr)
			{
				var enc = EncodedValue.EncodeValue(cimType, subtype, value, heapWriter);
				BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), (int)enc.rawValue);
			}
			else
			{
				switch (cimType)
				{
					case CimType.SInt8: valueTable[startIndex] = (byte)(sbyte)value; break;
					case CimType.UInt8: valueTable[startIndex] = (byte)value; break;
					case CimType.SInt16: BinaryPrimitives.WriteInt16LittleEndian(valueTable.Slice(startIndex, 2), (short)value); break;
					case CimType.UInt16: BinaryPrimitives.WriteUInt16LittleEndian(valueTable.Slice(startIndex, 2), (ushort)value); break;
					case CimType.SInt32: BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), (int)value); ; break;
					case CimType.UInt32: BinaryPrimitives.WriteUInt32LittleEndian(valueTable.Slice(startIndex, 4), (uint)value); break;
					case CimType.SInt64: BinaryPrimitives.WriteInt64LittleEndian(valueTable.Slice(startIndex, 8), (long)value); break;
					case CimType.UInt64: BinaryPrimitives.WriteUInt64LittleEndian(valueTable.Slice(startIndex, 8), (ulong)value); break;
					case CimType.Real32: BinaryPrimitives.WriteSingleLittleEndian(valueTable.Slice(startIndex, 4), (float)value); break;
					case CimType.Real64: BinaryPrimitives.WriteDoubleLittleEndian(valueTable.Slice(startIndex, 8), (double)value); break;
					// [MS-WMIO] § 2.2.75 - BOOL
					case CimType.Boolean: BinaryPrimitives.WriteUInt16LittleEndian(valueTable.Slice(startIndex, 2), (ushort)((bool)value ? 0xFFFF : 0)); break;
					case CimType.String: BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), heapWriter.WriteHeapString((string)value).Value); break;
					case CimType.DateTime:
						BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), heapWriter.WriteHeapString(subtype switch
						{
							CimSubtype.Interval => ((WmiInterval)value).ToWmiString(),
							_ => ((WmiDateTime)value).ToWmiString()
						}).Value); break;
					case CimType.Reference: BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), heapWriter.WriteHeapString(((WmiReference)value).Path).Value); break;
					case CimType.Char16: BinaryPrimitives.WriteUInt16LittleEndian(valueTable.Slice(startIndex, 2), (char)value); break;
					case CimType.Object: BinaryPrimitives.WriteInt32LittleEndian(valueTable.Slice(startIndex, 4), heapWriter.WriteHeapObject((WmiObject)value)); break;
					default:
						break;
				}
			}
		}

		public static void WriteWmiObject(
			this ByteWriter writer,
			WmiObject? obj,
			ByteWriter heapWriter
			)
		{
			throw new NotImplementedException();

			if (obj == null)
				writer.WriteHeapRef(HeapRef.NullHeapRefValue);

			writer.WriteHeapRef(heapWriter.Position);

			int offObj = heapWriter.ReserveEncodingLength();
			obj.EncodeObjectBlockTo(heapWriter);
			heapWriter.WriteEncodingLengthAt(offObj);
		}

		// [MS-WMIO] § 2.2.82 - CimType
		public static void WriteCimType(this ByteWriter writer, CimType cimType)
		{
			writer.WriteUInt32LE((uint)cimType);
		}

		public static void WriteHeapRef(this ByteWriter writer, HeapRef heapRef)
		{
			writer.WriteInt32LE(heapRef.Value);
		}

		public static void WriteHeapRef(this ByteWriter writer, HeapStringRef heapRef)
		{
			writer.WriteInt32LE(heapRef.Value);
		}

		public static void WriteHeapRef(this ByteWriter writer, int heapOffset)
		{
			writer.WriteInt32LE(heapOffset);
		}

		public static void WriteHeapRef(this ByteWriter writer, uint heapOffset)
		{
			writer.WriteUInt32LE(heapOffset);
		}

		public static HeapRef WriteHeapStruct<T>(this ByteWriter writer, ref readonly T struc)
			where T : IPduStruct
		{
			var pos = writer.Position;
			writer.WritePduStruct(struc);
			return new HeapRef((uint)pos);
		}
	}
}
