using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.71 - EncodedValue
	struct EncodedValue
	{
		public EncodedValue(CimType cimType, long rawValue)
		{
			this.cimType = cimType;
			this.rawValue = rawValue;
		}

		public static EncodedValue NoValue => new EncodedValue(CimType.None, 0);

		public CimType cimType { get; }

		internal long rawValue;

		public bool IsArray => 0 != (this.cimType & CimType.Array);

		internal static EncodedValue EncodeValue(
			CimType cimType,
			CimSubtype subtype,
			object? value,
			ByteWriter heapWriter)
		{
			// [MS-WMIO] § 2.2.74 - NoValue
			if (value is null)
				return EncodedValue.NoValue;

			Debug.Assert(WmiProperty.CheckValue(cimType, subtype, value));

			if (value is Array arr)
			{
				var elemType = (cimType & ~CimType.Array);
				EncodedValue[] encValues = new EncodedValue[arr.Length];
				for (int i = 0; i < arr.Length; i++)
				{
					var elem = arr.GetValue(i);
					encValues[i] = EncodeValue(elemType, subtype, elem, heapWriter);
				}

				var pos = heapWriter.Position;
				heapWriter.WriteUInt32LE((uint)arr.Length);
				foreach (var elem in encValues)
				{
					heapWriter.WriteEncodedValue(elem);
				}

				return new EncodedValue(cimType, (uint)pos);
			}
			else
			{
				return cimType switch
				{
					CimType.SInt8 => new EncodedValue(cimType, (byte)value),
					CimType.UInt8 => new EncodedValue(cimType, (byte)value),
					CimType.SInt16 => new EncodedValue(cimType, (short)value),
					CimType.UInt16 => new EncodedValue(cimType, (ushort)value),
					CimType.SInt32 => new EncodedValue(cimType, (int)value),
					CimType.UInt32 => new EncodedValue(cimType, (uint)value),
					CimType.SInt64 => new EncodedValue(cimType, (long)value),
					CimType.UInt64 => new EncodedValue(cimType, (long)(ulong)value),
					CimType.Real32 => new EncodedValue(cimType, BitConverter.SingleToInt32Bits((float)value)),
					CimType.Real64 => new EncodedValue(cimType, BitConverter.DoubleToInt64Bits((double)value)),
					// [MS-WMIO] § 2.2.75 - BOOL
					CimType.Boolean => new EncodedValue(cimType, (ushort)((bool)value ? 0xFFFF : 0)),
					CimType.String => new EncodedValue(cimType, heapWriter.WriteHeapString((string)value).Value),
					CimType.DateTime => subtype switch
					{
						CimSubtype.Interval => new EncodedValue(cimType, heapWriter.WriteHeapString(((WmiInterval)value).ToWmiString()).Value),
						_ => new EncodedValue(cimType, heapWriter.WriteHeapString(((WmiDateTime)value).ToWmiString()).Value)
					},
					CimType.Reference => new EncodedValue(cimType, heapWriter.WriteHeapString(((WmiReference)value).Path).Value),
					CimType.Char16 => new EncodedValue(cimType, (char)value),
					CimType.Object => throw new NotImplementedException(),
					_ => throw new NotImplementedException()
				};
			}
		}

		internal object? Resolve(Heap heap, WmiProperty? property, string? tag = null)
		{
			if (this.IsArray)
				return this.ResolveArray(heap, this.cimType & ~CimType.Array);
			else
			{
				var subtype = property?.SubtypeCode ?? CimSubtype.Unspecified;
				return this.cimType switch
				{
					CimType.None => null,
					CimType.Array => throw new NotSupportedException(),
					CimType.SInt8 => (sbyte)this.rawValue,
					CimType.UInt8 => (byte)this.rawValue,
					CimType.SInt16 => (short)this.rawValue,
					CimType.UInt16 => (ushort)this.rawValue,
					CimType.SInt32 => (int)this.rawValue,
					CimType.UInt32 => (uint)this.rawValue,
					CimType.SInt64 => this.rawValue,
					CimType.UInt64 => (ulong)this.rawValue,
					CimType.Real32 => BitConverter.UInt32BitsToSingle((uint)this.rawValue),
					CimType.Real64 => BitConverter.Int64BitsToDouble(this.rawValue),
					CimType.Boolean => 0 != this.rawValue,
					CimType.String => heap.ResolveString(new HeapStringRef((uint)this.rawValue), tag),
					CimType.Char16 => (char)this.rawValue,
					CimType.DateTime => subtype switch
					{
						CimSubtype.Interval => ResolveInterval(heap),
						_ => ResolveDateTime(heap)
					},
					CimType.Reference => ResolveReference(heap),
					CimType.Object => ResolveObject(heap),
				};
			}
		}

		internal static int ValueTableSizeOf(CimType cimType)
		{
			if (0 != (cimType & CimType.Array))
				return 4;
			else
			{
				return cimType switch
				{
					CimType.SInt8
					or CimType.UInt8 => 1,

					CimType.SInt16
					or CimType.UInt16
					or CimType.Boolean
					or CimType.Char16 => 2,

					CimType.SInt32
					or CimType.UInt32
					or CimType.Real32
					or CimType.String
					or CimType.DateTime
					or CimType.Reference
					or CimType.Object => 4,

					CimType.SInt64
					or CimType.UInt64
					or CimType.Real64 => 8,
				};
			}
		}

		private WmiObject? ResolveObject(Heap heap)
		{
			if (this.rawValue == HeapRef.NullHeapRefValue)
				return null;

			// [MS-WMIO] § 2.2.71 - EncodedValue
			var obj = heap.Resolve(new HeapRef((uint)this.rawValue), HeapRangeType.ObjectBlock, WmiReader.ReadEncodedObject);
			return obj;
		}

		// [MS-WMIO] § 2.3.1 - CIM DateTime Type
		private WmiDateTime? ResolveDateTime(Heap heap)
		{
			var str = heap.ResolveString(new HeapStringRef((uint)this.rawValue));
			if (str is null)
				return null;

			// [DMTF-DSP0004] § 2.2.1 - Datetime Type
			// Example: "20250517064000.163045-420"
			var dt = new WmiDateTime(new DateTime(
				// Year
				int.Parse(str[0..4]),
				// Month
				int.Parse(str[4..6]),
				// Day
				int.Parse(str[6..8]),
				// Hour
				int.Parse(str[8..10]),
				// Minutes
				int.Parse(str[10..12]),
				// Seconds
				int.Parse(str[12..14])
				) +
				// A tick is a 100-nanosecond interval
				TimeSpan.FromTicks(int.Parse(str[15..21]) * 10),
				int.Parse(str[21..25])
				);
			return dt;
		}

		private static int ParseIntervalComponent(string value)
		{
			Debug.Assert(value.Length > 0);
			if (value[0] == '*')
				return -1;
			else
				return int.Parse(value);
		}

		//  [DMTF-DSP0004] § 2.2.1 - Datetime Type
		private WmiInterval? ResolveInterval(Heap heap)
		{
			var str = heap.ResolveString(new HeapStringRef((uint)this.rawValue));
			if (str is null)
				return null;

			// [DMTF-DSP0004] § 2.2.1 - Datetime Type
			// Example: "00000000064000.163045-420"
			var dt = new WmiInterval(
				// Days
				ParseIntervalComponent(str[0..8]),
				// Hours
				ParseIntervalComponent(str[8..10]),
				// Minutes
				ParseIntervalComponent(str[10..12]),
				// Seconds
				ParseIntervalComponent(str[12..14]),
				// Microseconds
				ParseIntervalComponent(str[15..21])
				);

#if DEBUG
			Debug.Assert(dt.ToWmiString() == str);
#endif
			return dt;
		}

		// [MS-WMIO] § 2.3.2 - CIM ReferenceType
		private WmiReference? ResolveReference(Heap heap)
		{
			var str = heap.ResolveString(new HeapStringRef((uint)this.rawValue));
			if (str is null)
				return null;

			// TODO: Create a type for references
			return new WmiReference(str);
		}

		// [MS-WMIO] § 2.2.79 - Encoded-Array
		private object[]? ResolveArray(Heap heap, CimType elementType)
		{
			var heapRef = new HeapRef((uint)this.rawValue);
			if (heapRef.IsNull)
				return null;
			else
			{
				var array = heap.Resolve(
					new HeapRef((uint)this.rawValue),
					HeapRangeType.Array,
					reader =>
					{
						var count = reader.ReadUInt32();
						object[] values = new object[count];
						for (int i = 0; i < count; i++)
						{
							var encodedValue = reader.ReadEncodedValue(elementType);
							var value = encodedValue.Resolve(heap, null);
							values[i] = value;
						}
						return values;
					}
					);

				return array;
			}
		}
	}
}